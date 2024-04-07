using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using BEE.ThuVien;
using System.Linq;
using System.Net.Mail;

namespace BEEREMA.HeThong
{
    public partial class frmForgotPassword : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        bool click = false;
        public frmForgotPassword()
        {
            InitializeComponent();
            defaultLookAndFeel1.LookAndFeel.SkinName = Properties.Settings.Default.Skins;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void Login_frm_Load(object sender, EventArgs e)
        {            
            if (Properties.Settings.Default.SavePassword)
                txtMaSo.Text = Properties.Settings.Default.UserName;

            BEE.NgonNgu.Language.TranslateControl(this); 
        }
        
        private void btnDongY_Click(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                var obj = db.NhanViens.SingleOrDefault(p => p.MaSo == txtMaSo.Text.Trim() & p.Email == txtEmail.Text.Trim());
                if (obj == null)
                {
                    DialogBox.Error("[Mã số] hoặc [Mật khẩu] không chính xác.");
                    return;
                }

                //Send mail
                var objAcc = db.mailConfigs.Where(p => p.IsForgotPassword.GetValueOrDefault()).FirstOrDefault();
                if (objAcc == null)
                {
                    DialogBox.Infomation("Vui lòng thiết lập mail [Mặc định] trước.");
                    return;
                }

                MailProviderCls objMail = new MailProviderCls();
                var objMailForm = new MailAddress(objAcc.Email, objAcc.Username);
                objMail.MailAddressFrom = objMailForm;
                var objMailTo = new MailAddress(txtEmail.Text.Trim(), txtMaSo.Text.Trim());
                objMail.MailAddressTo = objMailTo;
                objMail.SmtpServer = objAcc.Server;
                objMail.EnableSsl = objAcc.EnableSsl.Value;
                objMail.PassWord = it.EncDec.Decrypt(objAcc.Password);
                objMail.Port = objAcc.Port ?? 0;
                objMail.Subject = "Hệ thống BEEREM tài khoản đăng nhập";

                string str = string.Format("Dear {0},<br/><br/>* Thông tin tài khoản:<br/>", obj.HoTen);
                str += string.Format("- Mã số: <b>{0}</b><br/>", txtMaSo.Text.Trim());
                str += string.Format("- Mật khẩu: <b>{0}</b><br/><br/>", it.CommonCls.GiaiMa(obj.MatKhau));
                str += "---------------------------------------------------<br/>";
                str += "<a href='http://beesky.vn'>BEESKY VIETNAM.</a>";
                objMail.Content = str;

                try
                {
                    objMail.SendMailV2();
                }
                catch { }
            }

            this.DialogResult = DialogResult.OK;
        }

        private void hyperLinkEdit1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://beesky.vn");
        }
    }
}