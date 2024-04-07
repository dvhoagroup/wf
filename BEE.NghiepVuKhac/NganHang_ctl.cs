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
    public partial class NganHang_ctl : DevExpress.XtraEditors.XtraUserControl
    {
        public NganHang_ctl()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
        }

        void LoadData()
        {
            it.NganHangCls o = new it.NganHangCls();
            gridControl1.DataSource = o.Select();
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = BEE.ThuVien.Common.PerID;
            o.AccessData.Form.FormID = 35;
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
            NganHang_frm frm = new NganHang_frm();
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHuong) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa ngân hàng: <" + gridView1.GetFocusedRowCellValue(colTenHuong).ToString() + "> ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    try
                    {
                        it.NganHangCls o = new it.NganHangCls();
                        o.MaNH = byte.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString());
                        o.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì Ngân hàng: <" + gridView1.GetFocusedRowCellValue(colTenHuong).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn ngân hàng cần xóa. Xin cảm ơn");
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
                NganHang_frm frm = new NganHang_frm();
                frm.KeyID = byte.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn ngân hàng cần sửa. Xin cảm ơn");
        }
    }
}
