using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.QuangCao.Mail
{
    public partial class frmConfig : DevExpress.XtraEditors.XtraForm
    {
        public int? MailID { get; set; }

        MasterDataContext db = new MasterDataContext();
        mailConfig objConfig;

        public frmConfig()
        {
            InitializeComponent();
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            if (MailID != null)
            {
                objConfig = db.mailConfigs.Single(p => p.ID == MailID);
                txtEmail.EditValue = objConfig.Email;
                txtServer.EditValue = objConfig.Server;
                spinSendMax.EditValue = objConfig.SendMax;
                chkEnable.EditValue = objConfig.EnableSsl;
                spinPort.EditValue = objConfig.Port ?? 0;
                txtUsername.Text = objConfig.Username;
                ckNoiBo.Checked = objConfig.IsNoiBo ?? false;
                chkDefault.Checked = objConfig.IsForgotPassword.GetValueOrDefault();
                txtURLLogo1.EditValue = objConfig.Logo1;
                txtURLLogo2.EditValue = objConfig.Logo2;

                txtPassword.EditValue = it.EncDec.Decrypt(objConfig.Password);

            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                DialogBox.Error("Vui lòng nhập [Email], xin cảm ơn.");
                txtEmail.Focus();
                return;
            }
            if (txtUsername.Text == "")
            {
                DialogBox.Error("Vui lòng nhập [Tên hiển thị], xin cảm ơn.");
                txtUsername.Focus();
                return;
            }

            if (txtPassword.Text == "")
            {
                DialogBox.Error("Vui lòng nhập [Mật khẩu], xin cảm ơn.");
                txtPassword.Focus();
                return;
            }

            if (txtServer.Text == "")
            {
                DialogBox.Error("Vui lòng nhập [Server], xin cảm ơn.");
                txtServer.Focus();
                return;
            }

            if (spinPort.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập [Port], xin cảm ơn.");
                spinPort.Focus();
                return;
            }

            if (MailID == null)
            {
                objConfig = new mailConfig();
                db.mailConfigs.InsertOnSubmit(objConfig);
            }
            
            objConfig.Email = txtEmail.Text;
            objConfig.Password = it.EncDec.Encrypt(txtPassword.Text);
            objConfig.Server = txtServer.Text;
            objConfig.EnableSsl = chkEnable.Checked;
            objConfig.SendMax = int.Parse(spinSendMax.EditValue.ToString());
            objConfig.StaffID = Common.StaffID;
            objConfig.DateModify = DateTime.Now;
            objConfig.Port = Convert.ToInt32(spinPort.EditValue);
            objConfig.Username = txtUsername.Text;
            objConfig.IsForgotPassword = chkDefault.Checked;
            objConfig.IsNoiBo = ckNoiBo.Checked;
            objConfig.Logo1 = txtURLLogo1.Text;
            objConfig.Logo2 = txtURLLogo2.Text;
            try
            {
                db.SubmitChanges();
                DialogBox.Infomation();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { DialogBox.Infomation("Error: " + ex.Message); }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}