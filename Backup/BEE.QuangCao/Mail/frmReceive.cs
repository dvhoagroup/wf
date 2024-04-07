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
    public partial class frmReceive : DevExpress.XtraEditors.XtraForm
    {
        public int? ReceID { get; set; }

        MasterDataContext db = new MasterDataContext();
        mailReceive objRece;

        public frmReceive()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void frmReceive_Load(object sender, EventArgs e)
        {
            if (ReceID != null)
            {
                objRece = db.mailReceives.Single(p => p.ReceID == ReceID);
                txtTempName.Text = objRece.ReceName;
                txtContents.Text = objRece.Description;
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTempName.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập tên danh sách");
                txtTempName.Focus();
                return;
            }

            if (ReceID == null)
            {
                objRece = new mailReceive();
                objRece.DateCreate = DateTime.Now;
                objRece.StaffID = BEE.ThuVien.Common.StaffID;
                db.mailReceives.InsertOnSubmit(objRece);
            }

            objRece.ReceName = txtTempName.Text;
            objRece.Description = txtContents.Text;
            objRece.StaffModify = BEE.ThuVien.Common.StaffID;
            objRece.DateModify = DateTime.Now;

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