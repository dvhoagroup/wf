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
    public partial class About_frm : DevExpress.XtraEditors.XtraForm
    {
        public About_frm()
        {
            InitializeComponent();
        }

        private void About_frm_Click(object sender, EventArgs e)
        {
            
        }

        private void About_frm_Load(object sender, EventArgs e)
        {
            lblCopyright.Text = string.Format("Copyright © 2007 - {0}", DateTime.Now.Year);
        }

        private void labelControl6_Click(object sender, EventArgs e)
        {
            WebBrowser o = new WebBrowser();
            o.Navigate("http://beesky.vn", true);
        }
    }
}