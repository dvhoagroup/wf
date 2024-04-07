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
    public partial class ctlDoanhSoTheoDuAn : DevExpress.XtraEditors.XtraUserControl
    {
        public ctlDoanhSoTheoDuAn()
        {
            InitializeComponent();
        }

        private void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        private void BaoCao_Nap()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var tuNgay = (DateTime?)itemTuNgay.EditValue;
                var denNgay = (DateTime?)itemDenNgay.EditValue;

                chartControl1.Series.Clear();

                using (var db = new MasterDataContext())
                {
                    var ltDuAn = (from gc in db.pgcPhieuGiuChos
                                  where SqlMethods.DateDiffDay(tuNgay, gc.NgayGD) >= 0 && SqlMethods.DateDiffDay(gc.NgayGD, denNgay) >= 0 && gc.bdsSanPham.MaTT > 2
                                  join da in db.DuAns on gc.bdsSanPham.MaDA equals da.MaDA
                                  group gc by new { da.MaDA, da.TenDA } into gr
                                  select new { gr.Key.TenDA, SoLuong = gr.Count() }).ToList();

                    var strTitle = string.Format("BIỂU ĐỒ DOANH SỐ THEO DỰ ÁN TỪ NGÀY {0:dd/MM/yyyy} ĐẾN NGÀY {1:dd/MM/yyyy}", tuNgay, denNgay);

                    Series series1 = new Series(strTitle, ViewType.Pie3D);

                    // Add points to the series.
                    foreach (var da in ltDuAn)
                        series1.Points.Add(new SeriesPoint(string.Format("{0}; SL: {1:#,0.##}", da.TenDA, da.SoLuong), da.SoLuong));

                    // Add both series to the chart.
                    chartControl1.Series.AddRange(new Series[] { series1 });

                    // Access labels of the first series.
                    ((Pie3DSeriesLabel)series1.Label).Visible = true;
                    ((Pie3DSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

                    // Access the series options.
                    series1.PointOptions.PointView = PointView.ArgumentAndValues;
                    ((SimpleDiagram3D)chartControl1.Diagram).RuntimeRotation = true;

                    // Customize the view-type-specific properties of the series.
                    Pie3DSeriesView pie3DView = (Pie3DSeriesView)series1.View;
                    pie3DView.Depth = 10;
                    pie3DView.SizeAsPercentage = 100;
                    pie3DView.ExplodeMode = PieExplodeMode.All;

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

                    // Add the chart to the form.
                    chartControl1.Series[0].PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    chartControl1.Series[0].PointOptions.ValueNumericOptions.Precision = 0;
                }
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        private void ctlDoanhSoTheoDuAn_Load(object sender, EventArgs e)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(0);

            BaoCao_Nap();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
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