using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.QuangCao.SMS
{
    public partial class ctlTemplatesSMS : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();
        public int KeyID = 0;
        public bool IsChoice = false;

        public ctlTemplatesSMS()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
        }

        public ctlTemplatesSMS(bool isChoice)
        {
            InitializeComponent();
            IsChoice = isChoice;
        }

        private void ctlTemplates_Load(object sender, EventArgs e)
        {
            LoadData();
            if (!IsChoice)
            {
                itemClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemChoice.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        void LoadData()
        {
            db = new MasterDataContext();
            gcTemplates.DataSource = db.SMSTemplates.OrderByDescending(p => p.TempName).AsEnumerable().Select((p, index) => new { STT = index + 1, p.TempName, p.TempID, p.Contents, p.CateID, CateName = p.SMSCategory == null ? "" : p.SMSCategory.CateName }).ToList();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmTemplates f = new frmTemplates();
            f.ShowDialog();
            if(f.IsUpdate)
                LoadData();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        void Edit()
        {
            if (gridView1.GetFocusedRowCellValue(colTempID) != null)
            {
                frmTemplates f = new frmTemplates();
                f.KeyID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colTempID));
                f.ShowDialog();
                if (f.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn mẫu muốn sửa nội dung, xin cảm ơn.");
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gridView1.GetFocusedRowCellValue(colTempID) != null)
                {
                    if (DialogBox.Question() == DialogResult.Yes)
                    {
                        gridView1.DeleteSelectedRows();
                        db.SubmitChanges();
                    }
                }
                else
                    DialogBox.Infomation("Vui lòng chọn mẫu muốn xóa, xin cảm ơn.");
            }
            catch { }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ParentForm.Close();
        }

        private void itemChoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colTempID) != null)
            {
                frmChoiceTemplate f = (frmChoiceTemplate)this.ParentForm;
                f.KeyID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colTempID));
                this.ParentForm.Close();
            }
            else
                DialogBox.Infomation("Vui lòng chọn mẫu muốn xóa, xin cảm ơn.");
        }
    }
}