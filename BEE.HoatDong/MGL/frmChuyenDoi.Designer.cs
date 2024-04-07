namespace BEE.HoatDong.MGL
{
    partial class frmChuyenDoi
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.Accept = new DevExpress.XtraEditors.SimpleButton();
            this.lookTrangThai = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lookTrangthaiHT = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtTieuDe = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.lookPhuongThuc = new DevExpress.XtraEditors.LookUpEdit();
            this.dateNgayXL = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtLyDo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangThai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangthaiHT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTieuDe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookPhuongThuc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayXL.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayXL.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLyDo
            // 
            this.txtLyDo.Location = new System.Drawing.Point(152, 167);
            this.txtLyDo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.Size = new System.Drawing.Size(506, 198);
            this.txtLyDo.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(36, 169);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(35, 16);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Lý do:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(571, 373);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Bỏ qua";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Accept
            // 
            this.Accept.Location = new System.Drawing.Point(476, 373);
            this.Accept.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(87, 28);
            this.Accept.TabIndex = 2;
            this.Accept.Text = "Đồng ý";
            this.Accept.Click += new System.EventHandler(this.Accept_Click);
            // 
            // lookTrangThai
            // 
            this.lookTrangThai.Location = new System.Drawing.Point(152, 75);
            this.lookTrangThai.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lookTrangThai.Name = "lookTrangThai";
            this.lookTrangThai.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookTrangThai.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTT", "TenTT")});
            this.lookTrangThai.Properties.DisplayMember = "TenTT";
            this.lookTrangThai.Properties.NullText = "";
            this.lookTrangThai.Properties.ShowHeader = false;
            this.lookTrangThai.Properties.ValueMember = "MaLoai";
            this.lookTrangThai.Size = new System.Drawing.Size(506, 22);
            this.lookTrangThai.TabIndex = 13;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(36, 78);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(62, 17);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = "Trạng thái";
            // 
            // lookTrangthaiHT
            // 
            this.lookTrangthaiHT.Enabled = false;
            this.lookTrangthaiHT.Location = new System.Drawing.Point(152, 43);
            this.lookTrangthaiHT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lookTrangthaiHT.Name = "lookTrangthaiHT";
            this.lookTrangthaiHT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookTrangthaiHT.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTT", "TenTT")});
            this.lookTrangthaiHT.Properties.DisplayMember = "TenTT";
            this.lookTrangthaiHT.Properties.NullText = "";
            this.lookTrangthaiHT.Properties.ShowHeader = false;
            this.lookTrangthaiHT.Properties.ValueMember = "MaLoai";
            this.lookTrangthaiHT.Size = new System.Drawing.Size(506, 22);
            this.lookTrangthaiHT.TabIndex = 11;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(36, 46);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(109, 17);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "Trạng thái hiện tại";
            // 
            // txtTieuDe
            // 
            this.txtTieuDe.Location = new System.Drawing.Point(152, 13);
            this.txtTieuDe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.Size = new System.Drawing.Size(506, 22);
            this.txtTieuDe.TabIndex = 15;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(36, 16);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(49, 17);
            this.labelControl8.TabIndex = 14;
            this.labelControl8.Text = "Tiêu đề:";
            // 
            // lookPhuongThuc
            // 
            this.lookPhuongThuc.Location = new System.Drawing.Point(152, 105);
            this.lookPhuongThuc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lookPhuongThuc.Name = "lookPhuongThuc";
            this.lookPhuongThuc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookPhuongThuc.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenPT", "Name3")});
            this.lookPhuongThuc.Properties.DisplayMember = "TenPT";
            this.lookPhuongThuc.Properties.NullText = "";
            this.lookPhuongThuc.Properties.ShowHeader = false;
            this.lookPhuongThuc.Properties.ValueMember = "MaPT";
            this.lookPhuongThuc.Size = new System.Drawing.Size(506, 22);
            this.lookPhuongThuc.TabIndex = 19;
            // 
            // dateNgayXL
            // 
            this.dateNgayXL.EditValue = null;
            this.dateNgayXL.Enabled = false;
            this.dateNgayXL.Location = new System.Drawing.Point(152, 137);
            this.dateNgayXL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateNgayXL.Name = "dateNgayXL";
            this.dateNgayXL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayXL.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateNgayXL.Properties.DisplayFormat.FormatString = "{0:dd/MM/yyy HH:mm:ss}";
            this.dateNgayXL.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateNgayXL.Properties.EditFormat.FormatString = "{0:dd/MM/yyy HH-mm-ss}";
            this.dateNgayXL.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateNgayXL.Properties.Mask.EditMask = "dd/MM/yyy HH:mm:ss";
            this.dateNgayXL.Size = new System.Drawing.Size(506, 22);
            this.dateNgayXL.TabIndex = 18;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(36, 140);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 17);
            this.labelControl3.TabIndex = 16;
            this.labelControl3.Text = "Ngày xử lý:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(36, 108);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(82, 17);
            this.labelControl5.TabIndex = 17;
            this.labelControl5.Text = "Phương thức";
            // 
            // frmChuyenDoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 429);
            this.Controls.Add(this.lookPhuongThuc);
            this.Controls.Add(this.dateNgayXL);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtTieuDe);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.lookTrangThai);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.lookTrangthaiHT);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.Accept);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtLyDo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmChuyenDoi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chuyển trạng thái";
            this.Load += new System.EventHandler(this.frmDuyet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtLyDo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangThai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangthaiHT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTieuDe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookPhuongThuc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayXL.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayXL.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit txtLyDo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton Accept;
        private DevExpress.XtraEditors.LookUpEdit lookTrangThai;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit lookTrangthaiHT;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtTieuDe;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LookUpEdit lookPhuongThuc;
        private DevExpress.XtraEditors.DateEdit dateNgayXL;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}