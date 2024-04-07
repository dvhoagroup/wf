namespace BEE.QuangCao.SMS
{
    partial class frmSendSMS2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSendSMS2));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtContent = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbSenderName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnAccept = new DevExpress.XtraEditors.SimpleButton();
            this.lblCharCount = new DevExpress.XtraEditors.LabelControl();
            this.linkDot = new System.Windows.Forms.LinkLabel();
            this.linkEmail = new System.Windows.Forms.LinkLabel();
            this.linkContract = new System.Windows.Forms.LinkLabel();
            this.linkSoTien = new System.Windows.Forms.LinkLabel();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSenderName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 60);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(46, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Nội dung:";
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(12, 79);
            this.txtContent.Name = "txtContent";
            this.txtContent.Properties.MaxLength = 160;
            this.txtContent.Size = new System.Drawing.Size(372, 60);
            this.txtContent.TabIndex = 1;
            this.txtContent.EditValueChanged += new System.EventHandler(this.txtContent_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(38, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Đầu số:";
            // 
            // cmbSenderName
            // 
            this.cmbSenderName.Location = new System.Drawing.Point(12, 34);
            this.cmbSenderName.Name = "cmbSenderName";
            this.cmbSenderName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSenderName.Properties.CycleOnDblClick = false;
            this.cmbSenderName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbSenderName.Size = new System.Drawing.Size(372, 20);
            this.cmbSenderName.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageIndex = 4;
            this.btnCancel.ImageList = this.imageCollection1;
            this.btnCancel.Location = new System.Drawing.Point(309, 145);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.ImageIndex = 7;
            this.btnAccept.ImageList = this.imageCollection1;
            this.btnAccept.Location = new System.Drawing.Point(228, 145);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 7;
            this.btnAccept.Text = "Gửi";
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // lblCharCount
            // 
            this.lblCharCount.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblCharCount.Location = new System.Drawing.Point(64, 60);
            this.lblCharCount.Name = "lblCharCount";
            this.lblCharCount.Size = new System.Drawing.Size(46, 13);
            this.lblCharCount.TabIndex = 9;
            this.lblCharCount.Text = "160 ký tự";
            // 
            // linkDot
            // 
            this.linkDot.AutoSize = true;
            this.linkDot.Location = new System.Drawing.Point(240, 60);
            this.linkDot.Name = "linkDot";
            this.linkDot.Size = new System.Drawing.Size(44, 13);
            this.linkDot.TabIndex = 13;
            this.linkDot.TabStop = true;
            this.linkDot.Text = "[DotTT]";
            this.linkDot.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkPass_LinkClicked);
            // 
            // linkEmail
            // 
            this.linkEmail.AutoSize = true;
            this.linkEmail.Location = new System.Drawing.Point(188, 60);
            this.linkEmail.Name = "linkEmail";
            this.linkEmail.Size = new System.Drawing.Size(46, 13);
            this.linkEmail.TabIndex = 12;
            this.linkEmail.TabStop = true;
            this.linkEmail.Text = "[TenKH]";
            this.linkEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkEmail_LinkClicked);
            // 
            // linkContract
            // 
            this.linkContract.AutoSize = true;
            this.linkContract.Location = new System.Drawing.Point(290, 60);
            this.linkContract.Name = "linkContract";
            this.linkContract.Size = new System.Drawing.Size(41, 13);
            this.linkContract.TabIndex = 13;
            this.linkContract.TabStop = true;
            this.linkContract.Text = "[SoHD]";
            this.linkContract.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkContract_LinkClicked);
            // 
            // linkSoTien
            // 
            this.linkSoTien.AutoSize = true;
            this.linkSoTien.Location = new System.Drawing.Point(337, 60);
            this.linkSoTien.Name = "linkSoTien";
            this.linkSoTien.Size = new System.Drawing.Size(47, 13);
            this.linkSoTien.TabIndex = 13;
            this.linkSoTien.TabStop = true;
            this.linkSoTien.Text = "[SoTien]";
            this.linkSoTien.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSoTien_LinkClicked);
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
            // 
            // frmSendSMS2
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(400, 176);
            this.Controls.Add(this.linkSoTien);
            this.Controls.Add(this.linkContract);
            this.Controls.Add(this.linkDot);
            this.Controls.Add(this.linkEmail);
            this.Controls.Add(this.lblCharCount);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbSenderName);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSendSMS2";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gửi tin nhắn";
            this.Load += new System.EventHandler(this.frmSmsMarketing_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSenderName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit txtContent;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSenderName;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnAccept;
        private DevExpress.XtraEditors.LabelControl lblCharCount;
        private System.Windows.Forms.LinkLabel linkDot;
        private System.Windows.Forms.LinkLabel linkEmail;
        private System.Windows.Forms.LinkLabel linkContract;
        private System.Windows.Forms.LinkLabel linkSoTien;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}