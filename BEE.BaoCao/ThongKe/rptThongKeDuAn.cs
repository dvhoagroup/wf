using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BEE.ThuVien;
using System.Linq;
using System.Data.Linq;

namespace BEE.BaoCao.ThongKe
{
    public partial class rptThongKeDuAn : DevExpress.XtraReports.UI.XtraReport
    {
        public rptThongKeDuAn(int? MaDA, string MaCT, DateTime? tungay, DateTime? denngay)
        {
            InitializeComponent();
            cSTT.DataBindings.Add("Text", null, "STT");
            cTenCongTrinh.DataBindings.Add("Text", null, "TenKhu");
            cTongSP.DataBindings.Add("Text", null, "TongSP");
            cDaKyDenNgay.DataBindings.Add("Text", null, "DaKyDauKy");
            cDatCocTrongKy.DataBindings.Add("Text", null, "DatCocKy");
            cDaKyTrongKy.DataBindings.Add("Text", null, "DaKyTrongKy");
            cGiuChoTrongKy.DataBindings.Add("Text", null, "GiuChoTrongKy");

            cSumTongSP.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:n0}");
            cSumTongSP.DataBindings.Add("Text", null, "TongSP");

            cSumDaKyDenNgay.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:n0}");
            cSumDaKyDenNgay.DataBindings.Add("Text", null, "DaKyDauKy");

            cSumDatCocTrongKy.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:n0}");
            cSumDatCocTrongKy.DataBindings.Add("Text", null, "DatCocKy");

            cSumDaKyTrongKy.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:n0}");
            cSumDaKyTrongKy.DataBindings.Add("Text", null, "DaKyTrongKy");

            cSumGiuChoTrongKy.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:n0}");
            cSumGiuChoTrongKy.DataBindings.Add("Text", null, "GiuChoTrongKy");

            try
            {
                lbNgay.Text = String.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", tungay, denngay);
                using (var db = new MasterDataContext())
                {
                    var list = db.getReportTongHopDuAn(MaDA, tungay, denngay, ","+MaCT+",").ToList();
                    var duan = db.DuAns.Single(p => p.MaDA == MaDA);
                    cTenDuAn.Text = duan.TenDA;
                    this.DataSource = list;
                }
            }
            catch
            { }
        }

    }
}
