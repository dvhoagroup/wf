namespace BEE.BaoCao.Invoice
{
    partial class rptInvoice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.subReport = new DevExpress.XtraReports.UI.XRSubreport();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.lblBank = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCode = new DevExpress.XtraReports.UI.XRLabel();
            this.lblMethod = new DevExpress.XtraReports.UI.XRLabel();
            this.lblAddress = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCompany = new DevExpress.XtraReports.UI.XRLabel();
            this.lblPersonal = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDateOut = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblGrandTotalText = new DevExpress.XtraReports.UI.XRLabel();
            this.lblGrandTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.lblVAT = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSubTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.lblVATRate = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.subReport});
            this.Detail.HeightF = 208.4167F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // subReport
            // 
            this.subReport.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 7.999992F);
            this.subReport.Name = "subReport";
            this.subReport.SizeF = new System.Drawing.SizeF(765.9996F, 23.00002F);
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblBank,
            this.lblCode,
            this.lblMethod,
            this.lblAddress,
            this.lblCompany,
            this.lblPersonal,
            this.lblDateOut});
            this.ReportHeader.HeightF = 405.2083F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // lblBank
            // 
            this.lblBank.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.lblBank.LocationFloat = new DevExpress.Utils.PointFloat(648.9582F, 316.9166F);
            this.lblBank.Name = "lblBank";
            this.lblBank.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblBank.SizeF = new System.Drawing.SizeF(150.2915F, 23F);
            this.lblBank.StylePriority.UseFont = false;
            this.lblBank.StylePriority.UseTextAlignment = false;
            this.lblBank.Text = "Bank\'s A/C No";
            this.lblBank.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblCode
            // 
            this.lblCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.lblCode.LocationFloat = new DevExpress.Utils.PointFloat(209.375F, 292.9166F);
            this.lblCode.Name = "lblCode";
            this.lblCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCode.SizeF = new System.Drawing.SizeF(589.8745F, 23F);
            this.lblCode.StylePriority.UseFont = false;
            this.lblCode.StylePriority.UseTextAlignment = false;
            this.lblCode.Text = "Code";
            this.lblCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblMethod
            // 
            this.lblMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.lblMethod.LocationFloat = new DevExpress.Utils.PointFloat(288.5416F, 316.9166F);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblMethod.SizeF = new System.Drawing.SizeF(150.2915F, 23F);
            this.lblMethod.StylePriority.UseFont = false;
            this.lblMethod.StylePriority.UseTextAlignment = false;
            this.lblMethod.Text = "Payment Method";
            this.lblMethod.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblAddress
            // 
            this.lblAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.lblAddress.LocationFloat = new DevExpress.Utils.PointFloat(157.3753F, 268.9166F);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblAddress.SizeF = new System.Drawing.SizeF(641.8743F, 23F);
            this.lblAddress.StylePriority.UseFont = false;
            this.lblAddress.StylePriority.UseTextAlignment = false;
            this.lblAddress.Text = "Address";
            this.lblAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblCompany
            // 
            this.lblCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.lblCompany.LocationFloat = new DevExpress.Utils.PointFloat(230.2083F, 245.2083F);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCompany.SizeF = new System.Drawing.SizeF(569.0413F, 23F);
            this.lblCompany.StylePriority.UseFont = false;
            this.lblCompany.StylePriority.UseTextAlignment = false;
            this.lblCompany.Text = "Company";
            this.lblCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblPersonal
            // 
            this.lblPersonal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.lblPersonal.LocationFloat = new DevExpress.Utils.PointFloat(329.1666F, 221.0833F);
            this.lblPersonal.Name = "lblPersonal";
            this.lblPersonal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblPersonal.SizeF = new System.Drawing.SizeF(470.0829F, 22.99998F);
            this.lblPersonal.StylePriority.UseFont = false;
            this.lblPersonal.StylePriority.UseTextAlignment = false;
            this.lblPersonal.Text = "Personal";
            this.lblPersonal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblDateOut
            // 
            this.lblDateOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.lblDateOut.LocationFloat = new DevExpress.Utils.PointFloat(262.5F, 184.5F);
            this.lblDateOut.Name = "lblDateOut";
            this.lblDateOut.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDateOut.SizeF = new System.Drawing.SizeF(231.1666F, 23F);
            this.lblDateOut.StylePriority.UseFont = false;
            this.lblDateOut.StylePriority.UseTextAlignment = false;
            this.lblDateOut.Text = "01                  02              2013";
            this.lblDateOut.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblGrandTotalText,
            this.lblGrandTotal,
            this.lblVAT,
            this.lblSubTotal,
            this.lblVATRate});
            this.ReportFooter.HeightF = 240.625F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // lblGrandTotalText
            // 
            this.lblGrandTotalText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.lblGrandTotalText.LocationFloat = new DevExpress.Utils.PointFloat(262.5F, 107.7916F);
            this.lblGrandTotalText.Name = "lblGrandTotalText";
            this.lblGrandTotalText.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblGrandTotalText.SizeF = new System.Drawing.SizeF(523.2083F, 23.00002F);
            this.lblGrandTotalText.StylePriority.UseFont = false;
            this.lblGrandTotalText.StylePriority.UseTextAlignment = false;
            this.lblGrandTotalText.Text = "GrandTotalText";
            this.lblGrandTotalText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblGrandTotal
            // 
            this.lblGrandTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.lblGrandTotal.LocationFloat = new DevExpress.Utils.PointFloat(506.25F, 73.12495F);
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.lblGrandTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblGrandTotal.SizeF = new System.Drawing.SizeF(269.7495F, 23.00002F);
            this.lblGrandTotal.StylePriority.UseFont = false;
            this.lblGrandTotal.StylePriority.UseTextAlignment = false;
            this.lblGrandTotal.Text = "GrandTotal";
            this.lblGrandTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblVAT
            // 
            this.lblVAT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.lblVAT.LocationFloat = new DevExpress.Utils.PointFloat(506.2499F, 43.12496F);
            this.lblVAT.Name = "lblVAT";
            this.lblVAT.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblVAT.SizeF = new System.Drawing.SizeF(269.7496F, 23.00002F);
            this.lblVAT.StylePriority.UseFont = false;
            this.lblVAT.StylePriority.UseTextAlignment = false;
            this.lblVAT.Text = "VAT";
            this.lblVAT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.lblSubTotal.LocationFloat = new DevExpress.Utils.PointFloat(506.25F, 13.95836F);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblSubTotal.SizeF = new System.Drawing.SizeF(269.7495F, 22.99995F);
            this.lblSubTotal.StylePriority.UseFont = false;
            this.lblSubTotal.StylePriority.UseTextAlignment = false;
            this.lblSubTotal.Text = "SubTotal";
            this.lblSubTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblVATRate
            // 
            this.lblVATRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.lblVATRate.LocationFloat = new DevExpress.Utils.PointFloat(230.2082F, 43.12496F);
            this.lblVATRate.Name = "lblVATRate";
            this.lblVATRate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblVATRate.SizeF = new System.Drawing.SizeF(208.6248F, 23.00002F);
            this.lblVATRate.StylePriority.UseFont = false;
            this.lblVATRate.StylePriority.UseTextAlignment = false;
            this.lblVATRate.Text = "VATRate";
            this.lblVATRate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // rptInvoice
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter});
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "10.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRSubreport subReport;
        private DevExpress.XtraReports.UI.XRLabel lblDateOut;
        private DevExpress.XtraReports.UI.XRLabel lblAddress;
        private DevExpress.XtraReports.UI.XRLabel lblCompany;
        private DevExpress.XtraReports.UI.XRLabel lblPersonal;
        private DevExpress.XtraReports.UI.XRLabel lblCode;
        private DevExpress.XtraReports.UI.XRLabel lblMethod;
        private DevExpress.XtraReports.UI.XRLabel lblVATRate;
        private DevExpress.XtraReports.UI.XRLabel lblGrandTotal;
        private DevExpress.XtraReports.UI.XRLabel lblVAT;
        private DevExpress.XtraReports.UI.XRLabel lblSubTotal;
        private DevExpress.XtraReports.UI.XRLabel lblGrandTotalText;
        private DevExpress.XtraReports.UI.XRLabel lblBank;
    }
}
