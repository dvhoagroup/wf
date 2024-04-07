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
    public partial class frmSearch : DevExpress.XtraEditors.XtraForm
    {
        public string KeyWord = "";
        public frmSearch()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            string s = txtKeyWord.Text.Trim().ToLower().Replace("delete", "").Replace("update", "").Replace("alter", "").Replace("drop", "").Replace("select", "").Replace("--", "").Replace("/*", "").Replace("*/", "");
            KeyWord = s;
            
            this.Close();
        }

        private void Search_frm_Load(object sender, EventArgs e)
        {

        }
    }
}