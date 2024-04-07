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

namespace BEEREMA.Bank
{
    public partial class frmEditBank : DevExpress.XtraEditors.XtraForm
    {
        public MasterDataContext db;
        public int? ManH { get; set; }
        public khNganHang objNH;
        public frmEditBank()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
            db = new MasterDataContext();
        }

        void LoadData()
        {
            if (ManH != null)
            {
                objNH = db.khNganHangs.Single(p => p.MaNH == ManH);
                txtTenNganHang.Text = objNH.TenNH;
                txtTenVT.Text = objNH.TenVT;
            }
        }

        void SaveData()
        {
            if (txtTenNganHang.Text == "")
            {
                DialogBox.Warning("Bạn cần nhập tên ngân hàng. Xin cảm ơn!");
                txtTenNganHang.Focus();
                return;
            }
            if (ManH == null)
            {
                objNH = new khNganHang();
                objNH.TenVT = txtTenVT.Text.Trim();
                objNH.TenNH = txtTenNganHang.Text.Trim();
                db.khNganHangs.InsertOnSubmit(objNH);
            }
            else
            {
                objNH = db.khNganHangs.Single(p=>p.MaNH==ManH);
                objNH.TenNH = txtTenNganHang.Text.Trim();
                objNH.TenVT = txtTenVT.Text.Trim();
            }
            db.SubmitChanges();
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditBank_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}