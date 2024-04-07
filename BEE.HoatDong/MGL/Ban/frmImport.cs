using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEEREMA;
using BEE.ThuVien;
using LinqToExcel;
using System.IO;

namespace BEE.HoatDong.MGL.Ban
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        public bool IsSave;
        dip.cmdExcel objExcel;

        public frmImport()
        {
            InitializeComponent();
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

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.Question() == DialogResult.No) return;
            grvSP.DeleteSelectedRows();
        }

        int KhachHang_Them(BEE.ThuVien.KhachHang objKH)
        {
            int maKH;
            using (MasterDataContext dbKH = new MasterDataContext())
            {
                var listKH = dbKH.KhachHangs.FirstOrDefault(p => p.DiDong == objKH.DiDong || (!string.IsNullOrEmpty(objKH.DiDong2) && p.DiDong2 == objKH.DiDong2)
                || (!string.IsNullOrEmpty(objKH.DiDong3) && objKH.DiDong3 == p.DiDong3) || (!string.IsNullOrEmpty(objKH.DiDong4) && p.DiDong4 == objKH.DiDong4));
                if (listKH != null)
                {
                    maKH = listKH.MaKH;
                }
                else
                {
                    objKH.MaNV = Common.StaffID;
                    objKH.MaTT = 2;
                    dbKH.KhachHangs.InsertOnSubmit(objKH);
                    dbKH.SubmitChanges();
                    maKH = objKH.MaKH;
                }
            }
            return maKH;
        }

        void NguoiLenHe_Them(BEE.ThuVien.NguoiDaiDien objNDD, int makh)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                var listNDD = db.NguoiDaiDiens.AsEnumerable().Where(p => ((p.HoTen == objNDD.HoTen & objNDD.HoTen != "")
                    || (p.MaQH == objNDD.MaQH & objNDD.MaQH != null) || (p.DTDD == objNDD.DTDD & objNDD.DTDD != "")
                    || (p.DTCD == objNDD.DTCD & objNDD.DTCD != "") || (p.Email == objNDD.Email & objNDD.Email != "")
                    || (p.DiaChiLienLac == objNDD.DiaChiLienLac & objNDD.DiaChiLienLac != "")) & (p.MaKH == makh)).ToList();

                if (listNDD.Count == 0 && objNDD.HoTen != "" && objNDD.DTDD != "")
                {
                    objNDD.MaKH = makh;
                    objNDD.NgayNhap = DateTime.Now;
                    objNDD.MaNVN = Common.StaffID;
                    db.NguoiDaiDiens.InsertOnSubmit(objNDD);
                    db.SubmitChanges();
                }
            }
        }

        void NguoiLenHe_Them2(BEE.ThuVien.NguoiDaiDien objNDD2, int makh2)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                var listNDD1 = db.NguoiDaiDiens.AsEnumerable().Where(p => ((p.HoTen == objNDD2.HoTen & objNDD2.HoTen != "")
                    || (p.MaQH == objNDD2.MaQH & objNDD2.MaQH != null) || (p.DTDD == objNDD2.DTDD & objNDD2.DTDD != "")
                    || (p.DTCD == objNDD2.DTCD & objNDD2.DTCD != "") || (p.Email == objNDD2.Email & objNDD2.Email != "")
                    || (p.DiaChiLienLac == objNDD2.DiaChiLienLac & objNDD2.DiaChiLienLac != "")) & (p.MaKH == makh2)).ToList();

                if (listNDD1.Count == 0 && objNDD2.HoTen != "" && objNDD2.DTDD != "")
                {
                    objNDD2.MaKH = makh2;
                    objNDD2.NgayNhap = DateTime.Now;
                    objNDD2.MaNVN = Common.StaffID;
                    db.NguoiDaiDiens.InsertOnSubmit(objNDD2);
                    db.SubmitChanges();
                }
            }
        }

        void NguoiLenHe_Them3(BEE.ThuVien.NguoiDaiDien objNDD3, int makh3)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                var listNDD3 = db.NguoiDaiDiens.AsEnumerable().Where(p => ((p.HoTen == objNDD3.HoTen & objNDD3.HoTen != "")
                    || (p.MaQH == objNDD3.MaQH & objNDD3.MaQH != null) || (p.DTDD == objNDD3.DTDD & objNDD3.DTDD != "")
                    || (p.DTCD == objNDD3.DTCD & objNDD3.DTCD != "") || (p.Email == objNDD3.Email & objNDD3.Email != "")
                    || (p.DiaChiLienLac == objNDD3.DiaChiLienLac & objNDD3.DiaChiLienLac != "")) & (p.MaKH == makh3)).ToList();

                if (listNDD3.Count == 0 && objNDD3.HoTen != "" && objNDD3.DTDD != "")
                {
                    objNDD3.MaKH = makh3;
                    objNDD3.NgayNhap = DateTime.Now;
                    objNDD3.MaNVN = Common.StaffID;
                    db.NguoiDaiDiens.InsertOnSubmit(objNDD3);
                    db.SubmitChanges();
                }
            }
        }

        void NguoiLenHe_Them4(BEE.ThuVien.NguoiDaiDien objNDD4, int makh4)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                var listND4 = db.NguoiDaiDiens.AsEnumerable().Where(p => ((p.HoTen == objNDD4.HoTen & objNDD4.HoTen != "")
                    || (p.MaQH == objNDD4.MaQH & objNDD4.MaQH != null) || (p.DTDD == objNDD4.DTDD & objNDD4.DTDD != "")
                    || (p.DTCD == objNDD4.DTCD & objNDD4.DTCD != "") || (p.Email == objNDD4.Email & objNDD4.Email != "")
                    || (p.DiaChiLienLac == objNDD4.DiaChiLienLac & objNDD4.DiaChiLienLac != "")) & (p.MaKH == makh4)).ToList();

                if (listND4.Count == 0 && objNDD4.HoTen != "" && objNDD4.DTDD != "")
                {
                    objNDD4.MaKH = makh4;
                    objNDD4.NgayNhap = DateTime.Now;
                    objNDD4.MaNVN = Common.StaffID;
                    db.NguoiDaiDiens.InsertOnSubmit(objNDD4);
                    db.SubmitChanges();
                }
            }
        }

        void NguoiLenHe_Them5(BEE.ThuVien.NguoiDaiDien objNDD5, int makh5)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                var listNDD5 = db.NguoiDaiDiens.AsEnumerable().Where(p => ((p.HoTen == objNDD5.HoTen & objNDD5.HoTen != "")
                    || (p.MaQH == objNDD5.MaQH & objNDD5.MaQH != null) || (p.DTDD == objNDD5.DTDD & objNDD5.DTDD != "")
                    || (p.DTCD == objNDD5.DTCD & objNDD5.DTCD != "") || (p.Email == objNDD5.Email & objNDD5.Email != "")
                    || (p.DiaChiLienLac == objNDD5.DiaChiLienLac & objNDD5.DiaChiLienLac != "")) & (p.MaKH == makh5)).ToList();

                if (listNDD5.Count == 0 && objNDD5.HoTen != "" && objNDD5.DTDD != "")
                {
                    objNDD5.MaKH = makh5;
                    objNDD5.NgayNhap = DateTime.Now;
                    objNDD5.MaNVN = Common.StaffID;
                    db.NguoiDaiDiens.InsertOnSubmit(objNDD5);
                    db.SubmitChanges();
                }
            }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            MasterDataContext db = new MasterDataContext();
            try
            {
                int maXa;
                byte maLT, maTinh;
                short maHuyen, maCD, maLBDS;

                for (int i = 0; i < grvSP.RowCount; i++)
                {
                    grvSP.UnselectRow(i);
                    #region ràng buộc
                    if (grvSP.GetRowCellValue(i, "SoNha").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập Số nhà");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    if (grvSP.GetRowCellValue(i, "NhuCau").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập nhu cầu");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    if (grvSP.GetRowCellValue(i, "LoaiBDS").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập loại bất động sản");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    else
                    {
                        try
                        {
                            maLBDS = db.LoaiBDs.Single(p => p.TenLBDS == grvSP.GetRowCellValue(i, "LoaiBDS").ToString().Trim()).MaLBDS;
                        }
                        catch
                        {
                            DialogBox.Error("Loại bất động sản không chính xác");
                            grvSP.FocusedRowHandle = i;
                            return;
                        }
                    }


                    if (grvSP.GetRowCellValue(i, "Tinh").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập tỉnh");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    else
                    {
                        try
                        {
                            maTinh = db.Tinhs.Single(p => p.TenTinh == grvSP.GetRowCellValue(i, "Tinh").ToString().Trim()).MaTinh;
                        }
                        catch
                        {
                            DialogBox.Error("tỉnh không chính xác");
                            grvSP.FocusedRowHandle = i;
                            return;

                        }
                    }

                    if (grvSP.GetRowCellValue(i, "Huyen").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập Huyện");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    else
                    {
                        try
                        {
                            maHuyen = db.Huyens.Where(p => p.MaTinh == maTinh).Single(p => p.TenHuyen == grvSP.GetRowCellValue(i, "Huyen").ToString().Trim()).MaHuyen;
                        }
                        catch
                        {
                            DialogBox.Error("huyện không chính xác");
                            grvSP.FocusedRowHandle = i;
                            return;
                        }
                    }


                    if (grvSP.GetRowCellValue(i, "Xa").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập xã");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    else
                    {
                        try
                        {
                            maXa = db.Xas.Where(p => p.MaHuyen == maHuyen).Single(p => p.TenXa.ToUpper() == grvSP.GetRowCellValue(i, "Xa").ToString().ToUpper()).MaXa;
                        }
                        catch
                        {
                            DialogBox.Error("Xã không chính xác");
                            grvSP.FocusedRowHandle = i;
                            return;

                        }
                    }

                    if (grvSP.GetRowCellValue(i, "LoaiTien").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập loại tiền");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    else
                    {
                        try
                        {
                            maLT = db.LoaiTiens.Single(p => p.TenLoaiTien == grvSP.GetRowCellValue(i, "LoaiTien").ToString().Trim()).MaLoaiTien;
                        }
                        catch
                        {
                            DialogBox.Error("Loại tiền không chính xác");
                            grvSP.FocusedRowHandle = i;
                            return;
                        }
                    }

                    if (grvSP.GetRowCellValue(i, "HoTenKH").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập họ tên khách hàng");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    if (grvSP.GetRowCellValue(i, "DienThoai").ToString().Trim() == "" && grvSP.GetRowCellValue(i, "CMND").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập điện thoại hoặc số CMND");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    #endregion
                    var objKH = new BEE.ThuVien.KhachHang();
                    string hoTenKH = grvSP.GetRowCellValue(i, "HoTenKH").ToString().Trim();
                    objKH.HoKH = hoTenKH.Substring(0, hoTenKH.LastIndexOf(' '));
                    objKH.TenKH = hoTenKH.Substring(hoTenKH.LastIndexOf(' ') + 1);
                    objKH.DiDong = grvSP.GetRowCellValue(i, "DienThoai").ToString();
                    objKH.DiDong2 = grvSP.GetRowCellValue(i, "DienThoai2").ToString();
                    objKH.DiDong3 = grvSP.GetRowCellValue(i, "DienThoai3").ToString();
                    objKH.DiDong4 = grvSP.GetRowCellValue(i, "DienThoai4").ToString();
                    objKH.SoCMND = grvSP.GetRowCellValue(i, "CMND").ToString();
                    objKH.DiaChi = grvSP.GetRowCellValue(i, "DiaChiKH").ToString();
                    objKH.IsPersonal = true;
                    int maKH = KhachHang_Them(objKH);

                    #region nguoi lien he
                    var objNLH = new NguoiDaiDien();
                    objNLH.HoTen = grvSP.GetRowCellValue(i, "NguoiLH1").ToString();
                    try
                    {
                        objNLH.MaQH = db.MoiQuanHes.Single(p => p.TenQH == grvSP.GetRowCellValue(i, "MoiQuanHe1").ToString()).ID;
                    }
                    catch { }
                    objNLH.DTDD = grvSP.GetRowCellValue(i, "DiDongNLH1").ToString();
                    objNLH.DTCD = grvSP.GetRowCellValue(i, "DTCDNLH1").ToString();
                    objNLH.Email = grvSP.GetRowCellValue(i, "EmailNLH1").ToString();
                    objNLH.DiaChiLL = grvSP.GetRowCellValue(i, "DiaChiLH1").ToString();
                    NguoiLenHe_Them(objNLH, maKH);

                    var objNLH1 = new NguoiDaiDien();
                    objNLH1.HoTen = grvSP.GetRowCellValue(i, "NguoiLH2").ToString();
                    try
                    {
                        objNLH1.MaQH = db.MoiQuanHes.Single(p => p.TenQH == grvSP.GetRowCellValue(i, "MoiQuanHe2").ToString()).ID;
                    }
                    catch { }
                    objNLH1.DTDD = grvSP.GetRowCellValue(i, "DiDongNLH2").ToString();
                    objNLH1.DTCD = grvSP.GetRowCellValue(i, "DTCDNLH2").ToString();
                    objNLH1.Email = grvSP.GetRowCellValue(i, "EmailNLH2").ToString();
                    objNLH1.DiaChiLL = grvSP.GetRowCellValue(i, "DiaChiLH2").ToString();
                    NguoiLenHe_Them2(objNLH1, maKH);

                    var objNLH2 = new NguoiDaiDien();
                    objNLH2.HoTen = grvSP.GetRowCellValue(i, "NguoiLH3").ToString();
                    try
                    {
                        objNLH2.MaQH = db.MoiQuanHes.Single(p => p.TenQH == grvSP.GetRowCellValue(i, "MoiQuanHe3").ToString()).ID;
                    }
                    catch { }
                    objNLH2.DTDD = grvSP.GetRowCellValue(i, "DiDongNLH3").ToString();
                    objNLH2.DTCD = grvSP.GetRowCellValue(i, "DTCDNLH3").ToString();
                    objNLH2.Email = grvSP.GetRowCellValue(i, "EmailNLH3").ToString();
                    objNLH2.DiaChiLL = grvSP.GetRowCellValue(i, "DiaChiLH3").ToString();
                    NguoiLenHe_Them3(objNLH2, maKH);

                    var objNLH3 = new NguoiDaiDien();
                    objNLH3.HoTen = grvSP.GetRowCellValue(i, "NguoiLH4").ToString();
                    try
                    {
                        objNLH3.MaQH = db.MoiQuanHes.Single(p => p.TenQH == grvSP.GetRowCellValue(i, "MoiQuanHe4").ToString()).ID;
                    }
                    catch { }
                    objNLH3.DTDD = grvSP.GetRowCellValue(i, "DiDongNLH4").ToString();
                    objNLH3.DTCD = grvSP.GetRowCellValue(i, "DTCDNLH4").ToString();
                    objNLH3.Email = grvSP.GetRowCellValue(i, "EmailNLH4").ToString();
                    objNLH3.DiaChiLL = grvSP.GetRowCellValue(i, "DiaChiLH4").ToString();
                    NguoiLenHe_Them4(objNLH3, maKH);

                    var objNLH4 = new NguoiDaiDien();
                    objNLH4.HoTen = grvSP.GetRowCellValue(i, "NguoiLH5").ToString();
                    try
                    {
                        objNLH4.MaQH = db.MoiQuanHes.Single(p => p.TenQH == grvSP.GetRowCellValue(i, "MoiQuanHe5").ToString()).ID;
                    }
                    catch { }
                    objNLH4.DTDD = grvSP.GetRowCellValue(i, "DiDongNLH5").ToString();
                    objNLH.DTCD = grvSP.GetRowCellValue(i, "DTCDNLH5").ToString();
                    objNLH4.Email = grvSP.GetRowCellValue(i, "EmailNLH5").ToString();
                    objNLH4.DiaChiLL = grvSP.GetRowCellValue(i, "DiaChiLH5").ToString();
                    NguoiLenHe_Them5(objNLH4, maKH);
                    #endregion

                    mglbcBanChoThue objBC = new mglbcBanChoThue();
                    if (grvSP.GetRowCellValue(i, "NgayDK") != "")
                        objBC.NgayDK = Convert.ToDateTime(grvSP.GetRowCellValue(i, "NgayDK"));
                    objBC.NgayCN = DateTime.Now;
                    if (grvSP.GetRowCellValue(i, "NhanVienMG") != "" && grvSP.GetRowCellValue(i, "NhanVienQL") != "")
                    {
                        try
                        {
                            objBC.MaNVKD = db.NhanViens.Single(p => p.MaSo.ToUpper() == grvSP.GetRowCellValue(i, "NhanVienMG").ToString().ToUpper()).MaNV;
                        }
                        catch { DialogBox.Error("Nhân viên MG chưa được thiết lập. Vui lòng thiết lập trước"); return; }
                        try
                        {
                            objBC.MaNVQL = db.NhanViens.Single(p => p.MaSo.ToUpper() == grvSP.GetRowCellValue(i, "NhanVienQL").ToString().ToUpper()).MaNV;
                        }
                        catch { DialogBox.Error("Nhân viên QL được thiết lập. Vui lòng thiết lập trước"); return; }
                    }
                    else
                    {
                        DialogBox.Error("Vui lòng nhập nhân viên. Xin cảm ơn!"); return;
                    }
                    //Bat dong san
                    objBC.KyHieu = grvSP.GetRowCellValue(i, "KyHieu").ToString();
                    objBC.SoNha = grvSP.GetRowCellValue(i, "SoNha").ToString();
                    objBC.IsBan = grvSP.GetRowCellValue(i, "NhuCau").ToString().Trim() == "Cần bán" ? true : false;
                    if (grvSP.GetRowCellValue(i, "TenDuong") != "")
                    {
                        try
                        {
                            objBC.MaDuong = db.Duongs.Single(p => p.TenDuong.ToUpper() == grvSP.GetRowCellValue(i, "TenDuong").ToString().ToUpper()).MaDuong;
                        }
                        catch { DialogBox.Error("Đường chưa được thiết lập. Vui lòng thiết lập trước"); return; }
                    }

                    objBC.MaXa = maXa;
                    objBC.MaHuyen = maHuyen;
                    objBC.MaTinh = maTinh;
                    objBC.NgangXD = Convert.ToDecimal(grvSP.GetRowCellValue(i, "MatTien") == "" ? 0 : grvSP.GetRowCellValue(i, "MatTien"));
                    objBC.DienTichDat = Convert.ToDecimal(grvSP.GetRowCellValue(i, "DienTichDat") == "" ? 0 : grvSP.GetRowCellValue(i, "DienTichDat"));
                    objBC.DienTichXD = Convert.ToDecimal(grvSP.GetRowCellValue(i, "DienTichXD") == "" ? 0 : grvSP.GetRowCellValue(i, "DienTichXD"));
                    objBC.DienTich = Convert.ToDecimal(grvSP.GetRowCellValue(i, "DienTichBC") == "" ? 0 : grvSP.GetRowCellValue(i, "DienTichBC"));
                    objBC.SoTang = Convert.ToByte(grvSP.GetRowCellValue(i, "SoTang") == "" ? 0 : grvSP.GetRowCellValue(i, "SoTang"));
                    objBC.SoTangXD = Convert.ToInt32(grvSP.GetRowCellValue(i, "SoTangXD") == "" ? 0 : grvSP.GetRowCellValue(i, "SoTangXD"));
                    if (grvSP.GetRowCellValue(i, "GiaTien") != "")
                        objBC.DonGia = Convert.ToDecimal(grvSP.GetRowCellValue(i, "GiaTien"));
                    if (grvSP.GetRowCellValue(i, "ThanhTien") != "")
                        objBC.ThanhTien = Convert.ToDecimal(grvSP.GetRowCellValue(i, "ThanhTien"));
                    objBC.DacTrung = grvSP.GetRowCellValue(i, "DacTrung").ToString();
                    objBC.GhiChu = grvSP.GetRowCellValue(i, "GhiChu").ToString();
                    if (grvSP.GetRowCellValue(i, "TrangThai") != "")
                    {
                        try
                        {
                            objBC.MaTT = db.mglbcTrangThais.Single(p => p.TenTT.ToUpper() == grvSP.GetRowCellValue(i, "TrangThai").ToString().ToUpper()).MaTT;
                        }
                        catch { DialogBox.Error("Trạng thái chưa được thiết lập. Vui lòng thiết lập trước"); return; }
                    }
                    else
                    {
                        DialogBox.Error("Vui lòng nhập trạng thái. xin cảm ơn!");
                        return;
                    }

                    objBC.DonViThueCu = (string)grvSP.GetRowCellValue(i, "DonViTC");
                    objBC.DonViDangThue = (string)grvSP.GetRowCellValue(i, "DonViDT");
                    if (grvSP.GetRowCellValue(i, "ThoiHanHD") != "")
                        objBC.ThoiGianHD = Convert.ToDateTime(grvSP.GetRowCellValue(i, "ThoiHanHD"));
                    objBC.MaLBDS = maLBDS;
                    objBC.MaLT = maLT;
                    var toado = grvSP.GetRowCellValue(i, "ToaDo").ToString();
                    objBC.ToaDo = toado;
                    if (toado != "")
                    {
                        var nam = toado.Substring(0, 12);
                        var bac = toado.Substring(toado.Length - 13);
                        objBC.KinhDo = nam;
                        objBC.ViDo = bac;
                    }
                    if (grvSP.GetRowCellValue(i, "PhiMG") != "")
                        objBC.PhiMG = Convert.ToDecimal(grvSP.GetRowCellValue(i, "PhiMG"));
                    if (grvSP.GetRowCellValue(i, "KyHDMG") != "")
                    {
                        try
                        {
                            objBC.TrangThaiHDMG = db.mglbcTrangThaiHDMGs.Single(p => p.TenTT.ToString().ToUpper() == grvSP.GetRowCellValue(i, "KyHDMG").ToString().ToUpper()).ID;
                        }
                        catch { DialogBox.Error("KyHDMG chưa được thiết lập. Vui lòng thiết lập trước"); return; }
                    }

                    if (grvSP.GetRowCellValue(i, "PN") != "")
                        objBC.PhongNgu = Convert.ToByte(grvSP.GetRowCellValue(i, "PN"));
                    if (grvSP.GetRowCellValue(i, "PVS") != "")
                        objBC.PhongVS = Convert.ToByte(grvSP.GetRowCellValue(i, "PVS"));
                    if (grvSP.GetRowCellValue(i, "HuongCua") != "")
                    {
                        try
                        {
                            objBC.MaHuong = db.PhuongHuongs.Single(p => p.TenPhuongHuong.ToString().ToUpper() == grvSP.GetRowCellValue(i, "HuongCua").ToString().ToUpper()).MaPhuongHuong;
                        }
                        catch { DialogBox.Error("Hướng chưa được thiết lập. Vui lòng thiết lập trước"); return; }
                    }

                    if (grvSP.GetRowCellValue(i, "ThoiGianBG") != "")
                        objBC.ThoiGianBGMB = Convert.ToDateTime(grvSP.GetRowCellValue(i, "ThoiGianBG"));
                    if (grvSP.GetRowCellValue(i, "Nguon") != "")
                    {
                        try
                        {
                            objBC.MaNguon = db.mglNguons.Single(p => p.TenNguon.ToUpper() == grvSP.GetRowCellValue(i, "Nguon").ToString().ToUpper()).MaNguon;
                        }
                        catch { DialogBox.Error("Nguồn chưa được thiết lập. Vui lòng thiết lập trước"); return; }
                    }
                    if (grvSP.GetRowCellValue(i, "PhapLy") != "")
                    {
                        try
                        {
                            objBC.MaPL = db.PhapLies.Single(p => p.TenPL.ToUpper() == grvSP.GetRowCellValue(i, "PhapLy").ToString().ToUpper()).MaPL;
                        }
                        catch { DialogBox.Error("Pháp lý chưa được thiết lập. Vui lòng thiết lập trước"); return; }
                    }

                    //Khach hang
                    objBC.MaKH = maKH;
                    objBC.MaTTD = 2;
                    objBC.NgayNhap = DateTime.Now;
                    db.mglbcBanChoThues.InsertOnSubmit(objBC);
                    db.SubmitChanges();
                    grvSP.SelectRow(i);

                    if (grvSP.GetRowCellValue(i, "MatTien1") != "" || grvSP.GetRowCellValue(i, "DienTich1") != ""
                        || grvSP.GetRowCellValue(i, "GiaTien1") != "" || grvSP.GetRowCellValue(i, "SoTang1") != "")
                    {
                        var objTT = new mglbcThongTinTT();
                        if (grvSP.GetRowCellValue(i, "MatTien1") != "")
                            objTT.MatTien = Convert.ToDecimal(grvSP.GetRowCellValue(i, "MatTien1"));
                        if (grvSP.GetRowCellValue(i, "DienTich1") != "")
                            objTT.DienTich = Convert.ToDecimal(grvSP.GetRowCellValue(i, "DienTich1"));
                        if (grvSP.GetRowCellValue(i, "GiaTien1") != "")
                            objTT.GiaTien = Convert.ToDecimal(grvSP.GetRowCellValue(i, "GiaTien1"));
                        if (grvSP.GetRowCellValue(i, "SoTang1") != "")
                            objTT.SoTang = Convert.ToInt32(grvSP.GetRowCellValue(i, "SoTang1"));
                        objTT.MaBC = objBC.MaBC;
                        db.mglbcThongTinTTs.InsertOnSubmit(objTT);
                        db.SubmitChanges();
                    }

                    if (grvSP.GetRowCellValue(i, "MatTien2") != "" || grvSP.GetRowCellValue(i, "DienTich2") != ""
                        || grvSP.GetRowCellValue(i, "GiaTien2") != "" || grvSP.GetRowCellValue(i, "SoTang2") != "")
                    {
                        var objTT1 = new mglbcThongTinTT();
                        if (grvSP.GetRowCellValue(i, "MatTien2") != "")
                            objTT1.MatTien = Convert.ToDecimal(grvSP.GetRowCellValue(i, "MatTien2"));
                        if (grvSP.GetRowCellValue(i, "DienTich2") != "")
                            objTT1.DienTich = Convert.ToDecimal(grvSP.GetRowCellValue(i, "DienTich2"));
                        if (grvSP.GetRowCellValue(i, "GiaTien2") != "")
                            objTT1.GiaTien = Convert.ToDecimal(grvSP.GetRowCellValue(i, "GiaTien2"));
                        if (grvSP.GetRowCellValue(i, "SoTang2") != "")
                            objTT1.SoTang = Convert.ToInt32(grvSP.GetRowCellValue(i, "SoTang2"));
                        objTT1.MaBC = objBC.MaBC;
                        db.mglbcThongTinTTs.InsertOnSubmit(objTT1);
                        db.SubmitChanges();
                    }

                    if (grvSP.GetRowCellValue(i, "MatTien3") != "" || grvSP.GetRowCellValue(i, "DienTich3") != ""
                        || grvSP.GetRowCellValue(i, "GiaTien3") != "" || grvSP.GetRowCellValue(i, "SoTang3") != "")
                    {
                        var objTT2 = new mglbcThongTinTT();
                        if (grvSP.GetRowCellValue(i, "MatTien3") != "")
                            objTT2.MatTien = Convert.ToDecimal(grvSP.GetRowCellValue(i, "MatTien3"));
                        if (grvSP.GetRowCellValue(i, "DienTich3") != "")
                            objTT2.DienTich = Convert.ToDecimal(grvSP.GetRowCellValue(i, "DienTich3"));
                        if (grvSP.GetRowCellValue(i, "GiaTien3") != "")
                            objTT2.GiaTien = Convert.ToDecimal(grvSP.GetRowCellValue(i, "GiaTien3"));
                        if (grvSP.GetRowCellValue(i, "SoTang3") != "")
                            objTT2.SoTang = Convert.ToInt32(grvSP.GetRowCellValue(i, "SoTang3"));
                        objTT2.MaBC = objBC.MaBC;
                        db.mglbcThongTinTTs.InsertOnSubmit(objTT2);
                        db.SubmitChanges();
                    }
                }
                gcSP.DataSource = null;
                this.IsSave = true;
                DialogBox.Infomation("Dữ liệu đã được lưu");
                grvSP.DeleteSelectedRows();
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

        private void itemDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (itemSheet.EditValue != null)
            {
                itemLuu.Enabled = false;
                var excel = new ExcelQueryFactory(this.Tag.ToString());
                var list = excel.Worksheet(itemSheet.EditValue.ToString()).Select(p => new
                {
                    NhuCau = p[0].ToString().Trim(),
                    NgayDK = p[1].ToString().Trim(),
                    SoNha = p[2].ToString().Trim(),
                    TenDuong = p[3].ToString().Trim(),
                    Xa = p[4].ToString().Trim(),
                    Huyen = p[5].ToString().Trim(),
                    Tinh = p[6].ToString().Trim(),
                    MatTien = p[7].ToString().Trim(),
                    DienTichDat = p[8].ToString().Trim(),
                    DienTichXD = p[9].ToString().Trim(),
                    SoTang = p[10].ToString().Trim(),
                    DienTichBC = p[11].ToString().Trim(),
                    SoTangXD = p[12].ToString().Trim(),
                    GiaTien = p[13].ToString().Trim(),
                    ThanhTien = p[14].ToString().Trim(),
                    MatTien1 = p[15].ToString().Trim(),
                    DienTich1 = p[16].ToString().Trim(),
                    GiaTien1 = p[17].ToString().Trim(),
                    SoTang1 = p[18].ToString().Trim(),
                    MatTien2 = p[19].ToString().Trim(),
                    DienTich2 = p[20].ToString().Trim(),
                    GiaTien2 = p[21].ToString().Trim(),
                    SoTang2 = p[22].ToString().Trim(),
                    MatTien3 = p[23].ToString().Trim(),
                    DienTich3 = p[24].ToString().Trim(),
                    GiaTien3 = p[25].ToString().Trim(),
                    SoTang3 = p[26].ToString().Trim(),
                    DacTrung = p[27].ToString().Trim(),
                    GhiChu = p[28].ToString().Trim(),
                    TrangThai = p[29].ToString().Trim(),
                    DonViTC = p[30].ToString().Trim(),
                    DonViDT = p[31].ToString().Trim(),
                    ThoiHanHD = p[32].ToString().Trim(),
                    HoTenKH = p[33].ToString().Trim(),
                    DienThoai = p[34].ToString().Trim(),
                    DienThoai2 = p[35].ToString().Trim(),
                    DienThoai3 = p[36].ToString().Trim(),
                    DienThoai4 = p[37].ToString().Trim(),
                    CMND = p[38].ToString().Trim(),
                    DiaChiKH = p[39].ToString().Trim(),
                    NguoiLH1 = p[40].ToString().Trim(),
                    MoiQuanHe1 = p[41].ToString().Trim(),
                    DiDongNLH1 = p[42].ToString().Trim(),
                    DTCDNLH1 = p[43].ToString().Trim(),
                    EmailNLH1 = p[44].ToString().Trim(),
                    DiaChiLH1 = p[45].ToString().Trim(),
                    NguoiLH2 = p[46].ToString().Trim(),
                    MoiQuanHe2 = p[47].ToString().Trim(),
                    DiDongNLH2 = p[48].ToString().Trim(),
                    DTCDNLH2 = p[49].ToString().Trim(),
                    EmailNLH2 = p[50].ToString().Trim(),
                    DiaChiLH2 = p[51].ToString().Trim(),
                    NguoiLH3 = p[52].ToString().Trim(),
                    MoiQuanHe3 = p[53].ToString().Trim(),
                    DiDongNLH3 = p[54].ToString().Trim(),
                    DTCDNLH3 = p[55].ToString().Trim(),
                    EmailNLH3 = p[56].ToString().Trim(),
                    DiaChiLH3 = p[57].ToString().Trim(),
                    NguoiLH4 = p[58].ToString().Trim(),
                    MoiQuanHe4 = p[59].ToString().Trim(),
                    DiDongNLH4 = p[60].ToString().Trim(),
                    DTCDNLH4 = p[61].ToString().Trim(),
                    EmailNLH4 = p[62].ToString().Trim(),
                    DiaChiLH4 = p[63].ToString().Trim(),
                    NguoiLH5 = p[64].ToString().Trim(),
                    MoiQuanHe5 = p[65].ToString().Trim(),
                    DiDongNLH5 = p[66].ToString().Trim(),
                    DTCDNLH5 = p[67].ToString().Trim(),
                    EmailNLH5 = p[68].ToString().Trim(),
                    DiaChiLH5 = p[69].ToString().Trim(),
                    KyHieu = p[70].ToString().Trim(),
                    NhanVienMG = p[71].ToString().Trim(),
                    NhanVienQL = p[72].ToString().Trim(),
                    LoaiBDS = p[73].ToString().Trim(),
                    ToaDo = p[74].ToString().Trim(),
                    PhiMG = p[75].ToString().Trim(),
                    KyHDMG = p[76].ToString().Trim(),
                    PN = p[77].ToString().Trim(),
                    PVS = p[78].ToString().Trim(),
                    HuongCua = p[79].ToString().Trim(),
                    ThoiGianBG = p[80].ToString().Trim(),
                    Nguon = p[81].ToString().Trim(),
                    PhapLy = p[82].ToString().Trim(),
                    LoaiTien = p[83].ToString().Trim(),
                    Error = ""
                }).ToList();

                var listCus = new List<Item>();
                foreach (var r in list)
                {
                    var o = new Item();
                    o.NhuCau = r.NhuCau;
                    o.NgayDK = r.NgayDK;
                    o.SoNha = r.SoNha;
                    o.TenDuong = r.TenDuong;
                    o.Xa = r.Xa;
                    o.Huyen = r.Huyen;
                    o.Tinh = r.Tinh;
                    o.MatTien = r.MatTien;
                    o.DienTichDat = r.DienTichDat;
                    o.DienTichXD = r.DienTichXD;
                    o.SoTang = r.SoTang;
                    o.DienTichBC = r.DienTichBC;
                    o.SoTangXD = r.SoTangXD;
                    o.GiaTien = r.GiaTien;
                    o.ThanhTien = r.ThanhTien;
                    o.MatTien1 = r.MatTien1;
                    o.DienTich1 = r.DienTich1;
                    o.GiaTien1 = r.GiaTien1;
                    o.SoTang1 = r.SoTang1;
                    o.MatTien2 = r.MatTien2;
                    o.DienTich2 = r.DienTich2;
                    o.GiaTien2 = r.GiaTien2;
                    o.SoTang2 = r.SoTang2;
                    o.MatTien3 = r.MatTien3;
                    o.DienTich3 = r.DienTich3;
                    o.GiaTien3 = r.GiaTien3;
                    o.SoTang3 = r.SoTang3;
                    o.DacTrung = r.DacTrung;
                    o.GhiChu = r.GhiChu;
                    o.TrangThai = r.TrangThai;
                    o.DonViTC = r.DonViTC;
                    o.DonViDT = r.DonViDT;
                    o.ThoiHanHD = r.ThoiHanHD;
                    o.HoTenKH = r.HoTenKH;
                    o.DienThoai = r.DienThoai;
                    o.DienThoai2 = r.DienThoai2;
                    o.DienThoai3 = r.DienThoai3;
                    o.DienThoai4 = r.DienThoai4;
                    o.CMND = r.CMND;
                    o.DiaChiKH = r.DiaChiKH;
                    o.NguoiLH1 = r.NguoiLH1;
                    o.MoiQuanHe1 = r.MoiQuanHe1;
                    o.DiDongNLH1 = r.DiDongNLH1;
                    o.DTCDNLH1 = r.DTCDNLH1;
                    o.EmailNLH1 = r.EmailNLH1;
                    o.DiaChiLH1 = r.DiaChiLH1;
                    o.NguoiLH2 = r.NguoiLH2;
                    o.MoiQuanHe2 = r.MoiQuanHe2;
                    o.DiDongNLH2 = r.DiDongNLH2;
                    o.DTCDNLH2 = r.DTCDNLH2;
                    o.EmailNLH2 = r.EmailNLH2;
                    o.DiaChiLH2 = r.DiaChiLH2;
                    o.NguoiLH3 = r.NguoiLH3;
                    o.MoiQuanHe3 = r.MoiQuanHe3;
                    o.DiDongNLH3 = r.DiDongNLH3;
                    o.DTCDNLH3 = r.DTCDNLH3;
                    o.EmailNLH3 = r.EmailNLH3;
                    o.DiaChiLH3 = r.DiaChiLH3;
                    o.NguoiLH4 = r.NguoiLH4;
                    o.MoiQuanHe4 = r.MoiQuanHe4;
                    o.DiDongNLH4 = r.DiDongNLH4;
                    o.DTCDNLH4 = r.DTCDNLH4;
                    o.EmailNLH4 = r.EmailNLH4;
                    o.DiaChiLH4 = r.DiaChiLH4;
                    o.NguoiLH5 = r.NguoiLH5;
                    o.MoiQuanHe5 = r.MoiQuanHe5;
                    o.DiDongNLH5 = r.DiDongNLH5;
                    o.DTCDNLH5 = r.DTCDNLH5;
                    o.EmailNLH5 = r.EmailNLH5;
                    o.DiaChiLH5 = r.DiaChiLH5;
                    o.KyHieu = r.KyHieu;
                    o.NhanVienMG = r.NhanVienMG;
                    o.NhanVienQL = r.NhanVienQL;
                    o.LoaiBDS = r.LoaiBDS;
                    o.ToaDo = r.ToaDo;
                    o.PhiMG = r.PhiMG;
                    o.KyHDMG = r.KyHDMG;
                    o.PN = r.PN;
                    o.PVS = r.PVS;
                    o.HuongCua = r.HuongCua;
                    o.ThoiGianBG = r.ThoiGianBG;
                    o.Nguon = r.Nguon;
                    o.PhapLy = r.PhapLy;
                    o.LoaiTien = r.LoaiTien;
                    o.Error = r.Error;

                    listCus.Add(o);
                }

                gcSP.DataSource = listCus;
            }
            else
            {
                gcSP.DataSource = null;
            }
        }

        private void itemCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wait = DialogBox.WaitingForm(); int iRow = 1;
            MasterDataContext db = new MasterDataContext();
            try
            {
                int maXa;
                byte maLT, maTinh;
                short maHuyen, maCD, maLBDS;

                var lstKh = db.KhachHangs.ToList();

                var tableError = AddColumn();
                tableError.TableName = "Data";

                DataRow rowError;
             
                bool isError = false;
                bool isEnable = false;

                for (int i = 0; i < grvSP.RowCount; i++)
                {
                    isError = false;
                    iRow += 1;
                    rowError = tableError.NewRow();
                    rowError["SttError"] = iRow;

                    grvSP.UnselectRow(i);
                    #region ràng buộc
                    if (grvSP.GetRowCellValue(i, "SoNha").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập Số nhà");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    if (grvSP.GetRowCellValue(i, "NhuCau").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập nhu cầu");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    if (grvSP.GetRowCellValue(i, "LoaiBDS").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập loại bất động sản");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    else
                    {
                        try
                        {
                            maLBDS = db.LoaiBDs.Single(p => p.TenLBDS == grvSP.GetRowCellValue(i, "LoaiBDS").ToString().Trim()).MaLBDS;
                        }
                        catch
                        {
                            DialogBox.Error("Loại bất động sản không chính xác");
                            grvSP.FocusedRowHandle = i;
                            return;
                        }
                    }


                    if (grvSP.GetRowCellValue(i, "Tinh").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập tỉnh");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    else
                    {
                        try
                        {
                            maTinh = db.Tinhs.Single(p => p.TenTinh == grvSP.GetRowCellValue(i, "Tinh").ToString().Trim()).MaTinh;
                        }
                        catch
                        {
                            DialogBox.Error("tỉnh không chính xác");
                            grvSP.FocusedRowHandle = i;
                            return;

                        }
                    }

                    if (grvSP.GetRowCellValue(i, "Huyen").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập Huyện");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    else
                    {
                        try
                        {
                            maHuyen = db.Huyens.Where(p => p.MaTinh == maTinh).Single(p => p.TenHuyen == grvSP.GetRowCellValue(i, "Huyen").ToString().Trim()).MaHuyen;
                        }
                        catch
                        {
                            DialogBox.Error("huyện không chính xác");
                            grvSP.FocusedRowHandle = i;
                            return;
                        }
                    }


                    if (grvSP.GetRowCellValue(i, "Xa").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập xã");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    else
                    {
                        try
                        {
                            maXa = db.Xas.Where(p => p.MaHuyen == maHuyen).Single(p => p.TenXa.ToUpper() == grvSP.GetRowCellValue(i, "Xa").ToString().ToUpper()).MaXa;
                        }
                        catch
                        {
                            DialogBox.Error("Xã không chính xác");
                            grvSP.FocusedRowHandle = i;
                            return;

                        }
                    }

                    if (grvSP.GetRowCellValue(i, "LoaiTien").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập loại tiền");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    else
                    {
                        try
                        {
                            maLT = db.LoaiTiens.Single(p => p.TenLoaiTien == grvSP.GetRowCellValue(i, "LoaiTien").ToString().Trim()).MaLoaiTien;
                        }
                        catch
                        {
                            DialogBox.Error("Loại tiền không chính xác");
                            grvSP.FocusedRowHandle = i;
                            return;
                        }
                    }

                    if (grvSP.GetRowCellValue(i, "HoTenKH").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập họ tên khách hàng");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    if (grvSP.GetRowCellValue(i, "DienThoai").ToString().Trim() == "" && grvSP.GetRowCellValue(i, "CMND").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập điện thoại hoặc số CMND");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    #endregion

                    #region check cũ
                    var objKH = new BEE.ThuVien.KhachHang();
                    string hoTenKH = grvSP.GetRowCellValue(i, "HoTenKH").ToString().Trim();
                    try
                    {
                        objKH.HoKH = hoTenKH.Substring(0, hoTenKH.LastIndexOf(' '));
                        objKH.TenKH = hoTenKH.Substring(hoTenKH.LastIndexOf(' ') + 1);
                    }
                    catch 
                    {
                        DialogBox.Error($"Row: {iRow} lỗi. Cần phân biệt rõ họ tên khách hàng."); return;
                    }
                   

                    mglbcBanChoThue objBC = new mglbcBanChoThue();
                    if (grvSP.GetRowCellValue(i, "NgayDK") != "")
                        objBC.NgayDK = Convert.ToDateTime(grvSP.GetRowCellValue(i, "NgayDK"));
                    objBC.NgayCN = DateTime.Now;
                    if (grvSP.GetRowCellValue(i, "NhanVienMG") != "" && grvSP.GetRowCellValue(i, "NhanVienQL") != "")
                    {
                        try
                        {
                            objBC.MaNVKD = db.NhanViens.Single(p => p.MaSo.ToUpper() == grvSP.GetRowCellValue(i, "NhanVienMG").ToString().ToUpper()).MaNV;
                        }
                        catch { DialogBox.Error("Nhân viên MG chưa được thiết lập. Vui lòng thiết lập trước"); return; }
                        try
                        {
                            objBC.MaNVQL = db.NhanViens.Single(p => p.MaSo.ToUpper() == grvSP.GetRowCellValue(i, "NhanVienQL").ToString().ToUpper()).MaNV;
                        }
                        catch { DialogBox.Error("Nhân viên QL được thiết lập. Vui lòng thiết lập trước"); return; }
                    }
                    else
                    {
                        DialogBox.Error("Vui lòng nhập nhân viên. Xin cảm ơn!"); return;
                    }
                    //Bat dong san
                    objBC.KyHieu = grvSP.GetRowCellValue(i, "KyHieu").ToString();
                    objBC.SoNha = grvSP.GetRowCellValue(i, "SoNha").ToString();
                    objBC.IsBan = grvSP.GetRowCellValue(i, "NhuCau").ToString().Trim() == "Cần bán" ? true : false;
                    if (grvSP.GetRowCellValue(i, "TenDuong") != "")
                    {
                        try
                        {
                            objBC.MaDuong = db.Duongs.Single(p => p.TenDuong.ToUpper() == grvSP.GetRowCellValue(i, "TenDuong").ToString().ToUpper()).MaDuong;
                        }
                        catch { DialogBox.Error("Đường chưa được thiết lập. Vui lòng thiết lập trước"); return; }
                    }

                    objBC.MaXa = maXa;
                    objBC.MaHuyen = maHuyen;
                    objBC.MaTinh = maTinh;
                    objBC.NgangXD = Convert.ToDecimal(grvSP.GetRowCellValue(i, "MatTien") == "" ? 0 : grvSP.GetRowCellValue(i, "MatTien"));
                    objBC.DienTichDat = Convert.ToDecimal(grvSP.GetRowCellValue(i, "DienTichDat") == "" ? 0 : grvSP.GetRowCellValue(i, "DienTichDat"));
                    objBC.DienTichXD = Convert.ToDecimal(grvSP.GetRowCellValue(i, "DienTichXD") == "" ? 0 : grvSP.GetRowCellValue(i, "DienTichXD"));
                    objBC.DienTich = Convert.ToDecimal(grvSP.GetRowCellValue(i, "DienTichBC") == "" ? 0 : grvSP.GetRowCellValue(i, "DienTichBC"));
                    objBC.SoTang = Convert.ToByte(grvSP.GetRowCellValue(i, "SoTang") == "" ? 0 : grvSP.GetRowCellValue(i, "SoTang"));
                    objBC.SoTangXD = Convert.ToInt32(grvSP.GetRowCellValue(i, "SoTangXD") == "" ? 0 : grvSP.GetRowCellValue(i, "SoTangXD"));
                    if (grvSP.GetRowCellValue(i, "GiaTien") != "")
                        objBC.DonGia = Convert.ToDecimal(grvSP.GetRowCellValue(i, "GiaTien"));
                    if (grvSP.GetRowCellValue(i, "ThanhTien") != "")
                        objBC.ThanhTien = Convert.ToDecimal(grvSP.GetRowCellValue(i, "ThanhTien"));
                    objBC.DacTrung = grvSP.GetRowCellValue(i, "DacTrung").ToString();
                    objBC.GhiChu = grvSP.GetRowCellValue(i, "GhiChu").ToString();
                    if (grvSP.GetRowCellValue(i, "TrangThai") != "")
                    {
                        try
                        {
                            objBC.MaTT = db.mglbcTrangThais.Single(p => p.TenTT.ToUpper() == grvSP.GetRowCellValue(i, "TrangThai").ToString().ToUpper()).MaTT;
                        }
                        catch { DialogBox.Error("Trạng thái chưa được thiết lập. Vui lòng thiết lập trước"); return; }
                    }
                    else
                    {
                        DialogBox.Error("Vui lòng nhập trạng thái. xin cảm ơn!");
                        return;
                    }

                    objBC.DonViThueCu = (string)grvSP.GetRowCellValue(i, "DonViTC");
                    objBC.DonViDangThue = (string)grvSP.GetRowCellValue(i, "DonViDT");
                    if (grvSP.GetRowCellValue(i, "ThoiHanHD") != "")
                        objBC.ThoiGianHD = Convert.ToDateTime(grvSP.GetRowCellValue(i, "ThoiHanHD"));
                    objBC.MaLBDS = maLBDS;
                    objBC.MaLT = maLT;
                    var toado = grvSP.GetRowCellValue(i, "ToaDo").ToString();
                    objBC.ToaDo = toado;
                    if (toado != "")
                    {
                        var nam = toado.Substring(0, 12);
                        var bac = toado.Substring(toado.Length - 13);
                        objBC.KinhDo = nam;
                        objBC.ViDo = bac;
                    }
                    if (grvSP.GetRowCellValue(i, "PhiMG") != "")
                        objBC.PhiMG = Convert.ToDecimal(grvSP.GetRowCellValue(i, "PhiMG"));
                    if (grvSP.GetRowCellValue(i, "KyHDMG") != "")
                    {
                        try
                        {
                            objBC.TrangThaiHDMG = db.mglbcTrangThaiHDMGs.Single(p => p.TenTT.ToString().ToUpper() == grvSP.GetRowCellValue(i, "KyHDMG").ToString().ToUpper()).ID;
                        }
                        catch { DialogBox.Error("KyHDMG chưa được thiết lập. Vui lòng thiết lập trước"); return; }
                    }

                    if (grvSP.GetRowCellValue(i, "PN") != "")
                        objBC.PhongNgu = Convert.ToByte(grvSP.GetRowCellValue(i, "PN"));
                    if (grvSP.GetRowCellValue(i, "PVS") != "")
                        objBC.PhongVS = Convert.ToByte(grvSP.GetRowCellValue(i, "PVS"));
                    if (grvSP.GetRowCellValue(i, "HuongCua") != "")
                    {
                        try
                        {
                            objBC.MaHuong = db.PhuongHuongs.Single(p => p.TenPhuongHuong.ToString().ToUpper() == grvSP.GetRowCellValue(i, "HuongCua").ToString().ToUpper()).MaPhuongHuong;
                        }
                        catch { DialogBox.Error("Hướng chưa được thiết lập. Vui lòng thiết lập trước"); return; }
                    }

                    if (grvSP.GetRowCellValue(i, "ThoiGianBG") != "")
                        objBC.ThoiGianBGMB = Convert.ToDateTime(grvSP.GetRowCellValue(i, "ThoiGianBG"));
                    if (grvSP.GetRowCellValue(i, "Nguon") != "")
                    {
                        try
                        {
                            objBC.MaNguon = db.mglNguons.Single(p => p.TenNguon.ToUpper() == grvSP.GetRowCellValue(i, "Nguon").ToString().ToUpper()).MaNguon;
                        }
                        catch { DialogBox.Error("Nguồn chưa được thiết lập. Vui lòng thiết lập trước"); return; }
                    }
                    if (grvSP.GetRowCellValue(i, "PhapLy") != "")
                    {
                        try
                        {
                            objBC.MaPL = db.PhapLies.Single(p => p.TenPL.ToUpper() == grvSP.GetRowCellValue(i, "PhapLy").ToString().ToUpper()).MaPL;
                        }
                        catch { DialogBox.Error("Pháp lý chưa được thiết lập. Vui lòng thiết lập trước"); return; }
                    }

                    #endregion


                    var dienThoai = grvSP.GetRowCellValue(i, "DienThoai");
                    var dienThoai2 = grvSP.GetRowCellValue(i, "DienThoai2");
                    var dienThoai3 = grvSP.GetRowCellValue(i, "DienThoai3");
                    var dienThoai4 = grvSP.GetRowCellValue(i, "DienThoai4");

                    var lstBc = db.mglbcBanChoThues.Where(p => p.SoNha == objBC.SoNha && p.MaDuong == objBC.MaDuong
                && p.MaHuyen == objBC.MaHuyen && p.MaTinh == objBC.MaTinh).ToList();
                    if (lstBc.Count > 0)
                    {
                        var lstMaKH = lstBc.Select(p => p.MaKH);
                        var lstKhNew = lstKh.Where(p => lstMaKH.Contains(p.MaKH)).ToList();

                        var checkDienthoai1 = CheckAllPhoneExits(dienThoai.ToString(), lstKhNew);
                        var checkDienthoai2 = CheckAllPhoneExits(dienThoai2.ToString(), lstKhNew);
                        var checkDienthoai3 = CheckAllPhoneExits(dienThoai3.ToString(), lstKhNew);
                        var checkDienthoai4 = CheckAllPhoneExits(dienThoai4.ToString(), lstKhNew);

                        if (checkDienthoai1 || checkDienthoai2 || checkDienthoai3 || checkDienthoai4)
                        {
                            isEnable = true;
                            isError = true;
                            rowError["TrangThaiImport"] = "Thông tin đã có trên hệ thống HOALAND";

                        }
                    }

                    #region thêm dữ liệu vào data lỗi

                    rowError["NhuCau"] = grvSP.GetRowCellValue(i, "NhuCau");
                    rowError["NgayDK"] = grvSP.GetRowCellValue(i, "NgayDK");
                    rowError["SoNha"] = grvSP.GetRowCellValue(i, "SoNha");
                    rowError["TenDuong"] = grvSP.GetRowCellValue(i, "TenDuong");
                    rowError["PhuongXa"] = grvSP.GetRowCellValue(i, "Xa");
                    rowError["Huyen"] = grvSP.GetRowCellValue(i, "Huyen");
                    rowError["Tinh"] = grvSP.GetRowCellValue(i, "Tinh");
                    rowError["MatTien"] = grvSP.GetRowCellValue(i, "MatTien");
                    rowError["DienTichDat"] = grvSP.GetRowCellValue(i, "DienTichDat");
                    rowError["DienTichXD"] = grvSP.GetRowCellValue(i, "DienTichXD");
                    rowError["SoTang"] = grvSP.GetRowCellValue(i, "SoTang");
                    rowError["DienTichBC"] = grvSP.GetRowCellValue(i, "DienTichBC");
                    rowError["SoTangXD"] = grvSP.GetRowCellValue(i, "SoTangXD");
                    rowError["GiaTien"] = grvSP.GetRowCellValue(i, "GiaTien");
                    rowError["ThanhTien"] = grvSP.GetRowCellValue(i, "ThanhTien");
                    rowError["MatTien1"] = grvSP.GetRowCellValue(i, "MatTien1");
                    rowError["DienTich1"] = grvSP.GetRowCellValue(i, "DienTich1");
                    rowError["GiaTien1"] = grvSP.GetRowCellValue(i, "GiaTien1");
                    rowError["SoTang1"] = grvSP.GetRowCellValue(i, "SoTang1");
                    rowError["MatTien2"] = grvSP.GetRowCellValue(i, "MatTien2");
                    rowError["DienTich2"] = grvSP.GetRowCellValue(i, "DienTich2");
                    rowError["GiaTien2"] = grvSP.GetRowCellValue(i, "GiaTien2");
                    rowError["SoTang2"] = grvSP.GetRowCellValue(i, "SoTang2");
                    rowError["MatTien3"] = grvSP.GetRowCellValue(i, "MatTien3");
                    rowError["DienTich3"] = grvSP.GetRowCellValue(i, "DienTich3");
                    rowError["GiaTien3"] = grvSP.GetRowCellValue(i, "GiaTien3");
                    rowError["SoTang3"] = grvSP.GetRowCellValue(i, "SoTang3");
                    rowError["DacTrung"] = grvSP.GetRowCellValue(i, "DacTrung");
                    rowError["GhiChu"] = grvSP.GetRowCellValue(i, "GhiChu");
                    rowError["TrangThai"] = grvSP.GetRowCellValue(i, "TrangThai");
                    rowError["DonViTC"] = grvSP.GetRowCellValue(i, "DonViTC");
                    rowError["DonViDT"] = grvSP.GetRowCellValue(i, "DonViDT");
                    rowError["ThoiHanHD"] = grvSP.GetRowCellValue(i, "ThoiHanHD");
                    rowError["HoTenKH"] = grvSP.GetRowCellValue(i, "HoTenKH");
                    rowError["DienThoai"] = grvSP.GetRowCellValue(i, "DienThoai");
                    rowError["DienThoai2"] = grvSP.GetRowCellValue(i, "DienThoai2");
                    rowError["DienThoai3"] = grvSP.GetRowCellValue(i, "DienThoai3");
                    rowError["DienThoai4"] = grvSP.GetRowCellValue(i, "DienThoai4");
                    rowError["CMND"] = grvSP.GetRowCellValue(i, "CMND");
                    rowError["DiaChiKH"] = grvSP.GetRowCellValue(i, "DiaChiKH");
                    rowError["NguoiLH1"] = grvSP.GetRowCellValue(i, "NguoiLH1");
                    rowError["MoiQuanHe1"] = grvSP.GetRowCellValue(i, "MoiQuanHe1");
                    rowError["DiDongNLH1"] = grvSP.GetRowCellValue(i, "DiDongNLH1");
                    rowError["DTCDNLH1"] = grvSP.GetRowCellValue(i, "DTCDNLH1");
                    rowError["EmailNLH1"] = grvSP.GetRowCellValue(i, "EmailNLH1");
                    rowError["DiaChiLH1"] = grvSP.GetRowCellValue(i, "DiaChiLH1");
                    rowError["NguoiLH2"] = grvSP.GetRowCellValue(i, "NguoiLH2");
                    rowError["MoiQuanHe2"] = grvSP.GetRowCellValue(i, "MoiQuanHe2");
                    rowError["DiDongNLH2"] = grvSP.GetRowCellValue(i, "DiDongNLH2");
                    rowError["DTCDNLH2"] = grvSP.GetRowCellValue(i, "DTCDNLH2");
                    rowError["EmailNLH2"] = grvSP.GetRowCellValue(i, "EmailNLH2");
                    rowError["DiaChiLH2"] = grvSP.GetRowCellValue(i, "DiaChiLH2");
                    rowError["NguoiLH3"] = grvSP.GetRowCellValue(i, "NguoiLH3");
                    rowError["MoiQuanHe3"] = grvSP.GetRowCellValue(i, "MoiQuanHe3");
                    rowError["DiDongNLH3"] = grvSP.GetRowCellValue(i, "DiDongNLH3");
                    rowError["DTCDNLH3"] = grvSP.GetRowCellValue(i, "DTCDNLH3");
                    rowError["EmailNLH3"] = grvSP.GetRowCellValue(i, "EmailNLH3");
                    rowError["DiaChiLH3"] = grvSP.GetRowCellValue(i, "DiaChiLH3");
                    rowError["NguoiLH4"] = grvSP.GetRowCellValue(i, "NguoiLH4");
                    rowError["MoiQuanHe4"] = grvSP.GetRowCellValue(i, "MoiQuanHe4");
                    rowError["DiDongNLH4"] = grvSP.GetRowCellValue(i, "DiDongNLH4");
                    rowError["DTCDNLH4"] = grvSP.GetRowCellValue(i, "DTCDNLH4");
                    rowError["EmailNLH4"] = grvSP.GetRowCellValue(i, "EmailNLH4");
                    rowError["DiaChiLH4"] = grvSP.GetRowCellValue(i, "DiaChiLH4");
                    rowError["NguoiLH5"] = grvSP.GetRowCellValue(i, "NguoiLH5");
                    rowError["MoiQuanHe5"] = grvSP.GetRowCellValue(i, "MoiQuanHe5");
                    rowError["DiDongNLH5"] = grvSP.GetRowCellValue(i, "DiDongNLH5");
                    rowError["DTCDNLH5"] = grvSP.GetRowCellValue(i, "DTCDNLH5");
                    rowError["EmailNLH5"] = grvSP.GetRowCellValue(i, "EmailNLH5");
                    rowError["DiaChiLH5"] = grvSP.GetRowCellValue(i, "DiaChiLH5");
                    rowError["KyHieu"] = grvSP.GetRowCellValue(i, "KyHieu");
                    rowError["NhanVienMG"] = grvSP.GetRowCellValue(i, "NhanVienMG");
                    rowError["NhanVienQL"] = grvSP.GetRowCellValue(i, "NhanVienQL");
                    rowError["LoaiBDS"] = grvSP.GetRowCellValue(i, "LoaiBDS");
                    rowError["ToaDo"] = grvSP.GetRowCellValue(i, "ToaDo");
                    rowError["PhiMG"] = grvSP.GetRowCellValue(i, "PhiMG");
                    rowError["KyHDMG"] = grvSP.GetRowCellValue(i, "KyHDMG");
                    rowError["PN"] = grvSP.GetRowCellValue(i, "PN");
                    rowError["PVS"] = grvSP.GetRowCellValue(i, "PVS");
                    rowError["HuongCua"] = grvSP.GetRowCellValue(i, "HuongCua");
                    rowError["ThoiGianBG"] = grvSP.GetRowCellValue(i, "ThoiGianBG");
                    rowError["Nguon"] = grvSP.GetRowCellValue(i, "Nguon");
                    rowError["PhapLy"] = grvSP.GetRowCellValue(i, "PhapLy");
                    rowError["LoaiTien"] = grvSP.GetRowCellValue(i, "LoaiTien");
                    #endregion

                    if (isError)
                    {
                        tableError.Rows.Add(rowError);
                    }
                    else
                    {
                        var checkDienthoai1 = CheckAllPhoneExits(dienThoai.ToString(), lstKh);
                        var checkDienthoai2 = CheckAllPhoneExits(dienThoai2.ToString(), lstKh);
                        var checkDienthoai3 = CheckAllPhoneExits(dienThoai3.ToString(), lstKh);
                        var checkDienthoai4 = CheckAllPhoneExits(dienThoai4.ToString(), lstKh);
                        if (checkDienthoai1 || checkDienthoai2 || checkDienthoai3 || checkDienthoai4)
                        {
                            rowError["TrangThaiImport"] = "Khách hàng lấy từ hệ thống";
                        }
                        else
                        {
                            rowError["TrangThaiImport"] = "Khách hàng mới";
                        }
                        tableError.Rows.Add(rowError);
                    }
                    //Khach hang
                    objBC.MaTTD = 2;
                    objBC.NgayNhap = DateTime.Now;

                    grvSP.SelectRow(i);
                }

                if (isEnable)
                {
                    itemLuu.Enabled = false;
                }
                else
                {
                    itemLuu.Enabled = true;
                }

                it.CommonCls.ExportExcel(tableError);
                this.IsSave = true;
                DialogBox.Infomation("Dữ liệu đúng đã được bôi đậm");
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message+ iRow);
            }
            finally
            {
                db.Dispose();
                wait.Close();
            }
        }

        public bool CheckAllPhoneExits(string phone, List<BEE.ThuVien.KhachHang> lstKH)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                if (!string.IsNullOrEmpty(phone))
                {
                    var exits = lstKH.Where(p => (p.DiDong == phone || p.DiDong2 == phone || p.DiDong3 == phone || p.DiDong4 == phone
                                                                || p.DienThoaiCT == phone)).Count();
                    return exits > 0 ? true : false;
                }

                return false;
            }

        }

        public DataTable AddColumn()
        {
            DataTable table = new DataTable();
            table.Columns.Add("NhuCau", typeof(string));
            table.Columns.Add("NgayDK", typeof(string));
            table.Columns.Add("SoNha", typeof(string));
            table.Columns.Add("TenDuong", typeof(string));
            table.Columns.Add("PhuongXa", typeof(string));
            table.Columns.Add("Huyen", typeof(string));
            table.Columns.Add("Tinh", typeof(string));
            table.Columns.Add("MatTien", typeof(string));
            table.Columns.Add("DienTichDat", typeof(string));
            table.Columns.Add("DienTichXD", typeof(string));
            table.Columns.Add("SoTang", typeof(string));
            table.Columns.Add("DienTichBC", typeof(string));
            table.Columns.Add("SoTangXD", typeof(string));
            table.Columns.Add("GiaTien", typeof(string));
            table.Columns.Add("ThanhTien", typeof(string));
            table.Columns.Add("MatTien1", typeof(string));
            table.Columns.Add("DienTich1", typeof(string));
            table.Columns.Add("GiaTien1", typeof(string));
            table.Columns.Add("SoTang1", typeof(string));
            table.Columns.Add("MatTien2", typeof(string));
            table.Columns.Add("DienTich2", typeof(string));
            table.Columns.Add("GiaTien2", typeof(string));
            table.Columns.Add("SoTang2", typeof(string));
            table.Columns.Add("MatTien3", typeof(string));
            table.Columns.Add("DienTich3", typeof(string));
            table.Columns.Add("GiaTien3", typeof(string));
            table.Columns.Add("SoTang3", typeof(string));
            table.Columns.Add("DacTrung", typeof(string));
            table.Columns.Add("GhiChu", typeof(string));
            table.Columns.Add("TrangThai", typeof(string));
            table.Columns.Add("DonViTC", typeof(string));
            table.Columns.Add("DonViDT", typeof(string));
            table.Columns.Add("ThoiHanHD", typeof(string));
            table.Columns.Add("HoTenKH", typeof(string));
            table.Columns.Add("DienThoai", typeof(string));
            table.Columns.Add("DienThoai2", typeof(string));
            table.Columns.Add("DienThoai3", typeof(string));
            table.Columns.Add("DienThoai4", typeof(string));
            table.Columns.Add("CMND", typeof(string));
            table.Columns.Add("DiaChiKH", typeof(string));
            table.Columns.Add("NguoiLH1", typeof(string));
            table.Columns.Add("MoiQuanHe1", typeof(string));
            table.Columns.Add("DiDongNLH1", typeof(string));
            table.Columns.Add("DTCDNLH1", typeof(string));
            table.Columns.Add("EmailNLH1", typeof(string));
            table.Columns.Add("DiaChiLH1", typeof(string));
            table.Columns.Add("NguoiLH2", typeof(string));
            table.Columns.Add("MoiQuanHe2", typeof(string));
            table.Columns.Add("DiDongNLH2", typeof(string));
            table.Columns.Add("DTCDNLH2", typeof(string));
            table.Columns.Add("EmailNLH2", typeof(string));
            table.Columns.Add("DiaChiLH2", typeof(string));
            table.Columns.Add("NguoiLH3", typeof(string));
            table.Columns.Add("MoiQuanHe3", typeof(string));
            table.Columns.Add("DiDongNLH3", typeof(string));
            table.Columns.Add("DTCDNLH3", typeof(string));
            table.Columns.Add("EmailNLH3", typeof(string));
            table.Columns.Add("DiaChiLH3", typeof(string));
            table.Columns.Add("NguoiLH4", typeof(string));
            table.Columns.Add("MoiQuanHe4", typeof(string));
            table.Columns.Add("DiDongNLH4", typeof(string));
            table.Columns.Add("DTCDNLH4", typeof(string));
            table.Columns.Add("EmailNLH4", typeof(string));
            table.Columns.Add("DiaChiLH4", typeof(string));
            table.Columns.Add("NguoiLH5", typeof(string));
            table.Columns.Add("MoiQuanHe5", typeof(string));
            table.Columns.Add("DiDongNLH5", typeof(string));
            table.Columns.Add("DTCDNLH5", typeof(string));
            table.Columns.Add("EmailNLH5", typeof(string));
            table.Columns.Add("DiaChiLH5", typeof(string));
            table.Columns.Add("KyHieu", typeof(string));
            table.Columns.Add("NhanVienMG", typeof(string));
            table.Columns.Add("NhanVienQL", typeof(string));
            table.Columns.Add("LoaiBDS", typeof(string));
            table.Columns.Add("ToaDo", typeof(string));
            table.Columns.Add("PhiMG", typeof(string));
            table.Columns.Add("KyHDMG", typeof(string));
            table.Columns.Add("PN", typeof(string));
            table.Columns.Add("PVS", typeof(string));
            table.Columns.Add("HuongCua", typeof(string));
            table.Columns.Add("ThoiGianBG", typeof(string));
            table.Columns.Add("Nguon", typeof(string));
            table.Columns.Add("PhapLy", typeof(string));
            table.Columns.Add("LoaiTien", typeof(string));
            table.Columns.Add("SttError", typeof(int));
            table.Columns.Add("TrangThaiImport", typeof(string));

            return table;
        }
    }
    class Item
    {
        public string NhuCau { get; set; }
        public string NgayDK { get; set; }
        public string SoNha { get; set; }
        public string TenDuong { get; set; }
        public string Xa { get; set; }
        public string Huyen { get; set; }
        public string Tinh { get; set; }
        public string MatTien { get; set; }
        public string DienTichDat { get; set; }
        public string DienTichXD { get; set; }
        public string SoTang { get; set; }
        public string DienTichBC { get; set; }
        public string SoTangXD { get; set; }
        public string GiaTien { get; set; }
        public string ThanhTien { get; set; }
        public string MatTien1 { get; set; }
        public string DienTich1 { get; set; }
        public string GiaTien1 { get; set; }
        public string SoTang1 { get; set; }
        public string MatTien2 { get; set; }
        public string DienTich2 { get; set; }
        public string GiaTien2 { get; set; }
        public string SoTang2 { get; set; }
        public string MatTien3 { get; set; }
        public string DienTich3 { get; set; }
        public string GiaTien3 { get; set; }
        public string SoTang3 { get; set; }
        public string DacTrung { get; set; }
        public string GhiChu { get; set; }
        public string TrangThai { get; set; }
        public string DonViTC { get; set; }
        public string DonViDT { get; set; }
        public string ThoiHanHD { get; set; }
        public string HoTenKH { get; set; }
        public string DienThoai { get; set; }
        public string DienThoai2 { get; set; }
        public string DienThoai3 { get; set; }
        public string DienThoai4 { get; set; }
        public string CMND { get; set; }
        public string DiaChiKH { get; set; }
        public string NguoiLH1 { get; set; }
        public string MoiQuanHe1 { get; set; }
        public string DiDongNLH1 { get; set; }
        public string DTCDNLH1 { get; set; }
        public string EmailNLH1 { get; set; }
        public string DiaChiLH1 { get; set; }
        public string NguoiLH2 { get; set; }
        public string MoiQuanHe2 { get; set; }
        public string DiDongNLH2 { get; set; }
        public string DTCDNLH2 { get; set; }
        public string EmailNLH2 { get; set; }
        public string DiaChiLH2 { get; set; }
        public string NguoiLH3 { get; set; }
        public string MoiQuanHe3 { get; set; }
        public string DiDongNLH3 { get; set; }
        public string DTCDNLH3 { get; set; }
        public string EmailNLH3 { get; set; }
        public string DiaChiLH3 { get; set; }
        public string NguoiLH4 { get; set; }
        public string MoiQuanHe4 { get; set; }
        public string DiDongNLH4 { get; set; }
        public string DTCDNLH4 { get; set; }
        public string EmailNLH4 { get; set; }
        public string DiaChiLH4 { get; set; }
        public string NguoiLH5 { get; set; }
        public string MoiQuanHe5 { get; set; }
        public string DiDongNLH5 { get; set; }
        public string DTCDNLH5 { get; set; }
        public string EmailNLH5 { get; set; }
        public string DiaChiLH5 { get; set; }
        public string KyHieu { get; set; }
        public string NhanVienMG { get; set; }
        public string NhanVienQL { get; set; }
        public string LoaiBDS { get; set; }
        public string ToaDo { get; set; }
        public string PhiMG { get; set; }
        public string KyHDMG { get; set; }
        public string PN { get; set; }
        public string PVS { get; set; }
        public string HuongCua { get; set; }
        public string ThoiGianBG { get; set; }
        public string Nguon { get; set; }
        public string PhapLy { get; set; }
        public string LoaiTien { get; set; }
        public string Error { get; set; }
    }
}