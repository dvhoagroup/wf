namespace BEE.NhanVien
{
    partial class frmCaiDatVungQuanLy
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
            this.treeViewRegion = new System.Windows.Forms.TreeView();
            this.ckbCheckAll = new DevExpress.XtraEditors.CheckEdit();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.ckhThuGonVung = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbCheckAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckhThuGonVung.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // treeViewRegion
            // 
            this.treeViewRegion.Location = new System.Drawing.Point(12, 12);
            this.treeViewRegion.Name = "treeViewRegion";
            this.treeViewRegion.Size = new System.Drawing.Size(588, 543);
            this.treeViewRegion.TabIndex = 4;
            this.treeViewRegion.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewRegion_AfterCheck);
            // 
            // ckbCheckAll
            // 
            this.ckbCheckAll.Location = new System.Drawing.Point(12, 568);
            this.ckbCheckAll.Name = "ckbCheckAll";
            this.ckbCheckAll.Properties.Caption = "Chọn tất cả";
            this.ckbCheckAll.Size = new System.Drawing.Size(88, 19);
            this.ckbCheckAll.TabIndex = 5;
            this.ckbCheckAll.CheckedChanged += new System.EventHandler(this.ckbCheckAll_CheckedChanged);
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.Location = new System.Drawing.Point(397, 564);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(89, 23);
            this.btnLuu.TabIndex = 6;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThoat.Location = new System.Drawing.Point(511, 564);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(89, 23);
            this.btnThoat.TabIndex = 7;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // ckhThuGonVung
            // 
            this.ckhThuGonVung.EditValue = true;
            this.ckhThuGonVung.Location = new System.Drawing.Point(115, 568);
            this.ckhThuGonVung.Name = "ckhThuGonVung";
            this.ckhThuGonVung.Properties.Caption = "Mở rộng tất cả";
            this.ckhThuGonVung.Size = new System.Drawing.Size(109, 19);
            this.ckhThuGonVung.TabIndex = 8;
            this.ckhThuGonVung.CheckedChanged += new System.EventHandler(this.ckhThuGonVung_CheckedChanged);
            // 
            // frmCaiDatVungQuanLy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 599);
            this.Controls.Add(this.ckhThuGonVung);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.ckbCheckAll);
            this.Controls.Add(this.treeViewRegion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmCaiDatVungQuanLy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cài đặt vùng quản lý";
            this.Load += new System.EventHandler(this.frmCaiDatVungQuanLy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ckbCheckAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckhThuGonVung.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewRegion;
        private DevExpress.XtraEditors.CheckEdit ckbCheckAll;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.CheckEdit ckhThuGonVung;
    }
}