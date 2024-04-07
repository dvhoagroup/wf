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

namespace BEE.VOIPSETUP.SERVER
{
    public partial class frmEditServer : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public short? ServerID { get; set; }
        voipServerConfig objCF;
        public frmEditServer()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtServer.Text.Trim() == "")
                {
                    DialogBox.Infomation("Vui lòng nhập ServerIP");
                    txtServer.Focus();
                    return;
                }
                if (txtUser.Text.Trim() == "")
                {
                    DialogBox.Infomation("Vui lòng nhập UserName");
                    txtUser.Focus();
                    return;
                } 
                if (txtPass.Text.Trim() == "")
                {
                    DialogBox.Infomation("Vui lòng nhập PassWord");
                    txtPass.Focus();
                    return;
                }
                objCF.Host = txtServer.Text.Trim();
                objCF.UserName=txtUser.Text.Trim();
                objCF.Pass = txtPass.Text.Trim();
                objCF.Port = txtPort.Text.Trim() == "" ? "5038" : txtPort.Text.Trim();
                objCF.SDT = txtSDT.Text.Trim();
                objCF.PassCDR = txtPassCSDR.Text.Trim();
                objCF.UserCDR = txtUSERCDR.Text.Trim();
                objCF.KeyConnect = txtKey.Text.Trim();
                db.SubmitChanges();
                DialogBox.Infomation("Dữ liệu đã lưu thành công");
                this.Close();
            }
            catch
            {
                DialogBox.Error("Dữ liệu không thể lưu!");
            }
        }

        private void frmEditServer_Load(object sender, EventArgs e)
        {
            slookNhanVien.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.HoTen, p.PhongBan.TenPB });
            if (ServerID == null)
            {
                objCF = new voipServerConfig();
                db.voipServerConfigs.InsertOnSubmit(objCF);
            }
            else
            {
                objCF = db.voipServerConfigs.FirstOrDefault(p=>p.ID == ServerID);
                txtPass.Text = objCF.Pass;
                txtPort.Text = objCF.Port;
                txtServer.Text = objCF.Host;
                txtUser.Text = objCF.UserName;
                txtSDT.Text = objCF.SDT;
                txtPassCSDR.Text = objCF.PassCDR;
                txtUSERCDR.Text = objCF.UserCDR;
                txtKey.Text = objCF.KeyConnect;
            }
            gcLine.DataSource = objCF.voipLineConfigs;
        }
    }
}