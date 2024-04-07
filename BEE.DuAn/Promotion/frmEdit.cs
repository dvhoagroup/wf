using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using System.Linq;
using BEEREMA;

namespace BEE.DuAn.Promotion
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? MaCD { get; set; }
        public int? MaDA { get; set; }
        public bool IsSave { get; set; }
        MasterDataContext db = new MasterDataContext();
        daKhuyenMai objCDT;

        public frmEdit()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);         
        }

        void LoadData()
        {
            lookDuAn.Properties.DataSource = db.DuAns.Select(p => new { p.MaDA, p.TenDA });
            if (MaCD == null)
            {
                lookDuAn.EditValue = MaDA;
                txtTenMuc.Text = "";
                dateDenNgay.EditValue = DateTime.Now;
                dateTuNgay.EditValue = DateTime.Now;
                spinTyLe.EditValue = 0;
            }
            else
            {
                objCDT = db.daKhuyenMais.Single(p => p.ID == MaCD);

                lookDuAn.EditValue = objCDT.MaDA;
                txtTenMuc.Text = objCDT.TenKhuyenMai;
                dateDenNgay.EditValue = (DateTime?)objCDT.DenNgay;
                dateTuNgay.EditValue = (DateTime?)objCDT.TuNgay;
                spinTyLe.EditValue = objCDT.TyLe ?? 0;
                spinTienMat.EditValue = objCDT.GiaTri ?? 0;
                txtTenQuaTang.Text = objCDT.TenQuaTang;
                txtDienGiai.Text = objCDT.DienGiai;
            }
        }

        void SaveData()
        {
            //Rang Buoc
            if (lookDuAn.EditValue == null)
            {
                DialogBox.Warning("Bạn cần chọn dự án để thiết lập. Xin cảm ơn!");
                lookDuAn.Focus();
                return;
            }

            var wait = DialogBox.WaitingForm();
            try
            {
                if (MaCD == null)
                {
                    objCDT = new daKhuyenMai();
                    objCDT.MaNVT = Common.StaffID;
                    objCDT.NgayTao = db.GetSystemDate();
                    db.daKhuyenMais.InsertOnSubmit(objCDT);
                }
                else
                {
                    objCDT.MaNVCN = Common.StaffID;
                    objCDT.NgayCN = db.GetSystemDate();
                    //LS
                    daKhuyenMaiL objLS = new daKhuyenMaiL();
                    objLS.MaNVCN = Common.StaffID;
                    objLS.NgayCN = objCDT.NgayCN;
                    objCDT.daKhuyenMaiLs.Add(objLS);
                }

                objCDT.MaDA = (int?)lookDuAn.EditValue;
                objCDT.TenKhuyenMai = txtTenMuc.Text.Trim();
                objCDT.TuNgay = (DateTime?)dateTuNgay.EditValue;
                objCDT.DenNgay = (DateTime?)dateDenNgay.EditValue;
                objCDT.TenQuaTang = txtTenQuaTang.Text;
                objCDT.DienGiai = txtDienGiai.Text;
                objCDT.TyLe = spinTyLe.Value;
                objCDT.GiaTri = spinTienMat.Value;

                db.SubmitChanges();
                this.IsSave = true;
            }
            catch { }
            finally
            {
                wait.Close();
                this.Close();
            }
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SaveData();
        }
    }
}