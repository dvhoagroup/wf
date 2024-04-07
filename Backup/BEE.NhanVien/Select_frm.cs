using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.NhanVien
{
    public partial class Select_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaNV = 0;
        public string HoTen = "";
        public Select_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNV) != null)
            {
                MaNV = int.Parse(gridView1.GetFocusedRowCellValue(colMaNV).ToString());
                HoTen = gridView1.GetFocusedRowCellValue(colTenNV).ToString();
                this.Close();
            }
            else
                DialogBox.Infomation("Vui lòng chọn nhân viên. Xin cảm ơn.");
        }

        void LoadData()
        {
            it.NhanVienCls o = new it.NhanVienCls();
            gridControl1.DataSource = o.Select();

            lookUpChucVu.DataSource = o.ChucVu.Select();
            lookUpNhomKD.DataSource = o.NKD.Select();
            lookUpPhongBan.DataSource = o.PhongBan.Select();
        }

        private void Select_frm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            btnDongY_Click(sender, e);
        }
    }
}