using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Net;
using BEEREMA;

namespace BEE.NghiepVuKhac
{
   
    public partial class UploadFile_frm : DevExpress.XtraEditors.XtraForm
    {
        public string FileUrl { get; set; }
        public string FileName { get; set; }
        public string Dir { get; set; }
        public string FileSource { get; set; }
        public bool IsUpload { get; set; }
        
        public UploadFile_frm()
        {
            InitializeComponent();
            IsUpload = false;
        }

        private void UploadFile_frm_Load(object sender, EventArgs e)
        {
            it.NhanVienCls o = new it.NhanVienCls();
            o.MaNV = BEE.ThuVien.Common.StaffID;
            //o.KeyCode = it.CommonCls.getKeyCode();
            o.UpdateKeyCode();

            try
            {
                this.Cursor = Cursors.WaitCursor;

               // Uri uri = new Uri(string.Format("{0}/uploadfile.aspx?keycode={1}&dir={2}&fn={3}", BEE.ThuVien.Common.HTTPServer,
               //     o.KeyCode, this.Dir, this.FileName));

                WebClient client = new WebClient();
                client.UploadProgressChanged += new UploadProgressChangedEventHandler(UploadProgressCallback);
                client.UploadFileCompleted += new UploadFileCompletedEventHandler(UploadFileCompleteCallback);
                //client.UploadFileAsync(uri, "POST", this.FileSource);
                FileUrl = this.FileName;
            }
            catch (Exception ex)
            {
                DialogBox.Infomation(ex.StackTrace.ToString());
                IsUpload = false;
                this.Close();
            }
        }

        private void UploadFileCompleteCallback(object sender, UploadFileCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                //DialogBox.Infomation("Có lỗi xảy ra trong quá trình tải. Vui lòng thử lại");
                IsUpload = false;
            }
            else
            {
                IsUpload = true;
            }
            this.Close();
        }

        private void UploadProgressCallback(object sender, UploadProgressChangedEventArgs e)
        {
            lblPross.Text = string.Format("Đã tải lên {0:#,0} trên tổng số {1:#,0} bytes.", e.BytesSent, e.TotalBytesToSend);
            progress.Text = e.ProgressPercentage.ToString();
            progress.Update();
        }
    }
}