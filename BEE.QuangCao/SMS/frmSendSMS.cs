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
    public partial class frmSendSMS : DevExpress.XtraEditors.XtraForm
    {
        public List<DIPSMS.Item> ListSMS { get; set; }

        private SmsConfig objConfig;

        public frmSendSMS()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void frmSmsMarketing_Load(object sender, EventArgs e)
        {
            var wait = DialogBox.WaitingForm();

            objConfig = new SmsConfig();
            objConfig.getAccount();

            DIPSMS.Hotting sms = new DIPSMS.Hotting(objConfig.ClientNo, objConfig.ClientPass);
            if (sms.CheckClient())
            {
                cmbSenderName.Properties.Items.Clear();
                cmbSenderName.Properties.Items.Add("DIPSMSHosting");
                if (cmbSenderName.SelectedIndex > 0)
                {
                    List<DIPSMS.Item> list = sms.GetClientSenderNameList();
                    foreach (DIPSMS.Item item in list)
                    {
                        if (item.SenderNameStatus == 3)
                            cmbSenderName.Properties.Items.Add(item.SenderName);
                    }
                }
                cmbSenderName.SelectedIndex = 0;

                txtContent.Text = "Kinh chao Ong/Ba [TenKH], CTCP Duc Khai thong bao";
            }
            else
            {
                DialogBox.Infomation("Tài khoản sms không tồn tại.");
            }

            wait.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            DialogBox.Infomation("Chức năng này chưa được kích hoạch. Vui lòng liên hệ DIP Vietnam, xin cảm ơn.");
            //return;
            //if (txtContent.Text.Trim() == "")
            //{
            //    DialogBox.Infomation("Vui lòng nhập nội dung.");
            //    txtContent.Focus();
            //    return;
            //}

            //foreach (DIPSMS.Item item in this.ListSMS)
            //{
            //    item.Message = txtContent.Text;
            //    item.Message = item.Message.Replace("[TenKH]", item.SenderName);
            //    //item.Message = item.Message.Replace("[Pass]", item.ServiceTypeName);
            //}

            //DIPSMS.Hotting hos = new DIPSMS.Hotting(objConfig.ClientNo, objConfig.ClientPass);
            //string SenderName = cmbSenderName.SelectedIndex > 0 ? cmbSenderName.Text : "";
            //hos.SendSMSToMultiMessage(SenderName, ListSMS);

            //DialogBox.Infomation("Đã gửi SMS thành công.");
            //this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtContent_EditValueChanged(object sender, EventArgs e)
        {
            lblCharCount.Text = string.Format("{0} ký tự", 160 - txtContent.Text.Length);
        }

        private void linkEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtContent.Text = txtContent.Text.Insert(txtContent.SelectionStart, "[TenKH]");
        }

        private void linkPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtContent.Text = txtContent.Text.Insert(txtContent.SelectionStart, "[Pass]");
        }
    }
}