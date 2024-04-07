using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;
using LinqToExcel;
using BEEREMA;

namespace BEE.KhachHang
{
    public partial class frmImportLinq : DevExpress.XtraEditors.XtraForm
    {
        dip.cmdExcel objExcel;

        public frmImportLinq()
        {
            InitializeComponent();
        }

        DateTime? getDate(string dateText)
        {
            if (dateText == "") return null;
            string[] ns = dateText.Split('/');
            if (ns.Length == 3)
            {
                if (int.Parse(ns[1]) > 12)
                    return new DateTime(int.Parse(ns[2].Substring(0, 4)), int.Parse(ns[0]), int.Parse(ns[1]));
                else
                    return new DateTime(int.Parse(ns[2].Substring(0, 4)), int.Parse(ns[1]), int.Parse(ns[0]));
            }
            else if (ns.Length == 2)
            {
                if (int.Parse(ns[1]) > 12)
                    return new DateTime(DateTime.Now.Year, int.Parse(ns[0]), int.Parse(ns[1]));
                else
                    return new DateTime(DateTime.Now.Year, int.Parse(ns[1]), int.Parse(ns[0]));
            }
            else
            {
                throw new Exception("Định dạng ngày tháng không đúng");
            }
        }

        private void itemExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                var excel = new ExcelQueryFactory(this.Tag.ToString());
                var list = excel.Worksheet(itemSheet.EditValue.ToString()).Select(p => new
                {
                    PhanLoai = p[0].ToString().Trim(),
                    QuyDanh = p[1].ToString().Trim(),
                    HoTen = p[2].ToString().Trim(),
                    NgaySinh = p[3].ToString().Trim(),
                    NoiSinh = p[4].ToString().Trim(),
                    QuocTich = p[5].ToString().Trim(),
                    CMND = p[6].ToString().Trim(),
                    NgayCap = p[7].ToString().Trim(),
                    NoiCap = p[8].ToString().Trim(),
                    NgheNghiep = p[9].ToString().Trim(),
                    ChucVu = p[10].ToString().Trim(),
                    DonViCongTac = p[11].ToString().Trim(),
                    DiaChiTT = p[12].ToString().Trim(),
                    DiaChiLH = p[13].ToString().Trim(),
                    DiDong = p[14].ToString().Trim(),
                    DienThoai = p[15].ToString().Trim(),
                    Email = p[16].ToString().Trim(),
                    NhomKhachHang = p[17].ToString().Trim(),
                    SanPham = p[18].ToString().Trim(),
                    NguonKH = p[19].ToString().Trim(),
                    NhanVien = p[20].ToString().Trim(),
                    CodeSUN = p[21].ToString().Trim(),
                    TenCongTy = p[22].ToString().Trim(),
                    SoGiayPhep = p[23].ToString().Trim(),
                    NgayCapGPKD = p[24].ToString().Trim(),
                    NoiCapGPKD = p[25].ToString().Trim(),
                    MaSoThueCT = p[26].ToString().Trim(),
                    DienThoaiCT = p[27].ToString().Trim(),
                    Fax = p[28].ToString().Trim(),
                    DiaChiCT = p[29].ToString().Trim(),
                    LoaiHinhKinhDoanh = p[30].ToString().Trim(),
                    SoTaiKhoan = p[31].ToString().Trim(),
                    NganHang = p[32].ToString().Trim(),
                    ChiNhanh = p[33].ToString().Trim(),
                    Error = ""
                }).ToList();

