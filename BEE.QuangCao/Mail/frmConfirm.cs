using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEE.QuangCao.Mail
{
    public partial class frmConfirm : DevExpress.XtraEditors.XtraForm
    {
        public int GroupID = 0;
        public Form parent;
        public frmConfirm()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void itemCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void itemAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmReceiveList frm = new frmReceiveList();
            frm.ReceID = GroupID;
            frm.ShowDialog(parent);
            if (frm.DialogResult == DialogResult.OK)
                this.DialogResult = DialogResult.OK;            
        }

        private void itemListReminder_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}