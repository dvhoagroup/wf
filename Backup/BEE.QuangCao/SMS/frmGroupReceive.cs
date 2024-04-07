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
    public partial class frmGroupReceive : DevExpress.XtraEditors.XtraForm
    {
        public int? GroupID { get; set; }
        public bool IsRemider = false;

        MasterDataContext db = new MasterDataContext();
        SMSGroupReceive objGroup;

        public frmGroupReceive()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void frmGroupReceive_Load(object sender, EventArgs e)
        {
            if (GroupID != null)
            {
                objGroup = db.SMSGroupReceives.Single(p => p.GroupID == GroupID);
                txtTempName.Text = objGroup.GroupName;
                txtContents.Text = objGroup.Description;
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTempName.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập tên danh sách. Xin cảm ơn.");
                txtTempName.Focus();
                return;
            }

            if (GroupID == null)
            {
                objGroup = new SMSGroupReceive();
                objGroup.DateCreate = DateTime.Now;
                objGroup.StaffID = BEE.ThuVien.Common.StaffID;
                db.SMSGroupReceives.InsertOnSubmit(objGroup);
            }

            objGroup.GroupName = txtTempName.Text;
            objGroup.Description = txtContents.Text; 
            objGroup.StaffModify = BEE.ThuVien.Common.StaffID;
            objGroup.DateModify = DateTime.Now;

            db.SubmitChanges();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}