using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.QuangCao.Care
{
    public partial class frmHappyBirthday : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        CareCustomer objCare;
        public List<int> listCustomer;
        public byte CateID = 1;
        public bool IsProcess = false;
        public frmHappyBirthday()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHappyBirthday_Load(object sender, EventArgs e)
        {
            lookUpStatus.Properties.DataSource = db.careStatus;
            lookUpStatus.ItemIndex = 0;
            if (IsProcess)
                lookUpStatus.ItemIndex = 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (int s in listCustomer)
            {
                objCare = new CareCustomer();
                objCare.CateID = CateID;
                objCare.Description = txtDescription.Text;
                objCare.StaffID = BEE.ThuVien.Common.StaffID;
                objCare.StatusID = (int?)lookUpStatus.EditValue;
                objCare.CustomerID = s;
                db.CareCustomers.InsertOnSubmit(objCare);
            }
            try
            {
                db.SubmitChanges();
            }
            catch { }

            DialogResult = System.Windows.Forms.DialogResult.OK;

            DialogBox.Infomation("Dữ liệu đã được cập nhật.");

            this.Close();
        }
    }
}
