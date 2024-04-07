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
    public partial class NhacNoSetting_frm : DevExpress.XtraEditors.XtraForm
    {
        string OldFileName = "";
        public NhacNoSetting_frm()
        {
            InitializeComponent();
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

        private void HappyBirthdaySetting_frm_Load(object sender, EventArgs e)
        {
            LoadNhomKH();

            it.SetHappyBirthdayCls o = new it.SetHappyBirthdayCls(2);
            txtTieuDe.Text = o.TieuDe;
            spinNgayGui.EditValue = o.SoNgay;
            htmlContent.InnerHtml = o.NoiDung;
            checkedCmbNhomKH.EditValue = o.MaNKH2;
            dateEdit1.EditValue = o.ThoiGianGui;
        }

        void DrapItem(object sender, DragEventArgs e)
        {
            checkedCmbNhomKH.ShowPopup();            
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            it.SetHappyBirthdayCls o = new it.SetHappyBirthdayCls();
            o.MaNHK = checkedCmbNhomKH.Properties.GetCheckedItems().ToString();
            o.NoiDung = htmlContent.InnerHtml;
            o.SetID = 2;
            o.SoNgay = byte.Parse(spinNgayGui.EditValue.ToString());
            o.TieuDe = txtTieuDe.Text;
            o.MaNKH2 = checkedCmbNhomKH.Text;
            o.FileAttach = "";
            o.ThoiGianGui = dateEdit1.DateTime;
            o.GiaHan = byte.Parse(spinGiaHan.EditValue.ToString());
            o.Update();

            DialogBox.Infomation("Dữ liệu đã được cập nhật.");
            this.Close();
        }

        private void lookUpMauThiep_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void HappyBirthdaySetting_frm_Shown(object sender, EventArgs e)
        {
            
        }

        private void htmlContent_ImageBrowser(object sender, ImageBrowserEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn file";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                BEE.NghiepVuKhac.InsertImage_frm frm = new BEE.NghiepVuKhac.InsertImage_frm();
                frm.FileName = ofd.FileName;
                frm.Directory = "httpdocs/upload/marketing/nhacno";
                frm.ShowDialog();
                e.ImageUrl = frm.FileName;
            }
        }
    }
}