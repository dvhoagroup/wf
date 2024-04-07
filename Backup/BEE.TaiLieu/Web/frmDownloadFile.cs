using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Security.AccessControl;
namespace BEEREMA.Web
{
   
    public partial class frmDownloadFile : DevExpress.XtraEditors.XtraForm
    {
        public frmDownloadFile()
        {
            InitializeComponent();
        }

        public string FileUrl { get; set; }
        public string FileName { get { return FileUrl.Substring(FileUrl.LastIndexOf("/") + 1); } }
        public bool SaveAs()
        {
            using (var frm = new SaveFileDialog())
            {
                frm.FileName = this.FileName;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    SavePath = frm.FileName;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private string SavePath { get; set; }

        private void UploadFile_frm_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadDataCompleted += new DownloadDataCompletedEventHandler(client_DownloadDataCompleted);
                client.DownloadDataAsync(new Uri(this.FileUrl));
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.StackTrace.ToString());
                this.Close();
            }
        }

        void client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var filePath = this.SavePath;
                if (this.SavePath == null)
                {
                    var dirPath = Application.StartupPath + "\\cach\\";
                    if (!Directory.Exists(dirPath))
                        Directory.CreateDirectory(dirPath);
                    filePath = dirPath + it.CommonCls.TiegVietKhongDau(this.FileName);
                }

                if (File.Exists(filePath))
                {
                    filePath = filePath.Insert(filePath.LastIndexOf('.'), DateTime.Now.ToString("hhmmss"));
                }

                FileStream fileStream = File.Create(filePath);
                fileStream.Write(e.Result, 0, e.Result.Length);
                fileStream.Close();

                if (this.SavePath == null)
                {
                    Process.Start(filePath);
                }
            }
            else
            {
                DialogBox.Error("Đã xảy ra lỗi trong quá trình tải xuống");
            }

            this.Close();
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            lblPross.Text = string.Format("Đang tải: {0:#,0} byte / {1:#,0} byte", e.BytesReceived, e.TotalBytesToReceive);

            double Position = Convert.ToDouble(e.BytesReceived) / Convert.ToDouble(e.TotalBytesToReceive) * 100;
            progress.Position = Convert.ToInt32(Math.Round(Position, 0));
            progress.Update();
        }
    }
}