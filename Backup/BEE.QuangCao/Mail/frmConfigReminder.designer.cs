using Microsoft.ConsultingServices.HtmlEditor;
namespace BEE.QuangCao.Mail
{
    partial class frmConfigReminder
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfigReminder));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.htmlContent = new Microsoft.ConsultingServices.HtmlEditor.HtmlEditorControl();
            this.lookUpMail = new DevExpress.XtraEditors.LookUpEdit();
            this.chkIsAuto = new DevExpress.XtraEditors.CheckEdit();
            this.lookUpTemplate = new DevExpress.XtraEditors.LookUpEdit();
            this.dateSend = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateReminder = new DevExpress.XtraEditors.DateEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.spinNhacTruoc = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpMail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsAuto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpTemplate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSend.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSend.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReminder.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReminder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNhacTruoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.htmlContent);
            this.panelControl1.Controls.Add(this.lookUpMail);
            this.panelControl1.Controls.Add(this.chkIsAuto);
            this.panelControl1.Controls.Add(this.lookUpTemplate);
            this.panelControl1.Controls.Add(this.dateSend);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.dateReminder);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.spinNhacTruoc);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(698, 426);
            this.panelControl1.TabIndex = 0;
            // 
            // htmlContent
            // 
            this.htmlContent.InnerHtml = null;
            this.htmlContent.InnerText = null;
            this.htmlContent.Location = new System.Drawing.Point(18, 119);
            this.htmlContent.Name = "htmlContent";
            this.htmlContent.ReadOnly = true;
            this.htmlContent.Size = new System.Drawing.Size(663, 292);
            this.htmlContent.TabIndex = 26;
            // 
            // lookUpMail
            // 
            this.lookUpMail.Location = new System.Drawing.Point(338, 29);
            this.lookUpMail.Name = "lookUpMail";
            this.lookUpMail.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpMail.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Email", "Email")});
            this.lookUpMail.Properties.DisplayMember = "Email";
            this.lookUpMail.Properties.NullText = "";
            this.lookUpMail.Properties.ShowHeader = false;
            this.lookUpMail.Properties.ValueMember = "ID";
            this.lookUpMail.Size = new System.Drawing.Size(343, 20);
            this.lookUpMail.TabIndex = 25;
            // 
            // chkIsAuto
            // 
            this.chkIsAuto.Location = new System.Drawing.Point(337, 74);
            this.chkIsAuto.Name = "chkIsAuto";
            this.chkIsAuto.Properties.Caption = "Tự động gửi mail theo thời gian cài đặt ở trên";
            this.chkIsAuto.Size = new System.Drawing.Size(315, 19);
            this.chkIsAuto.TabIndex = 4;
            // 
            // lookUpTemplate
            // 
            this.lookUpTemplate.Location = new System.Drawing.Point(18, 74);
            this.lookUpTemplate.Name = "lookUpTemplate";
            this.lookUpTemplate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpTemplate.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TempName", "Tên mẫu"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CateName", "Loại mẫu"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Contents", "Name4", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lookUpTemplate.Properties.DisplayMember = "TempName";
            this.lookUpTemplate.Properties.NullText = "";
            this.lookUpTemplate.Properties.ValueMember = "TempID";
            this.lookUpTemplate.Size = new System.Drawing.Size(313, 20);
            this.lookUpTemplate.TabIndex = 3;
            this.lookUpTemplate.EditValueChanged += new System.EventHandler(this.lookUpTemplate_EditValueChanged);
            // 
            // dateSend
            // 
            this.dateSend.EditValue = null;
            this.dateSend.Location = new System.Drawing.Point(236, 29);
            this.dateSend.Name = "dateSend";
            this.dateSend.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.dateSend.Properties.DisplayFormat.FormatString = "{0:hh:mm:ss tt}";
            this.dateSend.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateSend.Properties.EditFormat.FormatString = "{0:hh:mm:ss tt}";
            this.dateSend.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateSend.Properties.Mask.EditMask = "hh:mm:ss tt";
            this.dateSend.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateSend.Size = new System.Drawing.Size(95, 20);
            toolTipTitleItem1.Text = "Thời gian gửi";
            toolTipItem1.Appearance.Image = global::BEE.QuangCao.Properties.Resources.Alarm_Clock;
            toolTipItem1.Appearance.Options.UseImage = true;
            toolTipItem1.Image = global::BEE.QuangCao.Properties.Resources.Alarm_Clock;
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Nhập theo định dạng hh:mm:ss tt (Giờ:Phút:Giây SA/CH)";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.dateSend.SuperTip = superToolTip1;
            this.dateSend.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(235, 10);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(84, 13);
            this.labelControl1.TabIndex = 24;
            this.labelControl1.Text = "Thời gian gửi SMS";
            this.labelControl1.ToolTip = "Thời gian gửi thông báo nhắc nợ";
            this.labelControl1.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.labelControl1.ToolTipTitle = "Thông tin";
            // 
            // dateReminder
            // 
            this.dateReminder.EditValue = null;
            this.dateReminder.Location = new System.Drawing.Point(135, 29);
            this.dateReminder.Name = "dateReminder";
            this.dateReminder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.dateReminder.Properties.DisplayFormat.FormatString = "{0:hh:mm:ss tt}";
            this.dateReminder.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateReminder.Properties.EditFormat.FormatString = "{0:hh:mm:ss tt}";
            this.dateReminder.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateReminder.Properties.Mask.EditMask = "hh:mm:ss tt";
            this.dateReminder.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateReminder.Size = new System.Drawing.Size(95, 20);
            toolTipTitleItem2.Text = "Thời gian nhắc";
            toolTipItem2.Appearance.Image = global::BEE.QuangCao.Properties.Resources.Alarm_Clock;
            toolTipItem2.Appearance.Options.UseImage = true;
            toolTipItem2.Image = global::BEE.QuangCao.Properties.Resources.Alarm_Clock;
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Nhập theo định dạng hh:mm:ss tt (Giờ:Phút:Giây SA/CH)";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.dateReminder.SuperTip = superToolTip2;
            this.dateReminder.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(134, 10);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(69, 13);
            this.labelControl4.TabIndex = 24;
            this.labelControl4.Text = "Thời gian nhắc";
            this.labelControl4.ToolTip = "Thời gian gửi thông báo nhắc nợ";
            this.labelControl4.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.labelControl4.ToolTipTitle = "Thông tin";
            // 
            // spinNhacTruoc
            // 
            this.spinNhacTruoc.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinNhacTruoc.Location = new System.Drawing.Point(18, 29);
            this.spinNhacTruoc.Name = "spinNhacTruoc";
            this.spinNhacTruoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinNhacTruoc.Properties.DisplayFormat.FormatString = "{0:n0} ngày";
            this.spinNhacTruoc.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinNhacTruoc.Properties.EditFormat.FormatString = "{0:n0} ngày";
            this.spinNhacTruoc.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinNhacTruoc.Properties.MaxValue = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.spinNhacTruoc.Size = new System.Drawing.Size(111, 20);
            this.spinNhacTruoc.TabIndex = 0;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(18, 100);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(68, 13);
            this.labelControl5.TabIndex = 22;
            this.labelControl5.Text = "Nội dung nhắc";
            this.labelControl5.ToolTip = "Gửi thông báo nhắc nợ trước mấy ngày?";
            this.labelControl5.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.labelControl5.ToolTipTitle = "Thông tin";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(338, 10);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(59, 13);
            this.labelControl6.TabIndex = 22;
            this.labelControl6.Text = "Email gửi (*)";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(18, 55);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(78, 13);
            this.labelControl3.TabIndex = 22;
            this.labelControl3.Text = "Mẫu nhắc nợ (*)";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(18, 10);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(53, 13);
            this.labelControl2.TabIndex = 22;
            this.labelControl2.Text = "Nhắc trước";
            this.labelControl2.ToolTip = "Gửi thông báo nhắc nợ trước mấy ngày?";
            this.labelControl2.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.labelControl2.ToolTipTitle = "Thông tin";
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.ImageIndex = 7;
            this.btnHuy.ImageList = this.imageCollection1;
            this.btnHuy.Location = new System.Drawing.Point(632, 444);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(78, 23);
            this.btnHuy.TabIndex = 6;
            this.btnHuy.Text = "Hủy - ESC";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnDongY
            // 
            this.btnDongY.ImageIndex = 8;
            this.btnDongY.ImageList = this.imageCollection1;
            this.btnDongY.Location = new System.Drawing.Point(533, 444);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(92, 23);
            this.btnDongY.TabIndex = 5;
            this.btnDongY.Text = "Lưu && Đóng";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
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
            this.imageCollection1.Images.SetKeyName(20, "quatang.png");
            this.imageCollection1.Images.SetKeyName(21, "email2.png");
            this.imageCollection1.Images.SetKeyName(22, "mesager.png");
            this.imageCollection1.Images.SetKeyName(23, "HELP.png");
            this.imageCollection1.Images.SetKeyName(24, "warring.png");
            // 
            // frmConfigReminder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 479);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnDongY);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfigReminder";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cấu hình nhắc nợ tự động (Mail)";
            this.Load += new System.EventHandler(this.frmConfigReminder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpMail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsAuto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpTemplate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSend.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSend.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReminder.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReminder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNhacTruoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SpinEdit spinNhacTruoc;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dateReminder;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.DateEdit dateSend;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookUpTemplate;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit chkIsAuto;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private DevExpress.XtraEditors.LookUpEdit lookUpMail;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private HtmlEditorControl htmlContent;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}