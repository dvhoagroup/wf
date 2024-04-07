using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BEE.ThuVien;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.QuangCao.Mail
{
    public partial class ctlTemplates : XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();
        public bool IsChoice = false;

        public ctlTemplates()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
        }

        public ctlTemplates(bool isChoice)
        {
            InitializeComponent();
            IsChoice = isChoice;
        }

        private void ctlTemplates_Load(object sender, EventArgs e)
        {
            Template_Load();

            if (!IsChoice)
            {
                itemClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemChoice.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        void Template_Load()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                db = new MasterDataContext();
                gcTemp.DataSource = db.mailTemplates
                    .OrderByDescending(p => p.TempName).AsEnumerable()
                    .Select((p, index) => new
                    {
                        STT = index + 1,
                        p.TempID,
                        p.TempName,
                        p.mailCategory.CateName,
                        p.DateCreate,
                        StaffCreate = p.NhanVien.HoTen,
                        p.DateModify,
                        StaffModify = p.NhanVien1.HoTen
                    })
                    .ToList();
            }
            catch { }
            wait.Close();
        }

        void Template_Add()
        {
            var frm = new frmTemplates();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                Template_Load();
            }
        }

        void Template_Edit()
        {
            var tempID = (int?)grvTemp.GetFocusedRowCellValue("TempID");
            if (tempID == null)
            {
                DialogBox.Error("Vui lòng chọn mẫu");
                return;
            }

            var frm = new frmTemplates();
            frm.TempID = tempID;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                Template_Load();
            }
        }

        void Template_Delete()
        {
            var indexs = grvTemp.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn mẫu");
                return;
            }
            foreach (var i in indexs)
            {
                var objTemp = db.mailTemplates.Single(p=>p.TempID == (int)grvTemp.GetRowCellValue(i, "TempID"));
                db.mailTemplates.DeleteOnSubmit(objTemp);
            }
            db.SubmitChanges();
            Template_Load();
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Template_Add();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Template_Edit();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Template_Delete();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Template_Load();
        }

        private void grvTemp_DoubleClick(object sender, EventArgs e)
        {
            Template_Edit();
        }

        private void grvTemp_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                Template_Delete();
        }

        private void itemChoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var tempID = (int?)grvTemp.GetFocusedRowCellValue("TempID");
            if (tempID == null)
            {
                DialogBox.Error("Vui lòng chọn mẫu");
                return;
            }

            var objTemp = db.mailTemplates.Single(p => p.TempID == tempID);
            var frm = (frmTemplateSelect)this.ParentForm;
            frm.Content = objTemp.Contents;
            frm.DialogResult = DialogResult.OK;
            frm.Close();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ParentForm.Close();
        }        
    }
}