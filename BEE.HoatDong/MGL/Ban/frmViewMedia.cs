using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using BEE.ThuVien;
using System.Linq;
using System.Net;
using System.Media;
using System.Threading;

namespace BEE.HoatDong.MGL.Ban
{
    public partial class frmViewMedia : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public string filepath { get; set; }

        public frmViewMedia()
        {
            InitializeComponent();
        }


        private void frmViewVideo_Load(object sender, EventArgs e)
        {
            try
            {
                var objconfig = db.tblConfigs.FirstOrDefault(p => p.TypeID == 3);
                string ftppath = objconfig.FtpUrl + filepath;
                string user = objconfig.FtpUser;
                string pass = it.CommonCls.GiaiMa(objconfig.FtpPass);
                if (!System.IO.Directory.Exists("recordfile"))
                    System.IO.Directory.CreateDirectory("recordfile");
                var pp = Application.StartupPath + "\\recordfile" + "/" + DateTime.Now.ToString("ddyyyyMMHHmmssffffff") + ".wav";
                WebClient client = new WebClient();
                client.Credentials = new NetworkCredential(user, pass);
                client.DownloadFile(ftppath, pp);

                axWindowsMediaPlayer2.openPlayer(pp);
                Thread.Sleep(3000);
               // axWindowsMediaPlayer2.Dispose();
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + filepath);
            }

        }
        public static void Delete()
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "\\recordfile");
            foreach (FileInfo file in di.GetFiles())
            {
                try
                {

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    file.Delete();
                }
                catch (Exception ex)
                {

                }


            }

        }
        private void frmViewMedia_FormClosed(object sender, FormClosedEventArgs e)
        {
            Thread th = new Thread(Delete);
            th.Start();
        }
    }
}