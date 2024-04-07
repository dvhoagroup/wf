namespace BEE.HoatDong.MGL.Ban
{
    partial class frmDuong
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
            this.gcDuong = new DevExpress.XtraGrid.GridControl();
            this.grvDuong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtDuong = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.itemAdd = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcDuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDuong.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcDuong
            // 
            this.gcDuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDuong.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcDuong.Location = new System.Drawing.Point(2, 2);
            this.gcDuong.MainView = this.grvDuong;
            this.gcDuong.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcDuong.Name = "gcDuong";
            this.gcDuong.Size = new System.Drawing.Size(520, 339);
            this.gcDuong.TabIndex = 0;
            this.gcDuong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDuong});
            // 
            // grvDuong
            // 
            this.grvDuong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.grvDuong.DetailHeight = 431;
            this.grvDuong.GridControl = this.gcDuong;
            this.grvDuong.Name = "grvDuong";
            this.grvDuong.OptionsFind.Condition = DevExpress.Data.Filtering.FilterCondition.Contains;
            this.grvDuong.OptionsView.ColumnAutoWidth = false;
            this.grvDuong.OptionsView.ShowAutoFilterRow = true;
            this.grvDuong.OptionsView.ShowGroupPanel = false;
            this.grvDuong.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvDuong_FocusedRowChanged);
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Mã đường";
            this.gridColumn4.FieldName = "MaDuong";
            this.gridColumn4.MinWidth = 25;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Width = 94;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tên đường";
            this.gridColumn1.FieldName = "TenDuong";
            this.gridColumn1.MinWidth = 23;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 231;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Ngày tạo";
            this.gridColumn2.FieldName = "CreateDate";
            this.gridColumn2.MinWidth = 24;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 129;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Nhân viên tạo";
            this.gridColumn3.FieldName = "HoTen";
            this.gridColumn3.MinWidth = 24;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 117;
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(364, 407);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(87, 28);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(459, 407);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(87, 28);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(270, 407);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(87, 28);
            this.btnXoa.TabIndex = 3;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gcDuong);
            this.panelControl1.Location = new System.Drawing.Point(24, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(524, 343);
            this.panelControl1.TabIndex = 4;
            // 
            // txtDuong
            // 
            this.txtDuong.Location = new System.Drawing.Point(129, 368);
            this.txtDuong.Name = "txtDuong";
            this.txtDuong.Size = new System.Drawing.Size(419, 22);
            this.txtDuong.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(26, 371);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(68, 17);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Tên đường";
            // 
            // itemAdd
            // 
            this.itemAdd.Location = new System.Drawing.Point(177, 407);
            this.itemAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.itemAdd.Name = "itemAdd";
            this.itemAdd.Size = new System.Drawing.Size(87, 28);
            this.itemAdd.TabIndex = 7;
            this.itemAdd.Text = "Thêm";
            this.itemAdd.Click += new System.EventHandler(this.itemAdd_Click);
            // 
            // frmDuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 459);
            this.Controls.Add(this.itemAdd);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtDuong);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.IconOptions.ShowIcon = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmDuong";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đường";
            this.Load += new System.EventHandler(this.frmDuong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcDuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDuong.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcDuong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDuong;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtDuong;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton itemAdd;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}