using Microsoft.ConsultingServices.HtmlEditor;
namespace BEE.HoatDong.Maketing
{
    partial class HappyBirthdaySetting_frm
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HappyBirthdaySetting_frm));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.checkedCmbNhomKH = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.htmlContent = new Microsoft.ConsultingServices.HtmlEditor.HtmlEditorControl();
            this.btnFileAttach = new DevExpress.XtraEditors.ButtonEdit();
            this.spinNgayGui = new DevExpress.XtraEditors.SpinEdit();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController();
            this.lookUpMauThiep = new DevExpress.XtraEditors.LookUpEdit();
            this.txtTieuDe = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedCmbNhomKH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFileAttach.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNgayGui.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpMauThiep.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTieuDe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.checkedCmbNhomKH);
            this.panelControl1.Controls.Add(this.htmlContent);
            this.panelControl1.Controls.Add(this.btnFileAttach);
            this.panelControl1.Controls.Add(this.spinNgayGui);
            this.panelControl1.Controls.Add(this.lookUpMauThiep);
            this.panelControl1.Controls.Add(this.txtTieuDe);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(770, 419);
            this.panelControl1.TabIndex = 0;
            // 
            // checkedCmbNhomKH
            // 
            this.checkedCmbNhomKH.EditValue = "";
            this.checkedCmbNhomKH.Location = new System.Drawing.Point(284, 41);
            this.checkedCmbNhomKH.Name = "checkedCmbNhomKH";
            this.checkedCmbNhomKH.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.checkedCmbNhomKH.Properties.DisplayMember = "TenNKH";
            this.checkedCmbNhomKH.Properties.SelectAllItemCaption = "(Chọn tất cả)";
            this.checkedCmbNhomKH.Properties.ValueMember = "MaNKH";
            this.checkedCmbNhomKH.Size = new System.Drawing.Size(166, 20);
            this.checkedCmbNhomKH.TabIndex = 19;
            // 
            // htmlContent
            // 
            this.htmlContent.InnerHtml = null;
            this.htmlContent.InnerText = null;
            this.htmlContent.Location = new System.Drawing.Point(17, 76);
            this.htmlContent.Name = "htmlContent";
            this.htmlContent.Size = new System.Drawing.Size(734, 338);
            this.htmlContent.TabIndex = 7;
            this.htmlContent.ImageBrowser += new Microsoft.ConsultingServices.HtmlEditor.ImageBrowserEventHandler(this.htmlContent_ImageBrowser);
            // 
            // btnFileAttach
            // 
            this.btnFileAttach.Location = new System.Drawing.Point(561, 41);
            this.btnFileAttach.Name = "btnFileAttach";
            this.btnFileAttach.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Chọn", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.btnFileAttach.Properties.ReadOnly = true;
            this.btnFileAttach.Size = new System.Drawing.Size(190, 20);
            this.btnFileAttach.TabIndex = 6;
            this.btnFileAttach.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnFileAttach_ButtonClick);
            // 
            // spinNgayGui
            // 
            this.spinNgayGui.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinNgayGui.Location = new System.Drawing.Point(95, 41);
            this.spinNgayGui.Name = "spinNgayGui";
            this.spinNgayGui.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinNgayGui.Properties.DisplayFormat.FormatString = "{0:n0} ngày";
            this.spinNgayGui.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinNgayGui.Properties.EditFormat.FormatString = "{0:n0} ngày";
            this.spinNgayGui.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinNgayGui.Size = new System.Drawing.Size(70, 20);
            this.spinNgayGui.TabIndex = 5;
            this.spinNgayGui.ToolTip = "Gửi thiệp chúc mừng sinh nhật trước mấy ngày?";
            this.spinNgayGui.ToolTipController = this.toolTipController1;
            this.spinNgayGui.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.spinNgayGui.ToolTipTitle = "Thông tin";
            // 
            // toolTipController1
            // 
            this.toolTipController1.Rounded = true;
            this.toolTipController1.RoundRadius = 4;
            this.toolTipController1.ShowBeak = true;
            // 
            // lookUpMauThiep
            // 
            this.lookUpMauThiep.Location = new System.Drawing.Point(561, 15);
            this.lookUpMauThiep.Name = "lookUpMauThiep";
            this.lookUpMauThiep.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("lookUpMauThiep.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.lookUpMauThiep.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenThiep", "Name1")});
            this.lookUpMauThiep.Properties.DisplayMember = "TenThiep";
            this.lookUpMauThiep.Properties.NullText = "";
            this.lookUpMauThiep.Properties.ShowHeader = false;
            this.lookUpMauThiep.Properties.ValueMember = "MaThiep";
            this.lookUpMauThiep.Size = new System.Drawing.Size(190, 22);
            this.lookUpMauThiep.TabIndex = 4;
            this.lookUpMauThiep.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lookUpMauThiep_ButtonClick);
            this.lookUpMauThiep.EditValueChanged += new System.EventHandler(this.lookUpMauThiep_EditValueChanged);
            // 
            // txtTieuDe
            // 
            this.txtTieuDe.Location = new System.Drawing.Point(95, 15);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.Size = new System.Drawing.Size(355, 20);
            this.txtTieuDe.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(186, 44);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(89, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Nhóm khách hàng:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(490, 44);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(65, 13);
            this.labelControl5.TabIndex = 2;
            this.labelControl5.Text = "File đính kèm:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(490, 18);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Mẫu thiệp:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(17, 44);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(49, 13);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = "Gửi trước:";
            this.labelControl4.ToolTip = "Gửi thiệp chúc mừng sinh nhật trước mấy ngày?";
            this.labelControl4.ToolTipController = this.toolTipController1;
            this.labelControl4.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.labelControl4.ToolTipTitle = "Thông tin";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(39, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Tiêu đề:";
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.ImageIndex = 4;
            this.btnHuy.ImageList = this.imageCollection1;
            this.btnHuy.Location = new System.Drawing.Point(704, 437);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(78, 23);
            this.btnHuy.TabIndex = 18;
            this.btnHuy.Text = "Hủy - ESC";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnDongY
            // 
            this.btnDongY.ImageIndex = 6;
            this.btnDongY.ImageList = this.imageCollection1;
            this.btnDongY.Location = new System.Drawing.Point(605, 437);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(92, 23);
            this.btnDongY.TabIndex = 17;
            this.btnDongY.Text = "Lưu && Đóng";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
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
            // 
            // HappyBirthdaySetting_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 472);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnDongY);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HappyBirthdaySetting_frm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cài đặt chúc mừng sinh nhật khách hàng";
            this.Load += new System.EventHandler(this.HappyBirthdaySetting_frm_Load);
            this.Shown += new System.EventHandler(this.HappyBirthdaySetting_frm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedCmbNhomKH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFileAttach.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNgayGui.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpMauThiep.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTieuDe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtTieuDe;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit lookUpMauThiep;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SpinEdit spinNgayGui;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ButtonEdit btnFileAttach;
        private HtmlEditorControl htmlContent;
        private DevExpress.XtraEditors.CheckedComboBoxEdit checkedCmbNhomKH;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}