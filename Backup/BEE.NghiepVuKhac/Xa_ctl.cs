using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using System.Linq;
using BEEREMA;

namespace BEE.NghiepVuKhac
{
    public partial class Xa_ctl : XtraUserControl
    {
        byte MaTinh = 0;
        //short MaHuyen = 0;
        public Xa_ctl()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
        }

        void LoadData()
        {
            it.XaCls objXa = new it.XaCls();
            gridControl1.DataSource = objXa.Select(MaTinh);

            gridView1.ExpandAllGroups();
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

        void LoadHuyen()
        {
            it.HuyenCls o = new it.HuyenCls();
            lookUpHuyen2.DataSource = o.Select();
        }

        private void Huong_ctl_Load(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                lookUpProvince.DataSource = db.Tinhs.ToList();
            }
            LoadData();
            LoadHuyen();
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
            Xa_frm frm = new Xa_frm();
            //frm.MaHuyen = short.Parse(lookUpHuyen.EditValue.ToString());
            frm.MaTinh = byte.Parse(itemProvince.EditValue.ToString());
            MaTinh = byte.Parse(itemProvince.EditValue.ToString());
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHuong) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa xã: <" + gridView1.GetFocusedRowCellValue(colTenHuong).ToString() + "> ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    try
                    {
                        it.XaCls o = new it.XaCls();
                        o.MaXa = byte.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString());
                        o.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì Xã (Phường): <" + gridView1.GetFocusedRowCellValue(colTenHuong).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn xã cần xóa. Xin cảm ơn");
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
                Xa_frm frm = new Xa_frm();
                frm.KeyID = int.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString());
                frm.MaTinh = (byte)itemProvince.EditValue;
                //frm.MaHuyen = (short)gridView1.GetFocusedRowCellValue("MaHuyen");
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn xã cần sửa. Xin cảm ơn");
        }

        private void lookUpTinh1_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit _Tinh = (LookUpEdit)sender;
            MaTinh = byte.Parse(_Tinh.EditValue.ToString());
            LoadData();
        }

        private void itemProvince_EditValueChanged(object sender, EventArgs e)
        {
            MaTinh = byte.Parse(itemProvince.EditValue.ToString());
            LoadData();
        }
    }
}
