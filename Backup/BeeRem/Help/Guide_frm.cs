using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEEREMA.Help
{
    public partial class Guide_frm : DevExpress.XtraEditors.XtraForm
    {
        public Guide_frm()
        {
            InitializeComponent();
        }

        private void Guide_frm_Load(object sender, EventArgs e)
        {
            //pdfGuide.OpenFile(Application.StartupPath + "\\Guide.pdf");
        }
    }
}