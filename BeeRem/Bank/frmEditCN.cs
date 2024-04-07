using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.Linq;
using BEE.ThuVien;

namespace BEEREMA.Bank
{
    public partial class frmEditCN : DevExpress.XtraEditors.XtraForm
    {
        public int? MaCN { get; set; }
        public int? MaNH { get; set; }
        MasterDataContext db;
        khNganHangChiNhanh objCN;
        public frmEditCN()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
            db = new MasterDataContext();
        }

        void LoadData()
        {
            txtNganHang.Text = db.khNganHangs.Single(p => p.MaNH == MaNH).TenNH;
            if (MaCN != null)
                txtChiNhanh.Text = db.khNganHangChiNhanhs.Single(p => p.ID == MaCN).TenCN;
        }

        void SaveData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                if (txtChiNhanh.Text == "")
                {
                    DialogBox.Warning("Bạn cần nhập tên chi nhánh. Xin cảm ơn!");
                    txtChiNhanh.Focus();
                    return;
                }
                if (MaCN == null)
                {
                    objCN = new khNganHangChiNhanh();
                    objCN.MaNH = MaNH;
                    objCN.TenCN = txtChiNhanh.Text.Trim();
                    db.khNganHangChiNhanhs.InsertOnSubmit(objCN);
                }
                else
                {
                    objCN = db.khNganHangChiNhanhs.Single(p => p.ID == MaCN);
                    objCN.TenCN = txtChiNhanh.Text.Trim();
                }
                db.SubmitChanges();
            }
            catch { }
            finally
            { wait.Close(); }
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

        private void frmEditCN_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}