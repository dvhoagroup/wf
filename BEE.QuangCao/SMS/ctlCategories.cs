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
    public partial class ctlCategories : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();
        public ctlCategories()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
        }

        private void ctlCategories_Load(object sender, EventArgs e)
        {
            gcCategory.DataSource = db.SMSCategories;
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
                DialogBox.Infomation();
                this.ParentForm.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Infomation(ex.Message);
            }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ParentForm.Close();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db = new MasterDataContext();
            gcCategory.DataSource = db.SMSCategories;
            gvCategory.FocusedRowHandle = 0;
        }

        private void gvCategory_KeyUp(object sender, KeyEventArgs e)
        {
            if (gvCategory.FocusedRowHandle < 0)
                return;
            if (e.KeyCode == Keys.Delete)
                gvCategory.DeleteSelectedRows();
        }
    }
}
