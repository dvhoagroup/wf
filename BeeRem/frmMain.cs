using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Runtime.InteropServices;
using System.Diagnostics;
using DevExpress.XtraBars.Alerter;
using System.Linq;
using BEE.ThuVien;
using BEE.NgonNgu;
using BEE.DuAn;

namespace LandSoft
{    
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        //private MyJumplist list;
        private const int RF_MESSAGE = 0xA125;

        protected override void WndProc(ref Message message)
        {
            //filter the RF_MESSAGE
            if (message.Msg == RF_MESSAGE)
            {
                //display that we recieved the message, of course we could do
                //something else more important here.
                //DialogBox.Infomation(string.Format("Received message Code: {0}; ActionID: {1}", message.WParam.ToInt32(), message.LParam.ToInt32()));
                Transation(message.WParam.ToInt32(), message.LParam.ToInt32());
            }
            //be sure to pass along all messages to the base also
            base.WndProc(ref message);
        }

        void Transation(int productID, int type)
        {
            if (productID <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Sản phẩm], xin cảm ơn.");
                return;
            }

            var objSP = db.bdsSanPhams.Single(p => p.MaSP == productID);
            if (type == 2 && objSP.MaTT != 2)
            {
                DialogBox.Error("[Sản phẩm] này chưa mở bán hoặc đã giao dịch. Vui lòng kiểm tra lại, xin cảm ơn.");
                return;
            }

            if (type == 4 && objSP.MaTT != 2)
            {
                DialogBox.Error("[Sản phẩm] này chưa mở bán hoặc đã giao dịch. Vui lòng kiểm tra lại, xin cảm ơn.");
                return;
            }

