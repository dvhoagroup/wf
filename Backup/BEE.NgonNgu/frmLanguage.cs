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

namespace BEE.NgonNgu
{
    public partial class frmLanguage : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmLanguage()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmLanguage_Load);
            this.btnLuu.Click += new EventHandler(btnLuu_Click);
            this.btnHuy.Click += new EventHandler(btnHuy_Click);
            grView.KeyUp += new KeyEventHandler(grView_KeyUp);
        }

        void frmLanguage_Load(object sender, EventArgs e)
        {
            grControl.DataSource = db.lgLanguages.OrderBy(p => p.STT).Where(p => p.ID != 1);
        }

        void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                db.SubmitChanges();

                DialogBox.Infomation();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch(Exception ex){
                DialogBox.Error(ex.Message);
            }
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
    }
}