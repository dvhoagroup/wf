namespace BEE.HoatDong.MGL.SDK.PhotoViewer
{
    partial class Form1
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
            this.photoEditor1 = new BEE.HoatDong.MGL.SDK.PhotoViewer.PhotoEditor();
            this.SuspendLayout();
            // 
            // photoEditor1
            // 
            this.photoEditor1.AllowAddPicture = true;
            this.photoEditor1.AllowDeletePicture = false;
            this.photoEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.photoEditor1.IndexImage = 0;
            this.photoEditor1.Location = new System.Drawing.Point(0, 0);
            this.photoEditor1.MaBC = null;
            this.photoEditor1.Name = "photoEditor1";
            this.photoEditor1.Size = new System.Drawing.Size(891, 489);
            this.photoEditor1.TabIndex = 0;
            this.photoEditor1.URL_IMAGE = "";
            // 
            // Form1
            // 
            this.Appearance.BackColor = System.Drawing.Color.Gray;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 489);
            this.Controls.Add(this.photoEditor1);
            this.Name = "Form1";
            this.Text = "Photo Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PhotoEditor photoEditor1;
    }
}

