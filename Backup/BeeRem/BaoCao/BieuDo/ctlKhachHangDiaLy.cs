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
    public partial class ctlKhachHangDiaLy : DevExpress.XtraEditors.XtraUserControl
    {
        public ctlKhachHangDiaLy()
        {
            InitializeComponent();
        }
        List<ItemTinh1> listTinh;
        List<DuAnItem> list;
        byte getIDTuoi(byte? tinhid)
        {
            var dt = listTinh.FirstOrDefault(p => p.ID == tinhid);
            return dt != null ? dt.ID : listTinh.Last().ID;
        }
        private void BaoCao_Nap()
        {
            var wait = DialogBox.WaitingForm();
                    listTinh = new List<ItemTinh1>();
            try
            {
                var maDA = (int?)itemDuAn.EditValue;
                chartControl1.Series.Clear();
                using (var db = new MasterDataContext())
                {
                    var ltMaTinh = (from hd in db.pgcPhieuGiuChos
                                    join kh in db.KhachHangs on hd.MaKH equals kh.MaKH
                                    where kh.MaTinh != null
                                    & hd.bdsSanPham.MaDA == maDA
                                    select new
                                    {
                                        kh.MaTinh
                                    }).ToList();
                    var listSoLuong = (from lt in ltMaTinh
                                       join tinh in db.Tinhs on lt.MaTinh equals tinh.MaTinh
                                       group lt by new {tinh.MaTinh, tinh.TenTinh} into gr
                                       select new
                                       {
                                           MaTinh = gr.Key.MaTinh,
                                           TenTinh = gr.Key.TenTinh,
                                           SoLuong = gr.Count()
                                       }).OrderByDescending(p => p.SoLuong).Take(2).ToList();


                    byte i = 0;
                    foreach (var t in listSoLuong)
                    {
                        listTinh.Add(new ItemTinh1(++i , t.MaTinh, t.TenTinh));
                    }
                        listTinh.Add(new ItemTinh1(++i , 0 , "Others"));

                    var ltKetqua = (from ttt in
                                        (from tkh in ltMaTinh
                                         select new { IDTinh = getIDTuoi(tkh.MaTinh) }).ToList()
                                    join ldt in listTinh on ttt.IDTinh equals ldt.ID
                                    group ttt by new { IDTuoi = ttt.IDTinh, ldt.TenTinh } into gr
                                    select new
                                    {
                                        DoTuoi = gr.Key.TenTinh,
                                        SoLuong = gr.Count()
                                    }).OrderByDescending(p=>p.SoLuong).ToList();
                    var ten = list.Single(p => p.MaDA == maDA).TenDA;

                    var strTitle = "BIỂU ĐỒ KHÁCH HÀNG THEO KHU VỰC";

                    Series series1 = new Series(strTitle, ViewType.Doughnut);
                   
                    // Add points to the series.
                    foreach (var dt in ltKetqua)
                        series1.Points.Add(new SeriesPoint(string.Format("{0}", dt.DoTuoi), dt.SoLuong));

                    // Access labels of the first series.
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
                    // Customize the view-type-specific properties of the series.
                   // DoughnutSeriesView pie3DView = (DoughnutSeriesView)series1.View;
                    //pie3DView. = 10;
                   // pie3DView.SizeAsPercentage = 0;
                   // pie3DView.ExplodeMode = PieExplodeMode.All;

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
            list = new List<DuAnItem>();
            using (var db = new MasterDataContext())
            {
                list = (List<DuAnItem>)db.DuAns.OrderByDescending(p => p.NgayDang).Select(p => new DuAnItem(p.MaDA, p.TenDA)).ToList();
                lookDuAn.DataSource = list;
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

        private void itemDuAn_EditValueChanged(object sender, EventArgs e)
        {
            BaoCao_Nap();
        }
    }
    class ItemTinh1
    {
        public ItemTinh1(byte _STT, byte _ID, string _TenTinh)
        {
            this.STT = _STT;
            this.ID = _ID;
            this.TenTinh = _TenTinh;
        }
        public byte STT;
        public byte ID;
        public string TenTinh;
    }
}