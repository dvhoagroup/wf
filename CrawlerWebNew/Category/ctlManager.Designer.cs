namespace CrawlerWebNew.Category
{
    partial class ctlManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlManager));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemWebSite = new DevExpress.XtraBars.BarEditItem();
            this.lookWebSite = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemTinh = new DevExpress.XtraBars.BarEditItem();
            this.lookTinh = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.itemLuu = new DevExpress.XtraBars.BarButtonItem();
            this.itemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.itemAgree = new DevExpress.XtraBars.BarButtonItem();
            this.itemDisagree = new DevExpress.XtraBars.BarButtonItem();
            this.itemSaceDetail = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu();
            this.itemSave = new DevExpress.XtraGrid.GridControl();
            this.gvCategory = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookNhomTin = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookHuyen = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookChuyenMuc = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.lookParentCat = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookWebSite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNhomTin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookHuyen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookChuyenMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookParentCat)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.AllowMoveBarOnToolbar = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.AllowShowToolbarsPopup = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.itemDelete,
            this.itemRefresh,
            this.itemAgree,
            this.itemDisagree,
            this.itemSaceDetail,
            this.itemWebSite,
            this.itemLuu,
            this.itemTinh});
            this.barManager1.MaxItemId = 13;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookWebSite,
            this.lookTinh});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemWebSite),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemTinh),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemLuu, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemWebSite
            // 
            this.itemWebSite.Caption = "Website";
            this.itemWebSite.Edit = this.lookWebSite;
            this.itemWebSite.Id = 10;
            this.itemWebSite.Name = "itemWebSite";
            this.itemWebSite.Width = 204;
            this.itemWebSite.EditValueChanged += new System.EventHandler(this.itemWebSite_EditValueChanged);
            // 
            // lookWebSite
            // 
            this.lookWebSite.AutoHeight = false;
            this.lookWebSite.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookWebSite.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "WebSite"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Url", "Link")});
            this.lookWebSite.DisplayMember = "Name";
            this.lookWebSite.Name = "lookWebSite";
            this.lookWebSite.NullText = "[Chọn Website ...]";
            this.lookWebSite.ValueMember = "ID";
            // 
            // itemTinh
            // 
            this.itemTinh.Caption = "barEditItem1";
            this.itemTinh.Edit = this.lookTinh;
            this.itemTinh.Id = 12;
            this.itemTinh.Name = "itemTinh";
            this.itemTinh.Width = 114;
            this.itemTinh.EditValueChanged += new System.EventHandler(this.itemTinh_EditValueChanged);
            // 
            // lookTinh
            // 
            this.lookTinh.AutoHeight = false;
            this.lookTinh.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookTinh.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTinh", "Tỉnh")});
            this.lookTinh.DisplayMember = "TenTinh";
            this.lookTinh.Name = "lookTinh";
            this.lookTinh.NullText = "Tỉnh";
            this.lookTinh.ValueMember = "MaTinh";
            // 
            // itemRefresh
            // 
            this.itemRefresh.Caption = "Refresh";
            this.itemRefresh.Id = 3;
            this.itemRefresh.ImageIndex = 3;
            this.itemRefresh.Name = "itemRefresh";
            this.itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemRefresh_ItemClick);
            // 
            // itemLuu
            // 
            this.itemLuu.Caption = "Lưu";
            this.itemLuu.Id = 11;
            this.itemLuu.ImageIndex = 6;
            this.itemLuu.Name = "itemLuu";
            this.itemLuu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemLuu_ItemClick);
            // 
            // itemDelete
            // 
            this.itemDelete.Caption = "Xóa";
            this.itemDelete.Id = 2;
            this.itemDelete.ImageIndex = 2;
            this.itemDelete.Name = "itemDelete";
            this.itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemDelete_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1093, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 478);
            this.barDockControlBottom.Size = new System.Drawing.Size(1093, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 447);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1093, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 447);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "add.png");
            this.imageCollection1.Images.SetKeyName(1, "edit-icon.png");
            this.imageCollection1.Images.SetKeyName(2, "recyclebin.png");
            this.imageCollection1.Images.SetKeyName(3, "refresh4.png");
            this.imageCollection1.Images.SetKeyName(4, "cancel.png");
            this.imageCollection1.Images.SetKeyName(5, "print3.png");
            this.imageCollection1.Images.SetKeyName(6, "Luu.png");
            this.imageCollection1.Images.SetKeyName(7, "OK.png");
            this.imageCollection1.Images.SetKeyName(8, "print1.png");
            this.imageCollection1.Images.SetKeyName(9, "delay.png");
            this.imageCollection1.Images.SetKeyName(10, "export5.png");
            this.imageCollection1.Images.SetKeyName(11, "lock1.png");
            this.imageCollection1.Images.SetKeyName(12, "login.png");
            this.imageCollection1.Images.SetKeyName(13, "key.png");
            this.imageCollection1.Images.SetKeyName(14, "baogia.png");
            this.imageCollection1.Images.SetKeyName(15, "tien.png");
            this.imageCollection1.Images.SetKeyName(16, "UPDATE.png");
            // 
            // itemAgree
            // 
            this.itemAgree.Caption = "Agree";
            this.itemAgree.Id = 7;
            this.itemAgree.Name = "itemAgree";
            // 
            // itemDisagree
            // 
            this.itemDisagree.Caption = "Disagree";
            this.itemDisagree.Id = 8;
            this.itemDisagree.Name = "itemDisagree";
            // 
            // itemSaceDetail
            // 
            this.itemSaceDetail.Caption = "Cập nhật chi tiết";
            this.itemSaceDetail.Id = 9;
            this.itemSaceDetail.ImageIndex = 16;
            this.itemSaceDetail.Name = "itemSaceDetail";
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemSaceDetail)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.MenuCaption = "Tùy chọn";
            this.popupMenu1.Name = "popupMenu1";
            this.popupMenu1.ShowCaption = true;
            // 
            // itemSave
            // 
            this.itemSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemSave.Location = new System.Drawing.Point(0, 31);
            this.itemSave.MainView = this.gvCategory;
            this.itemSave.MenuManager = this.barManager1;
            this.itemSave.Name = "itemSave";
            this.itemSave.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookParentCat,
            this.lookNhomTin,
            this.lookHuyen,
            this.lookChuyenMuc});
            this.itemSave.ShowOnlyPredefinedDetails = true;
            this.itemSave.Size = new System.Drawing.Size(1093, 447);
            this.itemSave.TabIndex = 14;
            this.itemSave.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCategory});
            // 
            // gvCategory
            // 
            this.gvCategory.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12});
            this.gvCategory.GridControl = this.itemSave;
            this.gvCategory.Name = "gvCategory";
            this.gvCategory.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvCategory.OptionsView.ColumnAutoWidth = false;
            this.gvCategory.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvCategory.OptionsView.ShowAutoFilterRow = true;
            this.gvCategory.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvCategory_InitNewRow);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Name";
            this.gridColumn1.FieldName = "Name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 4;
            this.gridColumn1.Width = 183;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "WebID";
            this.gridColumn2.FieldName = "WebID";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "STT";
            this.gridColumn5.FieldName = "STT";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 50;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Loại nhu cầu";
            this.gridColumn6.FieldName = "IsBanThue_MuaThue";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Link ";
            this.gridColumn7.FieldName = "LinkCat";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            this.gridColumn7.Width = 252;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Lấy dữ liệu";
            this.gridColumn8.FieldName = "IsGetData";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            this.gridColumn8.Width = 64;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Nhóm tin";
            this.gridColumn9.ColumnEdit = this.lookNhomTin;
            this.gridColumn9.FieldName = "GroupID";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 2;
            this.gridColumn9.Width = 89;
            // 
            // lookNhomTin
            // 
            this.lookNhomTin.AutoHeight = false;
            this.lookNhomTin.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookNhomTin.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Nhóm")});
            this.lookNhomTin.DisplayMember = "Name";
            this.lookNhomTin.Name = "lookNhomTin";
            this.lookNhomTin.NullText = "";
            this.lookNhomTin.ValueMember = "ID";
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Huyện";
            this.gridColumn10.ColumnEdit = this.lookHuyen;
            this.gridColumn10.FieldName = "MaHuyen";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 1;
            this.gridColumn10.Width = 102;
            // 
            // lookHuyen
            // 
            this.lookHuyen.AutoHeight = false;
            this.lookHuyen.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookHuyen.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenHuyen", "Huyện")});
            this.lookHuyen.DisplayMember = "TenHuyen";
            this.lookHuyen.Name = "lookHuyen";
            this.lookHuyen.NullText = "";
            this.lookHuyen.ValueMember = "MaHuyen";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Tinh";
            this.gridColumn11.FieldName = "MaTinh";
            this.gridColumn11.Name = "gridColumn11";
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Chuyên mục";
            this.gridColumn12.ColumnEdit = this.lookChuyenMuc;
            this.gridColumn12.FieldName = "MaHangMuc";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 3;
            this.gridColumn12.Width = 162;
            // 
            // lookChuyenMuc
            // 
            this.lookChuyenMuc.AutoHeight = false;
            this.lookChuyenMuc.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookChuyenMuc.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Chuyên mục")});
            this.lookChuyenMuc.DisplayMember = "Name";
            this.lookChuyenMuc.Name = "lookChuyenMuc";
            this.lookChuyenMuc.NullText = "";
            this.lookChuyenMuc.ValueMember = "ID";
            // 
            // lookParentCat
            // 
            this.lookParentCat.AutoHeight = false;
            this.lookParentCat.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookParentCat.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Tên Menu")});
            this.lookParentCat.DisplayMember = "Name";
            this.lookParentCat.Name = "lookParentCat";
            this.lookParentCat.NullText = "";
            this.lookParentCat.ValueMember = "ID";
            // 
            // ctlManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.itemSave);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ctlManager";
            this.Size = new System.Drawing.Size(1093, 478);
            this.Tag = "CATEGORY";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookWebSite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNhomTin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookHuyen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookChuyenMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookParentCat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem itemRefresh;
        private DevExpress.XtraBars.BarButtonItem itemDelete;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem itemAgree;
        private DevExpress.XtraBars.BarButtonItem itemDisagree;
        private DevExpress.XtraBars.BarButtonItem itemSaceDetail;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarEditItem itemWebSite;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookWebSite;
        private DevExpress.XtraGrid.GridControl itemSave;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCategory;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraBars.BarButtonItem itemLuu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookParentCat;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookNhomTin;
        private DevExpress.XtraBars.BarEditItem itemTinh;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookTinh;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookHuyen;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookChuyenMuc;
    }
}
