﻿namespace BEE.TaiLieu
{
    partial class ctlTaiLieu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlTaiLieu));
            this.gcTaiLieu = new DevExpress.XtraGrid.GridControl();
            this.grvTaiLieu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.itemThem = new DevExpress.XtraBars.BarButtonItem();
            this.itemSua = new DevExpress.XtraBars.BarButtonItem();
            this.itemXoa = new DevExpress.XtraBars.BarButtonItem();
            this.itemXem = new DevExpress.XtraBars.BarButtonItem();
            this.itemTaiVe = new DevExpress.XtraBars.BarButtonItem();
            this.popupTaiLieu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcTaiLieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTaiLieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupTaiLieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcTaiLieu
            // 
            this.gcTaiLieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTaiLieu.Location = new System.Drawing.Point(0, 0);
            this.gcTaiLieu.MainView = this.grvTaiLieu;
            this.gcTaiLieu.Name = "gcTaiLieu";
            this.gcTaiLieu.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1,
            this.repositoryItemLookUpEdit1});
            this.gcTaiLieu.Size = new System.Drawing.Size(999, 285);
            this.gcTaiLieu.TabIndex = 0;
            this.gcTaiLieu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTaiLieu});
            // 
            // grvTaiLieu
            // 
            this.grvTaiLieu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10});
            this.grvTaiLieu.GridControl = this.gcTaiLieu;
            this.grvTaiLieu.Name = "grvTaiLieu";
            this.grvTaiLieu.OptionsCustomization.AllowGroup = false;
            this.grvTaiLieu.OptionsView.ColumnAutoWidth = false;
            this.grvTaiLieu.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Ký hiệu";
            this.gridColumn1.FieldName = "KyHieu";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 85;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên tài liệu";
            this.gridColumn2.FieldName = "TenTL";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 257;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Diễn giải";
            this.gridColumn3.ColumnEdit = this.repositoryItemMemoExEdit1;
            this.gridColumn3.FieldName = "DienGiai";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 300;
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            this.repositoryItemMemoExEdit1.PopupFormSize = new System.Drawing.Size(300, 0);
            this.repositoryItemMemoExEdit1.ReadOnly = true;
            this.repositoryItemMemoExEdit1.ShowIcon = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Nhân viên";
            this.gridColumn4.FieldName = "HoTen";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 155;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Ngày cập nhật";
            this.gridColumn5.FieldName = "NgayTao";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 162;
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.itemThem,
            this.itemSua,
            this.itemXoa,
            this.itemXem,
            this.itemTaiVe});
            this.barManager1.MaxItemId = 5;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(999, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 285);
            this.barDockControlBottom.Size = new System.Drawing.Size(999, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 285);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(999, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 285);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "add.png");
            this.imageCollection1.Images.SetKeyName(1, "recyclebin.png");
            this.imageCollection1.Images.SetKeyName(2, "edit-icon.png");
            this.imageCollection1.Images.SetKeyName(3, "print3.png");
            this.imageCollection1.Images.SetKeyName(4, "cancel.png");
            this.imageCollection1.Images.SetKeyName(5, "refresh4.png");
            this.imageCollection1.Images.SetKeyName(6, "Luu.png");
            this.imageCollection1.Images.SetKeyName(7, "OK.png");
            this.imageCollection1.Images.SetKeyName(8, "print1.png");
            this.imageCollection1.Images.SetKeyName(9, "delay.png");
            this.imageCollection1.Images.SetKeyName(10, "excel.png");
            this.imageCollection1.Images.SetKeyName(11, "export5.png");
            this.imageCollection1.Images.SetKeyName(12, "lock1.png");
            this.imageCollection1.Images.SetKeyName(13, "login.png");
            this.imageCollection1.Images.SetKeyName(14, "key.png");
            this.imageCollection1.Images.SetKeyName(15, "baogia.png");
            this.imageCollection1.Images.SetKeyName(16, "tien.png");
            this.imageCollection1.Images.SetKeyName(17, "UPDATE.png");
            this.imageCollection1.Images.SetKeyName(18, "loaitailieu1.png");
            this.imageCollection1.Images.SetKeyName(19, "download.png");
            // 
            // itemThem
            // 
            this.itemThem.Caption = "Thêm";
            this.itemThem.Id = 0;
            this.itemThem.ImageIndex = 0;
            this.itemThem.Name = "itemThem";
            this.itemThem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemThem_ItemClick);
            // 
            // itemSua
            // 
            this.itemSua.Caption = "Sửa";
            this.itemSua.Id = 1;
            this.itemSua.ImageIndex = 2;
            this.itemSua.Name = "itemSua";
            this.itemSua.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemSua_ItemClick);
            // 
            // itemXoa
            // 
            this.itemXoa.Caption = "Xóa";
            this.itemXoa.Id = 2;
            this.itemXoa.ImageIndex = 1;
            this.itemXoa.Name = "itemXoa";
            this.itemXoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemXoa_ItemClick);
            // 
            // itemXem
            // 
            this.itemXem.Caption = "Xem";
            this.itemXem.Id = 3;
            this.itemXem.ImageIndex = 3;
            this.itemXem.Name = "itemXem";
            this.itemXem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemXem_ItemClick);
            // 
            // itemTaiVe
            // 
            this.itemTaiVe.Caption = "Tải về";
            this.itemTaiVe.Id = 4;
            this.itemTaiVe.ImageIndex = 19;
            this.itemTaiVe.Name = "itemTaiVe";
            this.itemTaiVe.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemTaiVe_ItemClick);
            // 
            // popupTaiLieu
            // 
            this.popupTaiLieu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemThem),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemSua),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemXoa),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemXem, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemTaiVe)});
            this.popupTaiLieu.Manager = this.barManager1;
            this.popupTaiLieu.Name = "popupTaiLieu";
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Tỉnh";
            this.gridColumn6.FieldName = "TenTinh";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Width = 138;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Huyện";
            this.gridColumn7.FieldName = "TenHuyen";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Width = 153;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Xã";
            this.gridColumn8.FieldName = "TenXa";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Width = 132;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Địa chỉ";
            this.gridColumn9.ColumnEdit = this.repositoryItemMemoExEdit1;
            this.gridColumn9.FieldName = "DiaChi";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Width = 140;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Số nhà";
            this.gridColumn10.FieldName = "SoNha";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Width = 140;
            // 
            // ctlTaiLieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcTaiLieu);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ctlTaiLieu";
            this.barManager1.SetPopupContextMenu(this, this.popupTaiLieu);
            this.Size = new System.Drawing.Size(999, 285);
            this.Load += new System.EventHandler(this.ctlTaiLieu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcTaiLieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTaiLieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupTaiLieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcTaiLieu;
        private DevExpress.XtraGrid.Views.Grid.GridView grvTaiLieu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem itemThem;
        private DevExpress.XtraBars.BarButtonItem itemSua;
        private DevExpress.XtraBars.BarButtonItem itemXoa;
        private DevExpress.XtraBars.BarButtonItem itemXem;
        private DevExpress.XtraBars.BarButtonItem itemTaiVe;
        private DevExpress.XtraBars.PopupMenu popupTaiLieu;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
    }
}
