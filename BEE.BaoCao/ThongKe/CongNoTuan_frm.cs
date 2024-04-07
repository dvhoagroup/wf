using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEE.BaoCao.ThongKe
{
    public partial class CongNoTuan_frm : DevExpress.XtraEditors.XtraForm
    {
        public int TuNgay = 0, DenNgay = 0, Thang = 0, Nam = 0, AmountWeek = 0;
        public string QueryString = "";
        public DateTime TuNgay2 = DateTime.Now, DenNgay2 = DateTime.Now;

        public CongNoTuan_frm()
        {
            InitializeComponent();
        }

        private void CongNo_frm_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = it.CommonCls.Table("BDS_CongNoTuan " + QueryString);
        }

        private void CongNo_frm_Shown(object sender, EventArgs e)
        {
            for (int i = 1; i <= 53; i++)
            {
                gridView1.Columns[12 + i].Caption = string.Format("Tuần {0}", i);
            }

            for (int i = 1; i <= 53; i++)
            {
                if (i <= AmountWeek)
                    gridView1.Columns[12 + i].Visible = true;
                else
                    gridView1.Columns[12 + i].Visible = false;
            }

            //for (int i = 0; i < gridView1.RowCount; i++)
            //{
            //    if (double.Parse(gridView1.GetRowCellValue(i, colTongCongNo).ToString()) != 0)
            //    {
            //        gridView1.SetRowCellValue(i, colTongThueVAT, double.Parse(gridView1.GetRowCellValue(i, colTongCongNo).ToString()) * 10 / 100);
            //        gridView1.SetRowCellValue(i, colTienMuaCH, double.Parse(gridView1.GetRowCellValue(i, colTongCongNo).ToString()) - (double.Parse(gridView1.GetRowCellValue(i, colTongCongNo).ToString()) * 10 / 100));
            //    }
            //}
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog fbd = new SaveFileDialog();
            fbd.FileName = "BaoCaoCongNoTheoTuan.xlsx";
            fbd.Filter = "File excel(.xlsx)|*.xlsx";
            fbd.Title = "Lưu báo cáo công nợ theo tuần";

            if (fbd.ShowDialog() == DialogResult.OK)
                gridView1.ExportToXlsx(fbd.FileName);
        }
    }
}