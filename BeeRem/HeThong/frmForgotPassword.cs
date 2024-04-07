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
            //defaultLookAndFeel1.LookAndFeel.SkinName = Properties.Settings.Default.Skins;
        }

        private int totalSeconds;

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
            grOtp.Visible = false;
            this.Width = 319;

            BEE.NgonNgu.Language.TranslateControl(this);
        }


        private void btnDongY_Click(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                var obj = db.NhanViens.SingleOrDefault(p => p.MaSo == txtMaSo.Text.Trim() & p.Email == txtEmail.Text.Trim());
                if (obj == null)
                {
                    DialogBox.Error("[Mã số] hoặc [Email] không chính xác.");
                    return;
                }
                var ran = new Random();
                var strOtp = ran.Next(1000, 9999).ToString();

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
                objMail.Subject = "Hệ thống HOALAND tài khoản đăng nhập";

                string str = string.Format("Dear {0},<br/><br/>* Thông tin tài khoản:<br/>", obj.HoTen);
                str += string.Format("- Mã số: <b>{0}</b><br/>", txtMaSo.Text.Trim());
                str += string.Format("- Otp: <b>{0}</b><br/><br/>", strOtp);
                str += "---------------------------------------------------<br/>";
                str += "<a href='https://hoaland.com'>HOALAND.</a>";
                objMail.Content = str;

                try
                {
                    objMail.SendMailV2();
                    var otp = db.ForgotPasswords.FirstOrDefault(p => p.MaNV == obj.MaNV);
                    if (otp != null)
                    {
                        otp.Otp = strOtp;
                    }
                    else
                    {
                        otp = new ForgotPassword();
                        otp.MaNV = obj.MaNV; otp.Otp = strOtp;
                        db.ForgotPasswords.InsertOnSubmit(otp);
                    }
                    db.SubmitChanges();

                    lblTime.Text = "";
                    lblTime.ForeColor = Color.Black;
                    int minute = 3;
                    int seconds = 0;
                    totalSeconds = (minute * 60) + seconds;
                    timerCountTime.Enabled = true;

                    btnDongY.Enabled = false;
                    grOtp.Visible = true;
                    this.Width = 547;
                }
                catch (Exception ex)
                {
                    DialogBox.Error("Gửi mail không thành công. Vui lòng kiếm tra lại.");
                    return;
                }
            }
        }

        private void hyperLinkEdit1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://beesky.vn");
        }

        private void btnAcceptOtp_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    var obj = db.NhanViens.SingleOrDefault(p => p.MaSo == txtMaSo.Text.Trim() & p.Email == txtEmail.Text.Trim());
                    var otp = db.ForgotPasswords.FirstOrDefault(p => p.MaNV == obj.MaNV && p.Otp == txtOtp.Text);
                    if (otp != null)
                    {
                        obj.MatKhau = it.CommonCls.MaHoa(BEE.ThuVien.Common.GetPasswordAuto());
                        obj.IsChangePassFirst = true;
                        obj.ChangedPass = false;
                        db.SubmitChanges();

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
                        objMail.Subject = "Hệ thống HOALAND tài khoản đăng nhập";

                        string str = string.Format("Dear {0},<br/><br/>* Thông tin tài khoản:<br/>", obj.HoTen);
                        str += string.Format("- Mã số: <b>{0}</b><br/>", txtMaSo.Text.Trim());
                        str += string.Format("- Mật khẩu mới của bạn: <b>{0}</b><br/><br/>", it.CommonCls.GiaiMa(obj.MatKhau));
                        str += "---------------------------------------------------<br/>";
                        str += "<a href='https://hoaland.com'>HOALAND.</a>";
                        objMail.Content = str;

                        try
                        {
                            objMail.SendMailV2();

                        }
                        catch (Exception ex)
                        {
                            DialogBox.Error("Gửi mail không thành công. Vui lòng kiếm tra lại.");
                            return;
                        }

                        DialogBox.Infomation($"Mật khẩu đã được gửi vào mail [{obj.Email}].");
                    }
                    else
                    {
                        DialogBox.Warning("Opt không đúng. Vui lòng nhập lại!");
                    }
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void timerCountTime_Tick(object sender, EventArgs e)
        {
            if (totalSeconds > 0)
            {
                totalSeconds--;
                int minute = totalSeconds / 60;
                int seconds = totalSeconds - (minute * 60);
                lblTime.Text = "Thời gian còn lại: " + minute.ToString() + ":" + seconds.ToString();
            }
            else
            {
                timerCountTime.Stop();
                btnAcceptOtp.Enabled = false;
                btnDongY.Enabled = true;

                using (var db = new MasterDataContext())
                {
                    var obj = db.NhanViens.SingleOrDefault(p => p.MaSo == txtMaSo.Text.Trim() & p.Email == txtEmail.Text.Trim());
                    var otp = db.ForgotPasswords.FirstOrDefault(p => p.MaNV == obj.MaNV);
                    if (otp != null)
                    {
                        otp.Otp = string.Empty;
                    }
                    db.SubmitChanges();
                }
                lblTime.Text = "OTP đã hết hạn, vui lòng thử lại";
                lblTime.ForeColor = Color.Red;
            }
        }
    }
}