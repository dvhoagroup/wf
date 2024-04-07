using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BEE.ThuVien;

namespace BEEREMA.BaoCao
{
    public partial class rptPhanTichTuoiNo : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPhanTichTuoiNo(DateTime? tuNgay, DateTime? denNgay, int? maDA)
        {
            InitializeComponent();

            #region DataBindings setting
            cellSTT.DataBindings.Add("Text", null, "STT");
            cellTenDA.DataBindings.Add("Text", null, "TenDA");
            cellKyHieu.DataBindings.Add("Text", null, "KyHieu");
            cellHoTenKH.DataBindings.Add("Text", null, "HoTenKH");
            cellSoHD.DataBindings.Add("Text", null, "SoHD");
            cellNgayKy.DataBindings.Add("Text", null, "NgayHD", "{0:dd-MM-yyyy}");
            cellGiaTriHD.DataBindings.Add("Text", null, "GiaTriHD", "{0:#,0.##}");
            cellDaThu.DataBindings.Add("Text", null, "DaThu", "{0:#,0.##}");
            cellConNo.DataBindings.Add("Text", null, "ConNo", "{0:#,0.##}");
            cellQua0.DataBindings.Add("Text", null, "Qua0", "{0:#,0.##}");
            cellQua30.DataBindings.Add("Text", null, "Qua30", "{0:#,0.##}");
            cellQua60.DataBindings.Add("Text", null, "Qua60", "{0:#,0.##}");
            cellQua90.DataBindings.Add("Text", null, "Qua90", "{0:#,0.##}");
            cellQua180.DataBindings.Add("Text", null, "Qua180", "{0:#,0.##}");
            cellQua360.DataBindings.Add("Text", null, "Qua360", "{0:#,0.##}");
            cellQua720.DataBindings.Add("Text", null, "Qua720", "{0:#,0.##}");
            cellTienLai.DataBindings.Add("Text", null, "TienLai", "{0:#,0.##}");
            cellPhaiNop.DataBindings.Add("Text", null, "PhaiNop", "{0:#,0.##}");
            cellSumGiaTriHD.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:n0}");
            cellSumGiaTriHD.DataBindings.Add("Text", null, "GiaTriHD");
            cellSumDaThu.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumDaThu.DataBindings.Add("Text", null, "DaThu", "{0:#,0.##}");
            cellSumConNo.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumConNo.DataBindings.Add("Text", null, "ConNo", "{0:#,0.##}");
            cellSumQua0.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumQua0.DataBindings.Add("Text", null, "Qua0", "{0:#,0.##}");
            cellSumQua30.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumQua30.DataBindings.Add("Text", null, "Qua30", "{0:#,0.##}");
            cellSumQua60.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumQua60.DataBindings.Add("Text", null, "Qua60", "{0:#,0.##}");
            cellSumQua90.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumQua90.DataBindings.Add("Text", null, "Qua90", "{0:#,0.##}");
            cellSumQua180.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumQua180.DataBindings.Add("Text", null, "Qua180", "{0:#,0.##}");
            cellSumQua360.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumQua360.DataBindings.Add("Text", null, "Qua360", "{0:#,0.##}");
            cellSumQua720.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumQua720.DataBindings.Add("Text", null, "Qua720", "{0:#,0.##}");
            cellSumTienLai.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumTienLai.DataBindings.Add("Text", null, "TienLai", "{0:#,0.##}");
            cellSumPhaiNop.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumPhaiNop.DataBindings.Add("Text", null, "PhaiNop", "{0:#,0.##}");
            #endregion

            lblThoiGian.Text = string.Format("(Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy})", tuNgay, denNgay);

            using (var db = new MasterDataContext())
            {
                this.DataSource = db.rptPhanTichTuoiNo(tuNgay, denNgay, maDA);
            }
        }
    }
}
