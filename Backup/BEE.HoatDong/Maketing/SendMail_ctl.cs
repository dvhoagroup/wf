using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BEE.HoatDong.Maketing
{
    public partial class SendMail_ctl : UserControl
    {
        public SendMail_ctl()
        {
            InitializeComponent();
        }

        private void SendMail_ctl_Load(object sender, EventArgs e)
        {
            it.SendMailCls o = new it.SendMailCls();
            gridControl1.DataSource = o.Select();
        }
    }
}
