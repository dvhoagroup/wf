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

namespace BEE.QuangCao.Mail
{
    public partial class frmConfigReminder : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        SetHappyBirthday objSHB;
        public byte SetID = 2;
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
            if (SetID == 5)
                Text = "Cấu hình gửi mail chúc mừng sinh nhật";
            lookUpTemplate.Properties.DataSource = db.mailTemplates.Select(p => new { p.Contents, p.TempID, p.TempName, CateName = p.mailCategory == null ? "" : p.mailCategory.CateName });
            lookUpMail.Properties.DataSource = db.mailConfigs.Select(p => new { p.ID, p.Email});
            objSHB = db.SetHappyBirthdays.Single(p => p.SetID == SetID);

            htmlContent.InnerHtml = objSHB.NoiDung;
            spinNhacTruoc.EditValue = objSHB.SoNgay;
            if (objSHB.ThoiGianNhac != null)
                dateReminder.DateTime = objSHB.ThoiGianNhac.Value;

            if (objSHB.ThoiGianGui != null)
                dateSend.DateTime = objSHB.ThoiGianGui.Value;

            lookUpTemplate.EditValue = objSHB.MaThiep;
            if (objSHB.MailID != null)
                lookUpMail.EditValue = objSHB.MailID;
            chkIsAuto.Checked = objSHB.IsAuto.Value;
        }

        private void lookUpTemplate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit _New = (LookUpEdit)sender;
                htmlContent.InnerHtml = _New.GetColumnValue("Contents").ToString();
            }
            catch { }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (lookUpMail.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn email gửi, xin cảm ơn.");
                lookUpMail.Focus();
                return;
            }

            if (lookUpTemplate.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn mẫu nhắc nợ, xin cảm ơn.");
                lookUpTemplate.Focus();
                return;
            }

            objSHB.IsAuto = chkIsAuto.Checked;
            objSHB.GiaHan = 0;
            objSHB.MaThiep = Convert.ToInt32(lookUpTemplate.EditValue);
            objSHB.NoiDung = htmlContent.InnerHtml;
            objSHB.SoNgay = Convert.ToInt32(spinNhacTruoc.EditValue);
            objSHB.MailID = Convert.ToInt32(lookUpMail.EditValue);
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
    }
}