namespace BEE.HoatDong.BaoCao
{
    partial class frmReportTotalCall
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
            this.components = new System.ComponentModel.Container();
            this.gcReportCall = new DevExpress.XtraGrid.GridControl();
            this.grvReportCall = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKhop = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoDK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKhachHang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookLoaiBDS = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.lookCapDo = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.lookNguon = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemMemoExEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.repositoryItemMemoExEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.repositoryItemMemoExEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.repositoryItemMemoExEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.repositoryItemMemoExEdit7 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.repositoryItemMemoExEdit8 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.chkCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemKyBaoCao = new DevExpress.XtraBars.BarEditItem();
            this.cmbKyBaoCao = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemTuNgay = new DevExpress.XtraBars.BarEditItem();
            this.dateTuNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemDenNgay = new DevExpress.XtraBars.BarEditItem();
            this.dateDenNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemNhanVien = new DevExpress.XtraBars.BarEditItem();
            this.cboNhanVien = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.btnNap = new DevExpress.XtraBars.BarButtonItem();
            this.itemExport = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.chkTrangThai = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcReportCall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvReportCall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookLoaiBDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookCapDo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNguon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTrangThai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcReportCall
            // 
            this.gcReportCall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcReportCall.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcReportCall.Location = new System.Drawing.Point(2, 2);
            this.gcReportCall.MainView = this.grvReportCall;
            this.gcReportCall.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcReportCall.Name = "gcReportCall";
            this.gcReportCall.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookLoaiBDS,
            this.lookCapDo,
            this.lookNguon,
            this.repositoryItemMemoExEdit3,
            this.repositoryItemMemoExEdit4,
            this.repositoryItemMemoExEdit5,
            this.repositoryItemMemoExEdit6,
            this.repositoryItemMemoExEdit7,
            this.repositoryItemMemoExEdit8,
            this.chkCheck});
            this.gcReportCall.Size = new System.Drawing.Size(1377, 795);
            this.gcReportCall.TabIndex = 1;
            this.gcReportCall.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvReportCall});
            // 
            // grvReportCall
            // 
            this.grvReportCall.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSTT,
            this.colKhop,
            this.colSoDK,
            this.colKhachHang,
            this.gridColumn1});
            this.grvReportCall.GridControl = this.gcReportCall;
            this.grvReportCall.GroupPanelText = "Kéo cột lên đây để xem theo nhóm";
            this.grvReportCall.Name = "grvReportCall";
            this.grvReportCall.OptionsSelection.MultiSelect = true;
            this.grvReportCall.OptionsView.ColumnAutoWidth = false;
            this.grvReportCall.OptionsView.ShowAutoFilterRow = true;
            this.grvReportCall.OptionsView.ShowFooter = true;
            // 
            // colSTT
            // 
            this.colSTT.Caption = "STT";
            this.colSTT.FieldName = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.OptionsColumn.AllowEdit = false;
            this.colSTT.OptionsColumn.AllowFocus = false;
            this.colSTT.SummaryItem.DisplayFormat = "{0:#,0.##}";
            this.colSTT.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.colSTT.Visible = true;
            this.colSTT.VisibleIndex = 0;
            this.colSTT.Width = 66;
            // 
            // colKhop
            // 
            this.colKhop.Caption = "Tên nhân viên";
            this.colKhop.FieldName = "HoTen";
            this.colKhop.Name = "colKhop";
            this.colKhop.OptionsColumn.AllowEdit = false;
            this.colKhop.Visible = true;
            this.colKhop.VisibleIndex = 1;
            this.colKhop.Width = 186;
            // 
            // colSoDK
            // 
            this.colSoDK.Caption = "Tổng số cuộc goi";
            this.colSoDK.FieldName = "TotalCall";
            this.colSoDK.Name = "colSoDK";
            this.colSoDK.OptionsColumn.AllowEdit = false;
            this.colSoDK.OptionsColumn.AllowFocus = false;
            this.colSoDK.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colSoDK.Visible = true;
            this.colSoDK.VisibleIndex = 2;
            this.colSoDK.Width = 167;
            // 
            // colKhachHang
            // 
            this.colKhachHang.Caption = "Tổng thời gian gọi";
            this.colKhachHang.FieldName = "ThoiGianGoi";
            this.colKhachHang.Name = "colKhachHang";
            this.colKhachHang.OptionsColumn.AllowEdit = false;
            this.colKhachHang.OptionsColumn.AllowFocus = false;
            this.colKhachHang.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colKhachHang.Visible = true;
            this.colKhachHang.VisibleIndex = 3;
            this.colKhachHang.Width = 185;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Thời gian xuất báo cáo";
            this.gridColumn1.FieldName = "DateFilter";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 4;
            this.gridColumn1.Width = 202;
            // 
            // lookLoaiBDS
            // 
            this.lookLoaiBDS.AutoHeight = false;
            this.lookLoaiBDS.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookLoaiBDS.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenLBDS", "Name1")});
            this.lookLoaiBDS.DisplayMember = "TenLBDS";
            this.lookLoaiBDS.Name = "lookLoaiBDS";
            this.lookLoaiBDS.NullText = "";
            this.lookLoaiBDS.ShowHeader = false;
            this.lookLoaiBDS.ValueMember = "MaLBDS";
            // 
            // lookCapDo
            // 
            this.lookCapDo.AutoHeight = false;
            this.lookCapDo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookCapDo.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTT", "Name1")});
            this.lookCapDo.DisplayMember = "TenCD";
            this.lookCapDo.Name = "lookCapDo";
            this.lookCapDo.NullText = "";
            this.lookCapDo.ShowHeader = false;
            this.lookCapDo.ValueMember = "MaCD";
            // 
            // lookNguon
            // 
            this.lookNguon.AutoHeight = false;
            this.lookNguon.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookNguon.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenNguon", "Name1")});
            this.lookNguon.DisplayMember = "TenNguon";
            this.lookNguon.Name = "lookNguon";
            this.lookNguon.NullText = "";
            this.lookNguon.ShowHeader = false;
            this.lookNguon.ValueMember = "MaNguon";
            // 
            // repositoryItemMemoExEdit3
            // 
            this.repositoryItemMemoExEdit3.AutoHeight = false;
            this.repositoryItemMemoExEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit3.Name = "repositoryItemMemoExEdit3";
            this.repositoryItemMemoExEdit3.ShowIcon = false;
            // 
            // repositoryItemMemoExEdit4
            // 
            this.repositoryItemMemoExEdit4.AutoHeight = false;
            this.repositoryItemMemoExEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit4.Name = "repositoryItemMemoExEdit4";
            this.repositoryItemMemoExEdit4.ShowIcon = false;
            // 
            // repositoryItemMemoExEdit5
            // 
            this.repositoryItemMemoExEdit5.AutoHeight = false;
            this.repositoryItemMemoExEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit5.Name = "repositoryItemMemoExEdit5";
            this.repositoryItemMemoExEdit5.ShowIcon = false;
            // 
            // repositoryItemMemoExEdit6
            // 
            this.repositoryItemMemoExEdit6.AutoHeight = false;
            this.repositoryItemMemoExEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit6.Name = "repositoryItemMemoExEdit6";
            this.repositoryItemMemoExEdit6.ShowIcon = false;
            // 
            // repositoryItemMemoExEdit7
            // 
            this.repositoryItemMemoExEdit7.AutoHeight = false;
            this.repositoryItemMemoExEdit7.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit7.Name = "repositoryItemMemoExEdit7";
            this.repositoryItemMemoExEdit7.ShowIcon = false;
            // 
            // repositoryItemMemoExEdit8
            // 
            this.repositoryItemMemoExEdit8.AutoHeight = false;
            this.repositoryItemMemoExEdit8.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit8.Name = "repositoryItemMemoExEdit8";
            this.repositoryItemMemoExEdit8.ShowIcon = false;
            // 
            // chkCheck
            // 
            this.chkCheck.AutoHeight = false;
            this.chkCheck.Name = "chkCheck";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnNap,
            this.itemKyBaoCao,
            this.itemTuNgay,
            this.itemDenNgay,
            this.itemNhanVien,
            this.itemExport});
            this.barManager1.MaxItemId = 14;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbKyBaoCao,
            this.dateTuNgay,
            this.dateDenNgay,
            this.chkTrangThai,
            this.cboNhanVien});
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemKyBaoCao),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemTuNgay),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemDenNgay),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemNhanVien),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnNap, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemKyBaoCao
            // 
            this.itemKyBaoCao.Caption = "Kỳ cáo cáo";
            this.itemKyBaoCao.Edit = this.cmbKyBaoCao;
            this.itemKyBaoCao.Id = 2;
            this.itemKyBaoCao.Name = "itemKyBaoCao";
            this.itemKyBaoCao.Width = 100;
            // 
            // cmbKyBaoCao
            // 
            this.cmbKyBaoCao.AutoHeight = false;
            this.cmbKyBaoCao.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbKyBaoCao.Name = "cmbKyBaoCao";
            this.cmbKyBaoCao.NullText = "Kỳ cáo cáo";
            this.cmbKyBaoCao.EditValueChanged += new System.EventHandler(this.cmbKyBaoCao_EditValueChanged);
            // 
            // itemTuNgay
            // 
            this.itemTuNgay.Caption = "Từ ngày";
            this.itemTuNgay.Edit = this.dateTuNgay;
            this.itemTuNgay.Id = 4;
            this.itemTuNgay.Name = "itemTuNgay";
            this.itemTuNgay.Width = 86;
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.AutoHeight = false;
            this.dateTuNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.NullText = "Từ ngày";
            this.dateTuNgay.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // itemDenNgay
            // 
            this.itemDenNgay.Caption = "Đến ngày";
            this.itemDenNgay.Edit = this.dateDenNgay;
            this.itemDenNgay.Id = 5;
            this.itemDenNgay.Name = "itemDenNgay";
            this.itemDenNgay.Width = 84;
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.AutoHeight = false;
            this.dateDenNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.NullText = "Đến ngày";
            this.dateDenNgay.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // itemNhanVien
            // 
            this.itemNhanVien.Caption = "Nhân viên";
            this.itemNhanVien.Edit = this.cboNhanVien;
            this.itemNhanVien.Id = 12;
            this.itemNhanVien.Name = "itemNhanVien";
            this.itemNhanVien.Width = 120;
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.AutoHeight = false;
            this.cboNhanVien.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboNhanVien.DisplayMember = "HoTen";
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.SelectAllItemCaption = "Tất cả";
            this.cboNhanVien.ValueMember = "MaNV";
            // 
            // btnNap
            // 
            this.btnNap.Caption = "Nạp";
            this.btnNap.Glyph = global::BEE.HoatDong.Properties.Resources.refresh_blue;
            this.btnNap.Id = 0;
            this.btnNap.Name = "btnNap";
            this.btnNap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNap_ItemClick);
            // 
            // itemExport
            // 
            this.itemExport.Caption = "Export";
            this.itemExport.Glyph = global::BEE.HoatDong.Properties.Resources.export;
            this.itemExport.Id = 13;
            this.itemExport.Name = "itemExport";
            this.itemExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemExport_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlTop.Size = new System.Drawing.Size(1381, 35);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 834);
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlBottom.Size = new System.Drawing.Size(1381, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 35);
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 799);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1381, 35);
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 799);
            // 
            // chkTrangThai
            // 
            this.chkTrangThai.AutoHeight = false;
            this.chkTrangThai.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkTrangThai.DisplayMember = "TenTT";
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.SelectAllItemCaption = "Tất cả";
            this.chkTrangThai.ValueMember = "MaTT";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gcReportCall);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 35);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1381, 799);
            this.panelControl1.TabIndex = 6;
            // 
            // frmReportTotalCall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1381, 857);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmReportTotalCall";
            this.ShowIcon = false;
            this.Text = "Báo cáo tổng hợp cuộc gọi";
            this.Load += new System.EventHandler(this.frmReportTotalCall_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcReportCall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvReportCall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookLoaiBDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookCapDo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNguon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTrangThai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcReportCall;
        private DevExpress.XtraGrid.Views.Grid.GridView grvReportCall;
        private DevExpress.XtraGrid.Columns.GridColumn colKhop;
        private DevExpress.XtraGrid.Columns.GridColumn colSTT;
        private DevExpress.XtraGrid.Columns.GridColumn colSoDK;
        private DevExpress.XtraGrid.Columns.GridColumn colKhachHang;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookLoaiBDS;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookNguon;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit8;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit7;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit6;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit5;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookCapDo;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnNap;
        private DevExpress.XtraBars.BarEditItem itemKyBaoCao;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbKyBaoCao;
        private DevExpress.XtraBars.BarEditItem itemTuNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateTuNgay;
        private DevExpress.XtraBars.BarEditItem itemDenNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateDenNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit chkTrangThai;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkCheck;
        private DevExpress.XtraBars.BarEditItem itemNhanVien;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit cboNhanVien;
        private DevExpress.XtraBars.BarButtonItem itemExport;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}