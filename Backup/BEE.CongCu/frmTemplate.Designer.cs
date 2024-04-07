namespace BEE.CongCu
{
    partial class frmTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTemplate));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gcKhac = new DevExpress.XtraGrid.GridControl();
            this.grvKhac = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookBieuThuc = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtDienGiai = new DevExpress.XtraEditors.MemoEdit();
            this.lookBieuMau = new DevExpress.XtraEditors.LookUpEdit();
            this.spinLan = new DevExpress.XtraEditors.SpinEdit();
            this.spinLien = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dateNgayKy = new DevExpress.XtraEditors.DateEdit();
            this.txtSoPhieu = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcKhac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookBieuThuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookBieuMau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinLan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinLien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayKy.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayKy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoPhieu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.gcKhac);
            this.panelControl1.Controls.Add(this.txtDienGiai);
            this.panelControl1.Controls.Add(this.lookBieuMau);
            this.panelControl1.Controls.Add(this.spinLan);
            this.panelControl1.Controls.Add(this.spinLien);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.dateNgayKy);
            this.panelControl1.Controls.Add(this.txtSoPhieu);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(635, 370);
            this.panelControl1.TabIndex = 0;
            // 
            // gcKhac
            // 
            this.gcKhac.Location = new System.Drawing.Point(17, 229);
            this.gcKhac.MainView = this.grvKhac;
            this.gcKhac.Name = "gcKhac";
            this.gcKhac.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookBieuThuc});
            this.gcKhac.ShowOnlyPredefinedDetails = true;
            this.gcKhac.Size = new System.Drawing.Size(602, 126);
            this.gcKhac.TabIndex = 7;
            this.gcKhac.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKhac});
            // 
            // grvKhac
            // 
            this.grvKhac.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.grvKhac.GridControl = this.gcKhac;
            this.grvKhac.Name = "grvKhac";
            this.grvKhac.OptionsCustomization.AllowGroup = false;
            this.grvKhac.OptionsView.ColumnAutoWidth = false;
            this.grvKhac.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvKhac.OptionsView.ShowGroupPanel = false;
            this.grvKhac.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.grvKhac_InvalidRowException);
            this.grvKhac.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grvKhac_ValidateRow);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Ký hiệu";
            this.gridColumn1.ColumnEdit = this.lookBieuThuc;
            this.gridColumn1.FieldName = "MaBT";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 85;
            // 
            // lookBieuThuc
            // 
            this.lookBieuThuc.AutoHeight = false;
            this.lookBieuThuc.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookBieuThuc.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KyHieu", "Ký hiệu"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenBT", 30, "Tên trường"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DienGiai", 50, "Diễn giải")});
            this.lookBieuThuc.DisplayMember = "KyHieu";
            this.lookBieuThuc.Name = "lookBieuThuc";
            this.lookBieuThuc.NullText = "";
            this.lookBieuThuc.PopupWidth = 400;
            this.lookBieuThuc.ShowLines = false;
            this.lookBieuThuc.ValueMember = "MaBT";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Giá trị";
            this.gridColumn2.FieldName = "GiaTri";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 120;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Diễn giải";
            this.gridColumn3.FieldName = "DienGiai";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 231;
            // 
            // txtDienGiai
            // 
            this.txtDienGiai.Location = new System.Drawing.Point(86, 93);
            this.txtDienGiai.Name = "txtDienGiai";
            this.txtDienGiai.Size = new System.Drawing.Size(533, 111);
            this.txtDienGiai.TabIndex = 6;
            // 
            // lookBieuMau
            // 
            this.lookBieuMau.Location = new System.Drawing.Point(86, 67);
            this.lookBieuMau.Name = "lookBieuMau";
            this.lookBieuMau.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookBieuMau.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenBM", "Name1")});
            this.lookBieuMau.Properties.DisplayMember = "TenBM";
            this.lookBieuMau.Properties.NullText = "";
            this.lookBieuMau.Properties.ShowHeader = false;
            this.lookBieuMau.Properties.ValueMember = "MaBM";
            this.lookBieuMau.Size = new System.Drawing.Size(533, 20);
            this.lookBieuMau.TabIndex = 5;
            this.lookBieuMau.EditValueChanged += new System.EventHandler(this.lookBieuMau_EditValueChanged);
            // 
            // spinLan
            // 
            this.spinLan.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinLan.Location = new System.Drawing.Point(438, 41);
            this.spinLan.Name = "spinLan";
            this.spinLan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinLan.Size = new System.Drawing.Size(181, 20);
            this.spinLan.TabIndex = 4;
            // 
            // spinLien
            // 
            this.spinLien.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinLien.Location = new System.Drawing.Point(86, 41);
            this.spinLien.Name = "spinLien";
            this.spinLien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinLien.Size = new System.Drawing.Size(202, 20);
            this.spinLien.TabIndex = 4;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(365, 44);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(21, 13);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Lần:";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(18, 210);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(74, 13);
            this.labelControl7.TabIndex = 3;
            this.labelControl7.Text = "Thông tin khác:";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(18, 96);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(44, 13);
            this.labelControl6.TabIndex = 3;
            this.labelControl6.Text = "Diễn giải:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(18, 70);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(47, 13);
            this.labelControl5.TabIndex = 3;
            this.labelControl5.Text = "Biểu mẫu:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(18, 44);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(23, 13);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "Liên:";
            // 
            // dateNgayKy
            // 
            this.dateNgayKy.EditValue = null;
            this.dateNgayKy.Location = new System.Drawing.Point(438, 15);
            this.dateNgayKy.Name = "dateNgayKy";
            this.dateNgayKy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayKy.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateNgayKy.Size = new System.Drawing.Size(181, 20);
            this.dateNgayKy.TabIndex = 2;
            // 
            // txtSoPhieu
            // 
            this.txtSoPhieu.Location = new System.Drawing.Point(86, 15);
            this.txtSoPhieu.Name = "txtSoPhieu";
            this.txtSoPhieu.Size = new System.Drawing.Size(202, 20);
            this.txtSoPhieu.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(365, 18);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(43, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Ngày ký:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(18, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(45, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Số phiếu:";
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.ImageIndex = 8;
            this.btnLuu.ImageList = this.imageCollection1;
            this.btnLuu.Location = new System.Drawing.Point(469, 388);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(97, 23);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu && Đóng";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuy.ImageIndex = 7;
            this.btnHuy.ImageList = this.imageCollection1;
            this.btnHuy.Location = new System.Drawing.Point(572, 388);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(74, 23);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "refresh4.png");
            this.imageCollection1.Images.SetKeyName(1, "add.png");
            this.imageCollection1.Images.SetKeyName(2, "recyclebin.png");
            this.imageCollection1.Images.SetKeyName(3, "edit-icon.png");
            this.imageCollection1.Images.SetKeyName(4, "print1.png");
            this.imageCollection1.Images.SetKeyName(5, "export5.png");
            this.imageCollection1.Images.SetKeyName(6, "print3.png");
            this.imageCollection1.Images.SetKeyName(7, "cancel.png");
            this.imageCollection1.Images.SetKeyName(8, "Luu.png");
            this.imageCollection1.Images.SetKeyName(9, "OK.png");
            this.imageCollection1.Images.SetKeyName(10, "delay.png");
            this.imageCollection1.Images.SetKeyName(11, "excel.png");
            this.imageCollection1.Images.SetKeyName(12, "lock1.png");
            this.imageCollection1.Images.SetKeyName(13, "login.png");
            this.imageCollection1.Images.SetKeyName(14, "key.png");
            this.imageCollection1.Images.SetKeyName(15, "baogia.png");
            this.imageCollection1.Images.SetKeyName(16, "tien.png");
            this.imageCollection1.Images.SetKeyName(17, "UPDATE.png");
            this.imageCollection1.Images.SetKeyName(18, "loaitailieu1.png");
            this.imageCollection1.Images.SetKeyName(19, "document32x32.png");
            // 
            // frmTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 423);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTemplate";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Biểu mẫu";
            this.Load += new System.EventHandler(this.frmTemplate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcKhac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookBieuThuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookBieuMau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinLan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinLien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayKy.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayKy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoPhieu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookBieuMau;
        private DevExpress.XtraEditors.SpinEdit spinLan;
        private DevExpress.XtraEditors.SpinEdit spinLien;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit dateNgayKy;
        private DevExpress.XtraEditors.TextEdit txtSoPhieu;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit txtDienGiai;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraGrid.GridControl gcKhac;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKhac;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookBieuThuc;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}