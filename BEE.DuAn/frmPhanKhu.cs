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
    public partial class frmPhanKhu : DevExpress.XtraEditors.XtraForm
    {
        
        public frmPhanKhu()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this, barManager1);
        }

        MasterDataContext db = new MasterDataContext();

        void PhanKhu_Load()
        {
            if (itemKhu.EditValue == null)
            {
                gcPhanKhu.DataSource = null;
                grvPhanKhu.OptionsBehavior.Editable = false;
            }
            else
            {
                gcPhanKhu.DataSource = db.PhanKhus.Where(p => p.MaKhu == (int?)itemKhu.EditValue);
                grvPhanKhu.OptionsBehavior.Editable = true;
            }
        }
        
        private void frmPhanKhu_Load(object sender, EventArgs e)
        {
            lookDuAn.DataSource = db.DuAns.Select(p => new { p.MaDA, p.TenDA }).ToList();
            PhanKhu_Load();
        }

        private void itemDuAn_EditValueChanged(object sender, EventArgs e)
        {
            lookKhu.DataSource = db.Khus.Where(p => p.MaDA == (int?)itemDuAn.EditValue);
            itemKhu.EditValue = null;
        }

        private void itemKhu_EditValueChanged(object sender, EventArgs e)
        {
            PhanKhu_Load();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhanKhu_Load();
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

        private void grvPhanKhu_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvPhanKhu.SetFocusedRowCellValue("MaKhu", itemKhu.EditValue);
        }

        private void grvPhanKhu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.Question() == System.Windows.Forms.DialogResult.No) return;
                grvPhanKhu.DeleteSelectedRows();
            }
        }

        
    }
}