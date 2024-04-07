using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEE.CongCu
{
    public partial class frmDuyet : DevExpress.XtraEditors.XtraForm
    {
        public string DienGiai { get; set; }
        
        public frmDuyet()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            this.DienGiai = txtDienGiai.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDuyet_Load(object sender, EventArgs e)
        {
        }
    }
}