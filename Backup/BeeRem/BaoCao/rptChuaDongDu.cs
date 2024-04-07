using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BEE.ThuVien;

namespace BEEREMA.BaoCao
{
    public partial class rptChuaDongDu : DevExpress.XtraReports.UI.XtraReport
    {
        int STTGroup = 0;
        int STT = 0;

        public rptChuaDongDu(DateTime? start, DateTime? end, int? maDA)
        {
            InitializeComponent();

            lblDate.Text = string.Format(lblDate.Text, start, end);

            DevExpress.XtraReports.UI.XRSummary xrSummaryGroup = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummaryTotal = new DevExpress.XtraReports.UI.XRSummary();

            //cellSTT.DataBindings.Add("Text", null, "STT");
            cellNgayGD.DataBindings.Add("Text", null, "NgayGD", "{0:dd/MM/yyyy}");
            cellKhachHang.DataBindings.Add("Text", null, "HoTenKH");
            cellMaLo.DataBindings.Add("Text", null, "MaLo");
            cellNguoiGioiThieu.DataBindings.Add("Text", null, "NguoiDaiDien");
            cellDienTich.DataBindings.Add("Text", null, "DienTichXD", "{0:#,0.#}");
            cellThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.#}");
            cellSoPhieu.DataBindings.Add("Text", null, "SoPhieu");
            cellGiaBan.DataBindings.Add("Text", null, "DonGiaXD", "{0:#,0.#}");
            cellTongThuDen.DataBindings.Add("Text", null, "DaThu", "{0:#,0.#}");
            cellPhatSinh.DataBindings.Add("Text", null, "PhatSinh", "{0:#,0.#}");
            cellConPhaiThu.DataBindings.Add("Text", null, "ConPhaiThu", "{0:#,0.#}");
            cellConPhaiThuHD.DataBindings.Add("Text", null, "ConLaiHD", "{0:#,0.#}");

            cellGroupProject.DataBindings.Add("Text", null, "TenDA", "{0}");
            this.groupHeader.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("TenDA", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});

            xrSummaryGroup.FormatString = "      Cộng: {0:n0}";
            xrSummaryGroup.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            xrSummaryGroup.Func = SummaryFunc.Count;
            cellGroupFooter.Summary = xrSummaryGroup;

            xrSummaryTotal.FormatString = "Tổng cộng: {0:n0}";
            xrSummaryTotal.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            xrSummaryTotal.Func = SummaryFunc.Count;
            cellTotal.Summary = xrSummaryTotal;

            using (var db = new MasterDataContext())
            {
                db.CommandTimeout = 300;
                this.DataSource = db.rptChuaDongDu(start, end, maDA);
            }
        }

        private void cellGroupProject_EvaluateBinding(object sender, BindingEventArgs e)
        {
            STTGroup++;
            STT = 0;

            cellSTTGroup.Text = Common.ToRoman(STTGroup);
        }

        private void cellMaLo_EvaluateBinding(object sender, BindingEventArgs e)
        {
            STT++;
            cellSTT.Text = STT.ToString();
        }
    }
}
