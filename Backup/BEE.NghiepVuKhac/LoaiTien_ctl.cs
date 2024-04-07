using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BEE.ThuVien;
using System.Linq;
using BEEREMA;

namespace BEE.NghiepVuKhac
{
    public partial class LoaiTien_ctl : DevExpress.XtraEditors.XtraUserControl
    {
        public LoaiTien_ctl()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
        }

        void LoadData()
        {
            it.LoaiTienCls o = new it.LoaiTienCls();
            gridControl1.DataSource = o.Select();
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = BEE.ThuVien.Common.PerID;
            o.AccessData.Form.FormID = 36;
            DataTable tblAction = o.SelectBy();
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            btnThem.Enabled = true;
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
            LoaiTien_frm frm = new LoaiTien_frm();
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHuong) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa loại tiền tệ: <" + gridView1.GetFocusedRowCellValue(colTenHuong).ToString() + "> ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    try
                    {
                        it.LoaiTienCls o = new it.LoaiTienCls();
                        o.MaLoaiTien = byte.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString());
                        o.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì Loại tiền tệ: <" + gridView1.GetFocusedRowCellValue(colTenHuong).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn loại tiền tệ cần xóa. Xin cảm ơn");
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
                LoaiTien_frm frm = new LoaiTien_frm();
                frm.KeyID = byte.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn loại tiền tệ cần sửa. Xin cảm ơn");
        }

        void LoadDetail()
        {
            using (var db = new MasterDataContext())
            {
                gcHistory.DataSource = db.ltTyGia_getBy(byte.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString()));
            }
        }

        private void itemAddHis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHuong) != null)
            {
                var frm = new Rate.frmEdit();
                frm.MaLT = byte.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString());
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadDetail();
            }
            else
                DialogBox.Infomation("Vui lòng chọn loại tiền tệ cần sửa. Xin cảm ơn");
        }

        private void itemEditHis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHuong) != null)
            {
                var frm = new Rate.frmEdit();
                frm.KeyID = Convert.ToInt32(gvHistory.GetFocusedRowCellValue("ID"));
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadDetail();
            }
            else
                DialogBox.Infomation("Vui lòng chọn [Lịch sử], xin cảm ơn");
        }

        private void itemDeleteHis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvHistory.GetFocusedRowCellValue("ID") != null)
            {
                if (DialogBox.Question() == DialogResult.No) return;

                using (var dc = new MasterDataContext())
                {
                    try
                    {
                        var obj = dc.ltTyGias.Single(p => p.ID == Convert.ToInt32(gvHistory.GetFocusedRowCellValue("ID")));
                        dc.ltTyGias.DeleteOnSubmit(obj);
                        dc.SubmitChanges();

                        gvHistory.DeleteSelectedRows();
                    }
                    catch { }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn [Lịch sử], xin cảm ơn");
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }
    }
}
