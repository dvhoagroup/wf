namespace BEE.KhachHang
{
    partial class Import_Introduce_frm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Import_Introduce_frm));
            this.btnBoQua = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoaDong = new DevExpress.XtraEditors.SimpleButton();
            this.btnMoFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnThucHien = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colHoTenKhachHang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoCMNDKH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHoTenNguoiGioiThieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgaySinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoiSinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoCMNDNDD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayCap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoiCap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiaChiLienLac = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiDong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGhiChu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBoQua
            // 
            this.btnBoQua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBoQua.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBoQua.ImageIndex = 4;
            this.btnBoQua.ImageList = this.imageCollection1;
            this.btnBoQua.Location = new System.Drawing.Point(691, 527);
            this.btnBoQua.Name = "btnBoQua";
            this.btnBoQua.Size = new System.Drawing.Size(81, 23);
            this.btnBoQua.TabIndex = 12;
            this.btnBoQua.Text = "Bỏ qua";
            this.btnBoQua.Click += new System.EventHandler(this.btnBoQua_Click);
            // 
            // btnXoaDong
            // 
            this.btnXoaDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoaDong.ImageIndex = 1;
            this.btnXoaDong.ImageList = this.imageCollection1;
            this.btnXoaDong.Location = new System.Drawing.Point(407, 528);
            this.btnXoaDong.Name = "btnXoaDong";
            this.btnXoaDong.Size = new System.Drawing.Size(80, 23);
            this.btnXoaDong.TabIndex = 11;
            this.btnXoaDong.Text = "Xóa dòng";
            this.btnXoaDong.Click += new System.EventHandler(this.btnXoaDong_Click);
            // 
            // btnMoFile
            // 
            this.btnMoFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoFile.ImageIndex = 10;
            this.btnMoFile.ImageList = this.imageCollection1;
            this.btnMoFile.Location = new System.Drawing.Point(493, 528);
            this.btnMoFile.Name = "btnMoFile";
            this.btnMoFile.Size = new System.Drawing.Size(105, 23);
            this.btnMoFile.TabIndex = 10;
            this.btnMoFile.Text = "Mở file Excel";
            this.btnMoFile.Click += new System.EventHandler(this.btnMoFile_Click);
            // 
            // btnThucHien
            // 
            this.btnThucHien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThucHien.ImageIndex = 6;
            this.btnThucHien.ImageList = this.imageCollection1;
            this.btnThucHien.Location = new System.Drawing.Point(604, 528);
            this.btnThucHien.Name = "btnThucHien";
            this.btnThucHien.Size = new System.Drawing.Size(81, 23);
            this.btnThucHien.TabIndex = 9;
            this.btnThucHien.Text = "Thực hiện";
            this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(12, 12);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(760, 509);
            this.gridControl1.TabIndex = 13;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colHoTenKhachHang,
            this.colSoCMNDKH,
            this.colHoTenNguoiGioiThieu,
            this.colNgaySinh,
            this.colNoiSinh,
            this.colSoCMNDNDD,
            this.colNgayCap,
            this.colNoiCap,
            this.colDiaChiLienLac,
            this.colDiDong,
            this.colEmail,
            this.colGhiChu});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colHoTenKhachHang
            // 
            this.colHoTenKhachHang.Caption = "Họ tên khách hàng";
            this.colHoTenKhachHang.FieldName = "HoTenKhachHang";
            this.colHoTenKhachHang.Name = "colHoTenKhachHang";
            this.colHoTenKhachHang.Visible = true;
            this.colHoTenKhachHang.VisibleIndex = 0;
            this.colHoTenKhachHang.Width = 132;
            // 
            // colSoCMNDKH
            // 
            this.colSoCMNDKH.Caption = "Số CMND khách hàng";
            this.colSoCMNDKH.FieldName = "SoCMNDKH";
            this.colSoCMNDKH.Name = "colSoCMNDKH";
            this.colSoCMNDKH.Visible = true;
            this.colSoCMNDKH.VisibleIndex = 1;
            this.colSoCMNDKH.Width = 115;
            // 
            // colHoTenNguoiGioiThieu
            // 
            this.colHoTenNguoiGioiThieu.Caption = "Họ tên người đại diện";
            this.colHoTenNguoiGioiThieu.FieldName = "HoTenNguoiGioiThieu";
            this.colHoTenNguoiGioiThieu.Name = "colHoTenNguoiGioiThieu";
            this.colHoTenNguoiGioiThieu.Visible = true;
            this.colHoTenNguoiGioiThieu.VisibleIndex = 2;
            this.colHoTenNguoiGioiThieu.Width = 129;
            // 
            // colNgaySinh
            // 
            this.colNgaySinh.Caption = "Ngày sinh";
            this.colNgaySinh.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.colNgaySinh.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgaySinh.FieldName = "NgaySinh";
            this.colNgaySinh.Name = "colNgaySinh";
            this.colNgaySinh.Visible = true;
            this.colNgaySinh.VisibleIndex = 3;
            this.colNgaySinh.Width = 91;
            // 
            // colNoiSinh
            // 
            this.colNoiSinh.Caption = "Nơi sinh";
            this.colNoiSinh.FieldName = "NoiSinh";
            this.colNoiSinh.Name = "colNoiSinh";
            this.colNoiSinh.Visible = true;
            this.colNoiSinh.VisibleIndex = 4;
            this.colNoiSinh.Width = 118;
            // 
            // colSoCMNDNDD
            // 
            this.colSoCMNDNDD.Caption = "Số CMND";
            this.colSoCMNDNDD.FieldName = "SoCMNDNDD";
            this.colSoCMNDNDD.Name = "colSoCMNDNDD";
            this.colSoCMNDNDD.Visible = true;
            this.colSoCMNDNDD.VisibleIndex = 5;
            this.colSoCMNDNDD.Width = 93;
            // 
            // colNgayCap
            // 
            this.colNgayCap.Caption = "Ngày cấp";
            this.colNgayCap.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.colNgayCap.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgayCap.FieldName = "NgayCap";
            this.colNgayCap.Name = "colNgayCap";
            this.colNgayCap.Visible = true;
            this.colNgayCap.VisibleIndex = 6;
            this.colNgayCap.Width = 83;
            // 
            // colNoiCap
            // 
            this.colNoiCap.Caption = "Nơi cấp";
            this.colNoiCap.FieldName = "NoiCap";
            this.colNoiCap.Name = "colNoiCap";
            this.colNoiCap.Visible = true;
            this.colNoiCap.VisibleIndex = 7;
            this.colNoiCap.Width = 98;
            // 
            // colDiaChiLienLac
            // 
            this.colDiaChiLienLac.Caption = "Địa chỉ liên lạc";
            this.colDiaChiLienLac.FieldName = "DiaChiLienLac";
            this.colDiaChiLienLac.Name = "colDiaChiLienLac";
            this.colDiaChiLienLac.Visible = true;
            this.colDiaChiLienLac.VisibleIndex = 8;
            this.colDiaChiLienLac.Width = 115;
            // 
            // colDiDong
            // 
            this.colDiDong.Caption = "Di động";
            this.colDiDong.FieldName = "DiDong";
            this.colDiDong.Name = "colDiDong";
            this.colDiDong.Visible = true;
            this.colDiDong.VisibleIndex = 9;
            this.colDiDong.Width = 88;
            // 
            // colEmail
            // 
            this.colEmail.Caption = "Email";
            this.colEmail.FieldName = "Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.Visible = true;
            this.colEmail.VisibleIndex = 10;
            this.colEmail.Width = 107;
            // 
            // colGhiChu
            // 
            this.colGhiChu.Caption = "Ghi chú";
            this.colGhiChu.FieldName = "GhiChu";
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.Visible = true;
            this.colGhiChu.VisibleIndex = 11;
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
            // Import_Introduce_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.btnBoQua);
            this.Controls.Add(this.btnXoaDong);
            this.Controls.Add(this.btnMoFile);
            this.Controls.Add(this.btnThucHien);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Import_Introduce_frm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Avatars";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Import_Avatar_frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnBoQua;
        private DevExpress.XtraEditors.SimpleButton btnXoaDong;
        private DevExpress.XtraEditors.SimpleButton btnMoFile;
        private DevExpress.XtraEditors.SimpleButton btnThucHien;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colHoTenKhachHang;
        private DevExpress.XtraGrid.Columns.GridColumn colSoCMNDKH;
        private DevExpress.XtraGrid.Columns.GridColumn colHoTenNguoiGioiThieu;
        private DevExpress.XtraGrid.Columns.GridColumn colNgaySinh;
        private DevExpress.XtraGrid.Columns.GridColumn colNoiSinh;
        private DevExpress.XtraGrid.Columns.GridColumn colSoCMNDNDD;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayCap;
        private DevExpress.XtraGrid.Columns.GridColumn colNoiCap;
        private DevExpress.XtraGrid.Columns.GridColumn colDiaChiLienLac;
        private DevExpress.XtraGrid.Columns.GridColumn colDiDong;
        private DevExpress.XtraGrid.Columns.GridColumn colEmail;
        private DevExpress.XtraGrid.Columns.GridColumn colGhiChu;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}