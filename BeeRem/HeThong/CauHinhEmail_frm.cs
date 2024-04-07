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
    public partial class CauHinhEmail_frm : DevExpress.XtraEditors.XtraForm
    {
        public string KeyID = "";
        public bool IsUpdate = false;
        public CauHinhEmail_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Huong_frm_Load(object sender, EventArgs e)
        {
            if (KeyID != "")
            {
                it.ConfigMailCls o = new it.ConfigMailCls(Properties.Settings.Default.StaffID, KeyID);
                txtEmail.Text = o.Email;
                txtServer.Text = o.Server;
                spinSendMax.EditValue = o.SendMax;
                chkEnable.Checked = o.EnableSsl;
                txtEmail.Enabled = false;
            }
            txtEmail.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập email. Xin cảm ơn");
                txtEmail.Focus();
                return;
            }
            if (txtPassword.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập mật khẩu. Xin cảm ơn");
                txtPassword.Focus();
                return;
            }
            if (txtServer.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập Server. Xin cảm ơn");
                txtServer.Focus();
                return;
            }
            
            it.ConfigMailCls o = new it.ConfigMailCls();
            o.Email = txtEmail.Text;
            o.EnableSsl = chkEnable.Checked;
            o.MaNV = Properties.Settings.Default.StaffID;
            o.Password = it.CommonCls.MaHoa(txtPassword.Text);
            o.SendMax = int.Parse(spinSendMax.EditValue.ToString());
            o.Server = txtServer.Text;

            if (KeyID != "")
            {
                o.Email = KeyID;
                o.Update();
            }
            else
                o.Insert();
            
            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }
    }
}