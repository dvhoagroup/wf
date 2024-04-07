using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.HoatDong.PhanQuyen
{
    public partial class Forms_frm : DevExpress.XtraEditors.XtraForm
    {
        public byte Module = 0;
        int FormID = 0;
        public Forms_frm()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            it.FormsCls o = new it.FormsCls();
            o.Modul.ModulID = Module;
            gridControl1.DataSource = o.SelectBy();
        }

        private void Forms_frm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddForm_frm frm = new AddForm_frm();
            frm.ModulID = Module;
            frm.ShowDialog();
            if(frm.IsUpdate)
                LoadData();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        void Edit()
        {
            if (gridView1.GetFocusedRowCellValue(colFormID) != null)
            {
                AddForm_frm frm = new AddForm_frm();
                frm.KeyID = int.Parse(gridView1.GetFocusedRowCellValue(colFormID).ToString());
                frm.ModulID = Module;
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn form cần sửa. Xin cảm ơn");
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colFormID) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa form: <" + gridView1.GetFocusedRowCellValue(colFormName).ToString() + "> ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    try
                    {
                        it.FormsCls o = new it.FormsCls();
                        o.FormID = int.Parse(gridView1.GetFocusedRowCellValue(colFormID).ToString());
                        o.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì form: <" + gridView1.GetFocusedRowCellValue(colFormName).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn form cần xóa. Xin cảm ơn");
        }

        private void btnTinhNang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colFormID) != null)
            {
                FormFeature_frm frm = new FormFeature_frm();
                frm.FormID = int.Parse(gridView1.GetFocusedRowCellValue(colFormID).ToString());
                frm.FormName = gridView1.GetFocusedRowCellValue(colFormName).ToString();
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadFeature();
            }
            else
                DialogBox.Infomation("Vui lòng chọn form cần sửa. Xin cảm ơn");
        }

        void LoadFeature()
        {
            it.FormFeaturesCls o = new it.FormFeaturesCls();
            o.FormID = FormID;
            gridControl2.DataSource = o.SelectBy();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colFormID) != null)
                FormID = int.Parse(gridView1.GetFocusedRowCellValue(colFormID).ToString());
            else
                FormID = 0;
            LoadFeature();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}