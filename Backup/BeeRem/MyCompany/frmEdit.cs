using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using System.Data.Linq;
using System.Linq;

namespace BEEREMA.MyCompany
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? MaCT { get; set; }
        MasterDataContext db;
        Company objCty;

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        void AddNew()
        {
            txtTenCTy.Text = "";
            txtChucVuNguoiDD.Text = "";
            txtDiaChi.Text = "";
            txtDienGiai.Text = "";
            txtDienThoai.Text = "";
            txtMaSoThue.Text = "";
            txtNguoiDD.Text = "";
            txtNoiCap.Text = "";
            txtSoGP.Text = "";
            txtTenVT.Text = "";
            txtWebSide.Text = "";
            txtTenTA.Text = "";
            txtFax.Text = "";
            dateNgayCap.EditValue = null;
            Enable(true);
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();

            try
            {
                if (MaCT == null)
                    AddNew();
                else
                {
                    objCty = db.Companies.Single(p => p.MaCT == MaCT);
                    txtTenCTy.Text = objCty.TenCT;
                    txtChucVuNguoiDD.Text = objCty.ChucVu;
                    txtDiaChi.Text = objCty.DiaChi;
                    txtDienGiai.Text = objCty.DienGiai;
                    txtDienThoai.Text = objCty.DienThoai;
                    txtMaSoThue.Text = objCty.MaSoThue;
                    txtNguoiDD.Text = objCty.NguoiDaiDien;
                    txtNoiCap.Text = objCty.NoiCap;
                    txtSoGP.Text = objCty.SoGPKD;
                    txtTenVT.Text = objCty.TenVT;
                    txtWebSide.Text = objCty.Website;
                    txtFax.Text = objCty.Fax;
                    txtTenTA.Text = objCty.TenTA;
                    dateNgayCap.EditValue = (DateTime?)objCty.NgayCap;
                }

            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        void Enable(bool bl)
        {
            panelControl1.Enabled = bl;
            groupControl1.Enabled = bl;
            itemDelete.Enabled = bl;
            itemAddNew.Enabled = !bl;
            itemSave.Enabled = bl;
            itemEdit.Enabled = !bl;
            itemStandBy.Enabled = bl;
        }

        void SaveData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                #region KiemTraRong
                if (txtTenCTy.Text == "")
                {
                    DialogBox.Warning("Bạn cần nhập tên công ty. Xin cảm ơn!");
                    txtTenCTy.Focus();
                    return;
                }

                if (MaCT == null)
                {
                    objCty = new Company();
                    objCty.NgayTao = db.GetSystemDate();
                    objCty.MaNV = Common.StaffID;
                    db.Companies.InsertOnSubmit(objCty);
                }
                else
                {
                    objCty.MaNVCN = Common.StaffID;
                    objCty.NgayCN = db.GetSystemDate();
                }
                #endregion

                objCty.ChucVu = txtChucVuNguoiDD.Text.Trim();
                objCty.DiaChi = txtDiaChi.Text.Trim();
                objCty.DienGiai = txtDienGiai.Text.Trim();
                objCty.DienThoai = txtDienGiai.Text.Trim();
                objCty.MaSoThue = txtMaSoThue.Text.Trim();
                objCty.NgayCap = (DateTime?)dateNgayCap.EditValue;
                objCty.NguoiDaiDien = txtNguoiDD.Text.Trim();
                objCty.NoiCap = txtNoiCap.Text.Trim();
                objCty.SoGPKD = txtSoGP.Text.Trim();
                objCty.TenCT = txtTenCTy.Text.Trim();
                objCty.TenVT = txtTenVT.Text.Trim();
                objCty.Website = txtWebSide.Text.Trim();
                objCty.TenTA = txtTenTA.Text.Trim();
                objCty.Fax = txtFax.Text.Trim();

                db.SubmitChanges();
            }
            catch { }
            finally
            {
                wait.Close();
            }
            Enable(false);
        }

        private void itemAddNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddNew();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Enable(true);
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveData();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemStandBy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Enable(false);
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            LoadData();

            BEE.NgonNgu.Language.TranslateControl(this, barManager1);
        }
    }
}