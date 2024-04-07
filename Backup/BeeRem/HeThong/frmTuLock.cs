using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;

namespace BEEREMA.HeThong
{
    public partial class frmTuLock : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        UserTuLock obj = new UserTuLock();

        public frmTuLock()
        {
            InitializeComponent();
        }

        private void frmTuLock_Load(object sender, EventArgs e)
        {

            obj = db.UserTuLocks.First();
            spinThoiGian.EditValue = obj.ThoiGisn;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                obj.ThoiGisn = Convert.ToInt32(spinThoiGian.EditValue);
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}