namespace BEE.NghiepVuKhac
{
    partial class ctlXa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlXa));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemTinh = new DevExpress.XtraBars.BarEditItem();
            this.lkTinh = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.itemAdd = new DevExpress.XtraBars.BarButtonItem();
            this.itemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.itemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gcXa = new DevExpress.XtraGrid.GridControl();
            this.gvXa = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTenHuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookUpHuyen2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.lookUpTinh = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkTinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcXa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvXa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpHuyen2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpTinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.itemRefresh,
            this.itemAdd,
            this.itemEdit,
            this.itemDelete,
            this.itemTinh});
            this.barManager1.MaxItemId = 5;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkTinh});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemTinh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemAdd, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemTinh
            // 
            this.itemTinh.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.itemTinh.Appearance.Options.UseFont = true;
            this.itemTinh.Caption = "Tỉnh";
            this.itemTinh.Edit = this.lkTinh;
            this.itemTinh.EditValue = 2;
            this.itemTinh.Id = 4;
            this.itemTinh.Name = "itemTinh";
            this.itemTinh.Width = 150;
            this.itemTinh.EditValueChanged += new System.EventHandler(this.itemTinh_EditValueChanged);
            // 
            // lkTinh
            // 
            this.lkTinh.AutoHeight = false;
            this.lkTinh.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkTinh.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTinh", "Name3")});
            this.lkTinh.DisplayMember = "TenTinh";
            this.lkTinh.Name = "lkTinh";
            this.lkTinh.NullText = "";
            this.lkTinh.ShowHeader = false;
            this.lkTinh.ShowLines = false;
            this.lkTinh.ValueMember = "MaTinh";
            // 
            // itemRefresh
            // 
            this.itemRefresh.Caption = "Nạp";
            this.itemRefresh.Id = 0;
            this.itemRefresh.ImageIndex = 0;
            this.itemRefresh.Name = "itemRefresh";
            this.itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemRefresh_ItemClick);
            // 
            // itemAdd
            // 
            this.itemAdd.Caption = "Thêm";
            this.itemAdd.Id = 1;
            this.itemAdd.ImageIndex = 1;
            this.itemAdd.Name = "itemAdd";
            this.itemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemAdd_ItemClick);
            // 
            // itemEdit
            // 
            this.itemEdit.Caption = "Sửa";
            this.itemEdit.Id = 2;
            this.itemEdit.ImageIndex = 2;
            this.itemEdit.Name = "itemEdit";
            this.itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemEdit_ItemClick);
            // 
            // itemDelete
            // 
            this.itemDelete.Caption = "Xóa";
            this.itemDelete.Id = 3;
            this.itemDelete.ImageIndex = 3;
            this.itemDelete.Name = "itemDelete";
            this.itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemDelete_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(750, 25);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 385);
            this.barDockControlBottom.Size = new System.Drawing.Size(750, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 25);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 360);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(750, 25);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 360);
            // 
            // gcXa
            // 
            this.gcXa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcXa.Location = new System.Drawing.Point(0, 25);
            this.gcXa.MainView = this.gvXa;
            this.gcXa.Name = "gcXa";
            this.gcXa.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookUpTinh,
            this.lookUpHuyen2});
            this.gcXa.Size = new System.Drawing.Size(750, 360);
            this.gcXa.TabIndex = 15;
            this.gcXa.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvXa});
            // 
            // gvXa
            // 
            this.gvXa.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTenHuong,
            this.gridColumn1});
            this.gvXa.GridControl = this.gcXa;
            this.gvXa.GroupPanelText = "Kéo một cột lên đây để xem theo nhóm";
            this.gvXa.IndicatorWidth = 35;
            this.gvXa.Name = "gvXa";
            this.gvXa.OptionsView.ShowAutoFilterRow = true;
            this.gvXa.OptionsView.ShowGroupPanel = false;
            this.gvXa.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvXa_CustomDrawRowIndicator);
            // 
            // colTenHuong
            // 
            this.colTenHuong.Caption = "Tên xã";
            this.colTenHuong.FieldName = "TenXa";
            this.colTenHuong.Name = "colTenHuong";
            this.colTenHuong.OptionsColumn.AllowEdit = false;
            this.colTenHuong.OptionsColumn.AllowFocus = false;
            this.colTenHuong.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colTenHuong.Visible = true;
            this.colTenHuong.VisibleIndex = 0;
            this.colTenHuong.Width = 589;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Quận (Huyện)";
            this.gridColumn1.ColumnEdit = this.lookUpHuyen2;
            this.gridColumn1.FieldName = "MaHuyen";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 205;
            // 
            // lookUpHuyen2
            // 
            this.lookUpHuyen2.AutoHeight = false;
            this.lookUpHuyen2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpHuyen2.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenHuyen", "TenHuyen")});
            this.lookUpHuyen2.DisplayMember = "TenHuyen";
            this.lookUpHuyen2.Name = "lookUpHuyen2";
            this.lookUpHuyen2.NullText = "";
            this.lookUpHuyen2.ShowHeader = false;
            this.lookUpHuyen2.ShowLines = false;
            this.lookUpHuyen2.ValueMember = "MaHuyen";
            // 
            // lookUpTinh
            // 
            this.lookUpTinh.AutoHeight = false;
            this.lookUpTinh.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpTinh.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TênTinh", "Name1")});
            this.lookUpTinh.DisplayMember = "TenTinh";
            this.lookUpTinh.Name = "lookUpTinh";
            this.lookUpTinh.NullText = "";
            this.lookUpTinh.ShowHeader = false;
            this.lookUpTinh.ValueMember = "MaTinh";
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "refresh4.png");
            this.imageCollection1.Images.SetKeyName(1, "add.png");
            this.imageCollection1.Images.SetKeyName(2, "edit.png");
            this.imageCollection1.Images.SetKeyName(3, "recyclebin.png");
            this.imageCollection1.Images.SetKeyName(4, "cancel.png");
            this.imageCollection1.Images.SetKeyName(5, "print3.png");
            this.imageCollection1.Images.SetKeyName(6, "Luu.png");
            this.imageCollection1.Images.SetKeyName(7, "OK.png");
            this.imageCollection1.Images.SetKeyName(8, "previous.png");
            this.imageCollection1.Images.SetKeyName(9, "next.png");
            this.imageCollection1.Images.SetKeyName(10, "delay.png");
            this.imageCollection1.Images.SetKeyName(11, "HELP2.png");
            this.imageCollection1.Images.SetKeyName(12, "import.png");
            this.imageCollection1.Images.SetKeyName(13, "excel.png");
            this.imageCollection1.Images.SetKeyName(14, "export5.png");
            this.imageCollection1.Images.SetKeyName(15, "print2.png");
            this.imageCollection1.Images.SetKeyName(16, "cart3.png");
            this.imageCollection1.Images.SetKeyName(17, "document.png");
            this.imageCollection1.Images.SetKeyName(18, "exit32.png");
            this.imageCollection1.Images.SetKeyName(19, "fiter.png");
            this.imageCollection1.Images.SetKeyName(20, "login.png");
            this.imageCollection1.Images.SetKeyName(21, "setting2.png");
            this.imageCollection1.Images.SetKeyName(22, "lock1.png");
            this.imageCollection1.Images.SetKeyName(23, "key.png");
            this.imageCollection1.Images.SetKeyName(24, "tuychon.png");
            this.imageCollection1.Images.SetKeyName(25, "tien.png");
            this.imageCollection1.Images.SetKeyName(26, "cart3.png");
            this.imageCollection1.Images.SetKeyName(27, "Alarm_Clock.png");
            this.imageCollection1.Images.SetKeyName(28, "download.png");
            // 
            // ctlXa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcXa);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ctlXa";
            this.Size = new System.Drawing.Size(750, 385);
            this.Load += new System.EventHandler(this.ctlXa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkTinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcXa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvXa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpHuyen2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpTinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem itemRefresh;
        private DevExpress.XtraBars.BarButtonItem itemAdd;
        private DevExpress.XtraBars.BarButtonItem itemEdit;
        private DevExpress.XtraBars.BarButtonItem itemDelete;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarEditItem itemTinh;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkTinh;
        private DevExpress.XtraGrid.GridControl gcXa;
        private DevExpress.XtraGrid.Views.Grid.GridView gvXa;
        private DevExpress.XtraGrid.Columns.GridColumn colTenHuong;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpHuyen2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpTinh;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}
