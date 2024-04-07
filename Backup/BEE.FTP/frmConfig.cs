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

namespace BEE.FTP
{
    public partial class frmConfig : DevExpress.XtraEditors.XtraForm
    {
        public frmConfig()
        {
            InitializeComponent();
        }

        private MasterDataContext db;
        private tblConfig objConfig;
          
        private void frmConfig_Load(object sender, EventArgs e)
        {
            try
            {
                db = new MasterDataContext();
                objConfig = db.tblConfigs.First();
                txtFtpUrl.EditValue = objConfig.FtpUrl;
                txtFtpUser.EditValue = objConfig.FtpUser;
                txtWebUrl.EditValue = objConfig.WebUrl;
            }
            catch { }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                objConfig.FtpUrl = txtFtpUrl.Text.Trim();
                objConfig.FtpUser = txtFtpUser.Text.Trim();
                objConfig.FtpPass = it.CommonCls.MaHoa(txtFtpPass.Text.Trim());
                objConfig.WebUrl = txtWebUrl.Text.Trim();
                db.SubmitChanges();

                DialogBox.Infomation();
                this.Close();
            }
            catch { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}