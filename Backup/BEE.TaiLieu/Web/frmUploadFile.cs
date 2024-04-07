using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Net;
using System.Linq;
using BEE.ThuVien;

namespace BEEREMA.Web
{
   
    public partial class frmUploadFile : DevExpress.XtraEditors.XtraForm
    {
        public frmUploadFile()
        {
            InitializeComponent();
        }

        public string WebSite { get; set; }
        public string Directory { get; set; }
        public string ClientPath { get; set; }
        public string FileName { get { return ClientPath.Substring(ClientPath.LastIndexOf(@"\") + 1); } }
        public string ServerPath { get { return WebSite + Directory.TrimStart('~') + FileName; } }

        public bool SelectFile(bool isImg)
        {
            var file = new OpenFileDialog();
            if (isImg)
                file.Filter = "Image (.jpg, .gif, .png)|*.jpg;*.gif;*.png";
            else
                file.Filter = "Word(.doc, .docx)|*.doc;*.docx|Excel(.xls,.xlsx)|*.xls;*.xlsx|Winrar(.rar, .zip)|*.rar;*.zip|Video(FLV)|*.flv|Flash|*.swf|All file|*.*";
            if (file.ShowDialog() == DialogResult.OK)
            {
                this.ClientPath = file.FileName;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void UploadFile_frm_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                var keyCode = it.CommonCls.RandomString(30, true);
                using (var db = new MasterDataContext())
                {
                    this.WebSite = db.webConfigs.SingleOrDefault().WebSite;
                    db.NhanViens.Single(p => p.MaNV == Common.StaffID).KeyCode = keyCode;
                    db.SubmitChanges();
                }
                Uri uri = new Uri(string.Format("{0}/service/uploadfile.aspx?keycode={1}&dir={2}&fn={3}",
                    this.WebSite, keyCode, this.Directory, this.FileName));

                WebClient client = new WebClient();
                client.UploadProgressChanged += new UploadProgressChangedEventHandler(UploadProgressCallback);
                client.UploadFileCompleted += new UploadFileCompletedEventHandler(UploadFileCompleteCallback);
                client.UploadFileAsync(uri, "POST", this.ClientPath);
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.StackTrace.ToString());
                this.Close();
            }
        }

        private void UploadFileCompleteCallback(object sender, UploadFileCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                DialogBox.Error("Có lỗi xảy ra trong quá trình tải. Vui lòng thử lại");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
            this.Close();
        }

        private void UploadProgressCallback(object sender, UploadProgressChangedEventArgs e)
        {
            lblPross.Text = string.Format("Đã tải lên {0:#,0} trên tổng số {1:#,0} bytes...", e.BytesSent, e.TotalBytesToSend);
            progress.Text = e.ProgressPercentage.ToString();
            progress.Update();
        }
    }
}