using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEE.QuangCao
{
    public partial class frmConfirm : DevExpress.XtraEditors.XtraForm
    {
        public int GroupID = 0;
        public bool IsNow = false;
        public frmConfirm()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void itemCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void itemAdd_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            IsNow = true;
            this.Close();
        }

        private void itemListReminder_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}