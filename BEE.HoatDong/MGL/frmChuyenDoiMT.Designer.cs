namespace BEE.HoatDong.MGL
{
    partial class frmChuyenDoiMT
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
            this.txtLyDo = new DevExpress.XtraEditors.MemoEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.Accept = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lookTrangThai = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lookTrangthaiHT = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.lookPhuongThuc = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dateNgayXL = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtTieuDe = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtLyDo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangThai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangthaiHT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookPhuongThuc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayXL.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayXL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTieuDe.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLyDo
            // 
            this.txtLyDo.Location = new System.Drawing.Point(113, 141);
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.Size = new System.Drawing.Size(434, 161);
            this.txtLyDo.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(504, 340);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Bỏ qua";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Accept
            // 
            this.Accept.Location = new System.Drawing.Point(423, 340);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(75, 23);
            this.Accept.TabIndex = 2;
            this.Accept.Text = "Đồng ý";
            this.Accept.Click += new System.EventHandler(this.Accept_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lookTrangThai);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.lookTrangthaiHT);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.txtLyDo);
            this.panelControl1.Controls.Add(this.lookPhuongThuc);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.dateNgayXL);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.txtTieuDe);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Location = new System.Drawing.Point(10, 11);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(568, 323);
            this.panelControl1.TabIndex = 7;
            // 
            // lookTrangThai
            // 
            this.lookTrangThai.Location = new System.Drawing.Point(113, 64);
            this.lookTrangThai.Name = "lookTrangThai";
            this.lookTrangThai.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookTrangThai.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTT", "TenTT")});
            this.lookTrangThai.Properties.DisplayMember = "TenTT";
            this.lookTrangThai.Properties.NullText = "";
            this.lookTrangThai.Properties.ShowHeader = false;
            this.lookTrangThai.Properties.ValueMember = "MaLoai";
            this.lookTrangThai.Size = new System.Drawing.Size(434, 20);
            this.lookTrangThai.TabIndex = 9;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(14, 67);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(49, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Trạng thái";
            // 
            // lookTrangthaiHT
            // 
            this.lookTrangthaiHT.Enabled = false;
            this.lookTrangthaiHT.Location = new System.Drawing.Point(113, 38);
            this.lookTrangthaiHT.Name = "lookTrangthaiHT";
            this.lookTrangthaiHT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookTrangthaiHT.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTT", "TenTT")});
            this.lookTrangthaiHT.Properties.DisplayMember = "TenTT";
            this.lookTrangthaiHT.Properties.NullText = "";
            this.lookTrangthaiHT.Properties.ShowHeader = false;
            this.lookTrangthaiHT.Properties.ValueMember = "MaLoai";
            this.lookTrangthaiHT.Size = new System.Drawing.Size(434, 20);
            this.lookTrangthaiHT.TabIndex = 7;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(14, 41);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(87, 13);
            this.labelControl6.TabIndex = 6;
            this.labelControl6.Text = "Trạng thái hiện tại";
            // 
            // lookPhuongThuc
            // 
            this.lookPhuongThuc.Location = new System.Drawing.Point(113, 90);
            this.lookPhuongThuc.Name = "lookPhuongThuc";
            this.lookPhuongThuc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookPhuongThuc.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenPT", "Name3")});
            this.lookPhuongThuc.Properties.DisplayMember = "TenPT";
            this.lookPhuongThuc.Properties.NullText = "";
            this.lookPhuongThuc.Properties.ShowHeader = false;
            this.lookPhuongThuc.Properties.ValueMember = "MaPT";
            this.lookPhuongThuc.Size = new System.Drawing.Size(434, 20);
            this.lookPhuongThuc.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(14, 145);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(46, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Nội dung:";
            // 
            // dateNgayXL
            // 
            this.dateNgayXL.EditValue = null;
            this.dateNgayXL.Enabled = false;
            this.dateNgayXL.Location = new System.Drawing.Point(113, 116);
            this.dateNgayXL.Name = "dateNgayXL";
            this.dateNgayXL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayXL.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateNgayXL.Properties.DisplayFormat.FormatString = "{0:dd/MM/yyy HH-mm-ss}";
            this.dateNgayXL.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateNgayXL.Properties.EditFormat.FormatString = "{0:dd/MM/yyy HH-mm-ss}";
            this.dateNgayXL.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateNgayXL.Properties.Mask.EditMask = "dd/MM/yyy HH-mm-ss";
            this.dateNgayXL.Size = new System.Drawing.Size(434, 20);
            this.dateNgayXL.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(14, 119);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(56, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Ngày xử lý:";
            // 
            // txtTieuDe
            // 
            this.txtTieuDe.Location = new System.Drawing.Point(113, 12);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.Size = new System.Drawing.Size(434, 20);
            this.txtTieuDe.TabIndex = 1;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(14, 93);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(62, 13);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Phương thức";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(14, 15);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(39, 13);
            this.labelControl8.TabIndex = 0;
            this.labelControl8.Text = "Tiêu đề:";
            // 
            // frmChuyenDoiMT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 377);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.Accept);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmChuyenDoiMT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chuyển trạng thái";
            this.Load += new System.EventHandler(this.frmDuyet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtLyDo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangThai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangthaiHT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookPhuongThuc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayXL.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayXL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTieuDe.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit txtLyDo;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton Accept;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookTrangThai;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit lookTrangthaiHT;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LookUpEdit lookPhuongThuc;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dateNgayXL;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtTieuDe;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl8;
    }
}