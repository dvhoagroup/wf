namespace BEEREMA
{
    partial class View_ctl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainerReport = new DevExpress.XtraEditors.SplitContainerControl();
            this.pnCustomerBy = new DevExpress.XtraEditors.PanelControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.ppCustomerBy = new DevExpress.XtraBars.PopupMenu(this.components);
            this.itemBCDoTuoi = new DevExpress.XtraBars.BarButtonItem();
            this.itemBCNhom = new DevExpress.XtraBars.BarButtonItem();
            this.itemBCKhuVuc = new DevExpress.XtraBars.BarButtonItem();
            this.itemBCNguonDen = new DevExpress.XtraBars.BarButtonItem();
            this.itemBCMucDich = new DevExpress.XtraBars.BarButtonItem();
            this.itemBCCapDo = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.itemDefault = new DevExpress.XtraBars.BarButtonItem();
            this.itemChoice = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.hyperLinkEdit1 = new DevExpress.XtraEditors.HyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerReport)).BeginInit();
            this.splitContainerReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnCustomerBy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppCustomerBy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerReport
            // 
            this.splitContainerReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerReport.Location = new System.Drawing.Point(0, 0);
            this.splitContainerReport.Name = "splitContainerReport";
            this.splitContainerReport.Panel1.Controls.Add(this.pnCustomerBy);
            this.splitContainerReport.Panel1.Text = "Panel1";
            this.splitContainerReport.Panel2.Text = "Panel2";
            this.splitContainerReport.Size = new System.Drawing.Size(992, 600);
            this.splitContainerReport.SplitterPosition = 395;
            this.splitContainerReport.TabIndex = 1;
            this.splitContainerReport.Text = "splitContainerControl1";
            // 
            // pnCustomerBy
            // 
            this.pnCustomerBy.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnCustomerBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnCustomerBy.Location = new System.Drawing.Point(0, 0);
            this.pnCustomerBy.Name = "pnCustomerBy";
            this.barManager1.SetPopupContextMenu(this.pnCustomerBy, this.ppCustomerBy);
            this.pnCustomerBy.Size = new System.Drawing.Size(395, 600);
            this.pnCustomerBy.TabIndex = 0;
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.itemDefault,
            this.itemChoice,
            this.barButtonItem1,
            this.itemBCDoTuoi,
            this.itemBCNhom,
            this.itemBCKhuVuc,
            this.itemBCNguonDen,
            this.itemBCMucDich,
            this.itemBCCapDo});
            this.barManager1.MaxItemId = 9;
            // 
            // ppCustomerBy
            // 
            this.ppCustomerBy.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemBCDoTuoi),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemBCNhom),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemBCKhuVuc),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemBCNguonDen),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemBCMucDich),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemBCCapDo)});
            this.ppCustomerBy.Manager = this.barManager1;
            this.ppCustomerBy.MenuCaption = "Tùy chọn";
            this.ppCustomerBy.Name = "ppCustomerBy";
            // 
            // itemBCDoTuoi
            // 
            this.itemBCDoTuoi.Caption = "Báo cáo theo độ tuổi";
            this.itemBCDoTuoi.Id = 3;
            this.itemBCDoTuoi.Name = "itemBCDoTuoi";
            this.itemBCDoTuoi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemBCDoTuoi_ItemClick);
            // 
            // itemBCNhom
            // 
            this.itemBCNhom.Caption = "Báo cáo theo nhóm";
            this.itemBCNhom.Id = 4;
            this.itemBCNhom.Name = "itemBCNhom";
            this.itemBCNhom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemBCNhom_ItemClick);
            // 
            // itemBCKhuVuc
            // 
            this.itemBCKhuVuc.Caption = "Báo cáo theo khu vực";
            this.itemBCKhuVuc.Id = 5;
            this.itemBCKhuVuc.Name = "itemBCKhuVuc";
            this.itemBCKhuVuc.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemBCKhuVuc_ItemClick);
            // 
            // itemBCNguonDen
            // 
            this.itemBCNguonDen.Caption = "Báo cáo theo nguồn đến";
            this.itemBCNguonDen.Id = 6;
            this.itemBCNguonDen.Name = "itemBCNguonDen";
            this.itemBCNguonDen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemBCNguonDen_ItemClick);
            // 
            // itemBCMucDich
            // 
            this.itemBCMucDich.Caption = "Báo cáo theo mục đích";
            this.itemBCMucDich.Id = 7;
            this.itemBCMucDich.Name = "itemBCMucDich";
            this.itemBCMucDich.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemBCMucDich_ItemClick);
            // 
            // itemBCCapDo
            // 
            this.itemBCCapDo.Caption = "Báo cáo theo cấp độ";
            this.itemBCCapDo.Id = 8;
            this.itemBCCapDo.Name = "itemBCCapDo";
            this.itemBCCapDo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemBCCapDo_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(992, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 600);
            this.barDockControlBottom.Size = new System.Drawing.Size(992, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 600);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(992, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 600);
            // 
            // itemDefault
            // 
            this.itemDefault.Caption = "Hình nền mặc định";
            this.itemDefault.Id = 0;
            this.itemDefault.Name = "itemDefault";
            this.itemDefault.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemDefault_ItemClick);
            // 
            // itemChoice
            // 
            this.itemChoice.Caption = "Chọn hình nền";
            this.itemChoice.Id = 1;
            this.itemChoice.Name = "itemChoice";
            this.itemChoice.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemChoice_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 2;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemDefault),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemChoice, true)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.MenuCaption = "Tùy chọn";
            this.popupMenu1.Name = "popupMenu1";
            this.popupMenu1.ShowCaption = true;
            // 
            // toolTipController1
            // 
            this.toolTipController1.AllowHtmlText = true;
            this.toolTipController1.AutoPopDelay = 60000;
            this.toolTipController1.InitialDelay = 10;
            this.toolTipController1.Rounded = true;
            this.toolTipController1.RoundRadius = 4;
            // 
            // hyperLinkEdit1
            // 
            this.hyperLinkEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hyperLinkEdit1.EditValue = "http://beesky.vn";
            this.hyperLinkEdit1.Location = new System.Drawing.Point(0, 0);
            this.hyperLinkEdit1.Name = "hyperLinkEdit1";
            this.hyperLinkEdit1.Properties.AllowFocused = false;
            this.hyperLinkEdit1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(252)))), ((int)(((byte)(250)))));
            this.hyperLinkEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.hyperLinkEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hyperLinkEdit1.Properties.HideSelection = false;
            this.hyperLinkEdit1.Properties.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.hyperLinkEdit1.Properties.ReadOnly = true;
            this.hyperLinkEdit1.Size = new System.Drawing.Size(585, 18);
            this.hyperLinkEdit1.TabIndex = 1;
            this.hyperLinkEdit1.Visible = false;
            // 
            // View_ctl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerReport);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "View_ctl";
            this.Size = new System.Drawing.Size(992, 600);
            this.Tag = "Main";
            this.Load += new System.EventHandler(this.View_ctl_Load);
            this.SizeChanged += new System.EventHandler(this.View_ctl_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerReport)).EndInit();
            this.splitContainerReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnCustomerBy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppCustomerBy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem itemDefault;
        private DevExpress.XtraBars.BarButtonItem itemChoice;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerReport;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem itemBCDoTuoi;
        private DevExpress.XtraBars.BarButtonItem itemBCNhom;
        private DevExpress.XtraBars.BarButtonItem itemBCKhuVuc;
        private DevExpress.XtraBars.BarButtonItem itemBCNguonDen;
        private DevExpress.XtraBars.BarButtonItem itemBCMucDich;
        private DevExpress.XtraBars.BarButtonItem itemBCCapDo;
        private DevExpress.XtraBars.PopupMenu ppCustomerBy;
        private DevExpress.XtraEditors.PanelControl pnCustomerBy;
        private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEdit1;
    }
}
