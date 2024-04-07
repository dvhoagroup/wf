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
    public partial class Tang_frm : DevExpress.XtraEditors.XtraForm
    {
        public int BlockID = 0;
        public Tang_frm()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        void LoadData()
        {
            it.TangNhaCls o = new it.TangNhaCls();
            gridControl1.DataSource = o.Select(BlockID);
        }

        private void btnThemNhieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ThemNhieuTang_frm frm = new ThemNhieuTang_frm();
            frm.BlockID = BlockID;
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ThemTang_frm frm = new ThemTang_frm();
            frm.BlockID = BlockID;
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        void Edit()
        {
            if (gridView1.GetFocusedRowCellValue(colMaTang) != null)
            {
                int row = gridView1.FocusedRowHandle;
                ThemTang_frm frm = new ThemTang_frm();
                frm.MaTang = int.Parse(gridView1.GetFocusedRowCellValue(colMaTang).ToString());
                frm.BlockID = BlockID;
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
                gridView1.FocusedRowHandle = row;
            }
            else
                DialogBox.Infomation("Vui lòng chọn Sàn/Tầng cần sửa. Xin cảm ơn");
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaTang) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa Sàn/Tầng: <" + gridView1.GetFocusedRowCellValue(colTen).ToString() + "> ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    try
                    {
                        it.TangNhaCls o = new it.TangNhaCls();
                        o.MaTangNha = int.Parse(gridView1.GetFocusedRowCellValue(colMaTang).ToString());
                        o.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì Sàn/Tầng: <" + gridView1.GetFocusedRowCellValue(colTen).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn Sàn/Tầng cần xóa. Xin cảm ơn");
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void Tang_frm_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}