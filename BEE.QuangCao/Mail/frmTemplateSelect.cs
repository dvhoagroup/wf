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
    public partial class frmTemplateSelect : DevExpress.XtraEditors.XtraForm
    {
        public string Content { get; set; }

        public frmTemplateSelect()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);

            var ctlTemp = new ctlTemplates(true);
            ctlTemp.Dock = DockStyle.Fill;
            this.Controls.Add(ctlTemp);
        }
    }
}