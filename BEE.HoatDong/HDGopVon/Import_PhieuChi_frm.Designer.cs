namespace LandSoft.NghiepVu.HDGopVon
{
    partial class Import_PhieuChi_frm
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnBoQua = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoaDong = new DevExpress.XtraEditors.SimpleButton();
            this.btnMoFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnThucHien = new DevExpress.XtraEditors.SimpleButton();
            this.colMaCanHo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoHDGV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDotTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoPhieuChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTKNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTKCo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoTien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNguoiNhan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoCMND = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiaChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDienGiai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNhanVien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoCMNDNhanVien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChungTuGoc = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(12, 12);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(760, 510);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaCanHo,
            this.colSoHDGV,
            this.colDotTT,
            this.colSoPhieuChi,
            this.colNgayChi,
            this.colTKNo,
            this.colTKCo,
            this.colSoTien,
            this.colNguoiNhan,
            this.colSoCMND,
            this.colDiaChi,
            this.colDienGiai,
            this.colNhanVien,
            this.colSoCMNDNhanVien,
            this.colChungTuGoc});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // btnBoQua
            // 
            this.btnBoQua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBoQua.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBoQua.Image = global::LandSoft.Operation.Properties.Resources.Cancel;
            this.btnBoQua.Location = new System.Drawing.Point(691, 527);
            this.btnBoQua.Name = "btnBoQua";
            this.btnBoQua.Size = new System.Drawing.Size(81, 23);
            this.btnBoQua.TabIndex = 16;
            this.btnBoQua.Text = "Bỏ qua";
            this.btnBoQua.Click += new System.EventHandler(this.btnBoQua_Click);
            // 
            // btnXoaDong
            // 
            this.btnXoaDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoaDong.Image = global::LandSoft.Operation.Properties.Resources.btnXoa;
            this.btnXoaDong.Location = new System.Drawing.Point(407, 528);
            this.btnXoaDong.Name = "btnXoaDong";
            this.btnXoaDong.Size = new System.Drawing.Size(80, 23);
            this.btnXoaDong.TabIndex = 15;
            this.btnXoaDong.Text = "Xóa dòng";
            this.btnXoaDong.Click += new System.EventHandler(this.btnXoaDong_Click);
            // 
            // btnMoFile
            // 
            this.btnMoFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoFile.Image = global::LandSoft.Operation.Properties.Resources.folder_add;
            this.btnMoFile.Location = new System.Drawing.Point(493, 528);
            this.btnMoFile.Name = "btnMoFile";
            this.btnMoFile.Size = new System.Drawing.Size(105, 23);
            this.btnMoFile.TabIndex = 14;
            this.btnMoFile.Text = "Mở file Excel";
            this.btnMoFile.Click += new System.EventHandler(this.btnMoFile_Click);
            // 
            // btnThucHien
            // 
            this.btnThucHien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThucHien.Image = global::LandSoft.Operation.Properties.Resources.OK;
            this.btnThucHien.Location = new System.Drawing.Point(604, 528);
            this.btnThucHien.Name = "btnThucHien";
            this.btnThucHien.Size = new System.Drawing.Size(81, 23);
            this.btnThucHien.TabIndex = 13;
            this.btnThucHien.Text = "Thực hiện";
            this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
            // 
            // colMaCanHo
            // 
            this.colMaCanHo.Caption = "Mã căn hộ";
            this.colMaCanHo.FieldName = "MaCanHo";
            this.colMaCanHo.Name = "colMaCanHo";
            this.colMaCanHo.Visible = true;
            this.colMaCanHo.VisibleIndex = 0;
            // 
            // colSoHDGV
            // 
            this.colSoHDGV.Caption = "Số hợp đồng";
            this.colSoHDGV.FieldName = "SoHDGV";
            this.colSoHDGV.Name = "colSoHDGV";
            this.colSoHDGV.Visible = true;
            this.colSoHDGV.VisibleIndex = 1;
            this.colSoHDGV.Width = 97;
            // 
            // colDotTT
            // 
            this.colDotTT.Caption = "Đợt TT";
            this.colDotTT.DisplayFormat.FormatString = "n0";
            this.colDotTT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDotTT.FieldName = "DotTT";
            this.colDotTT.Name = "colDotTT";
            this.colDotTT.Visible = true;
            this.colDotTT.VisibleIndex = 2;
            this.colDotTT.Width = 48;
            // 
            // colSoPhieuChi
            // 
            this.colSoPhieuChi.Caption = "Số phiếu chi";
            this.colSoPhieuChi.FieldName = "SoPhieuChi";
            this.colSoPhieuChi.Name = "colSoPhieuChi";
            this.colSoPhieuChi.Visible = true;
            this.colSoPhieuChi.VisibleIndex = 3;
            this.colSoPhieuChi.Width = 96;
            // 
            // colNgayChi
            // 
            this.colNgayChi.Caption = "Ngày chi";
            this.colNgayChi.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.colNgayChi.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgayChi.FieldName = "NgayChi";
            this.colNgayChi.Name = "colNgayChi";
            this.colNgayChi.Visible = true;
            this.colNgayChi.VisibleIndex = 4;
            this.colNgayChi.Width = 78;
            // 
            // colTKNo
            // 
            this.colTKNo.Caption = "TK Nợ";
            this.colTKNo.FieldName = "TKNo";
            this.colTKNo.Name = "colTKNo";
            this.colTKNo.Visible = true;
            this.colTKNo.VisibleIndex = 5;
            this.colTKNo.Width = 62;
            // 
            // colTKCo
            // 
            this.colTKCo.Caption = "TK Có";
            this.colTKCo.FieldName = "TKCo";
            this.colTKCo.Name = "colTKCo";
            this.colTKCo.Visible = true;
            this.colTKCo.VisibleIndex = 6;
            this.colTKCo.Width = 47;
            // 
            // colSoTien
            // 
            this.colSoTien.Caption = "Số tiền";
            this.colSoTien.DisplayFormat.FormatString = "n0";
            this.colSoTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSoTien.FieldName = "SoTien";
            this.colSoTien.Name = "colSoTien";
            this.colSoTien.Visible = true;
            this.colSoTien.VisibleIndex = 7;
            this.colSoTien.Width = 106;
            // 
            // colNguoiNhan
            // 
            this.colNguoiNhan.Caption = "Người nhận";
            this.colNguoiNhan.FieldName = "NguoiNhan";
            this.colNguoiNhan.Name = "colNguoiNhan";
            this.colNguoiNhan.Visible = true;
            this.colNguoiNhan.VisibleIndex = 8;
            this.colNguoiNhan.Width = 122;
            // 
            // colSoCMND
            // 
            this.colSoCMND.Caption = "Số CMND";
            this.colSoCMND.FieldName = "SoCMND";
            this.colSoCMND.Name = "colSoCMND";
            this.colSoCMND.Visible = true;
            this.colSoCMND.VisibleIndex = 9;
            this.colSoCMND.Width = 98;
            // 
            // colDiaChi
            // 
            this.colDiaChi.Caption = "Địa chỉ";
            this.colDiaChi.FieldName = "DiaChi";
            this.colDiaChi.Name = "colDiaChi";
            this.colDiaChi.Visible = true;
            this.colDiaChi.VisibleIndex = 10;
            this.colDiaChi.Width = 182;
            // 
            // colDienGiai
            // 
            this.colDienGiai.Caption = "Diễn giải";
            this.colDienGiai.FieldName = "DienGiai";
            this.colDienGiai.Name = "colDienGiai";
            this.colDienGiai.Visible = true;
            this.colDienGiai.VisibleIndex = 11;
            this.colDienGiai.Width = 139;
            // 
            // colNhanVien
            // 
            this.colNhanVien.Caption = "Nhân viên";
            this.colNhanVien.FieldName = "NhanVien";
            this.colNhanVien.Name = "colNhanVien";
            this.colNhanVien.Visible = true;
            this.colNhanVien.VisibleIndex = 12;
            this.colNhanVien.Width = 117;
            // 
            // colSoCMNDNhanVien
            // 
            this.colSoCMNDNhanVien.Caption = "Số CMND nhân viên";
            this.colSoCMNDNhanVien.FieldName = "SoCMNDNhanVien";
            this.colSoCMNDNhanVien.Name = "colSoCMNDNhanVien";
            this.colSoCMNDNhanVien.Visible = true;
            this.colSoCMNDNhanVien.VisibleIndex = 13;
            this.colSoCMNDNhanVien.Width = 115;
            // 
            // colChungTuGoc
            // 
            this.colChungTuGoc.Caption = "Chứng từ góc";
            this.colChungTuGoc.FieldName = "ChungTuGoc";
            this.colChungTuGoc.Name = "colChungTuGoc";
            this.colChungTuGoc.Visible = true;
            this.colChungTuGoc.VisibleIndex = 14;
            this.colChungTuGoc.Width = 114;
            // 
            // Import_PhieuChi_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.btnBoQua);
            this.Controls.Add(this.btnXoaDong);
            this.Controls.Add(this.btnMoFile);
            this.Controls.Add(this.btnThucHien);
            this.Controls.Add(this.gridControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Import_PhieuChi_frm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import_PhieuChi_frm";
            this.Load += new System.EventHandler(this.Import_PhieuChi_frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colMaCanHo;
        private DevExpress.XtraGrid.Columns.GridColumn colSoHDGV;
        private DevExpress.XtraGrid.Columns.GridColumn colDotTT;
        private DevExpress.XtraGrid.Columns.GridColumn colSoPhieuChi;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayChi;
        private DevExpress.XtraGrid.Columns.GridColumn colTKNo;
        private DevExpress.XtraGrid.Columns.GridColumn colTKCo;
        private DevExpress.XtraGrid.Columns.GridColumn colSoTien;
        private DevExpress.XtraGrid.Columns.GridColumn colNguoiNhan;
        private DevExpress.XtraEditors.SimpleButton btnBoQua;
        private DevExpress.XtraEditors.SimpleButton btnXoaDong;
        private DevExpress.XtraEditors.SimpleButton btnMoFile;
        private DevExpress.XtraEditors.SimpleButton btnThucHien;
        private DevExpress.XtraGrid.Columns.GridColumn colSoCMND;
        private DevExpress.XtraGrid.Columns.GridColumn colDiaChi;
        private DevExpress.XtraGrid.Columns.GridColumn colDienGiai;
        private DevExpress.XtraGrid.Columns.GridColumn colNhanVien;
        private DevExpress.XtraGrid.Columns.GridColumn colSoCMNDNhanVien;
        private DevExpress.XtraGrid.Columns.GridColumn colChungTuGoc;
    }
}