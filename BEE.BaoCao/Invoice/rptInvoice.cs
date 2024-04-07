using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using BEE.ThuVien;

namespace BEE.BaoCao.Invoice
{
    public partial class rptInvoice : DevExpress.XtraReports.UI.XtraReport
    {
        public rptInvoice(int InvoiceID)
        {
            InitializeComponent();

            var rpt = new rptInvoiceDetail(InvoiceID);
            subReport.ReportSource = rpt;

            using (var db = new MasterDataContext())
            {
                try
                {
                    var objIn = db.Invoices.Single(p => p.ID == InvoiceID);
                    lblAddress.Text = objIn.KhachHang.DCTT;
                    lblCode.Text = objIn.KhachHang.IsPersonal.Value ? objIn.KhachHang.MaSoThueCT : objIn.KhachHang.MaSoTTNCN;
                    lblCompany.Text = objIn.KhachHang.IsPersonal.Value ? "" : objIn.KhachHang.TenCongTy;
                    lblDateOut.Text = string.Format("{0:dd}                  {0:MM}              {0:yyyy}", objIn.DateOut);
                    lblBank.Text = "";
                    lblGrandTotal.Text = string.Format("{0:#,0.#}", objIn.GrandTotal);
                    lblGrandTotalText.Text = it.ConvertMoney.ToString((double)objIn.GrandTotal.Value);
                    lblMethod.Text = objIn.PaymentMethod.Value ? "TM" : "CK";
                    lblPersonal.Text = objIn.KhachHang.IsPersonal.Value ? objIn.KhachHang.HoTenKH : "";
                    lblSubTotal.Text = string.Format("{0:#,0.#}", objIn.SubTotal);
                    lblVAT.Text = string.Format("{0:#,0.#}", objIn.VAT);
                    lblVATRate.Text = string.Format("{0:#,0.#}", objIn.VATRate);

                    Detail.HeightF = 400f;
                }
                catch { }
            }
        }
    }
}
