using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ConsultingServices.HtmlEditor;
using BEE.NghiepVuKhac;
using BEEREMA;

namespace BEE.HoatDong.Maketing
{
    public partial class HappyBirthdaySetting_frm : DevExpress.XtraEditors.XtraForm
    {
        string OldFileName = "";
        public HappyBirthdaySetting_frm()
        {
            InitializeComponent();
        }

        private void lookUpMauThiep_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Template_frm frm = new Template_frm();
            frm.ShowDialog();            
            LoadMauThiep();
            lookUpMauThiep.EditValue = frm.MaThiep;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadNhomKH()
        {
            it.NhomKHCls o = new it.NhomKHCls();
            checkedCmbNhomKH.Properties.DataSource = o.Select();
        }

        void LoadMauThiep()
        {
            it.MauThiepCls o = new it.MauThiepCls();
            lookUpMauThiep.Properties.DataSource = o.Select();
        }

        private void HappyBirthdaySetting_frm_Load(object sender, EventArgs e)
        {
            LoadNhomKH();
            LoadMauThiep();

            it.SetHappyBirthdayCls o = new it.SetHappyBirthdayCls(1);
            txtTieuDe.Text = o.TieuDe;
            lookUpMauThiep.EditValue = o.MaThiep;
            spinNgayGui.EditValue = o.SoNgay;
            btnFileAttach.Text = o.FileAttach;
            htmlContent.InnerHtml = o.NoiDung;
            checkedCmbNhomKH.EditValue = o.MaNKH2;
            checkedCmbNhomKH.DragDrop += new DragEventHandler(DrapItem);
            checkedCmbNhomKH.Focus();
            txtTieuDe.Focus();
        }

        void DrapItem(object sender, DragEventArgs e)
        {
            checkedCmbNhomKH.ShowPopup();            
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            it.SetHappyBirthdayCls o = new it.SetHappyBirthdayCls();
            o.FileAttach = btnFileAttach.Text;
            o.MaNHK = checkedCmbNhomKH.Properties.GetCheckedItems().ToString();
            o.MaThiep = byte.Parse(lookUpMauThiep.EditValue.ToString());
            o.NoiDung = htmlContent.InnerHtml;
            o.SetID = 1;
            o.SoNgay = byte.Parse(spinNgayGui.EditValue.ToString());
            o.TieuDe = txtTieuDe.Text;
            o.MaNKH2 = checkedCmbNhomKH.Text;
            o.GiaHan = 0;
            o.Update();

            if (OldFileName != btnFileAttach.Text.Trim())
            {
                //Xoa file cu
                it.FTPCls objFTP = new it.FTPCls();
                objFTP.Delete(OldFileName);
            }

            DialogBox.Infomation("Dữ liệu đã được cập nhật.");
            this.Close();
        }

        private void lookUpMauThiep_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                htmlContent.InnerHtml = lookUpMauThiep.GetColumnValue("NoiDung").ToString();
            }
            catch { }
        }

        private void HappyBirthdaySetting_frm_Shown(object sender, EventArgs e)
        {
            
        }

        private void btnFileAttach_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn file";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                InsertImage_frm frm = new InsertImage_frm();
                frm.FileName = ofd.FileName;
                frm.Directory = "httpdocs/upload/marketing/birthday";
                frm.ShowDialog();
                btnFileAttach.Text = frm.FileName;
            }
        }

        private void htmlContent_ImageBrowser(object sender, ImageBrowserEventArgs e)
        {
            OldFileName = btnFileAttach.Text;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn file";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                InsertImage_frm frm = new InsertImage_frm();
                frm.FileName = ofd.FileName;
                frm.Directory = "httpdocs/upload/marketing/birthday";
                frm.ShowDialog();
                e.ImageUrl = frm.FileName;
            }
        }
    }
}