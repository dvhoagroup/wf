namespace BEE.BaoCao.ThongKe
{
    partial class CongNoThang_frm
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
            this.colMaSo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHoTenKH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoHD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayHD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colDonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDienTich = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThanhTien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTienSDDat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThueVAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTongCong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDaThu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgay1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgay2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgay3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgay4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgay5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgay6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgay7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgay8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgay9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgay10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgay11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgay12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTongCongNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTienMuaCH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTongThueVAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
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
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEdit1});
            this.gridControl1.Size = new System.Drawing.Size(960, 506);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gridView2});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.GroupPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.GroupPanel.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaSo,
            this.colHoTenKH,
            this.colSoHD,
            this.colNgayHD,
            this.colDonGia,
            this.colDienTich,
            this.colThanhTien,
            this.colTienSDDat,
            this.colThueVAT,
            this.colTongCong,
            this.colDaThu,
            this.colConNo,
            this.gridColumn1,
            this.colNgay1,
            this.colNgay2,
            this.colNgay3,
            this.colNgay4,
            this.colNgay5,
            this.colNgay6,
            this.colNgay7,
            this.colNgay8,
            this.colNgay9,
            this.colNgay10,
            this.colNgay11,
            this.colNgay12,
            this.colTongCongNo,
            this.colTienMuaCH,
            this.colTongThueVAT});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupPanelText = "BÁO CÁO CÔNG NỢ THEO THÁNG";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // colMaSo
            // 
            this.colMaSo.Caption = "Mã căn hộ";
            this.colMaSo.FieldName = "MaSo";
            this.colMaSo.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colMaSo.Name = "colMaSo";
            this.colMaSo.OptionsColumn.AllowEdit = false;
            this.colMaSo.OptionsColumn.AllowFocus = false;
            this.colMaSo.SummaryItem.DisplayFormat = "{0:n0} căn";
            this.colMaSo.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.colMaSo.Visible = true;
            this.colMaSo.VisibleIndex = 0;
            this.colMaSo.Width = 81;
            // 
            // colHoTenKH
            // 
            this.colHoTenKH.Caption = "Khách hàng hiên tại";
            this.colHoTenKH.FieldName = "HoTenKH";
            this.colHoTenKH.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colHoTenKH.Name = "colHoTenKH";
            this.colHoTenKH.OptionsColumn.AllowEdit = false;
            this.colHoTenKH.OptionsColumn.AllowFocus = false;
            this.colHoTenKH.Visible = true;
            this.colHoTenKH.VisibleIndex = 1;
            this.colHoTenKH.Width = 119;
            // 
            // colSoHD
            // 
            this.colSoHD.Caption = "Số hợp đồng";
            this.colSoHD.FieldName = "SoHDMB";
            this.colSoHD.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colSoHD.Name = "colSoHD";
            this.colSoHD.OptionsColumn.AllowEdit = false;
            this.colSoHD.OptionsColumn.AllowFocus = false;
            this.colSoHD.Visible = true;
            this.colSoHD.VisibleIndex = 2;
            this.colSoHD.Width = 85;
            // 
            // colNgayHD
            // 
            this.colNgayHD.Caption = "Ngày hợp đồng";
            this.colNgayHD.ColumnEdit = this.repositoryItemDateEdit1;
            this.colNgayHD.FieldName = "NgayKy";
            this.colNgayHD.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colNgayHD.Name = "colNgayHD";
            this.colNgayHD.OptionsColumn.AllowEdit = false;
            this.colNgayHD.OptionsColumn.AllowFocus = false;
            this.colNgayHD.Visible = true;
            this.colNgayHD.VisibleIndex = 3;
            this.colNgayHD.Width = 93;
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
            this.repositoryItemDateEdit1.Mask.EditMask = "dd/MM/yyyy";
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            this.repositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // colDonGia
            // 
            this.colDonGia.Caption = "Đơn giá";
            this.colDonGia.DisplayFormat.FormatString = "n0";
            this.colDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDonGia.FieldName = "DonGia";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.OptionsColumn.AllowEdit = false;
            this.colDonGia.OptionsColumn.AllowFocus = false;
            this.colDonGia.Visible = true;
            this.colDonGia.VisibleIndex = 4;
            this.colDonGia.Width = 82;
            // 
            // colDienTich
            // 
            this.colDienTich.Caption = "Diện tích";
            this.colDienTich.DisplayFormat.FormatString = "n2";
            this.colDienTich.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDienTich.FieldName = "DienTich";
            this.colDienTich.Name = "colDienTich";
            this.colDienTich.OptionsColumn.AllowEdit = false;
            this.colDienTich.OptionsColumn.AllowFocus = false;
            this.colDienTich.SummaryItem.DisplayFormat = "{0:n2}";
            this.colDienTich.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colDienTich.Visible = true;
            this.colDienTich.VisibleIndex = 5;
            this.colDienTich.Width = 82;
            // 
            // colThanhTien
            // 
            this.colThanhTien.Caption = "Thành tiền";
            this.colThanhTien.DisplayFormat.FormatString = "n0";
            this.colThanhTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colThanhTien.FieldName = "ThanhTien";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.OptionsColumn.AllowEdit = false;
            this.colThanhTien.OptionsColumn.AllowFocus = false;
            this.colThanhTien.SummaryItem.DisplayFormat = "{0:n0}";
            this.colThanhTien.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colThanhTien.Visible = true;
            this.colThanhTien.VisibleIndex = 6;
            this.colThanhTien.Width = 104;
            // 
            // colTienSDDat
            // 
            this.colTienSDDat.Caption = "Tiền SD đất";
            this.colTienSDDat.DisplayFormat.FormatString = "n0";
            this.colTienSDDat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTienSDDat.FieldName = "TienSDDat";
            this.colTienSDDat.Name = "colTienSDDat";
            this.colTienSDDat.OptionsColumn.AllowEdit = false;
            this.colTienSDDat.OptionsColumn.AllowFocus = false;
            this.colTienSDDat.SummaryItem.DisplayFormat = "{0:n0}";
            this.colTienSDDat.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colTienSDDat.Width = 81;
            // 
            // colThueVAT
            // 
            this.colThueVAT.Caption = "Thuế VAT";
            this.colThueVAT.DisplayFormat.FormatString = "n0";
            this.colThueVAT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colThueVAT.FieldName = "ThueVAT";
            this.colThueVAT.Name = "colThueVAT";
            this.colThueVAT.OptionsColumn.AllowEdit = false;
            this.colThueVAT.OptionsColumn.AllowFocus = false;
            this.colThueVAT.SummaryItem.DisplayFormat = "{0:n0}";
            this.colThueVAT.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colThueVAT.Visible = true;
            this.colThueVAT.VisibleIndex = 7;
            this.colThueVAT.Width = 86;
            // 
            // colTongCong
            // 
            this.colTongCong.Caption = "Tổng cộng";
            this.colTongCong.DisplayFormat.FormatString = "n0";
            this.colTongCong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTongCong.FieldName = "TongCong";
            this.colTongCong.Name = "colTongCong";
            this.colTongCong.OptionsColumn.AllowEdit = false;
            this.colTongCong.OptionsColumn.AllowFocus = false;
            this.colTongCong.SummaryItem.DisplayFormat = "{0:n0}";
            this.colTongCong.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colTongCong.Visible = true;
            this.colTongCong.VisibleIndex = 8;
            this.colTongCong.Width = 110;
            // 
            // colDaThu
            // 
            this.colDaThu.Caption = "Đã thu";
            this.colDaThu.DisplayFormat.FormatString = "n0";
            this.colDaThu.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDaThu.FieldName = "DaThu";
            this.colDaThu.Name = "colDaThu";
            this.colDaThu.OptionsColumn.AllowEdit = false;
            this.colDaThu.OptionsColumn.AllowFocus = false;
            this.colDaThu.SummaryItem.DisplayFormat = "{0:n0}";
            this.colDaThu.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colDaThu.Visible = true;
            this.colDaThu.VisibleIndex = 9;
            this.colDaThu.Width = 110;
            // 
            // colConNo
            // 
            this.colConNo.Caption = "Còn nợ";
            this.colConNo.DisplayFormat.FormatString = "n0";
            this.colConNo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colConNo.FieldName = "ConNo";
            this.colConNo.Name = "colConNo";
            this.colConNo.OptionsColumn.AllowEdit = false;
            this.colConNo.OptionsColumn.AllowFocus = false;
            this.colConNo.SummaryItem.DisplayFormat = "{0:n0}";
            this.colConNo.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colConNo.Visible = true;
            this.colConNo.VisibleIndex = 10;
            this.colConNo.Width = 110;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Quá hạn chuyển sang";
            this.gridColumn1.DisplayFormat.FormatString = "n0";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "QuaHan";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 11;
            this.gridColumn1.Width = 120;
            // 
            // colNgay1
            // 
            this.colNgay1.AppearanceHeader.Options.UseTextOptions = true;
            this.colNgay1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgay1.Caption = "Ngày 1";
            this.colNgay1.DisplayFormat.FormatString = "n0";
            this.colNgay1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNgay1.FieldName = "Ngay1";
            this.colNgay1.Name = "colNgay1";
            this.colNgay1.OptionsColumn.AllowEdit = false;
            this.colNgay1.OptionsColumn.AllowFocus = false;
            this.colNgay1.SummaryItem.DisplayFormat = "{0:n0}";
            this.colNgay1.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colNgay1.Visible = true;
            this.colNgay1.VisibleIndex = 12;
            this.colNgay1.Width = 110;
            // 
            // colNgay2
            // 
            this.colNgay2.AppearanceHeader.Options.UseTextOptions = true;
            this.colNgay2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgay2.Caption = "Ngày 2";
            this.colNgay2.DisplayFormat.FormatString = "n0";
            this.colNgay2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNgay2.FieldName = "Ngay2";
            this.colNgay2.Name = "colNgay2";
            this.colNgay2.OptionsColumn.AllowEdit = false;
            this.colNgay2.OptionsColumn.AllowFocus = false;
            this.colNgay2.SummaryItem.DisplayFormat = "{0:n0}";
            this.colNgay2.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colNgay2.Visible = true;
            this.colNgay2.VisibleIndex = 13;
            this.colNgay2.Width = 110;
            // 
            // colNgay3
            // 
            this.colNgay3.AppearanceHeader.Options.UseTextOptions = true;
            this.colNgay3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgay3.Caption = "Ngày 3";
            this.colNgay3.DisplayFormat.FormatString = "n0";
            this.colNgay3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNgay3.FieldName = "Ngay3";
            this.colNgay3.Name = "colNgay3";
            this.colNgay3.OptionsColumn.AllowEdit = false;
            this.colNgay3.OptionsColumn.AllowFocus = false;
            this.colNgay3.SummaryItem.DisplayFormat = "{0:n0}";
            this.colNgay3.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colNgay3.Visible = true;
            this.colNgay3.VisibleIndex = 14;
            this.colNgay3.Width = 110;
            // 
            // colNgay4
            // 
            this.colNgay4.AppearanceHeader.Options.UseTextOptions = true;
            this.colNgay4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgay4.Caption = "Ngày 4";
            this.colNgay4.DisplayFormat.FormatString = "n0";
            this.colNgay4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNgay4.FieldName = "Ngay4";
            this.colNgay4.Name = "colNgay4";
            this.colNgay4.OptionsColumn.AllowEdit = false;
            this.colNgay4.OptionsColumn.AllowFocus = false;
            this.colNgay4.SummaryItem.DisplayFormat = "{0:n0}";
            this.colNgay4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colNgay4.Visible = true;
            this.colNgay4.VisibleIndex = 15;
            this.colNgay4.Width = 110;
            // 
            // colNgay5
            // 
            this.colNgay5.AppearanceHeader.Options.UseTextOptions = true;
            this.colNgay5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgay5.Caption = "Ngày 5";
            this.colNgay5.DisplayFormat.FormatString = "n0";
            this.colNgay5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNgay5.FieldName = "Ngay5";
            this.colNgay5.Name = "colNgay5";
            this.colNgay5.OptionsColumn.AllowEdit = false;
            this.colNgay5.OptionsColumn.AllowFocus = false;
            this.colNgay5.SummaryItem.DisplayFormat = "{0:n0}";
            this.colNgay5.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colNgay5.Visible = true;
            this.colNgay5.VisibleIndex = 16;
            this.colNgay5.Width = 110;
            // 
            // colNgay6
            // 
            this.colNgay6.AppearanceHeader.Options.UseTextOptions = true;
            this.colNgay6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgay6.Caption = "Ngày 6";
            this.colNgay6.DisplayFormat.FormatString = "n0";
            this.colNgay6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNgay6.FieldName = "Ngay6";
            this.colNgay6.Name = "colNgay6";
            this.colNgay6.OptionsColumn.AllowEdit = false;
            this.colNgay6.OptionsColumn.AllowFocus = false;
            this.colNgay6.SummaryItem.DisplayFormat = "{0:n0}";
            this.colNgay6.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colNgay6.Visible = true;
            this.colNgay6.VisibleIndex = 17;
            this.colNgay6.Width = 110;
            // 
            // colNgay7
            // 
            this.colNgay7.AppearanceHeader.Options.UseTextOptions = true;
            this.colNgay7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgay7.Caption = "Ngày 7";
            this.colNgay7.DisplayFormat.FormatString = "n0";
            this.colNgay7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNgay7.FieldName = "Ngay7";
            this.colNgay7.Name = "colNgay7";
            this.colNgay7.OptionsColumn.AllowEdit = false;
            this.colNgay7.OptionsColumn.AllowFocus = false;
            this.colNgay7.SummaryItem.DisplayFormat = "{0:n0}";
            this.colNgay7.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colNgay7.Visible = true;
            this.colNgay7.VisibleIndex = 18;
            this.colNgay7.Width = 110;
            // 
            // colNgay8
            // 
            this.colNgay8.AppearanceHeader.Options.UseTextOptions = true;
            this.colNgay8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgay8.Caption = "Ngày 8";
            this.colNgay8.DisplayFormat.FormatString = "n0";
            this.colNgay8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNgay8.FieldName = "Ngay8";
            this.colNgay8.Name = "colNgay8";
            this.colNgay8.OptionsColumn.AllowEdit = false;
            this.colNgay8.OptionsColumn.AllowFocus = false;
            this.colNgay8.SummaryItem.DisplayFormat = "{0:n0}";
            this.colNgay8.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colNgay8.Visible = true;
            this.colNgay8.VisibleIndex = 19;
            this.colNgay8.Width = 110;
            // 
            // colNgay9
            // 
            this.colNgay9.AppearanceHeader.Options.UseTextOptions = true;
            this.colNgay9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgay9.Caption = "Ngày 9";
            this.colNgay9.DisplayFormat.FormatString = "n0";
            this.colNgay9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNgay9.FieldName = "Ngay9";
            this.colNgay9.Name = "colNgay9";
            this.colNgay9.OptionsColumn.AllowEdit = false;
            this.colNgay9.OptionsColumn.AllowFocus = false;
            this.colNgay9.SummaryItem.DisplayFormat = "{0:n0}";
            this.colNgay9.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colNgay9.Visible = true;
            this.colNgay9.VisibleIndex = 20;
            this.colNgay9.Width = 110;
            // 
            // colNgay10
            // 
            this.colNgay10.AppearanceHeader.Options.UseTextOptions = true;
            this.colNgay10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgay10.Caption = "Ngày 10";
            this.colNgay10.DisplayFormat.FormatString = "n0";
            this.colNgay10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNgay10.FieldName = "Ngay10";
            this.colNgay10.Name = "colNgay10";
            this.colNgay10.OptionsColumn.AllowEdit = false;
            this.colNgay10.OptionsColumn.AllowFocus = false;
            this.colNgay10.SummaryItem.DisplayFormat = "{0:n0}";
            this.colNgay10.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colNgay10.Visible = true;
            this.colNgay10.VisibleIndex = 21;
            this.colNgay10.Width = 110;
            // 
            // colNgay11
            // 
            this.colNgay11.AppearanceHeader.Options.UseTextOptions = true;
            this.colNgay11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgay11.Caption = "Ngày 11";
            this.colNgay11.DisplayFormat.FormatString = "n0";
            this.colNgay11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNgay11.FieldName = "Ngay11";
            this.colNgay11.Name = "colNgay11";
            this.colNgay11.OptionsColumn.AllowEdit = false;
            this.colNgay11.OptionsColumn.AllowFocus = false;
            this.colNgay11.SummaryItem.DisplayFormat = "{0:n0}";
            this.colNgay11.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colNgay11.Visible = true;
            this.colNgay11.VisibleIndex = 22;
            this.colNgay11.Width = 110;
            // 
            // colNgay12
            // 
            this.colNgay12.AppearanceHeader.Options.UseTextOptions = true;
            this.colNgay12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgay12.Caption = "Ngày 12";
            this.colNgay12.DisplayFormat.FormatString = "n0";
            this.colNgay12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNgay12.FieldName = "Ngay12";
            this.colNgay12.Name = "colNgay12";
            this.colNgay12.OptionsColumn.AllowEdit = false;
            this.colNgay12.OptionsColumn.AllowFocus = false;
            this.colNgay12.SummaryItem.DisplayFormat = "{0:n0}";
            this.colNgay12.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colNgay12.Visible = true;
            this.colNgay12.VisibleIndex = 23;
            this.colNgay12.Width = 110;
            // 
            // colTongCongNo
            // 
            this.colTongCongNo.Caption = "Tồng công nợ đến hạn";
            this.colTongCongNo.DisplayFormat.FormatString = "n0";
            this.colTongCongNo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTongCongNo.FieldName = "TongCongNo";
            this.colTongCongNo.Name = "colTongCongNo";
            this.colTongCongNo.OptionsColumn.AllowEdit = false;
            this.colTongCongNo.OptionsColumn.AllowFocus = false;
            this.colTongCongNo.SummaryItem.DisplayFormat = "{0:n0}";
            this.colTongCongNo.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colTongCongNo.Visible = true;
            this.colTongCongNo.VisibleIndex = 24;
            this.colTongCongNo.Width = 150;
            // 
            // colTienMuaCH
            // 
            this.colTienMuaCH.Caption = "Tiền mua CH";
            this.colTienMuaCH.DisplayFormat.FormatString = "n0";
            this.colTienMuaCH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTienMuaCH.FieldName = "TienMuaCH";
            this.colTienMuaCH.Name = "colTienMuaCH";
            this.colTienMuaCH.OptionsColumn.AllowEdit = false;
            this.colTienMuaCH.OptionsColumn.AllowFocus = false;
            this.colTienMuaCH.SummaryItem.DisplayFormat = "{0:n0}";
            this.colTienMuaCH.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colTienMuaCH.Visible = true;
            this.colTienMuaCH.VisibleIndex = 25;
            this.colTienMuaCH.Width = 110;
            // 
            // colTongThueVAT
            // 
            this.colTongThueVAT.Caption = "Thuế VAT";
            this.colTongThueVAT.DisplayFormat.FormatString = "n0";
            this.colTongThueVAT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTongThueVAT.FieldName = "TongThueVAT";
            this.colTongThueVAT.Name = "colTongThueVAT";
            this.colTongThueVAT.OptionsColumn.AllowEdit = false;
            this.colTongThueVAT.OptionsColumn.AllowFocus = false;
            this.colTongThueVAT.SummaryItem.DisplayFormat = "{0:n0}";
            this.colTongThueVAT.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colTongThueVAT.Visible = true;
            this.colTongThueVAT.VisibleIndex = 26;
            this.colTongThueVAT.Width = 110;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl1;
            this.gridView2.Name = "gridView2";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(897, 527);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // CongNoThang_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 562);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.gridControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CongNoThang_frm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo công nợ theo tháng";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CongNo_frm_Load);
            this.Shown += new System.EventHandler(this.CongNo_frm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn colMaSo;
        private DevExpress.XtraGrid.Columns.GridColumn colHoTenKH;
        private DevExpress.XtraGrid.Columns.GridColumn colSoHD;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayHD;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGia;
        private DevExpress.XtraGrid.Columns.GridColumn colDienTich;
        private DevExpress.XtraGrid.Columns.GridColumn colThanhTien;
        private DevExpress.XtraGrid.Columns.GridColumn colThueVAT;
        private DevExpress.XtraGrid.Columns.GridColumn colTongCong;
        private DevExpress.XtraGrid.Columns.GridColumn colDaThu;
        private DevExpress.XtraGrid.Columns.GridColumn colConNo;
        private DevExpress.XtraGrid.Columns.GridColumn colNgay1;
        private DevExpress.XtraGrid.Columns.GridColumn colNgay2;
        private DevExpress.XtraGrid.Columns.GridColumn colNgay3;
        private DevExpress.XtraGrid.Columns.GridColumn colNgay4;
        private DevExpress.XtraGrid.Columns.GridColumn colNgay5;
        private DevExpress.XtraGrid.Columns.GridColumn colNgay6;
        private DevExpress.XtraGrid.Columns.GridColumn colNgay7;
        private DevExpress.XtraGrid.Columns.GridColumn colNgay8;
        private DevExpress.XtraGrid.Columns.GridColumn colNgay9;
        private DevExpress.XtraGrid.Columns.GridColumn colNgay10;
        private DevExpress.XtraGrid.Columns.GridColumn colNgay11;
        private DevExpress.XtraGrid.Columns.GridColumn colNgay12;
        private DevExpress.XtraGrid.Columns.GridColumn colTongCongNo;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraGrid.Columns.GridColumn colTienMuaCH;
        private DevExpress.XtraGrid.Columns.GridColumn colTongThueVAT;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colTienSDDat;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}