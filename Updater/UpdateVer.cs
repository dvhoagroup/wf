using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Net;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Diagnostics;
using System.Security.Principal;
using System.Security.AccessControl;

namespace Updater
{
    public partial class UpdateVer : DevExpress.XtraEditors.XtraForm
    {
        string NewVersion = "";
        string oldVersion = "";


        public UpdateVer()
        {
            InitializeComponent();
        }

        void RepairUpdater()
        {
            string PathFile = Application.StartupPath + "\\uninsep.bat";
            string[] cmdLines = {":Repeat",
                "del \"" + Application.StartupPath + "\\updater.exe\"",
                "if exist \"" + Application.StartupPath + "\\updater.exe\" goto Repeat ",
                "rename \"" + Application.StartupPath + "\\updaternew.exe\" \"updater.exe\"",
                "del \"" + PathFile +  "\";" };

            System.IO.FileStream stream = System.IO.File.Create(PathFile);
            stream.Close();
            System.IO.File.WriteAllLines(PathFile, cmdLines);
            Process pLS = new Process();
            pLS.StartInfo.FileName = PathFile;
            pLS.StartInfo.CreateNoWindow = true;
            pLS.StartInfo.UseShellExecute = false;
            try
            {
                pLS.Start();
            }
            catch
            {
            }
        }

        private void Permission()
        {
            string User = System.Environment.UserDomainName + "\\" + Environment.UserName;
            DirectoryInfo myDirectoryInfo = new DirectoryInfo(Application.StartupPath);
            DirectorySecurity myDirectorySecurity = myDirectoryInfo.GetAccessControl();
            myDirectorySecurity.AddAccessRule(new FileSystemAccessRule(User,
                FileSystemRights.FullControl,
                InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                PropagationFlags.None,
                AccessControlType.Allow));
            myDirectoryInfo.SetAccessControl(myDirectorySecurity);
            myDirectoryInfo = null;
        }

        private void UpdateVer_Load(object sender, EventArgs e)
        {
            // var oldVersion = "";
            // var newVersion = "";
            try
            {
                Permission();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn cần thiết lập quyền ghi tập tin cho thư mục cài đặt trước khi cập nhật phiên bản.\n "
                        + ex.Message, "HOALAND", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Process.Start(Application.StartupPath);
                this.Close();
                System.Environment.Exit(1);
                return;
            }

            try
            {
                lblStatus.Text = "Looking for new updates";
                this.Update();

                System.Net.WebClient client = new System.Net.WebClient();
                client.Encoding = Encoding.UTF8;
                NewVersion = client.DownloadString("http://noibo.hoaland.com.vn/version19/version.txt");//.Trim();
                NewVersion = NewVersion.Split('|')[0].Trim();
                oldVersion = System.IO.File.ReadAllText(Application.StartupPath + "\\version.txt", Encoding.UTF8);
                oldVersion = oldVersion.Split('|')[0].Trim();
                oldVersion = oldVersion.Trim();

                //try
                //{
                //    string pathVersion = Application.StartupPath + "\\version.txt";
                //    StreamWriter sw = new StreamWriter(pathVersion);
                //    sw.WriteLine(NewVersion);
                //    sw.Close();
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Bạn cần thiết lập quyền ghi tập tin cho thư mục cài đặt trước khi cập nhật phiên bản.\n "
                //        + ex.Message, "HOALAND", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    Process.Start(Application.StartupPath);
                //    this.Close();
                //    System.Environment.Exit(1);
                //    return;
                //}

                if (NewVersion == oldVersion)
                {
                    lblStatus.Text = "You already have the lastest version of HOALAND installed!";
                    btnCancel.Text = "Exit";
                    return;
                }


                //foreach (Process p in Process.GetProcesses())
                //{
                //    if ((p.ProcessName.ToLower() == "BEEREMAHOALAND"))
                //        p.Kill();
                //}


                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadDataCompleted += new DownloadDataCompletedEventHandler(client_DownloadDataCompleted);
                client.DownloadDataAsync(new Uri("http://noibo.hoaland.com.vn/version19/version.zip"));



            }
            catch
            {
                lblStatus.Text = "You already have the lastest version of HOALAND installed!";
                btnCancel.Text = "Exit";

            }
        }

        void client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                if (e.Error == null)
                {
                    lblStatus.Text = "Installing...";
                    progress.Position = 0;
                    progress.Properties.Step = 1;

                    ZipInputStream zipIn = new ZipInputStream(new MemoryStream(e.Result));
                    ZipEntry entry;

                    int fileCount = 0;
                    while ((entry = zipIn.GetNextEntry()) != null)
                    {
                        fileCount++;
                    }
                    progress.Properties.Maximum = fileCount;

                    zipIn = new ZipInputStream(new MemoryStream(e.Result));
                    while ((entry = zipIn.GetNextEntry()) != null)
                    {
                        progress.PerformStep();
                        this.Update();

                        string path = Application.StartupPath + "\\" + (entry.Name.ToLower() != "updater.exe" ? entry.Name : "updaternew.exe");

                        if (entry.IsDirectory)
                        {
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            continue;
                        }

                        //File.Delete(AppDir + entry.Name);
                        FileStream streamWriter = File.Create(path);
                        long size = entry.Size;
                        byte[] data = new byte[size];
                        while (true)
                        {
                            size = zipIn.Read(data, 0, data.Length);
                            if (size > 0) streamWriter.Write(data, 0, (int)size);
                            else break;
                        }
                        streamWriter.Close();

                        if (entry.Name.ToLower() == "updater.exe")
                            RepairUpdater();
                    }
                    //
                    Properties.Settings.Default.Version = NewVersion;
                    Properties.Settings.Default.Save();

                    lblStatus.Text = "Installation is complete.";
                    this.DialogResult = DialogResult.OK;
                    try
                    {
                        string pathVersion = Application.StartupPath + "\\version.txt";
                        StreamWriter sw = new StreamWriter(pathVersion);
                        sw.WriteLine(NewVersion);
                        sw.Close();



                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Bạn cần thiết lập quyền ghi tập tin cho thư mục cài đặt trước khi cập nhật phiên bản.\n "
                            + ex.Message, "HOALAND", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Process.Start(Application.StartupPath);
                        this.Close();
                        System.Environment.Exit(1);
                        return;
                    }

                    this.Close();

                  
                }
                else
                {
                    MessageBox.Show("An error occurred during the download process.", "HOALAND", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {

                string pathVersion = Application.StartupPath + "\\version.txt";
                StreamWriter sw = new StreamWriter(pathVersion);
                sw.WriteLine(oldVersion);
                sw.Close();
            }
          
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            lblStatus.Text = string.Format("Downloading: {0:#,0} byte / {1:#,0} byte", e.BytesReceived, e.TotalBytesToReceive);

            double Position = Convert.ToDouble(e.BytesReceived) / Convert.ToDouble(e.TotalBytesToReceive) * 100;
            progress.Position = Convert.ToInt32(Math.Round(Position, 0));
            progress.Update();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {          
            this.Close();
        }

        private void UpdateVer_FormClosed(object sender, FormClosedEventArgs e)
        {
            oldVersion = System.IO.File.ReadAllText(Application.StartupPath + "\\version.txt", Encoding.UTF8);
            oldVersion = oldVersion.Split('|')[0].Trim();
            oldVersion = oldVersion.Trim();
            if (NewVersion == oldVersion)
                Process.Start(Application.StartupPath + "\\BEEREMAHOALAND.exe");
        }
    }
}