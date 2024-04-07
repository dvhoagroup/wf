using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.DuAn
{
    public partial class BieuMau_frm : DevExpress.XtraEditors.XtraForm
    {
        public string TenDA = "";
        public int MaDA = 0;
        public BieuMau_frm()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            it.hdmbBieuMauCls o = new it.hdmbBieuMauCls();
            gridControl1.DataSource = o.Select();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddTemplate_frm frm = new AddTemplate_frm();
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void BieuMau_frm_Load(object sender, EventArgs e)
        {
            this.Text = "Biểu mẫu - " + TenDA;
            lblDuAn.Text = "Dự án: " + TenDA;
            LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaBM) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa biểu mẫu: <" + gridView1.GetFocusedRowCellValue(colTenBM).ToString() + "> ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    try
                    {
                        it.hdmbBieuMauCls o = new it.hdmbBieuMauCls();
                        o.MaBM = byte.Parse(gridView1.GetFocusedRowCellValue(colMaBM).ToString());
                        o.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì biểu mẫu: <" + gridView1.GetFocusedRowCellValue(colTenBM).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn biểu mẫu cần xóa. Xin cảm ơn");
        }

        void Edit()
        {
            if (gridView1.GetFocusedRowCellValue(colMaBM) != null)
            {
                AddTemplate_frm frm = new AddTemplate_frm();
                frm.KeyID = byte.Parse(gridView1.GetFocusedRowCellValue(colMaBM).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn biểu mẫu cần sửa. Xin cảm ơn");
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaBM) != null)
            {
                BEE.NghiepVuKhac.Editor_frm frm = new BEE.NghiepVuKhac.Editor_frm();
                frm.MaDA = MaDA;
                frm.MaBM = byte.Parse(gridView1.GetFocusedRowCellValue(colMaBM).ToString());
                frm.LoaiHD = 1;
                frm.ShowDialog();
            }
            else
                DialogBox.Infomation("Vui lòng chọn biểu mẫu cần cập nhật nội dung. Xin cảm ơn");
        }
    }
}