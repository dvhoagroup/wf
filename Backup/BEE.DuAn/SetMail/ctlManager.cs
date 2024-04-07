using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;
using System.Data.Linq.SqlClient;
using BEEREMA;
namespace BEE.DuAn.SetMail
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();
        bool first = true;
        public ctlManager()
        {
            InitializeComponent();

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            //lookNhanVien1.DataSource = db.NhanViens;
            //lookTrinhTrang1.DataSource = db.cvdTrinhTrangs;
           // lookPhongBan.DataSource = db.PhongBans;
            Permission();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
        }

        void Permission()
        {
            try
            {
                var listAction = db.ActionDatas.Where(p => p.FormID == 135 & p.PerID == Common.PerID)
                    .Select(p => p.FeatureID).ToList();
                itemThem.Enabled = listAction.Contains(1);
                itemSua.Enabled = listAction.Contains(2);
                itemXoa.Enabled = listAction.Contains(3);
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            db = new MasterDataContext();
            try
            {
                lookDuAn.DataSource = db.DuAns.Select(p => new { p.MaDA, p.TenDA });
                DateTime tuNgay = itemTuNgay.EditValue != null ? (DateTime)itemTuNgay.EditValue : DateTime.Now.AddDays(-90);
                DateTime denNgay = itemDenNgay.EditValue != null ? (DateTime)itemDenNgay.EditValue : DateTime.Now;
                int maDA = itemDuAn.EditValue != null ? (int)itemDuAn.EditValue : -1;
                //gcCongVan.DataSource = db.CongVanDen_getDate(tuNgay, denNgay, 0, 0, 0);
                var obj = db.daCaiDatMails.Where(p => SqlMethods.DateDiffDay(tuNgay, p.TuNgay) >= 0 & 
                        SqlMethods.DateDiffDay(p.TuNgay, denNgay) >= 0 & 
                        (p.MaDA == maDA | maDA == -1))
                    .Select(q => new { 
                        q.ID,
                        q.DuAn.TenDA,
                        q.TuNgay,
                        q.DenNgay,
                        q.mailConfig.Email,
                        q.DSNhanVien,
                        q.daCaiDat_HinhThucMail.TenHinhThuc,
                        q.NhanVien.HoTen,
                        q.NgayTao,
                        q.TieuDe,
                        q.NoiDung
                    });
                gcSetMail.DataSource = obj;
            }
            catch { }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        void Edit()
        {
            if (gvSetMail.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Cài đặt], xin cảm ơn");
                return;
            }

            frmEdit frm = new frmEdit();
            frm.ID = (int)gvSetMail.GetFocusedRowCellValue("ID");
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                LoadData();
                Clicks();
            }
        }

        void Delete()
        {

            var indexs = gvSetMail.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Cài đặt], xin cảm ơn!");
                return;
            }
            if (DialogBox.Question() == DialogResult.No) return;
            using (var db = new MasterDataContext())
            {
                try
                {
                    foreach (var i in indexs)
                    {
                        var bg = db.daCaiDatMails.Single(p => p.ID == (int)gvSetMail.GetRowCellValue(i, "ID"));
                        db.daCaiDatMails.DeleteOnSubmit(bg);
                    }
                    db.SubmitChanges();
                    LoadData();
                }
                catch
                {
                    DialogBox.Error("Không thể xóa [Cài đặt] này!");
                }
            }

        }

        void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemDenNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemDenNgay_EditValueChanged);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if(!first) LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            SetDate(0);

            LoadData();

            first = false;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmEdit();
            frm.ID = null;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Delete();
        }

        private void gvCongVan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Delete();
            }
        }

        private void gvCongVan_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        void Clicks()
        {
            if (gvSetMail.FocusedRowHandle >= 0)
            {
                var MaCV = (int?)gvSetMail.GetFocusedRowCellValue("ID");
                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        gcNhatKy.DataSource = db.daCaiDatMailLs.Where(p => p.MaCDMail == MaCV).OrderByDescending(p => p.NgayCN).AsEnumerable()
                    .Select((p, index) => new
                    {
                        STT = index + 1,
                        HoTen = p.NhanVien.HoTen,
                        p.NgayCN,
                        p.daCaiDatMail.DuAn.TenDA
                    }).ToList();
                        break;
                    case 1:
                        ctlTaiLieu1.FormID = 135;
                        ctlTaiLieu1.LinkID = MaCV;
                        ctlTaiLieu1.MaNV = (int?)gvSetMail.GetFocusedRowCellValue("MaNV");
                        ctlTaiLieu1.TaiLieu_Load();
                        break;
                }
            }
            else
            {
                gcNhatKy.DataSource = null;
                ctlTaiLieu1.TaiLieu_Remove();
            }
        }

        private void gvCongVan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Clicks();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            Clicks();
        }


        private void gcCongVan_DoubleClick(object sender, EventArgs e)
        {
            if (!itemSua.Enabled) return;
            Edit();
        }

        private void gvCongVan_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            
        }

    }
}
