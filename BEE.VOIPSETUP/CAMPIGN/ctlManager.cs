using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.Linq.SqlClient;
using BEE.DULIEU;
using BEE.THUVIEN;
using DIPCRM;

namespace BEE.VOIPSETUP.CAMPAIGN
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        byte SDBID = 0;
        MasterDataContext db;

        private void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void CampaignLoad()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var tuNgay = (DateTime?)itemTuNgay.EditValue;
                var denNgay = (DateTime?)itemDenNgay.EditValue;
                int maPB = 0, MaNKD = 0, maNV = 0;
                switch (this.SDBID)
                {
                    case 2: maPB = Common.MaPB; break;
                    case 3: MaNKD = Common.MaNKD; break;
                    case 4: maNV = Common.MaNV; break;
                }

                using (var db = new MasterDataContext())
                {
                    if (gvChienDich.FocusedRowHandle == 0) gvChienDich.FocusedRowHandle = -1;
                    gcChienDich.DataSource = db.voipCampaigns.Where(p => SqlMethods.DateDiffDay(p.NgayXL, tuNgay) <= 0 && SqlMethods.DateDiffDay(p.NgayXL, denNgay) >= 0).Select(p => new
                    {
                        p.ID,
                        p.GhiChu,
                        p.Name,
                        p.NgayCN,
                        p.NgayTao,
                        NhanVienTao = p.NhanVien.HoTen,
                        NVCN = p.MaNVCN == null ? "" : p.NhanVien1.HoTen,
                        p.NgayXL,
                        p.MaNVCN,
                        TrangThai = p.TrangThai == true ? "Đã Map dữ liệu" : "Chiến dịch mới",
                        p.TuNgay,
                        p.DenNgay
                    });
                }
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        private void BanHang_Add()
        {
            if (!itemAdd.Enabled) return;

            using (var frm = new frmEdit())
            {
                frm.ShowDialog(this);
                 CampaignLoad();
            }
        }

        private void BanHang_Edit()
        {
            if (!itemEdit.Enabled) return;

            var id = (int?)gvChienDich.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn chiến dịch!");
                return;
            }
            using (var frm = new frmEdit())
            {
                frm.CamID = id;
                frm.ShowDialog(this);
                if (frm.IsSave)
                    CampaignLoad();
            }
        }

        private void BanHang_Delete()
        {
            if (!itemDelete.Enabled) return;

            var indexs = gvChienDich.GetSelectedRows();
            if (indexs.Length < 0)
            {
                DialogBox.Error("Vui lòng chọn Chiến dịch");
                return;
            }
            if (DialogBox.Question() == DialogResult.No) return;
            try
            {
                using (var db = new MasterDataContext())
                {
                    foreach (var i in indexs)
                    {
                        var bh = db.voipCampaigns.Single(p => p.ID == (int)gvChienDich.GetRowCellValue(i, "ID"));
                        db.voipCampaigns.DeleteOnSubmit(bh);
                    }
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error("Ràng buộc dữ liệu bạn không thể xóa dữ liệu chiến dịch này!");
            }
            gvChienDich.DeleteSelectedRows();
        }

        private void BanHang_Click()
        {
            var id = (int?)gvChienDich.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                gcDoiTuong.DataSource = null;
                gcQuestion.DataSource = null;
                return;
            }

            using (var db = new MasterDataContext())
            {
                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        var obj = db.voipCampaign_Questions.Where(p => p.CamID == id).Select(p => new { CauHoi = p.Question, p.ID }).ToList();
                        gcQuestion.DataSource = obj;
                        break;
                    case 1:
                        gcDoiTuong.DataSource = db.voipCampaign_ListNumbers.Where(p => p.CamID == id).Select(p => new
                        {
                            p.ID,
                            p.DateCall,
                            p.IsCall,
                            p.PhoneNumber,
                            KHNgoai = p.TenKH,
                            p.MaKH,
                            p.MaNV,
                            HoTen = p.MaNV == null ? "" : p.NhanVien.HoTen,
                            HoTenKH = p.MaKH == null ? "" : (p.KhachHang.TenCongTy == "" ? p.KhachHang.HoKH + " " + p.TenKH : p.KhachHang.TenCongTy),
                            TrangThai = p.MaTT == null ? "" : p.voipCall_Status.Name
                        });
                        break;
                }
            }
        }

        private void Question_CLick()
        {
            if (gvQuestion.FocusedRowHandle < 0)
            {
                gcAnswer.DataSource = null;
                return;
            }
            var obj=db.voipCampaign_Questions.FirstOrDefault(p=>p.ID == (int?)gvQuestion.GetFocusedRowCellValue("ID"));
            if (obj != null)
                gcAnswer.DataSource = obj.voipCampaign_Answers;
            else
                gcAnswer.DataSource = null;
        }

        public ctlManager()
        {
            InitializeComponent();
            db = new MasterDataContext();

            this.Load += new EventHandler(ctlManager_Load);
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
            itemDenNgay.EditValueChanged += new EventHandler(itemDenNgay_EditValueChanged);
            cmbKyBaoCao.EditValueChanged += new EventHandler(cmbKyBaoCao_EditValueChanged);
            itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemRefresh_ItemClick);
            itemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemAdd_ItemClick);
            itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemEdit_ItemClick);
            itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDelete_ItemClick);

            gvChienDich.DoubleClick += new EventHandler(grvBanHang_DoubleClick);
            gvChienDich.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(grvBanHang_FocusedRowChanged);
            gvChienDich.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(grvBanHang_CustomDrawRowIndicator);
        }


        void grvBanHang_DoubleClick(object sender, EventArgs e)
        {
            BanHang_Edit();
        }

        void grvSanPham_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        void grvBanHang_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        void grvBanHang_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            BanHang_Click();
        }

        void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BanHang_Delete();
        }

        void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BanHang_Edit();
        }

        void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BanHang_Add();
        }

        void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CampaignLoad();
        }

        void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            CampaignLoad();
        }

        void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            CampaignLoad();
        }

        void ctlManager_Load(object sender, EventArgs e)
        {
            this.SDBID = Common.Permission(barManager1, 115);

            lookNhanVien.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.HoTen });
            lookLoaiHoaDon.DataSource = db.LoaiHoaDons.ToList();
            cmbCongTy.DataSource = db.CongTies.Select(p => new { p.ID, p.TenCT }).ToList();

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(0);
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            it.CommonCls.ExportExcel(gcChienDich);
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            BanHang_Click();
        }

        private void gvQuestion_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Question_CLick();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvAnswer.FocusedRowHandle < 0)
                return;
            gvAnswer.DeleteSelectedRows();
            try
            {
                db.SubmitChanges();
                DialogBox.Infomation("Dữ liệu đã lưu!");
            }
            catch (Exception ex)
            {
                DialogBox.Error("Dữ liệu không thể lưu!");
            }
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
                DialogBox.Infomation("Dữ liệu đã lưu!");
            }
            catch (Exception ex)
            {
                DialogBox.Error("Dữ liệu không thể lưu!");
            }
        }

        private void itemConnect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvChienDich.FocusedRowHandle < 0)
                return;
            if (DialogBox.Question("Bạn vui lòng chắc chắn về [Nội dung] và [Danh sách] khảo sát \r\n đã chính xác để tiến hành kết nối dữ liệu") == DialogResult.No) 
                return;
            int ID =(int)gvChienDich.GetFocusedRowCellValue("ID");
            var objND = db.voipCampaign_Answers.Where(p => p.voipCampaign_Question.CamID == ID).ToList();
            var objNum = db.voipCampaign_ListNumbers.Where(p => p.CamID == ID).ToList();
            if (objND.Count == 0 || objNum.Count == 0)
            {
                DialogBox.Infomation("Danh sách điện thoại hoặc danh sách câu hỏi của chiến dịch đang trống. Vui lòng kiểm tra lại!");
                return;
            }
            var objCP = db.voipCampaigns.First(p => p.ID == ID);
            if (objCP.TrangThai.GetValueOrDefault())
            {
                DialogBox.Infomation("Dữ liệu chiến dịch đã được kết nối, Bạn không không thể kết nối lại được!");
                return;
            }
            
            foreach (var p in objND)
            {
                foreach (var t in objNum)
                {
                    var obj = new voipMap_Answer();
                    obj.MapID = t.ID;
                    obj.QuesID = p.QuestionID;
                    obj.AnswID = p.ID;
                    obj.DateRequire = t.voipCampaign.NgayXL;
                    db.voipMap_Answers.InsertOnSubmit(obj);
                }
            }
            try
            {
                objCP.TrangThai = true;
                db.SubmitChanges();
                DialogBox.Infomation("Dữ liệu đã được lưu!");
            }
            catch (Exception ex)
            {
                DialogBox.Error("Dữ liệu không thể lưu!");
            }

        }
    }
}
