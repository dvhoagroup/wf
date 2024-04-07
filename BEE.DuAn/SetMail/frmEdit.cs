using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using System.Data.Linq.SqlClient;
using BEEREMA;

namespace BEE.DuAn.SetMail
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? ID { get; set; }

        MasterDataContext db;
        daCaiDatMail objSM;
        public frmEdit()
        {
            InitializeComponent();

            db = new MasterDataContext();
            BEE.NgonNgu.Language.TranslateControl(this);
            this.Load += new EventHandler(frmEdit_Load);
        }

        void frmEdit_Load(object sender, EventArgs e)
        {
            ctlPhongBanCheckListEdit1.LoadData();
            lookDuAn.Properties.DataSource = db.DuAns.Select(p => new { p.MaDA,p.TenDA});
            lookMucDichGui.Properties.DataSource = db.daCaiDat_HinhThucMails;
            lookMailGui.Properties.DataSource = db.mailConfigs.Select(p => new { p.ID, p.Email });
            if (this.ID != null)
                SetMailLoad();
            else
                SetMailAddNew();
        }

        void SetMailAddNew()
        {
            db = new MasterDataContext();
            objSM = new daCaiDatMail();
            dateTuNgay.EditValue = DateTime.Now;
            dateDenNgay.EditValue = DateTime.Now;
            txtNoiDung.Text = "";
            txtTieuDe.Text = "";
            lookDuAn.EditValue = null;
            lookMailGui.EditValue = null;
            lookMucDichGui.EditValue = null;
        }

        void SetMailLoad()
        {
            db = new MasterDataContext();
            objSM = db.daCaiDatMails.Single(p => p.ID == this.ID);
            dateTuNgay.EditValue = (DateTime?)objSM.TuNgay;
            dateDenNgay.EditValue = (DateTime?)objSM.DenNgay;
            lookDuAn.EditValue = (int?)objSM.MaDA;
            lookMailGui.EditValue = (int?)objSM.MailGui;
            lookMucDichGui.EditValue = objSM.HinhThucGui;
            txtTieuDe.Text = objSM.TieuDe;
            txtNoiDung.Text = objSM.NoiDung;
           // ctlPhongBanCheckListEdit1.SetEditValue(objSM.);
            string[] ListNV = objSM.DSMaNhanVien.Split(',');
            string ListPB="";
            foreach (var nv in ListNV)
            {
                ListPB += "," + db.NhanViens.Single(p => p.MaNV == Convert.ToInt32(nv)).MaPB.ToString();
            }
            ListPB = ListPB.TrimStart(',');
            ctlPhongBanCheckListEdit1.SetEditValue(ListPB);
            ctlNhanVienCheckListEdit1.SetEditValue(objSM.DSMaNhanVien);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void CongVan_Save()
        {
            #region Rang buoc du lieu
           
            if (ctlPhongBanCheckListEdit1.EditValue == null || ctlPhongBanCheckListEdit1.EditValue.ToString() == "")
            {
                DialogBox.Error("Vui lòng chọn [Phòng ban nhận] , xin cảm ơn.");
                ctlPhongBanCheckListEdit1.Focus();
                return;
            }
            #endregion

            if (this.ID == null)
            {
                objSM = new daCaiDatMail();
                objSM.MaNVTao = Common.StaffID;
                objSM.NgayTao = DateTime.Now;
                db.daCaiDatMails.InsertOnSubmit(objSM);
            }
            else
            {
                objSM = db.daCaiDatMails.Single(p => p.ID == this.ID);
                objSM.MaNVCN = Common.StaffID;
                objSM.NgayCN = DateTime.Now;
                daCaiDatMailL objLS = new daCaiDatMailL();
                objLS.NgayCN = DateTime.Now;
                objLS.MaNV = Common.StaffID;
                objSM.daCaiDatMailLs.Add(objLS);
            }

            objSM.TuNgay = (DateTime?)dateTuNgay.EditValue;
            objSM.DenNgay = (DateTime?)dateDenNgay.EditValue;
            objSM.MaDA = (int?)lookDuAn.EditValue;
            objSM.HinhThucGui = lookMucDichGui.EditValue.ToString(); ;
            objSM.MailGui=(int?)lookMailGui.EditValue;
            objSM.DSMaNhanVien = ctlNhanVienCheckListEdit1.EditValue.ToString().Trim();
            objSM.DSNhanVien = ctlNhanVienCheckListEdit1.Text;
            objSM.NoiDung = txtNoiDung.Text;
            objSM.TieuDe = txtTieuDe.Text;
            try
            {
                db.SubmitChanges();

                DialogBox.Infomation();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            CongVan_Save();
        }

        private void ctlPhongBanCheckListEdit1_EditValueChanged(object sender, EventArgs e)
        {
            var temp = ctlPhongBanCheckListEdit1.EditValue.ToString().Split(',');
            ctlNhanVienCheckListEdit1.MAPB = temp;
            ctlNhanVienCheckListEdit1.LoadData();
            ctlNhanVienCheckListEdit1.EditValue = null;
            //  ctlNhanVien1.LoadData();
            // ctlNhanVien1.EditValue = null;
        }

    }
}