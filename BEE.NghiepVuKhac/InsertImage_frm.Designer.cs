namespace BEE.NghiepVuKhac
{
    partial class InsertImage_frm
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
            this.lblWait = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWait
            // 
            this.lblWait.AllowHtmlString = true;
            this.lblWait.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Strikeout);
            this.lblWait.Location = new System.Drawing.Point(84, 15);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(151, 30);
            this.lblWait.TabIndex = 1;
            this.lblWait.Text = "<b>Hệ thống đang xử lý</b><br>Vui lòng đợi trong giây lát <b>...</b>";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::BEE.NghiepVuKhac.Properties.Resources.loading;
            this.pictureEdit1.Location = new System.Drawing.Point(21, 13);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ReadOnly = true;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(38, 35);
            this.pictureEdit1.TabIndex = 2;
            // 
            // InsertImage_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundImageStore = global::BEE.NghiepVuKhac.Properties.Resources.waiting;
            this.ClientSize = new System.Drawing.Size(270, 60);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.lblWait);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InsertImage_frm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đang tải file, vui lòng đợi trong giây lát.";
            this.Shown += new System.EventHandler(this.InsertImage_frm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblWait;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
    }
}