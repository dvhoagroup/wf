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

namespace BEEREMA.BaoCao.BieuDo
{
    public partial class ctlSanPhamTheoKhu : DevExpress.XtraEditors.XtraUserControl
    {
        ChartControl barChart3D;
        string Zone = "";
        int Amount = 0;
        List<Data> objData;
        public ctlSanPhamTheoKhu()
        {
            InitializeComponent();
        }

        private void BaoCao_Nap()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
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
            series1.LegendText = "Sold";
            Series series2 = new Series("Series 2", ViewType.FullStackedBar);
            series2.LegendText = "Available";

            // Add points to the series.
            using (var db = new MasterDataContext())
            {
                try
                {
                    var temp = (from sp in db.bdsSanPhams
                                join k in db.Khus on sp.MaKhu equals k.MaKhu
                                where sp.MaDA ==  (int?)itemDuAn.EditValue
                                group new { k, sp } by new { sp.MaKhu, k.TenKhu } into gr
                                select new
                                {
                                    MaKhu = gr.Key.MaKhu,
                                    TenKhu = gr.Key.TenKhu,
                                    DaBan = gr.Sum(p => p.sp.MaTT >= 3 ? 1 : 0),
                                    ChuaBan = gr.Sum(p => p.sp.MaTT < 3 ? 1 : 0)
                                }).ToList();

                    //var zone = db.DuAns.Select(p => new { p.MaDA, p.TenDA }).ToList();
                    //objData = new List<Data>();
                    int index = 1;
                    foreach (var s in temp)
                    {
                        //var val1 = db.funcCountSoldByProject(s.MaDA);
                        series1.Points.Add(new SeriesPoint(string.Format("{0}", s.TenKhu), s.DaBan));
                       // var val2 = db.funcCountAgentSoldByProject(s.MaDA);
                        series2.Points.Add(new SeriesPoint(string.Format("{0}", s.TenKhu), s.ChuaBan));

                        //var item = new Library.Data(index, string.Format("{0}", s.TenDA), (int)val1, (int)val2, (int)val3, 0);
                       // objData.Add(item);
                        index++;
                    }
                    Amount = temp.Count;
                }
                catch { }
            }
            barChart3D.Series.AddRange(new Series[] { series1, series2 });

            // Access labels of the first series.
            ((FullStackedBarSeriesLabel)series1.Label).Visible = true;
            ((FullStackedBarSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            ((FullStackedBarSeriesLabel)series2.Label).Visible = true;
            ((FullStackedBarSeriesLabel)series2.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            series1.PointOptions.PointView = PointView.Values;
            series2.PointOptions.PointView = PointView.Values;

            FullStackedBarSeriesView myView = (FullStackedBarSeriesView)series1.View;
            myView.Transparency = 50; myView.BarWidth = GetBarWidth();
            myView.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(178)))), ((int)(((byte)(238)))));
            myView.FillStyle.FillMode = FillMode.Solid;

            FullStackedBarSeriesView myView2 = (FullStackedBarSeriesView)series2.View;
            myView2.Transparency = 50; myView2.BarWidth = GetBarWidth();
            myView2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(118)))), ((int)(((byte)(33)))));
            myView2.FillStyle.FillMode = FillMode.Solid;


            ((XYDiagram)barChart3D.Diagram).AxisY.Alignment = AxisAlignment.Far;
            ((XYDiagram)barChart3D.Diagram).AxisY.Range.SideMarginsEnabled = false;
            ((XYDiagram)barChart3D.Diagram).AxisY.NumericOptions.Format = NumericFormat.Percent;
            ((XYDiagram)barChart3D.Diagram).AxisY.NumericOptions.Precision = 0;

            barChart3D.Legend.Visible = true;
            barChart3D.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            barChart3D.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            barChart3D.Legend.Direction = LegendDirection.LeftToRight;
            barChart3D.Dock = DockStyle.Fill;

            // Add the chart to the form.
            
            SetNumericOptions(barChart3D.Series[0], NumericFormat.Percent,0);
            SetNumericOptions(barChart3D.Series[1], NumericFormat.Percent, 0);

            series1.LegendPointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
            series2.LegendPointOptions.ValueNumericOptions.Format = NumericFormat.Percent;

            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "Sản phẩm theo khu";
            barChart3D.Titles.Add(chartTitle1);

            this.splitMain.Panel1.Controls.Clear();
            this.splitMain.Panel1.Controls.Add(barChart3D);
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

        protected void SetNumericOptions(Series series, NumericFormat format, int precision)
        {
            series.PointOptions.ValueNumericOptions.Format = format;
            series.PointOptions.ValueNumericOptions.Precision = precision;
        }

        private void ctlDoanhSoTheoNam_Load(object sender, EventArgs e)
        {
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
            using (var db = new MasterDataContext())
            {
                lookDuAn.DataSource = db.DuAns.OrderByDescending(p => p.NgayDang).Select(p => new { p.MaDA, p.TenDA}).ToList();
            }
        }
    }
}