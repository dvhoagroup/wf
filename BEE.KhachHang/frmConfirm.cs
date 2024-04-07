using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BEE.ThuVien;

namespace BEE.KhachHang
{
    public partial class frmConfirm : DevExpress.XtraEditors.XtraForm
    {
        public int CustomerID = 0;
        public bool IsConfirm = true;

        public frmConfirm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                var objHis = new aCustomerHistory();
                objHis.CustomerID = CustomerID;
                objHis.Description = txtDescription.Text;
                objHis.StaffIDConfirm = BEE.ThuVien.Common.StaffID;
                objHis.Status = IsConfirm;
                try
                {
                    db.aCustomerHistories.InsertOnSubmit(objHis);
                    db.SubmitChanges();

                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                catch { }
            }

            this.Close();
        }
    }
}
