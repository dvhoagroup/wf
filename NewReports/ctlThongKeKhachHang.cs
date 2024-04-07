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

namespace NewReports
{
    public partial class ctlThongKeKhachHang : DevExpress.XtraEditors.XtraUserControl
    {
        bool KT = false;
        bool KT1 = false;
        List<ItemNhomKD> ListNKD = new List<ItemNhomKD>();
        public ctlThongKeKhachHang()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this);
        }
        
        void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void BaoCao_Nap()
        {
            var wait =  DialogBox.WaitingForm();
            try
            {
                if (itemNhuCau.EditValue == null)
                    return;

                var NhuCau = itemNhuCau.EditValue.ToString();
                var strTitle = string.Format("Thống kê khách có nhu cầu {0}", NhuCau);
                var tuNgay = (DateTime?)itemTuNgay.EditValue ?? DateTime.Now;
                var denNgay = (DateTime?)itemDenNgay.EditValue ?? DateTime.Now;
                var maNKD = itemNhomKD.EditValue == null ? 0 : Convert.ToByte(itemNhomKD.EditValue);

                chartControl1.Series.Clear();

                using (var db = new MasterDataContext())
                {
                    var ltMua = db.mglmtMuaThues.Where(p => (p.NhanVien.MaNKD == maNKD || maNKD == 0) && SqlMethods.DateDiffDay(p.NgayDK, tuNgay) <= 0 && SqlMethods.DateDiffDay(p.NgayDK, denNgay) >= 0).Select(p => new { p.NhanVien.HoTen }).ToList().GroupBy(p => p.HoTen).Select(
                        p => new
                        {
                            HoTen = p.Key,
                            GiaoDich = p.Count()
                        }).OrderByDescending(p => p.GiaoDich).ToList();
                    var ltBan = db.mglbcBanChoThues.Where(p => (p.NhanVien.MaNKD == maNKD || maNKD == 0) && SqlMethods.DateDiffDay(p.NgayDK, tuNgay) <= 0 && SqlMethods.DateDiffDay(p.NgayDK, denNgay) >= 0).Select(p => new { p.NhanVien.HoTen }).ToList().GroupBy(p => p.HoTen).Select(
                        p => new
                        {
                            HoTen = p.Key,
                            GiaoDich = p.Count()
                        }).OrderByDescending(p => p.GiaoDich).ToList();                 

                    Series series1 = new Series("SL", ViewType.Bar);
                    Series series2 = new Series("Nhân viên", ViewType.Line);


                    // Add points to the series.
                    Object obj = NhuCau != "Cần bán/cho thuê" ? ltMua : ltBan;
                    if (NhuCau != "Cần bán/cho thuê")
                        foreach (var l in ltMua)
                        {
                            series1.Points.Add(new SeriesPoint(string.Format("{0}", l.HoTen), l.GiaoDich));
                            series2.Points.Add(new SeriesPoint(string.Format("{0}", l.HoTen), l.GiaoDich));
                        }
                    else
                        foreach (var l in ltBan)
                        {
                            series1.Points.Add(new SeriesPoint(string.Format("{0}", l.HoTen), l.GiaoDich));
                            series2.Points.Add(new SeriesPoint(string.Format("{0}", l.HoTen), l.GiaoDich));
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
                    diagram.AxisY.Title.Text = "Giao dịch";
                    diagram.AxisY.Title.Visible = false;
                    diagram.AxisX.Visible = true;
                    diagram.AxisX.Label.Angle = 0;
                    diagram.AxisX.Label.Antialiasing = true;
                    diagram.AxisX.Label.Staggered = true;
                    diagram.AxisY.Title.Font = new Font("Tahoma", 14);

                    // Add a title to the chart and hide the legend.
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

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Nap();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            it.CommonCls.ExportExcel(chartControl1);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            BaoCao_Nap();
        }

        private void ctlThongKeGiaoDich_Load(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                //  var ListNKD = new List<ItemNhomKD>();
                var nkd = new ItemNhomKD() { MaNKD = Convert.ToByte(0), TenNKD = "<Tất cả>" };
                ListNKD.Add(nkd);
                foreach (var p in db.NhomKinhDoanhs)
                {
                    var obj = new ItemNhomKD() { MaNKD = p.MaNKD, TenNKD = p.TenNKD };
                    ListNKD.Add(obj);
                }
                lookNhomKD.DataSource = ListNKD;
                itemNhomKD.EditValue = Common.GroupID;
            }
            itemNhuCau.EditValue = "Cần mua/cần thuê";
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(4);

            BaoCao_Nap();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            BaoCao_Nap();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemNhuCau_EditValueChanged(object sender, EventArgs e)
        {
            BaoCao_Nap();
        }
    }

    public class ItemNhomKD
    {
        public byte? MaNKD { get; set; }
        public string TenNKD { get; set; }
    }
}