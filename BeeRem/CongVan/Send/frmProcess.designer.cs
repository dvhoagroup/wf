namespace LandSoft.CongVan.Send
{
    partial class frmProcess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProcess));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.ctlTrinhTrangCV1 = new LandSoft.Library.ctlTrinhTrangCV();
            this.btnFileAttach = new DevExpress.XtraEditors.ButtonEdit();
            this.txtNoiDung = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.spinTienDo = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.dateThoiHan = new DevExpress.XtraEditors.DateEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlTrinhTrangCV1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFileAttach.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoiDung.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTienDo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateThoiHan.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateThoiHan.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.dateThoiHan);
            this.panelControl1.Controls.Add(this.ctlTrinhTrangCV1);
            this.panelControl1.Controls.Add(this.btnFileAttach);
            this.panelControl1.Controls.Add(this.memoEdit1);
            this.panelControl1.Controls.Add(this.txtNoiDung);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.spinTienDo);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(590, 308);
            this.panelControl1.TabIndex = 0;
            // 
            // ctlTrinhTrangCV1
            // 
            this.ctlTrinhTrangCV1.Location = new System.Drawing.Point(328, 12);
            this.ctlTrinhTrangCV1.Name = "ctlTrinhTrangCV1";
            this.ctlTrinhTrangCV1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("ctlTrinhTrangCV1.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.ctlTrinhTrangCV1.Properties.DisplayMember = "TenTT";
            this.ctlTrinhTrangCV1.Properties.NullText = "";
            this.ctlTrinhTrangCV1.Properties.ValueMember = "MaTT";
            this.ctlTrinhTrangCV1.Size = new System.Drawing.Size(251, 20);
            this.ctlTrinhTrangCV1.TabIndex = 21;
            // 
            // btnFileAttach
            // 
            this.btnFileAttach.Location = new System.Drawing.Point(328, 38);
            this.btnFileAttach.Name = "btnFileAttach";
            this.btnFileAttach.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Chọn file", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.btnFileAttach.Size = new System.Drawing.Size(251, 20);
            this.btnFileAttach.TabIndex = 20;
            this.btnFileAttach.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnFileAttach_ButtonClick);
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.EditValue = "";
            this.txtNoiDung.Location = new System.Drawing.Point(12, 86);
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Properties.NullText = "[Nội dung công văn]";
            this.txtNoiDung.Size = new System.Drawing.Size(226, 210);
            this.txtNoiDung.TabIndex = 19;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(244, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(53, 13);
            this.labelControl2.TabIndex = 17;
            this.labelControl2.Text = "Trạng thái:";
            // 
            // spinTienDo
            // 
            this.spinTienDo.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinTienDo.Location = new System.Drawing.Point(90, 12);
            this.spinTienDo.Name = "spinTienDo";
            this.spinTienDo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinTienDo.Properties.DisplayFormat.FormatString = "{0:n0} %";
            this.spinTienDo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTienDo.Properties.EditFormat.FormatString = "{0:n0} %";
            this.spinTienDo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTienDo.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spinTienDo.Size = new System.Drawing.Size(114, 20);
            this.spinTienDo.TabIndex = 16;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(244, 41);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(65, 13);
            this.labelControl1.TabIndex = 15;
            this.labelControl1.Text = "File đính kèm:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 15);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(39, 13);
            this.labelControl4.TabIndex = 15;
            this.labelControl4.Text = "Tiến độ:";
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::LandSoft.Properties.Resources.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(523, 326);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(79, 23);
            this.btnHuy.TabIndex = 7;
            this.btnHuy.Text = "Hủy - ESC";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnDongY
            // 
            this.btnDongY.Image = global::LandSoft.Properties.Resources.Luu;
            this.btnDongY.Location = new System.Drawing.Point(423, 326);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(94, 23);
            this.btnDongY.TabIndex = 6;
            this.btnDongY.Text = "Lưu && Đóng";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // memoEdit1
            // 
            this.memoEdit1.EditValue = "";
            this.memoEdit1.Location = new System.Drawing.Point(244, 86);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.NullText = "[Nội dung công văn]";
            this.memoEdit1.Size = new System.Drawing.Size(335, 210);
            this.memoEdit1.TabIndex = 19;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Location = new System.Drawing.Point(12, 67);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 13);
            this.labelControl3.TabIndex = 15;
            this.labelControl3.Text = "Nơi nhận";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Location = new System.Drawing.Point(244, 67);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(52, 13);
            this.labelControl5.TabIndex = 15;
            this.labelControl5.Text = "Trích yếu";
            // 
            // dateThoiHan
            // 
            this.dateThoiHan.EditValue = null;
            this.dateThoiHan.Location = new System.Drawing.Point(90, 38);
            this.dateThoiHan.Name = "dateThoiHan";
            this.dateThoiHan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateThoiHan.Properties.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateThoiHan.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateThoiHan.Properties.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateThoiHan.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateThoiHan.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateThoiHan.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateThoiHan.Size = new System.Drawing.Size(114, 20);
            this.dateThoiHan.TabIndex = 8;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(12, 41);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(45, 13);
            this.labelControl6.TabIndex = 15;
            this.labelControl6.Text = "Thời hạn:";
            // 
            // frmProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 359);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnDongY);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProcess";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xử lý công văn";
            this.Load += new System.EventHandler(this.frmProcess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlTrinhTrangCV1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFileAttach.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoiDung.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTienDo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateThoiHan.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateThoiHan.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SpinEdit spinTienDo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.MemoEdit txtNoiDung;
        private DevExpress.XtraEditors.ButtonEdit btnFileAttach;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private Library.ctlTrinhTrangCV ctlTrinhTrangCV1;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit dateThoiHan;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}