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

namespace BEEREMA.Bank
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db;

        public ctlManager()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
            gvBank.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gvBank_FocusedRowChanged);
            gvChiNhanh.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gvChiNhanh_FocusedRowChanged);
        }

        private void LoadCN()
        {
            if (gvBank.FocusedRowHandle < 0)
            {
                gcChiNhanh.DataSource = null;
                return;
            }
            int MaNH = (int)gvBank.GetFocusedRowCellValue(ID);
            gcChiNhanh.DataSource = db.khNganHangChiNhanhs.Where(p => p.MaNH == MaNH);
        }

        private void LoadGV()
        {
            if (gvChiNhanh.FocusedRowHandle < 0)
            {
                gcGoiVay.DataSource = null;
                return;
            }
            int MaCN = (int)gvChiNhanh.GetFocusedRowCellValue(IDChiNhanh);
            var objGV = db.khNganHangGoiVays.Where(p => p.MaCN == MaCN)
                .Select(q => new
                {
                    q.ID,
                    q.MaCN,
                    q.TenGoi,
                    q.TuNgay,
                    q.DenNgay,
                    NVTao = q.NhanVien2.HoTen,
                    q.NgayTao,
                    NVSua = q.NhanVien1 == null ? "" : q.NhanVien1.HoTen,
                    q.NgayCN
                });
            gcGoiVay.DataSource = objGV;
        }

        private void LoadData()
        {
            db = new MasterDataContext();
            var wait = DialogBox.WaitingForm();
            try
            {
                gcBank.DataSource = db.khNganHangs.ToList();
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        private void EditData()
        {
            if (gvBank.FocusedRowHandle < 0)
            {
                DialogBox.Warning("Bạn cần chọn ngân hàng để chỉnh Sửa. Xinh cảm ơn!");
                return;
            }
            frmEditBank frm = new frmEditBank();
            frm.ManH = (int)gvBank.GetFocusedRowCellValue(ID);
            frm.ShowDialog();
        }

        private void DeleteData()
        {
            if (gvBank.FocusedRowHandle < 0)
            {
                DialogBox.Warning("Bạn cần chọn ngân hàng để xóa. Xin cảm ơn!");
                return;
            }
            int MaNH = (int)gvBank.GetFocusedRowCellValue(ID);
            var objNH = db.khNganHangs.Single(p=>p.MaNH==MaNH);
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn xóa ngân hàng này!", "Xác nhận thông tin trước khi xóa", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                db.khNganHangs.DeleteOnSubmit(objNH);
                db.SubmitChanges();
                LoadData();
            }
        }

        void gvChiNhanh_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadGV();
        }

        void gvBank_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadCN();
        }

        private void itemNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmEditBank frm = new frmEditBank();
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
            LoadData();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemThemCN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvBank.FocusedRowHandle < 0)
            {
                DialogBox.Warning("Bạn cần chọn ngân hàng để thêm chi nhánh. Xin cảm ơn!");
                gvBank.Focus();
                return;
            }
            int MaNH = (int)gvBank.GetFocusedRowCellValue(ID);
            frmEditCN frm = new frmEditCN();
            frm.MaNH = MaNH;
            frm.ShowDialog();
        }

        private void itemSuaCN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(gvChiNhanh.FocusedRowHandle<0)
            {
                DialogBox.Warning("Vui lòng chọn chi nhánh để sửa. Xin cảm ơn");
                gvChiNhanh.Focus();
                return;
            }
            frmEditCN frm = new frmEditCN();
            frm.MaNH=(int)gvChiNhanh.GetFocusedRowCellValue(MaNH);
            frm.MaCN=(int)gvChiNhanh.GetFocusedRowCellValue(IDChiNhanh);
            frm.ShowDialog();
        }

        private void itemXoaCN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvChiNhanh.FocusedRowHandle < 0)
            {
                DialogBox.Warning("Vui lòng chọn chi nhánh để xóa. Xin cảm ơn");
                gvChiNhanh.Focus();
                return;
            }
            int MaCN = (int)gvChiNhanh.GetFocusedRowCellValue(IDChiNhanh);
            var objCN = db.khNganHangChiNhanhs.SingleOrDefault(p=>p.ID==MaCN);
            db.khNganHangChiNhanhs.DeleteOnSubmit(objCN);
            db.SubmitChanges();
            LoadData();
        }

        private void itemThemGV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvChiNhanh.FocusedRowHandle < 0)
            {
                DialogBox.Warning("Bạn cần chọn chi nhánh để thêm gói vay. Xin cảm ơn!");
                gvChiNhanh.Focus();
                return;
            }
            int MaCN = (int)gvChiNhanh.GetFocusedRowCellValue(IDChiNhanh);
            frmEditGV frm = new frmEditGV();
            frm.MaCN = MaCN;
            frm.ShowDialog();
        }

        private void itemSuaGV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvGoiVay.FocusedRowHandle < 0)
            {
                DialogBox.Warning("Bạn cần chọn gói vay để chỉnh sửa. Xin cảm ơn!");
                gvGoiVay.Focus();
                return;
            }
            frmEditGV frm = new frmEditGV();
            int a = (int)gvGoiVay.GetFocusedRowCellValue(MaCN); 
            frm.MaCN = (int)gvGoiVay.GetFocusedRowCellValue(MaCN);
            frm.MaGV = (int)gvGoiVay.GetFocusedRowCellValue(IDGoiVay);
            frm.ShowDialog();
        }

        private void itemXoaGV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvGoiVay.FocusedRowHandle < 0)
            {
                DialogBox.Warning("Vui lòng chọn gói vay để xóa. Xin cảm ơn!");
                gvGoiVay.Focus();
                return;
            }
            int MaGV = (int)gvGoiVay.GetFocusedRowCellValue(IDGoiVay);
            var objGV = db.khNganHangGoiVays.SingleOrDefault(p => p.ID == MaGV);
            db.khNganHangGoiVays.DeleteOnSubmit(objGV);
            db.SubmitChanges();
            LoadData();
        }
    }
}
