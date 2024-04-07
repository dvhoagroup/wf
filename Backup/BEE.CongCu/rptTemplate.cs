using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System.Linq;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.CongCu
{
    public class rptTemplate : DevExpress.XtraRichEdit.RichEditControl
    {
        public rptTemplate(int id)
        {
            var wait = DialogBox.WaitingForm();
            MasterDataContext db = new MasterDataContext();
            try
            {
                var objTien = new it.TienTeCls();
                //Thong tin bieu mau
                var objBM = db.pgcBieuMaus.Single(p => p.ID == id);
                this.RtfText = objBM.NoiDung;
                Document.ReplaceAll("[SoPhieu]", objBM.SoPhieu, SearchOptions.None);
                Document.ReplaceAll("[NgayKy]", objBM.NgayKy.Value.ToString("dd/MM/yyyy"), SearchOptions.None);
                Document.ReplaceAll("[Ngay]", objBM.NgayKy.Value.ToString("dd"), SearchOptions.None);
                Document.ReplaceAll("[Thang]", objBM.NgayKy.Value.ToString("MM"), SearchOptions.None);
                Document.ReplaceAll("[Nam]", objBM.NgayKy.Value.ToString("yyyy"), SearchOptions.None);
                Document.ReplaceAll("[Lien]", objBM.Lien.ToString(), SearchOptions.None);
                Document.ReplaceAll("[Lan]", objBM.Lan.ToString(), SearchOptions.None);
                Document.ReplaceAll("[DienGiai]", objBM.DienGiai, SearchOptions.None);
                foreach (var i in objBM.pgcbmThongTinKhacs)
                    Document.ReplaceAll(i.daBieuThuc.KyHieu, i.GiaTri, SearchOptions.None);
                //Thong tin giu cho
                var objGC = objBM.pgcPhieuGiuCho;
                Document.ReplaceAll("[SoGC]", objGC.SoPhieu, SearchOptions.None);
                Document.ReplaceAll("[NgayGC]", objGC.NgayKy.Value.ToString("dd/MM/yyyy"), SearchOptions.None);
                Document.ReplaceAll("[ThoiHanGC]", objGC.ThoiHan.ToString(), SearchOptions.None);
                Document.ReplaceAll("[TienGC]", string.Format("{0:#,0.#} {1}", objGC.TienGiuCho ?? 0,
                    objGC.bdsSanPham.LoaiTien.DienGiai), SearchOptions.None);
                Document.ReplaceAll("[TienGCBC]", objTien.DocTienBangChu(objGC.TienGiuCho ?? 0,
                    objGC.bdsSanPham.LoaiTien.DienGiai), SearchOptions.None);
                Document.ReplaceAll("[NgayGC]", objGC.NgayKy.Value.ToString("dd"), SearchOptions.None);
                Document.ReplaceAll("[ThangGC]", objGC.NgayKy.Value.ToString("MM"), SearchOptions.None);
                Document.ReplaceAll("[NamGC]", objGC.NgayKy.Value.ToString("yyyy"), SearchOptions.None);
                Document.ReplaceAll("[GiaTriHopDongCoc]", string.Format("{0:#,0.#} {1}", objGC.GiaTriHDCoc ?? 0,
                    objGC.bdsSanPham.LoaiTien.DienGiai), SearchOptions.None);
                Document.ReplaceAll("[GiaTriHopDongCocBC]", objTien.DocTienBangChu(objGC.GiaTriHDCoc ?? 0,
                    objGC.bdsSanPham.LoaiTien.DienGiai), SearchOptions.None);
                //Thogn tin dat coc
                if (objGC.pdcPhieuDatCoc != null)
                {
                    Document.ReplaceAll("[SoDC]", objGC.pdcPhieuDatCoc.SoPhieu, SearchOptions.None);
                    Document.ReplaceAll("[NgayKyDC]", objGC.pdcPhieuDatCoc.NgayKy.Value.ToString("dd/MM/yyyy"), SearchOptions.None);
                    Document.ReplaceAll("[ThoiHanDC]", objGC.pdcPhieuDatCoc.ThoiHan.ToString(), SearchOptions.None);
                    Document.ReplaceAll("[TienDC]", string.Format("{0:#,0.#} {1}", objGC.pdcPhieuDatCoc.TienCoc ?? 0,
                        objGC.bdsSanPham.LoaiTien.DienGiai), SearchOptions.None);
                    Document.ReplaceAll("[TienDCBC]", objTien.DocTienBangChu(objGC.pdcPhieuDatCoc.TienCoc ?? 0,
                        objGC.bdsSanPham.LoaiTien.DienGiai), SearchOptions.None);
                    Document.ReplaceAll("[NgayDC]", objGC.pdcPhieuDatCoc.NgayKy.Value.ToString("dd"), SearchOptions.None);
                    Document.ReplaceAll("[ThangDC]", objGC.pdcPhieuDatCoc.NgayKy.Value.ToString("MM"), SearchOptions.None);
                    Document.ReplaceAll("[NamDC]", objGC.pdcPhieuDatCoc.NgayKy.Value.ToString("yyyy"), SearchOptions.None);
                }

                //Thong tin hop dong gop von
                if (objGC.vvbhHopDong != null)
                {
                    var objVV = objGC.vvbhHopDong;
                    Document.ReplaceAll("[SoVV]", objVV.SoHDVV, SearchOptions.None);
                    Document.ReplaceAll("[NgayKyVV]", objVV.NgayKy.Value.ToString("dd/MM/yyyy"), SearchOptions.None);
                    Document.ReplaceAll("[ThoiHanVV]", objVV.ThoiHan.ToString(), SearchOptions.None);
                    Document.ReplaceAll("[NgayVV]", objVV.NgayKy.Value.ToString("dd"), SearchOptions.None);
                    Document.ReplaceAll("[ThangVV]", objVV.NgayKy.Value.ToString("MM"), SearchOptions.None);
                    Document.ReplaceAll("[NamVV]", objVV.NgayKy.Value.ToString("yyyy"), SearchOptions.None);
                }

                //Thong tin hop dong mua ban
                if (objGC.HopDongMuaBan != null)
                {
                    var objMB = objGC.HopDongMuaBan;
                    Document.ReplaceAll("[SoMB]", objMB.SoHDMB, SearchOptions.None);
                    Document.ReplaceAll("[NgayKyMB]", objMB.NgayKy.Value.ToString("dd/MM/yyyy"), SearchOptions.None);
                    Document.ReplaceAll("[ThoiHanMB]", objMB.ThoiHan.ToString(), SearchOptions.None);
                    Document.ReplaceAll("[NgayMB]", objMB.NgayKy.Value.ToString("dd"), SearchOptions.None);
                    Document.ReplaceAll("[ThangMB]", objMB.NgayKy.Value.ToString("MM"), SearchOptions.None);
                    Document.ReplaceAll("[NamMB]", objMB.NgayKy.Value.ToString("yyyy"), SearchOptions.None);
                }

                //Thong tin ban giao
                if (objGC.bgbhBanGiao != null)
                {
                    var objBG = objGC.bgbhBanGiao;
                    Document.ReplaceAll("[SoBG]", objBG.SoBG, SearchOptions.None);
                    Document.ReplaceAll("[NgayBG]", objBG.NgayBG.Value.ToString("dd"), SearchOptions.None);
                    Document.ReplaceAll("[ThangBG]", objBG.NgayBG.Value.ToString("MM"), SearchOptions.None);
                    Document.ReplaceAll("[NamBG]", objBG.NgayBG.Value.ToString("yyyy"), SearchOptions.None);
                    Document.ReplaceAll("[NgayKyBG]", objBG.NgayBG.Value.ToString("dd/MM/yyyy"), SearchOptions.None);
                    Document.ReplaceAll("[DiaDiemBG]", objBG.DiaDiem, SearchOptions.None);
                    Document.ReplaceAll("[BenBG]", objBG.BenGiao, SearchOptions.None);
                    Document.ReplaceAll("[BenNBG]", objBG.BenNhan, SearchOptions.None);
                    Document.ReplaceAll("[NoiDungBG]", objBG.NoiDung, SearchOptions.None);
                    Document.ReplaceAll("[ChiaKhoaCanHo]", objBG.NoiDung, SearchOptions.None);
                    Document.ReplaceAll("[YeuCauSuaChua]", objBG.NoiDung, SearchOptions.None);

                    try
                    {
                        for (int i = 1; i < 10; i++)
                        {
                            if (i <= objBG.bgbhTaiLieus.Count)
                            {
                                bgbhTaiLieu objTL = objBG.bgbhTaiLieus[i - 1];
                                Document.ReplaceAll("[TenTL" + i + "]", objTL.TenTL ?? "", SearchOptions.None);
                                Document.ReplaceAll("[SLTL" + i + "]", string.Format("{0:#,0.#}", objTL.SoLuong ?? 0), SearchOptions.None);
                                Document.ReplaceAll("[GhiChuTL" + i + "]", objTL.GhiChu ?? "", SearchOptions.None);
                            }
                            else
                            {
                                Document.ReplaceAll("[TenTL" + i + "]", "", SearchOptions.None);
                                Document.ReplaceAll("[SLHM" + i + "]", "", SearchOptions.None);
                                Document.ReplaceAll("[GhiChuTL" + i + "]", "", SearchOptions.None);
                            }
                        }
                    }
                    catch { }
                }

                //Thong tin thanh ly
                if (objGC.tlbhThanhLy != null)
                {
                    var objTL = objGC.tlbhThanhLy;
                    Document.ReplaceAll("[SoTL]", objTL.SoTL, SearchOptions.None);
                    Document.ReplaceAll("[NgayTL]", objTL.NgayTL.Value.ToString("dd/MM/yyyy"), SearchOptions.None);
                    Document.ReplaceAll("[ThoiHanTL]", objTL.ThoiHan.ToString(), SearchOptions.None);
                    Document.ReplaceAll("[ChiPhiTL]", string.Format("{0:#,0.#} {1}", objTL.ChiPhi ?? 0, objGC.bdsSanPham.LoaiTien.DienGiai), SearchOptions.None);
                    Document.ReplaceAll("[PhatMuaTL]", string.Format("{0:#,0.#} {1}", objTL.PhatMua ?? 0, objGC.bdsSanPham.LoaiTien.DienGiai), SearchOptions.None);
                    Document.ReplaceAll("[PhatBanTL]", string.Format("{0:#,0.#} {1}", objTL.PhatBan ?? 0, objGC.bdsSanPham.LoaiTien.DienGiai), SearchOptions.None);
                    Document.ReplaceAll("[NgayHenTraTL]", objTL.NgayTra != null ? objTL.NgayTra.Value.ToString("dd/MM/yyyy") : "", SearchOptions.None);
                }

                //Bat dong san
                var objSP = objGC.bdsSanPham;
                Document.ReplaceAll("[KyHieu]", objSP.KyHieuSALE, SearchOptions.None);
                Document.ReplaceAll("[TenKhu]", objSP.MaKhu != null ? objSP.Khu.TenKhu : "", SearchOptions.None);
                Document.ReplaceAll("[TenPK]", objSP.MaPK != null ? objSP.PhanKhu.TenPK : "", SearchOptions.None);
                Document.ReplaceAll("[MaLo]", objSP.MaLo, SearchOptions.None);
                Document.ReplaceAll("[Tang]", objSP.SoTang.ToString(), SearchOptions.None);
                Document.ReplaceAll("[DaiXD]", string.Format("{0:#,0.##}", objSP.DaiXD ?? 0), SearchOptions.None);
                Document.ReplaceAll("[NgangXD]", string.Format("{0:#,0.##}", objSP.NgangXD ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DienTichXD]", string.Format("{0:#,0.##} m2", objSP.DienTichXD ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DonGiaXD]", string.Format("{0:#,0.#}", objSP.DonGiaXD ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DonGiaXDBC]", objTien.DocTienBangChu(objSP.DonGiaXD ?? 0, objSP.LoaiTien.DienGiai), SearchOptions.None);
                Document.ReplaceAll("[ThanhTienXD]", string.Format("{0:#,0.#}", objSP.ThanhTienXD ?? 0), SearchOptions.None);
                Document.ReplaceAll("[ThanhTienXDBC]", objTien.DocTienBangChu(objSP.ThanhTienXD ?? 0, objSP.LoaiTien.DienGiai), SearchOptions.None);

                Document.ReplaceAll("[DaiKV]", string.Format("{0:#,0.##}", objSP.DaiKV), SearchOptions.None);
                Document.ReplaceAll("[NgangKV]", string.Format("{0:#,0.##}", objSP.NgangKV), SearchOptions.None);
                Document.ReplaceAll("[DienTichKV]", string.Format("{0:#,0.##} m2", objSP.DienTichKV ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DonGiaKV]", string.Format("{0:#,0.#}", objSP.DonGiaKV ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DonGiaKVBC]", objTien.DocTienBangChu(objSP.DonGiaKV ?? 0, objSP.LoaiTien.DienGiai), SearchOptions.None);
                Document.ReplaceAll("[ThanhTienKV]", string.Format("{0:#,0.#}", objSP.ThanhTienKV ?? 0), SearchOptions.None);
                Document.ReplaceAll("[ThanhTienKVBC]", objTien.DocTienBangChu(objSP.ThanhTienKV ?? 0, objSP.LoaiTien.DienGiai), SearchOptions.None);
                Document.ReplaceAll("[ThanhTienHM]", string.Format("{0:#,0.#}", objSP.ThanhTienHM ?? 0), SearchOptions.None);
                Document.ReplaceAll("[ThanhTienHMBC]", objTien.DocTienBangChu(objSP.ThanhTienHM ?? 0, objSP.LoaiTien.DienGiai), SearchOptions.None);
                Document.ReplaceAll("[ThanhTien]", string.Format("{0:#,0.#}", objSP.ThanhTienXD ?? 0 + objSP.ThanhTienKV ?? 0), SearchOptions.None);
                Document.ReplaceAll("[ThanhTienBC]", objTien.DocTienBangChu(objSP.ThanhTienXD ?? 0 + objSP.ThanhTienKV ?? 0, objSP.LoaiTien.DienGiai), SearchOptions.None);
                Document.ReplaceAll("[TongGiaTri]", string.Format("{0:#,0.#}", objSP.ThanhTien ?? 0), SearchOptions.None);
                Document.ReplaceAll("[TongGiaTriBC]", objTien.DocTienBangChu(objSP.ThanhTien ?? 0, objSP.LoaiTien.DienGiai), SearchOptions.None);
                Document.ReplaceAll("[PhongKhach]", string.Format("{0:#,0}", objSP.PhongKhach), SearchOptions.None);
                Document.ReplaceAll("[PhongNgu]", string.Format("{0:#,0}", objSP.PhongNgu), SearchOptions.None);
                Document.ReplaceAll("[PhongTam]", string.Format("{0:#,0}", objSP.PhongTam), SearchOptions.None);
                Document.ReplaceAll("[Huong]", objSP.PhuongHuong != null ? objSP.PhuongHuong.TenPhuongHuong : "", SearchOptions.None);
                Document.ReplaceAll("[HuongBanCong]", objSP.PhuongHuong != null ? objSP.PhuongHuong.TenPhuongHuong : "", SearchOptions.None);
                Document.ReplaceAll("[LoaiSanPham]", objSP.LoaiBD != null ? objSP.LoaiBD.TenLBDS : "", SearchOptions.None);
                Document.ReplaceAll("[GCNQSDD]", objSP.GCNQSDD ?? "", SearchOptions.None);
                Document.ReplaceAll("[TinhTrangXD]", objSP.TinhTrangXD ?? "", SearchOptions.None);
                Document.ReplaceAll("[NhomKhachHang]", objSP.NhomKH ?? "", SearchOptions.None);
                Document.ReplaceAll("[SoVaoSoGCN]", objSP.SoVaoSoGCN ?? "", SearchOptions.None);
                Document.ReplaceAll("[SoThua]", objSP.SoThua ?? "", SearchOptions.None);
                Document.ReplaceAll("[DiaChiNha]", objSP.DiaChiNha ?? "", SearchOptions.None);
                if(objSP.NgayKyGCN != null)
                    Document.ReplaceAll("[NgayKyGCN]", objSP.NgayKyGCN.Value.ToString("dd/MM/yyyy"), SearchOptions.None);
                else
                    Document.ReplaceAll("[NgayKyGCN]", ".........../........./.............", SearchOptions.None);

                Document.ReplaceAll("[DienTichCH]", string.Format("{0:#,0.##} m2", objSP.DienTichCH ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DienTichThongThuy]", string.Format("{0:#,0.##} m2", objSP.DienTichTT ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DTSanTruoc]", string.Format("{0:#,0.##} m2", objSP.DTSanTruoc ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DTSanSau]", string.Format("{0:#,0.##} m2", objSP.DTSanSau ?? 0), SearchOptions.None);
                Document.ReplaceAll("[MatTien]", string.Format("{0:#,0.##}", objSP.MatTien ?? 0), SearchOptions.None);
                Document.ReplaceAll("[ChieuSau]", string.Format("{0:#,0.##}", objSP.ChieuSau ?? 0), SearchOptions.None);

                //Hop dong
                //Document.ReplaceAll("[DienTichCH]", string.Format("{0:#,0.##}", objGC.DTSD ?? 0), SearchOptions.None);
                Document.ReplaceAll("[GiaBan]", string.Format("{0:#,0.##}", objGC.DonGia ?? 0), SearchOptions.None);
                Document.ReplaceAll("[GiaBanBC]", objTien.DocTienBangChu(objGC.DonGia ?? 0, ""), SearchOptions.None);
                Document.ReplaceAll("[TongGiaBan]", string.Format("{0:#,0.##}", objSP.TongGiaBan ?? 0), SearchOptions.None);
                Document.ReplaceAll("[TongGiaBanBC]", objTien.DocTienBangChu(objSP.TongGiaBan ?? 0, ""), SearchOptions.None);
                decimal TongGiaBanVAT = (objSP.TongGiaBan ?? 0) * 1.1M;
                Document.ReplaceAll("[TongGiaBanVAT]", string.Format("{0:#,0.##}", TongGiaBanVAT), SearchOptions.None);
                Document.ReplaceAll("[TongGiaBanVATBC]", objTien.DocTienBangChu(TongGiaBanVAT, ""), SearchOptions.None);
                
                Document.ReplaceAll("[PhiBaoTri]", string.Format("{0:#,0.##}", objSP.PhiBaoTri ?? 0), SearchOptions.None);
                Document.ReplaceAll("[PhiBaoTriBC]", objTien.DocTienBangChu(objSP.PhiBaoTri ?? 0, ""), SearchOptions.None);

                //var objBK = objGC.bkBangKe;
                //if (objBK != null)
                //{
                    //Don gia niem yet
                    //Document.ReplaceAll("[KhuyenMai]", string.Format("{0:#,0.##}", objBK.KhuyenMai ?? 0), SearchOptions.None);
                    //Document.ReplaceAll("[KhuyenMaiBC]", objTien.DocTienBangChu(objBK.KhuyenMai ?? 0, ""), SearchOptions.None);

                    //Document.ReplaceAll("[SoTienTTDot1]", string.Format("{0:#,0.##}", objBK.TyLeDot1 ?? 0), SearchOptions.None);
                    //Document.ReplaceAll("[SoTienTTDot1BC]", objTien.DocTienBangChu(objBK.TyLeDot1 ?? 0, ""), SearchOptions.None);

                    //Document.ReplaceAll("[ChietKhauThanhToan]", string.Format("{0:#,0.##}", objBK.ChietKhau ?? 0), SearchOptions.None);
                    //Document.ReplaceAll("[ChietKhauThanhToanBC]", objTien.DocTienBangChu(objBK.ChietKhau ?? 0, ""), SearchOptions.None);

                    //Document.ReplaceAll("[GiaTriNiemYet]", string.Format("{0:#,0.##}", objBK.ThanhTien ?? 0), SearchOptions.None);
                    //Document.ReplaceAll("[GiaTriNiemYetBC]", objTien.DocTienBangChu(objBK.ThanhTien ?? 0, ""), SearchOptions.None);

                    //decimal giaTri = (objBK.ThanhTien ?? 0) + ((objBK.ThanhTien ?? 0) * 10 / 100);
                    //Document.ReplaceAll("[GiaTriNiemYetVAT]", string.Format("{0:#,0.##}", giaTri), SearchOptions.None);
                    //Document.ReplaceAll("[GiaTriNiemYetVATBC]", objTien.DocTienBangChu(giaTri, ""), SearchOptions.None);

                    //decimal ThanhTienSauCKKM = objBK.ThanhTienSauCKKM ?? 0;
                    //Document.ReplaceAll("[GiaTriSauCKKM]", string.Format("{0:#,0.##}", objGC.gi), SearchOptions.None);
                    //Document.ReplaceAll("[GiaTriSauCKKMBC]", objTien.DocTienBangChu(ThanhTienSauCKKM, ""), SearchOptions.None);

                    //double giaTriTruocThue = Math.Round((double)ThanhTienSauCKKM / 1.1, 0, MidpointRounding.AwayFromZero);
                    //Document.ReplaceAll("[GiaTriTruocThue]", string.Format("{0:#,0.##}", giaTriTruocThue), SearchOptions.None);
                    //Document.ReplaceAll("[GiaTriTruocThueBC]", objTien.DocTienBangChu((long)giaTriTruocThue, ""), SearchOptions.None);

                    decimal? thueVAT = objGC.TongGiaTriHD * objSP.TyLeVAT / 100;
                    Document.ReplaceAll("[ThueVAT]", string.Format("{0:#,0.##}", thueVAT), SearchOptions.None);
                    Document.ReplaceAll("[ThueVATBC]", objTien.DocTienBangChu(thueVAT ?? 0, ""), SearchOptions.None);

                    Document.ReplaceAll("[TongGiaTriHDMB]", string.Format("{0:#,0.##}", objGC.TongGiaTriHD), SearchOptions.None);
                    Document.ReplaceAll("[TongGiaTriHDMBBC]", objTien.DocTienBangChu(objGC.TongGiaTriHD ?? 0, ""), SearchOptions.None);
                //}

                Document.ReplaceAll("[DTPhongKhach]", string.Format("{0:#,0.##} m2", objSP.DTPKhach ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DTPhongNgu1]", string.Format("{0:#,0.##} m2", objSP.DTPNgu1 ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DTPhongNgu2]", string.Format("{0:#,0.##} m2", objSP.DTPNgu2 ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DTPhongNgu3]", string.Format("{0:#,0.##} m2", objSP.DTPNgu3 ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DTPhongNgu4]", string.Format("{0:#,0.##} m2", objSP.DTPNgu4 ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DTBep]", string.Format("{0:#,0.##} m2", objSP.DTBep ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DTPVeSinh1]", string.Format("{0:#,0.##} m2", objSP.DTVS1 ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DTPVeSinh2]", string.Format("{0:#,0.##} m2", objSP.DTVS2 ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DTPVeSinh3]", string.Format("{0:#,0.##} m2", objSP.DTVS3 ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DTPVeSinh4]", string.Format("{0:#,0.##} m2", objSP.DTVS4 ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DTPVeSinh5]", string.Format("{0:#,0.##} m2", objSP.DTVS5 ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DTPhongTho]", string.Format("{0:#,0.##} m2", objSP.DTPTho ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DTBanCong]", string.Format("{0:#,0.##} m2", objSP.DTBanCong ?? 0), SearchOptions.None);
                Document.ReplaceAll("[DTLogia]", string.Format("{0:#,0.##} m2", objSP.DTLogia ?? 0), SearchOptions.None);
                Document.ReplaceAll("[ViaHe]", string.Format("{0:#,0.##}", objSP.ViaHe ?? 0), SearchOptions.None);

                //Gia trị san pham tren hop dong
                //Document.ReplaceAll("[TongGiaTriHDMB]", string.Format("{0:#,0.##}", objGC.ThanhTien ?? 0), SearchOptions.None);
                //Document.ReplaceAll("[TongGiaTriHDMBBC]", objTien.DocTienBangChu(objGC.ThanhTien ?? 0, ""), SearchOptions.None);

                //Cong ty quan ly
                var objCom = objSP.Company;
                if(objCom != null){
                    Document.ReplaceAll("[CongTyQuanLy]", objCom.TenCT, SearchOptions.None);
                    Document.ReplaceAll("[MaSoThueCTQL]", objCom.MaSoThue, SearchOptions.None);
                }

                //Hang muc
                for (int i = 1; i < 30; i++)
                {
                    if (i <= objSP.bdsHangMucs.Count)
                    {
                        bdsHangMuc objHM = objSP.bdsHangMucs[i - 1];
                        Document.ReplaceAll("[TenHM" + i + "]", objHM.TenHM ?? "", SearchOptions.None);
                        Document.ReplaceAll("[SLHM" + i + "]", string.Format("{0:#,0.#}", objHM.SoLuong ?? 0), SearchOptions.None);
                        Document.ReplaceAll("[DVTHM" + i + "]", objHM.TenDVT ?? "", SearchOptions.None);
                        Document.ReplaceAll("[DonGiaHM" + i + "]", string.Format("{0:#,0.#}", objHM.DonGia ?? 0), SearchOptions.None);
                        Document.ReplaceAll("[DonGiaHMBC" + i + "]", objTien.DocTienBangChu(objHM.DonGia ?? 0, ""), SearchOptions.None);
                        Document.ReplaceAll("[ThanhTienHM" + i + "]", string.Format("{0:#,0.#}", objHM.ThanhTien ?? 0), SearchOptions.None);
                        Document.ReplaceAll("[ThanhTienHMBC" + i + "]", objTien.DocTienBangChu(objHM.ThanhTien ?? 0, objSP.LoaiTien.DienGiai), SearchOptions.None);
                    }
                    else
                    {
                        Document.ReplaceAll("[TenHM" + i + "]", "", SearchOptions.None);
                        Document.ReplaceAll("[SLHM" + i + "]", "", SearchOptions.None);
                        Document.ReplaceAll("[DVTHM" + i + "]", "", SearchOptions.None);
                        Document.ReplaceAll("[DonGiaHM" + i + "]", "", SearchOptions.None);
                        Document.ReplaceAll("[DonGiaHMBC" + i + "]", "", SearchOptions.None);
                        Document.ReplaceAll("[ThanhTienHM" + i + "]", "", SearchOptions.None);
                        Document.ReplaceAll("[ThanhTienHMBC" + i + "]", "", SearchOptions.None);
                    }
                }
                //Lich thanh toan
                for (int i = 1; i < 30; i++)
                {
                    if (i <= objGC.pgcLichThanhToans.Count)
                    {
                        var objLTT = objGC.pgcLichThanhToans[i - 1];
                        Document.ReplaceAll("[DotTT" + i + "]", objLTT.DotTT.ToString(), SearchOptions.None);
                        Document.ReplaceAll("[NgayTT" + i + "]", string.Format("{0:dd/MM/yyyy}", objLTT.NgayTT), SearchOptions.None);
                        Document.ReplaceAll("[TyLeTT" + i + "]", string.Format("{0:#,0.##} %", objLTT.TyLeTT ?? 0), SearchOptions.None);
                        Document.ReplaceAll("[TuongUngTT" + i + "]", string.Format("{0:#,0.#}", objLTT.TuongUng ?? 0), SearchOptions.None);
                        Document.ReplaceAll("[TyLeVATTT" + i + "]", string.Format("{0:#,0.##} %", objLTT.TyLeVAT ?? 0), SearchOptions.None);
                        Document.ReplaceAll("[ThueVATTT" + i + "]", string.Format("{0:#,0.#}", objLTT.ThueVAT ?? 0), SearchOptions.None);
                        Document.ReplaceAll("[SoTienTT" + i + "]", string.Format("{0:#,0.#}", objLTT.SoTien ?? 0), SearchOptions.None);
                        Document.ReplaceAll("[DienGiaiTT" + i + "]", objLTT.DienGiai.ToString(), SearchOptions.None);
                        Document.ReplaceAll("[TuongUngTTBC" + i + "]", objTien.DocTienBangChu(objLTT.TuongUng ?? 0, objSP.LoaiTien.DienGiai), SearchOptions.None);
                        Document.ReplaceAll("[ThueVATTTBC" + i + "]", objTien.DocTienBangChu(objLTT.ThueVAT ?? 0, objSP.LoaiTien.DienGiai), SearchOptions.None);
                        Document.ReplaceAll("[SoTienTTBC" + i + "]", objTien.DocTienBangChu(objLTT.SoTien ?? 0, objSP.LoaiTien.DienGiai), SearchOptions.None);
                        if (objLTT.OptionID != null)
                            Document.ReplaceAll("[KhoangCachDot" + i + "]", string.Format("{0:#,0.#} {1}", objLTT.SoNgay ?? 0, objLTT.OptionID.GetValueOrDefault() == 1 ? "ngày" : "tháng"), SearchOptions.None);
                        else
                            Document.ReplaceAll("[KhoangCachDot" + i + "]", string.Format("{0:#,0.#}", objLTT.SoNgay ?? 0), SearchOptions.None);
                    }
                    else
                    {
                        Document.ReplaceAll("[DotTT" + i + "]", "", SearchOptions.None);
                        Document.ReplaceAll("[NgayTT" + i + "]", "", SearchOptions.None);
                        Document.ReplaceAll("[TyLeTT" + i + "]", "", SearchOptions.None);
                        Document.ReplaceAll("[TuongUngTT" + i + "]", "", SearchOptions.None);
                        Document.ReplaceAll("[TyLeVATTT" + i + "]", "", SearchOptions.None);
                        Document.ReplaceAll("[ThueVATTT" + i + "]", "", SearchOptions.None);
                        Document.ReplaceAll("[SoTienTT" + i + "]", "", SearchOptions.None);
                        Document.ReplaceAll("[DienGiaiTT" + i + "]", "", SearchOptions.None);
                        Document.ReplaceAll("[TuongUngTTBC" + i + "]", "", SearchOptions.None);
                        Document.ReplaceAll("[ThueVATTTBC" + i + "]", "", SearchOptions.None);
                        Document.ReplaceAll("[SoTienTTBC" + i + "]", "", SearchOptions.None);
                        Document.ReplaceAll("[KhoangCachDot" + i + "]", "", SearchOptions.None);
                    }
                }
                //Khach hang
                BenMua_Load(objGC.KhachHang, "");

                //Nguoi dai dien
                if (objGC.NguoiDaiDien != null)
                    NguoiDaiDien_Load(objGC.NguoiDaiDien);
                else
                    NguoiDaiDien_Load();

                //Nguoi dung ten
                for (int i = 1; i <= objGC.pgcKhachHangs.Count; i++)
                {
                    BenMua_Load(objGC.pgcKhachHangs[i - 1].KhachHang, objGC.pgcKhachHangs[i - 1].STT.ToString());
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
                wait.Close();
            }
        }

        private void BenMua_Load(BEE.ThuVien.KhachHang objKH, string i)
        {
            Document.ReplaceAll("[QDBM" + i + "]", objKH.QuyDanh != null ? objKH.QuyDanh.TenQD : "", SearchOptions.None);
            Document.ReplaceAll("[HoTenBM" + i + "]", objKH.HoKH + " " + objKH.TenKH, SearchOptions.None);
            Document.ReplaceAll("[NgaySinhBM" + i + "]", Birthday(objKH.dd, objKH.MM, objKH.yyyy), SearchOptions.None);
            Document.ReplaceAll("[CMNDBM" + i + "]", objKH.SoCMND, SearchOptions.None);
            Document.ReplaceAll("[NgayCapBM" + i + "]", Birthday(objKH.dd2, objKH.MM2, objKH.yyyy2), SearchOptions.None);
            Document.ReplaceAll("[NoiCapBM" + i + "]", objKH.NoiCap, SearchOptions.None);
            Document.ReplaceAll("[DCLLBM" + i + "]", objKH.DCLL, SearchOptions.None);
            Document.ReplaceAll("[DCTTBM" + i + "]", objKH.DCTT, SearchOptions.None);
            Document.ReplaceAll("[DTCDBM" + i + "]", objKH.DTCD != null ? objKH.DTCD : "", SearchOptions.None);
            Document.ReplaceAll("[DTDDBM" + i + "]", objKH.DiDong != null ? objKH.DiDong : "", SearchOptions.None);
            Document.ReplaceAll("[EmailBM" + i + "]", objKH.Email != null ? objKH.Email : "", SearchOptions.None);
            Document.ReplaceAll("[MaSoTTNCNBM" + i + "]", objKH.Email != null ? objKH.Email : "", SearchOptions.None);
            Document.ReplaceAll("[ChucVuBM" + i + "]", objKH.ChucVu != null ? objKH.ChucVu : "", SearchOptions.None);
            Document.ReplaceAll("[TenCtyBM" + i + "]", objKH.TenCongTy != null ? objKH.TenCongTy : "", SearchOptions.None);
            Document.ReplaceAll("[DiaChiCtyBM" + i + "]", objKH.DiaChiCT != null ? objKH.DiaChiCT : "", SearchOptions.None);
            Document.ReplaceAll("[DTCTBM" + i + "]", objKH.DienThoaiCT != null ? objKH.DienThoaiCT : "", SearchOptions.None);
            Document.ReplaceAll("[FaxCtyBM" + i + "]", objKH.FaxCT != null ? objKH.FaxCT : "", SearchOptions.None);
            Document.ReplaceAll("[MaSoThueCtyBM" + i + "]", objKH.MaSoThueCT != null ? objKH.MaSoThueCT : "", SearchOptions.None);
        }

        private void NguoiDaiDien_Load(BEE.ThuVien.NguoiDaiDien objNDD)
        {
            //Document.ReplaceAll("[QDNDD]", objNDD.QuyDanh != null ? objNDD.QuyDanh.TenQD : "", SearchOptions.None);
            Document.ReplaceAll("[HoTenNDD]", objNDD.HoTen, SearchOptions.None);
            if (objNDD.NgaySinh != null)
                Document.ReplaceAll("[NgaySinhNDD]", string.Format("{0:dd/MM/yyyy}", objNDD.NgaySinh), SearchOptions.None);
            else
                Document.ReplaceAll("[NgaySinhNDD]", "......./......../..............", SearchOptions.None);
            Document.ReplaceAll("[CMNDNDD]", objNDD.SoCMND, SearchOptions.None);
            if (objNDD.NgayCap != null)
                Document.ReplaceAll("[NgayCapNDD]", string.Format("{0:dd/MM/yyyy}", objNDD.NgayCap), SearchOptions.None);
            else
                Document.ReplaceAll("[NgayCapNDD]", "......./......../..............", SearchOptions.None);
            Document.ReplaceAll("[NoiCapNDD]", objNDD.NoiCap, SearchOptions.None);
            Document.ReplaceAll("[DCLLNDD]", objNDD.DiaChiLienLac, SearchOptions.None);
            Document.ReplaceAll("[DCTTNDD]", objNDD.DiaChiThuongTru, SearchOptions.None);
            Document.ReplaceAll("[DTCDNDD]", objNDD.DTCD != null ? objNDD.DTCD : "", SearchOptions.None);
            Document.ReplaceAll("[DTDDNDD]", objNDD.DTDD != null ? objNDD.DTDD : "", SearchOptions.None);
            Document.ReplaceAll("[EmailNDD]", objNDD.Email != null ? objNDD.Email : "", SearchOptions.None);
            Document.ReplaceAll("[MaSoTTNCNNDD]", objNDD.Email != null ? objNDD.Email : "", SearchOptions.None);
        }

        private void NguoiDaiDien_Load()
        {
            Document.ReplaceAll("[HoTenNDD]", "", SearchOptions.None);
            Document.ReplaceAll("[NgaySinhNDD]", "......./......../..............", SearchOptions.None);
            Document.ReplaceAll("[CMNDNDD]", "", SearchOptions.None);
            Document.ReplaceAll("[NgayCapNDD]", "......./......../..............", SearchOptions.None);
            Document.ReplaceAll("[NoiCapNDD]", "", SearchOptions.None);
            Document.ReplaceAll("[DCLLNDD]", "", SearchOptions.None);
            Document.ReplaceAll("[DCTTNDD]", "", SearchOptions.None);
            Document.ReplaceAll("[DTCDNDD]", "", SearchOptions.None);
            Document.ReplaceAll("[DTDDNDD]", "", SearchOptions.None);
            Document.ReplaceAll("[EmailNDD]", "", SearchOptions.None);
            Document.ReplaceAll("[MaSoTTNCNNDD]", "", SearchOptions.None);
        }

        string Birthday(string Day, string Month, string Year)
        {
            string Result = "";

            if (Day == null | Day == "" | Day == "dd")
            {
                if (Month == null | Month == "" | Month == "MM")
                {
                    if (Year == null | Year == "" | Year == "yyyy")
                        Result = "....../....../...........";
                    else
                        Result = Year;
                }
                else
                    Result = string.Format("{0}/{1}", Month, Year);
            }
            else
            {
                if (Month == null | Month == "" | Month == "MM")
                {
                    if (Year == null | Year == "" | Year == "yyyy")
                        Result = Day;
                    else
                        Result = string.Format("{0}/....../{1}", Day, Year);
                }
                else
                    Result = string.Format("{0}/{1}/{2}", Day, Month, Year);
            }

            return Result;
        }
    }
}
