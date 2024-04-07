using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using System.Linq;
using System.Data.Linq.SqlClient;
using BEE.ThuVien;

namespace BEEREMA.BaoCao.Chart
{
    public partial class ctlResutlBuyAgent : DevExpress.XtraEditors.XtraUserControl
    {
        ChartControl barChart3D;
        string Zone = "";
        int Amount = 0;
        List<Data> objData;
        public ctlResutlBuyAgent()
        {
            InitializeComponent();
        }

        private void BaoCao_Nap()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                Pie3D();

                FullStackedBar();
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        void FullStackedBar()
        {
            // Create a new chart.
            barChart3D = new ChartControl();

            // Add a bar series to it.
            Series series1 = new Series("Series 1", ViewType.FullStackedBar);
            series1.LegendText = "Công ty";
            Series series2 = new Series("Series 2", ViewType.FullStackedBar);
            series2.LegendText = "Đại lý";

            // Add points to the series.
            using (var db = new MasterDataContext())
            {
                try
                {
                    var zone = db.Khus.Where(p => p.MaDA == (int)itemDuAn.EditValue).ToList();
                    objData = new List<Data>();
                    int index = 1;
                    foreach (var s in zone)
                    {
                        var val1 = db.funcCountSoldByZone(s.MaKhu);
                        series1.Points.Add(new SeriesPoint(string.Format("Khu {0}", s.TenKhu), val1));
                        var val2 = db.funcCountAgentSoldByZone(s.MaKhu);
                        series2.Points.Add(new SeriesPoint(string.Format("Khu {0}", s.TenKhu), val2));

                        var item = new Data(index, string.Format("Khu {0}", s.TenKhu), (int)val1, (int)val2, 0);
                        objData.Add(item);
                        index++;
                    }
                    Amount = zone.Count;
                }
                catch { }
            }

            // Add both series to the chart.
            barChart3D.Series.AddRange(new Series[] { series1, series2 });

            // Access labels of the first series.
            ((FullStackedBarSeriesLabel)series1.Label).Visible = true;
            ((FullStackedBarSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            ((FullStackedBarSeriesLabel)series2.Label).Visible = true;
            ((FullStackedBarSeriesLabel)series2.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
            // Access the series options.
            series1.PointOptions.PointView = PointView.Values;
            //((XYDiagram)barChart3D.Diagram).RuntimeRotation = true;
            series2.PointOptions.PointView = PointView.Values;

            // Customize the view-type-specific properties of the series.
            FullStackedBarSeriesView myView = (FullStackedBarSeriesView)series1.View;
            myView.Transparency = 50; myView.BarWidth = GetBarWidth();
            FullStackedBarSeriesView myView2 = (FullStackedBarSeriesView)series2.View;
            myView2.Transparency = 50; myView2.BarWidth = GetBarWidth();

            ((XYDiagram)barChart3D.Diagram).AxisY.Alignment = AxisAlignment.Far;
            ((XYDiagram)barChart3D.Diagram).AxisY.Range.SideMarginsEnabled = false;
            ((XYDiagram)barChart3D.Diagram).AxisY.NumericOptions.Format = NumericFormat.Percent;
            ((XYDiagram)barChart3D.Diagram).AxisY.NumericOptions.Precision = 0;

            // Add a title to the chart and hide the legend.
            //ChartTitle chartTitle1 = new ChartTitle();
            //chartTitle1.Text = "DIP Vietnam";
            //barChart3D.Titles.Add(chartTitle1);
            //Show lable
            barChart3D.Legend.Visible = false;
            barChart3D.Dock = DockStyle.Fill;

            // Add the chart to the form.
            barChart3D.Dock = DockStyle.Fill;
            
            SetNumericOptions(barChart3D.Series[0], NumericFormat.Percent, 0);
            SetNumericOptions(barChart3D.Series[1], NumericFormat.Percent, 0);

            this.splitSub.Panel1.Controls.Clear();
            this.splitSub.Panel1.Controls.Add(barChart3D);

            gcData.DataSource = objData;
        }

        double GetBarWidth()
        {
            switch (Amount)
            {
                case 1:
                    return 0.15;
                case 2:
                case 3:
                    return 0.3;
                default:
                    return 0.5;
            }
        }

        void Pie3D()
        {
            try
            {
                // Create a new chart.           
                barChart3D = new ChartControl();

                // Add a bar series to it.
                Series series1 = new Series("DIP Vietnam", ViewType.Pie);

                // Add points to the series.
                using (var db = new MasterDataContext())
                {
                    try
                    {
                        series1.Points.Add(new SeriesPoint("Công ty", db.funcCountSoldByProject((int)itemDuAn.EditValue)));
                        series1.Points.Add(new SeriesPoint("Đại lý", db.funcCountAgentSoldByProject((int)itemDuAn.EditValue)));
                    }
                    catch { }
                }

                // Add both series to the chart.
                barChart3D.Series.AddRange(new Series[] { series1 });

                // Access labels of the first series.
                ((PieSeriesLabel)series1.Label).Visible = true;
                ((PieSeriesLabel)series1.Label).ResolveOverlappingMode =
                    ResolveOverlappingMode.Default;

                // Access the series options.
                series1.LegendPointOptions.PointView = PointView.Argument;
                series1.PointOptions.PointView = PointView.Values;

                // Customize the view-type-specific properties of the series.
                PieSeriesView myView = (PieSeriesView)series1.View;
                //myView.Depth = 8;
                //myView.SizeAsPercentage = 100;
                myView.ExplodeMode = PieExplodeMode.All;

                // Add a title to the chart and hide the legend.
                ChartTitle chartTitle1 = new ChartTitle();
                chartTitle1.Text = Zone;
                chartTitle1.Font = new Font("Calibri", 16);
                barChart3D.Titles.Add(chartTitle1);
                barChart3D.Legend.Visible = true;
                barChart3D.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                barChart3D.Legend.AlignmentVertical = LegendAlignmentVertical.Bottom;
                barChart3D.Legend.MarkerSize = new System.Drawing.Size(10, 10);
                barChart3D.Legend.Font = new Font("Tahoma", 10);
                barChart3D.Legend.Direction = LegendDirection.LeftToRight;
                barChart3D.Dock = DockStyle.Fill;

                // Add the chart to the form.
                barChart3D.Dock = DockStyle.Fill;
                //((SimpleDiagram3D)barChart3D.Diagram).RuntimeRotation = true;
                SetNumericOptions(barChart3D.Series[0], NumericFormat.Percent, 0);

                this.splitMain.Panel1.Controls.Clear();
                this.splitMain.Panel1.Controls.Add(barChart3D);
            }
            catch { }
        }

        protected void SetNumericOptions(Series series, NumericFormat format, int precision)
        {
            series.PointOptions.ValueNumericOptions.Format = format;
            series.PointOptions.ValueNumericOptions.Precision = precision;
        }

        private void ctlDoanhSoTheoNam_Load(object sender, EventArgs e)
        {
            splitMain.SplitterPosition = (this.Width / 2) + 100;
            splitSub.SplitterPosition = splitMain.Panel2.Height / 2;
            using (var db = new MasterDataContext())
            {
                var ltDuAn = db.DuAns.OrderByDescending(p => p.TenDA).Select(p => new DuAnItem(p.MaDA, p.TenDA)).ToList();
                lookUpDuAn.DataSource = ltDuAn;
                if (ltDuAn.Count > 0)
                {
                    itemDuAn.EditValue = ltDuAn[0].MaDA;

                    var zone = db.Khus.Where(p => p.MaDA == ltDuAn[0].MaDA);
                    Zone = "";
                    try
                    {
                        foreach (var s in zone)
                            Zone += "Khu " + s.TenKhu + " + ";
                        Zone = Zone.Substring(0, Zone.LastIndexOf(" +"));
                    }
                    catch { }
                }
            }

            BaoCao_Nap();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Nap();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            it.CommonCls.ExportExcel(barChart3D);
        }

        private void ctlResutlBuy_SizeChanged(object sender, EventArgs e)
        {
            splitMain.SplitterPosition = (this.Width / 2) + 100;
            splitSub.SplitterPosition = splitMain.Panel2.Height / 2;
        }
    }
}