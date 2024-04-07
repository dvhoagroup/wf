using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BEEREMA;

namespace BEE.HoatDong.PhanQuyen
{
    public partial class Permission_ctl : DevExpress.XtraEditors.XtraUserControl
    {
        int PerID = 0;
        bool IsAgent = false;
        public Permission_ctl()
        {
            InitializeComponent(); 
            
            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
        }

        void LoadData()
        {
            it.PermissionsCls o = new it.PermissionsCls();
            gridControl1.DataSource = o.Select();
        }

        void LoadModules()
        {
            it.ModulesCls o = new it.ModulesCls();
            gridControl3.DataSource = o.SelectByPerID(PerID);
            gridView3.ExpandAllGroups();
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
            Permission_frm frm = new Permission_frm();
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHuong) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa nhóm người dùng: <" + gridView1.GetFocusedRowCellValue(colTenHuong).ToString() + "> ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    try
                    {
                        it.PermissionsCls o = new it.PermissionsCls();
                        o.PerID = int.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString());
                        o.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì nhóm người dùng: <" + gridView1.GetFocusedRowCellValue(colTenHuong).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn nhóm người dùng cần xóa. Xin cảm ơn");
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
            if (gridView1.GetFocusedRowCellValue(colMaHuong) != null)
            {
                Permission_frm frm = new Permission_frm();
                frm.KeyID = int.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn nhóm người dùng cần sửa. Xin cảm ơn");
        }

        void LoadStaff()
        {
            if (IsAgent)
            {
                it.NhanVienDaiLyCls o = new it.NhanVienDaiLyCls();
                o.Per.PerID = PerID;
                gridControl2.DataSource = o.SelectByPer();
            }
            else
            {
                it.NhanVienCls o = new it.NhanVienCls();
                o.PerID = PerID;
                gridControl2.DataSource = o.SelectByPer();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHuong) != null)
            {
                PerID = int.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString());
                IsAgent = bool.Parse(gridView1.GetFocusedRowCellValue(colIsAgent).ToString());
            }
            else
                PerID = 0;
            LoadStaff();
            LoadModules();
        }

        private void btnThemNguoiDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHuong) != null)
            {
                Staff_frm frm = new Staff_frm();
                frm.PerID = int.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString());
                frm.IsAgent = bool.Parse(gridView1.GetFocusedRowCellValue(colIsAgent).ToString()) ? true : false;
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadStaff();
            }
            else
                DialogBox.Infomation("Vui lòng chọn nhóm người dùng. Xin cảm ơn");
        }

        private void btnThietLap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHuong) != null)
            {
                Power_frm frm = new Power_frm();
                frm.PerID = int.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString());
                
                frm.ShowDialog();                
            }
            else
                DialogBox.Infomation("Vui lòng chọn nhóm người dùng. Xin cảm ơn");            
        }

        private void btnDeleteUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView2.GetFocusedRowCellValue(colMaNV) != null)
            {
                if (IsAgent)
                {
                    it.NhanVienDaiLyCls o = new it.NhanVienDaiLyCls();
                    o.MaNV = int.Parse(gridView2.GetFocusedRowCellValue(colMaNV).ToString());
                    o.UpdatePerID();
                }
                else
                {
                    it.NhanVienCls o = new it.NhanVienCls();
                    o.MaNV = int.Parse(gridView2.GetFocusedRowCellValue(colMaNV).ToString());
                    o.UpdatePerID();
                }
                LoadStaff();
                LoadModules();
            }
            else
                DialogBox.Infomation("Vui lòng chọn người dùng muốn xóa. Xin cảm ơn"); 
        }
    }
}
