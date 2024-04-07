namespace BEEREMA.BaoCao.Chart
{
    partial class ctlThuHoiCongNo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitSubPanel1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcContract = new DevExpress.XtraGrid.GridControl();
            this.gvContract = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.splitSubPanel2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcMoney = new DevExpress.XtraGrid.GridControl();
            this.gvMoney = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colName2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitSubPanel1)).BeginInit();
            this.splitSubPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcContract)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvContract)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitSubPanel2)).BeginInit();
            this.splitSubPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMoney)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMoney)).BeginInit();
            this.SuspendLayout();
            // 
            // colName
            // 
            this.colName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10F);
            this.colName.AppearanceCell.Options.UseFont = true;
            this.colName.AppearanceCell.Options.UseTextOptions = true;
            this.colName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colName.AppearanceHeader.Options.UseFont = true;
            this.colName.AppearanceHeader.Options.UseTextOptions = true;
            this.colName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colName.Caption = "Đợt mua";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.OptionsColumn.AllowMove = false;
            this.colName.OptionsColumn.AllowShowHide = false;
            this.colName.OptionsColumn.AllowSize = false;
            this.colName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colName.OptionsFilter.AllowAutoFilter = false;
            this.colName.OptionsFilter.AllowFilter = false;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 135;
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            this.splitMain.Panel1.Controls.Add(this.splitSubPanel1);
            this.splitMain.Panel1.Text = "Panel1";
            this.splitMain.Panel2.Controls.Add(this.splitSubPanel2);
            this.splitMain.Panel2.Text = "Panel2";
            this.splitMain.Size = new System.Drawing.Size(871, 489);
            this.splitMain.SplitterPosition = 419;
            this.splitMain.TabIndex = 0;
            this.splitMain.Text = "splitContainerControl1";
            // 
            // splitSubPanel1
            // 
            this.splitSubPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitSubPanel1.Horizontal = false;
            this.splitSubPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitSubPanel1.Name = "splitSubPanel1";
            this.splitSubPanel1.Panel1.Text = "Panel1";
            this.splitSubPanel1.Panel2.Controls.Add(this.gcContract);
            this.splitSubPanel1.Panel2.Text = "Panel2";
            this.splitSubPanel1.Size = new System.Drawing.Size(419, 489);
            this.splitSubPanel1.SplitterPosition = 224;
            this.splitSubPanel1.TabIndex = 0;
            this.splitSubPanel1.Text = "splitContainerControl1";
            // 
            // gcContract
            // 
            this.gcContract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcContract.Location = new System.Drawing.Point(0, 0);
            this.gcContract.MainView = this.gvContract;
            this.gcContract.Name = "gcContract";
            this.gcContract.Size = new System.Drawing.Size(419, 257);
            this.gcContract.TabIndex = 1;
            this.gcContract.ToolTipController = this.toolTipController1;
            this.gcContract.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvContract});
            // 
            // gvContract
            // 
            this.gvContract.ColumnPanelRowHeight = 35;
            this.gvContract.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colName,
            this.gridColumn4,
            this.gridColumn5});
            this.gvContract.GridControl = this.gcContract;
            this.gvContract.Name = "gvContract";
            this.gvContract.OptionsMenu.EnableColumnMenu = false;
            this.gvContract.OptionsMenu.EnableFooterMenu = false;
            this.gvContract.OptionsMenu.EnableGroupPanelMenu = false;
            this.gvContract.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gvContract.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvContract.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvContract.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gvContract.OptionsView.ColumnAutoWidth = false;
            this.gvContract.OptionsView.ShowGroupPanel = false;
            this.gvContract.OptionsView.ShowIndicator = false;
            this.gvContract.RowHeight = 30;
            this.gvContract.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvContract_RowCellStyle);
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10F);
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "HĐ đã thanh toán";
            this.gridColumn4.DisplayFormat.FormatString = "{0:n0}";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "Sold";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowMove = false;
            this.gridColumn4.OptionsColumn.AllowShowHide = false;
            this.gridColumn4.OptionsColumn.AllowSize = false;
            this.gridColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn4.OptionsFilter.AllowFilter = false;
            this.gridColumn4.SummaryItem.DisplayFormat = "{0:n0}";
            this.gridColumn4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 145;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10F);
            this.gridColumn5.AppearanceCell.Options.UseFont = true;
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "HĐ có công nợ";
            this.gridColumn5.DisplayFormat.FormatString = "{0:n0}";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "Deposit";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowMove = false;
            this.gridColumn5.OptionsColumn.AllowShowHide = false;
            this.gridColumn5.OptionsColumn.AllowSize = false;
            this.gridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn5.OptionsFilter.AllowFilter = false;
            this.gridColumn5.SummaryItem.DisplayFormat = "{0:n0}";
            this.gridColumn5.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 119;
            // 
            // toolTipController1
            // 
            this.toolTipController1.AutoPopDelay = 50000;
            this.toolTipController1.InitialDelay = 10;
            this.toolTipController1.Rounded = true;
            this.toolTipController1.RoundRadius = 4;
            // 
            // splitSubPanel2
            // 
            this.splitSubPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitSubPanel2.Horizontal = false;
            this.splitSubPanel2.Location = new System.Drawing.Point(0, 0);
            this.splitSubPanel2.Name = "splitSubPanel2";
            this.splitSubPanel2.Panel1.Text = "Panel1";
            this.splitSubPanel2.Panel2.Controls.Add(this.gcMoney);
            this.splitSubPanel2.Panel2.Text = "Panel2";
            this.splitSubPanel2.Size = new System.Drawing.Size(444, 489);
            this.splitSubPanel2.SplitterPosition = 227;
            this.splitSubPanel2.TabIndex = 0;
            this.splitSubPanel2.Text = "splitContainerControl2";
            // 
            // gcMoney
            // 
            this.gcMoney.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMoney.Location = new System.Drawing.Point(0, 0);
            this.gcMoney.MainView = this.gvMoney;
            this.gcMoney.Name = "gcMoney";
            this.gcMoney.Size = new System.Drawing.Size(444, 254);
            this.gcMoney.TabIndex = 2;
            this.gcMoney.ToolTipController = this.toolTipController1;
            this.gcMoney.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMoney});
            // 
            // gvMoney
            // 
            this.gvMoney.ColumnPanelRowHeight = 35;
            this.gvMoney.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colName2,
            this.gridColumn3,
            this.gridColumn6});
            this.gvMoney.GridControl = this.gcMoney;
            this.gvMoney.Name = "gvMoney";
            this.gvMoney.OptionsMenu.EnableColumnMenu = false;
            this.gvMoney.OptionsMenu.EnableFooterMenu = false;
            this.gvMoney.OptionsMenu.EnableGroupPanelMenu = false;
            this.gvMoney.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gvMoney.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvMoney.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvMoney.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gvMoney.OptionsView.ColumnAutoWidth = false;
            this.gvMoney.OptionsView.ShowGroupPanel = false;
            this.gvMoney.OptionsView.ShowIndicator = false;
            this.gvMoney.RowHeight = 30;
            this.gvMoney.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvMoney_RowCellStyle);
            // 
            // colName2
            // 
            this.colName2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10F);
            this.colName2.AppearanceCell.Options.UseFont = true;
            this.colName2.AppearanceCell.Options.UseTextOptions = true;
            this.colName2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colName2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colName2.AppearanceHeader.Options.UseFont = true;
            this.colName2.AppearanceHeader.Options.UseTextOptions = true;
            this.colName2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colName2.Caption = "Đợt mua";
            this.colName2.FieldName = "Name";
            this.colName2.Name = "colName2";
            this.colName2.OptionsColumn.AllowEdit = false;
            this.colName2.OptionsColumn.AllowMove = false;
            this.colName2.OptionsColumn.AllowShowHide = false;
            this.colName2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colName2.OptionsFilter.AllowAutoFilter = false;
            this.colName2.OptionsFilter.AllowFilter = false;
            this.colName2.Visible = true;
            this.colName2.VisibleIndex = 0;
            this.colName2.Width = 135;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10F);
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Số tiền đã thu";
            this.gridColumn3.DisplayFormat.FormatString = "{0:n0}";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "Sold";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowMove = false;
            this.gridColumn3.OptionsColumn.AllowShowHide = false;
            this.gridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            this.gridColumn3.SummaryItem.DisplayFormat = "{0:n0}";
            this.gridColumn3.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 120;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10F);
            this.gridColumn6.AppearanceCell.Options.UseFont = true;
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn6.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridColumn6.AppearanceHeader.Options.UseFont = true;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "Số tiền còn phải thu";
            this.gridColumn6.DisplayFormat.FormatString = "{0:n0}";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "Deposit";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowMove = false;
            this.gridColumn6.OptionsColumn.AllowShowHide = false;
            this.gridColumn6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn6.OptionsFilter.AllowFilter = false;
            this.gridColumn6.SummaryItem.DisplayFormat = "{0:n0}";
            this.gridColumn6.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 151;
            // 
            // ctlThuHoiCongNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitMain);
            this.Name = "ctlThuHoiCongNo";
            this.Size = new System.Drawing.Size(871, 489);
            this.Tag = "Tổng hợp thu hồi công nợ";
            this.Load += new System.EventHandler(this.ctlReference_Load);
            this.SizeChanged += new System.EventHandler(this.ctlThuHoiCongNo_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitSubPanel1)).EndInit();
            this.splitSubPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcContract)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvContract)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitSubPanel2)).EndInit();
            this.splitSubPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMoney)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMoney)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitMain;
        private DevExpress.XtraEditors.SplitContainerControl splitSubPanel1;
        private DevExpress.XtraEditors.SplitContainerControl splitSubPanel2;
        private DevExpress.XtraGrid.GridControl gcContract;
        private DevExpress.XtraGrid.Views.Grid.GridView gvContract;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.GridControl gcMoney;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMoney;
        private DevExpress.XtraGrid.Columns.GridColumn colName2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.Utils.ToolTipController toolTipController1;
    }
}
