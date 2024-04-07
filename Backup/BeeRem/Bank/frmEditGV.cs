using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using BEE.ThuVien;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEEREMA.Bank
{
    public partial class frmEditGV : XtraForm
    {
        MasterDataContext db;
        public int? MaCN { get; set; }
        public int? MaGV { get; set; }
        khNganHangGoiVay objGV;
        public frmEditGV()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
            db = new MasterDataContext();
        }

        void LoadData()
        {
            txtChiNhanh.Text = db.khNganHangChiNhanhs.SingleOrDefault(p=>p.ID==MaCN).TenCN;
            if (MaGV != null)
            {
                objGV = db.khNganHangGoiVays.Single(p => p.ID == MaGV);
                txtTenGoi.Text = objGV.TenGoi;
                dateDenNgay.EditValue = (DateTime?)objGV.DenNgay;
                dateTuNgay.EditValue = (DateTime?)objGV.TuNgay;
            }
            else
            {
                dateTuNgay.EditValue = DateTime.Now;
                dateDenNgay.EditValue = DateTime.Now;
            }
        }

        void SaveData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                if (txtTenGoi.Text == "")
                {
                    DialogBox.Warning("Bạn cần nhập tên cho gói vay. Xin cảm ơn!");
                    txtTenGoi.Focus();
                    return;
                }
                if (MaGV == null)
                {
                    objGV = new khNganHangGoiVay();
                    objGV.MaCN = MaCN;
                    objGV.TenGoi = txtTenGoi.Text.Trim();
                    objGV.TuNgay = (DateTime?)dateTuNgay.EditValue;
                    objGV.DenNgay = (DateTime?)dateDenNgay.EditValue;
                    objGV.MaNV = Properties.Settings.Default.StaffID;
                    objGV.NgayTao = DateTime.Now;
                    db.khNganHangGoiVays.InsertOnSubmit(objGV);
                }
                else
                {
                    objGV = db.khNganHangGoiVays.Single(p => p.ID == MaGV);
                    objGV.TenGoi = txtTenGoi.Text.Trim();
                    objGV.TuNgay = (DateTime?)dateTuNgay.EditValue;
                    objGV.DenNgay = (DateTime?)dateDenNgay.EditValue;
                    objGV.MaNVCN = Properties.Settings.Default.StaffID;
                    objGV.NgayCN = DateTime.Now;
                }
                db.SubmitChanges();
            }
            catch { }
            finally
            { wait.Close(); }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();

        }

        private void frmEditGV_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
