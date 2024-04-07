using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;

namespace BEE.HoatDong.Maketing
{
    public partial class Chart_frm : DevExpress.XtraEditors.XtraForm
    {
        public string QueryString = "", Title = "", TieuDeForm = "";
        ChartControl pie3DChart = new ChartControl();
        public Chart_frm()
        {
            InitializeComponent();
        }

        private void Chart_frm_Load(object sender, EventArgs e)
        {
            Area();
        }

        void Area()
        {
            DataTable tblChart = it.CommonCls.Table(QueryString);
            // Add a bar series to it.
            Series series1 = new Series(string.Format("BIỂU ĐỒ PHÂN TÍCH THỐNG KÊ THEO {0}", Title), ViewType.Pie3D);

            // Add points to the series.
            foreach (DataRow r in tblChart.Rows)
                series1.Points.Add(new SeriesPoint(r["TenTinh"].ToString(), double.Parse(r["SoLuong"].ToString())));

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
            chartTitle1.Text = string.Format("BIỂU ĐỒ PHÂN TÍCH THỐNG KÊ THEO {0}", Title);
            chartTitle1.WordWrap = true;
            pie3DChart.Titles.Add(chartTitle1);
            pie3DChart.Legend.Visible = true;

            // Add the chart to the form.
            pie3DChart.Dock = DockStyle.Fill;
            panelControl1.Controls.Add(pie3DChart);
            SetNumericOptions(pie3DChart.Series[0], NumericFormat.Percent, 0);

            this.Text = string.Format("BIỂU ĐỒ PHÂN TÍCH THỐNG KÊ THEO {0}", Title);
        }

        void Project()
        {
            DataTable tblChart = it.CommonCls.Table(QueryString);
            // Add a bar series to it.
            Series series1 = new Series(string.Format("BIỂU ĐỒ DOANH SỐ THEO {0}", Title), ViewType.Pie3D);

            // Add points to the series.
            foreach (DataRow r in tblChart.Rows)
                series1.Points.Add(new SeriesPoint(r["TenDA"].ToString(), double.Parse(r["SoLuong"].ToString())));

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
            chartTitle1.Text = string.Format("BIỂU ĐỒ DOANH SỐ THEO {0}", Title);
            chartTitle1.WordWrap = true;
            pie3DChart.Titles.Add(chartTitle1);
            pie3DChart.Legend.Visible = false;

            // Add the chart to the form.
            pie3DChart.Dock = DockStyle.Fill;
            panelControl1.Controls.Add(pie3DChart);
            SetNumericOptions(pie3DChart.Series[0], NumericFormat.Percent, 0);

            this.Text = string.Format("BIỂU ĐỒ DOANH SỐ THEO {0}", Title);
        }

        protected void SetNumericOptions(Series series, NumericFormat format, int precision)
        {
            series.PointOptions.ValueNumericOptions.Format = format;
            series.PointOptions.ValueNumericOptions.Precision = precision;
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
                pie3DChart.ExportToXlsx(sfd.FileName);
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            pie3DChart.Print();
        }
    }
}