namespace BEE.BaoCao.Marketing
{
    partial class SubMail_rpt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubMail_rpt));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.richText = new DevExpress.XtraReports.UI.XRRichText();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.mergeMail_src1 = new BEE.BaoCao.Marketing.MergeMail_src();
            this.khachHang_MergeMailTableAdapter = new BEE.BaoCao.Marketing.MergeMail_srcTableAdapters.KhachHang_MergeMailTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.richText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mergeMail_src1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.richText});
            this.Detail.HeightF = 23F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // richText
            // 
            this.richText.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Rtf", null, "KhachHang_MergeMail.NoiDung")});
            this.richText.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.richText.Name = "richText";
            this.richText.SerializableRtfString = resources.GetString("richText.SerializableRtfString");
            this.richText.SizeF = new System.Drawing.SizeF(750F, 23F);
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
            // mergeMail_src1
            // 
            this.mergeMail_src1.DataSetName = "MergeMail_src";
            this.mergeMail_src1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // khachHang_MergeMailTableAdapter
            // 
            this.khachHang_MergeMailTableAdapter.ClearBeforeFill = true;
            // 
            // SubMail_rpt
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.DataAdapter = this.khachHang_MergeMailTableAdapter;
            this.DataMember = "KhachHang_MergeMail";
            this.DataSource = this.mergeMail_src1;
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 0, 0);
            this.Version = "10.2";
            ((System.ComponentModel.ISupportInitialize)(this.richText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mergeMail_src1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRRichText richText;
        private MergeMail_src mergeMail_src1;
        private BEE.BaoCao.Marketing.MergeMail_srcTableAdapters.KhachHang_MergeMailTableAdapter khachHang_MergeMailTableAdapter;
    }
}
