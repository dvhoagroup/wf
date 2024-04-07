using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEE.KhachHang
{
    public partial class ShowCustomer_frm : DevExpress.XtraEditors.XtraForm
    {
        public string queryString = ""; 
        public ShowCustomer_frm()
        {
            InitializeComponent();
        }

        private void ShowCustomer_frm_Load(object sender, EventArgs e)
        {
            //khachHang_ctl1.queryString = queryString;
            //khachHang_ctl1.LoadData();
        }
    }
}