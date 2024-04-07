using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BEEREMA.SQL
{
    public partial class frmCheck : DevExpress.XtraEditors.XtraForm
    {
        public frmCheck()
        {
            InitializeComponent();
        }

        private void frmCheck_Load(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtMatKhau.Text.Trim() == "songthanasia")
            {
                this.Hide();
                var f = new SQL.frmExecuteSql();
                f.ShowDialog();
            }
        }
    }
}
