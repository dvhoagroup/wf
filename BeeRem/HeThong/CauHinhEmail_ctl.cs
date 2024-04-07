using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BEEREMA.HeThong
{
    public partial class CauHinhEmail_ctl : UserControl
    {
        public CauHinhEmail_ctl()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            it.ConfigMailCls o = new it.ConfigMailCls();
            o.MaNV = Properties.Settings.Default.StaffID;
            gridControl1.DataSource = o.SelectByMaNV();
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Properties.Settings.Default.PerID;
            o.AccessData.Form.FormID = 17;
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
            //LoadPermission();
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
            CauHinhEmail_frm frm = new CauHinhEmail_frm();
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colEmail) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa email: <" + gridView1.GetFocusedRowCellValue(colEmail).ToString() + "> ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    try
                    {
                        it.ConfigMailCls o = new it.ConfigMailCls();
                        o.Email = gridView1.GetFocusedRowCellValue(colEmail).ToString();
                        o.MaNV = Properties.Settings.Default.StaffID;
                        o.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì email: <" + gridView1.GetFocusedRowCellValue(colEmail).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn chức vụ cần xóa. Xin cảm ơn");
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
            if (gridView1.GetFocusedRowCellValue(colEmail) != null)
            {
                CauHinhEmail_frm frm = new CauHinhEmail_frm();
                frm.KeyID = gridView1.GetFocusedRowCellValue(colEmail).ToString();
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn email cần sửa. Xin cảm ơn");
        }
    }
}
