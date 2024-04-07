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
    public partial class ctlKetQuaKS : DevExpress.XtraEditors.XtraUserControl
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

        void LoadListCD()
        {
            var tuNgay = (DateTime?)itemTuNgay.EditValue;
            var denNgay = (DateTime?)itemDenNgay.EditValue;
            lookCD.DataSource = db.voipCampaigns.Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayXL) >= 0 && SqlMethods.DateDiffDay(p.NgayXL, denNgay) >= 0).Select(p => new { p.Name, p.ID });
        }

        private void CampaignLoad()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                
                var MaCD = (int?)itemChienDich.EditValue;

                using (var db = new MasterDataContext())
                {
                    gcDoiTuong.DataSource = db.voipCampaign_ListNumbers.Where(p => p.CamID == MaCD).Select(p => new
                    {
                        p.ID,
                        p.DateCall,
                        TrangThai = p.IsCall.GetValueOrDefault() == true ? "Đã gọi" : "Chưa gọi",
                        p.PhoneNumber,
                        KhachHang = p.MaKH == null ? "" : (p.KhachHang.TenCongTy == "" ? p.KhachHang.HoKH + " " + p.KhachHang.TenKH : p.KhachHang.TenCongTy),
                        p.TenKH,
                        p.NgayGoiThuc
                    });
                }
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        public ctlKetQuaKS()
        {
            InitializeComponent();
            db = new MasterDataContext();

            this.Load += new EventHandler(ctlManager_Load);
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
            itemDenNgay.EditValueChanged += new EventHandler(itemDenNgay_EditValueChanged);
            cmbKyBaoCao.EditValueChanged += new EventHandler(cmbKyBaoCao_EditValueChanged);
            itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemRefresh_ItemClick);
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
            LoadListCD();
        }

        void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadListCD();
        }

        void ctlManager_Load(object sender, EventArgs e)
        {
            this.SDBID = Common.Permission(barManager1, 115);

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(0);
        }

        private void gvDoiTuong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvDoiTuong.FocusedRowHandle < 0)
            {
                return;
            }
            var id = (int?)gvDoiTuong.GetFocusedRowCellValue("ID");
            var objDT = db.voipCampaign_ListNumbers.FirstOrDefault(p => p.ID == (int?)gvDoiTuong.GetFocusedRowCellValue("ID"));
            lookCauHoi.DataSource = db.voipCampaign_Questions.Where(p => p.CamID == objDT.CamID).Select(p => new { p.ID, p.Question });
            lookCauTL.DataSource = db.voipCampaign_Answers.Where(p => p.voipCampaign_Question.CamID == objDT.CamID).Select(p => new { p.ID, p.Result });
            gcAnswer.DataSource = objDT.voipMap_Answers;
        }

        private void btnExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            it.CommonCls.ExportExcel(gcDoiTuong);
        }
    }
}
