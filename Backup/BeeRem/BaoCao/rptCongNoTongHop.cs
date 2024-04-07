using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BEE.ThuVien;

namespace BEEREMA.BaoCao
{
    public partial class rptCongNoTongHop : DevExpress.XtraReports.UI.XtraReport
    {
        public rptCongNoTongHop(DateTime? denNgay)
        {
            InitializeComponent();

            #region DataBindings setting
            cellSTT.DataBindings.Add("Text", null, "STT");
            cellHoTenKH.DataBindings.Add("Text", null, "HoTenKH");
            cellGiaTriHD.DataBindings.Add("Text", null, "GiaTriHD", "{0:#,0.##}");
            cellDauKy.DataBindings.Add("Text", null, "DauKy", "{0:#,0.##}");
            cellPhatSinh.DataBindings.Add("Text", null, "PhatSinh", "{0:#,0.##}");
            cellDaThu.DataBindings.Add("Text", null, "DaThu", "{0:#,0.##}");
            cellChietKhau.DataBindings.Add("Text", null, "ChietKhau", "{0:#,0.##}");
            cellTienPhat.DataBindings.Add("Text", null, "TienPhat", "{0:#,0.##}");
            cellConNo.DataBindings.Add("Text", null, "ConNo", "{0:#,0.##}");
            cellTrongHan.DataBindings.Add("Text", null, "TrongHan", "{0:#,0.##}");
            cellQua0.DataBindings.Add("Text", null, "Qua0", "{0:#,0.##}");
            cellQua7.DataBindings.Add("Text", null, "Qua7", "{0:#,0.##}");
            cellQua14.DataBindings.Add("Text", null, "Qua14", "{0:#,0.##}");
            cellQua30.DataBindings.Add("Text", null, "Qua30", "{0:#,0.##}");
            cellQua45.DataBindings.Add("Text", null, "Qua45", "{0:#,0.##}");
            cellQua60.DataBindings.Add("Text", null, "Qua60", "{0:#,0.##}");
            cellQua90.DataBindings.Add("Text", null, "Qua90", "{0:#,0.##}");
            cellSoNgay.DataBindings.Add("Text", null, "SoNgay", "{0:#,0.##}");
            cellSumDauKy.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:n0}");
            cellSumDauKy.DataBindings.Add("Text", null, "DauKy", "{0:#,0.##}");
            cellSumPhatSinh.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumPhatSinh.DataBindings.Add("Text", null, "PhatSinh", "{0:#,0.##}");
            cellSumDaThu.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumDaThu.DataBindings.Add("Text", null, "DaThu", "{0:#,0.##}");
            cellSumChietKhau.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumChietKhau.DataBindings.Add("Text", null, "ChietKhau", "{0:#,0.##}");
            cellSumTienPhat.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumTienPhat.DataBindings.Add("Text", null, "TienPhat", "{0:#,0.##}");
            cellSumConNo.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumConNo.DataBindings.Add("Text", null, "ConNo", "{0:#,0.##}");
            cellSumTrongHan.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumTrongHan.DataBindings.Add("Text", null, "TrongHan", "{0:#,0.##}");
            cellSumQua0.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumQua0.DataBindings.Add("Text", null, "Qua0", "{0:#,0.##}");
            cellSumQua7.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumQua7.DataBindings.Add("Text", null, "Qua7", "{0:#,0.##}");
            cellSumQua14.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumQua14.DataBindings.Add("Text", null, "Qua14", "{0:#,0.##}");
            cellSumQua30.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumQua30.DataBindings.Add("Text", null, "Qua30", "{0:#,0.##}");
            cellSumQua45.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumQua45.DataBindings.Add("Text", null, "Qua45", "{0:#,0.##}");
            cellSumQua60.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumQua60.DataBindings.Add("Text", null, "Qua60", "{0:#,0.##}");
            cellSumQua90.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumQua90.DataBindings.Add("Text", null, "Qua90", "{0:#,0.##}");
            #endregion

            lblTitle.Text = string.Format("MẪU BÁO CÁO QUẢN LÝ CÔNG NỢ (TỔNG HỢP) ĐẾN NGÀY {0:dd/MM/yyyy}", denNgay);

            using (var db = new MasterDataContext())
            {
                this.DataSource = db.rptCongNoTongHop(denNgay);
            }
        }
    }
}
