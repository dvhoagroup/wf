using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEEREMA.HeThong
{
    public partial class ConfigFTP_frm : DevExpress.XtraEditors.XtraForm
    {
        public ConfigFTP_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtServer.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập <Server>. Xin cảm ơn.");
                txtServer.Focus();
                return;
            }

            if (txtEmail.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập <Tên đăng nhập>. Xin cảm ơn.");
                txtEmail.Focus();
                return;
            }

            if (txtPassword.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập <Mật khẩu>. Xin cảm ơn.");
                txtPassword.Focus();
                return;
            }

            it.FTPCls o = new it.FTPCls();
            o.SetAccountFTP(string.Format("ALTER PROCEDURE GetAccountFTP @ServerIP nvarchar(50) output, @UserName nvarchar(50) output, @Password nvarchar(50) output as set @ServerIP = '{0}' set @UserName = '{1}' set @Password = '{2}'", txtServer.Text, txtEmail.Text, txtPassword.Text));

            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }

        private void ConfigFTP_frm_Load(object sender, EventArgs e)
        {
            it.FTPCls o = new it.FTPCls();
            o.GetAccountFTP();
            txtEmail.Text = o.FTPUserID;
            txtPassword.Text = o.FTPPassword;
            txtServer.Text = o.FTPServerIP;
        }
    }
}