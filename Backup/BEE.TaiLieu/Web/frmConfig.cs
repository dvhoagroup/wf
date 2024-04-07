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

namespace BEEREMA.Web
{
    public partial class frmConfig : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        webConfig objConfig;

        public frmConfig()
        {
            InitializeComponent();
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            objConfig = db.webConfigs.Single();

            txtWebsite.EditValue = objConfig.WebSite;
            txtEmail.EditValue = objConfig.Email;
            txtServer.EditValue = objConfig.MailServer;
            chkEnable.EditValue = objConfig.EnableSsl;
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtWebsite.Text == "")
            {
                DialogBox.Error("Vui lòng nhập website");
                txtWebsite.Focus();
                return;
            }
            if (txtEmail.Text == "")
            {
                DialogBox.Error("Vui lòng nhập email");
                txtEmail.Focus();
                return;
            }
            if (txtPassword.Text == "")
            {
                DialogBox.Error("Vui lòng nhập mật khẩu");
                txtPassword.Focus();
                return;
            }
            if (txtServer.Text == "")
            {
                DialogBox.Error("Vui lòng nhập Server");
                txtServer.Focus();
                return;
            }

            objConfig.WebSite = txtWebsite.Text;
            objConfig.Email = txtEmail.Text;
            objConfig.PassWord = it.EncDec.Encrypt(txtPassword.Text);
            objConfig.MailServer = txtServer.Text;
            objConfig.EnableSsl = chkEnable.Checked;

            db.SubmitChanges();

            DialogBox.Infomation("Dữ liệu đã được lưu");

            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}