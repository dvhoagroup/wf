namespace BEE.KhachHang
{
    partial class ShowCustomer_frm
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
            this.khachHang_ctl1 = new BEE.KhachHang.KhachHang_ctl();
            this.SuspendLayout();
            // 
            // khachHang_ctl1
            // 
            this.khachHang_ctl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.khachHang_ctl1.Location = new System.Drawing.Point(0, 0);
            this.khachHang_ctl1.Name = "khachHang_ctl1";
            this.khachHang_ctl1.Size = new System.Drawing.Size(889, 558);
            this.khachHang_ctl1.TabIndex = 0;
            // 
            // ShowCustomer_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 558);
            this.Controls.Add(this.khachHang_ctl1);
            this.Name = "ShowCustomer_frm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin khách hàng";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ShowCustomer_frm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private KhachHang_ctl khachHang_ctl1;
    }
}