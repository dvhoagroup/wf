namespace BEE.BaoCao.GiaoDich
{
    partial class ShowGiaoDich_frm
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
            this.printCtl = new DevExpress.XtraPrinting.Control.PrintControl();
            this.SuspendLayout();
            // 
            // printCtl
            // 
            this.printCtl.BackColor = System.Drawing.Color.Empty;
            this.printCtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printCtl.ForeColor = System.Drawing.Color.Empty;
            this.printCtl.IsMetric = true;
            this.printCtl.Location = new System.Drawing.Point(0, 0);
            this.printCtl.Name = "printCtl";
            this.printCtl.Size = new System.Drawing.Size(784, 562);
            this.printCtl.TabIndex = 0;
            this.printCtl.TooltipFont = new System.Drawing.Font("Tahoma", 8.25F);
            // 
            // ShowGiaoDich_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.printCtl);
            this.Name = "ShowGiaoDich_frm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preview";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ShowGiaoDich_frm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraPrinting.Control.PrintControl printCtl;
    }
}