using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;

namespace BEE.BaoCao.ThongKe.BDS
{
    public partial class ChartByYear_frm : DevExpress.XtraEditors.XtraForm
    {
        public int Year = 0;
        public bool IsRevenue = false;
        public string QueryString = "";
        ChartControl lineChart = new ChartControl();
        public ChartByYear_frm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "File Excel(.xlsx)|*.xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
                lineChart.ExportToXlsx(sfd.FileName);
        }

        string SetText()
        {
            if (IsRevenue)
                return string.Format("BIỂU ĐỒ DOANH THU THEO NĂM {0}", Year);
            else
                return string.Format("BIỂU ĐỒ SỐ LÔ ĐÃ BÁN THEO NĂM {0}", Year);
        }

        void Bar()
        {
            this.Text = SetText();

            DataRow r = it.CommonCls.Table(QueryString).Rows[0];
            // Add a bar series to it.
            Series series1 = new Series(SetText(), ViewType.Bar);
            Series series2 = new Series(SetText(), ViewType.Line);

            // Add points to the series.
            for (int i = 1; i <= 12; i++)
            {
                series1.Points.Add(new SeriesPoint(string.Format("Tháng {0}", i), string.Format("{0:n0}", double.Parse(r["T" + i].ToString()))));
                series2.Points.Add(new SeriesPoint(string.Format("Tháng {0}", i), string.Format("{0:n0}", double.Parse(r["T" + i].ToString()))));
            }
            
            // Add both series to the chart.
            lineChart.Series.AddRange(new Series[] { series1, series2 });

            // Access labels of the first series.
            ((BarSeriesLabel)series1.Label).Visible = true;
            ((BarSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            ((PointSeriesLabel)series2.Label).Visible = false;
            ((PointSeriesLabel)series2.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            // Access the series options.
            series1.PointOptions.PointView = PointView.Values;
            //series1.PointOptions.PointView = PointView.Undefined;

            // Customize the view-type-specific properties of the series.
            BarSeriesView myView = (BarSeriesView)series1.View;
            myView.Transparency = 50;

            // Add a title to the chart and hide the legend.
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = SetText();
            lineChart.Titles.Add(chartTitle1);
            lineChart.Legend.Visible = false;
            lineChart.Dock = DockStyle.Fill;

            // Add the chart to the form.
            lineChart.Dock = DockStyle.Fill;
            panelControl1.Controls.Add(lineChart);
            SetNumericOptions(lineChart.Series[0], NumericFormat.Number, 0);
        }

        protected void SetNumericOptions(Series series, NumericFormat format, int precision)
        {
            series.PointOptions.ValueNumericOptions.Format = format;
            series.PointOptions.ValueNumericOptions.Precision = precision;
        }

        private void ChartByMonth_frm_Load(object sender, EventArgs e)
        {
            Bar();
        }
    }
}