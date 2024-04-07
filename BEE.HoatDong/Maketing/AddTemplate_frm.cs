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
    public partial class AddTemplate_frm : DevExpress.XtraEditors.XtraForm
    {
        public List<string> ListEmail = new List<string>();
        //string FileName = "";
        public byte MaThiep = 0;
        public bool IsUpdate = false;
        public AddTemplate_frm()
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

            it.MauThiepCls o = new it.MauThiepCls();
            o.TenThiep = txtTieuDe.Text;
            o.NoiDung = htmlContent.InnerHtml;
            if (MaThiep != 0)
            {
                o.MaThiep = MaThiep;
                o.Update();
            }
            else
                o.Insert();
            DialogBox.Infomation("Dữ liệu đã được cập nhật.");
            IsUpdate = true;
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
            if (MaThiep != 0)
            {
                it.MauThiepCls o = new it.MauThiepCls(MaThiep);
                txtTieuDe.Text = o.TenThiep;
                htmlContent.InnerHtml = o.NoiDung;
            }
        }

        private void htmlContent_ImageBrowser(object sender, ImageBrowserEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn file";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                NghiepVuKhac.InsertImage_frm frm = new NghiepVuKhac.InsertImage_frm();
                frm.FileName = ofd.FileName;
                frm.Directory = "httpdocs/upload/template";
                frm.ShowDialog();
                e.ImageUrl = frm.FileName;              
            }
        }
    }
}