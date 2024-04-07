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
    public partial class ctlThuHoiCongNo : DevExpress.XtraEditors.XtraUserControl
    {
        ChartControl barChart3D;
        public ctlThuHoiCongNo()
        {
            InitializeComponent();
        }

        void FullStackedBar()
        {
            // Create a new chart.
            barChart3D = new ChartControl();
            this.barChart3D.AppearanceName = "Pastel Kit";

            // Add a bar series to it.
            Series series1 = new Series("Series 1", ViewType.FullStackedBar);
            series1.LegendText = "HĐ đã TT";
            Series series2 = new Series("Series 2", ViewType.FullStackedBar);
            series2.LegendText = "HĐ có công nợ";

            List<Data> objData = new List<Data>();
            Data item = new Data(1, "Đợt 1-2-3-4", 85, 15, 0);
            objData.Add(item);

            item = new Data(2, "Đợt 5", 87, 13, 0);
            objData.Add(item);

            item = new Data(3, "Đợt 6-7", 73, 27, 0);
            objData.Add(item);

            item = new Data(0, "Tổng cộng", 245, 55, 0);
            objData.Add(item);

            // Add points to the series.
            series1.Points.Add(new SeriesPoint("Đợt 1-2-3-4", 85));
            series2.Points.Add(new SeriesPoint("Đợt 1-2-3-4", 15));

            series1.Points.Add(new SeriesPoint("Đợt 5", 87));
            series2.Points.Add(new SeriesPoint("Đợt 5", 23));

            series1.Points.Add(new SeriesPoint("Đợt 6-7", 73));
            series2.Points.Add(new SeriesPoint("Đợt 6-7", 27));



            // Add both series to the chart.
            barChart3D.Series.AddRange(new Series[] { series1, series2 });

            // Access labels of the first series.
            ((FullStackedBarSeriesLabel)series1.Label).Visible = true;
            ((FullStackedBarSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            ((FullStackedBarSeriesLabel)series2.Label).Visible = true;
            ((FullStackedBarSeriesLabel)series2.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            // Access the series options.
            series1.PointOptions.PointView = PointView.Values;
            series2.PointOptions.PointView = PointView.Values;

            // Customize the view-type-specific properties of the series.
            FullStackedBarSeriesView myView = (FullStackedBarSeriesView)series1.View;
            myView.Transparency = 50;
            myView.BarWidth = 0.4;
            myView.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            myView.FillStyle.FillMode = FillMode.Solid;

            FullStackedBarSeriesView myView2 = (FullStackedBarSeriesView)series2.View;
            myView2.Transparency = 50;
            myView2.BarWidth = 0.4;
            myView2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            myView2.FillStyle.FillMode = FillMode.Solid;

            ((XYDiagram)barChart3D.Diagram).Rotated = true;
            ((XYDiagram)barChart3D.Diagram).AxisY.Alignment = AxisAlignment.Far;
            ((XYDiagram)barChart3D.Diagram).AxisY.Range.SideMarginsEnabled = false;
            ((XYDiagram)barChart3D.Diagram).AxisY.NumericOptions.Format = NumericFormat.Percent;
            ((XYDiagram)barChart3D.Diagram).AxisY.NumericOptions.Precision = 0;

            // Add a title to the chart and hide the legend.
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "Tổng kết theo Hợp đồng";
            chartTitle1.Font = new Font("Calibri", 16);
            barChart3D.Titles.Add(chartTitle1);
            //Show lable
            barChart3D.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            barChart3D.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            barChart3D.Legend.Direction = LegendDirection.LeftToRight;
            barChart3D.Legend.Visible = true;
            barChart3D.Legend.MarkerSize = new System.Drawing.Size(10, 10);
            barChart3D.Legend.Font = new Font("Tahoma", 10);
            //two row
            //barChart3D.Legend.MaxVerticalPercentage = 50;
            //barChart3D.Legend.MaxHorizontalPercentage = 50;
            barChart3D.Padding.All = 0;

            // Add the chart to the form.
            barChart3D.Dock = DockStyle.Fill;
            SetNumericOptions(barChart3D.Series[0], NumericFormat.Percent, 0);
            SetNumericOptions(barChart3D.Series[1], NumericFormat.Percent, 0);

            this.splitSubPanel1.Panel1.Controls.Add(barChart3D);

            gcContract.DataSource = objData;
            objData = null;
            System.GC.Collect();
        }

        void FullStackedBarByMoney()
        {
            // Create a new chart.
            barChart3D = new ChartControl();
            this.barChart3D.AppearanceName = "Pastel Kit";

            // Add a bar series to it.
            Series series1 = new Series("Series 1", ViewType.FullStackedBar);
            series1.LegendText = "Số tiền cần phải thu";
            Series series2 = new Series("Series 2", ViewType.FullStackedBar);
            series2.LegendText = "Số tiền đã thu";

            List<Data> objData = new List<Data>();
            Data item = new Data(1, "Đợt 1-2-3-4", 750000000, 250000000, 0);
            objData.Add(item);

            item = new Data(2, "Đợt 5", 880000000, 120000000, 0);
            objData.Add(item);

            item = new Data(3, "Đợt 6-7", 970000000, 30000000, 0);
            objData.Add(item);

            item = new Data(0, "Tổng cộng", 2600000000, 400000000, 0);
            objData.Add(item);

            // Add points to the series.
            Random o = new Random();
            series1.Points.Add(new SeriesPoint("Đợt 1-2-3-4", 75));
            Random o2 = new Random();
            series2.Points.Add(new SeriesPoint("Đợt 1-2-3-4", 25));

            o = new Random();
            series1.Points.Add(new SeriesPoint("Đợt 5", 88));
            o2 = new Random();
            series2.Points.Add(new SeriesPoint("Đợt 5", 12));

            o = new Random();
            series1.Points.Add(new SeriesPoint("Đợt 6-7", 97));
            o2 = new Random();
            series2.Points.Add(new SeriesPoint("Đợt 6-7", 3));

            // Add both series to the chart.
            barChart3D.Series.AddRange(new Series[] { series1, series2 });

            // Access labels of the first series.
            ((FullStackedBarSeriesLabel)series1.Label).Visible = true;
            ((FullStackedBarSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            ((FullStackedBarSeriesLabel)series2.Label).Visible = true;
            ((FullStackedBarSeriesLabel)series2.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            // Access the series options.
            series1.PointOptions.PointView = PointView.Values;
            series2.PointOptions.PointView = PointView.Values;

            // Customize the view-type-specific properties of the series.
            FullStackedBarSeriesView myView = (FullStackedBarSeriesView)series1.View;
            myView.Transparency = 50;
            myView.BarWidth = 0.3;
            myView.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(0)))));
            myView.FillStyle.FillMode = FillMode.Solid;

            FullStackedBarSeriesView myView2 = (FullStackedBarSeriesView)series2.View;
            myView2.Transparency = 50;
            myView2.BarWidth = 0.3;
            myView2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(0)))), ((int)(((byte)(102)))));
            myView2.FillStyle.FillMode = FillMode.Solid;

            ((XYDiagram)barChart3D.Diagram).AxisY.Alignment = AxisAlignment.Far;
            ((XYDiagram)barChart3D.Diagram).AxisY.Range.SideMarginsEnabled = false;
            ((XYDiagram)barChart3D.Diagram).AxisY.NumericOptions.Format = NumericFormat.Percent;
            ((XYDiagram)barChart3D.Diagram).AxisY.NumericOptions.Precision = 0;

            // Add a title to the chart and hide the legend.
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "Tổng kết theo dòng tiền vào";
            chartTitle1.Font = new Font("Calibri", 16);
            barChart3D.Titles.Add(chartTitle1);
            //Show lable
            barChart3D.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            barChart3D.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            barChart3D.Legend.Direction = LegendDirection.LeftToRight;
            barChart3D.Legend.Visible = true;
            barChart3D.Legend.MarkerSize = new System.Drawing.Size(10, 10);
            barChart3D.Legend.Font = new Font("Tahoma", 10);
            //two row
            //barChart3D.Legend.MaxVerticalPercentage = 50;
            //barChart3D.Legend.MaxHorizontalPercentage = 50;
            barChart3D.Padding.All = 0;

            // Add the chart to the form.
            barChart3D.Dock = DockStyle.Fill;
            SetNumericOptions(barChart3D.Series[0], NumericFormat.Percent, 0);
            SetNumericOptions(barChart3D.Series[1], NumericFormat.Percent, 0);

            this.splitSubPanel2.Panel1.Controls.Add(barChart3D);

            gcMoney.DataSource = objData;
            objData = null;
            System.GC.Collect();
        }

        protected void SetNumericOptions(Series series, NumericFormat format, int precision)
        {
            series.PointOptions.ValueNumericOptions.Format = format;
            series.PointOptions.ValueNumericOptions.Precision = precision;
        }

        private void ctlReference_Load(object sender, EventArgs e)
        {
            FullStackedBar();
            FullStackedBarByMoney();
        }

        private void ctlThuHoiCongNo_SizeChanged(object sender, EventArgs e)
        {
            splitMain.SplitterPosition = (this.Width / 2);
            splitSubPanel1.SplitterPosition = (splitMain.Panel1.Height / 2) + 50;
            splitSubPanel2.SplitterPosition = (splitMain.Panel2.Height / 2) + 50;
        }

        private void gvContract_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (gvContract.GetRowCellValue(e.RowHandle, colName).ToString() == "Tổng cộng")
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            catch { }
        }

        private void gvMoney_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (gvMoney.GetRowCellValue(e.RowHandle, colName2).ToString() == "Tổng cộng")
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }
            catch { }
        }
    }
}
