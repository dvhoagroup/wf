namespace BEE.QuangCao.Mail
{
    partial class frmReceiveList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReceiveList));
            this.lblStep = new DevExpress.XtraEditors.LabelControl();
            this.btnPrevious = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.btnAction = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblAmountSelect = new DevExpress.XtraEditors.LabelControl();
            this.gcCustomers = new DevExpress.XtraGrid.GridControl();
            this.gridCustomers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colDanhXung = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHoTen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDienThoai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiaChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChucVu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.radioGroupObject = new DevExpress.XtraEditors.RadioGroup();
            this.chkSelectAll = new DevExpress.XtraEditors.CheckEdit();
            this.btnOpenFile = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lookLoaiKH = new DevExpress.XtraEditors.LookUpEdit();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupObject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelectAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookLoaiKH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStep
            // 
            this.lblStep.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblStep.Location = new System.Drawing.Point(12, 12);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(161, 17);
            this.lblStep.TabIndex = 0;
            this.lblStep.Text = "Bước 1/3: Chọn nguồn";
            // 
            // btnPrevious
            // 
            this.btnPrevious.Enabled = false;
            this.btnPrevious.ImageIndex = 18;
            this.btnPrevious.ImageList = this.imageCollection1;
            this.btnPrevious.Location = new System.Drawing.Point(464, 473);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 23);
            this.btnPrevious.TabIndex = 3;
            this.btnPrevious.Text = "Quay lại";
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.ImageIndex = 19;
            this.btnNext.ImageList = this.imageCollection1;
            this.btnNext.Location = new System.Drawing.Point(545, 473);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "Tiếp theo";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnAction
            // 
            this.btnAction.Enabled = false;
            this.btnAction.ImageIndex = 6;
            this.btnAction.ImageList = this.imageCollection1;
            this.btnAction.Location = new System.Drawing.Point(626, 473);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(75, 23);
            this.btnAction.TabIndex = 3;
            this.btnAction.Text = "Thực hiện";
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ImageIndex = 4;
            this.btnCancel.ImageList = this.imageCollection1;
            this.btnCancel.Location = new System.Drawing.Point(707, 473);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.ContentImage = global::BEE.QuangCao.Properties.Resources.bg_;
            this.panelControl1.ContentImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.panelControl1.Controls.Add(this.lblAmountSelect);
            this.panelControl1.Controls.Add(this.gcCustomers);
            this.panelControl1.Controls.Add(this.radioGroupObject);
            this.panelControl1.Location = new System.Drawing.Point(12, 44);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(770, 414);
            this.panelControl1.TabIndex = 2;
            // 
            // lblAmountSelect
            // 
            this.lblAmountSelect.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblAmountSelect.Location = new System.Drawing.Point(101, 173);
            this.lblAmountSelect.Name = "lblAmountSelect";
            this.lblAmountSelect.Size = new System.Drawing.Size(588, 17);
            this.lblAmountSelect.TabIndex = 3;
            this.lblAmountSelect.Text = "1 người nhận được chọn để đưa vào danh sách. Click nút \"Thực hiện\" để hoàn thành " +
                "quá trình.";
            this.lblAmountSelect.Visible = false;
            // 
            // gcCustomers
            // 
            this.gcCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCustomers.Location = new System.Drawing.Point(0, 0);
            this.gcCustomers.MainView = this.gridCustomers;
            this.gcCustomers.Name = "gcCustomers";
            this.gcCustomers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkCheck});
            this.gcCustomers.Size = new System.Drawing.Size(770, 414);
            this.gcCustomers.TabIndex = 2;
            this.gcCustomers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridCustomers});
            this.gcCustomers.Visible = false;
            // 
            // gridCustomers
            // 
            this.gridCustomers.Appearance.GroupFooter.Options.UseTextOptions = true;
            this.gridCustomers.Appearance.GroupFooter.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridCustomers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCheck,
            this.colDanhXung,
            this.colHoTen,
            this.gridColumn1,
            this.colDienThoai,
            this.gridColumn5,
            this.colDiaChi,
            this.colChucVu,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gridCustomers.GridControl = this.gcCustomers;
            this.gridCustomers.Name = "gridCustomers";
            this.gridCustomers.OptionsMenu.EnableColumnMenu = false;
            this.gridCustomers.OptionsMenu.EnableFooterMenu = false;
            this.gridCustomers.OptionsView.ColumnAutoWidth = false;
            this.gridCustomers.OptionsView.ShowAutoFilterRow = true;
            this.gridCustomers.OptionsView.ShowFooter = true;
            this.gridCustomers.OptionsView.ShowGroupPanel = false;
            // 
            // colCheck
            // 
            this.colCheck.Caption = " ";
            this.colCheck.ColumnEdit = this.chkCheck;
            this.colCheck.FieldName = "IsCheck";
            this.colCheck.Name = "colCheck";
            this.colCheck.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.colCheck.OptionsColumn.AllowSize = false;
            this.colCheck.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colCheck.OptionsColumn.FixedWidth = true;
            this.colCheck.Visible = true;
            this.colCheck.VisibleIndex = 0;
            this.colCheck.Width = 30;
            // 
            // chkCheck
            // 
            this.chkCheck.AutoHeight = false;
            this.chkCheck.Name = "chkCheck";
            this.chkCheck.EditValueChanged += new System.EventHandler(this.chkCheck_EditValueChanged);
            // 
            // colDanhXung
            // 
            this.colDanhXung.Caption = "Xưng hô";
            this.colDanhXung.FieldName = "Vocative";
            this.colDanhXung.Name = "colDanhXung";
            this.colDanhXung.SummaryItem.DisplayFormat = "{0} dòng";
            this.colDanhXung.SummaryItem.FieldName = "TenQD";
            this.colDanhXung.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.colDanhXung.Visible = true;
            this.colDanhXung.VisibleIndex = 1;
            this.colDanhXung.Width = 50;
            // 
            // colHoTen
            // 
            this.colHoTen.Caption = "Họ và tên";
            this.colHoTen.FieldName = "FullName";
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.Visible = true;
            this.colHoTen.VisibleIndex = 2;
            this.colHoTen.Width = 118;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Ngày sinh";
            this.gridColumn1.FieldName = "BirthDate";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 4;
            this.gridColumn1.Width = 76;
            // 
            // colDienThoai
            // 
            this.colDienThoai.Caption = "Điện thoại";
            this.colDienThoai.FieldName = "Phone";
            this.colDienThoai.Name = "colDienThoai";
            this.colDienThoai.Visible = true;
            this.colDienThoai.VisibleIndex = 3;
            this.colDienThoai.Width = 97;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Email";
            this.gridColumn5.FieldName = "Email";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 159;
            // 
            // colDiaChi
            // 
            this.colDiaChi.Caption = "Địa chỉ";
            this.colDiaChi.FieldName = "HomeAddress";
            this.colDiaChi.Name = "colDiaChi";
            this.colDiaChi.Visible = true;
            this.colDiaChi.VisibleIndex = 6;
            this.colDiaChi.Width = 176;
            // 
            // colChucVu
            // 
            this.colChucVu.Caption = "Chức vụ";
            this.colChucVu.FieldName = "JobTitle";
            this.colChucVu.Name = "colChucVu";
            this.colChucVu.Visible = true;
            this.colChucVu.VisibleIndex = 7;
            this.colChucVu.Width = 78;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Phòng ban";
            this.gridColumn2.FieldName = "Department";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 8;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Công ty";
            this.gridColumn3.FieldName = "CompanyName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 9;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Địa chỉ công ty";
            this.gridColumn4.FieldName = "BusinessAddress";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 10;
            // 
            // radioGroupObject
            // 
            this.radioGroupObject.EditValue = 1;
            this.radioGroupObject.Location = new System.Drawing.Point(18, 14);
            this.radioGroupObject.Name = "radioGroupObject";
            this.radioGroupObject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroupObject.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Khách hàng"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "Nhân viên"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(3, "Nhập khẩu (file excel)")});
            this.radioGroupObject.Size = new System.Drawing.Size(157, 103);
            this.radioGroupObject.TabIndex = 1;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.Location = new System.Drawing.Point(10, 473);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Properties.Caption = "Chọn/bỏ tất cả";
            this.chkSelectAll.Size = new System.Drawing.Size(177, 19);
            this.chkSelectAll.TabIndex = 4;
            this.chkSelectAll.Visible = false;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Enabled = false;
            this.btnOpenFile.ImageIndex = 10;
            this.btnOpenFile.ImageList = this.imageCollection1;
            this.btnOpenFile.Location = new System.Drawing.Point(383, 473);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFile.TabIndex = 3;
            this.btnOpenFile.Text = "Mở file";
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.LineVisible = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 461);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(770, 10);
            this.labelControl1.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.LineVisible = true;
            this.labelControl2.Location = new System.Drawing.Point(12, 28);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(770, 10);
            this.labelControl2.TabIndex = 5;
            // 
            // lookLoaiKH
            // 
            this.lookLoaiKH.Location = new System.Drawing.Point(626, 9);
            this.lookLoaiKH.Name = "lookLoaiKH";
            this.lookLoaiKH.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookLoaiKH.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenNKH", "Name1")});
            this.lookLoaiKH.Properties.DisplayMember = "TenNKH";
            this.lookLoaiKH.Properties.NullText = "Loại khách hàng";
            this.lookLoaiKH.Properties.ShowHeader = false;
            this.lookLoaiKH.Properties.ValueMember = "MaNKH";
            this.lookLoaiKH.Size = new System.Drawing.Size(156, 20);
            this.lookLoaiKH.TabIndex = 6;
            this.lookLoaiKH.EditValueChanged += new System.EventHandler(this.lookLoaiKH_EditValueChanged);
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
            // 
            // frmReceiveList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 507);
            this.Controls.Add(this.lookLoaiKH);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnAction);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.lblStep);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReceiveList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn người nhận";
            this.Load += new System.EventHandler(this.frmReceiveList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupObject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelectAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookLoaiKH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblStep;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnAction;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnPrevious;
        private DevExpress.XtraEditors.RadioGroup radioGroupObject;
        private DevExpress.XtraGrid.GridControl gcCustomers;
        private DevExpress.XtraGrid.Views.Grid.GridView gridCustomers;
        private DevExpress.XtraGrid.Columns.GridColumn colDanhXung;
        private DevExpress.XtraGrid.Columns.GridColumn colHoTen;
        private DevExpress.XtraGrid.Columns.GridColumn colDienThoai;
        private DevExpress.XtraGrid.Columns.GridColumn colDiaChi;
        private DevExpress.XtraEditors.LabelControl lblAmountSelect;
        private DevExpress.XtraGrid.Columns.GridColumn colCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkCheck;
        private DevExpress.XtraEditors.CheckEdit chkSelectAll;
        private DevExpress.XtraEditors.SimpleButton btnOpenFile;
        private DevExpress.XtraGrid.Columns.GridColumn colChucVu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit lookLoaiKH;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}