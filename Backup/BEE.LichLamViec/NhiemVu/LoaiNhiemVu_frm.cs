using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEEREMA.CongViec.NhiemVu
{
    public partial class LoaiNhiemVu_frm : DevExpress.XtraEditors.XtraForm
    {
        public LoaiNhiemVu_frm()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this, barManager1);
        }

        private void TinhTrang_frm_Load(object sender, EventArgs e)
        {
            LoadPermission();
            LoadData();
        }

        void LoadData()
        {
            it.NhiemVu_LoaiCls o = new it.NhiemVu_LoaiCls();
            DataTable tbl = o.Select();
            tbl.Columns["TenLNV"].Unique = true;
            tbl.Columns["TenLNV"].AllowDBNull = false;
            gridControl1.DataSource = tbl;
            tbl.Dispose();
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = BEE.ThuVien.Common.PerID;
            o.AccessData.Form.FormID = 68;
            DataTable tblAction = o.SelectBy();
            gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            gridView1.OptionsBehavior.Editable = false;
            btnXoa.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
                            break;
                        case 2:
                            gridView1.OptionsBehavior.Editable = true;
                            break;
                        case 3:
                            btnXoa.Enabled = true;
                            break;
                    }
                }
            }
        }

        private void btnDOng_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaTT) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn xóa <Loại nhiệm vụ> này không?") == DialogResult.Yes)
                {
                    try
                    {
                        it.NhiemVu_LoaiCls o = new it.NhiemVu_LoaiCls();
                        o.MaLNV = byte.Parse(gridView1.GetFocusedRowCellValue(colMaTT).ToString());
                        o.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì: <Loại nhiệm vụ> này đã được sử dụng.\nVui lòng kiểm tra lại, xin cảm ơn.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn <Loại nhiệm vụ> cần xóa.");
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable tblTemp = (DataTable)gridControl1.DataSource;
            it.NhiemVu_LoaiCls o;
            foreach (DataRow r in tblTemp.Rows)
            {
                if (r.RowState == DataRowState.Added)
                {
                    o = new it.NhiemVu_LoaiCls();
                    o.TenLNV = r["TenLNV"].ToString();
                    o.STT = 0;
                    o.Insert();
                }
                else
                {
                    if (r.RowState == DataRowState.Modified)
                    {
                        o = new it.NhiemVu_LoaiCls();
                        o.MaLNV = byte.Parse(r["MaLNV"].ToString());
                        o.TenLNV = r["TenLNV"].ToString();
                        o.STT = 0;
                        o.Update();
                    }
                }
            }

            DialogBox.Infomation("Dữ liệu đã được lưu.");
            tblTemp.Dispose();
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView1.SetFocusedRowCellValue(colMaTT, 0);
        }

        private void gridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            if (e.ErrorText.IndexOf("Column 'TenLNV' is constrained to be unique") >= 0)
                DialogBox.Infomation("<Tên loại nhiệm vụ> này đã bị trùng. Vui lòng nhập lại tên khác, xin cảm ơn.");

            if (e.ErrorText == "Column 'TenLNV' does not allow nulls")
                DialogBox.Infomation("<Tên loại nhiệm vụ> không được để trống. Vui lòng nhập <Tên loại nhiệm vụ>, xin cảm ơn.");

            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gridView1_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            try
            {
                if (gridView1.GetRowCellValue(e.RowHandle, colTenTT).ToString() == "")
                {
                    DialogBox.Infomation("Vui lòng nhập <Tên loại nhiệm vụ>, xin cảm ơn.");
                    e.Allow = false;
                    gridView1.FocusedRowHandle = e.RowHandle;
                }
            }
            catch { }
        }
    }
}