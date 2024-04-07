using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEE.QuangCao.SMS
{
    public partial class frmChoiceTemplate : DevExpress.XtraEditors.XtraForm
    {
        public int KeyID { get; set; }
        public frmChoiceTemplate()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void frmChoiceTemplate_Load(object sender, EventArgs e)
        {
            
        }

        public void LoadUserControl(UserControl ctl, string title)
        {
            this.Controls.Clear();
            this.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;
            this.Text = title;
        }

        public void SetKey(int keyID)
        {
            KeyID = keyID;
        }
    }
}