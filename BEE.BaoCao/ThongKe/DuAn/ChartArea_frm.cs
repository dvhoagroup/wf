using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;

namespace BEE.BaoCao.ThongKe.DuAn
{
    public partial class ChartArea_frm : DevExpress.XtraEditors.XtraForm
    {
        public string QueryString = "", TenTinh = "";
        public int Year = 0, ToMonth = 0, FromMonth = 0;
        ChartControl pie3DChart = new ChartControl();
        public ChartArea_frm()
        {
            InitializeComponent();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            pie3DChart.Print();
        }

        private void Chart_frm_Shown(object sender, EventArgs e)
        {
            Pie3D();
        }

        void Pie3D()
        {
            DataTable tblChart = it.CommonCls.Table("DuAn_chart_KhuVuc " + QueryString);
            // Add a bar series to it.
            Series series1 = new Series(string.Format("BIỂU ĐỒ DOANH SỐ THEO KHU VỰC {0} THÁNG {1} - {2}/{3}", TenTinh, ToMonth, FromMonth, Year), ViewType.Pie3D);

            // Add points to the series.
            foreach(DataRow r in tblChart.Rows)
                series1.Points.Add(new SeriesPoint(r["TenHuyen"].ToString(), double.Parse(r["SoLuong"].ToString())));

            // Add both series to the chart.
            pie3DChart.Series.AddRange(new Series[] { series1 });

            // Access labels of the first series.
            ((Pie3DSeriesLabel)series1.Label).Visible = true;
            ((Pie3DSeriesLabel)series1.Label).ResolveOverlappingMode =
                ResolveOverlappingMode.Default;

            // Access the series options.
            series1.PointOptions.PointView = PointView.ArgumentAndValues;

            // Customize the view-type-specific properties of the series.
            Pie3DSeriesView pie3DView = (Pie3DSeriesView)series1.View;
            pie3DView.Depth = 10;
            pie3DView.SizeAsPercentage = 100;
            pie3DView.ExplodeMode = PieExplodeMode.All;

            // Add a title to the chart and hide the legend.
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = string.Format("BIỂU ĐỒ DOANH SỐ THEO KHU VỰC {0} THÁNG {1} - {2}/{3}", TenTinh, ToMonth, FromMonth, Year);
            chartTitle1.WordWrap = true;
            pie3DChart.Titles.Add(chartTitle1);
            pie3DChart.Legend.Visible = true;
            pie3DChart.Dock = DockStyle.Fill;

            // Add the chart to the form.
            panelControl1.Controls.Add(pie3DChart);
            SetNumericOptions(pie3DChart.Series[0], NumericFormat.Percent, 0);

            this.Text = string.Format("BIỂU ĐỒ DOANH SỐ THEO DỰ ÁN THÁNG {0} - {1}/{2}", ToMonth, FromMonth, Year);
        }

        protected void SetNumericOptions(Series series, NumericFormat format, int precision)
        {
            series.PointOptions.ValueNumericOptions.Format = format;
            series.PointOptions.ValueNumericOptions.Precision = precision;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "File Excel(.xlsx)|*.xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
                pie3DChart.ExportToXlsx(sfd.FileName);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}