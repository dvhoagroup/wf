using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;
using LinqToExcel;
using BEEREMA;

namespace BEE.SanPham
{
    public partial class frmImportLinq : DevExpress.XtraEditors.XtraForm
    {
        public frmImportLinq()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this, barManager1);
        }

        public bool IsSave;

        MasterDataContext db = new MasterDataContext();

        void SanPham_Update(bdsSanPham sp, bdsSanPham objSP)
        {
            sp.MaLBDS = objSP.MaLBDS;
            sp.MaDA = objSP.MaDA;
            if (objSP.MaTT != null)
                sp.MaTT = objSP.MaTT;
            if (objSP.MaHuong != null)
                sp.MaHuong = objSP.MaHuong;
            if (objSP.MaPL != null)
                sp.MaPL = objSP.MaPL;
            if (objSP.MaLD != null)
                sp.MaLD = objSP.MaLD;
            if (objSP.bdsTienIches.Count > 0)
                sp.bdsTienIches = objSP.bdsTienIches;
            if (objSP.bdsHangMucs.Count > 0)
            {
                sp.bdsHangMucs = objSP.bdsHangMucs;
                sp.ThanhTienHM = objSP.ThanhTienHM;
                sp.ThanhTien = sp.ThanhTienXD + sp.ThanhTienKV + sp.ThanhTienHM;
            }
            if (objSP.MaLT != null)
                sp.MaLT = objSP.MaLT;
            sp.DuongRong = objSP.DuongRong;
            sp.NgangKV = objSP.NgangKV;
            sp.DaiKV = objSP.DaiKV;
            sp.SauKV = objSP.SauKV;
            sp.NgangXD = objSP.NgangXD;
            sp.DaiXD = objSP.DaiXD;
            sp.SauXD = objSP.SauXD;
            sp.KyHieuSUN = objSP.KyHieuSUN;
            if (objSP.MaNVKD != null)
                sp.MaNVKD = objSP.MaNVKD;
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            lookDuAn.DataSource = db.DuAns.Select(p => new { p.MaDA, p.TenDA });
            lookNVKD.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.HoTen });
            lookLoaiTien.DataSource = db.LoaiTiens;
            lookLoaiBDS.DataSource = db.LoaiBDs;
            lookHuong.DataSource = db.PhuongHuongs;
            lookPhapLy.DataSource = db.PhapLies;
            lookLoaiDuong.DataSource = db.LoaiDuongs;
            lookTrangThai.DataSource = db.bdsTrangThais.Where(p => p.MaTT < 3);
        }

        private void itemFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (OpenFileDialog file = new OpenFileDialog())
            {
                file.Filter = "(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;
                var excel = new ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();
                cmbSheet.Items.Clear();
                foreach (string s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));
                itemSheet.EditValue = null;
                this.Tag = file.FileName;
            }
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (itemSheet.EditValue != null)
            {
                var wait = DialogBox.WaitingForm();
                try
                {
                    var excel = new ExcelQueryFactory(this.Tag.ToString());
                    var list = excel.Worksheet(itemSheet.EditValue.ToString()).Select(p => new
                    {
                        DuAn = p[0].ToString().Trim(),
                        Khu = p[1].ToString().Trim(),
                        Tang = p[2].ToString().Trim(),
                        ViTri = p[3].ToString().Trim(),
                        KyHieu = p[4].ToString().Trim(),
                        MauCanHo = p[5].ToString().Trim(),
                        IsUse = p[6].ToString().Trim(),
                        Size = p[7].ToString().Trim(),
                        DienTichCH = p[8].ToString().Trim(),
                        DienTichKV = p[9].ToString().Trim(),
                        DienTichXD = p[10].ToString().Trim(),
                        DonGiaXD = p[11].ToString().Trim(),
                        DonGiaKV = p[12].ToString().Trim(),
                        GiaBan = p[13].ToString().Trim(),
                        TongGiaBan = p[14].ToString().Trim(),
                        TyLeChietKhau = p[15].ToString().Trim(),
                        TienChietKhau = p[16].ToString().Trim(),
                        TongTien = p[17].ToString().Trim(),
                        TyLeVAT = p[18].ToString().Trim(),
                        ThueVAT = p[19].ToString().Trim(),
                        TongGiaBanVAT = p[20].ToString().Trim(),
                        PhiBaoTri = p[21].ToString().Trim(),
                        TongGiaTriHD = p[22].ToString().Trim(),
                        Supply = p[23].ToString().Trim(),
                        HuongCua = p[24].ToString().Trim(),
                        HuongBanCong = p[25].ToString().Trim(),

                        DTSanTruoc = p[26].ToString().Trim(),
                        DTSanSau = p[27].ToString().Trim(),
                        MatTien = p[28].ToString().Trim(),
                        ChieuSau = p[29].ToString().Trim(),
                        DTPKhach = p[30].ToString().Trim(),
                        DTPNgu1 = p[31].ToString().Trim(),
                        DTPNgu2 = p[32].ToString().Trim(),
                        DTPNgu3 = p[33].ToString().Trim(),
                        DTPNgu4 = p[34].ToString().Trim(),
                        DTBep = p[35].ToString().Trim(),
                        DTPVS1 = p[36].ToString().Trim(),
                        DTPVS2 = p[37].ToString().Trim(),
                        DTPVS3 = p[38].ToString().Trim(),
                        DTPVS4 = p[39].ToString().Trim(),
                        DTPVS5 = p[40].ToString().Trim(),
                        DTPhongTho = p[41].ToString().Trim(),
                        DTBanCong = p[42].ToString().Trim(),
                        DTLogia = p[43].ToString().Trim(),
                        TangCao = p[44].ToString().Trim(),
                        MoTa = p[45].ToString().Trim(),
                        ViaHe = p[46].ToString().Trim(),
                        LongDuong = p[47].ToString().Trim(),
                        CongTy = p[48].ToString().Trim(),
                        PaymentTerm = p[49].ToString().Trim()
                    });

                    wait.Hide();

                    frmImportUpdate frmUpdate = new frmImportUpdate();
                    frmUpdate.ShowDialog(this);
                    if (frmUpdate.DialogResult != DialogResult.OK) return;

                    wait.Show();
                    var khu = db.Khus.Where(p => p.MaDA == frmUpdate.objSP.MaDA).ToList();
                    lookKhu.DataSource = khu;

                    var company = db.Companies;
                    //var phanKhu = db.PhanKhus.Where(p => p.Khu.MaDA == frmUpdate.objSP.MaDA).ToList();
                    //lookPhanKhu.DataSource = phanKhu;
                    //var lo = db.Loos.Where(p => p.PhanKhu.Khu.MaDA == frmUpdate.objSP.MaDA);

                    var ltSP = new List<bdsSanPham>();
                    foreach (var r in list)
                    {
                        try
                        {
                            bdsSanPham sp = new bdsSanPham();
                            SanPham_Update(sp, frmUpdate.objSP);
                            sp.KyHieu = "";
                            sp.KyHieuSUN = r.KyHieu;
                            sp.KyHieuSALE = r.KyHieu;
                            sp.Khu = khu.SingleOrDefault(p => p.TenKhu == r.Khu);
                            //sp.PhanKhu = phanKhu.SingleOrDefault(p => p.TenPK == r.PhanKhu);
                            //sp.Loo = lo.SingleOrDefault(p => p.TenLo == r.Lo);
                            sp.SoTang = Convert.ToByte(r.TangCao != "" ? Convert.ToByte(r.TangCao) : 0);
                            sp.LauSo = Convert.ToByte(r.Tang != "" ? Convert.ToByte(r.Tang) : 0);
                            sp.ViTri = Convert.ToInt32(r.ViTri != "" ? Convert.ToInt32(r.ViTri) : 0);
                            sp.Supply = Convert.ToInt32(r.Supply != "" ? Convert.ToInt32(r.Supply) : 0);
                            sp.IsUse = r.IsUse.ToLower() == "y" ? true : false;
                            sp.Size = Convert.ToDecimal(r.Size != "" ? Convert.ToDecimal(r.Size.Replace(".", ",")) : 0);
                            sp.MaLo = "";
                            sp.DuongRong = Convert.ToDecimal(r.LongDuong != "" ? Convert.ToDecimal(r.LongDuong) : 0);
                            sp.ViaHe = Convert.ToDecimal(r.ViaHe != "" ? Convert.ToDecimal(r.ViaHe.Replace(".", ",")) : 0);
                            sp.TenDuong = "";
                            sp.TenLMK = r.MauCanHo;
                            sp.DienGiai = r.MoTa;
                            sp.PaymentTerm = r.PaymentTerm;
                            if (sp.MaHuong == null)
                            {
                                var ltHuong = db.PhuongHuongs.Where(p => p.TenPhuongHuong == r.HuongCua).Select(p => p.MaPhuongHuong).ToList();
                                if (ltHuong.Count > 0)
                                {
                                    sp.MaHuong = ltHuong[0];
                                }

                                var ltHuong2 = db.PhuongHuongs.Where(p => p.TenPhuongHuong == r.HuongBanCong).Select(p => p.MaPhuongHuong).ToList();
                                if (ltHuong2.Count > 0)
                                {
                                    sp.MaHuong2 = ltHuong2[0];
                                }
                            }
                            sp.HeSo = 0;// Convert.ToDecimal(r.HeSo != "" ? Convert.ToDecimal(r.HeSo) : 0);
                            sp.DienTichXD = Convert.ToDecimal(r.DienTichXD != "" ? Convert.ToDecimal(r.DienTichXD.Replace(".", ",")) : 0);
                            sp.DonGiaXD = Convert.ToDecimal(r.DonGiaXD != "" ? Convert.ToDecimal(r.DonGiaXD.Replace(".", ",")) : 0);
                            sp.ThanhTienXD = sp.DienTichXD * sp.DonGiaXD;
                            sp.DienTichKV = Convert.ToDecimal(r.DienTichKV != "" ? Convert.ToDecimal(r.DienTichKV.Replace(".", ",")) : 0);
                            sp.DonGiaKV = Convert.ToDecimal(r.DonGiaKV != "" ? Convert.ToDecimal(r.DonGiaKV.Replace(".", ",")) : 0);
                            sp.ThanhTienKV = sp.DienTichKV * sp.DonGiaKV;// Convert.ToDecimal(r.ThanhTienKV != "" ? Convert.ToDecimal(r.ThanhTienKV) : 0);
                            sp.ThanhTienHM = 0;// Convert.ToDecimal(r.ThanhTienHM != "" ? Convert.ToDecimal(r.ThanhTienHM) : 0);
                            sp.ThanhTien = sp.ThanhTienXD + sp.ThanhTienKV + sp.ThanhTienHM;

                            sp.GiaBan = Convert.ToDecimal(r.GiaBan != "" ? Convert.ToDecimal(r.GiaBan.Replace(".", ",")) : 0);
                            sp.TongGiaBan = Convert.ToDecimal(r.TongGiaBan != "" ? Convert.ToDecimal(r.TongGiaBan.Replace(".", ",")) : 0);
                            sp.ThueVAT = Convert.ToDecimal(r.ThueVAT != "" ? Convert.ToDecimal(r.ThueVAT.Replace(".", ",")) : 0);
                            sp.TyLeVAT = Convert.ToDecimal(r.TyLeVAT != "" ? Convert.ToDecimal(r.TyLeVAT.Replace(".", ",")) : 0);

                            sp.Discount = Convert.ToDecimal(r.TyLeChietKhau != "" ? Convert.ToDecimal(r.TyLeChietKhau.Replace(".", ",")) : 0);
                            sp.DiscountMoney = Convert.ToDecimal(r.TienChietKhau != "" ? Convert.ToDecimal(r.TienChietKhau.Replace(".", ",")) : 0);

                            sp.DienTichCH = Convert.ToDecimal(r.DienTichCH != "" ? Convert.ToDecimal(r.DienTichCH.Replace(".", ",")) : 0);
                            sp.PhiBaoTri = Convert.ToDecimal(r.PhiBaoTri != "" ? Convert.ToDecimal(r.PhiBaoTri.Replace(".", ",")) : 0);
                            //sp.PhiBaoTri = Math.Round(sp.PhiBaoTri.Value / 1000, 0, MidpointRounding.AwayFromZero) * 1000;
                            
                            //Dien tich
                            sp.DTBanCong = Convert.ToDecimal(r.DTBanCong != "" ? Convert.ToDecimal(r.DTBanCong.Replace(".", ",")) : 0);
                            sp.DTBep = Convert.ToDecimal(r.DTBep != "" ? Convert.ToDecimal(r.DTBep.Replace(".", ",")) : 0);
                            sp.DTLogia = Convert.ToDecimal(r.DTLogia != "" ? Convert.ToDecimal(r.DTLogia.Replace(".", ",")) : 0);
                            sp.DTPKhach = Convert.ToDecimal(r.DTPKhach != "" ? Convert.ToDecimal(r.DTPKhach.Replace(".", ",")) : 0);
                            sp.DTPNgu1 = Convert.ToDecimal(r.DTPNgu1 != "" ? Convert.ToDecimal(r.DTPNgu1.Replace(".", ",")) : 0);
                            sp.DTPNgu2 = Convert.ToDecimal(r.DTPNgu2 != "" ? Convert.ToDecimal(r.DTPNgu2.Replace(".", ",")) : 0);
                            sp.DTPNgu3 = Convert.ToDecimal(r.DTPNgu3 != "" ? Convert.ToDecimal(r.DTPNgu3.Replace(".", ",")) : 0);
                            sp.DTPNgu4 = Convert.ToDecimal(r.DTPNgu4 != "" ? Convert.ToDecimal(r.DTPNgu4.Replace(".", ",")) : 0);

                            sp.DTPTho = Convert.ToDecimal(r.DTPhongTho != "" ? Convert.ToDecimal(r.DTPhongTho.Replace(".", ",")) : 0);
                            sp.DTSanSau = Convert.ToDecimal(r.DTSanSau != "" ? Convert.ToDecimal(r.DTSanSau.Replace(".", ",")) : 0);
                            sp.DTSanTruoc = Convert.ToDecimal(r.DTSanTruoc != "" ? Convert.ToDecimal(r.DTSanTruoc.Replace(".", ",")) : 0);

                            sp.DTVS1 = Convert.ToDecimal(r.DTPVS1 != "" ? Convert.ToDecimal(r.DTPVS1.Replace(".", ",")) : 0);
                            sp.DTVS2 = Convert.ToDecimal(r.DTPVS2 != "" ? Convert.ToDecimal(r.DTPVS2.Replace(".", ",")) : 0);
                            sp.DTVS3 = Convert.ToDecimal(r.DTPVS3 != "" ? Convert.ToDecimal(r.DTPVS3.Replace(".", ",")) : 0);
                            sp.DTVS4 = Convert.ToDecimal(r.DTPVS4 != "" ? Convert.ToDecimal(r.DTPVS4.Replace(".", ",")) : 0);
                            sp.DTVS5 = Convert.ToDecimal(r.DTPVS5 != "" ? Convert.ToDecimal(r.DTPVS5.Replace(".", ",")) : 0);

                            sp.TyLeMG = 0;// Convert.ToDecimal(r.TyLeMG != "" ? Convert.ToDecimal(r.TyLeMG) : 0);
                            sp.PhiMG = sp.ThanhTien * sp.TyLeMG / 100;
                            sp.PhongKhach = 0;// Convert.ToByte(r.PhongKhach != "" ? Convert.ToByte(r.PhongKhach) : 0);
                            sp.PhongNgu = Convert.ToByte(r.DTPNgu1 != "" ? Convert.ToByte(r.DTPNgu1) : 0);
                            sp.PhongTam = 0;// Convert.ToByte(r.PhongTam != "" ? Convert.ToByte(r.PhongTam) : 0);
                            sp.NgayNhap = DateTime.Now;
                            sp.NgayCN = DateTime.Now;
                            sp.MaNVKT = BEE.ThuVien.Common.StaffID;
                            if (sp.MaNVKD == null) sp.MaNVKD = BEE.ThuVien.Common.StaffID;
                            if (sp.MaTT == null) sp.MaTT = 1;
                            if (sp.MaLT == null) sp.MaLT = 1;
                            sp.DotTT = 0;// Convert.ToByte(r.DotTT != "" ? Convert.ToByte(r.DotTT) : 0);

                            var listCom = db.Companies.Where(p => p.TenCT == r.CongTy).Select(p => p.MaCT).ToList();
                            if (listCom.Count > 0)
                            {
                                sp.MaCT = listCom[0];
                            }
                            
                            //var hm = new bdsHangMuc();
                            //hm.DonGia = sp.ThanhTienHM;
                            //hm.SoLuong = 1;
                            //hm.TenDVT = "";
                            //hm.TenHM = "Thành tiền CSHT";
                            //hm.ThanhTien = sp.ThanhTienHM;
                            //sp.bdsHangMucs.Add(hm);

                            ltSP.Add(sp);
                        }
                        catch { }
                    }
                    gcSP.DataSource = ltSP;
                    list = null;
                }
                catch { }
                finally
                {
                    wait.Close();
                }
            }
            else
            {
                gcSP.DataSource = null;
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvSP.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng kéo chọn bất động sản");
                return;
            }

            frmImportUpdate frm = new frmImportUpdate();
            frm.ShowDialog();
            if (frm.DialogResult != DialogResult.OK) return;

            //var objSP = frm.objSP;
            var ltSP = (List<bdsSanPham>)gcSP.DataSource;
            foreach (var i in indexs)
            {
                var sp = (bdsSanPham)grvSP.GetRow(i);
                SanPham_Update(sp, frm.objSP);
            }

            grvSP.RefreshData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.Question() == DialogResult.No) return;
            grvSP.DeleteSelectedRows();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var ltSP = (List<bdsSanPham>)gcSP.DataSource;
                db.bdsSanPhams.InsertAllOnSubmit(ltSP);
                db.SubmitChanges();

                gcSP.DataSource = null;
                this.IsSave = true;
                DialogBox.Infomation("Dữ liệu đã được lưu");
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            wait.Close();
        }

        private void itemDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}