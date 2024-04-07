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

namespace BEE.DuAn
{
    public partial class frmLoo : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public frmLoo()
        {
            InitializeComponent();

            db = new MasterDataContext();
        }

        void LoadData()
        {
            db = new MasterDataContext();
            var maPK = itemPhanKhu.EditValue == null ? 0 : Convert.ToInt32(itemPhanKhu.EditValue);

            gcLoo.DataSource = db.Loos.Where(p => p.MaPK == maPK);
        }

        private void frmLoo_Load(object sender, EventArgs e)
        {
            lookUpDuAn.DataSource = db.DuAns.Select(p => new { p.MaDA, p.TenDA });
        }

        private void itemDuAn_EditValueChanged(object sender, EventArgs e)
        {
            var maDA = itemDuAn.EditValue == null ? 0 : Convert.ToInt32(itemDuAn.EditValue);
            lookUpKhu.DataSource = db.Khus.Where(p => p.MaDA == maDA);
        }

        private void itemKhu_EditValueChanged(object sender, EventArgs e)
        {
            var maKhu = itemKhu.EditValue == null ? 0 : Convert.ToInt32(itemKhu.EditValue);
            lookUpPhanKhu.DataSource = db.PhanKhus.Where(p => p.MaKhu == maKhu);
        }

        private void itemPhanKhu_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db.SubmitChanges();
            DialogBox.Infomation();
        }

        private void gvLoo_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var maPK = itemPhanKhu.EditValue == null ? 0 : Convert.ToInt32(itemPhanKhu.EditValue);
            gvLoo.SetFocusedRowCellValue("MaPK", maPK);
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (DialogBox.Question() == System.Windows.Forms.DialogResult.No) return;

                var obj = db.Loos.Single(p => p.MaLo == (int?)gvLoo.GetFocusedRowCellValue("MaLo"));
                db.Loos.DeleteOnSubmit(obj);
                db.SubmitChanges();

                gvLoo.DeleteSelectedRows();
            }
            catch { }
        }
    }
}