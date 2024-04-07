using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;

namespace BEEREMA.BaoCao.Chart
{
    public partial class ctlReference : DevExpress.XtraEditors.XtraUserControl
    {
        ChartControl barChart3D;
        public ctlReference()
        {
            InitializeComponent();
        }

        void FullStackedBar()
        {
            // Create a new chart.
            barChart3D = new ChartControl();
            this.barChart3D.AppearanceName = "Pastel Kit";

            // Add a bar series to it.
            Series series1 = new Series("Series 1", ViewType.Bar3D);
            series1.LegendText = "Đã thu trước 19/01/2013";
            Series series2 = new Series("Series 2", ViewType.Bar3D);
            series2.LegendText = "Đã thu tuần 21-26/01/2013";
            Series series3 = new Series("Series 3", ViewType.Bar3D);
            series3.LegendText = "Đã thu tuần này";
            Series series4 = new Series("Series 4", ViewType.Bar3D);
            series4.LegendText = "Còn phải thu";

            // Add points to the series.
            Random o = new Random();
            series1.Points.Add(new SeriesPoint("Đợt 1-2-3-4", o.Next(50, 180)));
            Random o2 = new Random();
            series2.Points.Add(new SeriesPoint("Đợt 1-2-3-4", o2.Next(20, 150)));
            Random o3 = new Random();
            series3.Points.Add(new SeriesPoint("Đợt 1-2-3-4", o2.Next(20, 150)));
            Random o4 = new Random();
            series4.Points.Add(new SeriesPoint("Đợt 1-2-3-4", o2.Next(20, 150)));

            o = new Random();
            series1.Points.Add(new SeriesPoint("Đợt 5", o.Next(50, 180)));
            o2 = new Random();
            series2.Points.Add(new SeriesPoint("Đợt 5", o2.Next(20, 150)));
            o3 = new Random();
            series3.Points.Add(new SeriesPoint("Đợt 5", o2.Next(20, 150)));
            o4 = new Random();
            series4.Points.Add(new SeriesPoint("Đợt 5", o2.Next(20, 150)));

            o = new Random();
            series1.Points.Add(new SeriesPoint("Đợt 6-7", o.Next(50, 50)));
            o2 = new Random();
            series2.Points.Add(new SeriesPoint("Đợt 6-7", o2.Next(20, 50)));
            o3 = new Random();
            series3.Points.Add(new SeriesPoint("Đợt 6-7", o2.Next(20, 50)));
            o4 = new Random();
            series4.Points.Add(new SeriesPoint("Đợt 6-7", o2.Next(20, 50)));

            // Add both series to the chart.
            barChart3D.Series.AddRange(new Series[] { series1, series2, series3, series4 });

            // Access labels of the first series.
            ((Bar3DSeriesLabel)series1.Label).Visible = true;
            ((Bar3DSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
            ((Bar3DSeriesLabel)series1.Label).TextOrientation = TextOrientation.TopToBottom;

            ((Bar3DSeriesLabel)series2.Label).Visible = true;
            ((Bar3DSeriesLabel)series2.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
            ((Bar3DSeriesLabel)series2.Label).TextOrientation = TextOrientation.TopToBottom;

            ((Bar3DSeriesLabel)series3.Label).Visible = true;
            ((Bar3DSeriesLabel)series3.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
            ((Bar3DSeriesLabel)series3.Label).TextOrientation = TextOrientation.TopToBottom;

            ((Bar3DSeriesLabel)series4.Label).Visible = true;
            ((Bar3DSeriesLabel)series4.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
            ((Bar3DSeriesLabel)series4.Label).TextOrientation = TextOrientation.TopToBottom;

            // Access the series options.
            series1.PointOptions.PointView = PointView.Values;
            series2.PointOptions.PointView = PointView.Values;
            series3.PointOptions.PointView = PointView.Values;
            series4.PointOptions.PointView = PointView.Values;
            ((XYDiagram3D)barChart3D.Diagram).RotationMatrixSerializable = "0.990798492101262;0.0249088550898135;0.133033443118687;0;-0.0489917961366646;0.98" +
    "226937134411;0.180960454327938;0;-0.126167158806093;-0.185812892602708;0.9744513" +
    "41515552;0;0;0;0;1";
            ((XYDiagram3D)barChart3D.Diagram).RuntimeRotation = true;
            ((XYDiagram3D)barChart3D.Diagram).RuntimeZooming = true;
            ((XYDiagram3D)barChart3D.Diagram).ZoomPercent = 150;

            // Customize the view-type-specific properties of the series.
            SideBySideBar3DSeriesView myView = (SideBySideBar3DSeriesView)series1.View;
            myView.Transparency = 50;
            myView.Model = Bar3DModel.Cylinder;
            myView.Color = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(98)))), ((int)(((byte)(144)))));

            SideBySideBar3DSeriesView myView2 = (SideBySideBar3DSeriesView)series2.View;
            myView2.Transparency = 50;
            myView2.Model = Bar3DModel.Cylinder;
            myView2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(159)))), ((int)(((byte)(61)))));

            SideBySideBar3DSeriesView myView3 = (SideBySideBar3DSeriesView)series3.View;
            myView3.Transparency = 50;
            myView3.Model = Bar3DModel.Cylinder;
            myView3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));

            SideBySideBar3DSeriesView myView4 = (SideBySideBar3DSeriesView)series4.View;
            myView4.Transparency = 50;
            myView4.Model = Bar3DModel.Cylinder;
            myView4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(76)))), ((int)(((byte)(123)))));

            // Add a title to the chart and hide the legend.
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "Biểu tham chiếu việc thu hồi công nợ (Demo)";
            chartTitle1.Font = new Font("Calibri", 16);
            barChart3D.Titles.Add(chartTitle1);
            //Show lable
            barChart3D.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            barChart3D.Legend.AlignmentVertical = LegendAlignmentVertical.Bottom;
            barChart3D.Legend.Direction = LegendDirection.LeftToRight;
            barChart3D.Legend.Visible = true;
            barChart3D.Legend.MarkerSize = new System.Drawing.Size(8, 8);
            barChart3D.Legend.Font = new Font("Tahoma", 10);
            //two row
            //barChart3D.Legend.MaxVerticalPercentage = 50;
            //barChart3D.Legend.MaxHorizontalPercentage = 50;
            barChart3D.Padding.All = 0;

            // Add the chart to the form.
            barChart3D.Dock = DockStyle.Fill;

            this.Controls.Add(barChart3D);
        }

        protected void SetNumericOptions(Series series, NumericFormat format, int precision)
        {
            series.PointOptions.ValueNumericOptions.Format = format;
            series.PointOptions.ValueNumericOptions.Precision = precision;
        }

        private void ctlReference_Load(object sender, EventArgs e)
        {
            FullStackedBar();
        }
    }
}
