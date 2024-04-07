using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using DevExpress.XtraCharts;
using System.Windows.Forms;

namespace DemoChart
{
    public partial class frmFullStackedBar : Form
    {
        ChartControl barChart3D = new ChartControl();
        public frmFullStackedBar()
        {
            InitializeComponent();
        }

        void Lines()
        {
            // Create a new chart.
            ChartControl barChart3D = new ChartControl();

            // Add a bar series to it.
            Series series1 = new Series("Series 1", ViewType.Bar);
            series1.LegendText = "DIP HCM";
            Series series2 = new Series("Series 2", ViewType.Bar);
            series2.LegendText = "DIP NT1";

            // Add points to the series.
            for (int i = 1; i < 6; i++)
            {
                Random o = new Random();
                series1.Points.Add(new SeriesPoint(string.Format("A-{0}", i), i * o.Next(5, 18)));
                Random o2 = new Random();
                series2.Points.Add(new SeriesPoint(string.Format("A-{0}", i), i * o2.Next(5, 15)));
            }

            // Add both series to the chart.
            barChart3D.Series.AddRange(new Series[] { series1, series2 });

            // Access labels of the first series.
            ((BarSeriesLabel)series1.Label).Visible = true;
            ((BarSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            ((BarSeriesLabel)series2.Label).Visible = true;
            ((BarSeriesLabel)series2.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            // Access the series options.
            series1.PointOptions.PointView = PointView.ArgumentAndValues;
            series2.PointOptions.PointView = PointView.ArgumentAndValues;

            // Customize the view-type-specific properties of the series.
            BarSeriesView myView = (BarSeriesView)series1.View;
            myView.Transparency = 50;
            BarSeriesView myView2 = (BarSeriesView)series2.View;
            myView2.Transparency = 50;

            // Add a title to the chart and hide the legend.
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "DIP Vietnam";
            barChart3D.Titles.Add(chartTitle1);
            //Show lable
            barChart3D.Legend.Visible = true;
            barChart3D.Dock = DockStyle.Fill;

            // Add the chart to the form.
            barChart3D.Dock = DockStyle.Fill;
            this.Controls.Add(barChart3D);
        }

        void FullStackedBar()
        {
            // Create a new chart.
            ChartControl barChart3D = new ChartControl();

            // Add a bar series to it.
            Series series1 = new Series("Series 1", ViewType.FullStackedBar);
            series1.LegendText = "Đã bán";            
            Series series2 = new Series("Series 2", ViewType.FullStackedBar);
            series2.LegendText = "Đặt cọc";
            Series series3 = new Series("Series 3", ViewType.FullStackedBar);
            series3.LegendText = "Còn trống";

            // Add points to the series.
            for (int i = 1; i < 3; i++)
            {
                Random o = new Random();
                series1.Points.Add(new SeriesPoint(string.Format("A-{0}", i), i * o.Next(50, 180)));
                Random o2 = new Random();
                series2.Points.Add(new SeriesPoint(string.Format("A-{0}", i), i * o2.Next(20, 150)));
                Random o3 = new Random();
                series3.Points.Add(new SeriesPoint(string.Format("A-{0}", i), i * o2.Next(20, 150)));
            }

            // Add both series to the chart.
            barChart3D.Series.AddRange(new Series[] { series3, series2, series1 });

            // Access labels of the first series.
            ((FullStackedBarSeriesLabel)series1.Label).Visible = true;
            ((FullStackedBarSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            ((FullStackedBarSeriesLabel)series2.Label).Visible = true;
            ((FullStackedBarSeriesLabel)series2.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            ((FullStackedBarSeriesLabel)series3.Label).Visible = true;
            ((FullStackedBarSeriesLabel)series3.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            // Access the series options.
            series1.PointOptions.PointView = PointView.Values;
            //((XYDiagram)barChart3D.Diagram).RuntimeRotation = true;
            series2.PointOptions.PointView = PointView.Values;
            //((XYDiagram)barChart3D.Diagram).RuntimeRotation = true;
            series3.PointOptions.PointView = PointView.Values;
            //((XYDiagram)barChart3D.Diagram).RuntimeRotation = true;

            // Customize the view-type-specific properties of the series.
            FullStackedBarSeriesView myView = (FullStackedBarSeriesView)series1.View;
            myView.Transparency = 50;
            FullStackedBarSeriesView myView2 = (FullStackedBarSeriesView)series2.View;
            myView2.Transparency = 50;
            FullStackedBarSeriesView myView3 = (FullStackedBarSeriesView)series2.View;
            myView3.Transparency = 50;

            // Add a title to the chart and hide the legend.
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "DIP Vietnam";
            barChart3D.Titles.Add(chartTitle1);
            //Show lable
            barChart3D.Legend.Visible = true;
            barChart3D.Dock = DockStyle.Fill;

            // Add the chart to the form.
            barChart3D.Dock = DockStyle.Fill;
            //this.barChart3D.IndicatorsPaletteRepository.Add("Palette 1", new DevExpress.XtraCharts.Palette("Palette 1", DevExpress.XtraCharts.PaletteScaleMode.Repeat, new DevExpress.XtraCharts.PaletteEntry[] {
            //    new DevExpress.XtraCharts.PaletteEntry(System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255))))), System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255))))))}));
            //this.barChart3D.PaletteName = "Palette 1";
            //this.barChart3D.PaletteRepository.Add("Palette 1", new DevExpress.XtraCharts.Palette("Palette 1", DevExpress.XtraCharts.PaletteScaleMode.Repeat, new DevExpress.XtraCharts.PaletteEntry[] {
            //    new DevExpress.XtraCharts.PaletteEntry(System.Drawing.Color.White, System.Drawing.Color.White),
            //    new DevExpress.XtraCharts.PaletteEntry(System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(183)))), ((int)(((byte)(46))))), System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(183)))), ((int)(((byte)(46)))))),
            //    new DevExpress.XtraCharts.PaletteEntry(System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(16)))), ((int)(((byte)(33))))), System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(16)))), ((int)(((byte)(33))))))}));
            
            this.Controls.Add(barChart3D);
            SetNumericOptions(barChart3D.Series[0], NumericFormat.Percent, 0);
            SetNumericOptions(barChart3D.Series[1], NumericFormat.Percent, 0);
            SetNumericOptions(barChart3D.Series[2], NumericFormat.Percent, 0);
        }

        void Spline()
        {
            // Create a new chart.
            ChartControl barChart3D = new ChartControl();

            // Add a bar series to it.
            Series series1 = new Series("Series 1", ViewType.Spline);
            series1.LegendText = "DIP HCM";
            Series series2 = new Series("Series 2", ViewType.Spline);
            series2.LegendText = "DIP NT1";
            Series series3 = new Series("Series 3", ViewType.Spline);
            series3.LegendText = "DIP NT2";

            // Add points to the series.
            for (int i = 1; i < 6; i++)
            {
                Random o = new Random();
                series1.Points.Add(new SeriesPoint(string.Format("A-{0}", i), i * o.Next(5, 18)));
                Random o2 = new Random();
                series2.Points.Add(new SeriesPoint(string.Format("A-{0}", i), i * o2.Next(5, 15)));
                Random o3 = new Random();
                series3.Points.Add(new SeriesPoint(string.Format("A-{0}", i), i * o3.Next(3, 7)));
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
            series1.PointOptions.PointView = PointView.ArgumentAndValues;
            series2.PointOptions.PointView = PointView.ArgumentAndValues;
            series3.PointOptions.PointView = PointView.ArgumentAndValues;

            // Customize the view-type-specific properties of the series.
            SplineSeriesView myView = (SplineSeriesView)series1.View;
            myView.LineTensionPercent = 50;

            SplineSeriesView myView2 = (SplineSeriesView)series2.View;
            myView2.LineTensionPercent = 50;
            
            SplineSeriesView myView3 = (SplineSeriesView)series3.View;
            myView3.LineTensionPercent = 50;

            // Add a title to the chart and hide the legend.
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "DIP Vietnam";
            barChart3D.Titles.Add(chartTitle1);
            //Show lable
            barChart3D.Legend.Visible = true;
            barChart3D.Dock = DockStyle.Fill;

            // Add the chart to the form.
            barChart3D.Dock = DockStyle.Fill;
            this.Controls.Add(barChart3D);
        }

        void Line()
        {
            // Create a new chart.
            ChartControl barChart3D = new ChartControl();

            // Add a bar series to it.
            Series series1 = new Series("Series 1", ViewType.Bar);

            // Add points to the series.
            for (int i = 1; i < 5; i++)
            {
                Random o = new Random();
                series1.Points.Add(new SeriesPoint(string.Format("A{0}", i), i * o.Next(5, 10)));
            }

            // Add both series to the chart.
            barChart3D.Series.AddRange(new Series[] { series1 });

            // Access labels of the first series.
            ((BarSeriesLabel)series1.Label).Visible = true;
            ((BarSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            // Access the series options.
            series1.PointOptions.PointView = PointView.ArgumentAndValues;

            // Customize the view-type-specific properties of the series.
            BarSeriesView myView = (BarSeriesView)series1.View;
            myView.Transparency = 50;

            // Add a title to the chart and hide the legend.
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "DIP Vietnam";
            barChart3D.Titles.Add(chartTitle1);
            barChart3D.Legend.Visible = false;
            barChart3D.Dock = DockStyle.Fill;

            // Add the chart to the form.
            barChart3D.Dock = DockStyle.Fill;
            this.Controls.Add(barChart3D);
        }

        void Pie3D()
        {
            // Create a new chart.           

            // Add a bar series to it.
            Series series1 = new Series("DIP Vietnam", ViewType.Pie3D);

            // Add points to the series.
            for (int i = 1; i <= 10; i++)
            {
                Random o = new Random();
                series1.Points.Add(new SeriesPoint(string.Format("DIP {0}", i), i * o.Next(5, 10)));
            }

            // Add both series to the chart.
            barChart3D.Series.AddRange(new Series[] { series1 });

            // Access labels of the first series.
            ((Pie3DSeriesLabel)series1.Label).Visible = true;
            ((Pie3DSeriesLabel)series1.Label).ResolveOverlappingMode =
                ResolveOverlappingMode.Default;

            // Access the series options.
            series1.PointOptions.PointView = PointView.ArgumentAndValues;            

            // Customize the view-type-specific properties of the series.
            Pie3DSeriesView myView = (Pie3DSeriesView)series1.View;
            myView.Depth = 10;
            myView.SizeAsPercentage = 100;
            myView.ExplodeMode = PieExplodeMode.All;

            // Add a title to the chart and hide the legend.
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "DIP Vietnam";
            barChart3D.Titles.Add(chartTitle1);
            barChart3D.Legend.Visible = false;
            barChart3D.Dock = DockStyle.Fill;

            // Add the chart to the form.
            barChart3D.Dock = DockStyle.Fill;
            this.Controls.Add(barChart3D);
            SetNumericOptions(barChart3D.Series[0], NumericFormat.Percent, 0);
        }

        protected void SetNumericOptions(Series series, NumericFormat format, int precision)
        {
            series.PointOptions.ValueNumericOptions.Format = format;
            series.PointOptions.ValueNumericOptions.Precision = precision;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            FullStackedBar();
        }
    }
}
