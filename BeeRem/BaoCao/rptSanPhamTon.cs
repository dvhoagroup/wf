using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BEE.ThuVien;

namespace BEEREMA.BaoCao
{
    public partial class rptSanPhamTon : DevExpress.XtraReports.UI.XtraReport
    {
        public rptSanPhamTon(DateTime? tuNgay, DateTime? denNgay)
        {
            InitializeComponent();

            #region DataBindings setting
            cellSTT.DataBindings.Add("Text", null, "STT");
            cellTenDA.DataBindings.Add("Text", null, "TenDA");
            cellTongSL.DataBindings.Add("Text", null, "TongSL", "{0:#,0.##}");
            cellTongDT.DataBindings.Add("Text", null, "TongDT", "{0:#,0.##}");
            cellBanSL.DataBindings.Add("Text", null, "BanSL", "{0:#,0.##}");
            cellBanDT.DataBindings.Add("Text", null, "BanDT", "{0:#,0.##}");
            cellBanGiaTri.DataBindings.Add("Text", null, "BanGiaTri", "{0:#,0.##}");
            cellBanDaThu.DataBindings.Add("Text", null, "BanDaThu", "{0:#,0.##}");
            cellBanPhaiThu.DataBindings.Add("Text", null, "BanPhaiThu", "{0:#,0.##}");
            cellTonSL.DataBindings.Add("Text", null, "TonSL", "{0:#,0.##}");
            cellTonDT.DataBindings.Add("Text", null, "TonDT", "{0:#,0.##}");
            cellSumTongSL.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:n0}");
            cellSumTongSL.DataBindings.Add("Text", null, "TongSL");
            cellSumTongDT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumTongDT.DataBindings.Add("Text", null, "TongDT", "{0:#,0.##}");
            cellSumBanSL.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumBanSL.DataBindings.Add("Text", null, "BanSL", "{0:#,0.##}");
            cellSumBanDT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumBanDT.DataBindings.Add("Text", null, "BanDT", "{0:#,0.##}");
            cellSumBanGiaTri.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumBanGiaTri.DataBindings.Add("Text", null, "BanGiaTri", "{0:#,0.##}");
            cellSumBanDaThu.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumBanDaThu.DataBindings.Add("Text", null, "BanDaThu", "{0:#,0.##}");
            cellSumBanPhaiThu.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumBanPhaiThu.DataBindings.Add("Text", null, "BanPhaiThu", "{0:#,0.##}");
            cellSumTonSL.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumTonSL.DataBindings.Add("Text", null, "TonSL", "{0:#,0.##}");
            cellSumTonDT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumTonDT.DataBindings.Add("Text", null, "TonDT", "{0:#,0.##}");
            #endregion

            using (var db = new MasterDataContext())
            {
                this.DataSource = db.rptSanPhamTon(tuNgay, denNgay);
            }
        }
    }
}
