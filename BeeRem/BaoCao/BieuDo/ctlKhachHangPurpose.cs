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
using DIPCRM.Support;

namespace BEE.CongCu.Report
{
    public partial class ctlKhachHangPurpose : DevExpress.XtraEditors.XtraUserControl
    {
        public ctlKhachHangPurpose()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this);
        }

        private void BaoCao_Nap()
        {
            //var wait = DialogBox.WaitingForm();
            try
            {
                chartControl1.Series.Clear();
                using (var db = new MasterDataContext())
                {
                    var ltHTK = (from kh in db.KhachHangs
                                 join p in db.cdPurposes on kh.PurposeID equals p.ID
                                 group p by new { p.ID, p.Name} into gr
                                   select new 
                                   {
                                       Name = gr.Key.Name,
                                       SoLuong = gr.Count()
                                   }).ToList();
                                        
                    //var ten = list.Single(p => p.MaDA == maDA).TenDA;

                    var strTitle = "Chart Of Customers By Purpose";

                    Series series1 = new Series(strTitle, ViewType.Doughnut);

                    // Add points to the series.
                    foreach (var dt in ltHTK)
                        series1.Points.Add(new SeriesPoint(string.Format("{0} ({1})", dt.Name, dt.SoLuong), dt.SoLuong));

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

                    //((SimpleDiagram3D)chartControl1.Diagram).RuntimeRotation = true;
                    //// Customize the view-type-specific properties of the series.
                    //DoughnutSeriesView pie3DView = (DoughnutSeriesView)series1.View;
                    ////pie3DView. = 10;
                    //pie3DView.SizeAsPercentage = 100;
                    //pie3DView.ExplodeMode = PieExplodeMode.All;

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
               // wait.Close();
            }
        }

        private void ctlKhachHangPurpose_Load(object sender, EventArgs e)
        {
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

        private void itemDuAn_EditValueChanged(object sender, EventArgs e)
        {
            BaoCao_Nap();
        }
    }
}