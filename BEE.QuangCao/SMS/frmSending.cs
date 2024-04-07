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
    public partial class frmSending : DevExpress.XtraEditors.XtraForm
    {
        public int? SendID { get; set; }

        MasterDataContext db = new MasterDataContext();
        SMSSending objSend;     

        public frmSending()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void frmSending_Load(object sender, EventArgs e)
        {
            //Load sendername
            SmsConfig objConfig = new SmsConfig();
            objConfig.getAccount();
            //DIPSMS.Hotting sms = new DIPSMS.Hotting(objConfig.ClientNo, objConfig.ClientPass);
            //var listSender = sms.GetClientSenderNameList();
            //foreach (var l in listSender)
            //    cmbBrandName.Properties.Items.Add(l.SenderName);
            var sms = new SMSService.ServiceSoapClient("ServiceSoap");
            var listSender = sms.getBrandNames(objConfig.ClientNo, objConfig.ClientPass);
            try
            {
                foreach (var l in listSender)
                    cmbBrandName.Properties.Items.Add(l.Sender);
            }
            catch { }
            //
            gcGroupReceive.DataSource = db.SMSSending_getGroupReceives(SendID);

            if (SendID != null)
            {
                objSend = db.SMSSendings.Single(p => p.SendID == SendID);
                txtTitle.EditValue = objSend.Title;
                cmbBrandName.Text = objSend.Sendername;
                ckbActive.EditValue = objSend.IsActive;
                dateSend.EditValue = objSend.DateSend;
                txtMess.EditValue = objSend.Contents;
            }
            else
            {
                objSend = new SMSSending();
                dateSend.DateTime = DateTime.Now;
                lblTotalChar.Text = "Số ký tự còn lại: <b>160</b> [<b>01</b>] tin nhắn (<b>0</b> ký tự)";
            }
        }

        private void btnSendList_Add_Click(object sender, EventArgs e)
        {
            frmChoiceGroup frm = new frmChoiceGroup();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                List<int> listGR = new List<int>();
                for (int i = 0; i < grvGroupReceive.RowCount; i++)
                    listGR.Add((int)grvGroupReceive.GetRowCellValue(i, "GroupID"));

                foreach (var g in frm.Selection)
                {
                    if (listGR.IndexOf(g) < 0)
                    {
                        var objGR = db.SMSGroupReceives.Single(p => p.GroupID == g);
                        grvGroupReceive.AddNewRow();
                        grvGroupReceive.SetFocusedRowCellValue("GroupID", objGR.GroupID);
                        grvGroupReceive.SetFocusedRowCellValue("GroupName", objGR.GroupName);
                        grvGroupReceive.SetFocusedRowCellValue("Description", objGR.Description);
                        grvGroupReceive.SetFocusedRowCellValue("StaffName", objGR.NhanVien.HoTen);
                        grvGroupReceive.SetFocusedRowCellValue("DateCreate", objGR.DateCreate);
                        grvGroupReceive.SetFocusedRowCellValue("ModifyName", objGR.NhanVien1.HoTen);
                        grvGroupReceive.SetFocusedRowCellValue("DateModify", objGR.DateModify);
                    }
                }
                grvGroupReceive.FocusedRowHandle = -1;
            }
        }

        private void btnSendList_Remove_Click(object sender, EventArgs e)
        {
            if (DialogBox.Question() == DialogResult.No) return;

            grvGroupReceive.DeleteSelectedRows();
        }

        private void btnTemplate_Click(object sender, EventArgs e)
        {
            frmChoiceTemplate f = new frmChoiceTemplate();
            f.LoadUserControl(new ctlTemplatesSMS(true), "Chọn mẫu");
            f.ShowDialog();
            if (f.KeyID != 0)
            {
                var objSMSTemp = db.SMSTemplates.Single(p => p.TempID == f.KeyID);
                txtMess.Text = objSMSTemp.Contents;
            }
        }

        private void btnFieldList_Click(object sender, EventArgs e)
        {
            frmFields frm = new frmFields();
            frm.txtContent = txtMess;
            frm.Show(this);
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text.Trim() == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên việc gửi");
                txtTitle.Focus();
                return;
            }

            if (dateSend.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập thời điểm gửi");
                dateSend.Focus();
                return;
            }

            if (txtMess.Text.Trim() == "")
            {
                DialogBox.Infomation("Vui lòng nhập nộ dung tin nhắn");
                txtMess.Focus();
                return;
            }

            objSend.Title = txtTitle.Text;
            objSend.Sendername = cmbBrandName.Text;
            objSend.IsActive = ckbActive.Checked;
            objSend.DateSend = dateSend.DateTime;
            objSend.Contents = txtMess.Text;
            objSend.StaffModify = BEE.ThuVien.Common.StaffID;
            objSend.DateModify = DateTime.Now;

            if (SendID == null)
            {
                objSend.DateCreate = DateTime.Now;
                objSend.StaffID = BEE.ThuVien.Common.StaffID;
                db.SMSSendings.InsertOnSubmit(objSend);
            }
            else
            {
                //Remove SMSGroupReceive_Sendings
                List<int> listGR = new List<int>();
                for (int i = 0; i < grvGroupReceive.RowCount; i++)
                    listGR.Add((int)grvGroupReceive.GetRowCellValue(i, "GroupID"));
                foreach (var l in objSend.SMSGroupReceive_Sendings)
                {
                    if (listGR.IndexOf(l.GroupID.Value) < 0)
                    {  
                        db.SMSGroupReceive_Sendings.DeleteOnSubmit(l);
                    }
                }
            }

            for (int i = 0; i < grvGroupReceive.RowCount; i++)
            {
                int groupID = (int)grvGroupReceive.GetRowCellValue(i, "GroupID");
                if (objSend.SMSGroupReceive_Sendings.Where(p => p.GroupID == groupID).Count() <= 0)
                {
                    var objGR = new SMSGroupReceive_Sending();
                    objGR.GroupID = groupID;
                    objSend.SMSGroupReceive_Sendings.Add(objGR);
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

        private void txtTitle_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit _New = (TextEdit)sender;
            this.Text = _New.Text + " - Gửi SMS";
        }

        private void txtMess_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit _New = (TextEdit)sender;
            int Number = (int)Math.Ceiling((double)_New.Text.Length / 160);
            lblTotalChar.Text = string.Format("Số ký tự còn lại: <b>{0}</b> [<b>{1}{2}</b>] tin nhắn (<b>{3}</b> ký tự)", _New.Text.Length % 160 == 0 ? 160 : (160 * Number) - _New.Text.Length, Number < 10 ? "0" : "", _New.Text.Length % 160 == 0 ? Number + 1 : Number, _New.Text.Length);
        }
    }
}