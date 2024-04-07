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
    public partial class LichThanhToan_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaDA =0;
        public bool IsUpdate = false;
        public string DuAn = "";
        public LichThanhToan_frm()
        {
            InitializeComponent();
        }

        private void LichThanhToan_frm_Load(object sender, EventArgs e)
        {
            lblDuAn.Text = "Dự án: " + DuAn;
            it.DuAn_LichThanhToanCls o = new it.DuAn_LichThanhToanCls();
            o.MaDA = MaDA;
            gridControl2.DataSource = o.SelectBY();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            it.DuAn_LichThanhToanCls o;
            for (int i = 0; i < gridView2.RowCount - 1; i++)
            {
                o = new it.DuAn_LichThanhToanCls();
                o.MaDA = MaDA;
                o.DotTT = byte.Parse(gridView2.GetRowCellValue(i, colDotTT).ToString());
                o.DienGiai = gridView2.GetRowCellValue(i, colDienGiai).ToString();
                o.TyLeTT = byte.Parse(gridView2.GetRowCellValue(i, colTyLe).ToString());
                if (gridView2.GetRowCellValue(i, colNgayTT).ToString() != "")
                    o.NgayTT = DateTime.Parse(gridView2.GetRowCellValue(i, colNgayTT).ToString());
                o.SoThang = int.Parse(gridView2.GetRowCellValue(i, colSoThang).ToString());
                o.SoNgay = int.Parse(gridView2.GetRowCellValue(i, colSoNgay).ToString());
                o.Update();
            }
            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã được lưu.");
            this.Close();
        }
    }
}