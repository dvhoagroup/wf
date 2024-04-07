using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BEE.ThuVien;

namespace BEEREMA.BaoCao
{
    public partial class rptTongHopCongNo : DevExpress.XtraReports.UI.XtraReport
    {
        int STTGroup = 0;
        int STT1 = 0;
        public rptTongHopCongNo(DateTime? tuNgay, DateTime? denNgay, int? maDA)
        {
            InitializeComponent();
            DevExpress.XtraReports.UI.XRSummary xrSummaryGroup = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummaryTotal = new DevExpress.XtraReports.UI.XRSummary();

            #region DataBindings setting
            //cellSTT.DataBindings.Add("Text", null, "STT1");
            cSoHD.DataBindings.Add("Text", null, "SoHDMB");
            cMaCanHo.DataBindings.Add("Text", null, "KyHieu");
            cellHoTenKH.DataBindings.Add("Text", null, "HoTenKH");
            cNgayKy.DataBindings.Add("Text", null, "NgayKy", "{0:dd/MM/yyyy}");
            cGiaTriHD.DataBindings.Add("Text", null, "TongGiaTriHD", "{0:#,0.##}");
            cPhaiThu.DataBindings.Add("Text", null, "TongGiaTriHD", "{0:#,0.##}");
            cDaThu.DataBindings.Add("Text", null, "DaThu", "{0:#,0.##}");
            cConLai.DataBindings.Add("Text", null, "ConLai", "{0:#,0.##}");
            cSoTienNoDot.DataBindings.Add("Text", null, "SoTienNoDot", "{0:#,0.##}");
            cSoDotNo.DataBindings.Add("Text", null, "SoDotNo");
            cLoaiCongNo.DataBindings.Add("Text", null, "TenLN");

            cSumPhaiThu.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumPhaiThu.DataBindings.Add("Text", null, "TongGiaTriHD", "{0:#,0.##}");
            cGroupTongPhaiThu.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,0.##}");
            cGroupTongPhaiThu.DataBindings.Add("Text", null, "TongGiaTriHD", "{0:#,0.##}");

            cSumTongGiaTriHD.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumTongGiaTriHD.DataBindings.Add("Text", null, "TongGiaTriHD", "{0:#,0.##}");
            cGroupTongGiaTriHD.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,0.##}");
            cGroupTongGiaTriHD.DataBindings.Add("Text", null, "TongGiaTriHD", "{0:#,0.##}");

            cellSumDaThu.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumDaThu.DataBindings.Add("Text", null, "DaThu", "{0:#,0.##}");
            cGroupTongDaThu.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,0.##}");
            cGroupTongDaThu.DataBindings.Add("Text", null, "DaThu", "{0:#,0.##}");

            cellSumConLai.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumConLai.DataBindings.Add("Text", null, "ConLai", "{0:#,0.##}");
            cGroupTongConLai.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,0.##}");
            cGroupTongConLai.DataBindings.Add("Text", null, "ConLai", "{0:#,0.##}");

            cellSumSoTienNoDot.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cellSumSoTienNoDot.DataBindings.Add("Text", null, "SoTienNoDot", "{0:#,0.##}");
            cGroupSoTienNoDot.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,0.##}");
            cGroupSoTienNoDot.DataBindings.Add("Text", null, "SoTienNoDot", "{0:#,0.##}");


            cGroupProject.DataBindings.Add("Text", null, "TenDA", "{0}");
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("TenDA", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});

            //xrSummaryGroup.FormatString = "      Cộng: {0:n0}";
            //xrSummaryGroup.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            //xrSummaryGroup.Func = SummaryFunc.Sum;
            //cGroupTongGiaTriHD.Summary = xrSummaryGroup;

            //xrSummaryTotal.FormatString = "Tổng cộng: {0:n0}";
            //xrSummaryTotal.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            //xrSummaryTotal.Func = SummaryFunc.Count;
            //xrTableCell11.Summary = xrSummaryTotal;


            #endregion

            lblTitle.Text = string.Format("MẪU BÁO CÁO QUẢN LÝ CÔNG NỢ (TỔNG HỢP) TỪ NGÀY {0:dd/MM/yyyy} ĐẾN NGÀY {0:dd/MM/yyyy}",tuNgay, denNgay);

            using (var db = new MasterDataContext())
            {
                this.DataSource = db.HopDongMuaBan_LoadCongNo(tuNgay, denNgay, maDA);
            }
        }

        private void cGroupProject_EvaluateBinding(object sender, BindingEventArgs e)
        {
            STTGroup++;
            STT1 = 0;

            cSTTGroup.Text = Common.ToRoman(STTGroup);
        }

        private void cMaCanHo_EvaluateBinding(object sender, BindingEventArgs e)
        {
            STT1++;
            cellSTT.Text = STT1.ToString();
        }
    }
}
