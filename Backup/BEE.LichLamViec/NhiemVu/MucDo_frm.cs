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
    public partial class MucDo_frm : DevExpress.XtraEditors.XtraForm
    {
        public MucDo_frm()
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
            it.NhiemVu_MucDoCls o = new it.NhiemVu_MucDoCls();
            DataTable tbl = o.Select();
            tbl.Columns["TenMD"].Unique = true;
            tbl.Columns["TenMD"].AllowDBNull = false;
            gridControl1.DataSource = tbl;
            tbl.Dispose();
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = BEE.ThuVien.Common.PerID;
            o.AccessData.Form.FormID = 70;
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
            if (DialogBox.Question("Bạn có chắc chắn xóa <Mức độ> này không?") == DialogResult.Yes)
            {
                try
                {
                    if (gridView1.GetFocusedRowCellValue(colMaMD) != null)
                    {
                        it.NhiemVu_MucDoCls o = new it.NhiemVu_MucDoCls();
                        o.MaMD = int.Parse(gridView1.GetFocusedRowCellValue(colMaMD).ToString());
                        o.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                }
                catch
                {
                    DialogBox.Infomation("Xóa không thành công vì! <Mức độ> này đã được sử dụng.\nVui lòng kiểm tra lại, xin cảm ơn.");
                }
            }
            else
            {
                DialogBox.Infomation("Vui lòng chọn <Mức độ> cần xóa.");
            }
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.FocusedColumn = colMaMD;
            DataTable tblTemp = (DataTable)gridControl1.DataSource;
            it.NhiemVu_MucDoCls o;
            foreach (DataRow r in tblTemp.Rows)
            {
                if (r.RowState == DataRowState.Added)
                {
                    o = new it.NhiemVu_MucDoCls();
                    o.TenMD = r["TenMD"].ToString();
                    o.STT = 0;
                    o.Insert();
                }
                else
                {
                    if (r.RowState == DataRowState.Modified)
                    {
                        o = new it.NhiemVu_MucDoCls();
                        o.MaMD = int.Parse(r["MaMD"].ToString());
                        o.TenMD = r["TenMD"].ToString();
                        o.Update();
                    }
                }
            }

            DialogBox.Infomation("Dữ liệu đã được lưu.");
            tblTemp.Dispose();
        }

        private void gridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            if (e.ErrorText.IndexOf("Column 'TenMD' is constrained to be unique") >= 0)
                DialogBox.Infomation("<Tên mức độ> này đã bị trùng. Vui lòng nhập lại tên khác, xin cảm ơn.");

            //if (e.ErrorText.IndexOf("Column 'MaMD' is constrained to be unique") >= 0)
            //    DialogBox.Infomation("Màu này đã bị trùng. Vui lòng chọn màu khác, xin cảm ơn.");

            if (e.ErrorText == "Column 'TenMD' does not allow nulls")
                DialogBox.Infomation("<Tên mức độ> không được để trống. Vui lòng nhập <Tên mức độ>, xin cảm ơn.");

            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView1.SetFocusedRowCellValue(colMaMD, -1);
        }

        private void gridView1_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            try
            {
                if (gridView1.GetRowCellValue(e.RowHandle, colTenTT).ToString() == "")
                {
                    DialogBox.Infomation("Vui lòng nhập <Tên mức độ>, xin cảm ơn.");
                    e.Allow = false;
                    gridView1.FocusedRowHandle = e.RowHandle;
                }
            }
            catch { }
        }
    }
}