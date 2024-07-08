namespace BEE.HoatDong.MGL
{
    partial class frmTrangThaiGD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrangThaiGD));
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.txtTenHuong = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lbNgay = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.spinOverTime = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.spinOder = new DevExpress.XtraEditors.SpinEdit();
            this.txtColor = new DevExpress.XtraEditors.ColorEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenHuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinOverTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinOder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtColor.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.ImageOptions.ImageIndex = 4;
            this.btnHuy.ImageOptions.ImageList = this.imageCollection1;
            this.btnHuy.Location = new System.Drawing.Point(287, 200);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(79, 23);
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
            this.imageCollection1.Images.SetKeyName(18, "loaitailieu1.png");
            // 
            // btnDongY
            // 
            this.btnDongY.ImageOptions.ImageIndex = 6;
            this.btnDongY.ImageOptions.ImageList = this.imageCollection1;
            this.btnDongY.Location = new System.Drawing.Point(190, 200);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(91, 23);
            this.btnDongY.TabIndex = 2;
            this.btnDongY.Text = "Lưu && Đóng";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // txtTenHuong
            // 
            this.txtTenHuong.Location = new System.Drawing.Point(107, 11);
            this.txtTenHuong.Name = "txtTenHuong";
            this.txtTenHuong.Size = new System.Drawing.Size(242, 20);
            this.txtTenHuong.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(89, 13);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Tên trạng thái (*):";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lbNgay);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.spinOverTime);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.txtCode);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.spinOder);
            this.panelControl1.Controls.Add(this.txtColor);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txtTenHuong);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(12, 10);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(354, 169);
            this.panelControl1.TabIndex = 9;
            // 
            // lbNgay
            // 
            this.lbNgay.Location = new System.Drawing.Point(293, 138);
            this.lbNgay.Name = "lbNgay";
            this.lbNgay.Size = new System.Drawing.Size(8, 13);
            this.lbNgay.TabIndex = 18;
            this.lbNgay.Text = "[]";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(8, 111);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(63, 13);
            this.labelControl5.TabIndex = 17;
            this.labelControl5.Text = "Hết hạn (giờ)";
            // 
            // spinOverTime
            // 
            this.spinOverTime.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinOverTime.Location = new System.Drawing.Point(107, 108);
            this.spinOverTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.spinOverTime.Name = "spinOverTime";
            this.spinOverTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinOverTime.Size = new System.Drawing.Size(242, 20);
            this.spinOverTime.TabIndex = 16;
            this.spinOverTime.EditValueChanged += new System.EventHandler(this.spinOverTime_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(8, 86);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(25, 13);
            this.labelControl4.TabIndex = 15;
            this.labelControl4.Text = "Code";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(107, 83);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(242, 20);
            this.txtCode.TabIndex = 14;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(8, 61);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(33, 13);
            this.labelControl3.TabIndex = 13;
            this.labelControl3.Text = "Thứ tự";
            // 
            // spinOder
            // 
            this.spinOder.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinOder.Location = new System.Drawing.Point(107, 58);
            this.spinOder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.spinOder.Name = "spinOder";
            this.spinOder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinOder.Size = new System.Drawing.Size(242, 20);
            this.spinOder.TabIndex = 12;
            // 
            // txtColor
            // 
            this.txtColor.EditValue = System.Drawing.Color.Empty;
            this.txtColor.Location = new System.Drawing.Point(107, 35);
            this.txtColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtColor.Name = "txtColor";
            this.txtColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtColor.Size = new System.Drawing.Size(242, 20);
            this.txtColor.TabIndex = 11;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 38);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(20, 13);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "Màu";
            // 
            // frmTrangThaiGD
            // 
            this.AcceptButton = this.btnDongY;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(397, 249);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnDongY);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTrangThaiGD";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trạng thái giao dịch";
            this.Load += new System.EventHandler(this.Huong_frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenHuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinOverTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinOder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtColor.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private DevExpress.XtraEditors.TextEdit txtTenHuong;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.ColorEdit txtColor;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SpinEdit spinOder;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SpinEdit spinOverTime;
        private DevExpress.XtraEditors.LabelControl lbNgay;
    }
}