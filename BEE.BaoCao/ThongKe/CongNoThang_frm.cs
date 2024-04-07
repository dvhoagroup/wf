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
    public partial class CongNoThang_frm : DevExpress.XtraEditors.XtraForm
    {
        public int TuThang = 0, DenThang = 0, Thang = 0, Nam = 0;
        public string QueryString = "";
        public CongNoThang_frm()
        {
            InitializeComponent();
        }

        private void CongNo_frm_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = it.CommonCls.Table("BDS_CongNoThang " + QueryString);
        }

        private void CongNo_frm_Shown(object sender, EventArgs e)
        {
            for (int i = 1; i <= 12; i++)
            {
                gridView1.Columns[12 + i].Caption = string.Format("Tháng {0}/{1}", i, Nam);
            }

            for (int i = 1; i <= 12; i++)
            {
                if (i >= TuThang && i <= DenThang)
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
            fbd.FileName = string.Format("BaoCaoCongNoTheoThang{0}-{1}-{2}.xlsx", TuThang, DenThang, Nam);
            fbd.Filter = "File excel(.xlsx)|*.xlsx";
            fbd.Title = "Lưu báo cáo công nợ theo tháng";
            
            if (fbd.ShowDialog() == DialogResult.OK)
                gridView1.ExportToXlsx(fbd.FileName);
        }
    }
}