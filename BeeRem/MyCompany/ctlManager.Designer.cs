namespace BEEREMA.MyCompany
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
            this.itemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.itemNew = new DevExpress.XtraBars.BarButtonItem();
            this.itemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.itemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.cmbKyBaoCao = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.repositoryItemDateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.lookDuAn = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcCompany = new DevExpress.XtraGrid.GridControl();
            this.gvCompany = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenVT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenCT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DiaChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DienThoai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.WebSite = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NguoiDaiDien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ChuVu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaSoThue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SoGPKD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NgayCap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NoiCap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DienGiai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NhanVienTao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NgayTao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NhanVienCN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NgayCN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookDuAn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCompany)).BeginInit();
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
            this.itemNew,
            this.itemEdit,
            this.itemDelete});
            this.barManager1.MaxItemId = 8;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbKyBaoCao,
            this.repositoryItemDateEdit1,
            this.repositoryItemDateEdit2,
            this.lookDuAn});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemNew, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemRefresh
            // 
            this.itemRefresh.Caption = "Nạp";
            this.itemRefresh.Id = 4;
            this.itemRefresh.ImageIndex = 5;
            this.itemRefresh.Name = "itemRefresh";
            this.itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemRefresh_ItemClick);
            // 
            // itemNew
            // 
            this.itemNew.Caption = "Thêm";
            this.itemNew.Id = 5;
            this.itemNew.ImageIndex = 0;
            this.itemNew.Name = "itemNew";
            this.itemNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemNew_ItemClick);
            // 
            // itemEdit
            // 
            this.itemEdit.Caption = "Sửa";
            this.itemEdit.Id = 6;
            this.itemEdit.ImageIndex = 2;
            this.itemEdit.Name = "itemEdit";
            this.itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemEdit_ItemClick);
            // 
            // itemDelete
            // 
            this.itemDelete.Caption = "Xóa";
            this.itemDelete.Id = 7;
            this.itemDelete.ImageIndex = 1;
            this.itemDelete.Name = "itemDelete";
            this.itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemDelete_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(932, 25);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 459);
            this.barDockControlBottom.Size = new System.Drawing.Size(932, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 25);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 434);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(932, 25);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 434);
            // 
            // cmbKyBaoCao
            // 
            this.cmbKyBaoCao.AutoHeight = false;
            this.cmbKyBaoCao.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbKyBaoCao.Name = "cmbKyBaoCao";
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.repositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.repositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            this.repositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // repositoryItemDateEdit2
            // 
            this.repositoryItemDateEdit2.AutoHeight = false;
            this.repositoryItemDateEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit2.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.repositoryItemDateEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit2.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.repositoryItemDateEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit2.Name = "repositoryItemDateEdit2";
            this.repositoryItemDateEdit2.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // lookDuAn
            // 
            this.lookDuAn.AutoHeight = false;
            this.lookDuAn.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookDuAn.Name = "lookDuAn";
            this.lookDuAn.NullText = "[Chọn dự án...]";
            // 
            // gcCompany
            // 
            this.gcCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCompany.Location = new System.Drawing.Point(0, 25);
            this.gcCompany.MainView = this.gvCompany;
            this.gcCompany.MenuManager = this.barManager1;
            this.gcCompany.Name = "gcCompany";
            this.gcCompany.Size = new System.Drawing.Size(932, 434);
            this.gcCompany.TabIndex = 0;
            this.gcCompany.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCompany});
            // 
            // gvCompany
            // 
            this.gvCompany.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.TenVT,
            this.TenCT,
            this.DiaChi,
            this.DienThoai,
            this.WebSite,
            this.NguoiDaiDien,
            this.ChuVu,
            this.MaSoThue,
            this.SoGPKD,
            this.NgayCap,
            this.NoiCap,
            this.DienGiai,
            this.NhanVienTao,
            this.NgayTao,
            this.NhanVienCN,
            this.NgayCN});
            this.gvCompany.GridControl = this.gcCompany;
            this.gvCompany.Name = "gvCompany";
            this.gvCompany.OptionsView.ColumnAutoWidth = false;
            this.gvCompany.OptionsView.ShowAutoFilterRow = true;
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "MaCT";
            this.ID.Name = "ID";
            this.ID.OptionsColumn.AllowEdit = false;
            // 
            // TenVT
            // 
            this.TenVT.Caption = "Tên viết tắt";
            this.TenVT.FieldName = "TenVT";
            this.TenVT.Name = "TenVT";
            this.TenVT.OptionsColumn.AllowEdit = false;
            this.TenVT.Visible = true;
            this.TenVT.VisibleIndex = 1;
            this.TenVT.Width = 87;
            // 
            // TenCT
            // 
            this.TenCT.Caption = "Tên công ty";
            this.TenCT.FieldName = "TenCT";
            this.TenCT.Name = "TenCT";
            this.TenCT.OptionsColumn.AllowEdit = false;
            this.TenCT.Visible = true;
            this.TenCT.VisibleIndex = 0;
            this.TenCT.Width = 146;
            // 
            // DiaChi
            // 
            this.DiaChi.Caption = "Địa chỉ";
            this.DiaChi.FieldName = "DiaChi";
            this.DiaChi.Name = "DiaChi";
            this.DiaChi.OptionsColumn.AllowEdit = false;
            this.DiaChi.Visible = true;
            this.DiaChi.VisibleIndex = 2;
            this.DiaChi.Width = 166;
            // 
            // DienThoai
            // 
            this.DienThoai.Caption = "Điện thoại";
            this.DienThoai.FieldName = "DienThoai";
            this.DienThoai.Name = "DienThoai";
            this.DienThoai.OptionsColumn.AllowEdit = false;
            this.DienThoai.Visible = true;
            this.DienThoai.VisibleIndex = 4;
            this.DienThoai.Width = 122;
            // 
            // WebSite
            // 
            this.WebSite.Caption = "WebSite";
            this.WebSite.FieldName = "Website";
            this.WebSite.Name = "WebSite";
            this.WebSite.OptionsColumn.AllowEdit = false;
            this.WebSite.Visible = true;
            this.WebSite.VisibleIndex = 3;
            this.WebSite.Width = 160;
            // 
            // NguoiDaiDien
            // 
            this.NguoiDaiDien.Caption = "Người đại diện";
            this.NguoiDaiDien.FieldName = "NguoiDaiDien";
            this.NguoiDaiDien.Name = "NguoiDaiDien";
            this.NguoiDaiDien.OptionsColumn.AllowEdit = false;
            this.NguoiDaiDien.Visible = true;
            this.NguoiDaiDien.VisibleIndex = 5;
            this.NguoiDaiDien.Width = 132;
            // 
            // ChuVu
            // 
            this.ChuVu.Caption = "Chức vụ";
            this.ChuVu.FieldName = "ChucVu";
            this.ChuVu.Name = "ChuVu";
            this.ChuVu.OptionsColumn.AllowEdit = false;
            this.ChuVu.Visible = true;
            this.ChuVu.VisibleIndex = 6;
            this.ChuVu.Width = 148;
            // 
            // MaSoThue
            // 
            this.MaSoThue.Caption = "Mã số thuế";
            this.MaSoThue.FieldName = "MaSoThue";
            this.MaSoThue.Name = "MaSoThue";
            this.MaSoThue.OptionsColumn.AllowEdit = false;
            this.MaSoThue.Visible = true;
            this.MaSoThue.VisibleIndex = 7;
            this.MaSoThue.Width = 100;
            // 
            // SoGPKD
            // 
            this.SoGPKD.Caption = "Số giấy phép kinh doanh";
            this.SoGPKD.FieldName = "SoGPKD";
            this.SoGPKD.Name = "SoGPKD";
            this.SoGPKD.OptionsColumn.AllowEdit = false;
            this.SoGPKD.Visible = true;
            this.SoGPKD.VisibleIndex = 8;
            this.SoGPKD.Width = 136;
            // 
            // NgayCap
            // 
            this.NgayCap.Caption = "Ngày cấp";
            this.NgayCap.FieldName = "NgayCap";
            this.NgayCap.Name = "NgayCap";
            this.NgayCap.OptionsColumn.AllowEdit = false;
            this.NgayCap.Visible = true;
            this.NgayCap.VisibleIndex = 9;
            this.NgayCap.Width = 104;
            // 
            // NoiCap
            // 
            this.NoiCap.Caption = "Nơi cấp";
            this.NoiCap.FieldName = "NoiCap";
            this.NoiCap.Name = "NoiCap";
            this.NoiCap.OptionsColumn.AllowEdit = false;
            this.NoiCap.Visible = true;
            this.NoiCap.VisibleIndex = 10;
            this.NoiCap.Width = 141;
            // 
            // DienGiai
            // 
            this.DienGiai.Caption = "Diễn giải";
            this.DienGiai.FieldName = "DienGiai";
            this.DienGiai.Name = "DienGiai";
            this.DienGiai.OptionsColumn.AllowEdit = false;
            this.DienGiai.Visible = true;
            this.DienGiai.VisibleIndex = 15;
            // 
            // NhanVienTao
            // 
            this.NhanVienTao.Caption = "Nhân viên tạo";
            this.NhanVienTao.FieldName = "NVTao";
            this.NhanVienTao.Name = "NhanVienTao";
            this.NhanVienTao.OptionsColumn.AllowEdit = false;
            this.NhanVienTao.Visible = true;
            this.NhanVienTao.VisibleIndex = 11;
            // 
            // NgayTao
            // 
            this.NgayTao.Caption = "Ngày tạo";
            this.NgayTao.FieldName = "NgayTao";
            this.NgayTao.Name = "NgayTao";
            this.NgayTao.OptionsColumn.AllowEdit = false;
            this.NgayTao.Visible = true;
            this.NgayTao.VisibleIndex = 12;
            // 
            // NhanVienCN
            // 
            this.NhanVienCN.Caption = "Nhân viên cập nhật";
            this.NhanVienCN.FieldName = "NVSua";
            this.NhanVienCN.Name = "NhanVienCN";
            this.NhanVienCN.OptionsColumn.AllowEdit = false;
            this.NhanVienCN.Visible = true;
            this.NhanVienCN.VisibleIndex = 13;
            // 
            // NgayCN
            // 
            this.NgayCN.Caption = "Ngày cập nhật";
            this.NgayCN.FieldName = "NgayCN";
            this.NgayCN.Name = "NgayCN";
            this.NgayCN.OptionsColumn.AllowEdit = false;
            this.NgayCN.Visible = true;
            this.NgayCN.VisibleIndex = 14;
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
            // 
            // ctlManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcCompany);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ctlManager";
            this.Size = new System.Drawing.Size(932, 459);
            this.Tag = "Công ty thành viên";
            this.Load += new System.EventHandler(this.ctlManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookDuAn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbKyBaoCao;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookDuAn;
        private DevExpress.XtraBars.BarButtonItem itemRefresh;
        private DevExpress.XtraBars.BarButtonItem itemNew;
        private DevExpress.XtraBars.BarButtonItem itemEdit;
        private DevExpress.XtraBars.BarButtonItem itemDelete;
        private DevExpress.XtraGrid.GridControl gcCompany;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCompany;
        private DevExpress.XtraGrid.Columns.GridColumn TenVT;
        private DevExpress.XtraGrid.Columns.GridColumn TenCT;
        private DevExpress.XtraGrid.Columns.GridColumn DiaChi;
        private DevExpress.XtraGrid.Columns.GridColumn ChuVu;
        private DevExpress.XtraGrid.Columns.GridColumn MaSoThue;
        private DevExpress.XtraGrid.Columns.GridColumn SoGPKD;
        private DevExpress.XtraGrid.Columns.GridColumn NgayCap;
        private DevExpress.XtraGrid.Columns.GridColumn NoiCap;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn NguoiDaiDien;
        private DevExpress.XtraGrid.Columns.GridColumn DienThoai;
        private DevExpress.XtraGrid.Columns.GridColumn WebSite;
        private DevExpress.XtraGrid.Columns.GridColumn DienGiai;
        private DevExpress.XtraGrid.Columns.GridColumn NhanVienTao;
        private DevExpress.XtraGrid.Columns.GridColumn NgayTao;
        private DevExpress.XtraGrid.Columns.GridColumn NhanVienCN;
        private DevExpress.XtraGrid.Columns.GridColumn NgayCN;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}
