using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using BEE.ThuVien;
using BEE.LichLamViec;
using System.Linq;
using DevExpress.LookAndFeel;
using BEE.NgonNgu;
using System.Runtime.InteropServices;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraGrid.Views.Grid;
using BEE.KhachHang;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Owin;
using Microsoft.AspNet.SignalR.Client;
using System.Threading;

namespace BEEREMA
{
    public partial class frmMainNew : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);
        bool isCapsLock = false, isNumLock = false, isInsert = false;
        int DemLH = 0, DemNV = 0;
        int formID;
        int linkID;
        int x = 50;
        int y = 50;
        public string SDT { get; set; }
        public string Unique { get; set; }
        public string MayLe { get; set; }
        public string HoTen { get; set; }

        MasterDataContext db = new MasterDataContext();

        public frmMainNew()
        {
            InitializeComponent();
        }

        void LoadCapsLock()
        {
            try
            {
                isCapsLock = (((ushort)GetKeyState(0x14 /*VK_CAPITAL*/)) & 0xffff) != 0;
                lblCapsLock.Caption = isCapsLock ? "CAPS" : "        ";
            }
            catch { }
        }

        void LoadNumLock()
        {
            try
            {
                isNumLock = (((ushort)GetKeyState(0x90 /*VK_NUMLOCK*/)) & 0xffff) != 0;
                lblNumLock.Caption = isNumLock ? "NUM" : "        ";
            }
            catch { }
        }

        void LoadInsert()
        {
            try
            {
                isInsert = (((ushort)GetKeyState(0x2D /*VK_Insert*/)) & 0xffff) != 0;
                lblInsert.Caption = isInsert ? "  INS  " : "        ";
            }
            catch { }
        }

        void InitSkinGallery()
        {
            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(itemSkins, true);
            UserLookAndFeel.Default.SetSkinStyle("Foggy");
        }

        void User_UpdateStatus(byte maTT)
        {
            try
            {
                db.NhanViens.Single(p => p.MaNV == Properties.Settings.Default.StaffID).MaTT = maTT;
                db.SubmitChanges();
            }
            catch { }
        }

        void User_ReLogin()
        {
            //try
            //{
            //    foreach (var item in Application.OpenForms)
            //    {
            //        if (((System.Windows.Forms.Form)item).Name != this.Name)
            //        {
            //            ((System.Windows.Forms.Form)item).Hide(); ((System.Windows.Forms.Form)item).Close();
            //        }
            //    }
            //}
            //catch { }
            this.Hide();

            Properties.Settings.Default.StaffID = 0;
            HeThong.Login_frm frmLogin = new HeThong.Login_frm();
            frmLogin.ShowDialog();

            if (frmLogin.DialogResult == DialogResult.OK)
            {
                new frmMainNew().Show();
            }
            else
            {
                this.Close();
            }

        }

        void LoadPermission()
        {
            if (Common.PerID == 1)
            {
                groupSystem_Sercurity.Visible = true;
                itemHT_TK_PhanQuyen.Enabled = true;
                itemHT_TK_Module.Enabled = true;
                //  itemSkins.Enabled = true;
            }
            else
            {
                groupSystem_Sercurity.Visible = false;
                itemHT_TK_PhanQuyen.Enabled = false;
                itemHT_TK_Module.Enabled = false;
                // itemSkins.Enabled = false;

                var listAction = db.ActionDatas.Where(p => p.PerID == Common.PerID & p.FeatureID == 1)
                   .Select(p => p.FormID).ToList();
                var listAccess = db.AccessDatas.Where(p => p.PerID == Common.PerID & p.SDBID < 6)
                       .Select(p => p.FormID).ToList();
                var listAccessLock = db.AccessDatas.Where(p => p.PerID == Common.PerID & p.SDBID == 6)
                      .Select(p => p.FormID).ToList();

                #region Nhan vien
                itemHT_NV_List.Enabled = !listAccessLock.Contains(15);
                itemHT_NV_PhongBan.Enabled = !listAccessLock.Contains(16);
                itemHT_NV_ChucVu.Enabled = !listAccessLock.Contains(17);
                itemHT_NV_NhomKD.Enabled = !listAccessLock.Contains(18);
                itemHT_TK_Module.Enabled = !listAccessLock.Contains(61);
                itemHT_TK_PhanQuyen.Enabled = !listAccessLock.Contains(62);
                itemHT_NV_Add.Enabled = !listAccessLock.Contains(189);
                itemNV_NV_LS.Enabled = !listAccessLock.Contains(190);
                itemTuLock.Enabled = listAccess.Contains(208);
                #endregion

                #region Cong ty
                itemSystem_Company_Add.Enabled = !listAccessLock.Contains(141);
                itemSystem_Company_List.Enabled = !listAccessLock.Contains(142);
                #endregion

                #region Khach hang
                itemCRM_Dictionary_DanhXung.Enabled = !listAccessLock.Contains(10);
                itemCRM_Distionary_Career.Enabled = !listAccessLock.Contains(13);
                itemCRM_Dictionary_NhomKD.Enabled = !listAccessLock.Contains(12);
                itemCRM_Dictionary_Source.Enabled = !listAccessLock.Contains(184);

                itemCRM_Dictionary_LoaiHinhKD.Enabled = !listAccessLock.Contains(14);
                itemCRM_Dictionary_Reason.Enabled = !listAccessLock.Contains(184);
                itemCRM_Dictionary_Purpose.Enabled = !listAccessLock.Contains(185);
                itemKH_NC_CD.Enabled = !listAccessLock.Contains(186);
                itemCRM_Customer_List2.Enabled = !listAccessLock.Contains(9);
                #endregion

                #region Du an
                itemProduct_Project_Lisr.Enabled = !listAccessLock.Contains(1);
                itemProduct_Direction.Enabled = !listAccessLock.Contains(2);
                itemProduct_Dictionary_ProductCate.Enabled = !listAccessLock.Contains(187);
                itemProduct_Dictionary_Zone.Enabled = !listAccessLock.Contains(126);
                //itemProduct_Direction.Enabled = !listAccessLock.Contains(188);
                itemProduct_Dictionary_Subdivision.Enabled = !listAccessLock.Contains(127);
                itemProduct_Project_Add.Enabled = !listAccessLock.Contains(146);
                itemProduct_Dictionary_Status.Enabled = !listAccessLock.Contains(148);
                itemProduct_Dictionary_ProjectCate.Enabled = !listAccessLock.Contains(163);
                #endregion

                #region Tai lieu
                itemDocument_List.Enabled = !listAccessLock.Contains(99);
                itemDocument_Category.Enabled = !listAccessLock.Contains(100);
                itemDocument_Add.Enabled = !listAccessLock.Contains(191);
                #endregion

                #region Sản phẩm cần bán/cho thuê
                itemSP_SP_DS.Enabled = !listAccessLock.Contains(174);
                itemDSCanChoThue.Enabled = !listAccessLock.Contains(175);
                itemThuTuCanBan.Enabled = listAccess.Contains(204);
                itemPhanQuyenBC.Enabled = listAccess.Contains(206);
                itemChoXoaCB.Enabled = listAccess.Contains(20);
                itemDuyetXoaCCT.Enabled = listAccess.Contains(21);
                #endregion

                #region Sản phầm cần mua/cần thuê
                itemKH_NC_DS.Enabled = !listAccessLock.Contains(176);
                itemDSCanThue.Enabled = !listAccessLock.Contains(177);
                itemThuTuCanMT.Enabled = listAccess.Contains(205);
                itemPhanQuyenMT.Enabled = listAccess.Contains(207);
                itemDuyetXoaCM.Enabled = listAccess.Contains(22);
                itemDuyetXoaCT.Enabled = listAccess.Contains(23);
                #endregion

                #region Giao dịch đang xử lý
                itemCV_CV_DS.Enabled = !listAccessLock.Contains(178);
                #endregion

                #region Giao dịch đã chốt
                itemCV_GD_DS.Enabled = !listAccessLock.Contains(180);
                #endregion

                #region Lich lam viec
                itemLLV_NhiemVu_List.Enabled = !listAccessLock.Contains(66);
                itemLLV_LichHen_List.Enabled = !listAccessLock.Contains(67);
                itemLLV_LoaiNhiemVu.Enabled = !listAccessLock.Contains(68);
                itemLLV_TrangThaiNhiemVu.Enabled = !listAccessLock.Contains(69);
                itemLLV_MucDoNhiemVu.Enabled = !listAccessLock.Contains(70);
                itemLLV_TienDo.Enabled = !listAccessLock.Contains(71);
                itemLLV_LoaiLichHen.Enabled = !listAccessLock.Contains(72);
                itemLLV_ThoiDiemLichHen.Enabled = !listAccessLock.Contains(73);
                #endregion

                #region Email Marketting
                itemMail_Config.Enabled = !listAccessLock.Contains(95);
                itemNV_DM_CD.Enabled = !listAccessLock.Contains(183);
                itemSendMail.Enabled = !listAccessLock.Contains(196);
                itemDSSendMail.Enabled = !listAccessLock.Contains(197);
                #endregion

                #region Nhà môi giới
                itemImportNhaMG.Enabled = !listAccessLock.Contains(198);
                itemThemNhaMG.Enabled = !listAccessLock.Contains(199);
                itemDSMG.Enabled = !listAccessLock.Contains(200);
                #endregion

                #region Crawler tin
                itemTinTuc.Enabled = !listAccessLock.Contains(182);
                itemCategoryWeb.Enabled = !listAccessLock.Contains(201);
                itemLayTin.Enabled = !listAccessLock.Contains(202);
                itemSettingHangMuc.Enabled = !listAccessLock.Contains(203);
                #endregion

                #region Khac
                itemHT_TK_FTP.Enabled = !listAccessLock.Contains(35);
                menuDanhMucDiaDanh.Enabled = !listAccessLock.Contains(36);
                #endregion
            }
        }

        void PGCClick()
        {
            //ppContainer.Controls.Clear();
            //ppContainer.ShowPopup(barManager1, new Point(Cursor.Position.X - ppContainer.Width, Cursor.Position.Y + 10));
            //var ctl = new BEE.CongCu.Alert.ctlManagerMini(this);
            //ctl.Dock = DockStyle.Fill;
            //if (ctl.LoadData())
            //    ppContainer.Controls.Add(ctl);
            //else ppContainer.HidePopup();
        }

        void PDCClick()
        {
            //ppContainer.Controls.Clear();
            //ppContainer.ShowPopup(barManager1, new Point(Cursor.Position.X - ppContainer.Width, Cursor.Position.Y + 10));
            //var ctl = new BEE.CongCu.Alert.ctlManagerDC(this);
            //ctl.Dock = DockStyle.Fill;
            //if (ctl.LoadData())
            //    ppContainer.Controls.Add(ctl);
            //else ppContainer.HidePopup();
        }

        public void ShowUserControlInTab(UserControl ctl)
        {
            if (BEE.NgonNgu.Language.LangID == 1)
            {
                try
                {
                    foreach (DevExpress.XtraTab.XtraTabPage t in tabMain.TabPages)
                    {
                        if (t.Text.ToUpper() == ctl.Tag.ToString().ToUpper())
                        {
                            tabMain.SelectedTabPage = t;
                            return;
                        }
                    }

                    DevExpress.XtraTab.XtraTabPage tab = new DevExpress.XtraTab.XtraTabPage();
                    tab.Text = ctl.Tag.ToString().ToUpper();
                    tab.Padding = new System.Windows.Forms.Padding(1);

                    ctl.Dock = DockStyle.Fill;
                    tab.Controls.Add(ctl);

                    tabMain.TabPages.Add(tab);
                    tabMain.SelectedTabPage = tab;
                }
                catch (Exception ex)
                {
                    DialogBox.Error("AddTab - Code: " + ex.Message);
                }
            }
            else
            {
                try
                {
                    db = new MasterDataContext();
                    var listData = (from t in db.lgTranslates
                                    join d in db.lgDictionaries on t.DicID equals d.ID
                                    where t.LangID == BEE.NgonNgu.Language.LangID
                                    select new BEE.NgonNgu.DicItem() { DicValue = d.DicValue, TranValue = t.TranValue }).ToList();

                    foreach (DevExpress.XtraTab.XtraTabPage t in tabMain.TabPages)
                    {
                        string tag = "";
                        try
                        {
                            tag = listData.FirstOrDefault(p => p.DicValue.Trim().ToLower() == ctl.Tag.ToString().ToLower()).TranValue;
                        }
                        catch { tag = ctl.Tag.ToString().ToUpper(); }
                        if (t.Text.ToUpper() == tag.ToUpper())
                        {
                            tabMain.SelectedTabPage = t;
                            return;
                        }
                    }

                    DevExpress.XtraTab.XtraTabPage tab = new DevExpress.XtraTab.XtraTabPage();
                    try
                    {
                        string tag2 = listData.FirstOrDefault(p => p.DicValue.Trim().ToLower() == ctl.Tag.ToString().ToLower()).TranValue;
                        tab.Text = tag2.ToUpper();
                    }
                    catch
                    {
                        tab.Text = ctl.Tag.ToString().ToUpper();

                        DictionaryNew DicNew = new DictionaryNew();
                        DicNew.AddDicValue(ctl.Tag.ToString());
                        db.SubmitChanges();
                    }
                    tab.Padding = new System.Windows.Forms.Padding(1);

                    ctl.Dock = DockStyle.Fill;
                    tab.Controls.Add(ctl);

                    tabMain.TabPages.Add(tab);
                    tabMain.SelectedTabPage = tab;
                }
                catch (Exception ex)
                {
                    DialogBox.Error("AddTab - Code: " + ex.Message);
                }
            }
        }

        public UserControl FindUserControlInTab(string text)
        {
            foreach (DevExpress.XtraTab.XtraTabPage t in tabMain.TabPages)
            {
                if (t.Text == text)
                {
                    tabMain.SelectedTabPage = t;
                    foreach (UserControl i in t.Controls)
                    {
                        if (i.Tag.ToString() == text)
                            return i;
                    }
                }
            }

            return null;
        }

        private void itemTG_Update_ItemClick(object sender, ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\updater.exe");
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void itemTG_About_ItemClick(object sender, ItemClickEventArgs e)
        {
            Help.About2_frm frm = new BEEREMA.Help.About2_frm();
            frm.ShowDialog();
        }

        private void itemHC_CV_Di_Add_ItemClick(object sender, ItemClickEventArgs e)
        {
            //var f = new CongVan.Send.frmEdit();
            //f.ShowDialog();
        }

        private void itemHC_CV_Di_List_ItemClick(object sender, ItemClickEventArgs e)
        {
            //ShowUserControlInTab(new CongVan.Send.ctlManager());
        }

        private void itemHT_TK_PhanQuyen_ItemClick(object sender, ItemClickEventArgs e)
        {
            BEE.HoatDong.PhanQuyen.Permission_ctl ctl = new BEE.HoatDong.PhanQuyen.Permission_ctl();
            ctl.Tag = "Phân quyền";
            ShowUserControlInTab(ctl);
        }

        private void itemHT_TK_Module_ItemClick(object sender, ItemClickEventArgs e)
        {
            BEE.HoatDong.PhanQuyen.Modules_ctl ctl = new BEE.HoatDong.PhanQuyen.Modules_ctl();
            ctl.Tag = "Module";
            ShowUserControlInTab(ctl);
        }

        private void itemHT_NV_List_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.NhanVien.NhanVien_ctl() { Tag = "Danh sách nhân viên" });
        }

        private void itemHT_NV_Add_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new BEE.NhanVien.NhanVien_frm();
            frm.ShowDialog();
        }

        private void itemHT_NV_ChucVu_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ctl = new BEE.NhanVien.ChucVu_ctl();
            ctl.Tag = "Chức vụ";
            ShowUserControlInTab(ctl);
        }

        private void itemHT_NV_PhongBan_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ctl = new BEE.NhanVien.PhongBan_ctl();
            ctl.Tag = "Phòng ban";
            ShowUserControlInTab(ctl);
        }

        private void itemHT_NV_NhomKD_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ctl = new BEE.NhanVien.NhomKD_ctl();
            ctl.Tag = "Nhóm kinh doanh";
            ShowUserControlInTab(ctl);
        }

        private void itemHT_Exit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void itemHT_TK_ChangePass_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new BEEREMA.HeThong.ChangePassword_frm();
            frm.ShowDialog();
        }

        private void itemHT_TK_FTP_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var f = new BEE.FTP.frmConfig())
            {
                f.ShowDialog();
            }
        }

        private void itemHT_TK_ReLogin_ItemClick(object sender, ItemClickEventArgs e)
        {
            User_ReLogin();
        }

        private void frmMainNew_Load(object sender, EventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            LoadPermission();
            DataRow r = it.CommonCls.Row("NhanVien_Login '" + Properties.Settings.Default.UserName + "'");
            try
            {
                if (BEE.NgonNgu.Language.LangID != 1)
                {
                    this.Text = Properties.Settings.Default.SoftName + " - Software Solution for Real Estate Business Management";

                    lblNguoiDung.Caption = ("User: " + r["HoTen"].ToString()).ToUpper();
                    lblQuyTruyCap.Caption = ("Permission Access: " + Common.PerName).ToUpper();
                }
                else
                {
                    this.Text = "PHẦN MỀM BẤT ĐỘNG SẢN HÒA LAND";

                    lblNguoiDung.Caption = ("Người dùng: " + r["HoTen"].ToString()).ToUpper();
                    lblQuyTruyCap.Caption = ("Quyền truy cập: " + Common.PerName).ToUpper();

                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("vi-VN");
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("vi-VN");
                    System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalDigits = 0;
                    System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol = "VNĐ";
                    System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";

                    System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalDigits = 0;
                    System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ",";
                    System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ".";

                    System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.PercentDecimalSeparator = ",";
                    System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.PercentGroupSeparator = ".";
                }

                VoidIPConnect();

              //  itemaccall.PerformClick();



                //  itemCopyright.EditValue = Properties.Resources.;

                BEE.NgonNgu.Language.TranslateControl(this, null, itemThemMG);

                //   ShowUserControlInTab(new View_ctl());
                ShowUserControlInTab(new ctlFirstView());

                var frmChat = new Chat.frmMessOffline();
                if (frmChat.MessCount > 0)
                    frmChat.Show(this);
                else
                    frmChat.Dispose();

                timerNhacViec.Start();
                timerAlert.Start();

                timer1.Interval = 30000;
                timer1.Start();

                timerTuLock.Interval = ((int)db.UserTuLocks.First().ThoiGisn * 60000);
                timerTuLock.Start();


                using (DefaultLookAndFeel dlf = new DefaultLookAndFeel())
                {
                    dlf.LookAndFeel.SkinName = Common.Skins;
                }
                InitSkinGallery();

                LoadCapsLock();
                LoadNumLock();
                LoadInsert();
            }
            catch { }

            wait.Close();
            wait.Dispose();

            itemThemMG.SelectedPage = ribbonPageSystem;

            SendKeys.Send("^{F1}");
        }

        private void itemSkins_GalleryItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {
            Common.Skins = e.Item.Caption;
        }

        private void itemProduct_Project_Lisr_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.DuAn.DuAn_ctl() { Tag = "Quản lý Dự án" });
        }

        private void itemProduct_Project_Promotion_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.DuAn.Promotion.ctlManager() { Tag = e.Item.Caption });
        }

        private void itemProduct_Project_Add_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.DuAn.DuAn_frm())
            {
                frm.ShowDialog();
            }
        }

        private void itemCRM_Customer_List_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.KhachHang.KhachHang_ctl() { Tag = "Danh sách khách hàng" });
        }

        private void itemCRM_Customer_Add_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.KhachHang.KhachHang_frm())
            {
                frm.ShowDialog();
            }
        }

        private void tabMain_CloseButtonClick(object sender, EventArgs e)
        {
            DevExpress.XtraTab.XtraTabPage page = (DevExpress.XtraTab.XtraTabPage)
                (e as DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs).Page;

            if (page.Text.ToLower() != "main")
            {
                tabMain.TabPages.Remove(page);
            }
        }

        private void frmMainNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.CapsLock)
            {
                if (isCapsLock)
                    isCapsLock = false;
                else
                    isCapsLock = true;
                lblCapsLock.Caption = isCapsLock ? "CAPS" : "         ";
            }

            if (e.KeyCode == Keys.NumLock)
            {
                if (isNumLock)
                    isNumLock = false;
                else
                    isNumLock = true;
                lblNumLock.Caption = isNumLock ? "NUM" : "         ";
            }

            if (e.KeyCode == Keys.Insert)
            {
                if (isInsert)
                    isInsert = false;
                else
                    isInsert = true;
                lblInsert.Caption = isInsert ? "  INS  " : "         ";
            }
        }

        private void itemSystem_Company_Add_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var f = new MyCompany.frmEdit())
            {
                f.ShowDialog();
            }
        }

        private void itemSystem_Company_List_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new MyCompany.ctlManager());
        }

        private void itemCRM_Dictionary_Source_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var f = new BEE.HoatDong.MGL.frmNguon())
            {
                f.ShowDialog();
            }
        }

        private void itemCRM_Dictionary_DanhXung_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.KhachHang.DanhXung_ctl() { Tag = "Danh xưng" });
        }

        private void itemCRM_Distionary_Career_ItemClick(object sender, ItemClickEventArgs e)
        {
            BEE.KhachHang.NgheNghiep_ctl ctl = new BEE.KhachHang.NgheNghiep_ctl();
            ctl.Tag = e.Item.Caption;
            ShowUserControlInTab(ctl);
        }

        private void itemCRM_Dictionary_NhomKD_ItemClick(object sender, ItemClickEventArgs e)
        {
            BEE.KhachHang.NhomKH_ctl ctl = new BEE.KhachHang.NhomKH_ctl();
            ctl.Tag = e.Item.Caption;
            ShowUserControlInTab(ctl);
        }

        private void itemCRM_Dictionary_LoaiHinhKD_ItemClick(object sender, ItemClickEventArgs e)
        {
            BEE.KhachHang.LoaiHinhKD_ctl ctl = new BEE.KhachHang.LoaiHinhKD_ctl();
            ctl.Tag = e.Item.Caption;
            ShowUserControlInTab(ctl);
        }

        private void itemProduct_Dictionary_Zone_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.DuAn.frmKhu())
            {
                frm.ShowDialog();
            }
        }

        private void itemProduct_Dictionary_Subdivision_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.DuAn.frmPhanKhu())
            {
                frm.ShowDialog();
            }
        }

        private void itemProduct_Dictionary_ProjectCate_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.DuAn.LoadiDA_ctl());
        }

        private void itemProduct_Direction_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.BatDongSan.Huong_ctl());
        }

        private void itemProduct_Dictionary_Status_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.SanPham.frmTrangThai())
            {
                frm.ShowDialog();
            }
        }

        private void itemProduct_Dictionary_ProductCate_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.BatDongSan.LoadiBDS_ctl() { Tag = e.Item.Caption });
        }

        private void itemSMS_SendList_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.QuangCao.SMS.ctlSendingSMS() { Tag = "SMS: " + e.Item.Caption });
        }

        private void itemSMS_ListReceived_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.QuangCao.SMS.ctlGroupReceivesSMS() { Tag = "SMS: " + e.Item.Caption });
        }

        private void itemSMS_Template_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.QuangCao.SMS.ctlTemplatesSMS() { Tag = "SMS: " + e.Item.Caption });
        }

        private void itemSMS_Category_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.QuangCao.SMS.ctlCategories() { Tag = "SMS: " + e.Item.Caption });
        }

        private void itemsubNhanVien_Add_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemHT_NV_Add_ItemClick(sender, e);
        }

        private void itemsubNhanVien_List_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemHT_NV_List_ItemClick(sender, e);
        }

        private void itemsubCongTy_Add_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemSystem_Company_Add_ItemClick(sender, e);
        }

        private void itemsubCongTy_List_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemSystem_Company_List_ItemClick(sender, e);
        }

        private void itemsubKhachHang_Add_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemCRM_Customer_Add_ItemClick(sender, e);
        }

        private void itemsubKhachHang_List_ItemClick(object sender, ItemClickEventArgs e)
        {
            //ShowUserControlInTab(new BEE.KhachHang.KhachHang_ctl() { Tag = e.Item.Caption });
            itemCRM_Customer_List_ItemClick(sender, e);
        }

        private void itemsubTuDien_DanhXung_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemCRM_Dictionary_DanhXung_ItemClick(sender, e);
        }

        private void itemsubTuDien_NganhNghe_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemCRM_Distionary_Career_ItemClick(sender, e);
        }

        private void itemsubTuDien_NhomKH_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemCRM_Dictionary_NhomKD_ItemClick(sender, e);
        }

        private void itemsubTuDien_NguonDen_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemCRM_Dictionary_Source_ItemClick(sender, e);
        }

        private void itemsubTuDien_LoaiHinhKD_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemCRM_Dictionary_LoaiHinhKD_ItemClick(sender, e);
        }

        private void itemsubDuAn_Add_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemProduct_Project_Add_ItemClick(sender, e);
        }

        private void itemsubDuAn_List_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemProduct_Project_Lisr_ItemClick(sender, e);
        }

        private void itemsubDuAn_ListSale_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemProduct_Project_Promotion_ItemClick(sender, e);
        }

        private void itemsubTuDien_Khu_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemProduct_Dictionary_Zone_ItemClick(sender, e);
        }

        private void itemsubTuDien_PhanKhu_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemProduct_Dictionary_Subdivision_ItemClick(sender, e);
        }

        private void itemsubTuDien_LoaiDuAn_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemProduct_Dictionary_ProjectCate_ItemClick(sender, e);
        }

        private void itemsubTuDien_Huong_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemProduct_Direction_ItemClick(sender, e);
        }

        private void itemsubTuDien_TrangThai_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemProduct_Dictionary_Status_ItemClick(sender, e);
        }

        private void itemsubTuDien_LoaiBatDongSan_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemProduct_Dictionary_ProductCate_ItemClick(sender, e);
        }

        private void itemsubSMS_KHGui_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemSMS_SendList_ItemClick(sender, e);
        }

        private void itemsubSMS_ListNguoiNhan_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemSMS_ListReceived_ItemClick(sender, e);
        }

        private void itemsubSMS_ListTemp_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemSMS_Template_ItemClick(sender, e);
        }

        private void itemsubSMS_PhanLoaiMau_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemSMS_Category_ItemClick(sender, e);
        }

        #region Chat module
        private void timerCheckOnline_Tick(object sender, EventArgs e)
        {
            timerCheckOnline.Stop();
            try
            {
                var ltNV = db.NhanViens.Where(p => p.MaTT == 2).Select(p => p.MaNV).ToList();
                Chat.ctlFriendList.ltOnline = new List<int>();
                foreach (var i in ltNV)
                {
                    Chat.ctlFriendList.ltOnline.Add(i);
                }
            }
            catch { }
            timerCheckOnline.Start();
        }

        private void timerNewChat_Tick(object sender, EventArgs e)
        {
            timerNewChat.Stop();
            try
            {
                var ltNV = db.chatTinNhans.Where(p => p.MaNhan == Common.StaffID & p.MaTT == 2 & p.ChuaDoc == true)
                        .Select(p => new { p.MaGui, p.NhanVien.HoTen, p.NhanVien.MaSo })
                        .Distinct().ToList();
                foreach (var i in ltNV)
                {
                    if (Chat.ctlFriendList.ltMessBox.Where(p => p.Name == i.MaGui.ToString()).Count() <= 0)
                    {
                        var frm = new Chat.frmSendMessage(i.MaGui.Value, i.HoTen + " (" + i.MaSo + ")", 2);
                        frm.Name = i.MaGui.ToString();
                        frm.Show();
                        Chat.ctlFriendList.ltMessBox.Add(frm);
                    }
                }
            }
            catch { }
            timerNewChat.Start();
        }
        #endregion

        #region Alert Module
        private void timerAlert_Tick(object sender, EventArgs e)
        {
            timerAlert.Stop();
            try
            {
                using (var db = new MasterDataContext())
                {
                    int maNV = Common.StaffID;
                    int count;

                    if (BEE.NgonNgu.Language.LangID == 1)
                    {
                        count = db.alAlerts.Where(p => p.FormID == 27 && p.UserID == maNV).Count();
                        itemHD_PGC_List.Caption = "PHIẾU GIỮ CHỖ (" + count + ")";

                        count = db.alAlerts.Where(p => p.FormID == 28 && p.UserID == maNV).Count();
                        itemHD_PDC_List.Caption = "PHIẾU ĐẶT CỌC (" + count + ")";

                        count = db.alAlerts.Where(p => p.FormID == 26 && p.UserID == maNV).Count();
                        itemHD_HDVV_List.Caption = "HỢP ĐỒNG GÓP VỐN (" + count + ")";

                        count = db.alAlerts.Where(p => p.FormID == 29 && p.UserID == maNV).Count();
                        itemHD_HDMB_List.Caption = "HỢP ĐỒNG MUA BÁN (" + count + ")";
                    }
                    else
                    {
                        count = db.alAlerts.Where(p => p.FormID == 27 && p.UserID == maNV).Count();
                        itemHD_PGC_List.Caption = "Reservasion Ticket (" + count + ")";

                        count = db.alAlerts.Where(p => p.FormID == 28 && p.UserID == maNV).Count();
                        itemHD_PDC_List.Caption = "Deposit Ticket (" + count + ")";

                        count = db.alAlerts.Where(p => p.FormID == 26 && p.UserID == maNV).Count();
                        itemHD_HDVV_List.Caption = "Capital Contribution Contract (" + count + ")";

                        count = db.alAlerts.Where(p => p.FormID == 29 && p.UserID == maNV).Count();
                        itemHD_HDMB_List.Caption = "Sales Contract (" + count + ")";
                    }
                }
            }
            catch { }

            timerAlert.Start();
        }

        private void alctAltert_AlertClick(object sender, DevExpress.XtraBars.Alerter.AlertClickEventArgs e)
        {
            try
            {
                dockPanel1.Show();
                //string[] str = e.Info.Tag.ToString().Split('|');
                //switch (int.Parse(str[0]))
                //{
                //    case 26:
                //        ShowUserControlInTab(new NghiepVu.VayVon.ctlManager());
                //        break;
                //    case 27:
                //        ShowUserControlInTab(new NghiepVu.GiuCho.ctlManager());
                //        break;
                //    case 28:
                //        ShowUserControlInTab(new NghiepVu.DatCoc.ctlManager());
                //        break;
                //    case 29:
                //        ShowUserControlInTab(new NghiepVu.HDMB.ctlManager());
                //        break;
                //    case 30:
                //        ShowUserControlInTab(new NghiepVu.BanGiao.ctlManager());
                //        break;
                //    case 31:
                //        var objTL = db.tlbhThanhLies.Single(p => p.MaTL == int.Parse(str[1]));
                //        var ctl = new NghiepVu.ThanhLy.ctlManager();
                //        ctl.MaLGD = objTL.MaLGD.Value;
                //        ShowUserControlInTab(ctl);
                //        break;
                //    case 1:
                //        ShowUserControlInTab(new CongViec.NhiemVu.List_ctl());
                //        break;
                //    case 2:
                //        ShowUserControlInTab(new CongViec.LichHen.SchedulerList_ctl());
                //        break;
                //}
                //this.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void alctAltert_ButtonClick(object sender, DevExpress.XtraBars.Alerter.AlertButtonClickEventArgs e)
        {
            //try
            //{
            //    var str = e.Info.Tag.ToString().Split('|'); // Nhiem vu (1|212);  Lich hen(0|23)
            //    switch (e.ButtonName)
            //    {
            //        case "Stop":
            //        case "Start":
            //            if (str[0] == "1")
            //                db.NhiemVu_updateNoRepeat(int.Parse(str[1]), Common.StaffID, e.ButtonName == "Stop");
            //            else
            //                db.LichHen_updateNoRepeat(int.Parse(str[1]), Common.StaffID, e.ButtonName == "Stop");

            //            if (e.ButtonName == "Stop")
            //            {
            //                e.Button.Name = "Start";
            //                e.Button.Image = Properties.Resources.Repeat;
            //                e.Button.Hint = "Nhắc lại";
            //            }
            //            else
            //            {
            //                e.Button.Name = "Stop";
            //                e.Button.Image = Properties.Resources.stop;
            //                e.Button.Hint = "Không nhắc lại";
            //            }
            //            break;

            //        case "Close":
            //            e.AlertForm.Close();
            //            break;

            //        case "Muted":
            //            MusicCls.Close();
            //            break;
            //        //case "Process":
            //        //    e.AlertForm.Close();
            //        //    if (str[0] == "1")
            //        //    {
            //        //        var f = new BEEREMA.CongViec.NhiemVu.frmDuyet();
            //        //        f.MaNVu = int.Parse(str[1]);
            //        //        f.ShowDialog();
            //        //    }
            //        //    else
            //        //    {
            //        //        var f = new BEE.LichLamViec.frmProcess();
            //        //        f.MaLH = int.Parse(str[1]);
            //        //        f.ShowDialog();
            //        //    }
            //        //    break;
            //    }
            //}
            //catch { }
        }

        private void alctAltert_FormClosing(object sender, DevExpress.XtraBars.Alerter.AlertFormClosingEventArgs e)
        {
            try
            {
                MusicCls.Close();
            }
            catch { }
        }
        #endregion

        #region Lich lam viec
        private void timerNhacViec_Tick(object sender, EventArgs e)
        {
            timerNhacViec.Stop();
            //try
            //{
            //    string time = new System.Net.WebClient().DownloadString("http://infoland.com.vn/upload/BEEREMA/demodiptrial.aspx?key=EC3jMlDL8F8EwULcbBNF/BJvwlIPgFgsFi2F9E1uL/c=");
            //    if (time == "False")
            //    {
            //        DialogBox.Infomation("The software has expired. Please contact DIP Vietnam. Thank you!");
            //        Application.Exit();
            //    }
            //}
            //catch { }
            try
            {

                var ltLLV = db.NhiemVu_getNhacViec(Common.StaffID).ToList();
                if (ltLLV.Count > 0)
                {
                    int aLH = ltLLV.Where(p => p.FormID != 1).Count();
                    int aNV = ltLLV.Where(p => p.FormID == 1).Count();

                    gcReminder.DataSource = ltLLV.Select(o => new
                    {
                        LinkID = o.LinkID,
                        FormID = o.FormID,
                        Caption = o.TieuDe,
                        Content = string.Format("{0:dd/MM/yyyy | HH:mm}", o.NgayBD) + " › - " + o.DienGiai
                    }).ToList();

                    var infoText = string.Format("Có: {0} lịch hẹn. \nCó: {1} nhiệm vụ", aLH, aNV);
                    var infoCaption = "Thông báo";
                    AlertInfo info = new AlertInfo(infoCaption, infoText, infoText, Properties.Resources.Alarm_Clock);
                    alctAltert.Show(this, info);
                }
            }
            catch { }
            timerNhacViec.Interval = 300000;
            timerNhacViec.Start();
        }
        #endregion

        private void timerRequest_Tick(object sender, EventArgs e)
        {
            //timerRequest.Stop();
            //try
            //{
            //    var NewEvent = db.AlertNewEvent(lblCustomerNew2.Enabled, lblSupport.Enabled, lblRegistration.Enabled).ToList();
            //    //int? Amount = 0;
            //    //db.aCustomerHistory_getNotReplyAlert(ref Amount);
            //    if (NewEvent[0].Support > 0)
            //    {
            //        //AlertInfo info = new AlertInfo("Thông báo", string.Format("Có ({0:n0}) yêu cầu hỗ trợ của Nhân viên Sàn.", NewEvent[0].Support));
            //        //info.Tag = "Request";
            //        //alertRequest.Show(this, info);
            //        lblSupport.Caption = string.Format("Support ({0})", NewEvent[0].Support);
            //        itemNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //    }
            //    else
            //    {
            //        lblSupport.Caption = "Support (0)";
            //        itemNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    }

            //    //db.aCustomer_getNotConfirmAlert(ref Amount);
            //    if (NewEvent[0].Customer > 0)
            //    {
            //        //AlertInfo info = new AlertInfo("Thông báo", string.Format("Có ({0:n0}) Khách hàng mới hoặc yêu cầu chia sẽ thông tin của Nhân viên Sàn.", NewEvent[0].Customer));
            //        //info.Tag = "Confirm";
            //        //alertRequest.Show(this, info);
            //        lblCustomerNew2.Caption = string.Format("New Customer ({0})", NewEvent[0].Customer);
            //        itemNew3.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //    }
            //    else
            //    {
            //        lblCustomerNew2.Caption = "New Customer (0)";
            //        itemNew3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    }

            //    if (NewEvent[0].Registration > 0)
            //    {
            //        //AlertInfo info = new AlertInfo("Thông báo", string.Format("Có ({0:n0}) Phiếu đăng ký.", NewEvent[0].Registration));
            //        //info.Tag = "Registration";
            //        //alertRequest.Show(this, info);
            //        lblRegistration.Caption = string.Format("Registration Form ({0})", NewEvent[0].Registration);
            //        itemNew2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //    }
            //    else
            //    {
            //        lblRegistration.Caption = "Registration Form (0)";
            //        itemNew2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    }
            //}
            //catch { }

            //timerRequest.Start();
            //timerRequest.Interval = 180000;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Interval = 600000;
            timer1.Start();
        }

        private void itemDocument_Add_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (BEE.TaiLieu.frmEdit f = new BEE.TaiLieu.frmEdit())
            {
                f.ShowDialog();
            }

            ShowUserControlInTab(new BEE.TaiLieu.ctlManager() { Tag = e.Item.Caption });
        }

        private void itemDocument_List_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.TaiLieu.ctlManager() { Tag = e.Item.Caption });
        }

        private void itemDocument_Category_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (BEE.TaiLieu.frmLoaiTaiLieu f = new BEE.TaiLieu.frmLoaiTaiLieu())
            {
                f.ShowDialog();
            }
        }

        private void itemsubDocument_Add_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemDocument_Add_ItemClick(sender, e);
        }

        private void itemsubDocument_List_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemDocument_List_ItemClick(sender, e);
        }

        private void itemsubDocument_Category_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemDocument_Category_ItemClick(sender, e);
        }

        private void itemMail_Sending_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.QuangCao.Mail.ctlSending() { Tag = "Mail: Kế hoạch gửi" });
        }

        private void itemMail_Receives_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.QuangCao.Mail.ctlReceive() { Tag = "Mail: Danh sách người nhận" });
        }

        private void itemMail_Template_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.QuangCao.Mail.ctlTemplates() { Tag = "Mail: Mẫu gửi" });
        }

        private void itemMail_Category_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.QuangCao.Mail.ctlCategory() { Tag = "Mail: Phân loại mẫu" });
        }

        private void itemMail_Config_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.QuangCao.Mail.ctlConfig() { Tag = "Mail: Cấu hình" });
        }

        private void itemSetupHBBDMail_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var f = new BEE.QuangCao.Mail.frmConfigReminder())
            {
                f.SetID = 5;
                f.ShowDialog();
            }
        }

        private void itemSetupHBBDSMS_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var f = new BEE.QuangCao.SMS.frmConfigReminder())
            {
                f.SetID = 6;
                f.ShowDialog();
            }
        }

        private void itemSMS_TopUp_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.QuangCao.SMS.frmTopUp())
            {
                frm.ShowDialog(this);
            }
        }

        private void itemSMS_TopupHistory_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.QuangCao.SMS.ctlTopupHistory());
        }

        private void itemSMS_Statistic_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.QuangCao.SMS.ctlStatistic());
        }

        private void itemSMS_Money_ItemClick(object sender, ItemClickEventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                SmsConfig objConfig = new SmsConfig();
                objConfig.getAccount();

                var sms = new SMSService.ServiceSoapClient("ServiceSoap");
                decimal balance = sms.getBalance(objConfig.ClientNo, objConfig.ClientPass);
                wait.Hide();
                DialogBox.Infomation(string.Format("Số tiền trong tài khoản của bạn: {0:#,0} VNĐ.", balance < 0 ? 0 : balance));
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                wait.Close();
            }
        }

        private void itemSMS_Account_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.QuangCao.SMS.frmAccount())
            {
                frm.ShowDialog();
            }
        }

        private void gvReminder_CalcPreviewText(object sender, DevExpress.XtraGrid.Views.Grid.CalcPreviewTextEventArgs e)
        {
            e.PreviewText = gvReminder.GetRowCellValue(e.RowHandle, "Content").ToString();
        }

        private void gvReminder_CustomDrawRowPreview(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            try
            {
                int dx = 5;
                // A rectangle for displaying text.
                Rectangle r = e.Bounds;
                r.X += e.Bounds.Height + dx * 2;
                r.Width -= (e.Bounds.Height + dx * 3);
                // Draw an image from the "Photo" column.
                string imgPath = Convert.ToInt32(gvReminder.GetRowCellValue(e.RowHandle, "FormID")) == 1 ? "\\tag-48.png" : "\\appointment.png";
                var img = Image.FromFile(Application.StartupPath + imgPath);
                e.Graphics.DrawImage(img, e.Bounds.X + dx,
                  e.Bounds.Y, e.Bounds.Height, e.Bounds.Height);
                // Draw the text.
                e.Appearance.DrawString(e.Cache, gvReminder.GetRowPreviewDisplayText(e.RowHandle), r);
                // No default painting is required
                e.Handled = true;
            }
            catch { }
        }

        private void gvReminder_RowClick(object sender, RowClickEventArgs e)
        {
            //popupControlContainer1.ShowPopup(Cursor.Position);
        }

        private void gvReminder_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        void LoadDetail()
        {
            try
            {
                formID = Convert.ToInt32(gvReminder.GetFocusedRowCellValue("FormID"));
                linkID = Convert.ToInt32(gvReminder.GetFocusedRowCellValue("LinkID"));

                try
                {
                    if (formID != 1)
                    {
                        gcHistory.DataSource = db.lhLichSus.Where(o => o.MaLH == linkID)
                                .OrderByDescending(o => o.NgayNhap)
                                .Select(o => new
                                {
                                    GhiChu = "‹" + o.NhanVien.MaSo + "› - " + o.DienGiai,
                                    o.NgayNhap
                                }).ToList();
                    }
                    else
                    {
                        gcHistory.DataSource = db.NhiemVu_LichSus.Where(o => o.MaNVu == linkID)
                                .OrderByDescending(o => o.NgayCN)
                                .Select(o => new
                                {
                                    GhiChu = "‹" + o.NhanVien.MaSo + "› - " + o.DienGiai,
                                    o.NgayCN
                                }).ToList();
                    }
                }
                catch
                {
                    gcHistory.DataSource = null;
                }
            }
            catch { }
        }

        private void itemLLV_NhiemVu_List_ItemClick(object sender, ItemClickEventArgs e)
        {
            //ShowUserControlInTab(new BEEREMA.Product.ctlViewGeneralFormUrlV2() { Tag = "Sơ đồ tổng thể: View 1" });
            ShowUserControlInTab(new BEEREMA.CongViec.NhiemVu.List_ctl() { Tag = "Nhiệm vụ" });
        }

        private void itemLLV_LichHen_List_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEEREMA.CongViec.LichHen.SchedulerList_ctl { Tag = "Lịch hẹn" });
        }

        private void itemLLV_NhiemVuThem_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEEREMA.CongViec.NhiemVu.AddNew_frm())
            {
                frm.ShowDialog();
            }
        }

        private void itemLLV_LichHenThem_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new CongViec.LichHen.AddNew_frm(null, null, ""))
            {
                frm.ShowDialog();
            }
        }

        private void itemProcess_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (formID == 1)
            {
                var f = new BEEREMA.CongViec.NhiemVu.frmDuyet();
                f.MaNVu = linkID;
                f.ShowDialog();
                if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadDetail();
            }
            else
            {
                var f = new BEE.LichLamViec.frmProcess();
                f.MaLH = linkID;
                f.ShowDialog();
                if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadDetail();
            }
        }

        private void itemMute_ItemClick(object sender, ItemClickEventArgs e)
        {
            MusicCls.Close();
        }

        private void itemStop_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (formID == 1)
                    db.NhiemVu_updateNoRepeat(linkID, Common.StaffID, true);
                else
                    db.LichHen_updateNoRepeat(linkID, Common.StaffID, true);
                gvReminder.DeleteSelectedRows();
            }
            catch { }
        }

        private void itemBCLichSuGD_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BaoCao.ctlLichSuGiaoDich() { Tag = "BC: " + e.Item.Caption });
        }

        private void itemHT_CD_QuocGia_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var f = new BEEREMA.Other.frmNationality())
            {
                f.ShowDialog();
            }
        }

        private void itemHT_CD_Tinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.NghiepVuKhac.Tinh_ctl() { Tag = "Tỉnh (TP)" });
        }

        private void itemHT_CD_Huyen_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.NghiepVuKhac.Huyen_ctl() { Tag = "Quận (huyện)" });
        }

        private void itemHT_CD_Xa_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.NghiepVuKhac.Xa_ctl() { Tag = "Xã (Phường)" });
        }

        private void itemSP_SP_TM_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.HoatDong.MGL.Ban.frmEdit())
            {
                frm.ShowDialog();
            }
        }

        private void itemSP_SP_DS_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.HoatDong.MGL.Ban.ctlManager());
        }

        private void itemKH_NC_TM_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.HoatDong.MGL.Mua.frmEdit())
            {
                frm.ShowDialog();
            }
        }

        private void itemKH_NC_DS_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.HoatDong.MGL.Mua.ctlManager());
        }

        private void itemKH_NC_CD_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.HoatDong.MGL.frmCaiDatLoc())
            {
                frm.ShowDialog();
            }
        }

        private void itemCV_GD_TM_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.HoatDong.MGL.GiaoDich.frmEdit())
            {
                frm.ShowDialog();
            }
        }

        private void itemCV_GD_DS_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.HoatDong.MGL.GiaoDich.ctlManager());
        }

        private void itemCV_CV_DS_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.HoatDong.MGL.XuLy.ctlManager());
        }

        private void itemNV_DM_CD_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.HoatDong.MGL.frmCaiDatNhanMailCV())
            {
                frm.ShowDialog();
            }
        }

        private void itemLLV_ThoiDiemLichHen_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEEREMA.CongViec.LichHen.ThoiDiem_frm())
            {
                frm.ShowDialog();
            }
        }

        private void itemLLV_LoaiLichHen_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new CongViec.LichHen.ChuDe_frm())
            {
                frm.ShowDialog();
            }
        }

        private void itemLLV_LoaiNhiemVu_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEEREMA.CongViec.NhiemVu.LoaiNhiemVu_frm())
            {
                frm.ShowDialog();
            }
        }

        private void itemLLV_TienDo_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEEREMA.CongViec.NhiemVu.frmTienDo())
            {
                frm.ShowDialog();
            }
        }

        private void itemLLV_TrangThaiNhiemVu_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEEREMA.CongViec.NhiemVu.TinhTrang_frm())
            {
                frm.ShowDialog();
            }
        }

        private void itemLLV_MucDoNhiemVu_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEEREMA.CongViec.NhiemVu.MucDo_frm())
            {
                frm.ShowDialog();
            }
        }

        private void itemNV_NV_LS_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEEREMA.HeThong.ctlLSDangNhap());
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new CrawlerWebNew.ctlNew());
        }

        private void itemCategoryWeb_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new CrawlerWebNew.Category.ctlManager());
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            CrawlerWebNew.frmGetNews frm = new CrawlerWebNew.frmGetNews();
            frm.ShowDialog();
        }

        private void itemImportNhaMG_ItemClick(object sender, ItemClickEventArgs e)
        {

            using (var frm = new BEE.HoatDong.MGL.NguoiMG.frmImport())
            {
                frm.ShowDialog();
            }
        }

        private void itemDSMG_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.HoatDong.MGL.NguoiMG.ctlManager());
        }

        private void itemThemNhaMG_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.HoatDong.MGL.NguoiMG.frmEdit())
            {
                frm.ShowDialog();
            }
        }

        private void itemTest_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.QuangCao.MailV2.frmContentEdit())
            {
                frm.ShowDialog();
            }
        }

        private void itemDSSendMail_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.QuangCao.MailV2.ctlManager());
        }

        private void itemDSCanChoThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.HoatDong.MGL.Ban.ctlManagerChoThue());
        }

        private void itemDSCanThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.HoatDong.MGL.Mua.ctlManagerCanThue());
        }

        private void itemSettingHangMuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new CrawlerWebNew.frmChuyenMuc())
            {
                frm.ShowDialog();
            }
        }

        private void itemCRM_Dictionary_ThoiDiem_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void itemCHKKH_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.KhachHang.frmCheckDT())
            {
                frm.ShowDialog();
            }
        }

        private void itemDuong_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.HoatDong.MGL.Ban.frmDuong())
            {
                frm.ShowDialog();
            }
        }

        private void itemTuLock_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new HeThong.frmTuLock())
            {
                frm.ShowDialog();
            }
        }
        Point cursorPoint;
        int minutesIdle = 0;
        private bool isIdle(int minutes)
        {
            return minutesIdle >= minutes;
        }

        private void timerTuLock_Tick(object sender, EventArgs e)
        {
            timerTuLock.Stop();
            if (Cursor.Position != cursorPoint)
            {
                // The mouse moved since last check
                timerTuLock.Interval = Convert.ToInt32(db.UserTuLocks.First().ThoiGisn * 60000);
            }
            else
            {
                // Mouse still stoped
                if (timerTuLock.Interval == (db.UserTuLocks.First().ThoiGisn * 60000))
                {
                    User_ReLogin();
                }
            }

            // Save current position
            cursorPoint = Cursor.Position;
            timerTuLock.Start();
        }

        private void itemThuTuCanBan_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.HoatDong.MGL.Ban.frmCaiDatThuTu())
            {
                frm.ShowDialog();
            }
        }

        private void itemThuTuCanMT_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.HoatDong.MGL.Mua.frmCaiDatThuTu())
            {
                frm.ShowDialog();
            }
        }

        private void itemLoaiTien_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.NghiepVuKhac.LoaiTien_ctl() { Tag = "Loại tiền" });
        }

        private void itemMoiQuanHe_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.KhachHang.frmMoiQuanHe())
            {
                frm.ShowDialog();
            }
        }

        private void itemPhanQuyenBC_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.HoatDong.MGL.Ban.frmPhanQuyen())
            {
                frm.ShowDialog();
            }
        }

        private void itemPhanQuyenMT_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new BEE.HoatDong.MGL.Mua.frmPhanQuyen())
            {
                frm.ShowDialog();
            }
        }

        private void itemKhachChoXoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.KhachHang.KhachHangChoDuyet() { Tag = "Khách hàng chờ xóa" });
        }

        private void itemChoXoaCB_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.HoatDong.MGL.Ban.ctlManagerChoXoa() { Tag = "Sản phẩm cần bán chờ xóa" });
        }

        private void itemCauHinhTongDai_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetupVoip();
        }
        void SetupVoip()
        {
            short? ID;
            if (db.voipServerConfigs.Count() > 0)
            {
                ID = db.voipServerConfigs.FirstOrDefault().ID;
                using (var frm = new BEE.VOIPSETUP.SERVER.frmEditServer() { ServerID = ID })
                {
                    frm.ShowDialog();
                }
            }
            else
                using (var frm = new BEE.VOIPSETUP.SERVER.frmEditServer())
                {
                    frm.ShowDialog();
                }

        }

        public void PopupVoip(string _sdt, string _line, string _unqueid)
        {

            var objConfig = db.voipServerConfigs.FirstOrDefault();
            var objLine = db.voipLineConfigs.Where(p => p.MaNV == Common.StaffID).Select(p => p.LineNumber).ToList();
            if (objLine.Count() <= 0)
            {
                 DialogBox.Infomation("Bạn chưa cài đặt line, vui lòng kiểm tra lại.");
                return;
            }

            if ((objLine.Count() > 0 && objLine.Contains(_line.ToString()) == true))
            {
                var frC = new BEE.VOIPSETUP.Call.frmCall();
                frC.SDT = _sdt;
                frC.Unique = _unqueid;
                frC.line = _line;
                frC.NhanVienTN = Common.StaffName;
                frC.LoaiCG = 0;
                var x1 = x + 20;
                var y1 = y + 20;
                frC.Location = new Point(x1, y1);
                this.x = x1;
                this.y = y1;
                frC.ShowDialog();
            }


        }

        private String UserName { get; set; }
        private  IHubProxy HubProxy { get; set; }
        const string ServerURI = "http://hoaland.quanlykinhdoanh.com.vn:3333/signalr";
        private HubConnection Connection { get; set; }
        private void Connection_Closed()
        {
            //Deactivate chat UI; show login UI. 
            //this.Invoke((Action)(() => ChatPanel.Visible = false));
            // this.Invoke((Action)(() => ButtonSend.Enabled = false));
            // this.Invoke((Action)(() => StatusText.Text = "You have been disconnected."));
            // this.Invoke((Action)(() => SignInPanel.Visible = true));
        }

        public void ConnectVOIP()
        {
            try
            {

                Connection = new HubConnection(ServerURI);
                Connection.Closed += Connection_Closed;
                HubProxy = Connection.CreateHubProxy("MyHub");
                //Handle incoming event from server: use Invoke to write to console from SignalR's thread
                HubProxy.On<string, string, string>("addNewMessageToPage", (line, sdt, uniqueid) =>
                    this.Invoke((Action)(() =>
                       // MessageBox.Show(line + sdt + uniqueid)

                       PopupVoip(sdt, line, uniqueid)
                    ))
                );
                try
                {
                    Connection.Start();
                }
                catch
                {
                    //StatusText.Text = "Unable to connect to server: Start server before connecting clients.";
                    //No connection: Don't enable Send button or show chat UI
                    return;
                }

                //Activate UI
                // SignInPanel.Visible = false;
                // ChatPanel.Visible = true;
                //  ButtonSend.Enabled = true;
                // TextBoxMessage.Focus();
                //  RichTextBoxConsole.AppendText("Connected to server at " + ServerURI + Environment.NewLine);
            }
            catch (Exception exx)
            {
                MessageBox.Show(exx.Message);
            }

        }

        private void itemaccall_ItemClick(object sender, ItemClickEventArgs e)
        {
            ConnectVOIP();
        }

        private void itemLichSuTongDai_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.VOIPSETUP.CDR.cltManagerYeastar() { Tag = "Lịch sử tổng đài " });
        }

        private void itemLichSuTuVan_ItemClick(object sender, ItemClickEventArgs e)
        {
           /// ShowUserControlInTab(new BEE.KhachHang.TuVan.ctlThongKeKhaoSat() { Tag = "Lịch sử tư vấn" });
        }

        private void frmMainNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Connection != null)
            {
                Connection.Stop();
                Connection.Dispose();
            }
        }

        private void itemDuyetXoaCCT_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.HoatDong.MGL.Ban.ctlManagerChoXoaCT() { Tag = "Sản phẩm cần cho thuê chờ xóa" });
        }

        private void itemDuyetXoaCM_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.HoatDong.MGL.Mua.ctlManagerChoXoa() { Tag = "Sản phẩm cần mua chờ xóa" });
        }

        private void itemDuyetXoaCT_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.HoatDong.MGL.Mua.ctlManagerChoXoaCT() { Tag = "Sản phẩm cần thuê chờ xóa" });
        }

        PopupUCMThienVM.PopupUCMThienVM objPOP;

        private void itemThemKHNgoaiHeThong_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new BEE.VOIPSETUP.frmSDT();
            frm.ShowDialog();
        }

        public void VoidIPConnect()
        {
            if (db.voipServerConfigs.Count() > 0)
            {
                try
                {
                    var objConfig = db.voipServerConfigs.FirstOrDefault();
                    string host = objConfig.Host;
                    int port = Convert.ToInt32(objConfig.Port);
                    string user = objConfig.UserName;
                    string password = objConfig.Pass;
                    //   astUCM = new AsteriskUCMSdk.AsteriskUCMSdk(host, port, user, password);
                    objPOP = new PopupUCMThienVM.PopupUCMThienVM(host, port, user, password);
                    if (objPOP.xacthuc(objConfig.KeyConnect))
                    {
                        objPOP.Tick += new PopupUCMThienVM.PopupUCMThienVM.TickHandler(HeardIt);
                        objPOP.Start();
                    }
                    else
                    {
                        MessageBox.Show("Sai key kết nối tổng đài");
                    }

                    //if (astUCM.Connect() == 1)
                    //{
                    //    //DialogBox.Infomation("Kết nối dữ liệu thành công");
                    //    astUCM.NewCall += astUCM_NewCall;
                    //}
                    //else
                    //    DialogBox.Error("Kết nối dữ liệu tổng đài lỗi!");
                }
                catch
                {
                    DialogBox.Error("Vui lòng kiểm tra lại thông số kết nối tồng đài");
                }
            }
            else
            {
                DialogBox.Error("Bạn chưa cài đặt thông số kết nối cho tổng đài. \r\n Vui lòng vào danh mục cài đặt để thiết lập!");
            }
        }
        public void HeardIt(PopupUCMThienVM.PopupUCMThienVM m, PopupUCMThienVM.popup e)
        {
            using (var db = new MasterDataContext())
            {
                var objConfig = db.voipServerConfigs.FirstOrDefault();
                var objLine = db.voipLineConfigs.Where(p => p.MaNV == Common.StaffID).Select(p => p.LineNumber).ToList();
                if (objLine.Count() <= 0)
                {
                    DialogBox.Infomation(objLine.Count().ToString());
                    return;
                }
                if ((objLine.Count() > 0 && objLine.Contains(e.mayle.ToString()) == true))
                {

                    // form
                    if (e.uniqueid != Unique || e.sdt != SDT || e.uniqueid == null || e.sdt == null)
                    {
                        this.SDT = e.sdt;

                        this.Unique = e.uniqueid;
                        this.MayLe = e.mayle;

                        Thread tid1 = new Thread(new ThreadStart(PopupVoip1));
                        tid1.Start();
                        //var frC = new Call.frmCall();
                        //frC.SDT = e.sdt;
                        //frC.Unique = e.uniqueid;
                        //frC.line = e.mayle;
                        //frC.NhanVienTN = Common.HoTen;
                        //frC.LoaiCG = 0;
                        //frC.ShowDialog();
                    }


                }
            }
        }
        public void PopupVoip1()
        {
            var db = new MasterDataContext();
            var objConfig = db.voipServerConfigs.FirstOrDefault();
            var objLine = db.voipLineConfigs.Where(p => p.MaNV == Common.StaffID).Select(p => p.LineNumber).ToList();
            if (objLine.Count() <= 0)
            {
                DialogBox.Infomation("Bạn chưa cài đặt line, vui lòng kiểm tra lại.");
                return;
            }

            if ((objLine.Count() > 0 && objLine.Contains(MayLe.ToString()) == true))
            {
                var frC = new BEE.VOIPSETUP.Call.frmInCall();
                frC.SDT = this.SDT;
                frC.Unique = this.Unique;
                frC.line = this.MayLe;
                frC.NhanVienTN = Common.StaffName;
                frC.LoaiCG = 0;
                var x1 = x + 20;
                var y1 = y + 20;
                frC.Location = new Point(x1, y1);
                this.x = x1;
                this.y = y1;
                frC.ShowDialog();
            }
        }
    }
}