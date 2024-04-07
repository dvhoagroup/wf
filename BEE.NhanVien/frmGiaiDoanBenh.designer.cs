namespace BEE.KHACHHANG
{
    partial class frmGiaiDoanBenh
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
            this.grdGiaiDoanBenh = new DevExpress.XtraGrid.GridControl();
            this.grvGiaiDoanBenh = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmMaGDB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmMaLB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmTenGDB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cbxLoaiBenh = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdGiaiDoanBenh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvGiaiDoanBenh)).BeginInit();
            this.SuspendLayout();
            // 
            // grdGiaiDoanBenh
            // 
            this.grdGiaiDoanBenh.Location = new System.Drawing.Point(12, 54);
            this.grdGiaiDoanBenh.MainView = this.grvGiaiDoanBenh;
            this.grdGiaiDoanBenh.Name = "grdGiaiDoanBenh";
            this.grdGiaiDoanBenh.Size = new System.Drawing.Size(360, 465);
            this.grdGiaiDoanBenh.TabIndex = 0;
            this.grdGiaiDoanBenh.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvGiaiDoanBenh});
            // 
            // grvGiaiDoanBenh
            // 
            this.grvGiaiDoanBenh.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmMaGDB,
            this.clmMaLB,
            this.clmTenGDB});
            this.grvGiaiDoanBenh.GridControl = this.grdGiaiDoanBenh;
            this.grvGiaiDoanBenh.Name = "grvGiaiDoanBenh";
            this.grvGiaiDoanBenh.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvGiaiDoanBenh.OptionsView.ShowAutoFilterRow = true;
            this.grvGiaiDoanBenh.OptionsView.ShowGroupPanel = false;
            this.grvGiaiDoanBenh.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grvGaiDoanBenh_KeyUp);
            // 
            // clmMaGDB
            // 
            this.clmMaGDB.Caption = "Mã GĐB";
            this.clmMaGDB.FieldName = "MaGDB";
            this.clmMaGDB.Name = "clmMaGDB";
            // 
            // clmMaLB
            // 
            this.clmMaLB.Caption = "Mã LB";
            this.clmMaLB.FieldName = "MaLB";
            this.clmMaLB.Name = "clmMaLB";
            // 
            // clmTenGDB
            // 
            this.clmTenGDB.Caption = "Tên Giai Đoạn Bệnh";
            this.clmTenGDB.FieldName = "TenGDB";
            this.clmTenGDB.Name = "clmTenGDB";
            this.clmTenGDB.Visible = true;
            this.clmTenGDB.VisibleIndex = 0;
            this.clmTenGDB.Width = 300;
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(216, 526);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 23);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(297, 526);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 23);
            this.btnXoa.TabIndex = 1;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 17);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(46, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Loại bệnh";
            // 
            // cbxLoaiBenh
            // 
            this.cbxLoaiBenh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLoaiBenh.FormattingEnabled = true;
            this.cbxLoaiBenh.Location = new System.Drawing.Point(75, 14);
            this.cbxLoaiBenh.Name = "cbxLoaiBenh";
            this.cbxLoaiBenh.Size = new System.Drawing.Size(297, 21);
            this.cbxLoaiBenh.TabIndex = 4;
            this.cbxLoaiBenh.SelectedIndexChanged += new System.EventHandler(this.cbxLoaiBenh_SelectedIndexChanged);
            // 
            // frmGiaiDoanBenh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 561);
            this.Controls.Add(this.cbxLoaiBenh);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.grdGiaiDoanBenh);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGiaiDoanBenh";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Giai đoạn bệnh";
            this.Load += new System.EventHandler(this.frmGaiDoanBenh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdGiaiDoanBenh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvGiaiDoanBenh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdGiaiDoanBenh;
        private DevExpress.XtraGrid.Views.Grid.GridView grvGiaiDoanBenh;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
        private DevExpress.XtraGrid.Columns.GridColumn clmMaGDB;
        private DevExpress.XtraGrid.Columns.GridColumn clmMaLB;
        private DevExpress.XtraGrid.Columns.GridColumn clmTenGDB;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ComboBox cbxLoaiBenh;
    }
}