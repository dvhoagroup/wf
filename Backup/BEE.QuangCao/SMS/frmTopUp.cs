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
    public partial class frmTopUp : DevExpress.XtraEditors.XtraForm
    {
        public frmTopUp()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            if (spinSoTien.Value <= 0)
            {
                DialogBox.Error("Vui lòng nhập số tiền");
                return;
            }

            SmsConfig objConfig = new SmsConfig();
            objConfig.getAccount();

            DIPSMS.Hotting hot = new DIPSMS.Hotting(objConfig.ClientNo, objConfig.ClientPass);
            var id = hot.Topup(spinSoTien.Value, "lytuong");
            if (id < 0)
            {
                DialogBox.Error("Vui lòng kiểm tra lại tài khoản SMS");
                return;
            }

            BaoKimPayment objBK = new BaoKimPayment();
            string urlBaoKim = objBK.createRequestUrl(id.ToString(), "info@mua24h.com.vn", spinSoTien.Value.ToString(), "0", "0", "Nap tien vao tai khoan SMS",
                "http://sms.dip.vn/success.aspx", "http://sms.dip.vn/cancel.aspx", "#");

            this.Hide();

            frmPayment frm = new frmPayment();
            frm.PayUrl = urlBaoKim;
            frm.ShowDialog(this.Owner);

            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}