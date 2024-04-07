namespace BEE.BaoCao.ThongKe
{
    partial class PhanTichTuoiNo_frm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PhanTichTuoiNo_frm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.dateNgay = new DevExpress.XtraEditors.DateEdit();
            this.lookUpTang = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpBlock = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl52 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl53 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpDuAn2 = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpNhomKH = new DevExpress.XtraEditors.LookUpEdit();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgay.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpTang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpBlock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpDuAn2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpNhomKH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.dateNgay);
            this.panelControl1.Controls.Add(this.lookUpTang);
            this.panelControl1.Controls.Add(this.lookUpBlock);
            this.panelControl1.Controls.Add(this.labelControl52);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl53);
            this.panelControl1.Controls.Add(this.lookUpDuAn2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.lookUpNhomKH);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(260, 204);
            this.panelControl1.TabIndex = 0;
            // 
            // dateNgay
            // 
            this.dateNgay.EditValue = null;
            this.dateNgay.Location = new System.Drawing.Point(23, 34);
            this.dateNgay.Name = "dateNgay";
            this.dateNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgay.Properties.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateNgay.Properties.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateNgay.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateNgay.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateNgay.Size = new System.Drawing.Size(214, 20);
            this.dateNgay.TabIndex = 58;
            // 
            // lookUpTang
            // 
            this.lookUpTang.Location = new System.Drawing.Point(133, 123);
            this.lookUpTang.Name = "lookUpTang";
            this.lookUpTang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpTang.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTangNha", "Name5")});
            this.lookUpTang.Properties.DisplayMember = "TenTangNha";
            this.lookUpTang.Properties.NullText = "";
            this.lookUpTang.Properties.ShowHeader = false;
            this.lookUpTang.Properties.ValueMember = "MaTangNha";
            this.lookUpTang.Size = new System.Drawing.Size(104, 20);
            this.lookUpTang.TabIndex = 57;
            // 
            // lookUpBlock
            // 
            this.lookUpBlock.Location = new System.Drawing.Point(23, 123);
            this.lookUpBlock.Name = "lookUpBlock";
            this.lookUpBlock.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpBlock.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BlockName", "Name4")});
            this.lookUpBlock.Properties.DisplayMember = "BlockName";
            this.lookUpBlock.Properties.NullText = "";
            this.lookUpBlock.Properties.ShowHeader = false;
            this.lookUpBlock.Properties.ValueMember = "BlockID";
            this.lookUpBlock.Size = new System.Drawing.Size(104, 20);
            this.lookUpBlock.TabIndex = 56;
            this.lookUpBlock.EditValueChanged += new System.EventHandler(this.lookUpBlock_EditValueChanged);
            // 
            // labelControl52
            // 
            this.labelControl52.Location = new System.Drawing.Point(133, 107);
            this.labelControl52.Name = "labelControl52";
            this.labelControl52.Size = new System.Drawing.Size(24, 13);
            this.labelControl52.TabIndex = 55;
            this.labelControl52.Text = "Tầng";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(23, 149);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(85, 13);
            this.labelControl2.TabIndex = 54;
            this.labelControl2.Text = "Nhóm khách hàng";
            // 
            // labelControl53
            // 
            this.labelControl53.Location = new System.Drawing.Point(23, 106);
            this.labelControl53.Name = "labelControl53";
            this.labelControl53.Size = new System.Drawing.Size(24, 13);
            this.labelControl53.TabIndex = 54;
            this.labelControl53.Text = "Block";
            // 
            // lookUpDuAn2
            // 
            this.lookUpDuAn2.Location = new System.Drawing.Point(23, 81);
            this.lookUpDuAn2.Name = "lookUpDuAn2";
            this.lookUpDuAn2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpDuAn2.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenDA", "Name9")});
            this.lookUpDuAn2.Properties.DisplayMember = "TenDA";
            this.lookUpDuAn2.Properties.NullText = "";
            this.lookUpDuAn2.Properties.ShowHeader = false;
            this.lookUpDuAn2.Properties.ValueMember = "MaDA";
            this.lookUpDuAn2.Size = new System.Drawing.Size(214, 20);
            this.lookUpDuAn2.TabIndex = 53;
            this.lookUpDuAn2.EditValueChanged += new System.EventHandler(this.lookUpDuAn_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(23, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(43, 13);
            this.labelControl1.TabIndex = 52;
            this.labelControl1.Text = "Thời gian";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(23, 62);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(29, 13);
            this.labelControl5.TabIndex = 52;
            this.labelControl5.Text = "Dự án";
            // 
            // lookUpNhomKH
            // 
            this.lookUpNhomKH.Location = new System.Drawing.Point(23, 168);
            this.lookUpNhomKH.Name = "lookUpNhomKH";
            this.lookUpNhomKH.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpNhomKH.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenNKH", "Name5")});
            this.lookUpNhomKH.Properties.DisplayMember = "TenNKH";
            this.lookUpNhomKH.Properties.NullText = "";
            this.lookUpNhomKH.Properties.ShowHeader = false;
            this.lookUpNhomKH.Properties.ValueMember = "MaNKH";
            this.lookUpNhomKH.Size = new System.Drawing.Size(214, 20);
            this.lookUpNhomKH.TabIndex = 57;
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.ImageIndex = 4;
            this.btnHuy.ImageList = this.imageCollection1;
            this.btnHuy.Location = new System.Drawing.Point(145, 222);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(79, 23);
            this.btnHuy.TabIndex = 12;
            this.btnHuy.Text = "Hủy - ESC";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnDongY
            // 
            this.btnDongY.ImageIndex = 6;
            this.btnDongY.ImageList = this.imageCollection1;
            this.btnDongY.Location = new System.Drawing.Point(58, 222);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(81, 23);
            this.btnDongY.TabIndex = 11;
            this.btnDongY.Text = "Đồng ý";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "add.png");
            this.imageCollection1.Images.SetKeyName(1, "recyclebin.png");
            this.imageCollection1.Images.SetKeyName(2, "edit-icon.png");
            this.imageCollection1.Images.SetKeyName(3, "print3.png");
            this.imageCollection1.Images.SetKeyName(4, "cancel.png");
            this.imageCollection1.Images.SetKeyName(5, "refresh4.png");
            this.imageCollection1.Images.SetKeyName(6, "Luu.png");
            this.imageCollection1.Images.SetKeyName(7, "OK.png");
            this.imageCollection1.Images.SetKeyName(8, "print1.png");
            this.imageCollection1.Images.SetKeyName(9, "delay.png");
            this.imageCollection1.Images.SetKeyName(10, "excel.png");
            this.imageCollection1.Images.SetKeyName(11, "export5.png");
            this.imageCollection1.Images.SetKeyName(12, "lock1.png");
            this.imageCollection1.Images.SetKeyName(13, "login.png");
            this.imageCollection1.Images.SetKeyName(14, "key.png");
            this.imageCollection1.Images.SetKeyName(15, "baogia.png");
            this.imageCollection1.Images.SetKeyName(16, "tien.png");
            this.imageCollection1.Images.SetKeyName(17, "UPDATE.png");
            this.imageCollection1.Images.SetKeyName(18, "previous-icon.png");
            this.imageCollection1.Images.SetKeyName(19, "next-icon.png");
            this.imageCollection1.Images.SetKeyName(20, "document32x32.png");
            this.imageCollection1.Images.SetKeyName(21, "clock1.png");
            this.imageCollection1.Images.SetKeyName(22, "HELP.png");
            this.imageCollection1.Images.SetKeyName(23, "thanhly.png");
            this.imageCollection1.Images.SetKeyName(24, "muaban.png");
            this.imageCollection1.Images.SetKeyName(25, "chuyentrangthai.png");
            this.imageCollection1.Images.SetKeyName(26, "giaodich32x32.png");
            this.imageCollection1.Images.SetKeyName(27, "repeat.png");
            this.imageCollection1.Images.SetKeyName(28, "cart3.png");
            this.imageCollection1.Images.SetKeyName(29, "import.png");
            this.imageCollection1.Images.SetKeyName(30, "pause.png");
            this.imageCollection1.Images.SetKeyName(31, "play.png");
            // 
            // PhanTichTuoiNo_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 257);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnDongY);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PhanTichTuoiNo_frm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tham số báo cáo";
            this.Load += new System.EventHandler(this.PhanTichTuoiNo_frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgay.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpTang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpBlock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpDuAn2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpNhomKH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookUpTang;
        private DevExpress.XtraEditors.LookUpEdit lookUpBlock;
        private DevExpress.XtraEditors.LabelControl labelControl52;
        private DevExpress.XtraEditors.LabelControl labelControl53;
        private DevExpress.XtraEditors.LookUpEdit lookUpDuAn2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.DateEdit dateNgay;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private DevExpress.XtraEditors.LookUpEdit lookUpNhomKH;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}