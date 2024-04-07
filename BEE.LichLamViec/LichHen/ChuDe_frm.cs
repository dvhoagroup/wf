using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEEREMA.CongViec.LichHen
{
    public partial class ChuDe_frm : DevExpress.XtraEditors.XtraForm
    {
        public ChuDe_frm()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void TinhTrang_frm_Load(object sender, EventArgs e)
        {
            LoadPermission();
            LoadData();
        }

        void LoadData()
        {
            it.LichHen_ChuDeCls o = new it.LichHen_ChuDeCls();
            DataTable tbl = o.Select();
            tbl.Columns["TenCD"].Unique = true;
            tbl.Columns["TenCD"].AllowDBNull = false;
            gridControl1.DataSource = tbl;
            tbl.Dispose();
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = BEE.ThuVien.Common.PerID;
            o.AccessData.Form.FormID = 71;
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
                if (DialogBox.Question("Bạn có chắc chắn xóa <Chủ đề> này không!") == DialogResult.Yes)
                {
                    try
                    {
                        it.LichHen_ChuDeCls o = new it.LichHen_ChuDeCls();
                        o.MaCD = byte.Parse(gridView1.GetFocusedRowCellValue(colMaTT).ToString());
                        o.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì: <Chủ đề> này đã được dùng.\nVui lòng kiểm tra lại, xin cảm ơn.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn <Chủ đề> cần xóa, xin cảm ơn.");
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable tblTemp = (DataTable)gridControl1.DataSource;
            it.LichHen_ChuDeCls o;
            foreach (DataRow r in tblTemp.Rows)
            {
                if (r.RowState == DataRowState.Added)
                {
                    o = new it.LichHen_ChuDeCls();
                    o.TenCD = r["TenCD"].ToString();
                    o.STT = 0;
                    o.Insert();
                }
                else
                {
                    if (r.RowState == DataRowState.Modified)
                    {
                        o = new it.LichHen_ChuDeCls();
                        o.MaCD = int.Parse(r["MaCD"].ToString());
                        o.TenCD = r["TenCD"].ToString();
                        o.STT = 0;
                        o.Update();
                    }
                }
            }

            DialogBox.Infomation("Dữ liệu đã được lưu.");
            tblTemp.Dispose();
            this.Close();
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView1.SetFocusedRowCellValue(colMaTT, 0);
        }

        private void gridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            if (e.ErrorText.IndexOf("Column 'TenCD' is constrained to be unique") >= 0)
                DialogBox.Infomation("<Tên chủ đề> này đã bị trùng. Vui lòng nhập lại tên khác, xin cảm ơn.");

            if (e.ErrorText == "Column 'TenCD' does not allow nulls")
                DialogBox.Infomation("<Tên chủ đề> không được để trống. Vui lòng nhập <Tên chủ đề>, xin cảm ơn.");

            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gridView1_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            try
            {
                if (gridView1.GetRowCellValue(e.RowHandle, colTenTT).ToString() == "")
                {
                    DialogBox.Infomation("Vui lòng nhập <Tên chủ đề>, xin cảm ơn.");
                    e.Allow = false;
                    gridView1.FocusedRowHandle = e.RowHandle;
                }
            }
            catch { }
        }
    }
}