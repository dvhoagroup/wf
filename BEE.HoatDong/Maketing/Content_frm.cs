using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ConsultingServices.HtmlEditor;
using BEEREMA;

namespace BEE.HoatDong.Maketing
{
    public partial class Content_frm : DevExpress.XtraEditors.XtraForm
    {
        public List<string> ListEmail = new List<string>();
        //string FileName = "";
        int KeySend = 0;
        public Content_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTieuDe.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tiêu đề.");
                txtTieuDe.Focus();
                return;
            }

            if (DialogBox.Question("Bạn có muốn gửi không?") == DialogResult.No) return;
            it.SendMailCls o = new it.SendMailCls();
            o.Contents = htmlContent.InnerHtml;
            o.FileAtach = "";// btnFileAttach.Text;
            o.MailFrom = "";// BEE.ThuVien.Common.MailServer;
            o.NhanVien.MaNV = BEE.ThuVien.Common.StaffID;
            o.SendCount = 0;
            o.SendDate = DateTime.Now;
            o.Title = txtTieuDe.Text;
            KeySend = o.Insert();
            string sendnot = "";
            int count = 0;
            foreach (string mail in ListEmail)
            {
                try
                {
                    MailProviderCls objMail = new MailProviderCls();
                    string[] temp = mail.Split(':');
                    objMail.MailTo = temp[0];
                    objMail.Subject = txtTieuDe.Text;
                    objMail.Content = htmlContent.InnerHtml.Replace("[HoTenKH]", temp[1]);
                    objMail.SendMail();
                    count++;
                }
                catch
                {
                    sendnot += "-\r\t\t" + mail;
                }
            }
            o.KeySend = KeySend;
            o.SendCount = count;
            o.UpdateCount();

            if (sendnot != "")
                DialogBox.Infomation("Đã gửi xong. Danh sách không gửi được: " + sendnot);
            else
                DialogBox.Infomation("Đã gửi thông báo đến email khách hàng đã chọn.");
            this.Close();
        }

        private void btnFileAttach_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Title = "Chọn file";
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    FileName = ofd.FileName.Substring(ofd.FileName.LastIndexOf("\\") + 1);
            //    btnFileAttach.Text = "http://duckhai.vn/upload/alerts/" + FileName;
            //    Khac.InsertImage_frm frm = new BEE.NghiepVuKhac.InsertImage_frm();
            //    frm.FileName = btnFileAttach.Text;
            //    frm.ShowDialog();
            //}
        }

        private void Content_frm_Load(object sender, EventArgs e)
        {
            LoadEmail();
            lookUpEmail.ItemIndex = 0;
            LoadMauThiep();
        }

        void LoadEmail()
        {
            it.ConfigMailCls o = new it.ConfigMailCls();
            o.MaNV = BEE.ThuVien.Common.StaffID;
            lookUpEmail.Properties.DataSource = o.SelectByMaNVEn();
        }

        private void htmlContent_ImageBrowser(object sender, ImageBrowserEventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Title = "Chọn file";
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    e.ImageUrl = "http://duckhai.vn/upload/alerts/" + ofd.FileName.Substring(ofd.FileName.LastIndexOf("\\") + 1);
            //    Khac.InsertImage_frm frm = new BEE.NghiepVuKhac.InsertImage_frm();
            //    frm.FileName = e.ImageUrl;
            //    frm.ShowDialog();
            //}
        }

        private void lookUpEmail_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit _Email = (LookUpEdit)sender;
            //BEE.ThuVien.Common.MailServer = lookUpEmail.GetColumnValue("Server").ToString();
            //BEE.ThuVien.Common.YourMail = lookUpEmail.GetColumnValue("Email").ToString();
            //BEE.ThuVien.Common.MailPass = lookUpEmail.GetColumnValue("Password").ToString();
            //BEE.ThuVien.Common.Save();
        }

        void LoadMauThiep()
        {
            it.MauThiepCls o = new it.MauThiepCls();
            lookUpMauThiep.Properties.DataSource = o.Select();
        }

        private void lookUpMauThiep_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                htmlContent.InnerHtml = lookUpMauThiep.GetColumnValue("NoiDung").ToString();
            }
            catch { }
        }

        private void lookUpMauThiep_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Template_frm frm = new Template_frm();
            frm.ShowDialog();
            LoadMauThiep();
            lookUpMauThiep.EditValue = frm.MaThiep;
            if (frm.MaThiep == 0)
                htmlContent.InnerHtml = "";
        }
    }
}