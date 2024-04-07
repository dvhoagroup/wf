namespace BEEREMA.BaoCao
{
    partial class frmReportSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportSetting));
            this.listField1 = new DevExpress.XtraEditors.ListBoxControl();
            this.listField2 = new DevExpress.XtraEditors.ListBoxControl();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton();
            this.btnUp = new DevExpress.XtraEditors.SimpleButton();
            this.btnDown = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.vgcField = new DevExpress.XtraVerticalGrid.VGridControl();
            this.cmbAlignment = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            this.rWidth = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rTextAlignment = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rTextColor = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rBGColor = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rTextSize = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.categoryRow1 = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rHeaderAlignment = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rHeaderColor = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rHeaderBGColor = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rHeaderSize = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbOrientation = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.spinRight = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.spinLeft = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.spinBottom = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.spinTop = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbPaperKind = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnAccept = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnDefault = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.listField1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listField2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vgcField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAlignment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdbOrientation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinRight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinLeft.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinBottom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTop.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaperKind.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // listField1
            // 
            this.listField1.DisplayMember = "Description";
            this.listField1.Location = new System.Drawing.Point(16, 20);
            this.listField1.Name = "listField1";
            this.listField1.Size = new System.Drawing.Size(173, 125);
            this.listField1.TabIndex = 0;
            this.listField1.ValueMember = "FieldName";
            // 
            // listField2
            // 
            this.listField2.DisplayMember = "Description";
            this.listField2.Location = new System.Drawing.Point(244, 20);
            this.listField2.Name = "listField2";
            this.listField2.Size = new System.Drawing.Size(173, 125);
            this.listField2.TabIndex = 0;
            this.listField2.ValueMember = "FieldName";
            this.listField2.SelectedIndexChanged += new System.EventHandler(this.listField2_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.ImageIndex = 2;
            this.btnAdd.ImageList = this.imageCollection1;
            this.btnAdd.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnAdd.Location = new System.Drawing.Point(195, 22);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(43, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "botton.png");
            this.imageCollection1.Images.SetKeyName(1, "previous-icon.png");
            this.imageCollection1.Images.SetKeyName(2, "next-icon.png");
            this.imageCollection1.Images.SetKeyName(3, "top.png");
            // 
            // btnRemove
            // 
            this.btnRemove.ImageIndex = 1;
            this.btnRemove.ImageList = this.imageCollection1;
            this.btnRemove.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnRemove.Location = new System.Drawing.Point(195, 51);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(43, 23);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnUp
            // 
            this.btnUp.ImageIndex = 3;
            this.btnUp.ImageList = this.imageCollection1;
            this.btnUp.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnUp.Location = new System.Drawing.Point(195, 89);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(43, 23);
            this.btnUp.TabIndex = 1;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.ImageIndex = 0;
            this.btnDown.ImageList = this.imageCollection1;
            this.btnDown.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnDown.Location = new System.Drawing.Point(195, 118);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(43, 23);
            this.btnDown.TabIndex = 1;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.vgcField);
            this.groupBox1.Controls.Add(this.listField1);
            this.groupBox1.Controls.Add(this.btnDown);
            this.groupBox1.Controls.Add(this.listField2);
            this.groupBox1.Controls.Add(this.btnUp);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnRemove);
            this.groupBox1.Location = new System.Drawing.Point(12, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(667, 159);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Trường thông tin";
            // 
            // vgcField
            // 
            this.vgcField.Location = new System.Drawing.Point(423, 20);
            this.vgcField.Name = "vgcField";
            this.vgcField.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbAlignment,
            this.repositoryItemColorEdit1});
            this.vgcField.RowHeaderWidth = 90;
            this.vgcField.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rWidth,
            this.rTextAlignment,
            this.rTextColor,
            this.rBGColor,
            this.rTextSize,
            this.categoryRow1});
            this.vgcField.Size = new System.Drawing.Size(228, 125);
            this.vgcField.TabIndex = 0;
            // 
            // cmbAlignment
            // 
            this.cmbAlignment.AutoHeight = false;
            this.cmbAlignment.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAlignment.Name = "cmbAlignment";
            this.cmbAlignment.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // repositoryItemColorEdit1
            // 
            this.repositoryItemColorEdit1.AutoHeight = false;
            this.repositoryItemColorEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemColorEdit1.ColorText = DevExpress.XtraEditors.Controls.ColorText.Integer;
            this.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1";
            this.repositoryItemColorEdit1.StoreColorAsInteger = true;
            // 
            // rWidth
            // 
            this.rWidth.Name = "rWidth";
            this.rWidth.Properties.Caption = "Chiều rộng";
            this.rWidth.Properties.FieldName = "Width";
            // 
            // rTextAlignment
            // 
            this.rTextAlignment.Name = "rTextAlignment";
            this.rTextAlignment.Properties.Caption = "Canh lề";
            this.rTextAlignment.Properties.FieldName = "TextAlignment";
            this.rTextAlignment.Properties.RowEdit = this.cmbAlignment;
            // 
            // rTextColor
            // 
            this.rTextColor.Height = 20;
            this.rTextColor.Name = "rTextColor";
            this.rTextColor.Properties.Caption = "Màu chữ";
            this.rTextColor.Properties.FieldName = "TextColor";
            this.rTextColor.Properties.RowEdit = this.repositoryItemColorEdit1;
            // 
            // rBGColor
            // 
            this.rBGColor.Name = "rBGColor";
            this.rBGColor.Properties.Caption = "Màu nền";
            this.rBGColor.Properties.FieldName = "BGColor";
            this.rBGColor.Properties.RowEdit = this.repositoryItemColorEdit1;
            // 
            // rTextSize
            // 
            this.rTextSize.Name = "rTextSize";
            this.rTextSize.Properties.Caption = "Cở chữ";
            this.rTextSize.Properties.FieldName = "TextSize";
            // 
            // categoryRow1
            // 
            this.categoryRow1.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rHeaderAlignment,
            this.rHeaderColor,
            this.rHeaderBGColor,
            this.rHeaderSize});
            this.categoryRow1.Height = 15;
            this.categoryRow1.Name = "categoryRow1";
            this.categoryRow1.Properties.Caption = "Tiêu đề";
            // 
            // rHeaderAlignment
            // 
            this.rHeaderAlignment.Name = "rHeaderAlignment";
            this.rHeaderAlignment.Properties.Caption = "Canh lề";
            this.rHeaderAlignment.Properties.FieldName = "HeaderAlignment";
            this.rHeaderAlignment.Properties.RowEdit = this.cmbAlignment;
            // 
            // rHeaderColor
            // 
            this.rHeaderColor.Name = "rHeaderColor";
            this.rHeaderColor.Properties.Caption = "Màu chữ";
            this.rHeaderColor.Properties.FieldName = "HeaderColor";
            this.rHeaderColor.Properties.RowEdit = this.repositoryItemColorEdit1;
            // 
            // rHeaderBGColor
            // 
            this.rHeaderBGColor.Name = "rHeaderBGColor";
            this.rHeaderBGColor.Properties.Caption = "Màu nền";
            this.rHeaderBGColor.Properties.FieldName = "HeaderBGColor";
            this.rHeaderBGColor.Properties.RowEdit = this.repositoryItemColorEdit1;
            // 
            // rHeaderSize
            // 
            this.rHeaderSize.Name = "rHeaderSize";
            this.rHeaderSize.Properties.Caption = "Cở chữ";
            this.rHeaderSize.Properties.FieldName = "HeaderSize";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbOrientation);
            this.groupBox2.Controls.Add(this.labelControl6);
            this.groupBox2.Controls.Add(this.spinRight);
            this.groupBox2.Controls.Add(this.labelControl5);
            this.groupBox2.Controls.Add(this.spinLeft);
            this.groupBox2.Controls.Add(this.labelControl4);
            this.groupBox2.Controls.Add(this.spinBottom);
            this.groupBox2.Controls.Add(this.labelControl3);
            this.groupBox2.Controls.Add(this.spinTop);
            this.groupBox2.Controls.Add(this.labelControl2);
            this.groupBox2.Controls.Add(this.cmbPaperKind);
            this.groupBox2.Controls.Add(this.labelControl1);
            this.groupBox2.Location = new System.Drawing.Point(12, 173);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(667, 84);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Trang in";
            // 
            // rdbOrientation
            // 
            this.rdbOrientation.Location = new System.Drawing.Point(80, 48);
            this.rdbOrientation.Name = "rdbOrientation";
            this.rdbOrientation.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rdbOrientation.Properties.Appearance.Options.UseBackColor = true;
            this.rdbOrientation.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rdbOrientation.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "Ngang"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "Dọc")});
            this.rdbOrientation.Size = new System.Drawing.Size(122, 24);
            this.rdbOrientation.TabIndex = 5;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(20, 51);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(60, 13);
            this.labelControl6.TabIndex = 4;
            this.labelControl6.Text = "Định hướng:";
            // 
            // spinRight
            // 
            this.spinRight.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinRight.Location = new System.Drawing.Point(239, 48);
            this.spinRight.Name = "spinRight";
            this.spinRight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinRight.Properties.IsFloatValue = false;
            this.spinRight.Properties.Mask.EditMask = "N00";
            this.spinRight.Size = new System.Drawing.Size(63, 20);
            this.spinRight.TabIndex = 3;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(207, 51);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 13);
            this.labelControl5.TabIndex = 2;
            this.labelControl5.Text = "Phải:";
            // 
            // spinLeft
            // 
            this.spinLeft.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinLeft.Location = new System.Drawing.Point(239, 22);
            this.spinLeft.Name = "spinLeft";
            this.spinLeft.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinLeft.Properties.IsFloatValue = false;
            this.spinLeft.Properties.Mask.EditMask = "N00";
            this.spinLeft.Size = new System.Drawing.Size(63, 20);
            this.spinLeft.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(207, 25);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(22, 13);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = "Trái:";
            // 
            // spinBottom
            // 
            this.spinBottom.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinBottom.Location = new System.Drawing.Point(358, 48);
            this.spinBottom.Name = "spinBottom";
            this.spinBottom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinBottom.Properties.IsFloatValue = false;
            this.spinBottom.Properties.Mask.EditMask = "N00";
            this.spinBottom.Size = new System.Drawing.Size(63, 20);
            this.spinBottom.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(326, 51);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(26, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Dưới:";
            // 
            // spinTop
            // 
            this.spinTop.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinTop.Location = new System.Drawing.Point(358, 22);
            this.spinTop.Name = "spinTop";
            this.spinTop.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinTop.Properties.IsFloatValue = false;
            this.spinTop.Properties.Mask.EditMask = "N00";
            this.spinTop.Size = new System.Drawing.Size(63, 20);
            this.spinTop.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(326, 25);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(26, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Trên:";
            // 
            // cmbPaperKind
            // 
            this.cmbPaperKind.Location = new System.Drawing.Point(80, 22);
            this.cmbPaperKind.Name = "cmbPaperKind";
            this.cmbPaperKind.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPaperKind.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbPaperKind.Size = new System.Drawing.Size(100, 20);
            this.cmbPaperKind.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(45, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Khổ giấy:";
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(523, 263);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 4;
            this.btnAccept.Text = "Đồng ý";
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(604, 263);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Bỏ qua";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(12, 263);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(75, 23);
            this.btnDefault.TabIndex = 4;
            this.btnDefault.Text = "Mặc định";
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // frmReportSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 298);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmReportSetting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu hình báo cáo";
            this.Load += new System.EventHandler(this.frmReportSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listField1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listField2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vgcField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAlignment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdbOrientation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinRight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinLeft.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinBottom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTop.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaperKind.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl listField1;
        private DevExpress.XtraEditors.ListBoxControl listField2;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.SimpleButton btnRemove;
        private DevExpress.XtraEditors.SimpleButton btnUp;
        private DevExpress.XtraEditors.SimpleButton btnDown;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraVerticalGrid.VGridControl vgcField;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rWidth;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rTextAlignment;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rTextColor;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rBGColor;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryRow1;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rHeaderAlignment;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rHeaderColor;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rHeaderBGColor;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rTextSize;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rHeaderSize;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.ComboBoxEdit cmbPaperKind;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SpinEdit spinRight;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SpinEdit spinLeft;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SpinEdit spinBottom;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SpinEdit spinTop;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.RadioGroup rdbOrientation;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton btnAccept;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnDefault;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbAlignment;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorEdit repositoryItemColorEdit1;
    }
}