            if (objSP.MaTT == 2)
            {
                var obj = db.pgcPhieuGiuChos.Where(p => p.MaSP == productID).Select(p => new { p.MaTT, p.NgayKy, p.MaPGC, p.MaKGD }).OrderByDescending(p => p.NgayKy).ToList();
                if (obj.Count > 0)
                {
                    if (obj[0].MaTT == 1 | obj[0].MaTT == 3)
                    {
                        //check permission deposit
                        goto doo;
                        //DialogBox.Infomation("[Sản phẩm] này đã làm [Phiếu giữ chỗ] và đang trong tình trạng [Chờ duyệt].\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                        //return;
                    }
                    else
                    {
                        if (obj[0].MaTT == 8)
                        {
                            var objPDC = db.pdcPhieuDatCocs.Where(p => p.MaPDC == obj[0].MaPGC).Select(p => new { p.MaTT }).ToList();
                            if (objPDC.Count > 0)
                                if (objPDC[0].MaTT != 10)
                                {
                                    DialogBox.Infomation("Sản phẩm này đã làm [Phiếu đặt cọc] và đang trong tình trạng [Chờ duyệt].\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                                    return;
                                }
                        }

                        if (obj[0].MaTT == 9)
                        {
                            var objVV = db.vvbhHopDongs.Where(p => p.MaHDVV == obj[0].MaPGC).Select(p => new { p.MaTT }).ToList();
                            if (objVV.Count > 0)
                                if (objVV[0].MaTT != 9)
                                {
                                    DialogBox.Infomation("Sản phẩm này đã làm [Hợp đồng góp vốn] và đang trong tình trạng [Chờ duyệt].\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                                    return;
                                }
                        }

                        if (obj[0].MaTT == 10)
                        {
                            var objMB = db.HopDongMuaBans.Where(p => p.MaHDMB == obj[0].MaPGC).Select(p => new { p.MaTT }).ToList();
                            if (objMB.Count > 0)
                                if (objMB[0].MaTT != 9)
                                {
                                    DialogBox.Infomation("Sản phẩm này đã làm [Hợp đồng mua bán] và đang trong tình trạng [Chờ duyệt].\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                                    return;
                                }
                        }
                    }
                }
            }

        doo:
            switch (type)
            {
                case 1: //Giu cho
                    NghiepVu.GiuCho.frmEdit frmPGC = new NghiepVu.GiuCho.frmEdit();
                    frmPGC.MaSP = productID;
                    frmPGC.ShowDialog();
                    break;
                case 2: //Dat coc
                    NghiepVu.DatCoc.frmEdit frmPDC = new NghiepVu.DatCoc.frmEdit();
                    frmPDC.MaSP = productID;
                    frmPDC.ShowDialog();
                    break;
                case 3: //vay von
                    NghiepVu.VayVon.frmEdit frmVayVon = new NghiepVu.VayVon.frmEdit();
                    frmVayVon.MaSP = productID;
                    frmVayVon.ShowDialog();
                    break;
                case 4: //mua ban
                    NghiepVu.HDMB.frmEdit frmHDMB = new NghiepVu.HDMB.frmEdit();
                    frmHDMB.MaSP = productID;
                    frmHDMB.ShowDialog();
                    break;
            }
        }

        public frmMain()
        {
            InitializeComponent();

            //list = new MyJumplist(this.Handle);
        }

        public void ShowUserControlInTab(UserControl ctl)
        {
            if (BEE.NgonNgu.Language.LangID == 1)
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
            else
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
                catch {
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

        void LoadPermission()
        {
            if (Library.Common.PerID == 1)
            {
                btnPhanQuyen.Enabled = true;
                btnModules.Enabled = true;
                itemSetupSkin.Enabled = true;
            }
            else
            {
                btnPhanQuyen.Enabled = false;
                btnModules.Enabled = false;
                itemSetupSkin.Enabled = false;

                var listAction = db.ActionDatas.Where(p => p.PerID == Library.Common.PerID & p.FeatureID == 1)
                   .Select(p => p.FormID).ToList();
                var listAccess = db.AccessDatas.Where(p => p.PerID == Library.Common.PerID & p.SDBID < 6)
                       .Select(p => p.FormID).ToList();
                #region Du an
                if (listAccess.Contains(110))
                {
                    barButtonItemThemDuAn.Enabled = listAction.Contains(1);
                    navBarItemThemDuAn.Enabled = listAction.Contains(1);
                    barButtonItemQuanLyDuAn.Enabled = listAccess.Contains(1);
                    navBarItemDSDuAn.Enabled = listAccess.Contains(1);
                    //Loai du an
                    barButtonItemLoaiDuAn.Enabled = listAccess.Contains(2);

                    itemSetupMailProject.Enabled = listAccess.Contains(146);
                    itemSetupTime.Enabled = listAccess.Contains(147);
                    itemPromotion.Enabled = listAccess.Contains(148);
                }
                else
                {
                    subDuAn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    grDuAn.Visible = false;
                }
                #endregion

                #region Bat dong san
                if (listAccess.Contains(111))
                {
                    navBarItemThemBDS.Enabled = listAction.Contains(4);
                    barButtonItemThemBDS.Enabled = listAction.Contains(4);
                    navBarItemDSBDS.Enabled = listAccess.Contains(4);
                    barButtonItemQuanLyBDS.Enabled = listAccess.Contains(4);
                    //Loai BDS
                    barButtonItemLoaiBDS.Enabled = listAccess.Contains(5);
                    //Blocks
                    barButtonItemBlocks.Enabled = listAccess.Contains(6);
                    //Huong
                    barButtonItemHuong.Enabled = listAccess.Contains(7);
                    //Tinh trang BDS
                    barButtonItemTinhTrang.Enabled = listAccess.Contains(8);
                    //Khu
                    itemDuAn_Khu.Enabled = listAccess.Contains(126);
                    //Phu khu
                    itemDuAn_PhanKhu.Enabled = listAccess.Contains(127);
                }
                else
                {
                    grSanPham.Visible = false;
                    subSanPham.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                #endregion

                #region Khach hang
                if (listAccess.Contains(109))
                {
                    barButtonItemThemKhachHang.Enabled = listAction.Contains(9);
                    barButtonItemQuanLyKhachHang.Enabled = listAccess.Contains(9);
                    navBarItemThemKhachHang.Enabled = listAction.Contains(9);
                    navBarItemDSKhachHang.Enabled = listAccess.Contains(9);
                    //Danh xung
                    barButtonItemDanhXung.Enabled = listAccess.Contains(10);
                    //Thoi diem lien lac
                    barButtonItemThoiDiemLienLac.Enabled = listAccess.Contains(11);
                    //Nhom KH
                    barButtonIteNhomKhachHang.Enabled = listAccess.Contains(12);
                    //Nghe nghiep
                    barButtonItemNganhNghe.Enabled = listAccess.Contains(13);
                    //Loai hinh kinh doanh
                    barButtonItemLoaiHinhKD.Enabled = listAccess.Contains(14);
                    //Khach hàng cho duyet
                    itemDSKHChoDuyet.Enabled = listAccess.Contains(131);
                    lblCustomerNew2.Enabled = listAccess.Contains(131);
                }
                else
                {
                    grKhachHang.Visible = false;
                    subKhachHang.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                #endregion

                #region Nhan vien
                if (listAccess.Contains(107))
                {
                    barButtonItemThemNhanVien.Enabled = listAction.Contains(15);
                    navBarItemThemNhanVien.Enabled = listAction.Contains(15);
                    navBarItemDSNhanVien.Enabled = listAccess.Contains(15);
                    barButtonItemQuanLyNhanVien.Enabled = listAccess.Contains(15);
                    //Phong ban
                    barButtonItemPhongBan.Enabled = listAccess.Contains(16);
                    //Chuc vu
                    barButtonItemChucVu.Enabled = listAccess.Contains(17);
                    //Nhom KD
                    barButtonItemNhomKinhDoanh.Enabled = listAccess.Contains(18);
                }
                else
                {
                    grNhanVien.Visible = false;
                    subNhanVien.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                #endregion

                #region Nha phan phoi
                if (listAccess.Contains(108))
                {
                    btnThemNPP.Enabled = listAction.Contains(19);
                    btnDSNPP.Enabled = listAccess.Contains(19);
                    itemPhieuDangKy.Enabled = listAccess.Contains(109);
                    barButtonItemYeuCauHT.Enabled = listAccess.Contains(23);
                    navBarItemDSYeuCau.Enabled = listAccess.Contains(23);
                    itemPhieuDangKy.Enabled = listAccess.Contains(109);
                    //btnThemNPP.Enabled = listAction.Contains(19);
                    //btnDSNPP.Enabled = listAccess.Contains(20);
                    //Dai ly
                    //barButtonItemThemDaiLy.Enabled = listAction.Contains(21);
                    //barButtonItemDSDaiLy.Enabled = listAccess.Contains(21);
                    //navBarItemThemDaiLy.Enabled = listAction.Contains(21);
                    //navBarItemDSDaiLy.Enabled = listAccess.Contains(21);
                    //Hoa hong dai ly
                    //btnHoaHongDaiLy.Enabled = listAccess.Contains(22);
                    //Muc do yeu cau
                    //navBarItemThemMDYC.Enabled = listAction.Contains(22);
                    //navBarItemMucDoYeuCau.Enabled = listAccess.Contains(22);
                    ////Yeu cau ho tro
                    //navBarItemThemYeuCau.Enabled = listAction.Contains(23);
                    //navBarItemDSYeuCau.Enabled = listAccess.Contains(23);
                    lblSupport.Enabled = listAccess.Contains(23);
                    lblRegistration.Enabled = listAccess.Contains(130);
                }
                else
                {
                    subNhaPhanPhoi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                #endregion

                #region Ke hoach ban hang
                if (listAccess.Contains(118))
                    barButtonItemKeHoachBanHang.Enabled = listAccess.Contains(24);
                else
                    barButtonItemKeHoachBanHang.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                #endregion
                //Chi tieu ban hang
                //barButtonItemKyKD.Enabled = listAccess.Contains(25);

                #region HDGV
                if (listAccess.Contains(114))
                {
                    navBarItemThemHDGV.Enabled = listAction.Contains(26);
                    navBarItemDSHDGV.Enabled = listAccess.Contains(26);
                    barButtonItemThemHDGV.Enabled = listAction.Contains(26);
                    barButtonItemDSHDGV.Enabled = listAccess.Contains(26);
                    navBarItemThemHopDongGV.Enabled = listAction.Contains(26);
                    navBarItemDSHopDongGV.Enabled = listAccess.Contains(26);
                    navBBTLGV.Enabled = listAccess.Contains(105);
                    itemThanhLy_VayVon.Enabled = listAccess.Contains(105);
                }
                else
                {
                    grVayVon.Visible = false;
                    subVayVon.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                #endregion

                #region Phieu giu cho
                if (listAccess.Contains(112))
                {
                    navBarItemThemPGC.Enabled = listAction.Contains(27);
                    navBarItemDSPGC.Enabled = listAccess.Contains(27);
                    barButtonItem1.Enabled = listAccess.Contains(27);
                    barButtonItem2.Enabled = listAccess.Contains(27);
                    navBarItemThemPhieuGiuCho.Enabled = listAction.Contains(27);
                    navBarItemDSPhieuGiuCho.Enabled = listAccess.Contains(27);
                    itemThanhLy_GiuCho.Enabled = listAccess.Contains(31);
                    navBBTLPGC.Enabled = listAccess.Contains(31);
                }
                else
                {
                    grGiuCho.Visible = false;
                    subGiuCho.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                #endregion

                #region Dat coc
                if (listAccess.Contains(113))
                {
                    navBarItemThemHDDC.Enabled = listAction.Contains(28);
                    navBarItemDSHDDC.Enabled = listAccess.Contains(28);
                    barButtonItem3.Enabled = listAccess.Contains(28);
                    barButtonItem4.Enabled = listAccess.Contains(28);
                    navBarItemThemHopDongDC.Enabled = listAction.Contains(28);
                    navBarItemDSHopDongDC.Enabled = listAccess.Contains(28);
                    navBBTLPDC.Enabled = listAccess.Contains(104);
                    itemThanhLy_DatCoc.Enabled = listAccess.Contains(104);

                    //Chuyen nhuong coc
                    btnThemCNCoc.Enabled = listAction.Contains(65);
                    btnThemChuyenNhuongCoc.Enabled = listAction.Contains(65);
                    btnDSCNCoc.Enabled = listAccess.Contains(65);
                    btnDSChuyenNhuongCoc.Enabled = listAccess.Contains(65);
                }
                else
                {
                    grDatCoc.Visible = false;
                    subDatCoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                #endregion

                #region HDMB
                if (listAccess.Contains(115))
                {
                    navBarItemThemHDMB.Enabled = listAction.Contains(29);
                    navBarItemDSHDMB.Enabled = listAccess.Contains(29);
                    barButtonItemThemHDMB.Enabled = listAction.Contains(29);
                    barButtonItemDSHDMB.Enabled = listAccess.Contains(29);
                    navBarItemThemHopDongMB.Enabled = listAction.Contains(29);
                    navBarItemDSHopDongMB.Enabled = listAccess.Contains(29);
                    navBBTLMB.Enabled = listAccess.Contains(106);
                    itemThanhLy_MuaBan.Enabled = listAccess.Contains(106);
                    navBienBanBanGiao.Enabled = itemBanGiao.Enabled = listAccess.Contains(30);
                }
                else
                {
                    grMuaBan.Visible = false;
                    subMuaBan.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                #endregion

                //Phieu ky gui
                navBarItemThemHDKG.Enabled = listAction.Contains(30);
                navBarItemDSHDKG.Enabled = listAccess.Contains(30);
                barButtonItemThemKG.Enabled = listAction.Contains(30);
                barButtonItemDSKG.Enabled = listAccess.Contains(30);
                navBarItemThemPhieuKyGui.Enabled = listAction.Contains(30);
                navBarItemDSPhieuKyGui.Enabled = listAccess.Contains(30);

                #region Chuyen nhuong
                if (listAccess.Contains(116))
                {
                    barChuyenNhuong_Add.Enabled = listAction.Contains(32);
                    itemChuyenNhuong_Add.Enabled = barChuyenNhuong_Add.Enabled;
                    barChuyenNhuong.Enabled = listAccess.Contains(32);
                    itemChuyenNhuong.Enabled = barChuyenNhuong.Enabled;
                }
                else
                {
                    grChuyenNhuong.Visible = false;
                    subChuyenNhuong.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                #endregion

                #region Quy
                if (listAccess.Contains(117))
                {
                    //Phieu thu
                    navBarItemThemPhieuThu.Enabled = listAction.Contains(33);
                    navBarItemDSPhieuThu.Enabled = listAccess.Contains(33);
                    barButtonItemPhieuThu1.Enabled = listAccess.Contains(33);
                    //Phieu chi
                    navBarItemThemPhieuChi.Enabled = listAction.Contains(34);
                    navBarItemDSPhieuChi.Enabled = listAccess.Contains(34);
                    barButtonItemPhieuChi.Enabled = listAccess.Contains(34);
                }
                else
                {
                    grQuy.Visible = false;
                    subQuy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                #endregion

                #region Khac
                if (listAccess.Contains(119))
                {
                    //Ngan hang
                    barButtonItemNganHang.Enabled = listAccess.Contains(35);
                    //Loai tien
                    barButtonItemLoaiTien.Enabled = listAccess.Contains(36);
                    //Tai khoan
                    btnHeThongTaiKhoan.Enabled = listAccess.Contains(37);
                    //Dia diem
                    btnDiaDiem.Enabled = listAccess.Contains(38);
                    //Cai dat quy tac
                    btnCaiDatQuyTac.Enabled = listAccess.Contains(44);
                    //Cai dat giu cho
                    btnCaiDatGiuCho.Enabled = listAccess.Contains(45);
                    //Cai dat dat coc
                    btnCaiDatDatCoc.Enabled = listAccess.Contains(46);
                    //Bieu mau
                    barSubItemBieuMau.Enabled = listAccess.Contains(47);
                }
                else
                    subKhac.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                #endregion

                #region Thong ke
                if (listAccess.Contains(122))
                {
                    itemReport_SanPhamTon.Enabled = listAccess.Contains(54);
                    itemReport_PhanTichTuoiNo.Enabled = listAccess.Contains(55);
                    itemBaoCao_CongNoTongHop.Enabled = listAccess.Contains(56);
                    subBieuDo.Enabled = listAccess.Contains(59);
                    itemBCLichSuGD.Enabled = listAccess.Contains(52);
                    itemDoanhSoBanHang.Enabled = listAccess.Contains(53);
                    itemTonKho.Enabled = listAccess.Contains(60);
                    itemBCKHDongChuaDu.Enabled = listAccess.Contains(58);
                }
                else
                    subThongKe.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                #endregion

                #region Lich lam viec
                if (listAccess.Contains(123))
                {
                    itemLLV_NhiemVuThem.Enabled = listAction.Contains(66);
                    itemLLV_NhiemVu.Enabled = listAccess.Contains(66);
                    itemLLV_LichHenThem.Enabled = listAction.Contains(67);
                    itemLLV_LichHen.Enabled = listAccess.Contains(67);
                    itemLLV_LichHen_DangLuoi.Enabled = listAccess.Contains(67);
                    itemLLV_LoaiNhiemVu.Enabled = listAccess.Contains(68);
                    itemLLV_TrangThaiNhiemVu.Enabled = listAccess.Contains(69);
                    itemLLV_MucDoNhiemVu.Enabled = listAccess.Contains(70);
                    itemLLV_TienDo.Enabled = listAccess.Contains(71);
                    itemLLV_LoaiLichHen.Enabled = listAccess.Contains(72);
                    itemLLV_ThoiDiemLichHen.Enabled = listAccess.Contains(73);
                }
                else
                {
                    subLichLamViec.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                #endregion

                #region Giao dich moi gioi
                if (listAccess.Contains(124))
                {
                    //San pham ban, cho thue
                    itemMGLSP_Them.Enabled = listAccess.Contains(83);
                    navBarItemThemPhieuGiaoDich.Enabled = listAction.Contains(83);
                    itemMGLSP.Enabled = listAccess.Contains(83);
                    navBarItemDSPhieuGD.Enabled = listAccess.Contains(83);
                    //Nhu cau mua, thue
                    itemMGL_DangKyMuaThue.Enabled = listAction.Contains(84);
                    barMGL_DangKyMuaThue.Enabled = listAction.Contains(84);
                    itemMGL_MuaThue.Enabled = listAccess.Contains(84);
                    barMGL_MuaThue.Enabled = listAccess.Contains(84);
                    //Giao dich moi gioi
                    itemMGL_GiaoDich.Enabled = listAccess.Contains(85);
                    barMGL_GiaoDich.Enabled = listAccess.Contains(85);
                    //Cap do
                    itemMGL_CapDo.Enabled = listAccess.Contains(96);
                    //Nguon
                    itemMGL_Nguon.Enabled = listAccess.Contains(97);
                    //Bieu mau
                    itemMGL_BieuMau.Enabled = listAccess.Contains(98);
                }
                else
                {
                    subMoiGioi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    grMoiGioi.Visible = false;
                }
                #endregion

                #region Marketing
                if (listAccess.Contains(121))
                {
                    //SMS Marketing
                    itemSMS_SendList.Enabled = listAccess.Contains(86);
                    itemSMS_ListReceived.Enabled = listAccess.Contains(87);
                    itemSMS_Template.Enabled = listAccess.Contains(88);
                    itemSMS_Category.Enabled = listAccess.Contains(89);
                    itemSetupHBBDSMS.Enabled = listAccess.Contains(102);
                    //itemSMS_Manager.Enabled = listAccess.Contains(90);
                    //Mail Marketing
                    itemMail_Sending.Enabled = listAccess.Contains(91);
                    itemMail_Receives.Enabled = listAccess.Contains(92);
                    itemMail_Template.Enabled = listAccess.Contains(93);
                    itemMail_Category.Enabled = listAccess.Contains(94);
                    itemMail_Config.Enabled = listAccess.Contains(95);
                    itemSetupHBBDMail.Enabled = listAccess.Contains(103);

                    itemSinhNhatKH.Enabled = listAccess.Contains(101);
                    itemSearch.Enabled = listAccess.Contains(39);
                }
                else
                {
                    subMarketing.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                #endregion

                #region Tai lieu
                if (listAccess.Contains(125))
                {
                    navThemTaiLieu.Enabled = listAction.Contains(99);
                    navDSTaiLieu.Enabled = listAccess.Contains(99);
                    navDSLoaiTaiLieu.Enabled = listAccess.Contains(100);
                }
                else
                    grTaiLieu.Visible = false;
                #endregion

                #region Ban giao
                if (listAccess.Contains(30))
                {
                    navThongBaoBG.Enabled = listAccess.Contains(137);
                    itemDSThongBaoBG.Enabled = listAccess.Contains(137);

                    navBienBanBanGiao.Enabled = listAccess.Contains(138);
                    itemBanGiao.Enabled = listAccess.Contains(138);

                    itemNghiemThuKH.Enabled = listAccess.Contains(139);
                    navNghiemThuKH.Enabled = listAccess.Contains(139);

                    itemNghiemThuNB.Enabled = listAccess.Contains(140);
                    navNghiemThuNB.Enabled = listAccess.Contains(140);
                }
                else
                {
                    grBanGiao.Visible = false;
                    subBanGiao.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                #endregion

                #region Company
                if (listAccess.Contains(143))
                {
                    itemAddCompany.Enabled = listAction.Contains(137);
                    itemListCompany.Enabled = listAccess.Contains(137);
                }
                else
                {
                    subCompany.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                #endregion

                #region Cong no
                if (listAccess.Contains(120))
                {
                    //itemTongHopCongNo.Enabled = listAccess.Contains(165);
                    btnCongNoKHMoi.Enabled = listAccess.Contains(41);
                    itemLSGiaHan.Enabled = listAccess.Contains(129);
                    itemCongNo_ThongBaoNopTien.Enabled = listAccess.Contains(159);
                    itemCongNo_ThanhLy.Enabled = listAccess.Contains(160);
                    itemSetupSMS.Enabled = listAccess.Contains(162);
                    itemSetupMail.Enabled = listAccess.Contains(161);
                }
                else
                {
                    subCongNo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                #endregion
            }
        }

        void User_UpdateStatus(byte maTT)
        {
            try
            {
                db.NhanViens.Single(p => p.MaNV == Library.Common.StaffID).MaTT = maTT;
                db.SubmitChanges();
            }
            catch { }
        }

        void User_ReLogin()
        {
            this.Hide();
            Library.Common.StaffID = 0;

            HeThong.Login_frm frmLogin = new HeThong.Login_frm();
            frmLogin.ShowDialog();

            if (frmLogin.DialogResult == DialogResult.OK)
            {
                this.frmMain_Load(this, new EventArgs());
                this.Show();
            }
            else
            {
                this.Close();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            var wait = DialogBox.WaitingForm();

            if (BEE.NgonNgu.Language.LangID != 1)
            {
                this.Text = "BECAMEX TOKYU - Software Solution for Real Estate Business Management";

                lblNguoiDung.Caption = ("User: " + Library.Common.StaffName).ToUpper();
                lblQuyTruyCap.Caption = ("Permission Access: " + Library.Common.PerName).ToUpper();
            }
            else
            {
                lblNguoiDung.Caption = ("Người dùng: " + Library.Common.StaffName).ToUpper();
                lblQuyTruyCap.Caption = ("Quyền truy cập: " + Library.Common.PerName).ToUpper();

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

            itemCopyright.EditValue = Properties.Resources.beesky;
            User_UpdateStatus(2);

            LoadPermission();
                        
            defaultLookAndFeel1.LookAndFeel.SkinName = Library.Common.Skins;

            ShowUserControlInTab(new View_ctl());

            var frmChat = new Chat.frmMessOffline();
            if (frmChat.MessCount > 0)
                frmChat.Show(this);
            else
                frmChat.Dispose();

            timerNhacViec.Start();
            timerAlert.Start();
            //timerCheckOnline.Start();
            //timerNewChat.Start();
            timer1.Interval = 30000;
            timer1.Start();

            itemCopyright.EditValue = Properties.Resources.beesky;
            itemNew.EditValue = Properties.Resources.icon_new;
            itemNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            itemNew2.EditValue = Properties.Resources.icon_new;
            itemNew2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            itemNew3.EditValue = Properties.Resources.icon_new;
            itemNew3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            if (Library.Common.PerID == 1)
                timerRequest.Start();

            //if (it.CommonCls.GiaiMa(Properties.Settings.Default.Password) == "Admin")
            //{
            //    itemBCBieuThamChieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //    itemBCCongNoDaThu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //    itemBCConNoXau.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //    itemBCKetQuaBanHang.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //    itemBCKQBanTungBen.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //    itemBCTongKetThuHoi.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //}
            //else
            //{
                itemBCBieuThamChieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemBCCongNoDaThu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemBCConNoXau.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemBCKetQuaBanHang.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemBCKQBanTungBen.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemBCTongKetThuHoi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //}
                        
            BEE.NgonNgu.Language.TranslateControl(this, barManager1);

            wait.Close();
            wait.Dispose();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            User_UpdateStatus(1);
        }

        private void navBarControl1_NavPaneStateChanged(object sender, EventArgs e)
        {
            if (navBarControl1.OptionsNavPane.NavPaneState == DevExpress.XtraNavBar.NavPaneState.Collapsed)
                splitContainerControl1.SplitterPosition = 35;
            else
                splitContainerControl1.SplitterPosition = 200;
        }

        private void barButtonItemThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void barButtonItemQuanLyKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BEE.KhachHang.KhachHang_ctl ctl = new BEE.KhachHang.KhachHang_ctl();
            ctl.Tag = "Khách hàng";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemNganhNghe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BEE.KhachHang.NgheNghiep_ctl ctl = new BEE.KhachHang.NgheNghiep_ctl();
            ctl.Tag = "Nghề nghiệp";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemDanhXung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KhachHang.DanhXung_ctl ctl = new BEE.KhachHang.DanhXung_ctl();
            ctl.Tag = "Danh xưng";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemThoiDiemLienLac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KhachHang.ThoiDiemLienLac_ctl ctl = new BEE.KhachHang.ThoiDiemLienLac_ctl();
            ctl.Tag = "Thời điểm liên lạc";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemChucVu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NhanVien.ChucVu_ctl ctl = new BEE.NhanVien.ChucVu_ctl();
            ctl.Tag = "Chức vụ";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemPhongBan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NhanVien.PhongBan_ctl ctl = new BEE.NhanVien.PhongBan_ctl();
            ctl.Tag = "Phòng ban";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemHuong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BEE.BatDongSan.Huong_ctl ctl = new BEE.BatDongSan.Huong_ctl();
            ctl.Tag = "Hướng";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemLoaiBDS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BEE.BatDongSan.LoadiBDS_ctl ctl = new BEE.BatDongSan.LoadiBDS_ctl();
            ctl.Tag = "Loại BĐS";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemTinhTrang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new BEE.SanPham.frmTrangThai();
            frm.ShowDialog();
        }

        private void barButtonItemLoaiDuAn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadiDA_ctl ctl = new BEE.DuAn.LoadiDA_ctl();
            ctl.Tag = "DS Loại dự án";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemNganHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ctl = new LandSoft.Bank.ctlManager();
            ctl.Tag = "Ngân hàng";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemLoaiTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BEE.NghiepVuKhac.LoaiTien_ctl ctl = new BEE.NghiepVuKhac.LoaiTien_ctl();
            ctl.Tag = "Loại tiền";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemTinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.Tinh_ctl ctl = new BEE.NghiepVuKhac.Tinh_ctl();
            ctl.Tag = "Tỉnh (TP)";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemHuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.Huyen_ctl ctl = new BEE.NghiepVuKhac.Huyen_ctl();
            ctl.Tag = "Quận (huyện)";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemXa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.Xa_ctl ctl = new BEE.NghiepVuKhac.Xa_ctl();
            ctl.Tag = "Xã (Phường)";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemBlue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            defaultLookAndFeel1.LookAndFeel.SkinName = e.Item.Caption;
            Library.Common.Skins = e.Item.Caption;
            Properties.Settings.Default.Save();
        }

        private void barButtonItemCaramel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            defaultLookAndFeel1.LookAndFeel.SkinName = e.Item.Caption;
            Library.Common.Skins = e.Item.Caption;
            Properties.Settings.Default.Save();
        }

        private void barButtonItemMoneyTwins_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            defaultLookAndFeel1.LookAndFeel.SkinName = e.Item.Caption;
            Library.Common.Skins = e.Item.Caption;
            Properties.Settings.Default.Save();
        }

        private void barButtonItemTheAsphaltWorld_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            defaultLookAndFeel1.LookAndFeel.SkinName = e.Item.Caption;
            Library.Common.Skins = e.Item.Caption;
            Properties.Settings.Default.Save();
        }

        private void barButtonItemLilian_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            defaultLookAndFeel1.LookAndFeel.SkinName = e.Item.Caption;
            Library.Common.Skins = e.Item.Caption;
            Properties.Settings.Default.Save();
        }

        private void barButtonItemiMaginary_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            defaultLookAndFeel1.LookAndFeel.SkinName = e.Item.Caption;
            Library.Common.Skins = e.Item.Caption;
            Properties.Settings.Default.Save();
        }

        private void barButtonItemBlack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            defaultLookAndFeel1.LookAndFeel.SkinName = e.Item.Caption;
            Library.Common.Skins = e.Item.Caption;
            Properties.Settings.Default.Save();
        }

        private void barButtonIteNhomKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KhachHang.NhomKH_ctl ctl = new BEE.KhachHang.NhomKH_ctl();
            ctl.Tag = "Nhóm khách hàng";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemNhomKinhDoanh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NhanVien.NhomKD_ctl ctl = new BEE.NhanVien.NhomKD_ctl();
            ctl.Tag = "Nhóm kinh doanh";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemDangNhapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            User_ReLogin();
        }

        private void barButtonItemDoiMatKhau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            HeThong.ChangePassword_frm frm = new LandSoft.HeThong.ChangePassword_frm();
            frm.ShowDialog();
        }

        private void barButtonItemCapNhatTaiKhoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItemValentine_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            defaultLookAndFeel1.LookAndFeel.SkinName = e.Item.Caption;
            Library.Common.Skins = e.Item.Caption;
            Properties.Settings.Default.Save();
        }

        private void barButtonItemXmas2008Blue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            defaultLookAndFeel1.LookAndFeel.SkinName = e.Item.Caption;
            Library.Common.Skins = e.Item.Caption;
            Properties.Settings.Default.Save();
        }

        private void barButtonItemWin7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            defaultLookAndFeel1.LookAndFeel.SkinName = e.Item.Caption;
            Library.Common.Skins = e.Item.Caption;
            Properties.Settings.Default.Save();
        }

        private void barButtonItemLiquidSky_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            defaultLookAndFeel1.LookAndFeel.SkinName = e.Item.Caption;
            Library.Common.Skins = e.Item.Caption;
            Properties.Settings.Default.Save();
        }

        private void barButtonItemDevExpressStyle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            defaultLookAndFeel1.LookAndFeel.SkinName = "DevExpress Style";
            Library.Common.Skins = "DevExpress Style";
            Properties.Settings.Default.Save();
        }

        private void navBarControl1_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            if (navBarControl1.Groups[0].Expanded)
            {
                ShowUserControlInTab(new View_ctl());
            }
        }

        private void barButtonItemThemDuAn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DuAn.DuAn_frm frm = new BEE.DuAn.DuAn_frm();
            frm.ShowDialog();

            DuAn.DuAn_ctl ctl = new BEE.DuAn.DuAn_ctl();
            ctl.Tag = "Dự án";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemQuanLyDuAn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DuAn.DuAn_ctl ctl = new BEE.DuAn.DuAn_ctl();
            ctl.Tag = "Dự án";
            ShowUserControlInTab(ctl);
        }

        private void navBarItemThemDuAn_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DuAn.DuAn_frm frm = new BEE.DuAn.DuAn_frm();
            frm.ShowDialog();

            DuAn.DuAn_ctl ctl = new BEE.DuAn.DuAn_ctl();
            ctl.Tag = "Dự án";
            ShowUserControlInTab(ctl);
        }

        private void navBarItemDSDuAn_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DuAn.DuAn_ctl ctl = new BEE.DuAn.DuAn_ctl();
            ctl.Tag = "Dự án";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemMcSkin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            defaultLookAndFeel1.LookAndFeel.SkinName = e.Item.Caption;
            Library.Common.Skins = e.Item.Caption;
            Properties.Settings.Default.Save();
        }

        private void barButtonItemBDSMoBan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //BDS.MoBanBDS_ctl ctl = new BEE.BatDongSan.MoBanBDS_ctl();
            //ctl.Tag = "Theo dỏi bán hàng";
            //ShowUserControlInTab(ctl);
        }

        private void barButtonItemQuanLyBDS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.SanPham.ctlManagerNew());
            //BDS.BDS_ctl ctl = new BEE.BatDongSan.BDS_ctl();
            //ctl.Tag = "Bất động sản";
            //ShowUserControlInTab(ctl);
        }

        private void barButtonItemThemBDS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SanPham.frmEditV2 frm = new SanPham.frmEditV2();
            frm.ShowDialog();
            //BDS.BDS_frm frm = new BEE.BatDongSan.BDS_frm();
            //frm.ShowDialog();

            //BDS.BDS_ctl ctl = new BEE.BatDongSan.BDS_ctl();
            //ctl.Tag = "Bất động sản";
            //ShowUserControlInTab(ctl);
        }

        private void barButtonItemBlocks_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DuAn.Blocks_ctl ctl = new BEE.DuAn.Blocks_ctl();
            ctl.Tag = "Block";
            ShowUserControlInTab(ctl);
        }

        private void navBarItemThemBDS_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SanPham.frmEdit frm = new SanPham.frmEdit();
            frm.ShowDialog();
            if (frm.IsSave)
                ShowUserControlInTab(new SanPham.ctlManagerNew());
        }

        private void navBarItemDSBDS_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new SanPham.ctlManagerNew());
        }

        private void navBarItemDSKhachHang_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            KhachHang.KhachHang_ctl ctl = new BEE.KhachHang.KhachHang_ctl();
            ctl.Tag = "Khách hàng";
            ShowUserControlInTab(ctl);
        }

        private void navBarItemDSNhanVien_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NhanVien.NhanVien_ctl ctl = new BEE.NhanVien.NhanVien_ctl();
            ctl.Tag = "Nhân viên";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemKeHoachBanHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KeHoach.ctlManager ctl = new LandSoft.KeHoach.ctlManager();
            ctl.Tag = "Quyết định mở bán";
            ShowUserControlInTab(ctl);
        }

        private void navBarItemDSPGC_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.GiuCho.ctlManager());
        }

        private void navBarItemThemPGC_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NghiepVu.GiuCho.frmEdit frm = new BEE.GiuCho.frmEdit();
            frm.ShowDialog();
            if (frm.IsSave)
                ShowUserControlInTab(new NghiepVu.GiuCho.ctlManager());
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NghiepVu.GiuCho.frmEdit frm = new BEE.GiuCho.frmEdit();
            frm.ShowDialog();
            if (frm.IsSave)
                ShowUserControlInTab(new NghiepVu.GiuCho.ctlManager());
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.GiuCho.ctlManager());

            //GiuCho.PhieuGiuCho_ctl ctl = new LandSoft.GiuCho.PhieuGiuCho_ctl();
            //ctl.Tag = "Phiếu đặt chỗ";
            //ShowUserControlInTab(ctl);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NghiepVu.DatCoc.frmEdit frm = new BEE.DatCoc.frmEdit();
            frm.ShowDialog();
            if (frm.IsSave)
                ShowUserControlInTab(new NghiepVu.DatCoc.ctlManager());       
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.DatCoc.ctlManager());
            //DatCoc.PhieuDatCoc_ctl ctl = new LandSoft.DatCoc.PhieuDatCoc_ctl();
            //ctl.Tag = "Phiếu đặt cọc";
            //ShowUserControlInTab(ctl);
        }

        private void navBarItemThemHDDC_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NghiepVu.DatCoc.frmEdit frm = new BEE.DatCoc.frmEdit();
            frm.ShowDialog();
            if (frm.IsSave)
                ShowUserControlInTab(new NghiepVu.DatCoc.ctlManager());  
        }

        private void navBarItemDSHDDC_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.DatCoc.ctlManager());  
        }

        private void navBarItemDSPhieuThu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new BEE.SoQuy.PhieuThuBanHang.ctlManager());
        }

        private void navBarItemThemPhieuThu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //NghiepVu.Quy.PhieuThu_ctl ctl = new BEE.SoQuy.PhieuThu_ctl();
            //ctl.Tag = "Phiếu thu";
            //ShowUserControlInTab(ctl);
        }

        private void navBarItemDSPhieuChi_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new BEE.SoQuy.PhieuChiBanHang.ctlManager());
        }

        private void barButtonItemDSDaiLy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DaiLy.DaiLy_ctl ctl = new LandSoft.DaiLy.DaiLy_ctl();
            ctl.Tag = "Đại lý";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemThemDaiLy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DaiLy.DaiLy_frm frm = new LandSoft.DaiLy.DaiLy_frm();
            frm.ShowDialog();

            DaiLy.DaiLy_ctl ctl = new LandSoft.DaiLy.DaiLy_ctl();
            ctl.Tag = "Đại lý";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemKyKD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BEE.NghiepVuKhac.ChiTieuBanHang_ctl ctl = new BEE.NghiepVuKhac.ChiTieuBanHang_ctl();
            ctl.Tag = "Chi tiêu bán hàng";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemDangKy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DaiLy.DangKyDoanhSo_ctl ctl = new LandSoft.DaiLy.DangKyDoanhSo_ctl();
            ctl.Tag = "Đăng ký doanh số";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemDSDangKy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DaiLy.DangKyDoanhSo_ctl ctl = new LandSoft.DaiLy.DangKyDoanhSo_ctl();
            ctl.Tag = "Đăng ký doanh số";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemMucDoYeuCau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DaiLy.MucDoYeuCau_frm frm = new LandSoft.DaiLy.MucDoYeuCau_frm();
            frm.ShowDialog();

            DaiLy.MucDoYeuCau_ctl ctl = new LandSoft.DaiLy.MucDoYeuCau_ctl();
            ctl.Tag = "Mức độ yêu cầu";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemYeuCauHT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new Agents.ctlSupport());
        }

        private void barButtonItemPhieuThu1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //NghiepVu.Quy.PhieuThu_ctl ctl = new BEE.SoQuy.PhieuThu_ctl();
            //ctl.Tag = "Phiếu thu";
            //ShowUserControlInTab(ctl);
        }

        private void barButtonItemPhieuChi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //NghiepVu.Quy.PhieuChi_ctl ctl = new BEE.SoQuy.PhieuChi_ctl();
            //ctl.Tag = "Phiếu chi";
            //ShowUserControlInTab(ctl);
        }

        private void navBarItemThemDaiLy_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DaiLy.DaiLy_frm frm = new LandSoft.DaiLy.DaiLy_frm();
            frm.ShowDialog();

            DaiLy.DaiLy_ctl ctl = new LandSoft.DaiLy.DaiLy_ctl();
            ctl.Tag = "Đại lý";
            ShowUserControlInTab(ctl);
        }

        private void navBarItemThemMDYC_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DaiLy.MucDoYeuCau_frm frm = new LandSoft.DaiLy.MucDoYeuCau_frm();
            frm.ShowDialog();

            DaiLy.MucDoYeuCau_ctl ctl = new LandSoft.DaiLy.MucDoYeuCau_ctl();
            ctl.Tag = "Mức độ yêu cầu";
            ShowUserControlInTab(ctl);
        }

        private void navBarItemDSDaiLy_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DaiLy.DaiLy_ctl ctl = new LandSoft.DaiLy.DaiLy_ctl();
            ctl.Tag = "Đại lý";
            ShowUserControlInTab(ctl);
        }

        private void navBarItemMucDoYeuCau_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DaiLy.MucDoYeuCau_ctl ctl = new LandSoft.DaiLy.MucDoYeuCau_ctl();
            ctl.Tag = "Mức độ yêu cầu";
            ShowUserControlInTab(ctl);
        }

        private void navBarItemDSYeuCau_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DaiLy.YeuCauHoTro_ctl ctl = new LandSoft.DaiLy.YeuCauHoTro_ctl();
            ctl.Tag = "Yêu cầu hỗ trợ";
            ShowUserControlInTab(ctl);
        }

        private void navBarItemThemYeuCau_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DaiLy.YeuCauHoTro_frm frm = new LandSoft.DaiLy.YeuCauHoTro_frm();
            frm.ShowDialog();

            DaiLy.YeuCauHoTro_ctl ctl = new LandSoft.DaiLy.YeuCauHoTro_ctl();
            ctl.Tag = "Yêu cầu hỗ trợ";
            ShowUserControlInTab(ctl);
        }

        private void navBarItemThemHDMB_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NghiepVu.HDMB.frmEdit frm = new BEE.HopDong.HDMB.frmEdit();
            frm.ShowDialog();
            if (frm.IsSave)
                ShowUserControlInTab(new NghiepVu.HDMB.ctlManager());
        }

        private void navBarItemDSHDMB_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.HDMB.ctlManager());
        }

        private void barButtonItemThemHDMB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NghiepVu.HDMB.frmEdit frm = new BEE.HopDong.HDMB.frmEdit();
            frm.ShowDialog();
            if (frm.IsSave)
                ShowUserControlInTab(new NghiepVu.HDMB.ctlManager());      
        }

        private void barButtonItemDSHDMB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.HDMB.ctlManager());
            //HDMB.HopDongMuaBan_ctl ctl = new LandSoft.HDMB.HopDongMuaBan_ctl();
            //ctl.Tag = "Hợp đồng mua bán";
            //ShowUserControlInTab(ctl);
        }

        private void barButtonItemPhieuGiuCho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.Editor_frm frm = new BEE.NghiepVuKhac.Editor_frm();
            frm.MaBM = 1;
            frm.ShowDialog();
        }

        private void barButtonItemHĐatCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.Editor_frm frm = new BEE.NghiepVuKhac.Editor_frm();
            frm.MaBM = 2;
            frm.ShowDialog();
        }

        private void barButtonItemHDMuaBan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.Editor_frm frm = new BEE.NghiepVuKhac.Editor_frm();
            frm.MaBM = 3;
            frm.ShowDialog();
        }

        private void barButtonItemLoaiHinhKD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KhachHang.LoaiHinhKD_ctl ctl = new BEE.KhachHang.LoaiHinhKD_ctl();
            ctl.Tag = "Loại hình kinh doanh";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemThemKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KhachHang.KhachHang_frm frm = new BEE.KhachHang.KhachHang_frm();
            frm.ShowDialog();

            //KhachHang.KhachHang_ctl ctl = new BEE.KhachHang.KhachHang_ctl();
            //ctl.Tag = "Khách hàng";
            //ShowUserControlInTab(ctl);
        }

        private void navBarItemDSHDKG_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NghiepVu.KyGui.KyGui_ctl ctl = new BEE.HoatDong.KyGui.KyGui_ctl();
            ctl.Tag = "Ký gửi";
            ShowUserControlInTab(ctl);
        }

        private void navBarItemThemHDKG_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NghiepVu.KyGui.KyGui_frm frm = new BEE.HoatDong.KyGui.KyGui_frm();
            frm.ShowDialog();

            NghiepVu.KyGui.KyGui_ctl ctl = new BEE.HoatDong.KyGui.KyGui_ctl();
            ctl.Tag = "Ký gửi";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemThemKG_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NghiepVu.KyGui.KyGui_ctl ctl = new BEE.HoatDong.KyGui.KyGui_ctl();
            ctl.Tag = "Ký gửi";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemDSKG_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NghiepVu.KyGui.KyGui_ctl ctl = new BEE.HoatDong.KyGui.KyGui_ctl();
            ctl.Tag = "Ký gửi";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemHieuKyGui_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.Editor_frm frm = new BEE.NghiepVuKhac.Editor_frm();
            frm.MaBM = 6;
            frm.ShowDialog();
        }

        private void navBarItemThemHopDongGV_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //NghiepVu.HDGopVon.HopDongGopVon_ctl ctl = new BEE.CongCu.HDGopVon.HopDongGopVon_ctl();
            //ctl.Tag = "Hợp đồng góp vốn";
            //ShowUserControlInTab(ctl);
        }

        private void navBarItemThemPhieuGiuCho_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NghiepVu.GiuCho.frmEdit frm = new BEE.GiuCho.frmEdit();
            frm.ShowDialog();
            if (frm.IsSave)
                ShowUserControlInTab(new NghiepVu.GiuCho.ctlManager());
        }

        private void navBarItemThemHopDongDC_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NghiepVu.DatCoc.frmEdit frm = new BEE.DatCoc.frmEdit();
            frm.ShowDialog();
            if (frm.IsSave)
                ShowUserControlInTab(new NghiepVu.DatCoc.ctlManager());
        }

        private void navBarItemThemHopDongMB_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NghiepVu.HDMB.frmEdit frm = new BEE.HopDong.HDMB.frmEdit();
            frm.ShowDialog();
            if (frm.IsSave)
                ShowUserControlInTab(new NghiepVu.HDMB.ctlManager());
        }

        private void navBarItemThemPhieuKyGui_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NghiepVu.KyGui.KyGui_frm frm = new BEE.HoatDong.KyGui.KyGui_frm();
            frm.ShowDialog();

            NghiepVu.KyGui.KyGui_ctl ctl = new BEE.HoatDong.KyGui.KyGui_ctl();
            ctl.Tag = "Ký gửi";
            ShowUserControlInTab(ctl);
        }

        private void navBarItemDSPhieuGiuCho_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.GiuCho.ctlManager());
        }

        private void navBarItemDSHopDongDC_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.DatCoc.ctlManager());
        }

        private void navBarItemDSHopDongMB_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.HDMB.ctlManager());
        }

        private void navBarItemDSPhieuKyGui_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NghiepVu.KyGui.KyGui_ctl ctl = new BEE.HoatDong.KyGui.KyGui_ctl();
            ctl.Tag = "Ký gửi";
            ShowUserControlInTab(ctl);
        }
        
        private void barButtonItemQuanLyNhanVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NhanVien.NhanVien_ctl ctl = new BEE.NhanVien.NhanVien_ctl();
            ctl.Tag = "Nhân viên";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemThemNhanVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NhanVien.NhanVien_frm frm = new BEE.NhanVien.NhanVien_frm();
            frm.ShowDialog();

            NhanVien.NhanVien_ctl ctl = new BEE.NhanVien.NhanVien_ctl();
            ctl.Tag = "Nhân viên";
            ShowUserControlInTab(ctl);
        }

        private void navBarItemThemNhanVien_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NhanVien.NhanVien_frm frm = new BEE.NhanVien.NhanVien_frm();
            frm.ShowDialog();

            NhanVien.NhanVien_ctl ctl = new BEE.NhanVien.NhanVien_ctl();
            ctl.Tag = "Nhân viên";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItemXacNHanChuyenNhuong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.Editor_frm frm = new BEE.NghiepVuKhac.Editor_frm();
            frm.MaBM = 7;
            frm.ShowDialog();
        }

        private void barButtonItemThemXacNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new NghiepVu.ChuyenNhuong.frmEdit())
            {
                frm.ShowDialog();
                if (frm.IsSave)
                    ShowUserControlInTab(new NghiepVu.ChuyenNhuong.ctlManager());
            }
        }

        private void barButtonItemDSXacNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BEE.HoatDong.XNCN.XacNhanChuyenNhuong_ctl ctl = new BEE.HoatDong.XNCN.XacNhanChuyenNhuong_ctl();
            ctl.Tag = "Xác nhận chuyển nhượng";
            ShowUserControlInTab(ctl);
        }

        private void navBarItemDSHopDongGV_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.VayVon.ctlManager());
        }

        private void barButtonItemThemHDGV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new NghiepVu.VayVon.frmEdit();
            frm.ShowDialog();
            if (frm.IsSave)
                ShowUserControlInTab(new NghiepVu.VayVon.ctlManager());
        }

        private void barButtonItemDSHDGV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.VayVon.ctlManager());
        }

        private void btnThuXacNhanDongTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.Editor_frm frm = new BEE.NghiepVuKhac.Editor_frm();
            frm.MaBM = 8;
            frm.ShowDialog();
        }

        private void navBarItemDSHopDongCN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.ChuyenNhuong.ctlManager());
        }

        private void navBarItemDSHDGV_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.VayVon.ctlManager());
        }

        private void navBarItemThemHDGV_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var frm = new NghiepVu.VayVon.frmEdit();
            frm.ShowDialog();
            if (frm.IsSave)
                ShowUserControlInTab(new NghiepVu.VayVon.ctlManager());
        }

        private void btnHeThongTaiKhoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.TaiKhoan_ctl ctl = new BEE.NghiepVuKhac.TaiKhoan_ctl();
            ctl.Tag = "Tài khoản";
            ShowUserControlInTab(ctl);
        }

        private void btnTimKiemKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NghiepVu.Maketing.SearchCustomer_ctl ctl = new BEE.HoatDong.Maketing.SearchCustomer_ctl();
            ctl.Tag = "Tìm kiếm khách hàng";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NghiepVu.Maketing.SendMail_ctl ctl = new BEE.HoatDong.Maketing.SendMail_ctl();
            ctl.Tag = "Danh sách gửi mail";
            ShowUserControlInTab(ctl);
        }

        private void btnThemXNCN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            BEE.HoatDong.XNCN.XacNhanChuyenNhuong_frm frm = new BEE.HoatDong.XNCN.XacNhanChuyenNhuong_frm();
            frm.ShowDialog();

            BEE.HoatDong.XNCN.XacNhanChuyenNhuong_ctl ctl = new BEE.HoatDong.XNCN.XacNhanChuyenNhuong_ctl();
            ctl.Tag = "Xác nhận chuyển nhượng";
            ShowUserControlInTab(ctl);
        }

        private void btnDSXNCN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            BEE.HoatDong.XNCN.XacNhanChuyenNhuong_ctl ctl = new BEE.HoatDong.XNCN.XacNhanChuyenNhuong_ctl();
            ctl.Tag = "Xác nhận chuyển nhượng";
            ShowUserControlInTab(ctl);
        }

        private void btnModules_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BEE.HoatDong.PhanQuyen.Modules_ctl ctl = new BEE.HoatDong.PhanQuyen.Modules_ctl();
            ctl.Tag = "Module";
            ShowUserControlInTab(ctl);
        }

        private void btnPhanQuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BEE.HoatDong.PhanQuyen.Permission_ctl ctl = new BEE.HoatDong.PhanQuyen.Permission_ctl();
            ctl.Tag = "Phân quyền";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.Editor_frm frm = new BEE.NghiepVuKhac.Editor_frm();
            frm.MaBM = 9;
            frm.ShowDialog();
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.Editor_frm frm = new BEE.NghiepVuKhac.Editor_frm();
            frm.MaBM = 10;
            frm.ShowDialog();
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.Editor_frm frm = new BEE.NghiepVuKhac.Editor_frm();
            frm.MaBM = 11;
            frm.ShowDialog();
        }

        private void btnCongNoKHMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NghiepVu.CongNo.ctlBanHang ctl = new BEE.HoatDong.CongNo.ctlBanHang();
            ctl.Tag = e.Item.Caption;
            ShowUserControlInTab(ctl);
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NghiepVu.Maketing.HappyBirthdaySetting_frm frm = new BEE.HoatDong.Maketing.HappyBirthdaySetting_frm();
            frm.ShowDialog();
            //NghiepVu.Maketing.HappyBirthday_ctl ctl = new BEE.HoatDong.Maketing.HappyBirthday_ctl();
            //
            //ctl.Tag = "";
            //ShowUserControlInTab(ctl);
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NghiepVu.Maketing.NhacNoSetting_frm frm = new BEE.HoatDong.Maketing.NhacNoSetting_frm();
            frm.ShowDialog();
        }

        private void btnCaiDatGiuCho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.SetDefault_frm frm = new BEE.NghiepVuKhac.SetDefault_frm();
            frm.KeyID = 1;
            frm.Text = "Cài đặt giá trị mặc định cho phiếu giữ chỗ";
            frm.ShowDialog();
        }

        private void btnCaiDatDatCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.SetDefault_frm frm = new BEE.NghiepVuKhac.SetDefault_frm();
            frm.KeyID = 2;
            frm.Text = "Cài đặt giá trị mặc định cho thỏa thuận đặt cọc";
            frm.ShowDialog();
        }

        private void btnCaiDatQuyTac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.QuyTatDanhSo_ctl ctl = new BEE.NghiepVuKhac.QuyTatDanhSo_ctl();
            ctl.Tag = "Quy tắc đánh số";
            ShowUserControlInTab(ctl);
        }

        private void btnCauHinhEmail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            HeThong.CauHinhEmail_ctl ctl = new LandSoft.HeThong.CauHinhEmail_ctl();
            ctl.Tag = "Cấu hành email";
            ShowUserControlInTab(ctl);
        }

        private void btnHDMBDaThanhLy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //HDMBTL.HopDongMuaBan_ctl ctl = new LandSoft.HDMBTL.HopDongMuaBan_ctl();
            //ctl.Tag = "Hợp đồng mua bán đã thanh lý";
            //ShowUserControlInTab(ctl);
        }

        private void btnDSHDMBThanhLy_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //HDMBTL.HopDongMuaBan_ctl ctl = new LandSoft.HDMBTL.HopDongMuaBan_ctl();
            //ctl.Tag = "Hợp đồng mua bán đã thanh lý";
            //ShowUserControlInTab(ctl);
        }

        private void btnXacNhanChuaBanGiao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.Editor_frm frm = new BEE.NghiepVuKhac.Editor_frm();
            frm.MaBM = 12;
            frm.LoaiHD = 4;
            frm.ShowDialog();
        }

        private void btnThemNPP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new LandSoft.Agents.frmEdit();
            frm.ShowDialog();

            ShowUserControlInTab(new LandSoft.Agents.ctlManager());
        }

        private void btnDSNPP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new LandSoft.Agents.ctlManager());
        }

        private void btnHopDongMG_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.Editor_frm frm = new BEE.NghiepVuKhac.Editor_frm();
            frm.MaBM = 13;
            frm.ShowDialog();
        }

        private void btnThemHDMG_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NghiepVu.HDMG.HDMoiGioi_frm frm = new BEE.HoatDong.HDMG.HDMoiGioi_frm();
            frm.ShowDialog();

            NghiepVu.HDMG.HDMoiGioi_ctl ctl = new BEE.HoatDong.HDMG.HDMoiGioi_ctl();
            ctl.Tag = "Hợp đông môi giới";
            ShowUserControlInTab(ctl);
        }

        private void btnDSHDMG_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NghiepVu.HDMG.HDMoiGioi_ctl ctl = new BEE.HoatDong.HDMG.HDMoiGioi_ctl();
            ctl.Tag = "Hợp đông môi giới";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.Editor_frm frm = new BEE.NghiepVuKhac.Editor_frm();
            frm.MaBM = 4;
            frm.ShowDialog();
        }

        private void btnThanhLyHDGV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.Editor_frm frm = new BEE.NghiepVuKhac.Editor_frm();
            frm.MaBM = 14;
            frm.ShowDialog();
        }

        private void barEditItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void btnDoanhSoDaiLy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Report.ThongKe.Options_frm frm = new BEE.BaoCao.ThongKe.Options_frm();
            //frm.ShowDialog();
        }

        private void btnAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Help.About2_frm frm = new LandSoft.Help.About2_frm();
            frm.ShowDialog();
        }

        private void btnHoaHongDaiLy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.DaiLy.Options_frm frm = new BEE.BaoCao.DaiLy.Options_frm();
            frm.ShowDialog();
        }

        private void btnCongNoTheoNgay_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.Option_frm frm = new Report.ThongKe.Option_frm();
            frm.ShowDialog();
        }

        private void btnCongNoTheoTuan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.OptionWeek_frm frm = new Report.ThongKe.OptionWeek_frm();
            frm.ShowDialog();
        }

        private void btnCongNoTheoThang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.OptionMonth_frm frm = new Report.ThongKe.OptionMonth_frm();
            frm.ShowDialog();
        }

        private void btnThuThiep_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NghiepVu.Maketing.Template_frm frm = new BEE.HoatDong.Maketing.Template_frm();
            frm.IsView = true;
            frm.ShowDialog();
        }

        private void btnTienDoHDMB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.CongNo.Option_frm frm = new BEE.BaoCao.CongNo.Option_frm();
            frm.LoaiHD = 1;
            frm.ShowDialog();
        }

        private void btnTienDoHDGV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.CongNo.Option_frm frm = new BEE.BaoCao.CongNo.Option_frm();
            frm.LoaiHD = 2;
            frm.ShowDialog();
        }

        private void btnThemPhieuDangKy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnDSPhieuDangKy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //DialogBox.Infomation(Application.ExecutablePath);
            //Huong dan su dung
            Help.Guide_frm frm = new LandSoft.Help.Guide_frm();
            frm.ShowDialog();
        }

        private void btnCauHinhFTP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //HeThong.ConfigFTP_frm frm = new LandSoft.HeThong.ConfigFTP_frm();
            //frm.ShowDialog();
            var f = new FTP.frmConfig();
            f.ShowDialog();
        }

        private void navBarItemThemKhachHang_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            KhachHang.KhachHang_frm frm = new BEE.KhachHang.KhachHang_frm();
            frm.ShowDialog();

            //KhachHang.KhachHang_ctl ctl = new BEE.KhachHang.KhachHang_ctl();
            
            //ctl.Tag = "Khách hàng";
            //ShowUserControlInTab(ctl);
        }

        private void navBarItemThemHopDongCN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ChuyenNhuong.HopDongChuyenNhuong_frm frm = new BEE.ChuyenNhuong.HopDongChuyenNhuong_frm();
            frm.ShowDialog();

            ChuyenNhuong.HopDongChuyenNhuong_ctl ctl = new BEE.ChuyenNhuong.HopDongChuyenNhuong_ctl();
            ctl.Tag = "Hợp đồng chuyển nhượng";
            ShowUserControlInTab(ctl);
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KhachHang.NguoiMoiGioi_ctl ctl = new BEE.KhachHang.NguoiMoiGioi_ctl();
            
            ctl.Tag = "Người môi giới";
            ShowUserControlInTab(ctl);
        }

        private void btnBieuDo_DuAn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.DuAn.Option_frm frm = new Report.ThongKe.DuAn.Option_frm();
            frm.ShowDialog();
        }

        private void btnBieuDo_Block_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.DuAn.OptionBlock_frm frm = new Report.ThongKe.DuAn.OptionBlock_frm();
            frm.ShowDialog();
        }

        private void btnBiauDo_KhuVuc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.DuAn.OptionArea_frm frm = new Report.ThongKe.DuAn.OptionArea_frm();
            frm.ShowDialog();
        }

        private void barButtonItemNguoiDaiDien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KhachHang.ConfigRose_frm frm = new BEE.KhachHang.ConfigRose_frm();
            frm.ShowDialog();
        }

        private void btnRoseAvatar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Report.ThongKe.Avatar.Options_frm frm = new BEE.BaoCao.ThongKe.Avatar.Options_frm();
            //frm.ShowDialog();
        }

        private void btnUpdateVersion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\updater.exe");
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.OptionStaff_frm frm = new BEE.BaoCao.ThongKe.OptionStaff_frm();
            frm.ShowDialog();
        }

        private void btnBCPhieuThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.Quy.Options_frm frm = new BEE.BaoCao.Quy.Options_frm();
            frm.LoaiPhieu = 1;
            frm.ShowDialog();
        }

        private void btnBCPhieuChi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.Quy.Options_frm frm = new BEE.BaoCao.Quy.Options_frm();
            frm.LoaiPhieu = 2;
            frm.ShowDialog();
        }

        private void btnBCTongHop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.Quy.Options_frm frm = new BEE.BaoCao.Quy.Options_frm();
            frm.LoaiPhieu = 3;
            frm.ShowDialog();
        }

        private void btnBienBanTraCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.Editor_frm frm = new BEE.NghiepVuKhac.Editor_frm();
            frm.MaBM = 15;
            frm.ShowDialog();
        }

        private void btnThanhLyHDMB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Khac.Editor_frm frm = new BEE.NghiepVuKhac.Editor_frm();
            frm.MaBM = 16;
            frm.ShowDialog();
        }

        private void btnLichSuCanHo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.BDS.Option_frm frm = new BEE.BaoCao.ThongKe.BDS.Option_frm();
            frm.ShowDialog();
        }

        private void btnDangKyDoanhSo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DaiLy.DangKyDoanhSo_ctl ctl = new LandSoft.DaiLy.DangKyDoanhSo_ctl();
            ctl.Tag = "Đăng ký doanh số";
            ShowUserControlInTab(ctl);
        }

        private void btnCongNoTheoKH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.PhanTichTuoiNo_frm frm = new BEE.BaoCao.ThongKe.PhanTichTuoiNo_frm();
            frm.LoaiBC = 2;
            frm.ShowDialog();
        }

        private void btnCTCN_CaNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.CTHDQT.Option_frm frm = new BEE.BaoCao.ThongKe.CTHDQT.Option_frm();
            frm.LoaiKH = 1;
            frm.ShowDialog();
        }

        private void btnCTCN_DoanhNghiep_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.CTHDQT.Option_frm frm = new BEE.BaoCao.ThongKe.CTHDQT.Option_frm();
            frm.LoaiKH = 2;
            frm.ShowDialog();
        }

        private void btnKhachHangChuaDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.CTHDQT.OptionKHCDT_frm frm = new BEE.BaoCao.ThongKe.CTHDQT.OptionKHCDT_frm();
            frm.LoaiKH = 1;
            frm.ShowDialog();
        }

        private void btnKhachHangDongChuaDu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.CTHDQT.OptionKHCDT_frm frm = new BEE.BaoCao.ThongKe.CTHDQT.OptionKHCDT_frm();
            frm.LoaiKH = 2;
            frm.ShowDialog();
        }

        private void btnCongNoPhaiTra_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.CTHDQT.OptionCNPTKH_frm frm = new BEE.BaoCao.ThongKe.CTHDQT.OptionCNPTKH_frm();
            frm.ShowDialog();
        }

        private void barButtonItem9_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.CTHDQT.OptionTonKho_frm frm = new BEE.BaoCao.ThongKe.CTHDQT.OptionTonKho_frm();
            frm.ShowDialog();
        }

        private void barButtonItem17_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.CTHDQT.OptionTDTT_frm frm = new BEE.BaoCao.ThongKe.CTHDQT.OptionTDTT_frm();
            frm.ShowDialog();
        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //HĐGV da ra HĐMB
            Report.ThongKe.CTHDQT.HDGVDaRaHDMB_rpt rpt = new BEE.BaoCao.ThongKe.CTHDQT.HDGVDaRaHDMB_rpt();
            rpt.ShowPreviewDialog();
        }

        private void btnKeHoachThuNoKH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.PhanTichTuoiNo_frm frm = new BEE.BaoCao.ThongKe.PhanTichTuoiNo_frm();
            frm.LoaiBC = 3;
            frm.ShowDialog();
        }

        private void btnTongHopCongNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.CTHDQT.Option_frm frm = new BEE.BaoCao.ThongKe.CTHDQT.Option_frm();
            frm.LoaiKH = 3;
            frm.ShowDialog();
        }

        private void btnThemChuyenNhuongCoc_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //DatCoc.ChuyenNhuongCoc_frm frm = new LandSoft.DatCoc.ChuyenNhuongCoc_frm();
            //frm.ShowDialog();

            //DatCoc.ChuyenNhuongCoc_ctl ctl = new LandSoft.DatCoc.ChuyenNhuongCoc_ctl();
            //ctl.Tag = "Chuyển nhượng cọc";
            //ShowUserControlInTab(ctl);
        }

        private void btnDSChuyenNhuongCoc_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //DatCoc.ChuyenNhuongCoc_ctl ctl = new LandSoft.DatCoc.ChuyenNhuongCoc_ctl();
            //ctl.Tag = "Chuyển nhượng cọc";
            //ShowUserControlInTab(ctl);
        }

        private void btnThemCNCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //DatCoc.ChuyenNhuongCoc_frm frm = new LandSoft.DatCoc.ChuyenNhuongCoc_frm();
            //frm.ShowDialog();

            //DatCoc.ChuyenNhuongCoc_ctl ctl = new LandSoft.DatCoc.ChuyenNhuongCoc_ctl();
            //ctl.Tag = "Chuyển nhượng cọc";
            //ShowUserControlInTab(ctl);
        }

        private void btnDSCNCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //DatCoc.ChuyenNhuongCoc_ctl ctl = new LandSoft.DatCoc.ChuyenNhuongCoc_ctl();
            //ctl.Tag = "Chuyển nhượng cọc";
            //ShowUserControlInTab(ctl);
        }

        private void btnDSLoTrong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.BDS.OptionLo_frm frm = new BEE.BaoCao.BDS.OptionLo_frm();
            frm.MaLoai = 3;
            frm.ShowDialog();
        }

        private void btnDSLoGiuCho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.BDS.OptionLo_frm frm = new BEE.BaoCao.BDS.OptionLo_frm();
            frm.MaLoai = 1;
            frm.ShowDialog();
        }

        private void btnDSLoDaKyHDVV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.BDS.OptionLo_frm frm = new BEE.BaoCao.BDS.OptionLo_frm();
            frm.MaLoai = 2;
            frm.ShowDialog();
        }

        private void btnPGCDaXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.GiuCho.Options_frm frm = new BEE.BaoCao.GiuCho.Options_frm();
            frm.LoaiHD = 1;
            frm.ShowDialog();
        }

        private void btnHDVVDaXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.GiuCho.Options_frm frm = new BEE.BaoCao.GiuCho.Options_frm();
            frm.LoaiHD = 2;
            frm.ShowDialog();
        }

        private void btnDSPGCChuaKyHĐVV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.GiuCho.ExpiresList_rpt rpt = new BEE.BaoCao.GiuCho.ExpiresList_rpt();
            rpt.ShowPreviewDialog();
        }

        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.HDMB.ExpirestList_rpt rpt = new BEE.BaoCao.HDMB.ExpirestList_rpt();
            rpt.ShowPreviewDialog();
        }

        private void btnDSKHDaBocLo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.GiuCho.SubDivision_rpt rpt = new BEE.BaoCao.GiuCho.SubDivision_rpt();
            rpt.ShowPreviewDialog();
        }

        private void btnDSKHChuaBocLo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.GiuCho.NotLoList_rpt rpt = new BEE.BaoCao.GiuCho.NotLoList_rpt();
            rpt.ShowPreviewDialog();
        }

        private void btnTongSoLoTheoThang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.BDS.Options_frm frm = new BEE.BaoCao.ThongKe.BDS.Options_frm();
            frm.ShowDialog();
        }

        private void btnTongSoLoTheoNam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.BDS.OptionYear_frm frm = new BEE.BaoCao.ThongKe.BDS.OptionYear_frm();
            frm.ShowDialog();
        }

        private void btnDoanhThuTheoThang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.BDS.Options_frm frm = new BEE.BaoCao.ThongKe.BDS.Options_frm();
            frm.IsRevenue = true;
            frm.ShowDialog();
        }

        private void btnDoanhThuTheoNam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.BDS.OptionYear_frm frm = new BEE.BaoCao.ThongKe.BDS.OptionYear_frm();
            frm.IsRevenue = true;
            frm.ShowDialog();
        }

        private void btnTongLoTheoTuan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.BDS.OptionWeek_frm frm = new BEE.BaoCao.ThongKe.BDS.OptionWeek_frm();
            frm.ShowDialog();
        }

        private void btnDoanhThuTheoTuan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Report.ThongKe.BDS.OptionWeek_frm frm = new BEE.BaoCao.ThongKe.BDS.OptionWeek_frm();
            frm.IsRevenue = true;
            frm.ShowDialog();
        }

        private void navBarItemThemPhieuGiaoDich_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NghiepVu.MGL.Ban.frmEdit frm = new NghiepVu.MGL.Ban.frmEdit();
            frm.ShowDialog();
            if (frm.IsSave)
                ShowUserControlInTab(new NghiepVu.MGL.Ban.ctlManager());
        }

        private void navBarItemDSPhieuGD_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.MGL.Ban.ctlManager());
        }

        private void btnBDSMoiGioiLe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //BDS.Broker_ctl ctl = new BDS.Broker_ctl();
            //ctl.Tag = "Môi giới lẻ";
            //ShowUserControlInTab(ctl);
        }

        private void btnThemBDSDatNen_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SanPham.frmEdit frm = new SanPham.frmEdit();
            frm.ShowDialog();
            if (frm.IsSave)
                ShowUserControlInTab(new SanPham.ctlManagerNew());
        }

        private void btnThemBDSBietThu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SanPham.frmEdit frm = new SanPham.frmEdit();
            frm.ShowDialog();
            if (frm.IsSave)
                ShowUserControlInTab(new SanPham.ctlManagerNew());
        }

        private void btnThemBDSNhaLienKe_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SanPham.frmEdit frm = new SanPham.frmEdit();
            frm.ShowDialog();
            if (frm.IsSave)
                ShowUserControlInTab(new SanPham.ctlManagerNew());
        }

        private void tabMain_CloseButtonClick(object sender, EventArgs e)
        {
            DevExpress.XtraTab.XtraTabPage page = (DevExpress.XtraTab.XtraTabPage)
                (e as DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs).Page;

            if (page.Text != "Main")
            {
                tabMain.TabPages.Remove(page);
            }

            if (page.Name == "ctlManagerSupport")
                ExpandNavBarControl();
        }

        private void barButtonItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var frm = new NghiepVu.Import.frmGiaoDich();
            //frm.Show();
        }

        private void barButtonItem25_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new NghiepVu.Import.frmPhieuThu();
            frm.Show();
        }

        private void itemHoaHong_DinhMuc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            HoaHong.frmDinhMuc frm = new LandSoft.HoaHong.frmDinhMuc();
            frm.ShowDialog();
        }

        private void itemHoaHong_CaNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new HoaHong.ctlDoanhSo());
        }

        private void itemHoaHong_TongHop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new HoaHong.ctlHoaHong());
        }

        private void itemDatCocMGL_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NghiepVu.MGL.frmDatCoc frm = new BEE.HoatDong.MGL.frmDatCoc();
            frm.IsBan = true;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                ShowUserControlInTab(new NghiepVu.MGL.ctlDatCoc());
        }

        private void itemDatCocMGL_DanhSach_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.MGL.ctlDatCoc());
        }

        private void itemMGL_MuaThue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.MGL.Mua.ctlManager());
        }

        private void itemMGL_DangKyMuaThue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NghiepVu.MGL.Mua.frmEdit frm = new NghiepVu.MGL.Mua.frmEdit();
            if(frm.ShowDialog() == DialogResult.OK)
                ShowUserControlInTab(new NghiepVu.MGL.Mua.ctlManager());
        }

        private void barMGL_DangKyMuaThue_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NghiepVu.MGL.Mua.frmEdit frm = new NghiepVu.MGL.Mua.frmEdit();
            if (frm.ShowDialog() == DialogResult.OK)
                ShowUserControlInTab(new NghiepVu.MGL.Mua.ctlManager());
        }

        private void barMGL_MuaThue_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.MGL.Mua.ctlManager());
        }

        private void itemMGL_GiaoDich_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.MGL.GiaoDich.ctlManager());
        }

        private void barMGL_GiaoDich_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.MGL.GiaoDich.ctlManager());
        }

        private void itemPhieuThuBanHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.Quy.PhieuThuBanHang.ctlManager());
        }

        private void itemBanGiao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.BanGiao.ctlManager());
        }

        private void itemThanhLy_GiuCho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ctl = new NghiepVu.ThanhLy.ctlManager();
            ctl.MaLGD = 1;
            ctl.Tag = "Thanh lý giữ chỗ";
            ShowUserControlInTab(ctl);
        }

        private void itemThanhLy_DatCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ctl = new NghiepVu.ThanhLy.ctlManager();
            ctl.MaLGD = 2;
            ctl.Tag = "Thanh lý đặt cọc";
            ShowUserControlInTab(ctl);
        }

        private void itemThanhLy_VayVon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ctl = new NghiepVu.ThanhLy.ctlManager();
            ctl.MaLGD = 3;
            ctl.Tag = "Thanh lý hợp đồng vay, góp vốn";
            ShowUserControlInTab(ctl);
        }

        private void itemThanhLy_MuaBan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ctl = new NghiepVu.ThanhLy.ctlManager();
            ctl.MaLGD = 4;
            ctl.Tag = "Thanh lý hợp đồng mua bán";
            ShowUserControlInTab(ctl);
        }

        private void itemPhieuChi_ThanhLy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.Quy.PhieuChiBanHang.ctlManager());
        }

        private void itemCongNo_ThanhLy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.CongNo.ctlThanhLy());
        }

        #region Lich lam viec
        private void timerNhacViec_Tick(object sender, EventArgs e)
        {
            timerNhacViec.Stop();
            //try
            //{
            //    string time = new System.Net.WebClient().DownloadString("http://infoland.com.vn/upload/landsoft/demodiptrial.aspx?key=EC3jMlDL8F8EwULcbBNF/BJvwlIPgFgsFi2F9E1uL/c=");
            //    if (time == "False")
            //    {
            //        DialogBox.Infomation("The software has expired. Please contact DIP Vietnam. Thank you!");
            //        Application.Exit();
            //    }
            //}
            //catch { }
            try
            {
                var ltLLV = db.NhiemVu_getNhacViec(Library.Common.StaffID).ToList();
                alctAltert.Buttons[1].Visible = true;
                foreach (var l in ltLLV)
                {
                    var infoText = string.Format(" {0}.\n - Khách hàng: {1} {2}.\n - Thời gian: {3: hh:mm tt | dd/MM}.",
                        l.TieuDe, l.HoKH, l.TenKH, l.NgayBD);
                    var infoCaption = l.FormID == 1 ? "Nhiệm vụ" : "Lịch hẹn";
                    AlertInfo info = new AlertInfo(infoCaption, infoText, infoText, Properties.Resources.Alarm_Clock);
                    info.Tag = string.Format("{0}|{1}", l.FormID, l.LinkID);
                    alctAltert.Buttons[0].Visible = l.FormID == 2;
                    alctAltert.Show(this, info);
                    try
                    {
                        if (l.Rings != "")
                        {
                            MusicCls.FileName = l.Rings;
                            MusicCls.Play();
                        }
                    }
                    catch { }
                }
            }
            catch { }
            timerNhacViec.Start();
        }

        private void itemLLV_NhiemVuThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CongViec.NhiemVu.AddNew_frm frm = new LandSoft.CongViec.NhiemVu.AddNew_frm();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                ShowUserControlInTab(new LandSoft.CongViec.NhiemVu.List_ctl());
        }

        private void itemLLV_NhiemVu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new LandSoft.CongViec.NhiemVu.List_ctl());
        }

        private void itemLLV_LichHenThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new CongViec.LichHen.AddNew_frm(null, null, "");
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                ShowUserControlInTab(new LandSoft.CongViec.LichHen.SchedulerList_ctl());
        }

        private void itemLLV_LichHen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new LandSoft.CongViec.LichHen.SchedulerList_ctl());
        }

        private void itemLLV_LichHen_DangLuoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //ShowUserControlInTab(new CongViec.LichHen.ctlManager());
        }

        private void itemLLV_TienDo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new CongViec.NhiemVu.frmTienDo();
            frm.ShowDialog();
        }

        private void itemLLV_LoaiNhiemVu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new CongViec.NhiemVu.LoaiNhiemVu_frm();
            frm.ShowDialog();
        }

        private void itemLLV_TrangThaiNhiemVu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new CongViec.NhiemVu.TinhTrang_frm();
            frm.ShowDialog();
        }

        private void itemLLV_MucDoNhiemVu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new CongViec.NhiemVu.MucDo_frm();
            frm.ShowDialog();
        }

        private void itemLLV_LoaiLichHen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new CongViec.LichHen.ChuDe_frm();
            frm.ShowDialog();
        }

        private void itemLLV_ThoiDiemLichHen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new CongViec.LichHen.ThoiDiem_frm();
            frm.ShowDialog();
        }
        #endregion

        #region Notify
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                popupNotify.ShowPopup(MousePosition);
            }
        }

        private void itemPopupShowLandSoft_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Focus();
        }

        private void itemPopupCloseLandSoft_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemPopupChat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new Chat.frmFriendList();
            frm.Show(this);
        }

        private void itemPopupRelogin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            User_ReLogin();
        }

        private void itemPopupMessage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new Chat.frmMessage();
            frm.Show(this);
        }
        #endregion

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
                var ltNV = db.chatTinNhans.Where(p => p.MaNhan == Library.Common.StaffID & p.MaTT == 2 & p.ChuaDoc == true)
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

        #region alert module
        private void timerAlert_Tick(object sender, EventArgs e)
        {
            timerAlert.Stop();
            try
            {
                using (var db = new MasterDataContext())
                {
                    int maNV = Library.Common.StaffID;
                    int count;

                    //count = db.alAlerts.Where(p => p.FormID == 4 && p.UserID == maNV).Count();
                    //grSanPham.Caption = "Sản phẩm dự án (" + count + ")";
                    if (BEE.NgonNgu.Language.LangID == 1)
                    {
                        count = db.alAlerts.Where(p => p.FormID == 27 && p.UserID == maNV).Count();
                        grGiuCho.Caption = "PHIẾU GIỮ CHỖ (" + count + ")";

                        count = db.alAlerts.Where(p => p.FormID == 28 && p.UserID == maNV).Count();
                        grDatCoc.Caption = "PHIẾU ĐẶT CỌC (" + count + ")";

                        count = db.alAlerts.Where(p => p.FormID == 26 && p.UserID == maNV).Count();
                        grVayVon.Caption = "HỢP ĐỒNG GÓP VỐN (" + count + ")";

                        count = db.alAlerts.Where(p => p.FormID == 29 && p.UserID == maNV).Count();
                        grMuaBan.Caption = "HỢP ĐỒNG MUA BÁN (" + count + ")";
                    }
                    else
                    {
                        count = db.alAlerts.Where(p => p.FormID == 27 && p.UserID == maNV).Count();
                        grGiuCho.Caption = "Reservasion Ticket (" + count + ")";

                        count = db.alAlerts.Where(p => p.FormID == 28 && p.UserID == maNV).Count();
                        grDatCoc.Caption = "Deposit Ticket (" + count + ")";

                        count = db.alAlerts.Where(p => p.FormID == 26 && p.UserID == maNV).Count();
                        grVayVon.Caption = "Capital Contribution Contract (" + count + ")";

                        count = db.alAlerts.Where(p => p.FormID == 29 && p.UserID == maNV).Count();
                        grMuaBan.Caption = "Sales Contract (" + count + ")";
                    }
                }
            }
            catch { }

            timerAlert.Start();

            //try
            //{
            //    var ltAlert = db.alAlert_Select(Library.Common.StaffID).ToList();
            //    alctAltert.Buttons[0].Visible = false;
            //    alctAltert.Buttons[1].Visible = false;
            //    foreach (var l in ltAlert)
            //    {
            //        AlertInfo info = new AlertInfo("Thông báo", l.Text, l.Text);
            //        info.Tag = string.Format("{0}|{1}", l.FormID, l.LinkID);
            //        alctAltert.Show(this, info);
            //    }
            //}
            //catch { }
        }

        private void alctAltert_AlertClick(object sender, AlertClickEventArgs e)
        {
            try
            {
                string[] str = e.Info.Tag.ToString().Split('|');
                switch (int.Parse(str[0]))
                {
                    case 26:
                        ShowUserControlInTab(new NghiepVu.VayVon.ctlManager());
                        break;
                    case 27:
                        ShowUserControlInTab(new NghiepVu.GiuCho.ctlManager());
                        break;
                    case 28:
                        ShowUserControlInTab(new NghiepVu.DatCoc.ctlManager());
                        break;
                    case 29:
                        ShowUserControlInTab(new NghiepVu.HDMB.ctlManager());
                        break;
                    case 30:
                        ShowUserControlInTab(new NghiepVu.BanGiao.ctlManager());
                        break;
                    case 31:
                        var objTL = db.tlbhThanhLies.Single(p => p.MaTL == int.Parse(str[1]));
                        var ctl = new NghiepVu.ThanhLy.ctlManager();
                        ctl.MaLGD = objTL.MaLGD.Value;
                        ShowUserControlInTab(ctl);
                        break;
                    case 1:
                        ShowUserControlInTab(new CongViec.NhiemVu.List_ctl());
                        break;
                    case 2:
                        ShowUserControlInTab(new CongViec.LichHen.SchedulerList_ctl());
                        break;
                }
                this.WindowState = FormWindowState.Maximized;
            }
            catch(Exception ex){
                DialogBox.Error(ex.Message);
            }
        }

        private void alctAltert_ButtonClick(object sender, AlertButtonClickEventArgs e)
        {
            try
            {
                var str = e.Info.Tag.ToString().Split('|');
                switch (e.ButtonName)
                {
                    case "Stop":
                    case "Start":
                        var objLH = db.LichHens.Single(p => p.MaLH == int.Parse(str[1]));
                        objLH.DaNhac = e.ButtonName == "Stop";
                        db.SubmitChanges();
                        if (e.ButtonName == "Stop")
                        {
                            e.Button.Name = "Start";
                            e.Button.Image = Properties.Resources.Repeat;
                            e.Button.Hint = "Nhắc lại";
                        }
                        else
                        {
                            e.Button.Name = "Stop";
                            e.Button.Image = Properties.Resources.stop;
                            e.Button.Hint = "Không nhắc lại";
                        }
                        break;

                    case "Close":
                        e.AlertForm.Close();
                        break;

                    case "Muted":
                        MusicCls.Close();
                        break;

                    case "Process":
                        //var f = new LandSoft.CongViec.LichHen.frmProcess();
                        //f.MaNV = Common.StaffID;
                        //f.MaLH = int.Parse(str[1]);
                        //f.ShowDialog();
                        break;
                }
            }
            catch { }
        }

        private void alctAltert_FormClosing(object sender, AlertFormClosingEventArgs e)
        {
            try
            {
                MusicCls.Close();
            }
            catch { }
        }
        #endregion

        private void itemMGL_CapDo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new NghiepVu.MGL.frmCapDo();
            frm.ShowDialog();
        }

        private void itemMGL_Nguon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new NghiepVu.MGL.frmNguon();
            frm.ShowDialog();
        }

        private void itemMGLSP_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new NghiepVu.MGL.Ban.frmEdit();
            frm.ShowDialog();
            if (frm.IsSave)
            {
                ShowUserControlInTab(new NghiepVu.MGL.Ban.ctlManager());
            }
        }

        private void itemMGLSP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.MGL.Ban.ctlManager());
        }

        #region SMS Marketing
        private void itemSMS_SendList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new Marketing.SMS.ctlSendingSMS());
        }

        private void itemSMS_ListReceived_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new Marketing.SMS.ctlGroupReceivesSMS());
        }

        private void itemSMS_Template_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new Marketing.SMS.ctlTemplatesSMS());
        }

        private void itemSMS_Category_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new Marketing.SMS.ctlCategories());
        }

        private void itemSMS_Account_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new Marketing.SMS.frmAccount();
            frm.ShowDialog();
        }

        private void itemSMS_Test_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new Marketing.SMS.frmText();
            frm.ShowDialog();
        }

        private void itemSMS_Statistic_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new Marketing.SMS.ctlStatistic());
        }

        private void itemSMS_Money_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void itemSMS_TopUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new Marketing.SMS.frmTopUp();
            frm.ShowDialog(this);
        }

        private void itemSMS_TopupHistory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new Marketing.SMS.ctlTopupHistory());
        }
        #endregion        

        #region Mail Marketing
        private void itemMail_Sending_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new Marketing.Mail.ctlSending());
        }

        private void itemMail_Receives_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new Marketing.Mail.ctlReceive());
        }

        private void itemMail_Template_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new Marketing.Mail.ctlTemplates());
        }

        private void itemMail_Category_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new Marketing.Mail.ctlCategory());
        }

        private void itemMail_Config_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new Marketing.Mail.ctlConfig());
        }
        #endregion

        private void itemMGL_BieuMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.MGL.BieuMau.ctlManager());
        }

        private void itemDSPhanHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.SoTheoDoi.ctlManager());
        }

        #region Chuyen nhuong
        private void itemChuyenNhuong_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new NghiepVu.ChuyenNhuong.frmEdit())
            {
                frm.ShowDialog();
                if(frm.IsSave)
                    ShowUserControlInTab(new NghiepVu.ChuyenNhuong.ctlManager());
            }
        }

        private void itemChuyenNhuong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.ChuyenNhuong.ctlManager());
        }

        private void barChuyenNhuong_Add_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            using (var frm = new NghiepVu.ChuyenNhuong.frmEdit())
            {
                frm.ShowDialog();
                if (frm.IsSave)
                    ShowUserControlInTab(new NghiepVu.ChuyenNhuong.ctlManager());
            }
        }

        private void barChuyenNhuong_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.ChuyenNhuong.ctlManager());
        }
        #endregion

        private void itemDuAn_Khu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new DuAn.frmKhu())
            {
                frm.ShowDialog();
            }
        }

        private void itemDuAn_PhanKhu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new DuAn.frmPhanKhu())
            {
                frm.ShowDialog();
            }
        }

        private void itemReport_SanPhamTon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BaoCao.ctlSanPhamTon());
        }

        private void itemReport_PhanTichTuoiNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BaoCao.ctlPhanTichTuoiNo(1));
        }

        private void itemCongNo_ThongBaoNopTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.CongNo.ctlManager());
        }
        
        private void itemChart_DoanhThuTheoDuAn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BaoCao.BieuDo.ctlDoanhThuTheoDuAn());
        }

        private void itemChart_DoanhThuTheoThang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BaoCao.BieuDo.ctlDoanhThuTheoThang());
        }

        private void itemChart_DoanhThuTheoNam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BaoCao.BieuDo.ctlDoanhThuTheoNam());
        }

        private void itemChart_DoanhSoTheoDuAn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BaoCao.BieuDo.ctlDoanhSoTheoDuAn());
        }

        private void itemChart_DoanhSoTheoThang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BaoCao.BieuDo.ctlDoanhSoTheoThang());
        }

        private void itemChart_DoanhSoTheoNam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BaoCao.BieuDo.ctlDoanhSoTheoNam());
        }

        private void itemBaoCao_CongNoTongHop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BaoCao.ctlCongNoTongHop());
        }

        private void itemCopyright_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("http://beesky.vn");
        }

        private void itemDoanhSoBanHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BaoCao.ctlDoanhSoBanHang(1));
        }

        private void itemTonKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BaoCao.ctlDanhSachLoTrong());
        }

        private void barButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BaoCao.ctlLichSuGiaoDich());
        }

        private void itemBCKHDongChuaDu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BaoCao.ctlChuaDongDu());
        }

        private void navThemTaiLieu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            using (TaiLieu.frmEdit f = new TaiLieu.frmEdit())
            {
                f.ShowDialog();
            }

            ShowUserControlInTab(new TaiLieu.ctlManager());
        }

        private void navDSTaiLieu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new BEE.TaiLieu.ctlManager());
        }

        private void navDSLoaiTaiLieu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            using (BEE.TaiLieu.frmLoaiTaiLieu f = new BEE.TaiLieu.frmLoaiTaiLieu())
            {
                f.ShowDialog();
            }
        }

        private void itemSinhNhatKH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new Marketing.Care.ctlHappyBirthday());
        }

        private void itemSetupHBBDMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new Marketing.Mail.frmConfigReminder();
            f.SetID = 5;
            f.ShowDialog();
        }

        private void itemSetupHBBDSMS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new Marketing.SMS.frmConfigReminder();
            f.SetID = 6;
            f.ShowDialog();
        }

        private void navBBTLPGC_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var ctl = new NghiepVu.ThanhLy.ctlManager();
            ctl.MaLGD = 1;
            ctl.Tag = "Thanh lý giữ chỗ";
            ShowUserControlInTab(ctl);
        }

        private void navBBTLGV_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var ctl = new NghiepVu.ThanhLy.ctlManager();
            ctl.MaLGD = 3;
            ctl.Tag = "Thanh lý hợp đồng vay, góp vốn";
            ShowUserControlInTab(ctl);
        }

        private void navBBTLPDC_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var ctl = new NghiepVu.ThanhLy.ctlManager();
            ctl.MaLGD = 2;
            ctl.Tag = "Thanh lý đặt cọc";
            ShowUserControlInTab(ctl);
        }

        private void navBBTLMB_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var ctl = new NghiepVu.ThanhLy.ctlManager();
            ctl.MaLGD = 4;
            ctl.Tag = "Thanh lý hợp đồng mua bán";
            ShowUserControlInTab(ctl);
        }

        private void itemSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ctl = new Marketing.Care.ctlManager2();
            ctl.Tag = "Tìm kiếm Khách hàng";
            ShowUserControlInTab(ctl);
        }

        private void itemLSGiaHan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.CongNo.GiaHan.ctlManager());
        }

        private void itemNhanVienSan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new Agents.Staffs.ctlManager());
        }

        private void barButtonItem28_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new Agents.ctlRegistration());
        }

        private void itemKetQuaBanHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BaoCao.Chart.ctlResutlBuy());
        }

        private void itemDSKHChoDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new KhachHang.ctlListConfirm());
        }

        private void timerRequest_Tick(object sender, EventArgs e)
        {
            timerRequest.Stop();
            try
            {
                var NewEvent = db.AlertNewEvent(lblCustomerNew2.Enabled, lblSupport.Enabled, lblRegistration.Enabled).ToList();
                //int? Amount = 0;
                //db.aCustomerHistory_getNotReplyAlert(ref Amount);
                if (NewEvent[0].Support > 0)
                {
                    //AlertInfo info = new AlertInfo("Thông báo", string.Format("Có ({0:n0}) yêu cầu hỗ trợ của Nhân viên Sàn.", NewEvent[0].Support));
                    //info.Tag = "Request";
                    //alertRequest.Show(this, info);
                    lblSupport.Caption = string.Format("Support ({0})", NewEvent[0].Support);
                    itemNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                else
                {
                    lblSupport.Caption = "Support (0)";
                    itemNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                //db.aCustomer_getNotConfirmAlert(ref Amount);
                if (NewEvent[0].Customer > 0)
                {
                    //AlertInfo info = new AlertInfo("Thông báo", string.Format("Có ({0:n0}) Khách hàng mới hoặc yêu cầu chia sẽ thông tin của Nhân viên Sàn.", NewEvent[0].Customer));
                    //info.Tag = "Confirm";
                    //alertRequest.Show(this, info);
                    lblCustomerNew2.Caption = string.Format("New Customer ({0})", NewEvent[0].Customer);
                    itemNew3.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                else
                {
                    lblCustomerNew2.Caption = "New Customer (0)";
                    itemNew3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                if (NewEvent[0].Registration > 0)
                {
                    //AlertInfo info = new AlertInfo("Thông báo", string.Format("Có ({0:n0}) Phiếu đăng ký.", NewEvent[0].Registration));
                    //info.Tag = "Registration";
                    //alertRequest.Show(this, info);
                    lblRegistration.Caption = string.Format("Registration Form ({0})", NewEvent[0].Registration);
                    itemNew2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                else
                {
                    lblRegistration.Caption = "Registration Form (0)";
                    itemNew2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
            }
            catch { }

            timerRequest.Start();
            timerRequest.Interval = 180000;
        }

        void LoadNotReply()
        {
            try
            {
                int? Amount = 0;
                db.aCustomerHistory_getNotReplyAlert(ref Amount);
                if (Amount > 0)
                {
                    AlertInfo info = new AlertInfo("Notification", string.Format("Có ({0:n0}) yêu cầu hỗ trợ của Nhân viên Sàn.", Amount));
                    info.Tag = "Request";
                    alertRequest.Show(this, info);
                }
            }
            catch { }
        }

        void LoadNotConfirm()
        {
            try
            {
                int? Amount = 0;
                db.aCustomer_getNotConfirmAlert(ref Amount);
                if (Amount > 0)
                {
                    AlertInfo info = new AlertInfo("Notification", string.Format("Có ({0:n0}) Khách hàng thêm mới hoặc yêu cầu chia sẽ thông tin của Nhân viên Sàn.", Amount));
                    info.Tag = "Confirm";
                    alertRequest.Show(this, info);
                }
            }
            catch { }
        }

        private void alertRequest_AlertClick(object sender, AlertClickEventArgs e)
        {
            if (e.Info.Tag.ToString() == "Request")
                ShowUserControlInTab(new Agents.ctlSupport());
            else
                ShowUserControlInTab(new KhachHang.ctlListConfirm());
        }

        private void lblCustomerNew2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new KhachHang.ctlListConfirm());
        }

        private void lblSupport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new Agents.ctlSupport());
        }

        private void lblRegistration_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new Agents.ctlRegistration());
        }

        private void barButtonItem28_ItemClick_2(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BaoCao.Chart.ctlResutlBuyAgent());
        }

        private void itemBCBieuThamChieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BaoCao.Chart.ctlReference());
        }

        private void itemBCConNoXau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BaoCao.Chart.ctlCongNoXau());
        }

        private void itemBCTongKetThuHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BaoCao.Chart.ctlThuHoiCongNo());
        }

        private void navDSHoaDon_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.Invoice.ctlManager());
        }

        private void navBienBanBanGiao_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.BanGiao.ctlManager());
        }

        private void itenExecute_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new SQL.frmExecuteSql();
            f.Show();
        }

        private void itemCheckVersionExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dip.cmdExcel objExcel = new dip.cmdExcel();
            DialogBox.Infomation("Version: " + objExcel.CheckVersionExcel());
        }

        private void itemSuoport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region Check version
            try
            {
                System.Net.WebClient client = new System.Net.WebClient();
                var Version = client.DownloadString("http://beesky.vn");
                if (Version != DIPCRM.Support.Library.Common.Version)
                {
                    DIPCRM.Support.Updater.UpdateVer f = new DIPCRM.Support.Updater.UpdateVer();
                    f.ShowDialog();
                    DIPCRM.Support.Library.Common.Version = Version;
                }
            }
            catch
            { }
            #endregion
            var support = new DIPCRM.Support.Library.SupportConfig();
            DIPCRM.Support.Library.Common.ConnectionString = Library.Common.SqlConnString;
            support.GetAccount();
            DIPCRM.Support.Library.Common.ClientNo = support.ClientNo;
            DIPCRM.Support.Library.Common.ClientPass = support.ClientPass;
            DIPCRM.Support.Library.Common.ClientEmail = support.Email;
            DIPCRM.Support.Library.Common.ClientName = support.Name;
            DIPCRM.Support.Library.Common.StaffName = "(" + Properties.Settings.Default.UserName + ") " + Library.Common.StaffName;

            //Load UserControl
            ShowUserControlInTab(new DIPCRM.Support.ctlManagerSupport());
            
            CollapseNavBarControl();
        }

        void CollapseNavBarControl()
        {
            navBarControl1.OptionsNavPane.NavPaneState = DevExpress.XtraNavBar.NavPaneState.Collapsed;
        }

        void ExpandNavBarControl()
        {
            navBarControl1.OptionsNavPane.NavPaneState = DevExpress.XtraNavBar.NavPaneState.Expanded;
        }

        private void itemCongNoHangNgay_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.CongNo.ctlDaily());
        }

        private void itemSetupSMS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new Marketing.SMS.frmConfigReminder();
            f.SetID = 3;
            f.ShowDialog();
        }

        private void itemSetupMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new Marketing.Mail.frmConfigReminder();
            f.SetID = 4;
            f.ShowDialog();
        }

        private void navDSPhieuThanhToan_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.Quy.PhieuThanhToan.ctlManager());
        }

        private void navCamKet3Ben_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.PhongToa.ctlManager());
        }

        private void navDSCongVanDi_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //ShowUserControlInTab(new CongVan.Send.ctlManager());
        }

        private void navThemCVDi_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //var f = new CongVan.Send.frmEdit();
            //f.ShowDialog();
        }

        private void itemNghiepVu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.StepByStep.ctlManager());
        }

        private void navAddCongVanDen_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //var f = new CongVan.received.frmEdit();
            //f.ShowDialog();
        }

        private void navCongVanDen_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //ShowUserControlInTab(new CongVan.received.ctlManager());
        }

        private void navTinhTrangCongVan_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var f = new LandSoft.Other.frmTrangThiCV(1);
            f.Text = "Tình trạng công văn đi";
            f.ShowDialog();
        }

        private void navLoaiCongVan_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var f = new LandSoft.Other.frmLoaiCongVan(1);
            f.Text = "Loại công văn đi";
            f.ShowDialog();
        }

        private void navTinhTrangCVDen_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var f = new LandSoft.Other.frmTrangThiCV(0);
            f.Text = "Tình trạng công văn đến";
            f.ShowDialog();
        }

        private void navLoaiCVDen_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var f = new LandSoft.Other.frmLoaiCongVan(0);
            f.Text = "Loại công văn đến";
            f.ShowDialog();
        }

        private void itemDSQuyetToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.QuyetToan.ctlManager());
        }

        private void itemAlertGC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //PGCClick();
        }

        private void itemAlertDC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //PDCClick();
        }
        
        void PGCClick()
        {
            ppContainer.Controls.Clear();
            ppContainer.ShowPopup(barManager1, new Point(Cursor.Position.X - ppContainer.Width, Cursor.Position.Y + 10));
            var ctl = new BEE.CongCu.Alert.ctlManagerMini(this);
            ctl.Dock = DockStyle.Fill;
            if (ctl.LoadData())
                ppContainer.Controls.Add(ctl);
            else ppContainer.HidePopup();
        }

        void PDCClick()
        {
            ppContainer.Controls.Clear();
            ppContainer.ShowPopup(barManager1, new Point(Cursor.Position.X - ppContainer.Width, Cursor.Position.Y + 10));
            var ctl = new BEE.CongCu.Alert.ctlManagerDC(this);
            ctl.Dock = DockStyle.Fill;
            if (ctl.LoadData())
                ppContainer.Controls.Add(ctl);
            else ppContainer.HidePopup();
        }

        public void GiaoDich(byte maLGD, int? maPGC)
        {
            switch (maLGD)
            {
                case 1:
                    var frmDC = new NghiepVu.DatCoc.frmEdit();
                    frmDC.MaPGC = maPGC.Value;
                    frmDC.ShowDialog();
                    break;
                case 2:
                    var frmVV = new NghiepVu.VayVon.frmEdit();
                    frmVV.MaPGC = maPGC.Value;
                    frmVV.ShowDialog();
                    break;
                case 3:
                    var frmHD = new NghiepVu.HDMB.frmEdit();
                    frmHD.MaPGC = maPGC.Value;
                    frmHD.ShowDialog();
                    break;
                case 4:
                    var frmTL = new NghiepVu.ThanhLy.frmEdit();
                    frmTL.MaLGD = 1;
                    frmTL.MaPGC = maPGC.Value;
                    frmTL.ShowDialog();
                    break;
            }
        }

        public void ClosePopup()
        {
            ppContainer.HidePopup();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            LoadData();
            timer1.Interval = 600000;
            timer1.Start();
        }

        void LoadData()
        {
            using (var db = new MasterDataContext())
            {
                try
                {
                    var pgcHH = db.pgcPhieuGiuCho_expiredCount(0, 0, 0).Single().Amount;
                    itemAlertGC.Caption = "(" + pgcHH + ")";
                    var pdcHH = db.pdcPhieuDatCoc_expiredCount(0, 0, 0).Single().Amount;
                    itemAlertDC.Caption = "(" + pdcHH + ")";
                }
                catch { }
            }
        }

        private void navThongBaoBG_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.BanGiao.ThongBao.ctlManager());
        }

        private void navNghiemThuKH_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.BanGiao.NghiemThu.ctlManager());
        }

        private void navNghiemThuNB_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.BanGiao.NTNoiBo.ctlManager());
        }

        private void itemAddCompany_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new MyCompany.frmEdit();
            f.ShowDialog();
        }

        private void itemListCompany_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new MyCompany.ctlManager());
        }

        private void navListBangKe_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowUserControlInTab(new NghiepVu.BangKe.ctlManager());
        }

        private void navAddBangKe_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var f = new NghiepVu.BangKe.frmEdit();
            f.ShowDialog();
        }

        private void itemViewGeneral_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new SanPham.ctlViewGeneralV2());
        }

        private void itemDSHangMuc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new SanPham.frmLoaiHangMuc();
            f.ShowDialog();
        }

        private void itemSetupTime_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ctl = new DuAn.SetTime.ctlManager();
            ctl.Tag = "Cài đặt thời gian";
            ShowUserControlInTab(ctl);
        }

        private void itemSetupMailProject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ctl = new DuAn.SetMail.ctlManager();
            ctl.Tag = "Cài đặt mail";
            ShowUserControlInTab(ctl);
        }

        private void itemBCDoanhSoBanHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ctl = new BEE.BaoCao.ThongKe.ctlThongKeDuAn(1);
            ctl.Tag = e.Item.Caption;
            ShowUserControlInTab(ctl);
        }

        private void itemBCDoanhThuBanHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ctl = new BEE.BaoCao.ThongKe.ctlThongKeDuAn(2);
            ctl.Tag = e.Item.Caption;
            ShowUserControlInTab(ctl);
        }

        private void itemBaoCaoTongHop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ctl = new BEE.BaoCao.ThongKe.ctlThongKeDuAn(3);
            ctl.Tag = e.Item.Caption;
            ShowUserControlInTab(ctl);
        }

        private void itemBCDoanhSo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ctl = new BEE.BaoCao.ThongKe.ctlThongKeDuAnNam(1);
            ctl.Tag = e.Item.Caption;
            ShowUserControlInTab(ctl);
        }

        private void itemPromotion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ctl = new BEE.DuAn.Promotion.ctlManager();
            ctl.Tag = e.Item.Caption;
            ShowUserControlInTab(ctl);
        }

        private void itemSetupSkin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ctl = new CustomizeColumn.ctlManager();
            ctl.Tag = e.Item.Caption;
            ShowUserControlInTab(ctl);
        }

        private void itemDictionary_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new BEE.NgonNgu.frmTranslate();
            f.ShowDialog();
        }

        private void itemViewGeneralV2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new LandSoft.Product.ctlViewGeneralFormUrlV2());
        }

        private void itemCDDanhSach_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.KhachHang.NhuCau.ctlManager() { Tag = e.Item.Caption });
        }

        private void itemCDThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new BEE.KhachHang.NhuCau.frmEdit();
            f.ShowDialog();
        }

        private void itemMucDich_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new BEE.KhachHang.NhuCau.Dictionary.frmPurpose();
            f.ShowDialog();
        }

        private void itemLyDo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new BEE.KhachHang.NhuCau.Dictionary.frmReason();
            f.ShowDialog();
        }

        private void itemNguon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new BEE.KhachHang.NhuCau.Dictionary.frmHowToKnow();
            f.ShowDialog();
        }

        private void itemViewMaps_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.MauSoDo.ctlManager() { Tag = e.Item.Caption });
        }

        private void itemDSBaoGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControlInTab(new BEE.KhachHang.BaoGia.ctlManager() { Tag = e.Item.Caption });
        }

        private void itemBCCongNoDaThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}