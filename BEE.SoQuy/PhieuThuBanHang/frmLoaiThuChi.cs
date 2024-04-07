using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.DULIEU;
namespace BEE.SoQuy.PhieuThuBanHang
{
    public partial class frmLoaiThuChi : BForm
    {

        public frmLoaiThuChi()
        {
            InitializeComponent();

            TranslateUserControl(this);

            this.Load += new EventHandler(frmTrangThaiKH_Load);
            this.btnHuy.Click += new EventHandler(btnHuy_Click);
            grView.KeyUp += new KeyEventHandler(grView_KeyUp);
        }

        void frmTrangThaiKH_Load(object sender, EventArgs e)
        {
            grControl.DataSource = db.pgcLoaiPhieuThuChis;
        }

        void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.Question() == DialogResult.Yes)
                    grView.DeleteSelectedRows();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                db.SubmitChanges();

                DialogBox.Infomation();

                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }
    }
}