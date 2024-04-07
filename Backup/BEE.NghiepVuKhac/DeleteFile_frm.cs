using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Net;
using System.Threading;

namespace BEE.NghiepVuKhac
{
   
    public partial class DeleteFile_frm : DevExpress.XtraEditors.XtraForm
    {
        public string FileUrl { get; set; }

        public DeleteFile_frm()
        {
            InitializeComponent();
        }

        private void UploadFile_frm_Load(object sender, EventArgs e)
        {
            //DialogBox.ShowWaitDialog("Vui lòng đợi trong giây lát...", "Hệ thống đang xử lý");
            it.NhanVienCls o = new it.NhanVienCls();
            o.MaNV = BEE.ThuVien.Common.StaffID;
            //o.KeyCode = it.CommonCls.getKeyCode();
            o.UpdateKeyCode();

            WebBrowser wb = new WebBrowser();
            //wb.Navigate(new Uri(string.Format("{0}/DeleteFile.aspx?filePath={1}&keycode={2}", BEE.ThuVien.Common.HTTPServer, FileUrl, o.KeyCode)));
            this.Controls.Add(wb);

            Thread.Sleep(100);
            //try
            //{
            //    DialogBox.CloseWaitDialog();
            //}
            //catch { }
            //finally
            //{
            //    DialogBox.Dispose();
            //    System.GC.Collect();
            //}
            this.Close();
        }
    }
}