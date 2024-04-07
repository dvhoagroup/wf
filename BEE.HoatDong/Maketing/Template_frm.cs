using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.HoatDong.Maketing
{
    public partial class Template_frm : DevExpress.XtraEditors.XtraForm
    {
        public byte MaThiep = 0;
        public bool IsView = false;
        public Template_frm()
        {
            InitializeComponent();
        }

        private void btnChon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaThiep) != null)
            {
                MaThiep = byte.Parse(gridView1.GetFocusedRowCellValue(colMaThiep).ToString());
                this.Close();
            }
        }

        void LoadData()
        {
            it.MauThiepCls o = new it.MauThiepCls();
            gridControl1.DataSource = o.Select();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddTemplate_frm frm = new AddTemplate_frm();
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void Template_frm_Load(object sender, EventArgs e)
        {
            LoadData();
            if (IsView)
                btnChon.Caption = "Đóng";
            else
                btnChon.Caption = "Chọn và Đóng";
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(gridView1.GetFocusedRowCellValue(colMaThiep)!= null){
                AddTemplate_frm frm = new AddTemplate_frm();
                frm.MaThiep = byte.Parse(gridView1.GetFocusedRowCellValue(colMaThiep).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn mẫu thiệp - thư để cập nhật. Xin cảm ơn");
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaThiep) != null)
            {
                AddTemplate_frm frm = new AddTemplate_frm();
                frm.MaThiep = byte.Parse(gridView1.GetFocusedRowCellValue(colMaThiep).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn mẫu thiệp - thư để cập nhật. Xin cảm ơn");
        }

        private void Template_frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaThiep) != null)
            {
                MaThiep = byte.Parse(gridView1.GetFocusedRowCellValue(colMaThiep).ToString());
                this.Close();
            }
        }

        private void btnCapNhat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void btnCapNhatND_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaThiep) != null)
            {
                BEE.NghiepVuKhac.Editor_frm frm = new BEE.NghiepVuKhac.Editor_frm();
                frm.MaThiep = byte.Parse(gridView1.GetFocusedRowCellValue(colMaThiep).ToString());
                frm.LoaiHD = 3;
                frm.ShowDialog();
            }
            else
                DialogBox.Infomation("Vui lòng chọn mẫu thiệp - thư để cập nhật. Xin cảm ơn");
        }
    }
}