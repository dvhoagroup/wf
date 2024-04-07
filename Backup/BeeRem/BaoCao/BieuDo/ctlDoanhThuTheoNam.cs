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
    public partial class ctlDoanhThuTheoNam : DevExpress.XtraEditors.XtraUserControl
    {
        public ctlDoanhThuTheoNam()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this);
        }

        private void BaoCao_Nap()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var year = (int?)itemYear.EditValue;

                chartControl1.Series.Clear();

                using (var db = new MasterDataContext())
                {
                    var ltSource = (from th in db.tblThangs
                                    join pt in db.pgcPhieuThus.Where(p => p.NgayThu.GetValueOrDefault().Year == year) 
                                        on th.Thang equals pt.NgayThu.GetValueOrDefault().Month into p
                                    from pt in p.DefaultIfEmpty()
                                    group pt by new { th.Thang } into gr
                                    select new
                                    {
                                        gr.Key.Thang,
                                        SoTien = gr.Sum(p => p.SoTien/1000000)
                                    }).OrderBy(p => p.Thang).ToList();

                    

                    Series series1 = new Series("Month", ViewType.Bar);
                    Series series2 = new Series("Revenue", ViewType.Line);

                    // Add points to the series.
                    foreach (var l in ltSource)
                    {
                        series1.Points.Add(new SeriesPoint(string.Format("Month {0}", l.Thang), l.SoTien.GetValueOrDefault()));
                        series2.Points.Add(new SeriesPoint(string.Format("Month {0}", l.Thang), l.SoTien.GetValueOrDefault()));
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
                    myView.Transparency = 50;
                    myView.BarWidth = 0.4;
                    myView.Color = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(162)))), ((int)(((byte)(212)))));
                    myView.FillStyle.FillMode = FillMode.Solid;

                    PointSeriesView myView2 = (PointSeriesView)series2.View;
                    myView2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(212)))), ((int)(((byte)(58)))));
                    
                    //Diagram
                    XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
                    diagram.AxisY.Title.Text = "Revenue";
                    diagram.AxisY.Title.Visible = false;
                    diagram.AxisX.Visible = true;
                    diagram.AxisX.Label.Angle = 0;
                    diagram.AxisX.Label.Antialiasing = true;
                    diagram.AxisX.Label.Staggered = true;
                    diagram.AxisY.Title.Font = new Font("Tahoma", 18);

                    // Add a title to the chart and hide the legend.
                    var strTitle = string.Format("Chart Of Revenue {0}", year);
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

        private void ctlDoanhThuTheoNam_Load(object sender, EventArgs e)
        {
            for (int i = DateTime.Now.Year; i >= 2000; i--)
                cmbYear.Items.Add(i);
            itemYear.EditValue = DateTime.Now.Year;

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