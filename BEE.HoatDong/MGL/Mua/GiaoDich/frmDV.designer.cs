namespace BEE.HoatDong.MGL.Mua.GiaoDich
{
    partial class frmDV
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
            this.gcLoaiDuong = new DevExpress.XtraGrid.GridControl();
            this.gvLoaiDuong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkLoaiDV = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spSoLuong = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spDonGia = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcLoaiDuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoaiDuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDonGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcLoaiDuong
            // 
            this.gcLoaiDuong.Location = new System.Drawing.Point(12, 12);
            this.gcLoaiDuong.MainView = this.gvLoaiDuong;
            this.gcLoaiDuong.Name = "gcLoaiDuong";
            this.gcLoaiDuong.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemColorEdit1,
            this.lkLoaiDV,
            this.spSoLuong,
            this.spDonGia});
            this.gcLoaiDuong.ShowOnlyPredefinedDetails = true;
            this.gcLoaiDuong.Size = new System.Drawing.Size(475, 320);
            this.gcLoaiDuong.TabIndex = 0;
            this.gcLoaiDuong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLoaiDuong});
            // 
            // gvLoaiDuong
            // 
            this.gvLoaiDuong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gvLoaiDuong.GridControl = this.gcLoaiDuong;
            this.gvLoaiDuong.Name = "gvLoaiDuong";
            this.gvLoaiDuong.OptionsSelection.MultiSelect = true;
            this.gvLoaiDuong.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvLoaiDuong.OptionsView.ShowGroupPanel = false;
            this.gvLoaiDuong.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvLoaiDuong_InitNewRow);
            this.gvLoaiDuong.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gvLoaiDuong_KeyUp);
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Loại dịch vụ";
            this.gridColumn2.ColumnEdit = this.lkLoaiDV;
            this.gridColumn2.FieldName = "MaLDV";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 340;
            // 
            // lkLoaiDV
            // 
            this.lkLoaiDV.AutoHeight = false;
            this.lkLoaiDV.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkLoaiDV.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenLoaiDV", "Loại dịch vụ")});
            this.lkLoaiDV.DisplayMember = "TenLoaiDV";
            this.lkLoaiDV.Name = "lkLoaiDV";
            this.lkLoaiDV.NullText = "";
            this.lkLoaiDV.ValueMember = "ID";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "MaLD";
            this.gridColumn1.FieldName = "GDID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Số lượng";
            this.gridColumn3.ColumnEdit = this.spSoLuong;
            this.gridColumn3.FieldName = "SoLuong";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 265;
            // 
            // spSoLuong
            // 
            this.spSoLuong.AutoHeight = false;
            this.spSoLuong.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spSoLuong.DisplayFormat.FormatString = "{0:#,0.##}";
            this.spSoLuong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spSoLuong.Name = "spSoLuong";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Đơn giá";
            this.gridColumn4.ColumnEdit = this.spDonGia;
            this.gridColumn4.FieldName = "DonGia";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 265;
            // 
            // spDonGia
            // 
            this.spDonGia.AutoHeight = false;
            this.spDonGia.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spDonGia.DisplayFormat.FormatString = "{0:#,0.##}";
            this.spDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spDonGia.Name = "spDonGia";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Thành tiền";
            this.gridColumn5.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "ThanhTien";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 272;
            // 
            // repositoryItemColorEdit1
            // 
            this.repositoryItemColorEdit1.AutoHeight = false;
            this.repositoryItemColorEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemColorEdit1.ColorText = DevExpress.XtraEditors.Controls.ColorText.Integer;
            this.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1";
            this.repositoryItemColorEdit1.StoreColorAsInteger = true;
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(412, 340);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 23);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(313, 340);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(93, 23);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu && Đóng";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Location = new System.Drawing.Point(26, 348);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(0, 13);
            this.labelControl1.TabIndex = 2;
            // 
            // frmDV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 373);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.gcLoaiDuong);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmDV";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dịch vụ";
            this.Load += new System.EventHandler(this.frmNguon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcLoaiDuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoaiDuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDonGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcLoaiDuong;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLoaiDuong;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorEdit repositoryItemColorEdit1;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkLoaiDV;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spSoLuong;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spDonGia;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
    }
}