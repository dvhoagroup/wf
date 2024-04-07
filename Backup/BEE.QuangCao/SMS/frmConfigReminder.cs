using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using System.Linq
;
using BEEREMA;

namespace BEE.QuangCao.SMS
{
    public partial class frmConfigReminder : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        SetHappyBirthday objSHB;
        private SmsConfig objConfig;
        public byte SetID = 3;
        public frmConfigReminder()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmConfigReminder_Load(object sender, EventArgs e)
        {
            if (SetID == 6)
                this.Text = "Cấu hình chúc mừng sinh nhật tự động (SMS)";
            
            var wait = DialogBox.WaitingForm();

            objConfig = new SmsConfig();
            objConfig.getAccount();

            var sms = new SMSService.ServiceSoapClient("ServiceSoap");
            var ltBrandName = sms.getBrandNames(objConfig.ClientNo, objConfig.ClientPass);

            DataTable tblSenderName = new DataTable("SenderNames");
            tblSenderName.Columns.Add("SenderName", typeof(string));
            DataRow r;
            r = tblSenderName.NewRow();
            r["SenderName"] = "DIPSMSHosting";
            tblSenderName.Rows.Add(r);
            try
            {
                foreach (var s in ltBrandName)
                {
                    r = tblSenderName.NewRow();
                    r["SenderName"] = s;
                    tblSenderName.Rows.Add(r);
                }
            }
            catch { }
            lookUpSenderName.Properties.DataSource = tblSenderName;
            //if (sms.CheckClient())
            //{
            //    List<DIPSMS.Item> list = sms.GetClientSenderNameList();
            //    DataTable tblSenderName = new DataTable("SenderNames");
            //    tblSenderName.Columns.Add("SenderName", typeof(string));
            //    DataRow r;
            //    r = tblSenderName.NewRow();
            //    r["SenderName"] = "DIPSMSHosting";
            //    tblSenderName.Rows.Add(r);
            //    foreach (DIPSMS.Item item in list)
            //    {                    
            //        if (item.SenderNameStatus == 3)
            //        {
            //            r = tblSenderName.NewRow();
            //            r["SenderName"] = item.SenderName;
            //            tblSenderName.Rows.Add(r);
            //        }
            //    }

            //    lookUpSenderName.Properties.DataSource = tblSenderName;
            //}
            //else
            //{
            //    DialogBox.Infomation("Tài khoản sms không tồn tại.");
            //}

            wait.Close();

            lookUpTemplate.Properties.DataSource = db.SMSTemplates.Select(p => new { p.Contents, p.TempID, p.TempName, CateName = p.SMSCategory == null ? "" : p.SMSCategory.CateName });
            objSHB = db.SetHappyBirthdays.Single(p => p.SetID == SetID);
            txtPreview.Text = objSHB.NoiDung;
            spinNhacTruoc.EditValue = objSHB.SoNgay;
            if (objSHB.Sendername != "")
                lookUpSenderName.EditValue = objSHB.Sendername;
            if (objSHB.ThoiGianNhac != null)
                dateReminder.DateTime = objSHB.ThoiGianNhac.Value;

            if (objSHB.ThoiGianGui != null)
                dateSend.DateTime = objSHB.ThoiGianGui.Value;

            lookUpTemplate.EditValue = objSHB.MaThiep;
            chkIsAuto.Checked = objSHB.IsAuto.Value;
        }

        private void lookUpTemplate_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit _New = (LookUpEdit)sender;
            txtPreview.Text = _New.GetColumnValue("Contents").ToString();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            objSHB.IsAuto = chkIsAuto.Checked;
            objSHB.GiaHan = 0;
            objSHB.MaThiep = Convert.ToInt32(lookUpTemplate.EditValue);
            objSHB.NoiDung = txtPreview.Text;
            objSHB.SoNgay = Convert.ToInt32(spinNhacTruoc.EditValue);
            objSHB.Sendername = lookUpSenderName.Text == "DIPSMSHosting" ? "" : lookUpSenderName.Text;
            if (dateSend.Text != "")
                objSHB.ThoiGianGui = dateSend.DateTime;

            if (dateReminder.Text != "")
                objSHB.ThoiGianNhac = dateReminder.DateTime;
            objSHB.SetID = SetID;

            try
            {
                db.SubmitChanges();
            }
            catch { }
            DialogBox.Infomation();
            this.Close();
        }

        private void lookUpSenderName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                lookUpSenderName.EditValue = "DIPSMSHosting";
        }
    }
}