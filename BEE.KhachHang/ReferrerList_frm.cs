using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.KhachHang
{
    public partial class ReferrerList_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaNMG = 0;
        public string HoTen = "";
        public ReferrerList_frm()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            it.NguoiMoiGioiCls o = new it.NguoiMoiGioiCls();
            gridControl3.DataSource = o.Select();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NguoiMoiGioi_frm frm = new NguoiMoiGioi_frm();
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void NguoiMoiGioi_ctl_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView3.GetFocusedRowCellValue(colMaNMG) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa người giới thiệu này ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    try
                    {
                        it.NguoiMoiGioiCls o = new it.NguoiMoiGioiCls();
                        o.MaNMG = int.Parse(gridView3.GetFocusedRowCellValue(colMaNMG).ToString());
                        o.Delete();
                    }
                    catch
                    {
                        DialogBox.Infomation("Người giới thiệu này đã được sử dụng. Vui lòng kiểm tra lại");
                    }
                }
            }
        }

        void Edit()
        {
            if (gridView3.GetFocusedRowCellValue(colMaNMG) != null)
            {
                NguoiMoiGioi_frm frm = new NguoiMoiGioi_frm();
                frm.MaNMG = int.Parse(gridView3.GetFocusedRowCellValue(colMaNMG).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn người giới thiệu muốn cập nhật thông tin");
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void btnChon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView3.GetFocusedRowCellValue(colMaNMG) != null)
            {
                HoTen = gridView3.GetFocusedRowCellValue(colHoTen).ToString();
                MaNMG = int.Parse(gridView3.GetFocusedRowCellValue(colMaNMG).ToString());
                this.Close();
            }
            else
                DialogBox.Infomation("Vui lòng chọn người giới thiệu.");
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}