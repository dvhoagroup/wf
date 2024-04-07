using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEEREMA.Test
{
    public partial class CongNo_frm : DevExpress.XtraEditors.XtraForm
    {
        public int TuNgay = 0, DenNgay = 0, Thang = 0, Nam = 0;
        public DateTime TuNgay2 = DateTime.Now, DenNgay2 = DateTime.Now;
        public CongNo_frm()
        {
            InitializeComponent();
        }

        private void CongNo_frm_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = it.CommonCls.Table(string.Format("hdmbTest {0}, {1}, {2}, {3}", TuNgay, DenNgay, Thang, Nam));
        }

        private void CongNo_frm_Shown(object sender, EventArgs e)
        {
            for (int i = 1; i < 32; i++)
            {
                gridView1.Columns[10 + i].Caption = string.Format("{0}/{1}/{2}", i < 10 ? "0" + i.ToString() : i.ToString(), Thang < 10 ? "0" + Thang.ToString() : Thang.ToString(), Nam);
            }

            for (int i = 1; i < 32; i++)
            {
                if (i >= TuNgay && i <= DenNgay)
                    gridView1.Columns[10 + i].Visible = true;
                else
                    gridView1.Columns[10 + i].Visible = false;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog fbd = new SaveFileDialog();
            fbd.FileName = string.Format("BaoCaoCongNoTheoNgay.xlsx");
            fbd.Filter = "File excel(.xlsx)|*.xlsx";
            fbd.Title = "Lưu báo cáo công nợ theo ngày";
            
            if (fbd.ShowDialog() == DialogResult.OK)
                gridView1.ExportToXlsx(fbd.FileName);
        }
    }
}