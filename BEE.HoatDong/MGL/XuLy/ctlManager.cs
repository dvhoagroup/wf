﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.Linq.SqlClient;
using BEE.ThuVien;
using BEEREMA;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;

namespace BEE.HoatDong.MGL.XuLy
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();

        public ctlManager()
        {
            InitializeComponent();
            //    // Tạo BarEditItem cho ô tìm kiếm
            //    BarEditItem searchItem = new BarEditItem(barManager1);
            //    searchItem.Caption = "Search:";
            //    searchItem.Edit = new RepositoryItemTextEdit(); // Hoặc có thể sử dụng RepositoryItemSearchControl để có trải nghiệm tìm kiếm tốt hơn
            //    searchItem.Width = 150;
            //    bar1.AddItem(searchItem);

            //    // Tạo BarEditItem cho `ckNhanVien`
            //    BarEditItem nhanVienItem = new BarEditItem(barManager1);
            //    nhanVienItem.Caption = "Nhân viên 1:";
            //    nhanVienItem.Edit = new RepositoryItemCheckedComboBoxEdit();
            //    nhanVienItem.EditWidth = 200; // Thiết lập độ rộng cho `ckNhanVien`
            //    bar1.AddItem(nhanVienItem);

            //    // Đưa BarControl vào giao diện người dùng
            //    barManager1.MainMenu = bar1;

            //    // Xử lý sự kiện để tìm kiếm trong `ckNhanVien`
            //    ((RepositoryItemTextEdit)searchItem.Edit).KeyDown += (sender, e) =>
            //    {
            //        if (e.KeyCode == Keys.Enter)
            //        {
            //            string searchText = searchItem.EditValue.ToString().ToLower(); // Lấy giá trị từ ô tìm kiếm và chuyển về chữ thường để so sánh không phân biệt hoa thường
            //            List<ThuVien.NhanVien> nhanViens =db.NhanViens.ToList(); // Hàm này để lấy danh sách nhân viên, thay bằng cách lấy dữ liệu thực tế của bạn

            //            // Lọc danh sách nhân viên dựa trên searchText
            //            List<ThuVien.NhanVien> filteredNhanViens = db.NhanViens.Where(nv => nv.HoTen.ToLower().Contains(searchText)).ToList();

            //            // Cập nhật dữ liệu trong `ckNhanVien`
            //            ((RepositoryItemCheckedComboBoxEdit)nhanVienItem.Edit).DataSource = filteredNhanViens;
            //        }
            //    };
            //}
        }
       


        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 178;
            DataTable tblAction = o.SelectBy();
            itemExport.Enabled = false;
            itemChangeStatus.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 13:
                            itemExport.Enabled = true;
                            break;
                        case 88:
                            itemChangeStatus.Enabled = true;
                            break;
                        case 92:
                            grvDaChao.Columns["ThanhTienMG"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "ThanhTienMG", "{0:#,0.##}");
                            grvDaChao.Columns["PhiMGCT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PhiMGCT", "{0:#,0.##}");
                            grvDaChao.Columns["PhiMGCL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PhiMGCL", "{0:#,0.##}");
                            grvDaChao.Columns["ThanhTienMGMT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "ThanhTienMGMT", "{0:#,0.##}");
                            grvDaChao.Columns["PhiMGCTMua"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PhiMGCTMua", "{0:#,0.##}");
                            grvDaChao.Columns["PhiMGDTMua"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PhiMGDTMua", "{0:#,0.##}");
                            break;
                    }
                }
            }
        }

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 178);

            return o.SDB.SDBID;
        }
        // Cách tiếp cận đơn giản hơn để ẩn phần đầu của chuỗi
        public string HideStringPart(string sValue)
        {
            try
            {
                if (sValue.Length <= 3 && sValue == null)
                {
                    return sValue; // Trường hợp chuỗi quá ngắn, không cần xử lý
                }

                string lastThreeChars = sValue.Substring(sValue.Length - 3);
                return new string('x', sValue.Length - 3) + lastThreeChars;
            }
            catch
            {
                return null;
            }
           
        }

        private string GetAdress(int? MaBC)
        {
            var obj = db.mglbcBanChoThues.First(p => p.MaBC == MaBC);
            var xa = obj.MaXa == null ? "" : db.Xas.FirstOrDefault(p => p.MaXa == obj.MaXa).TenXa;
            var huyen = obj.MaHuyen == null ? "" : db.Huyens.First(p => p.MaHuyen == obj.MaHuyen).TenHuyen;
            var tinh = obj.MaTinh == null ? "" : db.Tinhs.First(p => p.MaTinh == obj.MaTinh).TenTinh;
            string DiaChi = string.Format("{0} - {1} - {2} - {3} - {4}", obj.SoNha, obj.TenDuong, xa, huyen, tinh);
            return DiaChi;
        }

        void BaoCao_Load()
        {
            var obj = db.mglbcPhanQuyens.Single(p => p.MaNV == Common.StaffID);
            var tuNgay = (DateTime?)itemTuNgay.EditValue ?? DateTime.Now;
            var denNgay = (DateTime?)itemDenNgay.EditValue ?? DateTime.Now;
            var strMaTT = (itemTrangThai.EditValue ?? "").ToString().Replace(" ", "");
            var arrMaTT = "," + strMaTT + ",";
            var strMaNV = (itemNhanVien.EditValue ?? "").ToString().Replace(" ", "");
            var arrMaNV = "," + strMaNV + ",";
            int MaNV = Common.StaffID;
            var wait = DialogBox.WaitingForm();
            try
            {
                if (itemTuNgay.EditValue == null || itemDenNgay.EditValue == null)
                {
                    gcDaChao.DataSource = null;
                    return;
                }
                db = new MasterDataContext();
                switch (GetAccessData())
                {
                    case 1://Tat ca
                        if (obj.DienThoai == false)
                        {
                            gcDaChao.DataSource = db.gdGetManagerGiaoDichV3(tuNgay, denNgay, MaNV, true, -1, -1, -1, arrMaTT, arrMaNV).Select(p => new
                            {
                                p.ID,
                                p.STT,
                                p.MaLoai,
                                p.DiaChi,
                                p.DacTrung,
                                p.GhiChu,
                                LinkAnh = p.LinkAnh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                p.ToaDo,
                                p.HoTenKHBC,
                                p.DienThoai1,
                                p.DiDong2,
                                p.DTDD,
                                p.DienThoai,
                                p.Phone2,
                                //  DienThoai1 = Common.Right(p.DienThoai1, 3),
                                // DiDong2 = Common.Right(p.DiDong2, 3),
                                p.PhiMoiGioi,
                                p.ThanhTienMG,
                                p.TenTT,
                                p.ThuongHieu,
                                p.HoTenKH,
                                // p.DienThoai,
                                //p.Phone2,
                                // DienThoai = Common.Right(p.DienThoai, 3), // không check thì ko dc xem 3 số 
                                // Phone2 = Common.Right(p.Phone2, 3),
                                p.PhiMG,
                                p.ThanhTienMGMT,
                                p.TenNC,
                                p.MaBC,
                                p.MaMT,
                                p.MaKH,
                                p.MaKHBC,
                                p.StartDate,
                                p.UpdateDate,
                                p.PhiMGCT,
                                p.PhiMGCL,
                                p.PhiMGCTMua,
                                p.PhiMGDTMua,
                                p.Color,
                                p.HoTenNLH,
                                //DTDD = Common.Right(p.DTDD, 3),
                                p.HoTenNVN,
                                p.Code,
                                p.StatusEndDate,
                                p.OverTime,
                                p.StatusOverTime
                            });
                        }
                        else if (obj.DienThoai3Dau == false)
                        {
                            var data = db.gdGetManagerGiaoDichV3(tuNgay, denNgay, MaNV, true, -1, -1, -1, arrMaTT, arrMaNV).Where(p => p.ID == 47146).ToList();
                            gcDaChao.DataSource = db.gdGetManagerGiaoDichV3(tuNgay, denNgay, MaNV, true, -1, -1, -1, arrMaTT, arrMaNV).Select(p => new
                            {
                                p.ID,
                                p.STT,
                                p.MaLoai,
                                p.DiaChi,
                                p.DacTrung,
                                p.GhiChu,
                                LinkAnh = p.LinkAnh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                p.ToaDo,
                                p.HoTenKHBC,
                                p.DienThoai1,
                                p.DiDong2,
                                p.DTDD,
                                p.DienThoai,
                                p.Phone2,
                                // DienThoai1 = Common.Right1(p.DienThoai1, 3),
                                // DiDong2 = Common.Right1(p.DiDong2, 3),
                                p.PhiMoiGioi,
                                p.ThanhTienMG,
                                p.TenTT,
                                p.ThuongHieu,
                                p.HoTenKH,
                                // DienThoai = Common.Right1(p.DienThoai, 3), // không check thì ko dc xem 3 số 
                                //Phone2 = Common.Right1(p.Phone2, 3),
                                p.PhiMG,
                                p.ThanhTienMGMT,
                                p.TenNC,
                                p.MaBC,
                                p.MaMT,
                                p.MaKH,
                                p.MaKHBC,
                                p.StartDate,
                                p.UpdateDate,
                                p.PhiMGCT,
                                p.PhiMGCL,
                                p.PhiMGCTMua,
                                p.PhiMGDTMua,
                                p.Color,
                                p.HoTenNLH,
                                // DTDD = Common.Right1(p.DTDD, 3),
                                p.HoTenNVN,
                                p.Code,
                                p.StatusEndDate,
                                p.OverTime,
                                p.StatusOverTime
                            });
                           

                        }
                        else if (obj.DienThoaiAn == false)
                        {
                            gcDaChao.DataSource = db.gdGetManagerGiaoDichV3(tuNgay, denNgay, MaNV, true, -1, -1, -1, arrMaTT, arrMaNV).Select(p => new
                            {
                                p.ID,
                                p.STT,
                                p.MaLoai,
                                p.DiaChi,
                                p.DacTrung,
                                p.GhiChu,
                                LinkAnh = p.LinkAnh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                p.ToaDo,
                                p.HoTenKHBC,
                                DienThoai1 = "",
                                DiDong2 = "",
                                p.PhiMoiGioi,
                                p.ThanhTienMG,
                                p.TenTT,
                                p.ThuongHieu,
                                p.HoTenKH,
                                DienThoai = "",
                                Phone2 = "",
                                p.PhiMG,
                                p.ThanhTienMGMT,
                                p.TenNC,
                                p.MaBC,
                                p.MaMT,
                                p.MaKH,
                                p.MaKHBC,
                                p.StartDate,
                                p.UpdateDate,
                                p.PhiMGCT,
                                p.PhiMGCL,
                                p.PhiMGCTMua,
                                p.PhiMGDTMua,
                                p.Color,
                                p.HoTenNLH,
                                DTDD = "",
                                p.HoTenNVN,
                                p.Code,
                                p.StatusEndDate,
                                p.OverTime,
                                p.StatusOverTime
                            });
                        }
                        else
                        {
                            gcDaChao.DataSource = db.gdGetManagerGiaoDichV3(tuNgay, denNgay, MaNV, true, -1, -1, -1, arrMaTT, arrMaNV).Select(p => new
                            {
                                p.ID,
                                p.STT,
                                p.MaLoai,
                                p.DiaChi,
                                p.DacTrung,
                                p.GhiChu,
                                LinkAnh = p.LinkAnh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                p.ToaDo,
                                p.HoTenKHBC,
                                DienThoai1 = p.DienThoai1,
                                DiDong2 = p.DiDong2,
                                p.PhiMoiGioi,
                                p.ThanhTienMG,
                                p.TenTT,
                                p.ThuongHieu,
                                p.HoTenKH,
                                DienThoai = p.DienThoai,
                                Phone2 = p.Phone2,
                                p.PhiMG,
                                p.ThanhTienMGMT,
                                p.TenNC,
                                p.MaBC,
                                p.MaMT,
                                p.MaKH,
                                p.MaKHBC,
                                p.StartDate,
                                p.UpdateDate,
                                p.PhiMGCT,
                                p.PhiMGCL,
                                p.PhiMGCTMua,
                                p.PhiMGDTMua,
                                p.Color,
                                p.HoTenNLH,
                                p.DTDD,
                                p.HoTenNVN,
                                p.Code,
                                p.StatusEndDate,
                                p.OverTime,
                                p.StatusOverTime
                            });
                        }
                        break;
                    case 2://Theo phong ban 
                        if (obj.DienThoai == false)
                        {
                            gcDaChao.DataSource = db.gdGetManagerGiaoDichV3(tuNgay, denNgay, MaNV, true, -1, -1, Common.DepartmentID, arrMaTT, arrMaNV).Select(p => new
                            {
                                p.ID,
                                p.STT,
                                p.MaLoai,
                                p.DiaChi,
                                p.DacTrung,
                                p.GhiChu,
                                LinkAnh = p.LinkAnh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                p.ToaDo,
                                p.HoTenKHBC,
                                p.DienThoai1,
                                p.DiDong2,
                                p.DTDD,
                                p.DienThoai,
                                p.Phone2,
                                // DienThoai1 = Common.Right(p.DienThoai1, 3),
                                // DiDong2 = Common.Right(p.DiDong2, 3),
                                p.PhiMoiGioi,
                                p.ThanhTienMG,
                                p.TenTT,
                                p.ThuongHieu,
                                p.HoTenKH,
                                //DienThoai = Common.Right(p.DienThoai, 3), // không check thì ko dc xem 3 số 
                                //Phone2 = Common.Right(p.Phone2, 3),
                                p.PhiMG,
                                p.ThanhTienMGMT,
                                p.TenNC,
                                p.MaBC,
                                p.MaMT,
                                p.MaKH,
                                p.MaKHBC,
                                p.StartDate,
                                p.UpdateDate,
                                p.PhiMGCT,
                                p.PhiMGCL,
                                p.PhiMGCTMua,
                                p.PhiMGDTMua,
                                p.Color,
                                p.HoTenNLH,
                                // DTDD = Common.Right(p.DTDD, 3),
                                p.HoTenNVN,
                                p.Code,
                                p.StatusEndDate,
                                p.OverTime,
                                p.StatusOverTime
                            });
                        }
                        else if (obj.DienThoai3Dau == false)
                        {
                            gcDaChao.DataSource = db.gdGetManagerGiaoDichV3(tuNgay, denNgay, MaNV, true, -1, -1, Common.DepartmentID, arrMaTT, arrMaNV).Select(p => new
                            {
                                p.ID,
                                p.STT,
                                p.MaLoai,
                                p.DiaChi,
                                p.DacTrung,
                                p.GhiChu,
                                LinkAnh = p.LinkAnh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                p.ToaDo,
                                p.HoTenKHBC,
                                p.DienThoai1,
                                p.DiDong2,
                                p.DTDD,
                                p.DienThoai,
                                p.Phone2,
                                // DienThoai1 = Common.Right1(p.DienThoai1, 3),
                                // DiDong2 = Common.Right1(p.DiDong2, 3),
                                p.PhiMoiGioi,
                                p.ThanhTienMG,
                                p.TenTT,
                                p.ThuongHieu,
                                p.HoTenKH,
                                // DienThoai = Common.Right1(p.DienThoai, 3), // không check thì ko dc xem 3 số 
                                //Phone2 = Common.Right1(p.Phone2, 3),
                                p.PhiMG,
                                p.ThanhTienMGMT,
                                p.TenNC,
                                p.MaBC,
                                p.MaMT,
                                p.MaKH,
                                p.MaKHBC,
                                p.StartDate,
                                p.UpdateDate,
                                p.PhiMGCT,
                                p.PhiMGCL,
                                p.PhiMGCTMua,
                                p.PhiMGDTMua,
                                p.Color,
                                p.HoTenNLH,
                                // DTDD = Common.Right1(p.DTDD, 3),
                                p.HoTenNVN,
                                p.Code,
                                p.StatusEndDate,
                                p.OverTime,
                                p.StatusOverTime
                            });

                        }
                        else if (obj.DienThoaiAn == false)
                        {
                            gcDaChao.DataSource = db.gdGetManagerGiaoDichV3(tuNgay, denNgay, MaNV, true, -1, -1, Common.DepartmentID, arrMaTT, arrMaNV).Select(p => new
                            {
                                p.ID,
                                p.STT,
                                p.MaLoai,
                                p.DiaChi,
                                p.DacTrung,
                                p.GhiChu,
                                LinkAnh = p.LinkAnh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                p.ToaDo,
                                p.HoTenKHBC,
                                DienThoai1 = "",
                                DiDong2 = "",
                                p.PhiMoiGioi,
                                p.ThanhTienMG,
                                p.TenTT,
                                p.ThuongHieu,
                                p.HoTenKH,
                                DienThoai = "",
                                Phone2 = "",
                                p.PhiMG,
                                p.ThanhTienMGMT,
                                p.TenNC,
                                p.MaBC,
                                p.MaMT,
                                p.MaKH,
                                p.MaKHBC,
                                p.StartDate,
                                p.UpdateDate,
                                p.PhiMGCT,
                                p.PhiMGCL,
                                p.PhiMGCTMua,
                                p.PhiMGDTMua,
                                p.Color,
                                p.HoTenNLH,
                                DTDD = "",
                                p.HoTenNVN,
                                p.Code,
                                p.StatusEndDate,
                                p.OverTime,
                                p.StatusOverTime
                            });
                        }
                        else
                        {
                            gcDaChao.DataSource = db.gdGetManagerGiaoDichV3(tuNgay, denNgay, MaNV, true, -1, -1, Common.DepartmentID, arrMaTT, arrMaNV).Select(p => new
                            {
                                p.ID,
                                p.STT,
                                p.MaLoai,
                                p.DiaChi,
                                p.DacTrung,
                                p.GhiChu,
                                LinkAnh = p.LinkAnh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                p.ToaDo,
                                p.HoTenKHBC,
                                DienThoai1 = p.DienThoai1,
                                DiDong2 = p.DiDong2,
                                p.PhiMoiGioi,
                                p.ThanhTienMG,
                                p.TenTT,
                                p.ThuongHieu,
                                p.HoTenKH,
                                DienThoai = p.DienThoai,
                                Phone2 = p.Phone2,
                                p.PhiMG,
                                p.ThanhTienMGMT,
                                p.TenNC,
                                p.MaBC,
                                p.MaMT,
                                p.MaKH,
                                p.MaKHBC,
                                p.StartDate,
                                p.UpdateDate,
                                p.PhiMGCT,
                                p.PhiMGCL,
                                p.PhiMGCTMua,
                                p.PhiMGDTMua,
                                p.Color,
                                p.HoTenNLH,
                                p.DTDD,
                                p.HoTenNVN,
                                p.Code,
                                p.StatusEndDate,
                                p.OverTime,
                                p.StatusOverTime
                            });
                        }
                        break;
                    case 3://Theo nhom
                        if (obj.DienThoai == false)
                        {
                            gcDaChao.DataSource = db.gdGetManagerGiaoDichV3(tuNgay, denNgay, MaNV, true, -1, Common.GroupID, -1, arrMaTT, arrMaNV).Select(p => new
                            {
                                p.ID,
                                p.STT,
                                p.MaLoai,
                                p.DiaChi,
                                p.DacTrung,
                                p.GhiChu,
                                LinkAnh = p.LinkAnh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                p.ToaDo,
                                p.HoTenKHBC,
                                p.DienThoai1,
                                p.DiDong2,
                                p.DTDD,
                                p.DienThoai,
                                p.Phone2,
                                // DienThoai1 = Common.Right(p.DienThoai1, 3),
                                //  DiDong2 = Common.Right(p.DiDong2, 3),
                                p.PhiMoiGioi,
                                p.ThanhTienMG,
                                p.TenTT,
                                p.ThuongHieu,
                                p.HoTenKH,
                                // DienThoai = Common.Right(p.DienThoai, 3), // không check thì ko dc xem 3 số 
                                //Phone2 = Common.Right(p.Phone2, 3),
                                p.PhiMG,
                                p.ThanhTienMGMT,
                                p.TenNC,
                                p.MaBC,
                                p.MaMT,
                                p.MaKH,
                                p.MaKHBC,
                                p.StartDate,
                                p.UpdateDate,
                                p.PhiMGCT,
                                p.PhiMGCL,
                                p.PhiMGCTMua,
                                p.PhiMGDTMua,
                                p.Color,
                                p.HoTenNLH,
                                // DTDD = Common.Right(p.DTDD, 3),
                                p.HoTenNVN,
                                p.Code,
                                p.StatusEndDate,
                                p.OverTime,
                                p.StatusOverTime
                            });
                        }
                        else if (obj.DienThoai3Dau == false)
                        {
                            gcDaChao.DataSource = db.gdGetManagerGiaoDichV3(tuNgay, denNgay, MaNV, true, -1, Common.GroupID, -1, arrMaTT, arrMaNV).Select(p => new
                            {
                                p.ID,
                                p.STT,
                                p.MaLoai,
                                p.DiaChi,
                                p.DacTrung,
                                p.GhiChu,
                                LinkAnh = p.LinkAnh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                p.ToaDo,
                                p.HoTenKHBC,
                                p.DienThoai1,
                                p.DiDong2,
                                p.DTDD,
                                p.DienThoai,
                                p.Phone2,
                                // DienThoai1 = Common.Right1(p.DienThoai1, 3),
                                // DiDong2 = Common.Right1(p.DiDong2, 3),
                                p.PhiMoiGioi,
                                p.ThanhTienMG,
                                p.TenTT,
                                p.ThuongHieu,
                                p.HoTenKH,
                                // DienThoai = Common.Right1(p.DienThoai, 3), // không check thì ko dc xem 3 số 
                                //Phone2 = Common.Right1(p.Phone2, 3),
                                p.PhiMG,
                                p.ThanhTienMGMT,
                                p.TenNC,
                                p.MaBC,
                                p.MaMT,
                                p.MaKH,
                                p.MaKHBC,
                                p.StartDate,
                                p.UpdateDate,
                                p.PhiMGCT,
                                p.PhiMGCL,
                                p.PhiMGCTMua,
                                p.PhiMGDTMua,
                                p.Color,
                                p.HoTenNLH,
                                //DTDD = Common.Right1(p.DTDD, 3),
                                p.HoTenNVN,
                                p.Code,
                                p.StatusEndDate,
                                p.OverTime,
                                p.StatusOverTime
                            });

                        }
                        else if (obj.DienThoaiAn == false)
                        {
                            gcDaChao.DataSource = db.gdGetManagerGiaoDichV3(tuNgay, denNgay, MaNV, true, -1, Common.GroupID, -1, arrMaTT, arrMaNV).Select(p => new
                            {
                                p.ID,
                                p.STT,
                                p.MaLoai,
                                p.DiaChi,
                                p.DacTrung,
                                p.GhiChu,
                                LinkAnh = p.LinkAnh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                p.ToaDo,
                                p.HoTenKHBC,
                                DienThoai1 = "",
                                DiDong2 = "",
                                p.PhiMoiGioi,
                                p.ThanhTienMG,
                                p.TenTT,
                                p.ThuongHieu,
                                p.HoTenKH,
                                DienThoai = "",
                                Phone2 = "",
                                p.PhiMG,
                                p.ThanhTienMGMT,
                                p.TenNC,
                                p.MaBC,
                                p.MaMT,
                                p.MaKH,
                                p.MaKHBC,
                                p.StartDate,
                                p.UpdateDate,
                                p.PhiMGCT,
                                p.PhiMGCL,
                                p.PhiMGCTMua,
                                p.PhiMGDTMua,
                                p.Color,
                                p.HoTenNLH,
                                DTDD = "",
                                p.HoTenNVN,
                                p.Code,
                                p.StatusEndDate,
                                p.OverTime,
                                p.StatusOverTime
                            });
                        }
                        else
                        {
                            gcDaChao.DataSource = db.gdGetManagerGiaoDichV3(tuNgay, denNgay, MaNV, true, -1, Common.GroupID, -1, arrMaTT, arrMaNV).Select(p => new
                            {
                                p.ID,
                                p.STT,
                                p.MaLoai,
                                p.DiaChi,
                                p.DacTrung,
                                p.GhiChu,
                                LinkAnh = p.LinkAnh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                p.ToaDo,
                                p.HoTenKHBC,
                                DienThoai1 = p.DienThoai1,
                                DiDong2 = p.DiDong2,
                                p.PhiMoiGioi,
                                p.ThanhTienMG,
                                p.TenTT,
                                p.ThuongHieu,
                                p.HoTenKH,
                                DienThoai = p.DienThoai,
                                Phone2 = p.Phone2,
                                p.PhiMG,
                                p.ThanhTienMGMT,
                                p.TenNC,
                                p.MaBC,
                                p.MaMT,
                                p.MaKH,
                                p.MaKHBC,
                                p.StartDate,
                                p.UpdateDate,
                                p.PhiMGCT,
                                p.PhiMGCL,
                                p.PhiMGCTMua,
                                p.PhiMGDTMua,
                                p.Color,
                                p.HoTenNLH,
                                p.DTDD,
                                p.HoTenNVN,
                                p.Code,
                                p.StatusEndDate,
                                p.OverTime,
                                p.StatusOverTime
                            });
                        }
                        break;
                    case 4://Theo nhan vien
                        if (obj.DienThoai == false)
                        {
                            gcDaChao.DataSource = db.gdGetManagerGiaoDichV3(tuNgay, denNgay, MaNV, true, MaNV, -1, -1, arrMaTT, arrMaNV).Select(p => new
                            {
                                p.ID,
                                p.STT,
                                p.MaLoai,
                                p.DiaChi,
                                p.DacTrung,
                                p.GhiChu,
                                LinkAnh = p.LinkAnh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                p.ToaDo,
                                p.HoTenKHBC,
                                p.DienThoai1,
                                p.DiDong2,
                                p.DTDD,
                                p.DienThoai,
                                p.Phone2,
                                // DienThoai1 = Common.Right(p.DienThoai1, 3),
                                //DiDong2 = Common.Right(p.DiDong2, 3),
                                p.PhiMoiGioi,
                                p.ThanhTienMG,
                                p.TenTT,
                                p.ThuongHieu,
                                p.HoTenKH,
                                //DienThoai = Common.Right(p.DienThoai, 3), // không check thì ko dc xem 3 số 
                                //Phone2 = Common.Right(p.Phone2, 3),
                                p.PhiMG,
                                p.ThanhTienMGMT,
                                p.TenNC,
                                p.MaBC,
                                p.MaMT,
                                p.MaKH,
                                p.MaKHBC,
                                p.StartDate,
                                p.UpdateDate,
                                p.PhiMGCT,
                                p.PhiMGCL,
                                p.PhiMGCTMua,
                                p.PhiMGDTMua,
                                p.Color,
                                p.HoTenNLH,
                                //DTDD = Common.Right(p.DTDD, 3),
                                p.HoTenNVN,
                                p.Code,
                                p.StatusEndDate,
                                p.OverTime,
                                p.StatusOverTime
                            });
                        }
                        else if (obj.DienThoai3Dau == false)
                        {
                            gcDaChao.DataSource = db.gdGetManagerGiaoDichV3(tuNgay, denNgay, MaNV, true, MaNV, -1, -1, arrMaTT, arrMaNV).Select(p => new
                            {
                                p.ID,
                                p.STT,
                                p.MaLoai,
                                p.DiaChi,
                                p.DacTrung,
                                p.GhiChu,
                                LinkAnh = p.LinkAnh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                p.ToaDo,
                                p.HoTenKHBC,
                                p.DienThoai1,
                                p.DiDong2,
                                p.DTDD,
                                p.DienThoai,
                                p.Phone2,
                                // DienThoai1 = Common.Right1(p.DienThoai1, 3),
                                // DiDong2 = Common.Right1(p.DiDong2, 3),
                                p.PhiMoiGioi,
                                p.ThanhTienMG,
                                p.TenTT,
                                p.ThuongHieu,
                                p.HoTenKH,
                                //DienThoai = Common.Right1(p.DienThoai, 3), // không check thì ko dc xem 3 số 
                                //Phone2 = Common.Right1(p.Phone2, 3),
                                p.PhiMG,
                                p.ThanhTienMGMT,
                                p.TenNC,
                                p.MaBC,
                                p.MaMT,
                                p.MaKH,
                                p.MaKHBC,
                                p.StartDate,
                                p.UpdateDate,
                                p.PhiMGCT,
                                p.PhiMGCL,
                                p.PhiMGCTMua,
                                p.PhiMGDTMua,
                                p.Color,
                                p.HoTenNLH,
                                //DTDD = Common.Right1(p.DTDD, 3),
                                p.HoTenNVN,
                                p.Code,
                                p.StatusEndDate,
                                p.OverTime,
                                p.StatusOverTime
                            });

                        }
                        else if (obj.DienThoaiAn == false)
                        {
                            gcDaChao.DataSource = db.gdGetManagerGiaoDichV3(tuNgay, denNgay, MaNV, true, MaNV, -1, -1, arrMaTT, arrMaNV).Select(p => new
                            {
                                p.ID,
                                p.STT,
                                p.MaLoai,
                                p.DiaChi,
                                p.DacTrung,
                                p.GhiChu,
                                LinkAnh = p.LinkAnh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                p.ToaDo,
                                p.HoTenKHBC,
                                DienThoai1 = "",
                                DiDong2 = "",
                                p.PhiMoiGioi,
                                p.ThanhTienMG,
                                p.TenTT,
                                p.ThuongHieu,
                                p.HoTenKH,
                                DienThoai = "",
                                Phone2 = "",
                                p.PhiMG,
                                p.ThanhTienMGMT,
                                p.TenNC,
                                p.MaBC,
                                p.MaMT,
                                p.MaKH,
                                p.MaKHBC,
                                p.StartDate,
                                p.UpdateDate,
                                p.PhiMGCT,
                                p.PhiMGCL,
                                p.PhiMGCTMua,
                                p.PhiMGDTMua,
                                p.Color,
                                p.HoTenNLH,
                                DTDD = "",
                                p.HoTenNVN,
                                p.Code,
                                p.StatusEndDate,
                                p.OverTime,
                                p.StatusOverTime
                            });
                        }
                        else
                        {
                            gcDaChao.DataSource = db.gdGetManagerGiaoDichV3(tuNgay, denNgay, MaNV, true, MaNV, -1, -1, arrMaTT, arrMaNV).Select(p => new
                            {
                                p.ID,
                                p.STT,
                                p.MaLoai,
                                p.DiaChi,
                                p.DacTrung,
                                p.GhiChu,
                                LinkAnh = p.LinkAnh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                p.ToaDo,
                                p.HoTenKHBC,
                                DienThoai1 = p.DienThoai1,
                                DiDong2 = p.DiDong2,
                                p.PhiMoiGioi,
                                p.ThanhTienMG,
                                p.TenTT,
                                p.ThuongHieu,
                                p.HoTenKH,
                                DienThoai = p.DienThoai,
                                Phone2 = p.Phone2,
                                p.PhiMG,
                                p.ThanhTienMGMT,
                                p.TenNC,
                                p.MaBC,
                                p.MaMT,
                                p.MaKH,
                                p.MaKHBC,
                                p.StartDate,
                                p.UpdateDate,
                                p.PhiMGCT,
                                p.PhiMGCL,
                                p.PhiMGCTMua,
                                p.PhiMGDTMua,
                                p.Color,
                                p.HoTenNLH,
                                DTDD = p.DTDD,
                                p.HoTenNVN,
                                p.Code,
                                p.StatusEndDate,
                                p.OverTime,
                                p.StatusOverTime
                            });
                        }
                        break;
                    default:
                        gcDaChao.DataSource = null;
                        break;
                }

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

        private void BaoCao_Edit()
        {
            if (grvDaChao.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn công việc cần chỉnh sửa!");
                return;
            }
            using (var frm = new frmEdit())
            {
                frm.MaCV = (int?)grvDaChao.GetFocusedRowCellValue("ID");
                frm.ShowDialog();
            }

        }

        private void BaoCao_Delete()
        {
            var indexs = grvDaChao.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn báo cáo!");
                return;
            }
            if (DialogBox.Question() == DialogResult.No) return;
            try
            {
                foreach (var i in indexs)
                {
                    var objBC = db.mglBCCongViecs.Single(p => p.ID == (int)grvDaChao.GetRowCellValue(i, "ID"));
                    db.mglBCCongViecs.DeleteOnSubmit(objBC);
                }
                db.SubmitChanges();

            }
            catch (Exception ex)
            {
                DialogBox.Error("Dữ liệu đã tồn tại ràng buộc nên bạn không thể xóa!");
            }
            grvDaChao.DeleteSelectedRows();
        }

        private void TabPage_Load()
        {
            try
            {
                int? maMT = (int?)grvDaChao.GetFocusedRowCellValue("MaMT");
                int? maBc = (int?)grvDaChao.GetFocusedRowCellValue("MaBC");
                int? Id = (int?)grvDaChao.GetFocusedRowCellValue("ID");
                if (Id == 0)
                {
                    gcSanPham.DataSource = null;
                    return;
                }

                gcSanPham.DataSource = null;
                switch (tabMain.SelectedTabPageIndex)
                {
                    case 0:
                        #region San pham
                        gcSanPham.DataSource = db.gdLichSuGiaoDichMT(Id);

                        #endregion
                        break;
                    case 1:
                        ctlTaiLieu2.FormID = 174;
                        ctlTaiLieu2.LinkID = (int?)grvDaChao.GetFocusedRowCellValue("MaBC");
                        ctlTaiLieu2.MaNV = Common.StaffID;
                        ctlTaiLieu2.TaiLieu_Load();
                        break;
                    case 2:
                        #region Lich lam viec
                        int? maBC = (int?)grvDaChao.GetFocusedRowCellValue("MaBC");
                        var obj = db.mglbcPhanQuyens.Single(p => p.MaNV == Common.StaffID);
                        if (obj.DienThoai == false)
                        {
                            var listNhatKy = (from p in db.mglbcNhatKyXuLies
                                              join nv in db.NhanViens on p.MaNVG equals nv.MaNV into nhanvien
                                              from nv in nhanvien.DefaultIfEmpty()
                                              join nv1 in db.NhanViens on p.MaNVN equals nv1.MaNV into nhanvien1
                                              from nv1 in nhanvien1.DefaultIfEmpty()
                                              join pt in db.PhuongThucXuLies on p.MaPT equals pt.MaPT into phuongthuc
                                              from pt in phuongthuc.DefaultIfEmpty()
                                              join t in db.bdsStatusCalls on p.MaTTGoi equals t.MaTT into ttt
                                              from tt in ttt.DefaultIfEmpty()
                                              where p.MaBC == maBC
                                              orderby p.NgayXL descending
                                              select new
                                              {
                                                  p.ID,
                                                  p.NgayXL,
                                                  p.TieuDe,
                                                  p.MaTT,
                                                  p.NoiDung,
                                                  p.MaNVG,
                                                  HoTenNVG = nv.HoTen,
                                                  HoTenNVN = nv1.HoTen,
                                                  p.KetQua,
                                                  pt.TenPT,
                                                  p.StartDate,
                                                  p.Enddate,
                                                  StatusCall = tt.TenTT,
                                                  MaTTBefore = p.MaTTBefore,
                                                  p.FileRecord,
                                                  isSyncFileRecord = p.FileRecord == null ? false : true,
                                                  DiDong = Common.Right(p.DiDong, 3)
                                              }).ToList();
                            gcNhatKy.DataSource = listNhatKy;
                        }
                        else if (obj.DienThoai3Dau == false)
                        {
                            var listNhatKy = (from p in db.mglbcNhatKyXuLies
                                              join nv in db.NhanViens on p.MaNVG equals nv.MaNV into nhanvien
                                              from nv in nhanvien.DefaultIfEmpty()
                                              join nv1 in db.NhanViens on p.MaNVN equals nv1.MaNV into nhanvien1
                                              from nv1 in nhanvien1.DefaultIfEmpty()
                                              join pt in db.PhuongThucXuLies on p.MaPT equals pt.MaPT into phuongthuc
                                              from pt in phuongthuc.DefaultIfEmpty()
                                              join t in db.bdsStatusCalls on p.MaTTGoi equals t.MaTT into ttt
                                              from tt in ttt.DefaultIfEmpty()
                                              where p.MaBC == maBC
                                              orderby p.NgayXL descending
                                              select new
                                              {
                                                  p.ID,
                                                  p.NgayXL,
                                                  p.TieuDe,
                                                  p.MaTT,
                                                  p.NoiDung,
                                                  p.MaNVG,
                                                  HoTenNVG = nv.HoTen,
                                                  HoTenNVN = nv1.HoTen,
                                                  p.KetQua,
                                                  pt.TenPT,
                                                  p.StartDate,
                                                  p.Enddate,
                                                  StatusCall = tt.TenTT,
                                                  MaTTBefore = p.MaTTBefore,
                                                  p.FileRecord,
                                                  isSyncFileRecord = p.FileRecord == null ? false : true,
                                                  DiDong = Common.Right1(p.DiDong, 3)
                                              }).ToList();
                            gcNhatKy.DataSource = listNhatKy;

                        }
                        else if (obj.DienThoaiAn == false)
                        {
                            var listNhatKy = (from p in db.mglbcNhatKyXuLies
                                              join nv in db.NhanViens on p.MaNVG equals nv.MaNV into nhanvien
                                              from nv in nhanvien.DefaultIfEmpty()
                                              join nv1 in db.NhanViens on p.MaNVN equals nv1.MaNV into nhanvien1
                                              from nv1 in nhanvien1.DefaultIfEmpty()
                                              join pt in db.PhuongThucXuLies on p.MaPT equals pt.MaPT into phuongthuc
                                              from pt in phuongthuc.DefaultIfEmpty()
                                              join t in db.bdsStatusCalls on p.MaTTGoi equals t.MaTT into ttt
                                              from tt in ttt.DefaultIfEmpty()
                                              where p.MaBC == maBC
                                              orderby p.NgayXL descending

                                              select new
                                              {
                                                  p.ID,
                                                  p.NgayXL,
                                                  p.TieuDe,
                                                  p.MaTT,
                                                  p.NoiDung,
                                                  p.MaNVG,
                                                  HoTenNVG = nv.HoTen,
                                                  HoTenNVN = nv1.HoTen,
                                                  p.KetQua,
                                                  pt.TenPT,
                                                  p.StartDate,
                                                  p.Enddate,
                                                  StatusCall = tt.TenTT,
                                                  MaTTBefore = p.MaTTBefore,
                                                  p.FileRecord,
                                                  isSyncFileRecord = p.FileRecord == null ? false : true,
                                                  DiDong = ""
                                              }).ToList();
                            gcNhatKy.DataSource = listNhatKy;
                        }
                        else
                        {
                            var listNhatKy = (from p in db.mglbcNhatKyXuLies
                                              join nv in db.NhanViens on p.MaNVG equals nv.MaNV into nhanvien
                                              from nv in nhanvien.DefaultIfEmpty()
                                              join nv1 in db.NhanViens on p.MaNVN equals nv1.MaNV into nhanvien1
                                              from nv1 in nhanvien1.DefaultIfEmpty()
                                              join pt in db.PhuongThucXuLies on p.MaPT equals pt.MaPT into phuongthuc
                                              from pt in phuongthuc.DefaultIfEmpty()
                                              join t in db.bdsStatusCalls on p.MaTTGoi equals t.MaTT into ttt
                                              from tt in ttt.DefaultIfEmpty()
                                              where p.MaBC == maBC
                                              orderby p.NgayXL descending
                                              select new
                                              {
                                                  p.ID,
                                                  p.NgayXL,
                                                  p.TieuDe,
                                                  p.MaTT,
                                                  p.NoiDung,
                                                  p.MaNVG,
                                                  HoTenNVG = nv.HoTen,
                                                  HoTenNVN = nv1.HoTen,
                                                  p.KetQua,
                                                  pt.TenPT,
                                                  p.StartDate,
                                                  p.Enddate,
                                                  StatusCall = tt.TenTT,
                                                  MaTTBefore = p.MaTTBefore,
                                                  p.FileRecord,
                                                  isSyncFileRecord = p.FileRecord == null ? false : true,
                                                  p.DiDong
                                              }).ToList();
                            gcNhatKy.DataSource = listNhatKy;

                        }
                        #endregion
                        break;
                    case 3:
                        

                        var listNhatKyMT = (from p in db.mglmtNhatKyXuLies
                                          join nv in db.NhanViens on p.MaNVG equals nv.MaNV into nhanvien
                                          from nv in nhanvien.DefaultIfEmpty()
                                          join nv1 in db.NhanViens on p.MaNVN equals nv1.MaNV into nhanvien1
                                          from nv1 in nhanvien1.DefaultIfEmpty()
                                          join pt in db.PhuongThucXuLies on p.MaPT equals pt.MaPT into phuongthuc
                                          from pt in phuongthuc.DefaultIfEmpty()
                                          where p.MaMT == maMT
                                          orderby p.NgayXL descending

                                          select new
                                          {
                                              p.ID,
                                              p.NgayXL,
                                              p.TieuDe,
                                              p.MaTT,
                                              p.NoiDung,
                                              //p.PhuongThucXuLy.TenPT,
                                              p.MaNVG,
                                              HoTenNVG = nv.HoTen,
                                              HoTenNVN = nv1.HoTen,
                                              p.KetQua,
                                              pt.TenPT
                                          }).ToList();
                        gcNhatKyMT.DataSource = listNhatKyMT;
                        break;
                }
            }
            catch { }
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            cklNhanVien2.DataSource = db.NhanViens;
            lookTrangThai.DataSource = db.mglmtTrangThais;
           ckTrangThai.DataSource = db.mglTrangThaiGiaoDiches.Select(p=> new { p.Ord, p.MaLoai,TenTT = p.Code == null? p.TenTT :  "("+p.Code.ToString()+") "+p.TenTT}).OrderBy(p=>p.Ord);
            ckNhanVien.DataSource = db.NhanViens.Select(p => new { p.MaNV, HoTen = p.MaSo == null ? p.HoTen : p.HoTen + " (" + p.MaSo.ToString() + ")" });
            lkTrangThaiv1.DataSource = db.mglbcTrangThais;
           it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(0);
            //itemTuNgay.EditValue = new DateTime(2012, 5, 28);
            itemTuNgay.EditValue = DateTime.Now.AddDays(-1);
            itemNhanVien.EditValue = Common.StaffID.ToString();
           // ckNhanVien.value = Common.StaffID;
            LoadPermission();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Load();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Edit();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Delete();
        }

        private void grvMuaThue_DoubleClick(object sender, EventArgs e)
        {
            if (grvDaChao.FocusedRowHandle < 0)
            {
                return;
            }
            BaoCao_Edit();
        }

        private void grvMuaThue_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                BaoCao_Delete();
            }
        }

        void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            //BaoCao_Load();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            //BaoCao_Load();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            TabPage_Load();
        }

        private void itemIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemSP_GiaoDich_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvDaChao.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn nhu cầu");
                return;
            }

            if ((byte)grvDaChao.GetFocusedRowCellValue("MaTT") > 2)
            {
                DialogBox.Error("Nhu cầu đã giao dịch hoặc đang khóa");
                return;
            }

            int maBC = (int)gvSanPham.GetFocusedRowCellValue("MaSP");
            int maMT = (int)grvDaChao.GetFocusedRowCellValue("MaMT");
            int rowCount = db.mglgdGiaoDiches.Where(p => p.MaBC == maBC & p.MaMT == maMT).Count();
            if (rowCount > 0)
            {
                DialogBox.Error("Sản phẩm đã được giao dịch đang chờ duyệt");
                return;
            }
            GiaoDich.frmEdit frm = new BEE.HoatDong.MGL.GiaoDich.frmEdit();
            frm.MaMT = maMT;
            frm.MaBC = maBC;
            frm.ShowDialog();
        }

        private void itemUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] indexs = grvDaChao.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn sản phẩm");
                return;
            }
            foreach (var i in indexs)
            {
                if ((int)grvDaChao.GetRowCellValue(i, "MaNV") == Common.StaffID)
                {
                    db.mglBCCongViecs.Single(p => p.ID == (int)grvDaChao.GetRowCellValue(i, "ID")).NgayXuLy = DateTime.Now;
                }
            }
            db.SubmitChanges();

            BaoCao_Load();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            it.CommonCls.ExportExcel(gcDaChao);
        }

        private void itemXuLySP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvSanPham.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Vui lòng chọn bất động sản để xử lý!");
                gvSanPham.Focus();
                return;
            }
            using (var frm = new frmXuLy() { maSP = (int?)gvSanPham.GetFocusedRowCellValue("ID") })
            {
                frm.ShowDialog();
            }
        }

        private void itemXoaSP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvSanPham.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Vui lòng chọn bất động sản để xóa!");
                gvSanPham.Focus();
                return;
            }
            try
            {
                var objdelete = db.mglBCSanPhams.FirstOrDefault(p => p.ID == (int?)gvSanPham.GetFocusedRowCellValue("ID"));
                db.mglBCSanPhams.DeleteOnSubmit(objdelete);
                db.SubmitChanges();
                DialogBox.Infomation("Dữ liệu đã được xóa!");
            }
            catch (Exception ex)
            {
                DialogBox.Error("Lỗi: " + ex.Message);
            }
        }

        private void itemGDSP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvSanPham.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Vui lòng chọn bất động sản để giao dịch!");
                gvSanPham.Focus();
                return;
            }
            var objSP = db.mglBCSanPhams.FirstOrDefault(p => p.ID == (int?)gvSanPham.GetFocusedRowCellValue("ID"));
            using (var frm = new MGL.GiaoDich.frmEdit())
            {
                int? maMT = objSP.mglBCCongViec.MaCoHoiMT;
                int? maBC = objSP.MaSP;
                int rowCount = db.mglgdGiaoDiches.Where(p => p.MaBC == maBC & p.MaMT == maMT & p.MaTT != 6).Count();
                if (rowCount > 0)
                {
                    DialogBox.Error("Sản phẩm đã được giao dịch đang chờ duyệt");
                    return;
                }
                frm.objCV = objSP.mglBCCongViec;
                frm.MaMT = (int)maMT;
                frm.MaBC = (int)maBC;
                frm.ShowDialog();
            }
        }

        private void gvSanPham_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

            //if (gvSanPham.FocusedRowHandle < 0)
            //{
            //    gcXuLy.DataSource = null;
            //    return;
            //}

            //gcXuLy.DataSource = null;
            //var MaSP = (int?)gvSanPham.GetFocusedRowCellValue("ID");
            //gcXuLy.DataSource = db.mglspNhatKyXuLies.Where(p => p.MaSP == MaSP).OrderByDescending(p => p.NgayXL).Select(p => new
            //{
            //    p.ID,
            //    p.NhanVien.HoTen,
            //    p.NoiDung,
            //    NgayXuLy = p.NgayXL,
            //    p.mglbcTrangThaiXL.TenTT

            //});
        }

        private void btnCall_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var namecol = grvDaChao.FocusedColumn.Name;
            var makh = (int?)grvDaChao.GetFocusedRowCellValue("MaKH");
            var objkh = db.KhachHangs.SingleOrDefault(p => p.MaKH == makh);
            if (namecol == "colDienThoai1" || namecol == "colDiDong2")
            {
                makh = (int?)grvDaChao.GetFocusedRowCellValue("MaKHBC");
                objkh = db.KhachHangs.SingleOrDefault(p => p.MaKH == makh);
            }
            else
            {
                objkh = db.KhachHangs.SingleOrDefault(p => p.MaKH == makh);
            }

            string sdt = "";
            string sdthidden = "";
            objkh.ExEndCall = db.voipLineConfigs.SingleOrDefault(p => p.MaNV == Common.StaffID)?.LineNumber;
            db.SubmitChanges();
            switch (namecol)
            {
                case "colDienThoai1":
                    sdt = grvDaChao.GetFocusedRowCellValue("DienThoai").ToString();
                    sdthidden = grvDaChao.GetFocusedRowCellValue("DienThoai").ToString();
                    break;
                case "colDiDong2":
                    sdt = objkh.DiDong2;
                    sdthidden = grvDaChao.GetFocusedRowCellValue("DiDong2").ToString();
                    break;
                case "colDienThoai":
                    sdt = grvDaChao.GetFocusedRowCellValue("DienThoai").ToString();
                    sdthidden = grvDaChao.GetFocusedRowCellValue("DienThoai").ToString();
                    break;
                case "colPhone2":
                    sdt = objkh.DiDong2;
                    sdthidden = grvDaChao.GetFocusedRowCellValue("Phone2").ToString();
                    break;
            }
            ClickToCall(sdt);
            KhaoSat(sdt, sdthidden, false, makh);

        }
        private void ClickToCall(string Phone)
        {
            var line = db.voipLineConfigs.Where(p => p.MaNV == Common.StaffID);

            if (line.Count() == 0)
            {
                DialogBox.Infomation("Tài khoản này chưa được cấp Line. Nên không thể thực hiện cuộc gọi!");
                return;
            }
            if (Phone == "")
            {
                DialogBox.Infomation("Khách hàng này chưa có số điện thoại, vui lòng kiểm tra lại!");
                return;
            }
            var pre = line.First().PreCall ?? "";
            var obj = line.First().LineNumber;
            var objConfig = db.voipServerConfigs.FirstOrDefault();

            PopupUCMThienVM.PopupUCMThienVM m = new PopupUCMThienVM.PopupUCMThienVM(objConfig.Host, 5038, objConfig.UserName, objConfig.Pass);
            if (m.xacthuc(objConfig.KeyConnect))
            {
                m.Clicktocall(string.Format("{0}{1}", pre, Phone), obj);
            }
            else
            {
                MessageBox.Show("Key kết nối tổng đài sai. Vui lòng kiểm tra lại");
            }

        }
        void KhaoSat(string sdt, string sdthidden, bool? isNLH, int? MaKH)
        {
            var db = new MasterDataContext();
            var objConfig = db.voipServerConfigs.FirstOrDefault();
            var objLine = db.voipLineConfigs.FirstOrDefault(p => p.MaNV == Common.StaffID);
            if (objLine == null)
            {
                DialogBox.Infomation("vui lòng cài đặt line để gọi đi, xin cảm ơn!");
                return;
            }
            var frC = new BEE.VOIPSETUP.Call.frmCall();
            frC.SDT = sdt;
            frC.isNLH = isNLH;
            frC.sdtHiden = sdthidden;
            frC.MaKH = MaKH;
            frC.MaMT = (int?)grvDaChao.GetFocusedRowCellValue("MaMT");
            // frC.ModuleID = 0;
            frC.line = objLine.LineNumber;
            frC.NhanVienTN = Common.StaffName;
            frC.LoaiCG = 0;
            frC.ShowDialog();
        }

        private void grvDaChao_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TabPage_Load();
        }

        private void itemChangeStatus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvDaChao.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if (DialogBox.Question("Bạn có muốn chuyển thông tin không") == DialogResult.No) return;
            try
            {
                var frm = new frmChuyenDoiMT();
                frm.MaLoaiTruoc = Convert.ToByte(grvDaChao.GetFocusedRowCellValue("MaLoai"));
                frm.MaLoai = 2; // set mặc định chuyển đổi trạng thái :  đã đi xem
                frm.EnabledStatus = true;
                frm.ShowDialog();
                if (frm.isSave == true)
                {
                    var id = (int?)grvDaChao.GetFocusedRowCellValue("ID");
                    var maMT = (int?)grvDaChao.GetFocusedRowCellValue("MaMT");
                    var obj = db.mglMTSanPhams.SingleOrDefault(p => p.ID == id);


                    // nếu trạng thái đã chào ghi nhận cả ngày bđ và update, còn các trạng thái sau thì chỉ cần update
                   if(frm.MaLoai == 1)
                    {
                        obj.StartDate = DateTime.Now;
                        obj.UpdateDate = DateTime.Now;
                    }
                   else
                    {
                        obj.UpdateDate = DateTime.Now;
                    }    
                   

                    obj.MaLoai = frm.MaLoai;

                    // người chào hoặc xử lý sau không được cập nhật là nhân viên viên cuối cùng (bỏ update manv)
                   // obj.MaNV = Common.StaffID;

                    var objBC = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == (int)grvDaChao.GetFocusedRowCellValue("MaBC"));
                    if (objBC != null)
                    {
                        if (frm.MaLoai == 10 && (objBC.PhiMG - (obj.PhiMGDTBan == null ? 0 : obj.PhiMGDTBan)) > 0)
                        {
                            DialogBox.Warning("Bạn không thể chuyển trạng thái [Đã thu phí] khi chưa thu đủ phí");
                            return;
                        }
                    }

                    // bảng này thiếu thông tin cần bán
                    var objls = new mglLichSuXuLyGiaoDich_MT();
                    objls.MaNV = Common.StaffID;
                    objls.NoiDung = frm.NoiDung;
                    objls.MaMT = (int?)grvDaChao.GetFocusedRowCellValue("MaMT");
                    objls.MaLoai = frm.MaLoai;
                    objls.MaMTCV = (int?)grvDaChao.GetFocusedRowCellValue("ID");
                    objls.Title = frm.TieuDe;
                    objls.MaLoaiTruoc = frm.MaLoaiTruoc;
                    objls.NgayXL = frm.NgayXL;
                    objls.MaPT = frm.MaPT;
                    db.mglLichSuXuLyGiaoDich_MTs.InsertOnSubmit(objls);

                    //  ghi nhận : ngày cập nhật trạng thái cuối cùng
                    obj.StatusEndDate = db.getDate();

                    db.SubmitChanges();
                    DialogBox.Infomation("Thao thác thành công");
                    BaoCao_Load();
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void grvDaChao_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                if (e.Column.FieldName == "TenTT")
                {
                    var color = grvDaChao.GetRowCellValue(e.RowHandle, "Color");
                    if (color != null)
                    {
                        var colorArr = color.ToString().Split(',');
                        if (colorArr.Length > 1)
                        {
                            e.Appearance.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(colorArr[0].Trim()), Convert.ToInt32(colorArr[1].Trim()), Convert.ToInt32(colorArr[2].Trim()));
                        }
                        else
                        {
                            e.Appearance.BackColor = System.Drawing.Color.FromName(color.ToString());
                        }

                    }

                }
                if (e.Column.FieldName == "StatusOverTime")
                {
                    var OverTime = (int?)grvDaChao.GetRowCellValue(e.RowHandle, "OverTime");
                    if (OverTime <= 0)
                    {

                        e.Appearance.BackColor = System.Drawing.Color.Red;

                       
                    }

                    else
                    {
                        e.Appearance.BackColor = System.Drawing.Color.Green;
                    }


                }
            }
            catch { }
        }

        private void itemPhieuThuBan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)grvDaChao.GetFocusedRowCellValue("ID");
            using (var frm = new PhieuThu.frmEdit())
            {
                frm.MaNT = 1;
                frm.GD_ID = (int?)grvDaChao.GetFocusedRowCellValue("ID");
                frm.MaBC = (int?)grvDaChao.GetFocusedRowCellValue("MaBC");
                frm.ShowDialog();

                var total = db.pgcPhieuThus.Where(p => p.GDId == id && p.MaBC != null).Sum(p => p.SoTien);
                var objBc = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == (int?)grvDaChao.GetFocusedRowCellValue("MaBC"));
                var objData = db.mglMTSanPhams.FirstOrDefault(p => p.ID == id);
                if (objData != null && objBc != null)
                {
                    objData.PhiMGDTBan = total;
                    objData.PhiMGCTBan = objBc.PhiMG - total;
                }
                db.SubmitChanges();
            }
            BaoCao_Load();
        }

        private void itemPhieuThuMua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)grvDaChao.GetFocusedRowCellValue("ID");
            using (var frm = new PhieuThu.frmEdit())
            {
                frm.MaNT = 2;
                frm.GD_ID = (int?)grvDaChao.GetFocusedRowCellValue("ID");
                frm.MaBC = (int?)grvDaChao.GetFocusedRowCellValue("MaBC");
                frm.MaMT = (int?)grvDaChao.GetFocusedRowCellValue("MaMT");
                frm.ShowDialog();

                var total = db.pgcPhieuThus.Where(p => p.GDId == id && p.MaMT != null).Sum(p => p.SoTien);
                var objMt = db.mglmtMuaThues.FirstOrDefault(p => p.MaMT == (int?)grvDaChao.GetFocusedRowCellValue("MaMT"));
                var objData = db.mglMTSanPhams.FirstOrDefault(p => p.ID == id);
                
                if (objData != null && objMt != null)
                {
                    objData.PhiMGDTMua = total;
                    objData.PhiMGCTMua = objMt.PhiMG - total;
                }
                db.SubmitChanges();
            }
            BaoCao_Load();
        }

        private void btnDongBo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string dd = "";
            var id = (int?)grvNhatKy.GetFocusedRowCellValue("ID");
            try
            {
                dd = grvNhatKy.GetFocusedRowCellValue("DiDong").ToString();

            }
            catch { }

            if (id == null || id == 0)
            {
                DialogBox.Error("Vui lòng chọn nhật ký xử lý");
                return;
            }
            VOIPSETUP.Record.frmRecordFTP frm = new VOIPSETUP.Record.frmRecordFTP();
            frm.id = id;
            frm.didong = dd ?? "";
            frm.Show();
        }

        private void itemAddLSXL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvSanPham.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Vui lòng chọn bất động sản để xử lý!");
                gvSanPham.Focus();
                return;
            }
            using (var frm = new frmLSXL()
            {
                ID = (int?)grvDaChao.GetFocusedRowCellValue("ID")
            })
            {
                frm.ShowDialog();
            }

        }
    }
}