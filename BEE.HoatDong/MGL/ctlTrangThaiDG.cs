using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;
using BEE.ThuVien;
using System.Linq;

namespace BEE.HoatDong.MGL
{
    public partial class ctlTrangThaiDG : XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();
        public ctlTrangThaiDG()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
        }

        void LoadData()
        {

            try
            {
                db = new MasterDataContext();
                gridControl1.DataSource = db.mglTrangThaiGiaoDiches.OrderBy(p => p.Ord).ToList();
            }
            catch
            {

            }

            using (var db = new MasterDataContext())
            {
                gridControl1.DataSource = db.mglTrangThaiGiaoDiches.OrderBy(p => p.Ord);
            }
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = BEE.ThuVien.Common.PerID;
            o.AccessData.Form.FormID = 226;
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
            LoadPermission();
            LoadData();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnNap_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmTrangThaiGD frm = new frmTrangThaiGD();
            frm.flagAddnew = true;
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvTrangThaiGD.GetFocusedRowCellValue(colMaHuong) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa trạng thái giao dịch: <" + grvTrangThaiGD.GetFocusedRowCellValue(colTenHuong).ToString() + "> ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    try
                    {
                        var db = new MasterDataContext();
                        var matt = (byte)grvTrangThaiGD.GetFocusedRowCellValue("MaLoai");
                        var obj = db.mglTrangThaiGiaoDiches.FirstOrDefault(p => p.MaLoai == matt);
                        db.mglTrangThaiGiaoDiches.DeleteOnSubmit(obj);
                        db.SubmitChanges();
                        DialogBox.Infomation("Xóa dữ liệu thành công");
                        LoadData();
                        // grvTrangThaiGD.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì trạng thái giao dịch <" + grvTrangThaiGD.GetFocusedRowCellValue(colTenHuong).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn trạng thái giao dịch cần xóa. Xin cảm ơn");
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
            if (grvTrangThaiGD.GetFocusedRowCellValue(colMaHuong) != null)
            {
                frmTrangThaiGD frm = new frmTrangThaiGD();
                frm.KeyID = byte.Parse(grvTrangThaiGD.GetFocusedRowCellValue(colMaHuong).ToString());
                frm.flagAddnew = false;
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn trạng thái giao dịch cần sửa. Xin cảm ơn");
        }
    }
}
