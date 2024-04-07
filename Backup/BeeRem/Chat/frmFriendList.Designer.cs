namespace BEEREMA.Chat
{
    partial class frmFriendList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFriendList));
            this.ctlFriendList1 = new BEEREMA.Chat.ctlFriendList();
            this.SuspendLayout();
            // 
            // ctlFriendList1
            // 
            this.ctlFriendList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlFriendList1.Location = new System.Drawing.Point(0, 0);
            this.ctlFriendList1.Name = "ctlFriendList1";
            this.ctlFriendList1.Size = new System.Drawing.Size(294, 405);
            this.ctlFriendList1.TabIndex = 0;
            // 
            // frmFriendList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 405);
            this.Controls.Add(this.ctlFriendList1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFriendList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách nhân viên";
            this.ResumeLayout(false);

        }

        #endregion

        private ctlFriendList ctlFriendList1;
    }
}