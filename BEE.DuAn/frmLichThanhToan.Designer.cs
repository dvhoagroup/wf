namespace BEE.DuAn
{
    partial class frmLichThanhToan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLichThanhToan));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcTruongHop = new DevExpress.XtraGrid.GridControl();
            this.grvTruongHop = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinChietKhau = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinSoTien = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcLTT = new DevExpress.XtraGrid.GridControl();
            this.grvLTT = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinDot = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinKCDot = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookUpOption = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookKTT = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinEditSoTien = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtDienGiai = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.spinTyLe = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTruongHop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTruongHop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinChietKhau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoTien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvLTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinKCDot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpOption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookKTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditSoTien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTyLe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(12, 12);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gcTruongHop);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gcLTT);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(805, 443);
            this.splitContainerControl1.SplitterPosition = 162;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gcTruongHop
            // 
            this.gcTruongHop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTruongHop.Location = new System.Drawing.Point(0, 0);
            this.gcTruongHop.MainView = this.grvTruongHop;
            this.gcTruongHop.Name = "gcTruongHop";
            this.gcTruongHop.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.spinChietKhau,
            this.spinSoTien});
            this.gcTruongHop.ShowOnlyPredefinedDetails = true;
            this.gcTruongHop.Size = new System.Drawing.Size(805, 162);
            this.gcTruongHop.TabIndex = 0;
            this.gcTruongHop.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTruongHop});
            // 
            // grvTruongHop
            // 
            this.grvTruongHop.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn15,
            this.gridColumn12,
            this.gridColumn11,
            this.gridColumn2,
            this.gridColumn3});
            this.grvTruongHop.GridControl = this.gcTruongHop;
            this.grvTruongHop.Name = "grvTruongHop";
            this.grvTruongHop.OptionsCustomization.AllowGroup = false;
            this.grvTruongHop.OptionsSelection.MultiSelect = true;
            this.grvTruongHop.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvTruongHop.OptionsView.ShowGroupPanel = false;
            this.grvTruongHop.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.grvTruongHop_InitNewRow);
            this.grvTruongHop.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvTruongHop_FocusedRowChanged);
            this.grvTruongHop.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.grvTruongHop_InvalidRowException);
            this.grvTruongHop.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grvTruongHop_ValidateRow);
            this.grvTruongHop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grvTruongHop_KeyUp);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Name";
            this.gridColumn1.FieldName = "TenTH";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 322;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Short name";
            this.gridColumn15.FieldName = "ShortName";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 1;
            this.gridColumn15.Width = 92;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Chiết khấu";
            this.gridColumn12.ColumnEdit = this.spinChietKhau;
            this.gridColumn12.DisplayFormat.FormatString = "{0:#,0.#} %";
            this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn12.FieldName = "ChietKhau";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 2;
            this.gridColumn12.Width = 89;
            // 
            // spinChietKhau
            // 
            this.spinChietKhau.AutoHeight = false;
            this.spinChietKhau.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinChietKhau.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spinChietKhau.Name = "spinChietKhau";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Số tiền";
            this.gridColumn11.ColumnEdit = this.spinSoTien;
            this.gridColumn11.DisplayFormat.FormatString = "n0";
            this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn11.FieldName = "TienThuong";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 3;
            this.gridColumn11.Width = 120;
            // 
            // spinSoTien
            // 
            this.spinSoTien.AutoHeight = false;
            this.spinSoTien.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinSoTien.MaxValue = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.spinSoTien.Name = "spinSoTien";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Mặc định";
            this.gridColumn2.FieldName = "MacDinh";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 4;
            this.gridColumn2.Width = 76;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "gridColumn3";
            this.gridColumn3.FieldName = "MaDA";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gcLTT
            // 
            this.gcLTT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLTT.Location = new System.Drawing.Point(0, 0);
            this.gcLTT.MainView = this.grvLTT;
            this.gcLTT.Name = "gcLTT";
            this.gcLTT.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookKTT,
            this.spinKCDot,
            this.txtDienGiai,
            this.spinDot,
            this.spinTyLe,
            this.lookUpOption,
            this.spinEditSoTien});
            this.gcLTT.Size = new System.Drawing.Size(805, 276);
            this.gcLTT.TabIndex = 0;
            this.gcLTT.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvLTT});
            // 
            // grvLTT
            // 
            this.grvLTT.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn10,
            this.gridColumn13,
            this.gridColumn7,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn9,
            this.gridColumn14,
            this.gridColumn8});
            this.grvLTT.GridControl = this.gcLTT;
            this.grvLTT.Name = "grvLTT";
            this.grvLTT.OptionsCustomization.AllowGroup = false;
            this.grvLTT.OptionsSelection.MultiSelect = true;
            this.grvLTT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvLTT.OptionsView.ShowGroupPanel = false;
            this.grvLTT.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.grvLTT_InitNewRow);
            this.grvLTT.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.grvLTT_InvalidRowException);
            this.grvLTT.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grvLTT_ValidateRow);
            this.grvLTT.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grvLTT_KeyUp);
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Đợt";
            this.gridColumn4.ColumnEdit = this.spinDot;
            this.gridColumn4.FieldName = "DotTT";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 52;
            // 
            // spinDot
            // 
            this.spinDot.AutoHeight = false;
            this.spinDot.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinDot.MaxValue = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.spinDot.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinDot.Name = "spinDot";
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.Caption = "K/C đợt";
            this.gridColumn10.ColumnEdit = this.spinKCDot;
            this.gridColumn10.FieldName = "SoNgay";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 1;
            this.gridColumn10.Width = 59;
            // 
            // spinKCDot
            // 
            this.spinKCDot.AutoHeight = false;
            this.spinKCDot.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinKCDot.MaxValue = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.spinKCDot.Name = "spinKCDot";
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Option";
            this.gridColumn13.ColumnEdit = this.lookUpOption;
            this.gridColumn13.FieldName = "OptionID";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.ToolTip = "Ngày hoặc tháng";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 2;
            this.gridColumn13.Width = 60;
            // 
            // lookUpOption
            // 
            this.lookUpOption.AutoHeight = false;
            this.lookUpOption.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpOption.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name1")});
            this.lookUpOption.DisplayMember = "Name";
            this.lookUpOption.Name = "lookUpOption";
            this.lookUpOption.NullText = "";
            this.lookUpOption.ShowHeader = false;
            this.lookUpOption.ValueMember = "ID";
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Ngày TT";
            this.gridColumn7.FieldName = "NgayTT";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            this.gridColumn7.Width = 70;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Tỷ lệ TT";
            this.gridColumn5.DisplayFormat.FormatString = "{0:#,0.##} %";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "TyLeTT";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 55;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Tỷ lệ VAT";
            this.gridColumn6.DisplayFormat.FormatString = "{0:#,0.##} %";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "TyLeVAT";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 56;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Kiểu TT";
            this.gridColumn9.ColumnEdit = this.lookKTT;
            this.gridColumn9.FieldName = "MaKTT";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 4;
            this.gridColumn9.Width = 96;
            // 
            // lookKTT
            // 
            this.lookKTT.AutoHeight = false;
            this.lookKTT.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookKTT.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKTT", "Name1")});
            this.lookKTT.DisplayMember = "TenKTT";
            this.lookKTT.Name = "lookKTT";
            this.lookKTT.NullText = "";
            this.lookKTT.ShowHeader = false;
            this.lookKTT.ValueMember = "MaKTT";
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Số tiền";
            this.gridColumn14.ColumnEdit = this.spinEditSoTien;
            this.gridColumn14.FieldName = "SoTien";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 7;
            this.gridColumn14.Width = 116;
            // 
            // spinEditSoTien
            // 
            this.spinEditSoTien.AutoHeight = false;
            this.spinEditSoTien.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditSoTien.DisplayFormat.FormatString = "n0";
            this.spinEditSoTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEditSoTien.EditFormat.FormatString = "n0";
            this.spinEditSoTien.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEditSoTien.MaxValue = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            0});
            this.spinEditSoTien.Name = "spinEditSoTien";
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Diễn giải";
            this.gridColumn8.ColumnEdit = this.txtDienGiai;
            this.gridColumn8.FieldName = "DienGiai";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            this.gridColumn8.Width = 197;
            // 
            // txtDienGiai
            // 
            this.txtDienGiai.AutoHeight = false;
            this.txtDienGiai.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDienGiai.Name = "txtDienGiai";
            this.txtDienGiai.PopupFormMinSize = new System.Drawing.Size(400, 120);
            this.txtDienGiai.ShowIcon = false;
            // 
            // spinTyLe
            // 
            this.spinTyLe.AutoHeight = false;
            this.spinTyLe.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinTyLe.MaxValue = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.spinTyLe.Name = "spinTyLe";
            // 
            // btnLuu
            // 
            this.btnLuu.ImageIndex = 6;
            this.btnLuu.ImageList = this.imageCollection1;
            this.btnLuu.Location = new System.Drawing.Point(640, 461);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(90, 26);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Save";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.ImageIndex = 4;
            this.btnHuy.ImageList = this.imageCollection1;
            this.btnHuy.Location = new System.Drawing.Point(736, 461);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(81, 26);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Text = "Cancel";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
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
            this.imageCollection1.Images.SetKeyName(18, "previous-icon.png");
            this.imageCollection1.Images.SetKeyName(19, "next-icon.png");
            this.imageCollection1.Images.SetKeyName(20, "document32x32.png");
            this.imageCollection1.Images.SetKeyName(21, "clock1.png");
            this.imageCollection1.Images.SetKeyName(22, "HELP.png");
            this.imageCollection1.Images.SetKeyName(23, "thanhly.png");
            this.imageCollection1.Images.SetKeyName(24, "muaban.png");
            this.imageCollection1.Images.SetKeyName(25, "chuyentrangthai.png");
            this.imageCollection1.Images.SetKeyName(26, "giaodich32x32.png");
            this.imageCollection1.Images.SetKeyName(27, "repeat.png");
            this.imageCollection1.Images.SetKeyName(28, "cart3.png");
            this.imageCollection1.Images.SetKeyName(29, "import.png");
            this.imageCollection1.Images.SetKeyName(30, "pause.png");
            this.imageCollection1.Images.SetKeyName(31, "play.png");
            // 
            // frmLichThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 496);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.splitContainerControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLichThanhToan";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lịch thanh toán/ Payment term";
            this.Load += new System.EventHandler(this.frmLichThanhToan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTruongHop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTruongHop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinChietKhau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoTien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvLTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinKCDot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpOption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookKTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditSoTien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTyLe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gcTruongHop;
        private DevExpress.XtraGrid.Views.Grid.GridView grvTruongHop;
        private DevExpress.XtraGrid.GridControl gcLTT;
        private DevExpress.XtraGrid.Views.Grid.GridView grvLTT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookKTT;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinDot;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinKCDot;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit txtDienGiai;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinChietKhau;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinSoTien;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinTyLe;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpOption;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinEditSoTien;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}