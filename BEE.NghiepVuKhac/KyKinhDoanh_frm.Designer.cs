namespace BEE.NghiepVuKhac
{
    partial class KyKinhDoanh_frm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KyKinhDoanh_frm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lookUpDuAn = new DevExpress.XtraEditors.LookUpEdit();
            this.spinMucPhi = new DevExpress.XtraEditors.SpinEdit();
            this.spinSLMin = new DevExpress.XtraEditors.SpinEdit();
            this.spinSLMax = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpDuAn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMucPhi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSLMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSLMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lookUpDuAn);
            this.panelControl1.Controls.Add(this.spinMucPhi);
            this.panelControl1.Controls.Add(this.spinSLMin);
            this.panelControl1.Controls.Add(this.spinSLMax);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(335, 96);
            this.panelControl1.TabIndex = 10;
            // 
            // lookUpDuAn
            // 
            this.lookUpDuAn.Location = new System.Drawing.Point(84, 64);
            this.lookUpDuAn.Name = "lookUpDuAn";
            this.lookUpDuAn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpDuAn.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCT", 150, "Tên chỉ tiêu"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenDA", 150, "Dự án")});
            this.lookUpDuAn.Properties.DisplayMember = "TenCT";
            this.lookUpDuAn.Properties.NullText = "";
            this.lookUpDuAn.Properties.PopupWidth = 300;
            this.lookUpDuAn.Properties.ValueMember = "MaCT";
            this.lookUpDuAn.Size = new System.Drawing.Size(234, 20);
            this.lookUpDuAn.TabIndex = 3;
            // 
            // spinMucPhi
            // 
            this.spinMucPhi.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinMucPhi.Location = new System.Drawing.Point(84, 37);
            this.spinMucPhi.Name = "spinMucPhi";
            this.spinMucPhi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinMucPhi.Properties.DisplayFormat.FormatString = "{0:n2} %";
            this.spinMucPhi.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinMucPhi.Properties.EditFormat.FormatString = "{0:n2} %";
            this.spinMucPhi.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinMucPhi.Properties.MaxValue = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.spinMucPhi.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinMucPhi.Size = new System.Drawing.Size(71, 20);
            this.spinMucPhi.TabIndex = 1;
            // 
            // spinSLMin
            // 
            this.spinSLMin.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinSLMin.Location = new System.Drawing.Point(84, 11);
            this.spinSLMin.Name = "spinSLMin";
            this.spinSLMin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinSLMin.Properties.DisplayFormat.FormatString = "{0} căn";
            this.spinSLMin.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinSLMin.Properties.EditFormat.FormatString = "{0} căn";
            this.spinSLMin.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinSLMin.Properties.MaxValue = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.spinSLMin.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinSLMin.Size = new System.Drawing.Size(71, 20);
            this.spinSLMin.TabIndex = 1;
            // 
            // spinSLMax
            // 
            this.spinSLMax.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinSLMax.Location = new System.Drawing.Point(247, 11);
            this.spinSLMax.Name = "spinSLMax";
            this.spinSLMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinSLMax.Properties.DisplayFormat.FormatString = "{0} căn";
            this.spinSLMax.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinSLMax.Properties.EditFormat.FormatString = "{0} căn";
            this.spinSLMax.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinSLMax.Properties.MaxValue = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.spinSLMax.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinSLMax.Size = new System.Drawing.Size(71, 20);
            this.spinSLMax.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(18, 40);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(41, 13);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Mức phí:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(18, 14);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 13);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Số lượng từ:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(18, 67);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(40, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Chỉ tiêu:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(174, 14);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(67, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Số lượng đến:";
            // 
            // btnDongY
            // 
            this.btnDongY.ImageIndex = 6;
            this.btnDongY.ImageList = this.imageCollection1;
            this.btnDongY.Location = new System.Drawing.Point(91, 126);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(89, 23);
            this.btnDongY.TabIndex = 4;
            this.btnDongY.Text = "Lưu && Đóng";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.ImageIndex = 4;
            this.btnHuy.ImageList = this.imageCollection1;
            this.btnHuy.Location = new System.Drawing.Point(186, 126);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(79, 23);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "Hủy - ESC";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
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
            // 
            // KyKinhDoanh_frm
            // 
            this.AcceptButton = this.btnDongY;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(359, 161);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnDongY);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KyKinhDoanh_frm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kỳ kinh doanh";
            this.Load += new System.EventHandler(this.Huong_frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpDuAn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMucPhi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSLMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSLMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SpinEdit spinSLMin;
        private DevExpress.XtraEditors.SpinEdit spinSLMax;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lookUpDuAn;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SpinEdit spinMucPhi;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}