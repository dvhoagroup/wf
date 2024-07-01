namespace BEE.HoatDong.MGL.Ban
{
    partial class frmCaiDatThuTu
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
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcCanBan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCanBan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSTT)).BeginInit();
            this.SuspendLayout();
            // 
            // gcCanBan
            // 
            this.gcCanBan.Location = new System.Drawing.Point(13, 13);
            this.gcCanBan.MainView = this.grvCanBan;
            this.gcCanBan.Name = "gcCanBan";
            this.gcCanBan.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.spinSTT});
            this.gcCanBan.Size = new System.Drawing.Size(268, 355);
            this.gcCanBan.TabIndex = 0;
            this.gcCanBan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvCanBan});
            // 
            // grvCanBan
            // 
            this.grvCanBan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
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
            this.gridColumn1.Caption = "STT";
            this.gridColumn1.ColumnEdit = this.spinSTT;
            this.gridColumn1.FieldName = "STT";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 47;
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
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên Cột";
            this.gridColumn2.FieldName = "Cot";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 161;
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(125, 374);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 23);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(206, 374);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 23);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // frmCaiDatThuTu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 409);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.gcCanBan);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmCaiDatThuTu";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cài đặt thứ tự cột cần bán";
            this.Load += new System.EventHandler(this.frmCaiDatThuTu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcCanBan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCanBan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSTT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcCanBan;
        private DevExpress.XtraGrid.Views.Grid.GridView grvCanBan;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinSTT;
    }
}