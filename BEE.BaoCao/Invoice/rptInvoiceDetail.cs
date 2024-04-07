using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using BEE.ThuVien;

namespace BEE.BaoCao.Invoice
{
    public partial class rptInvoiceDetail : DevExpress.XtraReports.UI.XtraReport
    {
        public rptInvoiceDetail(int InvoiceID)
        {
            InitializeComponent();

            cellSTT.DataBindings.Add("Text", null, "No");
            cellDescrition.DataBindings.Add("Text", null, "Description");
            cellAmount.DataBindings.Add("Text", null, "Amount", "{0:#,0.#}");
            try
            {
                using (var db = new MasterDataContext())
                {
                    var data = db.InvoiceDetails.Where(p => p.InvoiceID == InvoiceID).ToList();

                    this.DataSource = data;
                }
            }
            catch { }
        }
    }
}
