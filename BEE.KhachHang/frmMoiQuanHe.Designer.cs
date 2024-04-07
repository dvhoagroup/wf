namespace BEE.KhachHang
{
    partial class frmMoiQuanHe
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
            this.gcMoiQuanHe = new DevExpress.XtraGrid.GridControl();
            this.grvMoiQuanHe = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.spinStt = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtMoiQuanHe = new DevExpress.XtraEditors.TextEdit();
            this.txtDoiUng = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcMoiQuanHe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMoiQuanHe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinStt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMoiQuanHe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDoiUng.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMoiQuanHe
            // 
            this.gcMoiQuanHe.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcMoiQuanHe.Location = new System.Drawing.Point(15, 16);
            this.gcMoiQuanHe.MainView = this.grvMoiQuanHe;
            this.gcMoiQuanHe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcMoiQuanHe.Name = "gcMoiQuanHe";
            this.gcMoiQuanHe.Size = new System.Drawing.Size(682, 309);
            this.gcMoiQuanHe.TabIndex = 0;
            this.gcMoiQuanHe.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvMoiQuanHe});
            // 
            // grvMoiQuanHe
            // 
            this.grvMoiQuanHe.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn5,
            this.gridColumn3,
            this.gridColumn4});
            this.grvMoiQuanHe.DetailHeight = 431;
            this.grvMoiQuanHe.GridControl = this.gcMoiQuanHe;
            this.grvMoiQuanHe.Name = "grvMoiQuanHe";
            this.grvMoiQuanHe.OptionsView.ColumnAutoWidth = false;
            this.grvMoiQuanHe.OptionsView.ShowAutoFilterRow = true;
            this.grvMoiQuanHe.OptionsView.ShowGroupPanel = false;
            this.grvMoiQuanHe.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvMoiQuanHe_FocusedRowChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "STT";
            this.gridColumn1.FieldName = "STT";
            this.gridColumn1.MinWidth = 23;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 62;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Mối quan hệ";
            this.gridColumn2.FieldName = "TenQH";
            this.gridColumn2.MinWidth = 23;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 196;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Mối liên hệ đối ứng";
            this.gridColumn5.FieldName = "DoiUng";
            this.gridColumn5.MinWidth = 25;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 143;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Nhân viên tạo";
            this.gridColumn3.FieldName = "HoTen";
            this.gridColumn3.MinWidth = 25;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 110;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Ngày tạo";
            this.gridColumn4.FieldName = "CreateDate";
            this.gridColumn4.MinWidth = 25;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 114;
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(516, 404);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(87, 28);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(610, 405);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(87, 28);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(421, 404);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(87, 28);
            this.btnXoa.TabIndex = 3;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 348);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 16);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "STT";
            // 
            // spinStt
            // 
            this.spinStt.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinStt.Location = new System.Drawing.Point(59, 344);
            this.spinStt.Name = "spinStt";
            this.spinStt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinStt.Size = new System.Drawing.Size(98, 24);
            this.spinStt.TabIndex = 9;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(176, 348);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(74, 17);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "Mối quan hệ";
            // 
            // txtMoiQuanHe
            // 
            this.txtMoiQuanHe.Location = new System.Drawing.Point(268, 345);
            this.txtMoiQuanHe.Name = "txtMoiQuanHe";
            this.txtMoiQuanHe.Size = new System.Drawing.Size(147, 22);
            this.txtMoiQuanHe.TabIndex = 11;
            // 
            // txtDoiUng
            // 
            this.txtDoiUng.Location = new System.Drawing.Point(556, 345);
            this.txtDoiUng.Name = "txtDoiUng";
            this.txtDoiUng.Size = new System.Drawing.Size(141, 22);
            this.txtDoiUng.TabIndex = 13;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(437, 348);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(113, 17);
            this.labelControl3.TabIndex = 12;
            this.labelControl3.Text = "Mối liên hệ đối ứng";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(283, 404);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(132, 28);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "Thêm mối quan hệ";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // frmMoiQuanHe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 459);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtDoiUng);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtMoiQuanHe);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.spinStt);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.gcMoiQuanHe);
            this.IconOptions.ShowIcon = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMoiQuanHe";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mối quan hệ";
            this.Load += new System.EventHandler(this.frmMoiQuanHe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcMoiQuanHe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMoiQuanHe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinStt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMoiQuanHe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDoiUng.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMoiQuanHe;
        private DevExpress.XtraGrid.Views.Grid.GridView grvMoiQuanHe;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SpinEdit spinStt;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtMoiQuanHe;
        private DevExpress.XtraEditors.TextEdit txtDoiUng;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
    }
}