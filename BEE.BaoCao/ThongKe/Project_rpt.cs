using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraCharts;

namespace BEE.BaoCao.ThongKe
{
    public partial class Project_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public Project_rpt()
        {
            InitializeComponent();
            Pie3D();
        }

        void Pie3D()
        {
            // Create a new chart.
            ChartControl barChart3D = new ChartControl();

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
            barChart3D.Legend.Visible = true;

            // Add the chart to the form.
            //barChart3D.Dock = DockStyle.Fill;
            panelControl1.Controls.Add(barChart3D);
            //SetNumericOptions(barChart3D.Series[0], NumericFormat.Percent, 0);
        }

        private void Project_rpt_DesignerLoaded(object sender, DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs e)
        {
            
        }
    }
}
