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
using LandSoft.Library;

namespace LandSoft.NghiepVu.Report
{
    public partial class ctlKhachHangTheoNhom : DevExpress.XtraEditors.XtraUserControl
    {
        public ctlKhachHangTheoNhom()
        {
            InitializeComponent();

            LandSoft.Translate.Language.TranslateUserControl(this);
        }
        private void BaoCao_Nap()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                chartControl1.Series.Clear();
                using (var db = new MasterDataContext())
                {
                    var ltGroup = (from nkh in db.NhomKHs
                                   join kh in db.KhachHangs on nkh.MaNKH equals kh.MaNKH
                                   group nkh by new { nkh.MaNKH } into gr
                                   select new 
                                   { 
                                       NhomKH = gr.Key.MaNKH,
                                       SoLuong = gr.Count()
                                   }).ToList();

                    var strTitle = "Chart Of Customers By Group";

                    Series series1 = new Series(strTitle, ViewType.Doughnut);

                    // Add points to the series.
                    foreach (var dt in ltGroup)
                        series1.Points.Add(new SeriesPoint(string.Format("{0} ({1})", dt.NhomKH, dt.SoLuong), dt.SoLuong));

                    ((DoughnutSeriesLabel)series1.Label).Position = PieSeriesLabelPosition.Inside;
                    ((DoughnutSeriesLabel)series1.Label).Visible = true;
                    ((DoughnutSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

                    // Access the series options.
                    series1.PointOptions.PointView = PointView.ArgumentAndValues;
                    series1.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    series1.PointOptions.ValueNumericOptions.Precision = 0;
                    series1.LegendPointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    series1.LegendPointOptions.ValueNumericOptions.Precision = 0;

                    // Add both series to the chart.
                    chartControl1.Series.Clear();
                    chartControl1.Series.AddRange(new Series[] { series1 });
                    // Add a title to the chart and hide the legend.
                    if (chartControl1.Titles.Count == 0)
                    {

                        ChartTitle chartTitle1 = new ChartTitle();
                        chartTitle1.Text = strTitle;
                        chartTitle1.WordWrap = true;
                        chartControl1.Titles.Add(chartTitle1);
                        chartControl1.Legend.Visible = true;
                    }
                    else
                    {
                        chartControl1.Titles[0].Text = strTitle;
                    }
                }
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }
        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Nap();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            it.CommonCls.ExportExcel(chartControl1);
        }

        private void ctlKhachHangTheoNhom_Load(object sender, EventArgs e)
        {
            BaoCao_Nap();
        }
    }
}