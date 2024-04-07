using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BEE.ThuVien;

namespace BEEREMA.BaoCao
{
    public partial class rptDanhSachLoTrong : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDanhSachLoTrong(DateTime? end)
        {
            InitializeComponent();

            DevExpress.XtraReports.UI.XRSummary xrSummaryGroup = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummaryTotal = new DevExpress.XtraReports.UI.XRSummary();

            cellSTT.DataBindings.Add("Text", null, "STT", "{0}");
            cellBlock.DataBindings.Add("Text", null, "BlockName", "{0}");
            cellThanhTien.DataBindings.Add("Text", null, "ThanhTienXD", "{0:#,0.#}");
            cellDonGia.DataBindings.Add("Text", null, "DonGiaXD", "{0:#,0.#}");
            cellDienTich.DataBindings.Add("Text", null, "DienTichXD", "{0:#,0.#}");
            cellKieu.DataBindings.Add("Text", null, "LoaiCH", "{0}");
            cellKhachHang.DataBindings.Add("Text", null, "HoTenKH", "{0}");
            cellMaLo.DataBindings.Add("Text", null, "MaLo", "{0}");
            cellTinhTrang.DataBindings.Add("Text", null, "TinhTrang", "{0}");
            cellNgayTT.DataBindings.Add("Text", null, "NgayTT", "{0:dd/MM/yyyy}");
            cellTang.DataBindings.Add("Text", null, "TenTangNha", "{0}");
            cellNgayHopDong.DataBindings.Add("Text", null, "NgayKy", "{0}");
            cellHuong.DataBindings.Add("Text", null, "TenPhuongHuong", "{0}");
            cellSoHopDong.DataBindings.Add("Text", null, "SoPhieu", "{0}");

            cellStatus.DataBindings.Add("Text", null, "GroupStatus", "{0}");
            this.groupStatus.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("MaTT", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});

            cellProject.DataBindings.Add("Text", null, "TenDA", "{0}");
            this.groupProject.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("TenDA", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});

            xrSummaryGroup.FormatString = "Cộng (a+b+c+d): {0:n0}";
            xrSummaryGroup.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            xrSummaryGroup.Func = SummaryFunc.Count;
            cellGroupSummary2.Summary = xrSummaryGroup;

            xrSummaryTotal.FormatString = "Tổng cộng: {0:n0}";
            xrSummaryTotal.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            xrSummaryTotal.Func = SummaryFunc.Count;
            cellTotal.Summary = xrSummaryTotal;

            using (var db = new MasterDataContext())
            {
                //db.CommandTimeout = 300;
                this.DataSource = db.rptDanhSachTonKho();
            }
        }
    }
}
