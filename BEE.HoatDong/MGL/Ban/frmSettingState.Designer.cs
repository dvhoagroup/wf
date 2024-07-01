namespace BEE.HoatDong.MGL.Ban
{
    partial class frmSettingState
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
            this.gcCanBan = new DevExpress.XtraGrid.GridControl();
            this.grvCanBan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinSTT = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.cmbtrangthai = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.lkTrangThai = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinDuration = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ckIsactive = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCanBan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCanBan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbtrangthai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkTrangThai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckIsactive)).BeginInit();
            this.SuspendLayout();
            // 
            // gcCanBan
            // 
            this.gcCanBan.Location = new System.Drawing.Point(13, 13);
            this.gcCanBan.MainView = this.grvCanBan;
            this.gcCanBan.Name = "gcCanBan";
            this.gcCanBan.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.spinSTT,
            this.cmbtrangthai,
            this.lkTrangThai,
            this.spinDuration,
            this.ckIsactive});
            this.gcCanBan.Size = new System.Drawing.Size(495, 355);
            this.gcCanBan.TabIndex = 0;
            this.gcCanBan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvCanBan});
            // 
            // grvCanBan
            // 
            this.grvCanBan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.grvCanBan.GridControl = this.gcCanBan;
            this.grvCanBan.Name = "grvCanBan";
            this.grvCanBan.OptionsView.ColumnAutoWidth = false;
            this.grvCanBan.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvCanBan.OptionsView.ShowAutoFilterRow = true;
            this.grvCanBan.OptionsView.ShowGroupPanel = false;
            this.grvCanBan.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.grvCanBan_InvalidRowException);
            this.grvCanBan.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grvCanBan_ValidateRow);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Từ trạng thái";
            this.gridColumn1.ColumnEdit = this.lkTrangThai;
            this.gridColumn1.FieldName = "first_state_id";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 112;
            // 
            // spinSTT
            // 
            this.spinSTT.AutoHeight = false;
            this.spinSTT.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinSTT.DisplayFormat.FormatString = "{0:#,0.##}";
            this.spinSTT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinSTT.EditFormat.FormatString = "{0:#,0.##}";
            this.spinSTT.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinSTT.Name = "spinSTT";
            this.spinSTT.EditValueChanged += new System.EventHandler(this.spinSTT_EditValueChanged);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(352, 374);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 23);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(433, 374);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 23);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // cmbtrangthai
            // 
            this.cmbtrangthai.AutoHeight = false;
            this.cmbtrangthai.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbtrangthai.Name = "cmbtrangthai";
            // 
            // lkTrangThai
            // 
            this.lkTrangThai.AutoHeight = false;
            this.lkTrangThai.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkTrangThai.DisplayMember = "TenTT";
            this.lkTrangThai.Name = "lkTrangThai";
            this.lkTrangThai.NullText = "";
            this.lkTrangThai.ValueMember = "MaTT";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Đến trạng thái";
            this.gridColumn2.ColumnEdit = this.lkTrangThai;
            this.gridColumn2.FieldName = "last_state_id";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 122;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Thời gian(tiếng)";
            this.gridColumn3.ColumnEdit = this.spinDuration;
            this.gridColumn3.FieldName = "max_duration";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 98;
            // 
            // spinDuration
            // 
            this.spinDuration.AutoHeight = false;
            this.spinDuration.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinDuration.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinDuration.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinDuration.Name = "spinDuration";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Active";
            this.gridColumn4.ColumnEdit = this.ckIsactive;
            this.gridColumn4.FieldName = "isactive";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 81;
            // 
            // ckIsactive
            // 
            this.ckIsactive.AutoHeight = false;
            this.ckIsactive.Name = "ckIsactive";
            // 
            // frmSettingState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 400);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.gcCanBan);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmSettingState";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cài đặt thời gian chuyển trạng thái giao dịch";
            this.Load += new System.EventHandler(this.frmSettingState_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcCanBan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCanBan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbtrangthai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkTrangThai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckIsactive)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcCanBan;
        private DevExpress.XtraGrid.Views.Grid.GridView grvCanBan;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinSTT;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbtrangthai;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkTrangThai;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinDuration;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit ckIsactive;
    }
}