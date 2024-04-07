namespace BEE.HoatDong.MGL.Ban
{
    partial class frmSyncQueue
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
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.itemRun = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lbTongBanGhiBDS = new System.Windows.Forms.Label();
            this.lbThanhCong = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.itemSyncError = new DevExpress.XtraEditors.SimpleButton();
            this.lbNotSync = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.spinSoLuong = new DevExpress.XtraEditors.SpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoLuong.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(395, 112);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 23);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Text = "EXIT";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // itemRun
            // 
            this.itemRun.Location = new System.Drawing.Point(314, 112);
            this.itemRun.Name = "itemRun";
            this.itemRun.Size = new System.Drawing.Size(75, 23);
            this.itemRun.TabIndex = 7;
            this.itemRun.Text = "SYNC ALL";
            this.itemRun.Click += new System.EventHandler(this.itemRun_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Tổng số bản ghi:";
            // 
            // lbTongBanGhiBDS
            // 
            this.lbTongBanGhiBDS.AutoSize = true;
            this.lbTongBanGhiBDS.Location = new System.Drawing.Point(146, 9);
            this.lbTongBanGhiBDS.Name = "lbTongBanGhiBDS";
            this.lbTongBanGhiBDS.Size = new System.Drawing.Size(15, 13);
            this.lbTongBanGhiBDS.TabIndex = 8;
            this.lbTongBanGhiBDS.Text = "[]";
            // 
            // lbThanhCong
            // 
            this.lbThanhCong.AutoSize = true;
            this.lbThanhCong.Location = new System.Drawing.Point(146, 35);
            this.lbThanhCong.Name = "lbThanhCong";
            this.lbThanhCong.Size = new System.Drawing.Size(15, 13);
            this.lbThanhCong.TabIndex = 9;
            this.lbThanhCong.Text = "[]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Thành công: ";
            // 
            // itemSyncError
            // 
            this.itemSyncError.Location = new System.Drawing.Point(395, 152);
            this.itemSyncError.Name = "itemSyncError";
            this.itemSyncError.Size = new System.Drawing.Size(75, 23);
            this.itemSyncError.TabIndex = 7;
            this.itemSyncError.Text = "SYNC ERROR";
            this.itemSyncError.Visible = false;
            this.itemSyncError.Click += new System.EventHandler(this.itemSyncError_Click);
            // 
            // lbNotSync
            // 
            this.lbNotSync.AutoSize = true;
            this.lbNotSync.Location = new System.Drawing.Point(146, 62);
            this.lbNotSync.Name = "lbNotSync";
            this.lbNotSync.Size = new System.Drawing.Size(15, 13);
            this.lbNotSync.TabIndex = 11;
            this.lbNotSync.Text = "[]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Chưa đồng bộ:";
            // 
            // spinSoLuong
            // 
            this.spinSoLuong.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinSoLuong.Location = new System.Drawing.Point(208, 114);
            this.spinSoLuong.Name = "spinSoLuong";
            this.spinSoLuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinSoLuong.Size = new System.Drawing.Size(100, 20);
            this.spinSoLuong.TabIndex = 13;
            // 
            // frmSyncQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 187);
            this.Controls.Add(this.spinSoLuong);
            this.Controls.Add(this.lbNotSync);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbThanhCong);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbTongBanGhiBDS);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.itemSyncError);
            this.Controls.Add(this.itemRun);
            this.Controls.Add(this.btnHuy);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmSyncQueue";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sync Queue";
            this.Load += new System.EventHandler(this.frmSyncQueue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spinSoLuong.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton itemRun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbTongBanGhiBDS;
        private System.Windows.Forms.Label lbThanhCong;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton itemSyncError;
        private System.Windows.Forms.Label lbNotSync;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SpinEdit spinSoLuong;
    }
}