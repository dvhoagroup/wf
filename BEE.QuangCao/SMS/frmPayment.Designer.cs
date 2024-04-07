namespace BEE.QuangCao.SMS
{
    partial class frmPayment
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
            this.webPayment = new it.ExtendedWebBrowser();
            this.SuspendLayout();
            // 
            // webPayment
            // 
            this.webPayment.AllowWebBrowserDrop = false;
            this.webPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webPayment.IsWebBrowserContextMenuEnabled = false;
            this.webPayment.Location = new System.Drawing.Point(0, 0);
            this.webPayment.MinimumSize = new System.Drawing.Size(20, 20);
            this.webPayment.Name = "webPayment";
            this.webPayment.ScriptErrorsSuppressed = true;
            this.webPayment.Size = new System.Drawing.Size(992, 566);
            this.webPayment.TabIndex = 0;
            this.webPayment.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webPayment_DocumentCompleted);
            // 
            // frmPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 566);
            this.Controls.Add(this.webPayment);
            this.Name = "frmPayment";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cổng thanh toán điện tử Bảo Kim";
            this.Load += new System.EventHandler(this.frmPayment_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private it.ExtendedWebBrowser webPayment;
    }
}