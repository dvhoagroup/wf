namespace BEE.NghiepVuKhac
{
    partial class ctlNgayThangNam
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtNgay = new DevExpress.XtraEditors.TextEdit();
            this.txtThang = new DevExpress.XtraEditors.TextEdit();
            this.txtNam = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNam.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNgay
            // 
            this.txtNgay.Location = new System.Drawing.Point(0, 0);
            this.txtNgay.Name = "txtNgay";
            this.txtNgay.Properties.Appearance.Options.UseTextOptions = true;
            this.txtNgay.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtNgay.Properties.MaxLength = 2;
            this.txtNgay.Properties.NullText = "dd";
            this.txtNgay.Size = new System.Drawing.Size(30, 20);
            this.txtNgay.TabIndex = 0;
            this.txtNgay.EditValueChanged += new System.EventHandler(this.txtNgay_EditValueChanged);
            // 
            // txtThang
            // 
            this.txtThang.Location = new System.Drawing.Point(32, 0);
            this.txtThang.Name = "txtThang";
            this.txtThang.Properties.Appearance.Options.UseTextOptions = true;
            this.txtThang.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtThang.Properties.MaxLength = 2;
            this.txtThang.Properties.NullText = "MM";
            this.txtThang.Size = new System.Drawing.Size(30, 20);
            this.txtThang.TabIndex = 1;
            this.txtThang.EditValueChanged += new System.EventHandler(this.txtThang_EditValueChanged);
            // 
            // txtNam
            // 
            this.txtNam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNam.Location = new System.Drawing.Point(64, 0);
            this.txtNam.Name = "txtNam";
            this.txtNam.Properties.Appearance.Options.UseTextOptions = true;
            this.txtNam.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtNam.Properties.MaxLength = 4;
            this.txtNam.Properties.NullText = "yyyy";
            this.txtNam.Size = new System.Drawing.Size(37, 20);
            this.txtNam.TabIndex = 2;
            this.txtNam.EditValueChanged += new System.EventHandler(this.txtNam_EditValueChanged);
            // 
            // ctlNgayThangNam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtNam);
            this.Controls.Add(this.txtThang);
            this.Controls.Add(this.txtNgay);
            this.Name = "ctlNgayThangNam";
            this.Size = new System.Drawing.Size(102, 20);
            ((System.ComponentModel.ISupportInitialize)(this.txtNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNam.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtNgay;
        private DevExpress.XtraEditors.TextEdit txtThang;
        private DevExpress.XtraEditors.TextEdit txtNam;
    }
}
