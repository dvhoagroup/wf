using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BEE.ThuVien;

namespace BEEREMA.BaoCao
{
    public partial class rptDoanhSoBanHang : DevExpress.XtraReports.UI.XtraReport
    {
        int STTGroup = 0;
        int STT = 0;

        public rptDoanhSoBanHang(DateTime? start, DateTime? end)
        {
            InitializeComponent();

            lblDate.Text = string.Format(lblDate.Text, start, end);

            DevExpress.XtraReports.UI.XRSummary xrSummaryGroup = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummaryTotal = new DevExpress.XtraReports.UI.XRSummary();

            //cellSTT.DataBindings.Add("Text", null, "STT");
            cellBlock.DataBindings.Add("Text", null, "BlockName");
            cellDCLL.DataBindings.Add("Text", null, "DCLL");
            cellDCTT.DataBindings.Add("Text", null, "DCTT");
            cellDienTich.DataBindings.Add("Text", null, "DTSD", "{0:#,0.#}");
            cellKieu.DataBindings.Add("Text", null, "LoaiCH");
            cellKhachHang.DataBindings.Add("Text", null, "HoTenKH");
            cellMaLo.DataBindings.Add("Text", null, "MaLo");
            cellNgayCoc.DataBindings.Add("Text", null, "NgayKy", "{0:dd}");
            cellNgayTT1.DataBindings.Add("Text", null, "DateBatch1", "{0:dd/MM/yyyy}");
            cellNgayTT2.DataBindings.Add("Text", null, "DateBatch2", "{0:dd/MM/yyyy}");
            cellNgayTT3.DataBindings.Add("Text", null, "DateBatch3", "{0:dd/MM/yyyy}");
            cellSoCMND.DataBindings.Add("Text", null, "SoCMND");
            cellSoLuong.DataBindings.Add("Text", null, "SoLuong");
            cellTang.DataBindings.Add("Text", null, "TenTangNha");
            cellThangCoc.DataBindings.Add("Text", null, "NgayKy", "{0:MM}");

            cellGroupHeader.DataBindings.Add("Text", null, "TenDA", "{0}");
            this.groupHeader.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("TenDA", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});

            xrSummaryGroup.FormatString = "{0:n0}";
            xrSummaryGroup.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            xrSummaryGroup.Func = SummaryFunc.Count;
            cellSoLuongSumGroup.Summary = xrSummaryGroup;

            xrSummaryTotal.FormatString = "{0:n0}";
            xrSummaryTotal.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            xrSummaryTotal.Func = SummaryFunc.Count;
            cellSoLuongSum.Summary = xrSummaryTotal;

            using (var db = new MasterDataContext())
            {
                db.CommandTimeout = 300;
                this.DataSource = db.rptDoanhSoBanHang(start, end);
            }
        }

        private void cellGroupHeader_EvaluateBinding(object sender, BindingEventArgs e)
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
