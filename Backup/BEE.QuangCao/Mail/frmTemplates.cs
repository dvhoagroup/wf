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

namespace BEE.QuangCao.Mail
{
    public partial class frmTemplates : DevExpress.XtraEditors.XtraForm
    {
        public int? TempID { get; set; }

        MasterDataContext db = new MasterDataContext();
        mailTemplate objTemp;

        public frmTemplates()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void frmTemplates_Load(object sender, EventArgs e)
        {
            lookCategory.Properties.DataSource = db.mailCategories;
            if (TempID != null)
            {
                objTemp = db.mailTemplates.Single(p => p.TempID == TempID);
                txtTempName.Text = objTemp.TempName;
                htmlContent.InnerHtml = objTemp.Contents;
                lookCategory.EditValue = objTemp.CateID;
            }
            else
                lookCategory.ItemIndex = 0;
        }

        private void btnFields_Click(object sender, EventArgs e)
        {
            frmFields frm = new frmFields();
            frm.txtContent = htmlContent;
            frm.Show(this);
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTempName.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập tên biểu mẫu. Xin cảm ơn.");
                txtTempName.Focus();
                return;
            }

            if (htmlContent.InnerHtml == null || htmlContent.InnerHtml.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập nội dung mẫu. Xin cảm ơn.");
                htmlContent.Focus();
                return;
            }

            if (lookCategory.Text == "<Vui lòng chọn>")
            {
                DialogBox.Error("Vui lòng chọn loại mẫu. Xin cảm ơn.");
                lookCategory.Focus();
                return;
            }

            if (TempID == null)
            {
                objTemp = new mailTemplate();
                objTemp.DateCreate = DateTime.Now;
                objTemp.StaffID = BEE.ThuVien.Common.StaffID;
                db.mailTemplates.InsertOnSubmit(objTemp);
            }

            objTemp.TempName = txtTempName.Text;
            objTemp.Contents = htmlContent.InnerHtml;
            objTemp.CateID = Convert.ToInt16(lookCategory.EditValue);
            objTemp.StaffModify = BEE.ThuVien.Common.StaffID;
            objTemp.DateModify = DateTime.Now;

            db.SubmitChanges();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void htmlContent_ImageBrowser(object sender, Microsoft.ConsultingServices.HtmlEditor.ImageBrowserEventArgs e)
        {
            var frm = new FTP.frmUploadFile();
            if (frm.SelectFile(false))
            {
                frm.Folder = "doc/" + DateTime.Now.ToString("yyyy/MM/dd");
                frm.ClientPath = frm.ClientPath;
                frm.ShowDialog();
                if (frm.DialogResult != DialogResult.OK) return;
                e.ImageUrl = frm.WebUrl;
            }
            frm.Dispose();
        }
    }
}