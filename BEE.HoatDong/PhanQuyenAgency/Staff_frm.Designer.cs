namespace BEE.HoatDong.PhanQuyenAgency
{
    partial class Staff_frm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Staff_frm));
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaNV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenNV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookUpPhongBan = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.lookUpChucVu = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookUpNhomKD = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.Email = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Phone = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpPhongBan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpChucVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpNhomKD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl2
            // 
            this.gridControl2.Location = new System.Drawing.Point(12, 12);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookUpPhongBan,
            this.lookUpChucVu,
            this.lookUpNhomKD});
            this.gridControl2.Size = new System.Drawing.Size(869, 317);
            this.gridControl2.TabIndex = 5;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaNV,
            this.colTenNV,
            this.gridColumn3,
            this.gridColumn6,
            this.gridColumn8,
            this.Email,
            this.Phone});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.MultiSelect = true;
            this.gridView2.OptionsView.ShowAutoFilterRow = true;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // colMaNV
            // 
            this.colMaNV.Caption = "gridColumn1";
            this.colMaNV.FieldName = "ID";
            this.colMaNV.Name = "colMaNV";
            // 
            // colTenNV
            // 
            this.colTenNV.Caption = "Name";
            this.colTenNV.FieldName = "Name";
            this.colTenNV.Name = "colTenNV";
            this.colTenNV.OptionsColumn.AllowEdit = false;
            this.colTenNV.OptionsColumn.AllowFocus = false;
            this.colTenNV.Visible = true;
            this.colTenNV.VisibleIndex = 0;
            this.colTenNV.Width = 133;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Username";
            this.gridColumn3.FieldName = "Username";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 104;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Phòng ban";
            this.gridColumn6.ColumnEdit = this.lookUpPhongBan;
            this.gridColumn6.FieldName = "MaPB";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 77;
            // 
            // lookUpPhongBan
            // 
            this.lookUpPhongBan.AutoHeight = false;
            this.lookUpPhongBan.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpPhongBan.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenPB", "Name1")});
            this.lookUpPhongBan.DisplayMember = "TenPB";
            this.lookUpPhongBan.Name = "lookUpPhongBan";
            this.lookUpPhongBan.NullText = "";
            this.lookUpPhongBan.ShowHeader = false;
            this.lookUpPhongBan.ValueMember = "MaPB";
            // 
            // lookUpChucVu
            // 
            this.lookUpChucVu.AutoHeight = false;
            this.lookUpChucVu.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpChucVu.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCV", "Name2")});
            this.lookUpChucVu.DisplayMember = "TenCV";
            this.lookUpChucVu.Name = "lookUpChucVu";
            this.lookUpChucVu.NullText = "";
            this.lookUpChucVu.ShowHeader = false;
            this.lookUpChucVu.ValueMember = "MaCV";
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Nhóm kinh doanh";
            this.gridColumn8.ColumnEdit = this.lookUpNhomKD;
            this.gridColumn8.FieldName = "MaNKD";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.AllowFocus = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 5;
            this.gridColumn8.Width = 121;
            // 
            // lookUpNhomKD
            // 
            this.lookUpNhomKD.AutoHeight = false;
            this.lookUpNhomKD.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpNhomKD.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenNKD", "Name3")});
            this.lookUpNhomKD.DisplayMember = "TenNKD";
            this.lookUpNhomKD.Name = "lookUpNhomKD";
            this.lookUpNhomKD.NullText = "";
            this.lookUpNhomKD.ShowHeader = false;
            this.lookUpNhomKD.ValueMember = "MaNKD";
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.ImageOptions.ImageIndex = 4;
            this.btnHuy.ImageOptions.ImageList = this.imageCollection1;
            this.btnHuy.Location = new System.Drawing.Point(805, 335);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(79, 23);
            this.btnHuy.TabIndex = 7;
            this.btnHuy.Text = "Hủy - ESC";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "refresh4.png");
            this.imageCollection1.Images.SetKeyName(1, "add.png");
            this.imageCollection1.Images.SetKeyName(2, "recyclebin.png");
            this.imageCollection1.Images.SetKeyName(3, "edit-icon.png");
            this.imageCollection1.Images.SetKeyName(4, "cancel.png");
            this.imageCollection1.Images.SetKeyName(5, "print3.png");
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
            this.imageCollection1.Images.SetKeyName(19, "document32x32.png");
            this.imageCollection1.Images.SetKeyName(20, "attach.png");
            this.imageCollection1.Images.SetKeyName(21, "pause.png");
            this.imageCollection1.Images.SetKeyName(22, "play.png");
            this.imageCollection1.Images.SetKeyName(23, "top.png");
            this.imageCollection1.Images.SetKeyName(24, "giaodich32x32.png");
            // 
            // btnDongY
            // 
            this.btnDongY.ImageOptions.ImageIndex = 6;
            this.btnDongY.ImageOptions.ImageList = this.imageCollection1;
            this.btnDongY.Location = new System.Drawing.Point(710, 335);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(89, 23);
            this.btnDongY.TabIndex = 6;
            this.btnDongY.Text = "Lưu && Đóng";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // Email
            // 
            this.Email.Caption = "Email";
            this.Email.FieldName = "Email";
            this.Email.Name = "Email";
            this.Email.Visible = true;
            this.Email.VisibleIndex = 3;
            this.Email.Width = 117;
            // 
            // Phone
            // 
            this.Phone.Caption = "Phone";
            this.Phone.FieldName = "Phone";
            this.Phone.Name = "Phone";
            this.Phone.Visible = true;
            this.Phone.VisibleIndex = 1;
            this.Phone.Width = 111;
            // 
            // Staff_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 370);
            this.Controls.Add(this.gridControl2);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnDongY);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Staff_frm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn cộng tác viên";
            this.Load += new System.EventHandler(this.Staff_frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpPhongBan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpChucVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpNhomKD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn colMaNV;
        private DevExpress.XtraGrid.Columns.GridColumn colTenNV;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpPhongBan;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpChucVu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpNhomKD;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.Columns.GridColumn Email;
        private DevExpress.XtraGrid.Columns.GridColumn Phone;
    }
}