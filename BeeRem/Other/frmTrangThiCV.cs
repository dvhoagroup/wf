using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BEE.ThuVien;

namespace BEEREMA.Other
{
    public partial class frmTrangThiCV : DevExpress.XtraEditors.XtraForm
    {
        int ID;
        public frmTrangThiCV(int _ID)
        {
            InitializeComponent();
            this.ID = _ID;
        }
        MasterDataContext db;
        private void frmTrangThiCV_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            if (ID == 1)
                gridControl1.DataSource = db.cvTrinhTrangs;
            else
            gridControl1.DataSource = db.cvdTrinhTrangs;
        }

        private void btmLuu_Click(object sender, EventArgs e)
        {
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

        private void btmHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.Question() == DialogResult.Yes)
                     gridView1.DeleteSelectedRows();
            }
        }
    }
}
