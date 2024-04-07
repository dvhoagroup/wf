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

namespace BEE.HoatDong.MGL.BieuMau
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db;

        public ctlManager()
        {
            InitializeComponent();
        }

        void BieuMau_Load()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                db = new MasterDataContext();
                gcBieuMau.DataSource = db.mglBieuMaus.AsEnumerable()
                    .OrderByDescending(p => p.NgayCN)
                    .Select((p, index) => new { p.MaBM, p.TenBM, p.DienGiai, p.Khoa, p.NhanVien.HoTen, p.NgayCN })
                    .ToList();
                grvBieuMau.FocusedRowHandle = -1;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            wait.Close();
        }

        void BieuMau_Add()
        {
            var frm = new frmEdit();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                BieuMau_Load();
            }
        }

        void BieuMau_Edit()
        {
            var MaBM = (int?)grvBieuMau.GetFocusedRowCellValue("MaBM");
            if (MaBM == null)
            {
                DialogBox.Error("Vui lòng chọn biểu mẫu");
                return;
            }
            var frm = new frmEdit();
            frm.MaBM = MaBM;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                BieuMau_Load();
            }
        }

        void BieuMau_Delete()
        {
            try
            {
                var indexs = grvBieuMau.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn biểu mẫu");
                    return;
                }

                if (DialogBox.Question() == DialogResult.No) return;

                foreach (var i in indexs)
                {
                    var objBM = db.mglBieuMaus.Single(p => p.MaBM == (int)grvBieuMau.GetRowCellValue(i, "MaBM"));
                    db.mglBieuMaus.DeleteOnSubmit(objBM);
                }

                db.SubmitChanges();

                BieuMau_Load();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            BieuMau_Load();
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BieuMau_Add();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BieuMau_Edit();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BieuMau_Delete();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BieuMau_Load();
        }

        private void grvBieuMau_DoubleClick(object sender, EventArgs e)
        {
            BieuMau_Edit();
        }

        private void grvBieuMau_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) 
                BieuMau_Delete();
        }

        private void itemPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var MaBM = (int?)grvBieuMau.GetFocusedRowCellValue("MaBM");
                if (MaBM == null)
                {
                    DialogBox.Error("Vui lòng chọn biểu mẫu");
                    return;
                }

                var objBM = db.mglBieuMaus.Single(p => p.MaBM == MaBM);
               // var frm = new BEE.DuAn.BieuMau.frmPreview();
               // frm.RtfText = objBM.NoiDung;
               // frm.ShowDialog();
            }
            catch(Exception ex){
                DialogBox.Error(ex.Message);
            }
        }
    }
}
