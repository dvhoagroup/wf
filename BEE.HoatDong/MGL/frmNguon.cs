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
using BEEREMA;

namespace BEE.HoatDong.MGL
{
    public partial class frmNguon : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmNguon()
        {
            InitializeComponent();
        }

        private void frmNguon_Load(object sender, EventArgs e)
        {
            try
            {
                gcNguon.DataSource = db.mglNguons;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                db.SubmitChanges();
                this.Close();
            }
            catch (Exception ex) {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvNguon_KeyUp(object sender, KeyEventArgs e)
        {
            if (grvNguon.FocusedRowHandle < 0)
                return;
            if (e.KeyCode == Keys.Delete)
            {
                grvNguon.DeleteSelectedRows();
            }
        }


    }
}