                var listCus = new List<Item>();
                foreach (var r in list)
                {
                    var o = new Item();
                    o.PhanLoai = r.PhanLoai;
                    o.QuyDanh = r.QuyDanh;
                    o.HoTen = r.HoTen;
                    o.NgaySinh = r.NgaySinh;
                    o.NoiSinh = r.NoiSinh;
                    o.QuocTich = r.QuocTich;
                    o.CMND = r.CMND;
                    o.NgayCap = r.NgayCap;
                    o.NoiCap = r.NoiCap;
                    o.NgheNghiep = r.NgheNghiep;
                    o.ChucVu = r.ChucVu;
                    o.DonViCongTac = r.DonViCongTac;
                    o.DiaChiTT = r.DiaChiTT;
                    o.DiaChiLH = r.DiaChiLH;
                    o.DiDong = r.DiDong;
                    o.DienThoai = r.DienThoai;
                    o.Email = r.Email;
                    o.NhomKhachHang = r.NhomKhachHang;
                    o.SanPham = r.SanPham;
                    o.NguonKH = r.NguonKH;
                    o.NhanVien = r.NhanVien;
                    o.CodeSUN = r.CodeSUN;
                    o.TenCongTy = r.TenCongTy;
                    o.SoGiayPhep = r.SoGiayPhep;
                    o.NgayCapGPKD = r.NgayCapGPKD;
                    o.NoiCapGPKD = r.NoiCapGPKD;
                    o.MaSoThueCT = r.MaSoThueCT;
                    o.DienThoaiCT = r.DienThoaiCT;
                    o.Fax = r.Fax;
                    o.DiaChiCT = r.DiaChiCT;
                    o.LoaiHinhKinhDoanh = r.LoaiHinhKinhDoanh;
                    o.SoTaiKhoan = r.SoTaiKhoan;
                    o.NganHang = r.NganHang;
                    o.ChiNhanh = r.ChiNhanh;
                    o.Error = r.Error;
                    
                    listCus.Add(o);
                }

