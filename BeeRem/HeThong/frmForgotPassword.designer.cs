using DevExpress.XtraEditors;

namespace BEEREMA.HeThong
{
    partial class frmForgotPassword
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmForgotPassword));
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.txtMaSo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grOtp = new System.Windows.Forms.GroupBox();
            this.lblTime = new DevExpress.XtraEditors.LabelControl();
            this.btnAcceptOtp = new DevExpress.XtraEditors.SimpleButton();
            this.txtOtp = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.timerCountTime = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaSo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grOtp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOtp.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 285);
            this.ribbonStatusBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(640, 30);
            // 
            // ribbon
            // 
            this.ribbon.ApplicationButtonImageOptions.Image = global::BEEREMA.Properties.Resources.logo_hoaland;
            toolTipTitleItem1.ImageOptions.Image = global::BEEREMA.Properties.Resources.BEEREM;
            toolTipTitleItem1.Text = "Phần mềm quản lý sàn BEEREMA\r\nPhát triển bởi Beesky";
            superToolTip1.Items.Add(toolTipTitleItem1);
            this.ribbon.ApplicationButtonSuperTip = superToolTip1;
            this.ribbon.ApplicationButtonText = null;
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ribbon.MaxItemId = 2;
            this.ribbon.Name = "ribbon";
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.Size = new System.Drawing.Size(640, 71);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            this.ribbon.Click += new System.EventHandler(this.ribbon_Click);
            // 
            // txtMaSo
            // 
            this.txtMaSo.Location = new System.Drawing.Point(84, 25);
            this.txtMaSo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMaSo.MenuManager = this.ribbon;
            this.txtMaSo.Name = "txtMaSo";
            this.txtMaSo.Size = new System.Drawing.Size(225, 22);
            this.txtMaSo.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 28);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 17);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Mã số:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(84, 57);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(225, 22);
            this.txtEmail.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 60);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 16);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Email:";
            // 
            // btnDongY
            // 
            this.btnDongY.ImageOptions.Image = global::BEEREMA.Properties.Resources.login20;
            this.btnDongY.Location = new System.Drawing.Point(84, 117);
            this.btnDongY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(105, 28);
            this.btnDongY.TabIndex = 3;
            this.btnDongY.Text = "Đồng ý";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.ImageOptions.Image = global::BEEREMA.Properties.Resources.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(196, 117);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(92, 28);
            this.btnHuy.TabIndex = 4;
            this.btnHuy.Text = "Bỏ qua";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMaSo);
            this.groupBox1.Controls.Add(this.btnHuy);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.btnDongY);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Location = new System.Drawing.Point(14, 84);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(339, 171);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Quên mật khẩu";
            // 
            // grOtp
            // 
            this.grOtp.Controls.Add(this.lblTime);
            this.grOtp.Controls.Add(this.btnAcceptOtp);
            this.grOtp.Controls.Add(this.txtOtp);
            this.grOtp.Controls.Add(this.labelControl3);
            this.grOtp.Location = new System.Drawing.Point(360, 90);
            this.grOtp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grOtp.Name = "grOtp";
            this.grOtp.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grOtp.Size = new System.Drawing.Size(258, 165);
            this.grOtp.TabIndex = 8;
            this.grOtp.TabStop = false;
            this.grOtp.Text = "Nhập otp";
            // 
            // lblTime
            // 
            this.lblTime.Location = new System.Drawing.Point(83, 52);
            this.lblTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(73, 17);
            this.lblTime.TabIndex = 7;
            this.lblTime.Text = "Đếm ngược";
            // 
            // btnAcceptOtp
            // 
            this.btnAcceptOtp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAcceptOtp.ImageOptions.Image = global::BEEREMA.Properties.Resources.Luu;
            this.btnAcceptOtp.Location = new System.Drawing.Point(83, 111);
            this.btnAcceptOtp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAcceptOtp.Name = "btnAcceptOtp";
            this.btnAcceptOtp.Size = new System.Drawing.Size(157, 28);
            this.btnAcceptOtp.TabIndex = 6;
            this.btnAcceptOtp.Text = "Xác nhận và Gửi mail";
            this.btnAcceptOtp.Click += new System.EventHandler(this.btnAcceptOtp_Click);
            // 
            // txtOtp
            // 
            this.txtOtp.Location = new System.Drawing.Point(83, 18);
            this.txtOtp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOtp.Name = "txtOtp";
            this.txtOtp.Size = new System.Drawing.Size(157, 22);
            this.txtOtp.TabIndex = 4;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(10, 22);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(20, 16);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Opt";
            // 
            // timerCountTime
            // 
            this.timerCountTime.Interval = 1000;
            this.timerCountTime.Tick += new System.EventHandler(this.timerCountTime_Tick);
            // 
            // frmForgotPassword
            // 
            this.AcceptButton = this.btnDongY;
            this.AllowMdiBar = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(640, 315);
            this.Controls.Add(this.grOtp);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmForgotPassword.IconOptions.Icon")));
            this.IconOptions.ShowIcon = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmForgotPassword";
            this.Ribbon = this.ribbon;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Forgot  password";
            this.Load += new System.EventHandler(this.Login_frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaSo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grOtp.ResumeLayout(false);
            this.grOtp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOtp.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraEditors.TextEdit txtMaSo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grOtp;
        private DevExpress.XtraEditors.SimpleButton btnAcceptOtp;
        private DevExpress.XtraEditors.TextEdit txtOtp;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private LabelControl lblTime;
        private System.Windows.Forms.Timer timerCountTime;
    }
}