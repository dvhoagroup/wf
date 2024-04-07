namespace BEE.NhanVien
{
    partial class frmExpiredLock
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExpiredLock));
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.rdoStatusLock = new DevExpress.XtraEditors.RadioGroup();
            this.memoComment = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateExprired = new DevExpress.XtraEditors.DateEdit();
            this.lblNumber = new DevExpress.XtraEditors.LabelControl();
            this.spinNumber = new DevExpress.XtraEditors.SpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoStatusLock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateExprired.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateExprired.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNumber.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.ImageOptions.ImageIndex = 4;
            this.btnHuy.ImageOptions.ImageList = this.imageCollection1;
            this.btnHuy.Location = new System.Drawing.Point(537, 264);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(92, 28);
            this.btnHuy.TabIndex = 3;
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
            this.imageCollection1.Images.SetKeyName(29, "import.png");
            this.imageCollection1.Images.SetKeyName(30, "pause.png");
            this.imageCollection1.Images.SetKeyName(31, "play.png");
            // 
            // btnDongY
            // 
            this.btnDongY.ImageOptions.ImageIndex = 6;
            this.btnDongY.ImageOptions.ImageList = this.imageCollection1;
            this.btnDongY.Location = new System.Drawing.Point(424, 264);
            this.btnDongY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(106, 28);
            this.btnDongY.TabIndex = 2;
            this.btnDongY.Text = "Lưu";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // rdoStatusLock
            // 
            this.rdoStatusLock.Location = new System.Drawing.Point(14, 14);
            this.rdoStatusLock.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdoStatusLock.Name = "rdoStatusLock";
            this.rdoStatusLock.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Theo giờ"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "Theo ngày"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(3, "Theo tháng"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(4, "Theo năm"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(5, "Đến ngày"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Vô thời hạn")});
            this.rdoStatusLock.Size = new System.Drawing.Size(615, 38);
            this.rdoStatusLock.TabIndex = 4;
            this.rdoStatusLock.SelectedIndexChanged += new System.EventHandler(this.rdoStatusLock_SelectedIndexChanged);
            // 
            // memoComment
            // 
            this.memoComment.Location = new System.Drawing.Point(115, 87);
            this.memoComment.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.memoComment.Name = "memoComment";
            this.memoComment.Size = new System.Drawing.Size(514, 169);
            this.memoComment.TabIndex = 10;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(15, 134);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(69, 16);
            this.labelControl3.TabIndex = 11;
            this.labelControl3.Text = "Ghi chú (*):";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 61);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(83, 17);
            this.labelControl1.TabIndex = 12;
            this.labelControl1.Text = "Ngày hết hạn";
            // 
            // dateExprired
            // 
            this.dateExprired.EditValue = null;
            this.dateExprired.Location = new System.Drawing.Point(115, 58);
            this.dateExprired.Name = "dateExprired";
            this.dateExprired.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateExprired.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateExprired.Size = new System.Drawing.Size(185, 22);
            this.dateExprired.TabIndex = 13;
            // 
            // lblNumber
            // 
            this.lblNumber.Location = new System.Drawing.Point(321, 63);
            this.lblNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(50, 17);
            this.lblNumber.TabIndex = 14;
            this.lblNumber.Text = "Nhập số";
            // 
            // spinNumber
            // 
            this.spinNumber.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinNumber.Location = new System.Drawing.Point(407, 57);
            this.spinNumber.Name = "spinNumber";
            this.spinNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinNumber.Size = new System.Drawing.Size(222, 24);
            this.spinNumber.TabIndex = 15;
            // 
            // frmExpiredLock
            // 
            this.AcceptButton = this.btnDongY;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(659, 316);
            this.Controls.Add(this.spinNumber);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.dateExprired);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.memoComment);
            this.Controls.Add(this.rdoStatusLock);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnDongY);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExpiredLock";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tự động khóa user";
            this.Load += new System.EventHandler(this.frmExpiredLock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoStatusLock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateExprired.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateExprired.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNumber.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.RadioGroup rdoStatusLock;
        private DevExpress.XtraEditors.MemoEdit memoComment;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dateExprired;
        private DevExpress.XtraEditors.LabelControl lblNumber;
        private DevExpress.XtraEditors.SpinEdit spinNumber;
    }
}