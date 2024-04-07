using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEEREMA.Test
{
    public partial class Test_Find_frm : DevExpress.XtraEditors.XtraForm
    {
        public Test_Find_frm()
        {
            InitializeComponent();
        }

        private void Test_Find_frm_Load(object sender, EventArgs e)
        {

        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit _New = (TextEdit)sender;
            it.KhachHangCls o = new it.KhachHangCls();
            o.SoCMND = _New.Text;
            lookUpEdit1.Properties.DataSource = o.SearchSoCMND();
        }
    }
}