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
    public partial class FormFeature_frm : DevExpress.XtraEditors.XtraForm
    {
        public int FormID = 0;
        public string FormName = "";
        public bool IsUpdate = false;
        public FormFeature_frm()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this, barManager1);
        }

        void LoadData()
        {
            it.FormFeaturesCls o = new it.FormFeaturesCls();
            o.FormID = FormID;
            gridControl1.DataSource = o.SelectBy();
        }

        private void Forms_frm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddFeature_frm frm = new AddFeature_frm();
            frm.FormID = FormID;
            frm.FormName = FormName;
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
                AddFeature_frm frm = new AddFeature_frm();
                frm.FormID = int.Parse(gridView1.GetFocusedRowCellValue(colFormID).ToString());
                frm.FeatureID = int.Parse(gridView1.GetFocusedRowCellValue(colFeatureID).ToString());
                frm.FormName = FormName;
                frm.ShowDialog();
                if (frm.IsUpdate)
                {
                    LoadData();
                    IsUpdate = true;
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn form cần sửa. Xin cảm ơn");
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colFormID) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa tính năng : <" + gridView1.GetFocusedRowCellValue(colFormName).ToString() + "> ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    try
                    {
                        it.FormFeaturesCls o = new it.FormFeaturesCls();
                        o.FormID = int.Parse(gridView1.GetFocusedRowCellValue(colFormID).ToString());
                        o.FeatureID = byte.Parse(gridView1.GetFocusedRowCellValue(colFeatureID).ToString());
                        o.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì tính năng: <" + gridView1.GetFocusedRowCellValue(colFormName).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn tính năng cần xóa. Xin cảm ơn");
        }

        private void btnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IsUpdate = true;
            this.Close();
        }
    }
}