                gcKH.DataSource = listCus;
            }
            else
            {
                gcKH.DataSource = null;
            }
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcKH.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn [Sheet], xin cảm ơn.");
                return;
            }

            var wait = DialogBox.WaitingForm();

            for (int i = 0; i < grvKH.RowCount; i++)
            {
                try
                {
                    grvKH.UnselectRow(i);

                    var hoTen = grvKH.GetRowCellValue(i, "HoTen").ToString().Trim();
                    if (hoTen == "")
                        throw new Exception("Vui lòng nhập họ tên khách hàng");
                    
                    var diDong = grvKH.GetRowCellValue(i, "DiDong").ToString().Trim();
                    var soCMND = grvKH.GetRowCellValue(i, "CMND").ToString().Trim();
                    var code = grvKH.GetRowCellValue(i, "CodeSUN").ToString().Trim();

                    using (var db = new MasterDataContext())
                    {
                        var objKH = new BEE.ThuVien.KhachHang();
                        objKH.IsPersonal = grvKH.GetRowCellValue(i, "PhanLoai").ToString().Trim().ToLower() == "cá nhân" ? true : false;
                        
                        if (objKH.IsPersonal.GetValueOrDefault())
                        {
                            if (hoTen.Split(' ').Length > 1)
                            {
                                objKH.HoKH = hoTen.Substring(0, hoTen.LastIndexOf(' '));
                                objKH.TenKH = hoTen.Substring(hoTen.LastIndexOf(' ') + 1);
                            }
                            else
                            {
                                objKH.HoKH = "";
                                objKH.TenKH = hoTen;
                            }
                        }
                        else
                        {
                            var congTy = grvKH.GetRowCellValue(i, "TenCongTy").ToString().Trim();
                            if (congTy == "")
                                throw new Exception("Vui lòng nhập [TenCongTy]");

                            var maSoThue = grvKH.GetRowCellValue(i, "MaSoThueCT").ToString().Trim();
                            if (maSoThue == "")
                                throw new Exception("Vui lòng nhập [MaSoThueCT].");

                            if(db.KhachHangs.Where(p => !p.IsPersonal.GetValueOrDefault() & p.MaSoThueCT == maSoThue).Count() > 0)
                                throw new Exception("[MaSoThueCT] đã có trong hệ thống.");

                            objKH.TenCongTy = congTy;
                            objKH.MaSoThueCT = maSoThue;
                            objKH.MaLHKD = 1;
                            objKH.SoGPKD = grvKH.GetRowCellValue(i, "SoGiayPhep").ToString().Trim();
                            if (hoTen.Split(' ').Length > 1)
                            {
                                objKH.HoKH = hoTen.Substring(0, hoTen.LastIndexOf(' '));
                                objKH.TenKH = hoTen.Substring(hoTen.LastIndexOf(' ') + 1);
                            }
                            else
                            {
                                objKH.HoKH = "";
                                objKH.TenKH = hoTen;
                            }

                            var ngayCapGPKD = grvKH.GetRowCellValue(i, "NgayCapGPKD").ToString().Trim().Split('/');
                            if (ngayCapGPKD.Count() > 0)
                            {
                                try
                                {
                                    objKH.NgayCapGDKKD = new DateTime(int.Parse(ngayCapGPKD[2]), int.Parse(ngayCapGPKD[1]), int.Parse(ngayCapGPKD[0]));
                                }
                                catch { }
                            }
                            objKH.NoiCapGDKKD = grvKH.GetRowCellValue(i, "NoiCapGPKD").ToString().Trim();
                            objKH.DienThoaiCT = grvKH.GetRowCellValue(i, "DienThoaiCT").ToString().Trim();
                            objKH.FaxCT = grvKH.GetRowCellValue(i, "Fax").ToString().Trim();
                            //objKH.DiaChiCT = grvKH.GetRowCellValue(i, "DiaChiCT").ToString().Trim();
                            var loaiKD = db.LoaiHinhKDs.Where(p => p.TenLHKD.ToLower() == grvKH.GetRowCellValue(i, "LoaiHinhKinhDoanh").ToString().ToLower()).Select(p => p.MaLHKD).ToList();
                            if (loaiKD.Count > 0)
                                objKH.MaNH = loaiKD[0];

                            objKH.SoTaiKhoan = grvKH.GetRowCellValue(i, "SoTaiKhoan").ToString().Trim();
                            var nganHang = db.khNganHangs.Where(p => p.TenNH.ToLower() == grvKH.GetRowCellValue(i, "NganHang").ToString().ToLower()).Select(p => p.MaNH).ToList();
                            if (nganHang.Count > 0)
                            {
                                objKH.MaNH = nganHang[0];

                                var chiNhanh = db.khNganHangChiNhanhs.Where(p => p.TenCN.ToLower() == grvKH.GetRowCellValue(i, "ChiNhanh").ToString().ToLower() & p.MaNH == objKH.MaNH).Select(p => p.MaNH).ToList();
                                if (nganHang.Count > 0)
                                    objKH.MaNH = nganHang[0];
                            }
                        }
                        objKH.DiDong = diDong;
                        objKH.DTCD = grvKH.GetRowCellValue(i, "DienThoai").ToString().Trim();
                        var ngaySinh = grvKH.GetRowCellValue(i, "NgaySinh").ToString().Trim().Replace(" 12:00:00 SA", "").Split('/');
                        if (ngaySinh.Count() > 0)
                        {
                            if (ngaySinh.Count() == 1)
                                objKH.yyyy = ngaySinh[0];
                            else
                            {
                                objKH.dd = ngaySinh[0];
                                objKH.MM = ngaySinh[1];
                                objKH.yyyy = ngaySinh[2];
                                try
                                {
                                    objKH.NgaySinh = new DateTime(int.Parse(ngaySinh[2]), int.Parse(ngaySinh[1]), int.Parse(ngaySinh[0]));
                                }
                                catch { }
                            }
                        }

                        objKH.NguyenQuan = grvKH.GetRowCellValue(i, "NoiSinh").ToString().Trim();
                        objKH.SoCMND = soCMND;
                        objKH.NgayCap = getDate(grvKH.GetRowCellValue(i, "NgayCap").ToString().Trim());
                        var ngayCap = grvKH.GetRowCellValue(i, "NgayCap").ToString().Trim().Replace(" 12:00:00 SA", "").Split('/');
                        if (ngayCap.Count() > 0)
                        {
                            if (ngayCap.Count() == 1)
                                objKH.yyyy2 = ngayCap[0];
                            else
                            {
                                objKH.dd2 = ngayCap[0];
                                objKH.MM2 = ngayCap[1];
                                objKH.yyyy2 = ngayCap[2];
                                try
                                {
                                    objKH.NgayCap = new DateTime(int.Parse(ngayCap[2]), int.Parse(ngayCap[1]), int.Parse(ngayCap[0]));
                                }
                                catch { }
                            }
                        }
                        objKH.NoiCap = grvKH.GetRowCellValue(i, "NoiCap").ToString().Trim();
                        objKH.Email = grvKH.GetRowCellValue(i, "Email").ToString().Trim();
                        //objKH.QuocTich = grvKH.GetRowCellValue(i, "QuocTich").ToString().Trim();
                        var quocGia = db.QuocGias.Where(p => p.TenQG.ToLower() == grvKH.GetRowCellValue(i, "QuocTich").ToString().ToLower()).Select(p => p.MaQG).ToList();
                        if (quocGia.Count > 0)
                            objKH.MaQG = quocGia[0];
                        objKH.CongTy1 = grvKH.GetRowCellValue(i, "DonViCongTac").ToString().Trim();
                        objKH.SanPham = grvKH.GetRowCellValue(i, "SanPham").ToString().Trim();

                        if (db.KhachHangs.Where(p => p.DiDong == diDong & diDong != "").Count() > 0)
                            throw new Exception("Số di động đã có trong hệ thống");
                        if (db.KhachHangs.Where(p => p.SoCMND == soCMND & soCMND != "").Count() > 0)
                            throw new Exception("[Số CMND] đã có trong hệ thống");
                        if (db.KhachHangs.Where(p => p.CodeSUN == code & code != "").Count() > 0)
                            throw new Exception("[Mã khách hàng] đã có trong hệ thống");
                        var diaChi = grvKH.GetRowCellValue(i, "DiaChiLH").ToString().Trim();
                        //var list = diaChi.Split(',');
                        //if (list.Length >= 4)
                        //{
                        //    try
                        //    {
                        //        objKH.DiaChi = list[0];
                        //        var tinh = db.Tinhs.Single(p => p.TenTinh == list[3].Trim());
                        //        objKH.MaTinh2 = tinh.MaTinh;
                        //        var huyen = db.Huyens.Single(p => p.TenHuyen == list[2].Trim() && p.MaTinh == tinh.MaTinh);
                        //        objKH.MaHuyen2 = huyen.MaHuyen;
                        //        var xa = db.Xas.Single(p => p.TenXa == list[1].Trim() && p.MaHuyen == huyen.MaHuyen);
                        //        objKH.MaXa2 = xa.MaXa;
                        //    }
                        //    catch { objKH.DiaChi = list[0]; }
                        //}
                        //else
                            objKH.DiaChi = diaChi;

                        var thuongTru = grvKH.GetRowCellValue(i, "DiaChiTT").ToString().Trim();
                            objKH.ThuongTru = thuongTru;

                        var quyDanh = db.QuyDanhs.Where(p => p.TenQD.ToLower() == grvKH.GetRowCellValue(i, "QuyDanh").ToString().ToLower()).Select(p => p.MaQD).ToList();
                        if (quyDanh.Count > 0)
                            objKH.MaQD = quyDanh[0];

                        //var duAn = db.DuAns.Where(p => p.TenDA.ToLower() == grvKH.GetRowCellValue(i, "DuAn").ToString().ToLower()).Select(p => p.MaDA).ToList();
                        //if (duAn.Count > 0)
                        //    objKH.MaDA = duAn[0];
                        var chucVu = db.ChucVus.Where(p => p.TenCV.ToLower() == grvKH.GetRowCellValue(i, "ChucVu").ToString().ToLower()).Select(p => p.MaCV).ToList();
                        if (chucVu.Count > 0)
                            objKH.MaDA = chucVu[0];

                        var ngheNghiep = db.NgheNghieps.Where(p => p.TenNN.ToLower() == grvKH.GetRowCellValue(i, "NgheNghiep").ToString().ToLower()).Select(p => p.MaNN).ToList();
                        if (ngheNghiep.Count > 0)
                            objKH.MaNN = ngheNghiep[0];

                        var nhomKH = db.NhomKHs.Where(p => p.TenNKH.ToLower() == grvKH.GetRowCellValue(i, "NhomKhachHang").ToString().ToLower()).Select(p => p.MaNKH).ToList();
                        if (nhomKH.Count > 0)
                            objKH.MaNKH = nhomKH[0];
                        else
                            objKH.MaNKH = 1;

                        //var loaiBDS = db.LoaiBDs.Where(p => p.TenLBDS.ToLower() == grvKH.GetRowCellValue(i, "SanPham").ToString().ToLower()).Select(p => p.MaLBDS).ToList();
                        //if (loaiBDS.Count > 0)
                        //    objKH.MaLBDS = loaiBDS[0];
                        var nguonKH = db.cdHowToKnows.Where(p => p.Name.ToLower() == grvKH.GetRowCellValue(i, "NguonKH").ToString().ToLower()).Select(p => p.ID).ToList();
                        if (nguonKH.Count > 0)
                            objKH.HowToKnowID = nguonKH[0];
                        else
                            objKH.HowToKnowID = 1;

                        var nhanVien = db.NhanViens.Where(p => p.HoTen.ToLower() == grvKH.GetRowCellValue(i, "NhanVien").ToString().ToLower())
                            .Select(p => p.MaNV).ToList();
                        if (nhanVien.Count > 0)
                            objKH.MaNV = nhanVien[0];
                        else
                            objKH.MaNV = BEE.ThuVien.Common.StaffID;
                        objKH.DTCoQuan = objKH.DTCD = grvKH.GetRowCellValue(i, "DienThoai").ToString().Trim();

                        objKH.CodeSUN = code;
                        objKH.NgayDangKy = db.GetSystemDate();
                        objKH.MaTT = 2;
                        db.KhachHangs.InsertOnSubmit(objKH);
                        db.SubmitChanges();

                        grvKH.SelectRow(i);
                    }
                }
                catch (Exception ex)
                {
                    grvKH.SetRowCellValue(i, "Error", ex.Message);
                }
            }

            grvKH.DeleteSelectedRows();

            wait.Close();

            DialogBox.Infomation("Dữ liệu đã được lưu");
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.Question("Bạn có chắc chắn muốn xóa những dòng này không?") == System.Windows.Forms.DialogResult.Yes)
                grvKH.DeleteSelectedRows();
        }
    }

    class Item
    {
        public string QuyDanh { get; set; }
        public string HoTen { get; set; }
        public string DiDong { get; set; }
        public string DienThoai { get; set; }
        public string NgaySinh { get; set; }
        public string NoiSinh { get; set; }
        public string CMND { get; set; }
        public string NgayCap { get; set; }
        public string NoiCap { get; set; }
        public string NgheNghiep { get; set; }
        public string DonViCongTac { get; set; }
        public string DiaChiLH { get; set; }
        public string DiaChiTT { get; set; }
        public string Email { get; set; }
        public string NhomKhachHang { get; set; }
        public string SanPham { get; set; }
        public string NguonKH { get; set; }
        public string NhanVien { get; set; }
        public string QuocTich { get; set; }
        public string PhanLoai { get; set; }
        public string ChucVu { get; set; }
        public string CodeSUN { get; set; }
        public string TenCongTy { get; set; }
        public string SoGiayPhep { get; set; }
        public string NgayCapGPKD { get; set; }
        public string NoiCapGPKD { get; set; }
        public string MaSoThueCT { get; set; }
        public string DienThoaiCT { get; set; }
        public string Fax { get; set; }
        public string DiaChiCT { get; set; }
        public string LoaiHinhKinhDoanh{ get; set; }
        public string SoTaiKhoan{ get; set; }
        public string NganHang { get; set; }
        public string ChiNhanh { get; set; }
        public string Error { get; set; }
    }
}