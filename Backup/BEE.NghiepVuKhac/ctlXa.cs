using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.NghiepVuKhac
{
    public partial class ctlXa : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();

        public ctlXa()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var maTinh = (int?)itemTinh.EditValue;
                gcXa.DataSource = (from x in db.Xas
                                   join h in db.Huyens on x.MaHuyen equals h.MaHuyen
                                   orderby h.TenHuyen
                                   where h.MaTinh == maTinh
                                   select new
                                   {
                                       x.MaXa,
                                       x.TenXa,
                                       x.MaHuyen,
                                       h.TenHuyen,
                                       h.MaTinh
                                   }).ToList();
            }
            catch
            {
                gcXa.DataSource = null;
            }
            finally
            {
                wait.Close();
            }
        }

        private void ctlXa_Load(object sender, EventArgs e)
        {
            lkTinh.DataSource = db.Tinhs.Select(p => new { p.MaTinh, p.TenTinh }).ToList();
            lookUpHuyen2.DataSource = db.Huyens.OrderBy(p => p.TenHuyen).Select(p => new { p.MaHuyen, p.TenHuyen }).ToList();
            LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maXa = (int?)gvXa.GetFocusedRowCellValue("MaXa");
            if (maXa == null)
            {
                DialogBox.Error("Vui lòng chọn dòng cần sửa");
                return;
            }

            using (var frm = new frmXa())
            {
                frm.MaTinh = (int?)itemTinh.EditValue;
                frm.MaHuyen = (int?)gvXa.GetFocusedRowCellValue("MaHuyen");
                frm.MaXa = maXa;
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmXa();
            frm.MaTinh = (int)itemTinh.EditValue;
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maXa = (int?)gvXa.GetFocusedRowCellValue("MaXa");
            if (maXa == null)
            {
                DialogBox.Error("Vui lòng chọn dòng cần xóa");
                return;
            }

            if (DialogBox.Question() == DialogResult.No) return;

            try
            {
                var obj = db.Xas.Single(p => p.MaXa == maXa);
                db.Xas.DeleteOnSubmit(obj);
                db.SubmitChanges();
                gvXa.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Error("Tên xã đã sử dụng, không thể xóa!");
            }
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gvXa_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void itemTinh_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
