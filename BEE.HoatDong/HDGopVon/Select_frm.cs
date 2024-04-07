using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LandSoft.NghiepVu.HDGopVon
{
    public partial class Select_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaHDGV = 0, MaKH = 0;
        public string HoTenKH = "", MaBDS = "";
        public Select_frm()
        {
            InitializeComponent();
        }

        private void Select_frm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            it.hdGopVonCls o = new it.hdGopVonCls();
            gridControl1.DataSource = o.Select();

            lookUpTinhTrang.DataSource = o.TinhTrang.Select();
            lookUpNhanVienKT.DataSource = o.NhanVien.SelectShow();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        void Select1()
        {
            if (gridView1.GetFocusedRowCellValue(colMaHDGV) != null)
            {
                MaHDGV = int.Parse(gridView1.GetFocusedRowCellValue(colMaHDGV).ToString());
                MaKH = int.Parse(gridView1.GetFocusedRowCellValue(colMaKH).ToString());
                HoTenKH = gridView1.GetFocusedRowCellValue(colHoTenKH).ToString();
                MaBDS = gridView1.GetFocusedRowCellValue(colMaBDS).ToString();
                this.Close();
            }
            else
                DialogBox.Infomation("Vui lòng chọn hợp đồng góp vốn. Xin cảm ơn!");
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Select1();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Select1();
        }
    }
}