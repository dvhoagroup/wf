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
using LandSoft.Library;

namespace LandSoft.NghiepVu.Import
{
    public partial class frmGiaoDichCapNhat : DevExpress.XtraEditors.XtraForm
    {
        dip.cmdExcel objExcel;

        public frmGiaoDichCapNhat()
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

            return null;
        }

        private void frmFull_Load(object sender, EventArgs e)
        {

        }

        private void itemExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (OpenFileDialog file = new OpenFileDialog())
            {
                file.Filter = "(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;
                objExcel = new dip.cmdExcel(file.FileName);
                string[] sheets = objExcel.GetExcelSheetNames();
                cmbSheet.Items.Clear();
                foreach (string s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));
                itemSheet.EditValue = null;
            }
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (itemSheet.EditValue != null)
            {
                using (DataTable tblExcel = objExcel.ExcelSelect(itemSheet.EditValue.ToString() + "$").Tables[0])
                {
                    tblExcel.Columns.Add("Error", typeof(string));
                    grvLTT.Columns.Clear();
                    DevExpress.XtraGrid.Columns.GridColumn col;
                    for (int i = 0; i < tblExcel.Columns.Count; i++)
                    {
                        if (tblExcel.Columns[i].Caption.IndexOf("F") == 0) continue;

                        col = new DevExpress.XtraGrid.Columns.GridColumn();
                        col.Caption = tblExcel.Columns[i].Caption;
                        col.FieldName = tblExcel.Columns[i].ColumnName;
                        col.Width = col.FieldName != "Error" ? 80 : 200;
                        col.VisibleIndex = i;
                        grvLTT.Columns.Add(col);
                    }
                    gcLTT.DataSource = tblExcel;
                }
            }
            else
            {
                gcLTT.DataSource = null;
            }
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcLTT.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();

            for (int i = 0; i < grvLTT.RowCount; i++)
            {
                try
                {
                    grvLTT.UnselectRow(i);
                    using (var db = new MasterDataContext())
                    {
                        var soCMND = grvLTT.GetRowCellValue(i, "SoCMND1").ToString().Trim();
                        if (soCMND == "") throw new Exception("Vui lòng nhập số CMND");
                        var hoTen = grvLTT.GetRowCellValue(i, "HoTenKH1").ToString().ToLower().Trim();
                        if (hoTen == "") throw new Exception("Vui lòng nhập họ tên khách hàng");
                        var phanLoai = grvLTT.GetRowCellValue(i, "PhanLoaiKH1").ToString().ToLower().Trim();
                        if (phanLoai == "") throw new Exception("Vui lòng nhập phân loại khách hàng 1");

                        var kyHieu = grvLTT.GetRowCellValue(i, "MaSanPham").ToString().Trim();
                        if (kyHieu == "") throw new Exception("Vui lòng nhập ký hiệu sản phẩm");
                        var maSoNV = grvLTT.GetRowCellValue(i, "NhanVien").ToString().ToLower().Trim();
                        if (maSoNV == "") throw new Exception("Vui lòng nhập mã số NVKD");

                        var agentName = grvLTT.GetRowCellValue(i, "DaiLy").ToString().ToLower().Trim();
                        int? agentID = null, staffID = null;
                        if (agentName != "bim")
                        {
                            var agents = db.Agents.Where(p => p.AgentNameShort == agentName).Select(p => p.AgentID).ToList();
                            if (agents.Count > 0)
                                agentID = agents[0];

                            var staffs = db.aStaffs.Where(p => p.FullName == maSoNV).Select(p => p.StaffID).ToList();
                            if (staffs.Count > 0)
                                staffID = staffs[0];

                            if (staffs.Count <= 0)
                                throw new Exception("Nhân viên [Sàn] không tồn tại.");
                        }
                        else
                        {
                            var nvs = db.NhanViens.Where(p => p.HoTen == maSoNV).Select(p => p.MaNV).ToList();
                            if (nvs.Count <= 0)
                                throw new Exception("Nhân viên kinh doanh không tồn tại.");

                            staffID = nvs[0];
                        }

                        var soGC = grvLTT.GetRowCellValue(i, "SoBBDC").ToString();
                        var soDC = grvLTT.GetRowCellValue(i, "SoHDDC").ToString();
                        var soVV = "";// grvLTT.GetRowCellValue(i, "SoVV").ToString();
                        var soHD = grvLTT.GetRowCellValue(i, "SoHopDong").ToString();
                        if (soGC == "" & soDC == "" & soVV == "" & soHD == "")
                            throw new Exception("Vui lòng nhập số phiếu, số hợp đồng.");

                        var soCMND2 = grvLTT.GetRowCellValue(i, "SoCMND2").ToString().ToLower().Trim();
                        var hoTen2 = grvLTT.GetRowCellValue(i, "HoTenKH2").ToString().ToLower().Trim();
                        var phanLoai2 = grvLTT.GetRowCellValue(i, "PhanLoaiKH2").ToString().ToLower().Trim();

                        int? maKH = null, maKH2 = null;
                        if (phanLoai == "cá nhân")
                        {
                            var kh = db.KhachHangs.Where(p => p.SoCMND == soCMND & p.HoKH + " " + p.TenKH == hoTen).Select(p => p.MaKH).ToList();
                            if (kh.Count <= 0)
                                throw new Exception("Khách hàng 1 không tồn tại.");
                            maKH = kh[0];
                        }
                        else
                        {
                            var kh = db.KhachHangs.Where(p => p.MaSoThueCT == soCMND & p.TenCongTy == hoTen).Select(p => p.MaKH).ToList();
                            if (kh.Count <= 0)
                                throw new Exception("Khách hàng 1 không tồn tại.");
                            maKH = kh[0];
                        }

                        if (phanLoai2 == "cá nhân")
                        {
                            var kh2 = db.KhachHangs.Where(p => p.SoCMND == soCMND2 & p.HoKH + " " + p.TenKH == hoTen2).Select(p => p.MaKH).ToList();
                            if (kh2.Count > 0)
                                maKH2 = kh2[0];
                        }
                        else
                        {
                            var kh2 = db.KhachHangs.Where(p => p.MaSoThueCT == soCMND2 & p.TenCongTy == hoTen2).Select(p => p.MaKH).ToList();
                            if (kh2.Count > 0)
                                maKH2 = kh2[0];
                        }

                        var bds = db.bdsSanPhams.Where(p => p.KyHieu == kyHieu).Select(p => p.MaSP).ToList();
                        if (bds.Count <= 0)
                            throw new Exception("Sản phẩm không tồn tại");

                        grvLTT.SetRowCellValue(i, "Error", "ok2");
                        pgcPhieuGiuCho objPGC = null;
                        HopDongMuaBan objHD = null;

                        if (soGC != "")
                            objPGC = db.pgcPhieuGiuChos.SingleOrDefault(p => p.SoPhieu == soGC);

                        if (soHD != "")
                            objHD = db.HopDongMuaBans.SingleOrDefault(p => p.SoHDMB == soHD);

                        if (objPGC == null)
                        {
                            if (objHD != null)
                                objPGC = objHD.pgcPhieuGiuCho;

                            if (agentID != null)
                            {
                                objPGC.MaNVDL = staffID;
                                objPGC.MaDL = agentID;
                            }
                            else
                                objPGC.MaNVKD = staffID;

                            objPGC.MaNVKT = Library.Common.StaffID;
                            objPGC.NgayKy = getDate(grvLTT.GetRowCellValue(i, "NgayKyBBDC").ToString());
                            if (objPGC.NgayKy == null)
                                objPGC.NgayKy = DateTime.Now;
                            objPGC.ThoiHan = 0;
                            objPGC.TienGiuCho = 0;
                            objPGC.PhuThu = 0;
                            objPGC.GiaTriCK = 0;
                            objPGC.MaDVCK = 1;
                            objPGC.MaCTP = 1;
                            objPGC.DTSD = grvLTT.GetRowCellValue(i, "DienTich").ToString() == "" ? 0 : Convert.ToDecimal(grvLTT.GetRowCellValue(i, "DienTich").ToString().Replace(".", ","));
                            objPGC.DonGia = grvLTT.GetRowCellValue(i, "GiaBan").ToString() == "" ? 0 : Convert.ToDecimal(grvLTT.GetRowCellValue(i, "GiaBan").ToString().Replace(".", ","));
                            objPGC.DonGiaSauCKKM = grvLTT.GetRowCellValue(i, "DonGiaSauCKKM").ToString() == "" ? 0 : Convert.ToDecimal(grvLTT.GetRowCellValue(i, "DonGiaSauCKKM").ToString().Replace(".", ","));
                            objPGC.ThanhTien = grvLTT.GetRowCellValue(i, "TongGiaTriCanHo").ToString() == "" ? 0 : Convert.ToDecimal(grvLTT.GetRowCellValue(i, "TongGiaTriCanHo").ToString().Replace(".", ","));
                            objPGC.TienCK = grvLTT.GetRowCellValue(i, "ChietKhau").ToString() == "" ? 0 : Convert.ToDecimal(grvLTT.GetRowCellValue(i, "ChietKhau").ToString().Replace(".", ","));
                            objPGC.TienCKKhac = grvLTT.GetRowCellValue(i, "ChietKhauThanhToan").ToString() == "" ? 0 : Convert.ToDecimal(grvLTT.GetRowCellValue(i, "ChietKhauThanhToan"));
                            objPGC.TongGiaTriCH = objPGC.ThanhTien - objPGC.TienCK - objPGC.TienCKKhac;
                            objPGC.VAT = grvLTT.GetRowCellValue(i, "VAT").ToString() == "" ? 0 : Convert.ToDecimal(grvLTT.GetRowCellValue(i, "VAT").ToString().Replace(".", ","));
                            objPGC.PhiTruocBa = grvLTT.GetRowCellValue(i, "PhiTruocBa").ToString() == "" ? 0 : Convert.ToDecimal(grvLTT.GetRowCellValue(i, "PhiTruocBa").ToString().Replace(".", ","));
                            objPGC.PhiDiaChinh = grvLTT.GetRowCellValue(i, "PhiDiaChinh").ToString() == "" ? 0 : Convert.ToDecimal(grvLTT.GetRowCellValue(i, "PhiDiaChinh"));
                            objPGC.PhiBaoTri = grvLTT.GetRowCellValue(i, "PhiBaoTri").ToString() == "" ? 0 : Convert.ToDecimal(grvLTT.GetRowCellValue(i, "PhiBaoTri").ToString().Replace(".", ","));
                            objPGC.NgayTTPBT = getDate(grvLTT.GetRowCellValue(i, "NgayTTPhiBaoTri").ToString());
                            objPGC.TongGiaTriHD = grvLTT.GetRowCellValue(i, "TongGiaTriHopDong").ToString() == "" ? 0 : Convert.ToDecimal(grvLTT.GetRowCellValue(i, "TongGiaTriHopDong"));

                            objPGC.SLCMND = grvLTT.GetRowCellValue(i, "SLCMND").ToString() == "" ? 0 : Convert.ToInt32(grvLTT.GetRowCellValue(i, "SLCMND"));
                            objPGC.SLHoKhau = grvLTT.GetRowCellValue(i, "SLHoKhau").ToString() == "" ? 0 : Convert.ToInt32(grvLTT.GetRowCellValue(i, "SLHoKhau"));
                            objPGC.DienGiai = grvLTT.GetRowCellValue(i, "GhiChu").ToString();
                            objPGC.NguoiDaiDienCDT = grvLTT.GetRowCellValue(i, "NguoiDaiDienCDT").ToString();
                            objPGC.IsTotal = true;
                            decimal tylehh = 0;
                            if (grvLTT.GetRowCellValue(i, "TyLeHH").ToString() != "")
                                Decimal.TryParse(grvLTT.GetRowCellValue(i, "TyLeHH").ToString().Replace(".", ","), out tylehh);
                            objPGC.TyLeMG = tylehh;
                            objPGC.PhiMoiGioi = objPGC.TyLeMG * (objPGC.TongGiaTriCH ?? 0) / 100;
                        }

                        #region Lich thanh toan
                        decimal tienDot1 = 0;
                        bool IsFirst = true;
                        int lttCount = (grvLTT.Columns.Count - 28) / 3;
                        for (int j = 1; j <= lttCount; j++)
                        {
                            try
                            {
                                pgcLichThanhToan objLTT = db.pgcLichThanhToans.SingleOrDefault(p => p.MaPGC == objPGC.MaPGC & p.DotTT == (byte)j);
                                if (objLTT == null)
                                    objLTT = new pgcLichThanhToan();

                                objLTT.DotTT = (byte)j;
                                objLTT.DienGiai = "";
                                objLTT.NgayTT = getDate(grvLTT.GetRowCellValue(i, "NgayTTDot" + j).ToString());
                                objLTT.MaKTT = 1;// Convert.ToByte(grvLTT.GetRowCellValue(i, "KieuTT" + j));
                                if (objLTT.MaKTT == 0) objLTT.MaKTT = 1;
                                objLTT.TyLeTT = GetValueDecimal(grvLTT.GetRowCellValue(i, "TyLeTTDot" + j).ToString().Replace(".", ",")) * 100;
                                decimal soTien = GetValueDecimal(grvLTT.GetRowCellValue(i, "SoTienDot" + j).ToString().Replace(".", ","));
                                //if (objLTT.TyLeTT == 0)
                                //{
                                    objLTT.TuongUng = objLTT.SoTien = soTien;
                                    objLTT.ThueVAT = 0;
                                    if (IsFirst)
                                    {
                                        tienDot1 = 0;
                                        IsFirst = false;
                                    }
                                //}
                                //else
                                //{
                                //    objLTT.TyLeVAT = GetValueDecimal(grvLTT.GetRowCellValue(i, "TyLeVAT" + j).ToString().Replace(".", ",")) * 100;
                                //    decimal? giaTri = 0;
                                //    switch (objLTT.MaKTT)
                                //    {
                                //        case 1:
                                //            giaTri = objPGC.TongGiaTriHD; break;
                                //        case 2:
                                //            giaTri = objPGC.bdsSanPham.ThanhTienXD; break;
                                //        case 3:
                                //            giaTri = objPGC.bdsSanPham.ThanhTienKV; break;
                                //        case 4:
                                //            giaTri = objPGC.bdsSanPham.ThanhTienHM; break;
                                //        case 5:
                                //            giaTri = objPGC.bdsSanPham.ThanhTienXD + objPGC.bdsSanPham.ThanhTienKV; break;
                                //        case 6:
                                //            giaTri = objPGC.bdsSanPham.ThanhTienXD + objPGC.bdsSanPham.ThanhTienHM; break;
                                //        case 7:
                                //            giaTri = objPGC.bdsSanPham.ThanhTienKV + objPGC.bdsSanPham.ThanhTienHM; break;
                                //        default:
                                //            giaTri = objPGC.TongGiaTriHD; break;
                                //    }

                                //    objLTT.TuongUng = giaTri * objLTT.TyLeTT / 100;
                                //    objLTT.ThueVAT = giaTri * objLTT.TyLeVAT / 100;
                                //    objLTT.SoTien = objLTT.TuongUng + objLTT.ThueVAT;
                                //    if (IsFirst)
                                //    {
                                //        tienDot1 = objLTT.TyLeTT ?? 0;
                                //        IsFirst = false;
                                //    }
                                //}

                                if (objLTT.ID == 0)
                                {
                                    if (objLTT.TuongUng > 0)
                                        objPGC.pgcLichThanhToans.Add(objLTT);
                                }
                            }
                            catch { }
                        }
                        #endregion

                        //Bảng kê
                        try
                        {
                            var objBK = objPGC.bkBangKe; 
                            objBK.NgayBK = objPGC.HopDongMuaBan.NgayKy;
                            objBK.MaBK = objPGC.HopDongMuaBan.MaHDMB;
                            objBK.MaKH = objPGC.MaKH;
                            objBK.DienGiai = "";
                            objBK.DienTich = objPGC.DTSD;
                            objBK.DonGia = objPGC.DonGia;
                            objBK.ThanhTien = objPGC.ThanhTien;
                            objBK.ThanhTienSauCKKM = objPGC.DonGiaSauCKKM;
                            objBK.KhuyenMai = 0;
                            objBK.TyLeDot1 = tienDot1;
                            objBK.ChietKhau = objPGC.TienCK + objPGC.TienCKKhac;
                        }
                        catch
                        {
                            throw new Exception("[Bảng kê] bị lỗi.");
                        }

                        db.SubmitChanges();

                        grvLTT.SelectRow(i);
                    }
                }
                catch (Exception ex)
                {
                    grvLTT.SetRowCellValue(i, "Error", ex.Message);
                }
            }

            grvLTT.DeleteSelectedRows();

            wait.Close();

            DialogBox.Infomation("Dữ liệu đã được lưu");
        }

        decimal GetValueDecimal(string str)
        {
            try
            {
                return Convert.ToDecimal(str.Trim());
            }
            catch { return 0; }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvLTT.DeleteSelectedRows();
        }
    }
}