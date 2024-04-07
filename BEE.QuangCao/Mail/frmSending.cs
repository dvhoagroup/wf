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
    public partial class frmSending : DevExpress.XtraEditors.XtraForm
    {
        public int? SendID { get; set; }

        MasterDataContext db = new MasterDataContext();
        mailSending objSend;     

        public frmSending()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void frmSending_Load(object sender, EventArgs e)
        {
            lookEmail.Properties.DataSource = db.mailConfigs.Select(p => new { p.ID, p.Email });
            gcReceive.DataSource = db.mailSending_getReceives(SendID);

            if (SendID != null)
            {
                objSend = db.mailSendings.Single(p => p.SendID == SendID);
                txtTitle.EditValue = objSend.Title;
                lookEmail.EditValue = objSend.MailID;
                ckbActive.EditValue = objSend.Active;
                dateSend.EditValue = objSend.DateSend;
                htmlContent.InnerHtml = objSend.Contents;
            }
            else
            {
                objSend = new mailSending();
                dateSend.DateTime = DateTime.Now;
            }
        }

        private void btnSendList_Add_Click(object sender, EventArgs e)
        {
            frmReceiveSelect frm = new frmReceiveSelect();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                List<int> listRece = new List<int>();
                for (int i = 0; i < grvReceive.RowCount; i++)
                    listRece.Add((int)grvReceive.GetRowCellValue(i, "ReceID"));

                foreach (var r in frm.Selection)
                {
                    if (listRece.IndexOf(r) < 0)
                    {
                        var objRece = db.mailReceives.Single(p => p.ReceID == r);
                        grvReceive.AddNewRow();
                        grvReceive.SetFocusedRowCellValue("ReceID", objRece.ReceID);
                        grvReceive.SetFocusedRowCellValue("ReceName", objRece.ReceName);
                        grvReceive.SetFocusedRowCellValue("Description", objRece.Description);
                        grvReceive.SetFocusedRowCellValue("StaffCreate", objRece.NhanVien.HoTen);
                        grvReceive.SetFocusedRowCellValue("DateCreate", objRece.DateCreate);
                        grvReceive.SetFocusedRowCellValue("StaffModify", objRece.NhanVien1.HoTen);
                        grvReceive.SetFocusedRowCellValue("DateModify", objRece.DateModify);
                    }
                }
                grvReceive.FocusedRowHandle = -1;
            }
        }

        private void btnSendList_Remove_Click(object sender, EventArgs e)
        {
            if (DialogBox.Question() == DialogResult.No) return;

            grvReceive.DeleteSelectedRows();
        }

        private void btnTemplate_Click(object sender, EventArgs e)
        {
            frmTemplateSelect frm = new frmTemplateSelect();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                htmlContent.InnerHtml = frm.Content;
            }
        }

        private void btnFieldList_Click(object sender, EventArgs e)
        {
            frmFields frm = new frmFields();
            frm.txtContent = htmlContent;
            frm.Show(this);
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập tên việc gửi");
                txtTitle.Focus();
                return;
            }

            if (lookEmail.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn email gửi");
                lookEmail.Focus();
                return;
            }

            if (dateSend.Text == "")
            {
                DialogBox.Error("Vui lòng nhập thời điểm gửi");
                dateSend.Focus();
                return;
            }

            if (htmlContent.InnerHtml == null || htmlContent.InnerHtml.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập nội dung");
                htmlContent.Focus();
                return;
            }

            objSend.Title = txtTitle.Text;
            objSend.MailID = (int)lookEmail.EditValue;
            objSend.Active = ckbActive.Checked;
            objSend.DateSend = dateSend.DateTime;
            objSend.Contents = htmlContent.InnerHtml;
            objSend.StaffModify = BEE.ThuVien.Common.StaffID;
            objSend.DateModify = DateTime.Now;

            if (SendID == null)
            {
                objSend.DateCreate = DateTime.Now;
                objSend.StaffID = BEE.ThuVien.Common.StaffID;
                db.mailSendings.InsertOnSubmit(objSend);
            }
            else
            {
                //Remove receives
                List<int> listRece = new List<int>();
                for (int i = 0; i < grvReceive.RowCount; i++)
                    listRece.Add((int)grvReceive.GetRowCellValue(i, "ReceID"));
                foreach (var l in objSend.mailSendingReceives)
                {
                    if (listRece.IndexOf(l.ReceID.Value) < 0)
                    {  
                        db.mailSendingReceives.DeleteOnSubmit(l);
                    }
                }
            }

            for (int i = 0; i < grvReceive.RowCount; i++)
            {
                int receID = (int)grvReceive.GetRowCellValue(i, "ReceID");
                if (objSend.mailSendingReceives.Where(p => p.ReceID == receID).Count() <= 0)
                {
                    var objRece = new mailSendingReceive();
                    objRece.ReceID = receID;
                    objSend.mailSendingReceives.Add(objRece);
                }
            }
      
            db.SubmitChanges();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
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