using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BEE.ThuVien;
using DevExpress.XtraEditors;
using System.Data.Linq.SqlClient;

namespace BEEREMA.MyCompany
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db;

        public ctlManager()
        {
            InitializeComponent();

            db = new MasterDataContext();
        }

        private void LoadData()
        {
            db = new MasterDataContext();
            var wait = DialogBox.WaitingForm();
            try
            {
                var objCty = db.Companies
                       .Select(q => new
                       {
                           q.MaCT,
                           q.TenCT,
                           q.TenVT,
                           q.DiaChi,
                           q.DienGiai,
                           q.DienThoai,
                           q.NgayCap,
                           q.NgayCN,
                           q.NgayTao,
                           NVTao = q.NhanVien.HoTen,
                           NVSua = q.NhanVien1.HoTen,
                           q.NguoiDaiDien,
                           q.ChucVu,
                           q.MaSoThue,
                           q.SoGPKD,
                           q.NoiCap,
                           q.Website
                       });
                gcCompany.DataSource = objCty;
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        void LoadPermission()
        {
            var ltAction = db.ActionDatas.Where(p => p.PerID == Common.PerID & p.FormID == 142).Select(p => p.FeatureID).ToList();
            itemEdit.Enabled = ltAction.Contains(2);
            itemDelete.Enabled = ltAction.Contains(3);
            itemNew.Enabled = ltAction.Contains(1);
        }

        private void EditData()
        {
            if (gvCompany.FocusedRowHandle < 0)
            {
                DialogBox.Warning("Bạn cần chọn công ty để chỉnh Sửa. Xinh cảm ơn!");
                return;
            }
                frmEdit frm = new frmEdit();
                frm.MaCT = (int)gvCompany.GetFocusedRowCellValue(ID);
                frm.ShowDialog();
        }

        private void DeleteData()
        {
            if (gvCompany.FocusedRowHandle < 0)
            {
                DialogBox.Warning("Bạn cần chọn phiếu nghiệm thu để xóa. Xin cảm ơn!");
                return;
            }
            int MaCT = (int)gvCompany.GetFocusedRowCellValue(ID);
            var objCT = db.Companies.Where(p => p.MaCT == MaCT).SingleOrDefault();
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn xóa công tynày!", "Xác nhận thông tin trước khi xóa", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                db.Companies.DeleteOnSubmit(objCT);
                db.SubmitChanges();
                LoadData();
            }
        }

        private void itemNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmEdit frm = new frmEdit();
            frm.ShowDialog();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditData();
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            LoadPermission();

            LoadData();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}
