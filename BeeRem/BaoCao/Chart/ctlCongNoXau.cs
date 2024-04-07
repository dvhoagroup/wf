using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using BEE.ThuVien;

namespace BEEREMA.BaoCao.Chart
{
    public partial class ctlCongNoXau : DevExpress.XtraEditors.XtraUserControl
    {
        ChartControl barChart3D;
        public ctlCongNoXau()
        {
            InitializeComponent();
        }

        void Spline()
        {
            // Create a new chart.
            barChart3D = new ChartControl();

            // Add a bar series to it.
            Series series1 = new Series("Series 1", ViewType.Line);
            series1.LegendText = "Tuần trước";
            Series series2 = new Series("Series 2", ViewType.Line);
            series2.LegendText = "Biến động giảm";
            Series series3 = new Series("Series 3", ViewType.Line);
            series3.LegendText = "Nợ xấu hiện tại";

            // Add points to the series.
            int j = 4;
            for (int i = 1; i < 5; i++)
            {
                Random o = new Random();
                series1.Points.Add(new SeriesPoint(string.Format("Nhóm {0}", i), j * o.Next(5, 18)));
                Random o2 = new Random();
                series2.Points.Add(new SeriesPoint(string.Format("Nhóm {0}", i), j * o2.Next(5, 10)));
                Random o3 = new Random();
                series3.Points.Add(new SeriesPoint(string.Format("Nhóm {0}", i), j * o3.Next(1, 5)));
                j--;
            }

            // Add both series to the chart.
            barChart3D.Series.AddRange(new Series[] { series1, series2, series3 });

            // Access labels of the first series.
            ((PointSeriesLabel)series1.Label).Visible = true;
            ((PointSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            ((PointSeriesLabel)series2.Label).Visible = true;
            ((PointSeriesLabel)series2.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            ((PointSeriesLabel)series3.Label).Visible = true;
            ((PointSeriesLabel)series3.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            // Access the series options.
            series1.PointOptions.PointView = PointView.Values;
            series2.PointOptions.PointView = PointView.Values;
            series3.PointOptions.PointView = PointView.Values;

            // Customize the view-type-specific properties of the series.
            LineSeriesView myView = (LineSeriesView)series1.View;
            myView.LineMarkerOptions.Kind = MarkerKind.Diamond;

            LineSeriesView myView2 = (LineSeriesView)series2.View;
            myView2.LineMarkerOptions.Kind = MarkerKind.Square;

            LineSeriesView myView3 = (LineSeriesView)series3.View;
            myView3.LineMarkerOptions.Kind = MarkerKind.Triangle;

            // Add a title to the chart and hide the legend.
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "Biểu cập nhật tình trạng nợ xấu";
            chartTitle1.Font = new Font("Calibri", 16);
            barChart3D.Titles.Add(chartTitle1);
            //Show lable
            barChart3D.Legend.Visible = true;
            barChart3D.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            barChart3D.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
            barChart3D.Legend.MaxHorizontalPercentage = 30;
            barChart3D.Legend.Direction = LegendDirection.LeftToRight;
            barChart3D.Legend.Visible = true;
            barChart3D.Legend.MarkerSize = new System.Drawing.Size(15, 15);
            barChart3D.Dock = DockStyle.Fill;

            // Add the chart to the form.
            barChart3D.Dock = DockStyle.Fill;
            this.splitContainerControl1.Panel1.Controls.Clear();
            this.splitContainerControl1.Panel1.Controls.Add(barChart3D);
        }

        void Pie3D()
        {
            try
            {
                // Create a new chart.           
                barChart3D = new ChartControl();

                // Add a bar series to it.
                Series series1 = new Series("DIP Vietnam", ViewType.Pie3D);

                // Add points to the series.
                //using (var db = new MasterDataContext())
                //{
                //    try
                //    {
                //        series1.Points.Add(new SeriesPoint("Kiến Á", db.funcCountSoldByProject((int)itemDuAn.EditValue)));
                //        series1.Points.Add(new SeriesPoint("Đại lý", db.funcCountAgentSoldByProject((int)itemDuAn.EditValue)));
                //    }
                //    catch { }
                //}
                series1.Points.Add(new SeriesPoint("Nhóm 1", (double)44.2));
                series1.Points.Add(new SeriesPoint("Nhóm 2", (double)18.6));
                series1.Points.Add(new SeriesPoint("Nhóm 3", (double)20.9));
                series1.Points.Add(new SeriesPoint("Nhóm 4", (double)16.3));

                // Add both series to the chart.
                barChart3D.Series.AddRange(new Series[] { series1 });

                // Access labels of the first series.
                ((Pie3DSeriesLabel)series1.Label).Visible = true;
                ((Pie3DSeriesLabel)series1.Label).ResolveOverlappingMode =
                    ResolveOverlappingMode.Default;

                // Access the series options.
                series1.LegendPointOptions.PointView = PointView.Argument;
                series1.PointOptions.PointView = PointView.Values;

                // Customize the view-type-specific properties of the series.
                Pie3DSeriesView myView = (Pie3DSeriesView)series1.View;
                myView.Depth = 8;
                myView.SizeAsPercentage = 100;
                myView.ExplodeMode = PieExplodeMode.All;

                // Add a title to the chart and hide the legend.
                ChartTitle chartTitle1 = new ChartTitle();
                chartTitle1.Text = "Tỷ lệ nhóm công nợ trên tổng công nợ xấu";
                chartTitle1.Font = new System.Drawing.Font("Tahoma", 14);
                barChart3D.Titles.Add(chartTitle1);
                barChart3D.Legend.Visible = true;
                barChart3D.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                barChart3D.Legend.AlignmentVertical = LegendAlignmentVertical.Bottom;
                barChart3D.Legend.MarkerSize = new System.Drawing.Size(8, 8);
                barChart3D.Legend.Direction = LegendDirection.LeftToRight;

                barChart3D.Dock = DockStyle.Fill;

                // Add the chart to the form.
                ((SimpleDiagram3D)barChart3D.Diagram).RuntimeRotation = true;
                SetNumericOptions(barChart3D.Series[0], NumericFormat.Percent, 1);

                this.splitContainerControl1.Panel2.Controls.Clear();
                this.splitContainerControl1.Panel2.Controls.Add(barChart3D);
            }
            catch { }
        }

        protected void SetNumericOptions(Series series, NumericFormat format, int precision)
        {
            series.PointOptions.ValueNumericOptions.Format = format;
            series.PointOptions.ValueNumericOptions.Precision = precision;
        }

        private void ctlReference_Load(object sender, EventArgs e)
        {
            Spline();
            splitContainerControl1.SplitterPosition = this.Width / 2;
            Pie3D();
        }

        private void ctlCongNoXau_SizeChanged(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = this.Width / 2;
        }
    }
}
