using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.QuangCao.SMS
{
    public partial class frmAccount : DevExpress.XtraEditors.XtraForm
    {
        public frmAccount()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void frmAccount_Load(object sender, EventArgs e)
        {
            try
            {
                SmsConfig objConfig = new SmsConfig();
                objConfig.getAccount();
                txtClientNo.Text = objConfig.ClientNo;
                txtClientNo.Focus();
            }
            catch (Exception ex){
                DialogBox.Infomation("Error: " + ex.Message);
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClientNo.Text.Trim() == "")
                {
                    DialogBox.Error("Vui lòng nhập tên đăng nhập");
                    txtClientNo.Focus();
                    return;
                }
                if (txtOldPass.Text.Trim() == "")
                {
                    DialogBox.Error("Vui lòng nhập mật khẩu");
                    txtOldPass.Focus();
                    return;
                }

                var sms = new SMSService.ServiceSoapClient("ServiceSoap");
                if (sms.checkClient(txtClientNo.Text, txtOldPass.Text) < 0)
                {
                    DialogBox.Error("Tài khoản không tồn tại");
                    return;
                }

                if (txtNewPass.Text.Trim() != "")
                {
                    if (sms.changePassword(txtClientNo.Text, txtOldPass.Text, txtNewPass.Text) < 0)
                    {
                        DialogBox.Error("Mật khẩu cũ không chính xác");
                        return;
                    }
                }

                SmsConfig objConfig = new SmsConfig();
                objConfig.ClientNo = txtClientNo.Text;
                if (txtNewPass.Text != "")
                    objConfig.ClientPass = it.EncDec.Encrypt(txtNewPass.Text);
                else
                    objConfig.ClientPass = it.EncDec.Encrypt(txtOldPass.Text);
                objConfig.setAccount();

                DialogBox.Infomation("Tài khoản SMS đã cập nhật thành công");
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}