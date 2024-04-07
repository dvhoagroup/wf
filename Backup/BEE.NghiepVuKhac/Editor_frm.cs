using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using BEEREMA;
//using Sunland.Class;

namespace BEE.NghiepVuKhac
{
    public partial class Editor_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaBM, MaDA = 0;
        public byte LoaiHD = 0, MaThiep = 0;
        public string Template = "";
        public Editor_frm()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);

            #region Popup menu
            //Thong tin phieu
            itemSoPhieu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemSoPhieu_ItemClick);
            itemNgayKy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemNgayKy_ItemClick);
            itemLien.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemLien_ItemClick);
            itemNgay.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemNgay_ItemClick);
            itemThang.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemThang_ItemClick);
            itemNam.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemNam_ItemClick);
            itemGhiChu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemGhiChu_ItemClick);

            //Khach hang
            itemHotenKH.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemHotenKH_ItemClick);
            itemNgaySinh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemNgaySinh_ItemClick);
            itemCMND.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemCMND_ItemClick);
            itemNgayCap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemNgayCap_ItemClick);
            itemNoiCap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemNoiCap_ItemClick);
            itemDCTT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDCTT_ItemClick);
            itemDCLL.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDCLL_ItemClick);
            itemDTKH.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDTKH_ItemClick);
            itemDDKH.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDDKH_ItemClick);
            itemCtyKH.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemCtyKH_ItemClick);
            itemDTCT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDTCT_ItemClick);
            itemDCCT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDCCT_ItemClick);
            itemEmail.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemEmail_ItemClick);
            itemMaSoThue.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemMaSoThue_ItemClick);

            //San pham
            itemMaSP.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemMaSP_ItemClick);
            itemSoNha.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemSoNha_ItemClick);
            itemTang.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemTang_ItemClick);
            itemLoaiSP.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemLoaiSP_ItemClick);
            itemTenDA.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemTenDA_ItemClick);
            itemDCSP.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDCSP_ItemClick);
            itemDienTich.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDienTich_ItemClick);
            itemDonGia.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDonGia_ItemClick);
            itemThanhTien.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemThanhTien_ItemClick);
            itemVAT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemVAT_ItemClick);
            itemGiaTri.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemGiaTri_ItemClick);
            itemGiaTriText.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemGiaTriText_ItemClick);
            itemBlock.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemBlock_ItemClick);
            itemViTri.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemViTri_ItemClick);
            itemTenThuongMai.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemTenThuongMai_ItemClick);
            
            //Chu dau tu
            itemTenCDT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemTenCDT_ItemClick);
            itemDiaChiCDT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDiaChiCDT_ItemClick);
            itemDienThoaiCDT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDienThoaiCDT_ItemClick);
            itemNguoiDaiDien.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemNguoiDaiDien_ItemClick);
            itemChucVuCDT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemChucVuCDT_ItemClick);

            //Lich thanh toan
            itemTienGiuCho.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemTienGiuCho_ItemClick);
            itemTienGiuChoText.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemTienGiuChoText_ItemClick);
            itemTienDatCoc.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemTienDatCoc_ItemClick);
            itemTienDatCocText.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemTienDatCocText_ItemClick);
            itemTienDot1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemTienDot1_ItemClick);
            itemTienDot1Text.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemTienDot1Text_ItemClick);
            itemLichThanhToan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemLichThanhToan_ItemClick);

            //Ben nhan chuyen nhuong
            itemHoTenBenNhan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemHoTenBenNhan_ItemClick);
            itemNgayBenNhan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemNgayBenNhan_ItemClick);
            itemCMNDBenNhan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemCMNDBenNhan_ItemClick);
            itemNoiCapBenNhan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemNoiCapBenNhan_ItemClick);
            itemNgayCapBenNhan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemNgayCapBenNhan_ItemClick);
            itemDiaChiLLBenNhan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDiaChiLLBenNhan_ItemClick);
            itemDiaChiTTBenNhan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDiaChiTTBenNhan_ItemClick);
            itemDiDongBenNhan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDiDongBenNhan_ItemClick);
            itemDienThoaiBenNhan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDienThoaiBenNhan_ItemClick);
            #endregion
        }

        #region Lich thanh toan
        void itemLichThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[LTT]");
        }

        void itemTienDot1Text_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[TienDot1Text]");
        }

        void itemTienDot1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[TienDot1]");
        }

        void itemTienDatCocText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[TienDatCocText]");
        }

        void itemTienDatCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[TienDatCoc]");
        }

        void itemTienGiuChoText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[TienGiuCHoText]");
        }

        void itemTienGiuCho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[TienGiuCho]");
        }
        #endregion

        #region Chu dau tu
        void itemChucVuCDT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[ChucVuCDT]");
        }

        void itemNguoiDaiDien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[DaiDien]");
        }

        void itemDienThoaiCDT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[DienThoaiCDT]");
        }

        void itemDiaChiCDT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[DiaChiCDT]");
        }

        void itemTenCDT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[TenCDT]");
        }
        #endregion

        #region San pham
        void itemGiaTriText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[GiaTriText]");
        }

        void itemGiaTri_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[GiaTri]");
        }

        void itemVAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[VAT]");
        }

        void itemThanhTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[ThanhTien]");
        }

        void itemDonGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[DonGia]");
        }

        void itemDienTich_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[DienTich]");
        }

        void itemDCSP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[DiaChiDA]");
        }

        void itemTenDA_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[TenDA]");
        }

        void itemTenThuongMai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[TenThuongMai]");
        }

        void itemLoaiSP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[LoaiCH]");
        }

        void itemTang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[Tang]");
        }

        void itemSoNha_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[SoNha]");
        }

        void itemMaSP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[MaBDS]");
        }

        void itemBlock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[Block]");
        }

        void itemViTri_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[ViTri]");
        }
        #endregion

        #region Menu Khach hang
        void itemDCCT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[DCCT]");
        }

        void itemDTCT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[DTCT]");
        }

        void itemCtyKH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[CtyKH]");
        }

        void itemDDKH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[DDKH]");
        }

        void itemDTKH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[DTKH]");
        }

        void itemDCLL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[DCLL]");
        }

        void itemDCTT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[DCTT]");
        }

        void itemNoiCap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[NoiCap]");
        }

        void itemNgayCap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[NgayCap]");
        }

        void itemCMND_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[CMND]");
        }

        void itemNgaySinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[NgaySinh]");
        }

        void itemHotenKH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[HoTenKH]");
        }

        void itemEmail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[Email]");
        }

        void itemMaSoThue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[MaSoThue]");
        }
        #endregion

        #region Ben nhan chuyen nhuong
        void itemHoTenBenNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[HoTenBenNhan]");
        }

        void itemNgayBenNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[NgaySinhBenNhan]");
        }

        void itemCMNDBenNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[CMNDBenNhan]");
        }

        void itemNgayCapBenNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[NgayCapBenNhan]");
        }

        void itemNoiCapBenNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[NoiCapBenNhan]");
        }

        void itemDiaChiTTBenNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[DiaChiTTBenNhan]");
        }

        void itemDiaChiLLBenNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[DiaChiLLBenNhan]");
        }

        void itemDienThoaiBenNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[DienThoaipBenNhan]");
        }

        void itemDiDongBenNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[DiDongBenNhan]");
        }


        #endregion

        #region Menu Thong tin phieu
        void itemNam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[Nam]");
        }

        void itemThang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[Thang]");
        }

        void itemNgay_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[Ngay]");
        }

        void itemGhiChu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[GhiChu]");
        }

        void itemNgayKy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[NgayKy]");
        }

        void itemLien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[Lien]");
        }

        void itemSoPhieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertText("[SoPhieu]");
        }
        #endregion

        void InsertText(string text)
        {
            try
            {
                txtContent.Document.Cut();
                txtContent.Document.InsertText(txtContent.Document.CaretPosition, text);
            }
            catch
            {
            }
        }

        private void Editor_frm_Load(object sender, EventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            
            switch (LoaiHD)
            {                
                case 1://Hop dong mua ban
                    break;
                case 2:
                    txtContent.RtfText = it.CommonCls.Row("DuAn_getTemplate " + MaDA)["Template"].ToString();
                    break;
                case 3://Mau thiep
                    txtContent.RtfText = it.CommonCls.Row("MauThiep_getNoiDung2 " + MaThiep)["NoiDung2"].ToString();
                    break;
                case 4://Cac mau PDC, HDVV
                    txtContent.RtfText = Template;
                    break;
                default://Bieu mau binh thuong
                    it.DuAn_BieuMauCls objHDMB = new it.DuAn_BieuMauCls(MaDA, (byte)MaBM);
                    txtContent.RtfText = objHDMB.Template;
                    objHDMB = null;
                    break;
            }
            wait.Close(); wait.Dispose();
            //this.TopMost = true;
            //this.TopMost = false;
        }

        //void SaveTemplate()
        //{
        //    it.BieuMauCls bm = new it.BieuMauCls();
        //    bm.MaBM = MaBM;
        //    bm.NoiDung = txtContent.RtfText;
        //    bm.Update();
        //}

        void SaveTemplate()
        {
            it.DuAn_BieuMauCls o = new it.DuAn_BieuMauCls();
            o.MaBM = (byte)MaBM;
            o.MaDA = MaDA;
            o.Template = txtContent.RtfText;
            o.Update();
        }

        void SaveHDMG()
        {
            it.DuAnCls o = new it.DuAnCls();
            o.MaDA = MaDA;
            o.Template = txtContent.RtfText;
            o.UpdateTemplate();
        }
        
        void SaveThiep()
        {
            it.MauThiepCls o = new it.MauThiepCls();
            o.MaThiep = MaThiep;
            o.NoiDung2 = txtContent.RtfText;
            o.Update2();
        }

        void Save()
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            switch (LoaiHD)
            {
                case 1://Hop dong mua ban
                    SaveTemplate();
                    break;
                case 2://Hop dong moi gioi
                    SaveHDMG();
                    break;
                case 3://Mau thiep
                    SaveThiep();
                    break;
                case 4://Cac mau PDC, HDVV
                    Template = txtContent.RtfText;
                    break;
                default://Bieu mau binh thuong
                    SaveTemplate();
                    break;
            }

            
            Cursor.Current = currentCursor;
        }

        void ShowLoading()
        {
            BEE.NghiepVuKhac.InsertImage_frm frm = new BEE.NghiepVuKhac.InsertImage_frm();
            frm.ShowDialog();
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            
            Save();
            wait.Close(); wait.Dispose();
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.DialogResult = DialogResult.OK;
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.Question("Dữ liệu có thay đổi, bạn có muốn cập nhật lại không?") == DialogResult.Yes)
                btnSave_ItemClick(sender, e);
            else
                this.Close();
        }

        private void Editor_frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}