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
using BEEREMA;

namespace BEE.CongCu.Report
{
    public partial class ctlKhachHangDoTuoi : DevExpress.XtraEditors.XtraUserControl
    {
        public ctlKhachHangDoTuoi()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this);
        }

        List<ItemDotuoi> listDoTuoi;
        List<DuAnItem> list;
        int getIDTuoi(int tuoi)
        {
            var dt = listDoTuoi.FirstOrDefault(p => p.tuoifrom <= tuoi & p.tuoito > tuoi);
            return dt != null ? dt.ID : listDoTuoi.First().ID;
        }

        private void BaoCao_Nap()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var maDA = (int?)itemDuAn.EditValue;
                BEEREMA.Properties.Settings.Default.ProjectID = maDA.Value;
                BEEREMA.Properties.Settings.Default.Save();
                chartControl1.Series.Clear();
                using (var db = new MasterDataContext())
                {
                    var ltDoTuoi = (from ttt in
                                        (from tkh in
                                             (from hd in db.pgcPhieuGiuChos
                                              join kh in db.KhachHangs on hd.MaKH equals kh.MaKH
                                              where kh.NgaySinh != null
                                              & hd.bdsSanPham.MaDA == maDA
                                              select new
                                              {
                                                  TuoiKH = (int)SqlMethods.DateDiffYear(kh.NgaySinh, DateTime.Now)
                                              })
                                         select new { IDTuoi = getIDTuoi(tkh.TuoiKH) }).ToList()
                                    join ldt in listDoTuoi on ttt.IDTuoi equals ldt.ID
                                    group ttt by new { ttt.IDTuoi, ldt.text } into gr
                                    select new
                                    {
                                        DoTuoi = gr.Key.text,
                                        SoLuong = gr.Count()
                                    }).ToList();
                    var ten = list.Single(p => p.MaDA == maDA).TenDA;

                    var strTitle = "Chart Of Customers By Age";

                    Series series1 = new Series(strTitle, ViewType.Doughnut);

                    // Add points to the series.
                    foreach (var dt in ltDoTuoi)
                        series1.Points.Add(new SeriesPoint(string.Format("{0} ({1})", dt.DoTuoi, dt.SoLuong), dt.SoLuong));

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
                wait.Close();
            }
        }

        private void ctlDoanhSoTheoDuAn_Load(object sender, EventArgs e)
        {
            listDoTuoi = new List<ItemDotuoi>();
            list = new List<DuAnItem>();
            using (var db = new MasterDataContext())
            {
                list = (List<DuAnItem>)db.DuAns.OrderByDescending(p => p.NgayDang).Select(p => new DuAnItem(p.MaDA, p.TenDA)).ToList();
                lookDuAn.DataSource = list;
            }

            listDoTuoi.Add(new ItemDotuoi(1, 17, 25, "17-25"));
            listDoTuoi.Add(new ItemDotuoi(2, 26, 35, "25-35"));
            listDoTuoi.Add(new ItemDotuoi(3, 36, 50, "35-50"));
            listDoTuoi.Add(new ItemDotuoi(4, 51, 60, "50-60"));
            listDoTuoi.Add(new ItemDotuoi(5, 61, 120, "60-over"));

            itemDuAn.EditValue = BEEREMA.Properties.Settings.Default.ProjectID;
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
    class ItemDotuoi
    {
        public ItemDotuoi(int _ID, int _from, int _to, string _text)
        {
            this.ID = _ID;
            this.tuoifrom = _from;
            this.tuoito = _to;
            this.text = _text;
        }
        public int ID;
        public int tuoifrom;
        public int tuoito;
        public string text;
    }
}