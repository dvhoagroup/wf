namespace BEE.QuangCao.Mail
{
    partial class frmConfirm
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.itemCancel = new DevExpress.XtraEditors.SimpleButton();
            this.itemAdd = new DevExpress.XtraEditors.SimpleButton();
            this.itemListReminder = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.itemCancel);
            this.panelControl1.Controls.Add(this.itemAdd);
            this.panelControl1.Controls.Add(this.itemListReminder);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(4, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(449, 138);
            this.panelControl1.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.AllowHtmlString = true;
            this.labelControl2.Location = new System.Drawing.Point(41, 55);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(382, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "<b>Thêm mới</b>:  Chọn danh sách người nhận (Khách hàng, Nhân viên, Import file)." +
                "";
            // 
            // labelControl3
            // 
            this.labelControl3.AllowHtmlString = true;
            this.labelControl3.Location = new System.Drawing.Point(27, 15);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(66, 14);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "<b>* Chú thích</b>:";
            // 
            // labelControl1
            // 
            this.labelControl1.AllowHtmlString = true;
            this.labelControl1.Location = new System.Drawing.Point(41, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(359, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "<b>Danh sách KH chọn</b>:  Danh sách <b>Khách hàng</b> đã chọn ở bước trước đó.";
            // 
            // itemCancel
            // 
            this.itemCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.24F);
            this.itemCancel.Appearance.Options.UseFont = true;
            this.itemCancel.Image = global::BEE.QuangCao.Properties.Resources.cancel_icon;
            this.itemCancel.Location = new System.Drawing.Point(318, 87);
            this.itemCancel.Name = "itemCancel";
            this.itemCancel.Size = new System.Drawing.Size(104, 43);
            this.itemCancel.TabIndex = 0;
            this.itemCancel.Text = "Bỏ qua";
            this.itemCancel.Click += new System.EventHandler(this.itemCancel_Click);
            // 
            // itemAdd
            // 
            this.itemAdd.Image = global::BEE.QuangCao.Properties.Resources.add32;
            this.itemAdd.Location = new System.Drawing.Point(208, 87);
            this.itemAdd.Name = "itemAdd";
            this.itemAdd.Size = new System.Drawing.Size(104, 43);
            this.itemAdd.TabIndex = 0;
            this.itemAdd.Text = "Thêm mới";
            this.itemAdd.Click += new System.EventHandler(this.itemAdd_Click);
            // 
            // itemListReminder
            // 
            this.itemListReminder.Image = global::BEE.QuangCao.Properties.Resources.document32x32;
            this.itemListReminder.Location = new System.Drawing.Point(34, 87);
            this.itemListReminder.Name = "itemListReminder";
            this.itemListReminder.Size = new System.Drawing.Size(168, 43);
            this.itemListReminder.TabIndex = 0;
            this.itemListReminder.Text = "Danh sách Khách hàng\r\nđã chọn";
            this.itemListReminder.Click += new System.EventHandler(this.itemListReminder_Click);
            // 
            // frmConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 146);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfirm";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tùy chọn";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton itemListReminder;
        private DevExpress.XtraEditors.SimpleButton itemAdd;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton itemCancel;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}