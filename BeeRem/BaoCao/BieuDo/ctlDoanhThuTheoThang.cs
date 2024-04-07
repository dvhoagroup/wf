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
    public partial class ctlDoanhThuTheoThang : DevExpress.XtraEditors.XtraUserControl
    {
        public ctlDoanhThuTheoThang()
        {
            InitializeComponent();
        }

        private void BaoCao_Nap()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var year = (int?)itemYear.EditValue;
                var month = (int?)itemMonth.EditValue;

                chartControl1.Series.Clear();

                using (var db = new MasterDataContext())
                {
                    var ltSource = (from ng in db.tblNgays
                                    join pt in db.pgcPhieuThus.Where(p => p.NgayThu.GetValueOrDefault().Year == year &&
                                        p.NgayThu.GetValueOrDefault().Month == month) on ng.Ngay equals pt.NgayThu.GetValueOrDefault().Day into p
                                    from pt in p.DefaultIfEmpty()
                                    group pt by new { ng.Ngay } into gr
                                    select new
                                    {
                                        gr.Key.Ngay,
                                        SoTien = gr.Sum(p => p.SoTien/1000000)
                                    }).ToList();

                    Series series1 = new Series("Ngày", ViewType.Bar);
                    Series series2 = new Series("Doanh thu", ViewType.Line);

                    // Add points to the series.
                    foreach (var l in ltSource)
                    {
                        series1.Points.Add(new SeriesPoint(l.Ngay.ToString(), l.SoTien.GetValueOrDefault()));
                        series2.Points.Add(new SeriesPoint(l.Ngay.ToString(), l.SoTien.GetValueOrDefault()));
                    }

                    // Add both series to the chart.
                    chartControl1.Series.AddRange(new Series[] { series1, series2 });

                    /// Access labels of the first series.
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

                    //Diagram
                    XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
                    diagram.AxisY.Title.Text = "Doanh thu";
                    diagram.AxisY.Title.Visible = true;
                    diagram.AxisX.Visible = true;
                    diagram.AxisX.Label.Angle = 0;
                    diagram.AxisX.Label.Antialiasing = true;
                    diagram.AxisX.Label.Staggered = true;
                    diagram.AxisY.Title.Font = new Font("Tahoma", 18);

                    // Add a title to the chart and hide the legend.
                    var strTitle = string.Format("BIỂU ĐỒ DOANH THU THEO THÁNG {0}/{1}", month, year);
                    if (chartControl1.Titles.Count == 0)
                    {

                        ChartTitle chartTitle1 = new ChartTitle();
                        chartTitle1.Text = strTitle;
                        chartTitle1.WordWrap = true;
                        chartControl1.Titles.Add(chartTitle1);
                        chartControl1.Legend.Visible = false;
                    }
                    else
                    {
                        chartControl1.Titles[0].Text = strTitle;
                    }

                    // Add the chart to the form.
                    chartControl1.Series[0].PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
                    chartControl1.Series[0].PointOptions.ValueNumericOptions.Precision = 0;
                }
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        private void ctlDoanhThuTheoThang_Load(object sender, EventArgs e)
        {
            for (int i = DateTime.Now.Year; i >= 2000; i--)
                cmbYear.Items.Add(i);
            itemYear.EditValue = DateTime.Now.Year;
            for (int i = 1; i < 13; i++)
                cmbMonth.Items.Add(i);
            itemMonth.EditValue = DateTime.Now.Month;

            BaoCao_Nap();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Nap();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            it.CommonCls.ExportExcel(chartControl1);
        }
    }
}