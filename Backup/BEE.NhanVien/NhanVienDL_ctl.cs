using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BEEREMA;

namespace BEE.NhanVien
{
    public partial class NhanVienDL_ctl : UserControl
    {
        public NhanVienDL_ctl()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            it.NhanVienDaiLyCls o = new it.NhanVienDaiLyCls();
            gridControl1.DataSource = o.Select();

            lookUpChucVu.DataSource = o.ChucVu.Select();
            lookUpNhomKD.DataSource = o.NhomKD.Select();
            lookUpPhongBan.DataSource = o.PhongBan.Select();
        }

        private void Huong_ctl_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnNap_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NhanVienDL_frm frm = new NhanVienDL_frm();
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNV) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa nhân viên: <" + gridView1.GetFocusedRowCellValue(colTenNV).ToString() + "> ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    try
                    {
                        it.NhanVienDaiLyCls o = new it.NhanVienDaiLyCls();
                        o.MaNV = byte.Parse(gridView1.GetFocusedRowCellValue(colMaNV).ToString());
                        o.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì nhân viên: <" + gridView1.GetFocusedRowCellValue(colTenNV).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn nhân viên cần xóa. Xin cảm ơn");
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        void Edit()
        {
            if (gridView1.GetFocusedRowCellValue(colMaNV) != null)
            {
                NhanVienDL_frm frm = new NhanVienDL_frm();
                frm.KeyID = int.Parse(gridView1.GetFocusedRowCellValue(colMaNV).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn nhân viên cần sửa. Xin cảm ơn");
        }

        private void btnKhoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNV) != null)
            {
                it.NhanVienDaiLyCls o = new it.NhanVienDaiLyCls();
                o.MaNV = int.Parse(gridView1.GetFocusedRowCellValue(colMaNV).ToString());
                o.Lock = true;
                o.LockStaff();
            }
            else
                DialogBox.Infomation("Vui lòng chọn nhân viên cần khóa. Xin cảm ơn");
        }

        private void btnMoKhoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNV) != null)
            {
                it.NhanVienDaiLyCls o = new it.NhanVienDaiLyCls();
                o.MaNV = int.Parse(gridView1.GetFocusedRowCellValue(colMaNV).ToString());
                o.Lock = false;
                o.LockStaff();
            }
            else
                DialogBox.Infomation("Vui lòng chọn nhân viên cần mở khóa. Xin cảm ơn");
        }
    }
}
