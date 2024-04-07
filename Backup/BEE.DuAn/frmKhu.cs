using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.DuAn
{
    public partial class frmKhu : DevExpress.XtraEditors.XtraForm
    {
        
        public frmKhu()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this, barManager1);
        }

        MasterDataContext db = new MasterDataContext();

        void Khu_Load()
        {
            if (itemDuAn.EditValue == null)
            {
                gcKhu.DataSource = null;
                grvKhu.OptionsBehavior.Editable = false;
            }
            else
            {
                gcKhu.DataSource = db.Khus.Where(p => p.MaDA == (int?)itemDuAn.EditValue);
                grvKhu.OptionsBehavior.Editable = true;
            }
        }
        
        private void frmKhu_Load(object sender, EventArgs e)
        {
            lookDuAn.DataSource = db.DuAns.Select(p => new { p.MaDA, p.TenDA }).ToList();
        }

        private void itemDuAn_EditValueChanged(object sender, EventArgs e)
        {
            Khu_Load();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khu_Load();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();

                DialogBox.Infomation();
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void grvKhu_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvKhu.SetFocusedRowCellValue("MaDA", itemDuAn.EditValue);
        }

        private void grvKhu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.Question() == System.Windows.Forms.DialogResult.No) return;
                grvKhu.DeleteSelectedRows();
            }
        }

        private void repositoryItemButtonEditImageUrl_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var frm = new FTP.frmUploadFile();
            if (frm.SelectFile(false))
            {
                frm.Folder = "doc/" + DateTime.Now.ToString("yyyy/MM/dd");
                frm.ClientPath = frm.ClientPath;
                frm.ShowDialog();
                if (frm.DialogResult != DialogResult.OK) return;
                grvKhu.SetFocusedRowCellValue("ImageUrl", frm.FileName);
            }
            frm.Dispose();
        }
    }
}