using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;
using BEE.ThuVien;
using System.Linq;
using System.Net.Mail;

namespace BEE.NhanVien.Agency
{
    public partial class frmResetPass : DevExpress.XtraEditors.XtraForm
    {
        public byte KeyID = 0;
        public bool IsUpdate = false;
        public frmResetPass()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Huong_frm_Load(object sender, EventArgs e)
        {
            if (KeyID != 0)
            {
                using (var db = new MasterDataContext())
                {
                    var objNv = db.DoiTacDangKies.FirstOrDefault(p => p.ID == KeyID);
                    //   rdoGhiNho.SelectedIndex = (int)objNv.RememberInfo - 1;
                    //  chkChangeFirst.Checked = objNv.IsChangePassFirst == null ? false : true;
                }
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                var objNv = db.DoiTacDangKies.FirstOrDefault(p => p.ID == KeyID);
             
                if (rdoGhiNho.SelectedIndex == 2)
                {
                    if (txtPass.Text != txtConfirmPass.Text)
                    {
                        DialogBox.Infomation("Password và confirm pass không giống nhau.");
                        return;
                    }
                    objNv.Password = it.CommonCls.MaHoa(txtPass.Text);
                }
                else if (rdoGhiNho.SelectedIndex == 0)
                {
                    objNv.Password = it.CommonCls.MaHoa(objNv.Password);
                }
                else
                {
                    objNv.Password = "041ZDYGrjUdxEPldYXQFZuAeBL6vE/fGIVf9yinRduX/qSmOoqrhBukPUP4p/oqoJufhba74GMhYqqASKkz0OAVMjXhq4Bufzw0vDval5rBE/sudurdXom+ieiNRue6NEzPM9vHJsWdoecRdTSwTZRzShSMUu9Stm9dXh9L7im0=";
                }

                db.SubmitChanges();

                ////Send mail
                //var objAcc = db.mailConfigs.Where(p => p.IsForgotPassword.GetValueOrDefault()).FirstOrDefault();
                //if (objAcc == null)
                //{
                //    DialogBox.Infomation("Vui lòng thiết lập mail [Mặc định] trước.");
                //    return;
                //}

                //MailProviderCls objMail = new MailProviderCls();
                //var objMailForm = new MailAddress(objAcc.Email, objAcc.Username);
                //objMail.MailAddressFrom = objMailForm;
                //var objMailTo = new MailAddress(objNv.Email, objNv.Username);
                //objMail.MailAddressTo = objMailTo;
                //objMail.SmtpServer = objAcc.Server;
                //objMail.EnableSsl = objAcc.EnableSsl.Value;
                //objMail.PassWord = it.EncDec.Decrypt(objAcc.Password);
                //objMail.Port = objAcc.Port ?? 0;
                //objMail.Subject = "Hệ thống BEEREM tài khoản đăng nhập";

                //string str = string.Format("Dear {0},<br/><br/>* Thông tin tài khoản:<br/>", objNv.Username);
                //str += string.Format("- Mã số: <b>{0}</b><br/>", objNv.Username);
                //str += string.Format("- Password: <b>{0}</b><br/><br/>", it.CommonCls.GiaiMa(objNv.Password));
                //str += "---------------------------------------------------<br/>";
                //str += "<a href='http://agency.hoaland.com.vn'>HOLAND.</a>";
                //objMail.Content = str;

                //try
                //{
                //    objMail.SendMailV2();
                //}
                //catch { }
            }
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }

        private void rdoGhiNho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoGhiNho.SelectedIndex == 2)
            {
                txtPass.Enabled = true;
                txtConfirmPass.Enabled = true;
            }
            else
            {
                txtPass.Enabled = false;
                txtConfirmPass.Enabled = false;
            }
        }
    }
}