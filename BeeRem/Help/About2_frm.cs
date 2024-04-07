using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace BEEREMA.Help
{
    public partial class About2_frm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public About2_frm()
        {
            InitializeComponent();
        }

        private void labelControl6_Click(object sender, EventArgs e)
        {
            WebBrowser o = new WebBrowser();
            o.Navigate("http://beesky.vn", true);
        }

        private void About2_frm_Load(object sender, EventArgs e)
        {
            lblCopyright.Text = string.Format("Copyright © 2010 - {0}", DateTime.Now.Year);
            lblVersion.Text = "Version " + System.IO.File.ReadAllLines(Application.StartupPath + "\\version.txt")[0];
        }

        private void ribbon_ApplicationButtonClick(object sender, EventArgs e)
        {
            WebBrowser o = new WebBrowser();
            o.Navigate("http://beesky.vn", true);
        }
    }
}