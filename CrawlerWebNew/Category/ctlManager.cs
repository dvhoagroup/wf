using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.Linq;
using BEE.ThuVien;
using BEEREMA;

namespace CrawlerWebNew.Category
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db;
        byte? MaTinh { get; set; }
        int? WebID { get; set; }
        public ctlManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            this.Load += new EventHandler(ctlManager_Load);
        }

        void ctlManager_Load(object sender, EventArgs e)
        {
            lookWebSite.DataSource = db.crlWebsites;
            lookNhomTin.DataSource = db.crlNewsGroups;
            lookTinh.DataSource = db.Tinhs;
            lookChuyenMuc.DataSource = db.crlHangMucTins;
            LoadData();
        }

        void LoadData()
        {
            db = new MasterDataContext();
            var wait = DialogBox.WaitingForm();
            try
            {
                lookParentCat.DataSource = db.crlCategories.Where(p => p.IsMainMenu == true && p.WebID == WebID);
                itemSave.DataSource = db.crlCategories.Where(p => p.WebID == WebID && (p.MaTinh == MaTinh || p.MaTinh == 1)).OrderBy(p => p.STT);
            }
            catch { }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvCategory.FocusedRowHandle < 0)
                return;
            if (DialogBox.Question("Bạn có chắc muốn xóa Menu này không?") == DialogResult.No) return;
            gvCategory.DeleteSelectedRows();
            try
            {
                db.SubmitChanges();
                DialogBox.Infomation("Dữ liệu đã xóa thành công!");
            }
            catch (Exception ex)
            {
                DialogBox.Error("Dữ liệu không thể xóa: " + ex.Message);
            }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                db.SubmitChanges();
                    DialogBox.Infomation("Dữ liệu đã lưu thành công!");
            }
            catch(Exception ex)
            {
                DialogBox.Infomation("Đã có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                wait.Close();
            }
        }

        private void itemWebSite_EditValueChanged(object sender, EventArgs e)
        {
            WebID = itemWebSite.EditValue == null ? -1 : Convert.ToInt32(itemWebSite.EditValue);
            LoadData();
        }

        private void gvCategory_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (itemWebSite.EditValue == null)
                return;
            gvCategory.SetFocusedRowCellValue("WebID", WebID);
            gvCategory.SetFocusedRowCellValue("MaTinh", MaTinh);
        }

        private void itemTinh_EditValueChanged(object sender, EventArgs e)
        {
            if (itemTinh.EditValue == null)
                return;
            lookHuyen.DataSource = db.Huyens.Where(p => p.MaTinh == (byte?)itemTinh.EditValue);
            MaTinh = (byte?)itemTinh.EditValue;
            LoadData();
        }
    }
}
