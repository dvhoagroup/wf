namespace BEE.BaoCao.ThongKe.CTHDQT
{
    partial class OptionCNPTKH_frm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionCNPTKH_frm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.dateThoiGian = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpBlock = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl53 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpDuAn2 = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateThoiGian.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateThoiGian.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpBlock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpDuAn2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.dateThoiGian);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.lookUpBlock);
            this.panelControl1.Controls.Add(this.labelControl53);
            this.panelControl1.Controls.Add(this.lookUpDuAn2);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(247, 154);
            this.panelControl1.TabIndex = 0;
            // 
            // dateThoiGian
            // 
            this.dateThoiGian.EditValue = null;
            this.dateThoiGian.Location = new System.Drawing.Point(18, 31);
            this.dateThoiGian.Name = "dateThoiGian";
            this.dateThoiGian.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateThoiGian.Properties.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateThoiGian.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateThoiGian.Properties.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateThoiGian.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateThoiGian.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateThoiGian.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateThoiGian.Size = new System.Drawing.Size(214, 20);
            this.dateThoiGian.TabIndex = 50;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(18, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(43, 13);
            this.labelControl3.TabIndex = 47;
            this.labelControl3.Text = "Thời gian";
            // 
            // lookUpBlock
            // 
            this.lookUpBlock.Location = new System.Drawing.Point(18, 117);
            this.lookUpBlock.Name = "lookUpBlock";
            this.lookUpBlock.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpBlock.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BlockName", "Name4")});
            this.lookUpBlock.Properties.DisplayMember = "BlockName";
            this.lookUpBlock.Properties.NullText = "";
            this.lookUpBlock.Properties.ShowHeader = false;
            this.lookUpBlock.Properties.ValueMember = "BlockID";
            this.lookUpBlock.Size = new System.Drawing.Size(214, 20);
            this.lookUpBlock.TabIndex = 46;
            // 
            // labelControl53
            // 
            this.labelControl53.Location = new System.Drawing.Point(18, 100);
            this.labelControl53.Name = "labelControl53";
            this.labelControl53.Size = new System.Drawing.Size(24, 13);
            this.labelControl53.TabIndex = 45;
            this.labelControl53.Text = "Block";
            // 
            // lookUpDuAn2
            // 
            this.lookUpDuAn2.Location = new System.Drawing.Point(18, 75);
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
            this.lookUpDuAn2.TabIndex = 44;
            this.lookUpDuAn2.EditValueChanged += new System.EventHandler(this.lookUpDuAn_EditValueChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(18, 56);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(29, 13);
            this.labelControl5.TabIndex = 43;
            this.labelControl5.Text = "Dự án";
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.ImageIndex = 4;
            this.btnHuy.ImageList = this.imageCollection1;
            this.btnHuy.Location = new System.Drawing.Point(140, 172);
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
            this.btnDongY.Location = new System.Drawing.Point(53, 172);
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
            // OptionCNPTKH_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 208);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnDongY);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionCNPTKH_frm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tham số báo cáo";
            this.Load += new System.EventHandler(this.Option_frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateThoiGian.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateThoiGian.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpBlock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpDuAn2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private DevExpress.XtraEditors.LookUpEdit lookUpBlock;
        private DevExpress.XtraEditors.LabelControl labelControl53;
        private DevExpress.XtraEditors.LookUpEdit lookUpDuAn2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit dateThoiGian;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}