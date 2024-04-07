using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BEEREMA;

namespace BEE.NghiepVuKhac
{
    public partial class ChiTieuBanHang_ctl : DevExpress.XtraEditors.XtraUserControl
    {
        int MaCT = 0;
        public ChiTieuBanHang_ctl()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
        }

        void LoadData()
        {
            it.ChiTieuBanHangCls o = new it.ChiTieuBanHangCls();
            gridControl1.DataSource = o.Select();
            lookUpDuAn.DataSource = o.DuAn.SelectShow();
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = BEE.ThuVien.Common.PerID;
            o.AccessData.Form.FormID = 25;
            DataTable tblAction = o.SelectBy();
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            barButtonItemKyDoanhSo.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            btnThem.Enabled = true;
                            barButtonItemKyDoanhSo.Enabled = true;
                            break;
                        case 2:
                            btnSua.Enabled = true;
                            break;
                        case 3:
                            btnXoa.Enabled = true;
                            break;
                    }
                }
            }
        }

        private void Huong_ctl_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadPermission();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (btnSua.Enabled)
                Edit();
        }

        private void btnNap_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChiTieuBanHang_frm frm = new ChiTieuBanHang_frm();
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHuong) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa chỉ tiêu bán hàng: <" + gridView1.GetFocusedRowCellValue(colTenHuong).ToString() + "> ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    try
                    {
                        it.ChiTieuBanHangCls o = new it.ChiTieuBanHangCls();
                        o.MaCT = byte.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString());
                        o.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì chỉ tiêu bán hàng: <" + gridView1.GetFocusedRowCellValue(colTenHuong).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn chỉ tiêu bán hàng cần xóa. Xin cảm ơn");
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        void Edit()
        {
            if (gridView1.GetFocusedRowCellValue(colMaHuong) != null)
            {
                ChiTieuBanHang_frm frm = new ChiTieuBanHang_frm();
                frm.MaCT = byte.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn chỉ tiêu bán hàng cần sửa. Xin cảm ơn");
        }

        private void barButtonItemKyDoanhSo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHuong) != null)
            {
                KyKinhDoanh_frm frm = new KyKinhDoanh_frm();
                frm.KeyID = byte.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadKyKinhDoanh();
            }
            else
                DialogBox.Infomation("Vui lòng chọn chỉ tiêu bán hàng muốn thêm kỳ doanh số. Xin cảm ơn");
        }

        void LoadKyKinhDoanh()
        {
            it.KyKinhDoanhCls o = new it.KyKinhDoanhCls();
            gridControl2.DataSource = o.Select(MaCT);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHuong) != null)
                MaCT = int.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString());
            else
                MaCT = 0;
            LoadKyKinhDoanh();
        }

        private void barButtonItemXoaKDS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView2.GetFocusedRowCellValue(colMaKKD) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa kỳ doanh số: <" + gridView2.GetFocusedRowCellValue(colSTT).ToString() + "> ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    try
                    {
                        it.KyKinhDoanhCls o = new it.KyKinhDoanhCls();
                        o.ChiTieu.MaCT = int.Parse(gridView2.GetFocusedRowCellValue(colMaKKD).ToString());
                        o.STT = byte.Parse(gridView2.GetFocusedRowCellValue(colSTT).ToString());
                        o.Delete();
                        gridView2.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì kỳ doanh số: <" + gridView2.GetFocusedRowCellValue(colSTT).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn kỳ doanh số cần xóa. Xin cảm ơn");
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView2.GetFocusedRowCellValue(colMaKKD) != null)
            {
                KyKinhDoanh_frm frm = new KyKinhDoanh_frm();
                frm.KeyID = int.Parse(gridView2.GetFocusedRowCellValue(colMaKKD).ToString());
                frm.STT = byte.Parse(gridView2.GetFocusedRowCellValue(colSTT).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadKyKinhDoanh();
            }
            else
                DialogBox.Infomation("Vui lòng chọn kỳ doanh số cần sửa. Xin cảm ơn");
        }

        private void barButtonItemThemKDS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHuong) != null)
            {
                KyKinhDoanh_frm frm = new KyKinhDoanh_frm();
                frm.KeyID = byte.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadKyKinhDoanh();
            }
            else
                DialogBox.Infomation("Vui lòng chọn chỉ tiêu bán hàng muốn thêm kỳ doanh số. Xin cảm ơn");
        }
    }
}
