using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using System.Linq;
using BEEREMA;

namespace BEE.QuangCao.SMS
{
    public partial class frmTemplates : DevExpress.XtraEditors.XtraForm
    {
        public int KeyID = 0;
        public bool IsUpdate = false;
        DateTime? DateCreate;
        int? StaffID = 0;

        MasterDataContext db;
        SMSTemplate objSMSTemp;
        public frmTemplates()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
            db = new MasterDataContext();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTempName.Text.Trim() == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên biểu mẫu. Xin cảm ơn.");
                txtTempName.Focus();
                return;
            }

            if (txtContents.Text.Trim() == "")
            {
                DialogBox.Infomation("Vui lòng nhập nội dung mẫu. Xin cảm ơn.");
                txtContents.Focus();
                return;
            }

            if (lookUpCategory.Text == "<Vui lòng chọn>")
            {
                DialogBox.Infomation("Vui lòng chọn loại mẫu. Xin cảm ơn.");
                lookUpCategory.Focus();
                return;
            }

            if (KeyID <= 0)
            {
                objSMSTemp = new SMSTemplate();
                objSMSTemp.DateCreate = DateTime.Now;
                objSMSTemp.StaffID = BEE.ThuVien.Common.StaffID;
            }
            else
            {
                objSMSTemp.StaffIDModify = BEE.ThuVien.Common.StaffID;
                objSMSTemp.DateModify = DateTime.Now;
                objSMSTemp.DateCreate = DateCreate;
                objSMSTemp.StaffID = StaffID;
            }

            objSMSTemp.TempName = txtTempName.Text;
            objSMSTemp.Contents = txtContents.Text;
            objSMSTemp.CateID = Convert.ToInt32(lookUpCategory.EditValue);   

            if (KeyID <= 0)
                db.SMSTemplates.InsertOnSubmit(objSMSTemp);

            try
            {
                db.SubmitChanges();
                DialogBox.Infomation();
                IsUpdate = true;
                this.Close();
            }
            catch { DialogBox.Infomation("Đã xảy ra lỗi, vui lòng thử lại lần nữa, xin cảm ơn."); }
        }

        private void frmTemplates_Load(object sender, EventArgs e)
        {
            lookUpCategory.Properties.DataSource = db.SMSCategories;
            if (KeyID != 0)
            {
                objSMSTemp = db.SMSTemplates.Single(p => p.TempID == KeyID);
                txtTempName.Text = objSMSTemp.TempName;
                txtContents.Text = objSMSTemp.Contents;
                lookUpCategory.EditValue = objSMSTemp.CateID;
                StaffID = objSMSTemp.StaffID;
                DateCreate = objSMSTemp.DateCreate;
            }
            else
                lookUpCategory.ItemIndex = 0;
        }

        private void btnFields_Click(object sender, EventArgs e)
        {
            frmFields frm = new frmFields();
            frm.txtContent = txtContents;
            frm.Show(this);
        }

        private void txtContents_EditValueChanged(object sender, EventArgs e)
        {
            MemoEdit _New = (MemoEdit)sender;
            int Number = (int)Math.Ceiling((double)_New.Text.Length / 160);
            lblTotalChar.Text = string.Format("Số ký tự còn lại: <b>{0}</b> [<b>{1}{2}</b>] tin nhắn (<b>{3}</b> ký tự)", _New.Text.Length % 160 == 0 ? 160 : (160 * Number) - _New.Text.Length, Number < 10 ? "0" : "", _New.Text.Length % 160 == 0 ? Number + 1 : Number, _New.Text.Length);
        }
    }
}