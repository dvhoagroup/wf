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
    public partial class ctlCategory : XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();

        public ctlCategory()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
        }

        void Category_Load()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                db = new MasterDataContext();
                gcCategory.DataSource = db.mailCategories;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                wait.Close();
            }
        }

        private void ctlCategory_Load(object sender, EventArgs e)
        {
            Category_Load();
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
                DialogBox.Infomation();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Category_Load();
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
