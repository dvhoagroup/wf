using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using System.Linq;
using BEE.ThuVien;
using System.Data.Linq.SqlClient;
using BEEREMA;

namespace BEE.DuAn.SetTime
{
    public partial class ctlManager : XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();
        byte SDBID = 6;
        bool IsPayment = false;
        bool IsConfirmInfo = false, IsConfirmBuy = false, IsConfirmPayment = false;
        public ctlManager()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);

            LoadPermission();
            lookDuAn.DataSource = db.DuAns.Select(p => new { p.MaDA, p.TenDA }).OrderBy(q => q.TenDA);
            lookTinhTrang.DataSource = db.pdcTinhTrangs;
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
        }

        void LoadPermission()
        {

        }

        void LoadSetTime()
        {
            db = new MasterDataContext();
            var tuNgay = (DateTime?)itemTuNgay.EditValue;
            var denNgay = (DateTime?)itemDenNgay.EditValue;
            int MaDA = itemDuAn.EditValue != null ? Convert.ToInt32(itemDuAn.EditValue) : 0;
            var obj = db.daCaiDatTimes.Where(p => SqlMethods.DateDiffDay(tuNgay, p.TuNgay) >= 0 && SqlMethods.DateDiffDay(p.TuNgay, denNgay) >= 0 && p.MaDA == MaDA)
                .Select(q => new
                {
                    q.ID,
                    q.DuAn.TenDA,
                    q.TenMuc,
                    q.TuNgay,
                    q.DenNgay,
                    DVTKoTien = q.daDonViTime.TenDVT,
                    DVTCoTien = q.daDonViTime1.TenDVT,
                    q.TimeGiuChoKoTien,
                    q.TimeGiuChoCoTien,
                    q.TienDatCoc,
                    q.SoNguoiGiu,
                    q.NhanVien.HoTen,
                    HoTen1 = q.NhanVien1.HoTen,
                    q.NgayTao,
                    q.NgayCN
                }).ToList();
            gcSetTime.DataSource = obj;
        }

        void EditSetTime()
        {
            if (grvSetTime.FocusedRowHandle < 0)
            {
                DialogBox.Warning("Vui lòng chọn [Mục] để chỉnh sửa.");
                grvSetTime.Focus();
                return;
            }

            int MaCD = (int)grvSetTime.GetFocusedRowCellValue(ID);
            BEE.DuAn.SetTime.frmEdit frm = new frmEdit();
            frm.MaCD = MaCD;
            frm.ShowDialog();
        }

        void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        void SetTimeClick()
        {
            if (grvSetTime.FocusedRowHandle < 0)
            {
                gcLichSu.DataSource = null;
                return;
            }
            db = new MasterDataContext();
            int MaST = (int)grvSetTime.GetFocusedRowCellValue(ID);
            switch (xtraTabControl1.SelectedTabPageIndex)
            {
                case 0:
                    gcLichSu.DataSource = db.daCaiDatTimeLs.Where(p => p.MaCDT == MaST).Select(q => new { 
                        q.daCaiDatTime.TenMuc,
                        q.NhanVien.HoTen,
                        q.NgayCN
                    }).OrderByDescending(t=>t.NgayCN);
                    break;
            }
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadSetTime();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadSetTime();
        }

        private void itemDuAn_EditValueChanged(object sender, EventArgs e)
        {
            LoadSetTime();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            using (db = new MasterDataContext())
            {
                lookDuAn.DataSource = db.DuAns.Select(p => new { p.MaDA, p.TenDA });
                lookDuAn.DisplayMember = "TenDA";
                lookDuAn.ValueMember = "MaDA";
                it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
                objKBC.Initialize(cmbKyBaoCao);
                SetDate(0);
                LoadSetTime();
            }
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadSetTime();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BEE.DuAn.SetTime.frmEdit frm = new frmEdit();
            frm.ShowDialog();
            if (frm.IsSave)
                LoadSetTime();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditSetTime();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvSetTime.FocusedRowHandle < 0)
            {
                DialogBox.Warning("Bạn cần chọn thông báo để xóa. Xin cảm ơn!");
                return;
            }

            int MaCD = (int)grvSetTime.GetFocusedRowCellValue(ID);
            var objTB = db.daCaiDatTimes.Where(p => p.ID == MaCD).SingleOrDefault();
           
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn xóa thông báo này!", "Xác nhận thông tin trước khi xóa", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                db.daCaiDatTimes.DeleteOnSubmit(objTB);
                db.SubmitChanges();
                LoadSetTime();
            }

            
        }

        private void grvDatCoc_DoubleClick(object sender, EventArgs e)
        {
          
        }

        private void grvDatCoc_KeyUp(object sender, KeyEventArgs e)
        {
           // if(e.KeyCode == Keys.Delete)
                
        }

        private void grvDatCoc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SetTimeClick();
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            SetTimeClick();
        }

    }
}
