using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BEEREMA;

namespace BEE.KhachHang
{
    public partial class NguoiMoiGioi_ctl : UserControl
    {
        public NguoiMoiGioi_ctl()
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

        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnRose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConfigRose_frm frm = new ConfigRose_frm();
            frm.ShowDialog();
        }

        private void btnImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}
