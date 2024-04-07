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
    public partial class Staff_frm : DevExpress.XtraEditors.XtraForm
    {
        public int PerID = 0;
        public bool IsUpdate = false, IsAgent = false;
        public Staff_frm()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Staff_frm_Load(object sender, EventArgs e)
        {
            if (IsAgent)
            {
                it.NhanVienDaiLyCls objNVDL = new it.NhanVienDaiLyCls();
                gridControl2.DataSource = objNVDL.SelectNotPer();
                lookUpChucVu.DataSource = objNVDL.ChucVu.Select();
                lookUpNhomKD.DataSource = objNVDL.Select();
                lookUpPhongBan.DataSource = objNVDL.PhongBan.Select();
            }
            else
            {
                it.NhanVienCls o = new it.NhanVienCls();
                gridControl2.DataSource = o.SelectNotPer();
                lookUpChucVu.DataSource = o.ChucVu.Select();
                lookUpNhomKD.DataSource = o.NKD.Select();
                lookUpPhongBan.DataSource = o.PhongBan.Select();
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (IsAgent)
                SaveStaffAgent();
            else
                SaveStaff();
            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }

        void SaveStaff()
        {
            int[] row = gridView2.GetSelectedRows();
            it.NhanVienCls o;
            foreach (int i in row)
            {
                o = new it.NhanVienCls();
                o.MaNV = int.Parse(gridView2.GetRowCellValue(i, colMaNV).ToString());
                o.PerID = PerID;
                o.UpdatePer();
            }
        }

        void SaveStaffAgent()
        {
            int[] row = gridView2.GetSelectedRows();
            it.NhanVienDaiLyCls o;
            foreach (int i in row)
            {
                o = new it.NhanVienDaiLyCls();
                o.MaNV = int.Parse(gridView2.GetRowCellValue(i, colMaNV).ToString());
                o.Per.PerID = PerID;
                o.UpdatePer();
            }
        }
    }
}