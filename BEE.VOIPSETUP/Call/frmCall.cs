using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using System.Text.RegularExpressions;
using BEEREMA;
using BEE.NghiepVuKhac;
using System.Threading;
using System.IO;
using BEE.HoatDong.MGL.Ban.Models.MessageQueue;
using RestSharp;
using Newtonsoft.Json;

namespace BEE.VOIPSETUP.Call
{
    public partial class frmCall : DevExpress.XtraEditors.XtraForm
    {
        public int? MaNC { get; set; }
        public int? ID { get; set; }
        public bool IsSave { get; set; }
        MasterDataContext db;
        public string MyProperty { get; set; }
        public string SDT;
        public string Unique;
        public string KhachHang { get; set; }
        public string line { get; set; }
        public string NhanVienTN { get; set; }
        public int? LoaiCG { get; set; }
        public int? MaKH;
        BEE.ThuVien.KhachHang objKH;
        public bool btnCCH = true;

        public int? MaBC { get; set; }
        public int? MaMT { get; set; }

        public string sdtHiden { get; set; }

        public bool? isNLH { get; set; }
        public int? MaNLH { get; set; }
        public DateTime StartDate { get; set; }
        BEE.ThuVien.NguoiDaiDien objNDD;
        public int IdHistory { get; set; }

        public frmCall()
        {
            InitializeComponent();
        }
        void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        int GetAccessDataBan()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 174);

            return o.SDB.SDBID;
        }


        int GetAccessDataCanMua()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 176);

            return o.SDB.SDBID;
        }

        private void frmCall_Load(object sender, EventArgs e)
        {

            db = new MasterDataContext();
            LoadPermissionKH();
            dateNgayGhiNhan.EditValue = db.getDate();
            lookPhuongThuc.Properties.DataSource = db.PhuongThucXuLies;

            lkLoaiBDS.Properties.DataSource = db.LoaiBDs;
            lkTrangThai.DataSource = lookBefore.Properties.DataSource = lookTrangThai.Properties.DataSource = db.mglbcTrangThais;
            lookNCD.DataSource = lookNgCDTuVan.Properties.DataSource = db.NhanViens.Select(p => new { p.HoTen, p.MaNV });
            lookUpNhomKH.Properties.DataSource = db.NhomKHs;
            lookNguonDen1.Properties.DataSource = db.mglNguons.Select(p => new { HowToKnowID = p.MaNguon, Name = p.TenNguon });

            lookQuyDanh.Properties.DataSource = db.QuyDanhs;
            lookMoiQuanHe.Properties.DataSource = db.MoiQuanHes;
            lookQuyDanh2.Properties.DataSource = db.QuyDanhs;
            lookStatusCall.Properties.DataSource = db.bdsStatusCalls;

            lookAddress.Properties.DataSource = (from p in db.mglbcBanChoThues
                                                 from t in db.Tinhs.Where(t => t.MaTinh == p.MaTinh).DefaultIfEmpty()
                                                 from h in db.Huyens.Where(h => h.MaHuyen == p.MaHuyen).DefaultIfEmpty()
                                                 from x in db.Xas.Where(x => x.MaXa == p.MaXa).DefaultIfEmpty()
                                                 from d in db.Duongs.Where(d => d.MaDuong == p.MaDuong).DefaultIfEmpty()
                                                 where p.MaKH == MaKH
                                                 select new
                                                 {
                                                     MaBC = p.MaBC,
                                                     Address = $"{p.SoNha} {d.TenDuong}, {h.TenHuyen}, {t.TenTinh}"
                                                 }).ToList();


            itemEditHistory.Enabled = true;

            if (this.MaBC > 0)
            {
                LoadMaBC((int)this.MaBC);
                xtraTabPage1.PageVisible = false;
            }

            if (isNLH == false)
            {

                if (SDT != null || Unique != null)
                {
                    var obj = db.KhachHangs.Where(p => p.DiDong == SDT || p.DienThoaiCT == SDT || p.DiDong == SDT || p.DiDong2 == SDT || p.DiDong3 == SDT || p.DiDong4 == SDT).ToList();
                    if (obj.Count() > 0)
                    {
                        objKH = new BEE.ThuVien.KhachHang();
                        this.objKH = db.KhachHangs.FirstOrDefault((p => p.DiDong == SDT || p.DienThoaiCT == SDT || p.DiDong == SDT || p.DiDong2 == SDT || p.DiDong3 == SDT || p.DiDong4 == SDT));
                        txtSDT.Text = SDT;
                        txtUnique.Text = Unique;
                        txtLine.Text = line;
                        txtKhachHang.Text = KhachHang;
                        dateNgayGhiNhan.EditValue = db.getDate();
                        lbNotice.Text = sdtHiden + " - KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";
                        lbNotice.BackColor = Color.Green;
                        lbNotice.ForeColor = Color.White;
                        KhachHangLoad(objKH.MaKH);
                        lookNgCDTuVan.EditValue = Common.StaffID;
                        this.MaKH = objKH.MaKH;
                        var td = "";
                        try
                        {
                            td = objKH.QuyDanh.TenQD;
                        }
                        catch
                        {

                            td = "";
                        }
                        txtTieuDe.Text = "Gọi: " + td + " " + objKH.HoTenKH;



                    }
                    else
                    {
                        lbNotice.Text = SDT + " - KHÁCH HÀNG KHÔNG CÓ TRONG HỆ THỐNG";
                        lbNotice.BackColor = Color.Red;
                        lbNotice.ForeColor = Color.White;
                        objKH = new ThuVien.KhachHang();
                        gcBan.DataSource = null;
                        gcMuaThue.DataSource = null;
                        KhachHangAddNew();

                        txtDiDong.Enabled = true;
                        txtDiDong2.Enabled = true;

                        txtDienThoai3.Enabled = true;

                        txtDienThoai4.Enabled = true;
                    }
                }
            }
            else
            {
                xtraTabControl2.SelectedTabPage = tabNguoiLienHe;
                if (SDT != null || Unique != null)
                {
                    objNDD = new BEE.ThuVien.NguoiDaiDien();
                    var obj = db.NguoiDaiDiens.Where(p => (p.DTDD == SDT || p.DTCD == SDT) && p.MaKH == this.MaKH).ToList();
                    if (obj.ToList().Count() > 0)
                    {
                        this.objNDD = obj.Single();
                        //  txtSDT.Text = SDT;
                        txtUnique.Text = Unique;
                        txtLine.Text = line;
                        txtKhachHang.Text = KhachHang;
                        dateNgayGhiNhan.EditValue = db.getDate();
                        lbNotice.Text = sdtHiden + " - KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";
                        lbNotice.BackColor = Color.Green;
                        lbNotice.ForeColor = Color.White;
                        NguoiLienHe_Load(objNDD.MaNDD);

                        lookNgCDTuVan.EditValue = Common.StaffID;

                        this.MaNLH = objNDD.MaNDD;
                        var td = "";
                        try
                        {
                            td = objNDD.QuyDanh.TenQD;
                        }
                        catch
                        {

                            td = "";
                        }
                        txtTieuDe.Text = "Gọi NLH: " + td + " " + objNDD.HoTen;

                        if (txtDiDong.Text == "")
                        {
                            txtDiDong.Enabled = true;
                        }
                        if (txtDiDong2.Text == "")
                        {
                            txtDiDong2.Enabled = true;
                        }
                        if (txtDienThoai3.Text == "")
                        {
                            txtDienThoai3.Enabled = true;
                        }
                        if (txtDienThoai4.Text == "")
                        {
                            txtDienThoai4.Enabled = true;

                        }
                        KhachHangLoad(objNDD.MaKH);

                    }
                    else
                    {
                        lbNotice.Text = SDT + " - KHÁCH HÀNG KHÔNG CÓ TRONG HỆ THỐNG";
                        lbNotice.BackColor = Color.Red;
                        lbNotice.ForeColor = Color.White;
                        objKH = new ThuVien.KhachHang();
                        gcBan.DataSource = null;
                        gcMuaThue.DataSource = null;
                        KhachHangAddNew();
                    }
                }

            }



        }
        short? MaHuyen { get; set; }
        short? MaHuyen2 { get; set; }
        byte? MaTinh { get; set; }
        byte? MaTinh2 { get; set; }
        void KhachHangAddNew()
        {
            if ((bool?)itemAdd.Tag == false)
            {
                DialogBox.Error("Bạn không có quyền thêm");
                return;
            }
            db = new MasterDataContext();
            var objKH = new BEE.ThuVien.KhachHang();

            // txtTenVietTat.EditValue = db.DinhDang(13, (db.KhachHangs.Max(p => (int?)p.MaKH) ?? 0) + 1);

            //  txtDiDong.EditValue = SDT;
            txtEmailCT.EditValue = null;
            txtGhiChu.EditValue = null;
            txtDiDong.EditValue = SDT;
            lookNgCDTuVan.EditValue = Common.StaffID;

        }

        int GetAccessData() // cần cho thuê
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 175);

            return o.SDB.SDBID;
        }

        void LoadPermissionKH()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 9;
            DataTable tblAction = o.SelectBy();
            itemSuaKH.Enabled = false;
            btnSaveKH.Enabled = false;
            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            btnSaveKH.Enabled = true;
                            break;
                        case 2:
                            itemSuaKH.Enabled = true;
                            break;
                    }
                }
            }
        }
        void NguoiLienHe_Load(int? MaNDD)
        {
            db = new MasterDataContext();

            objNDD = db.NguoiDaiDiens.Single(p => p.MaNDD == MaNDD);
            if (objNDD.DTCD == "")
            {
                txtDTCDNDD.Enabled = true;
            }
            else
            {
                txtDTCDNDD.Enabled = false;
            }
            if (objNDD.DTDD == "")
            {
                txtDiDongNDD.Enabled = true;
            }
            else
            {
                txtDiDongNDD.Enabled = false;
            }

            // lookQuyDanh.EditValue = (byte?)objNDD.MaQD;
            lookMoiQuanHe.EditValue = objNDD.MaQH;
            txtDiaChiLH.Text = objNDD.DiaChiLL;
            txtDiaChiTT.Text = objNDD.DiaChiTT;


            var obj = db.mglbcPhanQuyens.Single(p => p.MaNV == Common.StaffID);

            if (obj.DienThoai == false)
            {
                txtDiDongNDD.Text = Common.Right(objNDD.DTDD, 3);
                txtDTCDNDD.Text = Common.Right(objNDD.DTCD, 3);

            }
            else if (obj.DienThoai3Dau == false)
            {
                txtDiDongNDD.Text = Common.Right1(objNDD.DTDD, 3);
                txtDTCDNDD.Text = Common.Right1(objNDD.DTCD, 3);
            }
            else if (obj.DienThoaiAn == false)
            {
                txtDiDongNDD.Text = "";
                txtDTCDNDD.Text = "";
            }
            else
            {
                txtDiDongNDD.Text = objNDD.DTDD;
                txtDTCDNDD.Text = objNDD.DTCD;
            }

            txtEmailNDD.Text = objNDD.Email;
            txtHoTenNDD.Text = objNDD.HoTen;
            dateNgayCap.EditValue = objNDD.NgayCap;
            dateNgaySinh.EditValue = objNDD.NgaySinh;
            txtNoiCap.Text = objNDD.NoiCap;
            txtSoCMND.Text = objNDD.SoCMND;
            txtMaSoThue.Text = objNDD.MaSoThue;
            txtNoiSinh.Text = objNDD.NoiSinh;
            try
            {
                btnDCTT.Text = string.Format("{0}, {1}, {2}", objNDD.Xa.TenXa, objNDD.Xa.Huyen.TenHuyen, objNDD.Xa.Huyen.Tinh.TenTinh);
                btnDCLH.Text = string.Format("{0}, {1}, {2}", objNDD.Xa1.TenXa, objNDD.Xa1.Huyen.TenHuyen, objNDD.Xa1.Huyen.Tinh.TenTinh);

            }
            catch { }

            #region phan quyền sdt
            //var obj = db.mglbcPhanQuyens.Single(p => p.MaNV == Common.StaffID);
            //switch (GetAccessData())
            //{
            //    case 1:
            //        if (obj.DienThoai == false)
            //        {
            //            txtDiDong.Text = Common.Right(objKH.DiDong, 3);
            //            txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
            //            txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
            //            txtDienThoai4.Text = Common.Right(objKH.DiDong4, 3);
            //        }
            //        else if (obj.DienThoai3Dau == false)
            //        {
            //            txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
            //            txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
            //            txtDiDong2.Text = Common.Right1(objKH.DiDong3, 3);
            //            txtDienThoai4.Text = Common.Right1(objKH.DiDong4, 3);
            //        }
            //        else
            //        {
            //            txtDiDong.Text = objKH.DiDong;
            //            txtDiDong2.Text = objKH.DiDong2;
            //            txtDiDong3.Text = objKH.DiDong3;
            //            txtDienThoai4.Text = objKH.DiDong4;

            //        }
            //        break;

            //    case 2:
            //        if (obj.DienThoai == false)
            //        {
            //            txtDiDong.Text = Common.Right(objKH.DiDong, 3);
            //            txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
            //            txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
            //            txtDienThoai4.Text = Common.Right(objKH.DiDong4, 3);
            //        }
            //        else if (obj.DienThoai3Dau == false)
            //        {
            //            txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
            //            txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
            //            txtDiDong3.Text = Common.Right1(objKH.DiDong3, 3);
            //            txtDienThoai4.Text = Common.Right1(objKH.DiDong4, 3);
            //        }
            //        else
            //        {
            //            txtDiDong.Text = objKH.DiDong;
            //            txtDiDong2.Text = objKH.DiDong2;
            //            txtDiDong3.Text = objKH.DiDong3;
            //            txtDienThoai4.Text = objKH.DiDong4;

            //        }

            //        break;
            //    case 3:
            //        if (obj.DienThoai == false)
            //        {
            //            txtDiDong.Text = Common.Right(objKH.DiDong, 3);
            //            txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
            //            txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
            //            txtDienThoai4.Text = Common.Right(objKH.DiDong4, 3);
            //        }
            //        else if (obj.DienThoai3Dau == false)
            //        {
            //            txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
            //            txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
            //            txtDiDong3.Text = Common.Right1(objKH.DiDong3, 3);
            //            txtDienThoai4.Text = Common.Right1(objKH.DiDong4, 3);
            //        }
            //        else
            //        {
            //            txtDiDong.Text = objKH.DiDong;
            //            txtDiDong2.Text = objKH.DiDong2;
            //            txtDiDong3.Text = objKH.DiDong3;
            //            txtDienThoai4.Text = objKH.DiDong4;

            //        }

            //        break;
            //    case 4:
            //        if (obj.DienThoai == false)
            //        {
            //            txtDiDong.Text = Common.Right(objKH.DiDong, 3);
            //            txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
            //            txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
            //            txtDienThoai4.Text = Common.Right(objKH.DiDong4, 3);
            //        }
            //        else if (obj.DienThoai3Dau == false)
            //        {
            //            txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
            //            txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
            //            txtDiDong3.Text = Common.Right1(objKH.DiDong3, 3);
            //            txtDienThoai4.Text = Common.Right1(objKH.DiDong4, 3);
            //        }
            //        else
            //        {
            //            txtDiDong.Text = objKH.DiDong;
            //            txtDiDong2.Text = objKH.DiDong2;
            //            txtDiDong3.Text = objKH.DiDong3;
            //            txtDienThoai4.Text = objKH.DiDong4;

            //        }
            //        break;



            //}
            #endregion

        }
        void KhachHangLoad(int? makh)
        {
            db = new MasterDataContext();
            try
            {
                objKH = db.KhachHangs.FirstOrDefault(p => p.MaKH == makh);
            }
            catch (Exception)
            {

            }
            //
            txtHoKH.Text = objKH.HoKH;
            txtTenKH.Text = objKH.TenKH;
            var xht = (from t in db.Tinhs
                       join h in db.Huyens on objKH.MaHuyen equals h.MaHuyen
                       join x in db.Xas on objKH.MaXa equals x.MaXa
                       where t.MaTinh == objKH.MaTinh
                       select new { x.TenXa, h.TenHuyen, t.TenTinh }).FirstOrDefault();
            //if (xht != null)
            //{
            //    btnTinhHuyenXa.Text = (objKH.MaXa != null ? xht.TenXa + ", " : "") +
            //        (objKH.MaHuyen != null ? xht.TenHuyen + ", " : "") +
            //        (objKH.MaTinh != null ? xht.TenTinh + ", " : "");
            //}

            txtDienThoaiCT.EditValue = objKH.DienThoaiCT;
            txtEmail.EditValue = objKH.Email;

            txtGhiChuKH.Text = objKH.GhiChu;
            txtEmailCT.Text = objKH.Email;
            txtFax.Text = objKH.FaxCT;
            txtEmailCT.Text = objKH.Email;
            txtMaSoThueCT.Text = objKH.MaSoThueCT;

            txtThuongTru.Text = objKH.ThuongTru;
            txtDiaChi.Text = objKH.DiaChi;
            lookUpNhomKH.EditValue = objKH.MaNKH;
            lookNguonDen1.EditValue = (short?)objKH.HowToKnowID;
            //lookQuyDanh.EditValue = (byte?)objKH.MaQD;
            // lookQuyDanh2.EditValue = (byte?)1;

            lookQuyDanh2.EditValue = (byte?)objKH.MaQD;

            if (objKH.DiDong == "")
            {
                txtDiDong.Enabled = true;
            }
            else
            {
                txtDiDong.Enabled = false;
            }
            if (objKH.DiDong2 == "")
            {
                txtDiDong2.Enabled = true;
            }
            else
            {
                txtDiDong2.Enabled = false;
            }
            if (objKH.DiDong3 == "")
            {
                txtDienThoai3.Enabled = true;
            }
            else
            {
                txtDienThoai3.Enabled = false;
            }
            if (objKH.DiDong4 == "")
            {
                txtDienThoai4.Enabled = true;

            }
            else
            {
                txtDienThoai4.Enabled = false;
            }


            var obj = db.mglbcPhanQuyens.Single(p => p.MaNV == Common.StaffID);
            switch (GetAccessData())
            {
                case 1:
                    if (obj.DienThoai == false)
                    {
                        txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                        txtDienThoai3.Text = Common.Right(objKH.DiDong3, 3);
                        txtDienThoai4.Text = Common.Right(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoai3Dau == false)
                    {
                        txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                        txtDienThoai3.Text = Common.Right1(objKH.DiDong3, 3);
                        txtDienThoai4.Text = Common.Right1(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoaiAn == false)
                    {
                        txtDiDong.Text = "";
                        txtDiDong2.Text = "";
                        txtDienThoai3.Text = "";
                        txtDienThoai4.Text = "";
                        txtDiDong.Enabled = false;
                        txtDiDong2.Enabled = false;
                        txtDienThoai3.Enabled = false;
                        txtDienThoai4.Enabled = false;
                    }
                    else
                    {
                        txtDiDong.Text = objKH.DiDong;
                        txtDiDong2.Text = objKH.DiDong2;
                        txtDienThoai3.Text = objKH.DiDong3;
                        txtDienThoai4.Text = objKH.DiDong4;

                    }
                    break;

                case 2:
                    if (obj.DienThoai == false)
                    {
                        txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                        txtDienThoai3.Text = Common.Right(objKH.DiDong3, 3);
                        txtDienThoai4.Text = Common.Right(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoai3Dau == false)
                    {
                        txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                        txtDienThoai3.Text = Common.Right1(objKH.DiDong3, 3);
                        txtDienThoai4.Text = Common.Right1(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoaiAn == false)
                    {
                        txtDiDong.Text = "";
                        txtDiDong2.Text = "";
                        txtDienThoai3.Text = "";
                        txtDienThoai4.Text = "";

                        txtDiDong.Enabled = false;
                        txtDiDong2.Enabled = false;
                        txtDienThoai3.Enabled = false;
                        txtDienThoai4.Enabled = false;

                    }
                    else
                    {
                        txtDiDong.Text = objKH.DiDong;
                        txtDiDong2.Text = objKH.DiDong2;
                        txtDienThoai3.Text = objKH.DiDong3;
                        txtDienThoai4.Text = objKH.DiDong4;

                    }

                    break;
                case 3:
                    if (obj.DienThoai == false)
                    {
                        txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                        txtDienThoai3.Text = Common.Right(objKH.DiDong3, 3);
                        txtDienThoai4.Text = Common.Right(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoai3Dau == false)
                    {
                        txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                        txtDienThoai3.Text = Common.Right1(objKH.DiDong3, 3);
                        txtDienThoai4.Text = Common.Right1(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoaiAn == false)
                    {
                        txtDiDong.Text = "";
                        txtDiDong2.Text = "";
                        txtDienThoai3.Text = "";
                        txtDienThoai4.Text = "";
                        txtDiDong.Enabled = false;
                        txtDiDong2.Enabled = false;
                        txtDienThoai3.Enabled = false;
                        txtDienThoai4.Enabled = false;
                    }
                    else
                    {
                        txtDiDong.Text = objKH.DiDong;
                        txtDiDong2.Text = objKH.DiDong2;
                        txtDienThoai3.Text = objKH.DiDong3;
                        txtDienThoai4.Text = objKH.DiDong4;

                    }

                    break;
                case 4:
                    if (obj.DienThoai == false)
                    {
                        txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                        txtDienThoai3.Text = Common.Right(objKH.DiDong3, 3);
                        txtDienThoai4.Text = Common.Right(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoai3Dau == false)
                    {
                        txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                        txtDienThoai3.Text = Common.Right1(objKH.DiDong3, 3);
                        txtDienThoai4.Text = Common.Right1(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoaiAn == false)
                    {
                        txtDiDong.Text = "";
                        txtDiDong2.Text = "";
                        txtDienThoai3.Text = "";
                        txtDienThoai4.Text = "";
                        txtDiDong.Enabled = false;
                        txtDiDong2.Enabled = false;
                        txtDienThoai3.Enabled = false;
                        txtDienThoai4.Enabled = false;
                    }
                    else
                    {
                        txtDiDong.Text = objKH.DiDong;
                        txtDiDong2.Text = objKH.DiDong2;
                        txtDienThoai3.Text = objKH.DiDong3;
                        txtDienThoai4.Text = objKH.DiDong4;

                    }
                    break;

            }
            try
            {
                btnDCLH.Tag = objKH.Xa.MaXa;
            }
            catch { }


            try
            {
                btnDCLH.Tag = objKH.MaXa;
            }
            catch { }

            try
            {

                btnDCTT.Tag = objKH.Xa.MaXa;
            }
            catch { }

            using (var dbs = new MasterDataContext())
            {
                var dcll = dbs.getDCLL(objKH.MaKH);


                var dctt = dbs.getDCTT(objKH.MaKH);

            }
            MaTinh = objKH.MaTinh;
            MaTinh2 = objKH.MaTinh2;
            MaHuyen = objKH.MaHuyen;
            MaHuyen2 = objKH.MaHuyen2;
            using (var dbs = new MasterDataContext())
            {
                var dcll = dbs.getDCLL(objKH.MaKH);
                try
                {
                    btnDCLH.Text = dcll.Replace(txtDiaChi.Text, "");
                }
                catch { btnDCLH.Text = dcll; }

                var dctt = dbs.getDCTT(objKH.MaKH);
                try
                {
                    btnDCTT.Text = dctt.Replace(txtThuongTru.Text, "");
                }
                catch { btnDCTT.Text = dctt; }
            }
        }
        private void btnDCTT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }
        private void btnDCTT2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BEE.NghiepVuKhac.SelectPosition_frm frm = new BEE.NghiepVuKhac.SelectPosition_frm();
            frm.ShowDialog();
            if (frm.Result != "")
            {
                // btnDCTT2.Tag = frm.MaXa;
                //  btnDCTT2.Text = frm.Result;
                MaHuyen = frm.MaHuyen;
                MaTinh = frm.MaTinh;
            }
        }
        private void btnSaveKH_Click(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            #region Rang buoc
            //if (txtTenVietTat.Text.Trim() == "")
            //{
            //    DialogBox.Error("Vui lòng nhập mã hiệu");
            //    txtTenVietTat.Focus();
            //    return;
            //}
            //if (lookCode.Text.Trim() == "")
            //{
            //    DialogBox.Error("Vui lòng chọn mã");
            //    lookCode.Focus();
            //    return;
            //}

            if (lookUpNhomKH.EditValue == null)
            {
                DialogBox.Infomation("Vui lòng chọn [Nhóm khách hàng], xin cảm ơn!");
                lookUpNhomKH.Focus();
                return;
            }

            var diDong = txtDiDong.Text.Trim();
            if (diDong != "")
            {
                var count = db.KhachHangs.Where(p => p.DiDong == diDong & p.MaKH != objKH.MaKH).Count();
                if (count > 0)
                {
                    DialogBox.Error("Trùng số di động. Vui lòng kiểm tra lại!");
                    txtDiDong.Focus();
                    return;
                }
            }
            #endregion
            try
            {
                var objkh1 = db.KhachHangs.Single(p => p.MaKH == this.MaKH);
                //objKH.GioiTinh = (int?)rdGTinh.EditValue;
                //objKH.KyHieu = db.DinhDang(13, (db.KhachHangs.Max(p => (int?)p.MaKH) ?? 0) + 1);
                objkh1.HoKH = txtHoKH.Text;
                objkh1.TenKH = txtTenKH.Text;

                objkh1.MaQD = (byte?)lookQuyDanh2.EditValue;
                // objKH.DiaChiCT = txtDiaChiCT1.Text;
                objkh1.IsPersonal = true;

                objkh1.DienThoaiCT = txtDienThoaiCT.Text;
                objkh1.Email = txtEmail.Text;
                objkh1.MaSoThueCT = txtMaSoThueCT.Text;

                objkh1.GhiChu = txtGhiChuKH.Text;

                objkh1.MaNKH = (byte?)lookUpNhomKH.EditValue;


                if (objkh1.DiDong == null | objkh1.DiDong == "")
                {
                    objkh1.DiDong = txtDiDong.Text;
                }
                if (objkh1.DiDong2 == null | objkh1.DiDong2 == "")
                {
                    objkh1.DiDong2 = txtDiDong2.Text;
                }
                if (objkh1.DiDong3 == null | objkh1.DiDong3 == "")
                {
                    objkh1.DiDong3 = txtDienThoai3.Text;
                }
                if (objkh1.DiDong4 == null | objkh1.DiDong4 == "")
                {
                    objkh1.DiDong4 = txtDienThoai4.Text;
                }

                if (lookNguonDen1.ItemIndex != -1)
                {
                    objkh1.HowToKnowID = (short?)lookNguonDen1.EditValue;
                }
                else
                {
                    DialogBox.Error("Bạn chưa chọn nguồn đến!");
                    return;
                }


                objkh1.DiaChi = txtDiaChi.Text;
                if (btnDCTT.Text != "")
                    objkh1.Xa = db.Xas.First(p => p.MaXa == int.Parse(btnDCTT.Tag.ToString()));
                if (btnDCLH.Text != "")
                    objkh1.Xa1 = db.Xas.First(p => p.MaXa == int.Parse(btnDCTT.Tag.ToString()));

                objkh1.MaHuyen = (short?)MaHuyen;
                objkh1.MaHuyen2 = (short?)MaHuyen2;
                objkh1.MaTinh = MaTinh;
                objkh1.MaTinh2 = MaTinh2;



                if (objKH.MaKH == 0)
                {
                    //  var a = (db.KhachHangs.Max(p => (int?)p.MaKH) ?? 0) + 1;
                    //  objKH.mak = db.DinhDang(13, a);

                    objkh1.MaNV = Common.StaffID;
                    objkh1.NgayDangKy = db.getDate();
                    //objKH.IsVisible = true;
                    db.KhachHangs.InsertOnSubmit(objkh1);
                }
                db.SubmitChanges();
                this.MaKH = objKH.MaKH;
                MessageBox.Show("Xử lý thành công!");
                btnSaveHis.Enabled = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                db.Dispose();
            }



        }

        private void btnSaveHis_Click(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            if (txtTieuDe.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tiêu đề. Xin cảm ơn");
                txtTieuDe.Focus();
                return;
            }

            if (txtNoiDung.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập nội dung. Xin cảm ơn");
                txtNoiDung.Focus();
                return;
            }

            if (lookPhuongThuc.EditValue == null)
            {
                DialogBox.Infomation("Vui lòng chọn phương thức xử lý. Xin cảm ơn");
                lookPhuongThuc.Focus();
                return;
            }

            if (lookStatusCall.EditValue == null)
            {
                DialogBox.Infomation("Vui lòng chọn trạng thái gọi. Xin cảm ơn");
                lookStatusCall.Focus();
                return;
            }

            var objBC = db.mglbcBanChoThues.SingleOrDefault(p => p.MaBC == this.MaBC);

            var objNhatKy = new mglbcNhatKyXuLy();

            if (this.IdHistory > 0)
            {
                objNhatKy = db.mglbcNhatKyXuLies.FirstOrDefault(p => p.ID == this.IdHistory);
            }
            else
            {
                db.mglbcNhatKyXuLies.InsertOnSubmit(objNhatKy);
            }

            objNhatKy.MaBC = this.MaBC;
            objNhatKy.NgayXL = db.getDate();
            objNhatKy.TieuDe = txtTieuDe.Text;
            objNhatKy.NoiDung = txtNoiDung.Text;
            objNhatKy.MaPT = (byte?)lookPhuongThuc.EditValue;
            objNhatKy.MaNVG = Common.StaffID;
            objNhatKy.MaTT = (byte?)lookTrangThai.EditValue;
            objNhatKy.MaNVN = objBC.MaNVKD;
            objNhatKy.MaTTBefore = objBC.MaTT;
            objNhatKy.StartDate = this.StartDate;
            objNhatKy.Enddate = db.getDate();
            objNhatKy.MaTTGoi = (byte?)lookStatusCall.EditValue;
            objNhatKy.LoaiCG = this.LoaiCG;
            objNhatKy.DiDong = this.SDT;

            // update trạng thái bds
            objBC.MaTT = (byte?)lookTrangThai.EditValue;
            try
            {
                #region kafka
                // push data kafka
                var objrealestate = new mglbcBanChoThueModel();
                // khách hàng
                var objkhadd = new List<BEE.HoatDong.MGL.Ban.Models.MessageQueue.KhachHang>();
                var objitemkh = new BEE.HoatDong.MGL.Ban.Models.MessageQueue.KhachHang();
                objitemkh.HoTen = objBC.KhachHang.HoKH + " " + objBC.KhachHang.TenKH;
                objitemkh.DiDong = objBC.KhachHang.DiDong;
                objitemkh.MaKH = objBC.KhachHang.MaKH;
                objitemkh.DiDong2 = objBC.KhachHang.DiDong2;
                objitemkh.DiDong3 = objBC.KhachHang.DiDong3;
                objitemkh.DiDong4 = objBC.KhachHang.DiDong4;
                try
                {
                    objitemkh.AppMaKH = objBC.KhachHang.AppMaKH.ToString();  // thiếu trường
                }
                catch
                {

                }

                objkhadd.Add(objitemkh);
                objrealestate.KhachHang = objkhadd;
                // 
                objrealestate.MaBC = objBC.MaBC;
                objrealestate.MaTT = objBC.MaTT;
                objrealestate.IsBan = objBC.IsBan;
                // ảnh bds
                var lstimg = new List<mglbcAnhbdsModel>();
                foreach (var i in objBC.mglbcAnhbds)
                {
                    var objimgadd = new BEE.HoatDong.MGL.Ban.Models.MessageQueue.mglbcAnhbdsModel();
                    objimgadd.DuongDan = i.DuongDan;
                    if (i.IsS3 == true)
                        objimgadd.isS3 = 1;
                    else
                        objimgadd.isS3 = 0;
                    objimgadd.Position = i.Position;
                    objimgadd.Status = i.Status;
                    lstimg.Add(objimgadd);

                }
                objrealestate.AnhBDS = lstimg;

                var lstvideo = new List<mglbcVideobdsModel>();
                foreach (var i in db.mglbcVideobds.Where(p => p.MaBC == objBC.MaBC).ToList())
                {
                    var objvideo = new BEE.HoatDong.MGL.Ban.Models.MessageQueue.mglbcVideobdsModel();
                    objvideo.DuongDan = i.DuongDan;

                    if (i.IsS3 == true)
                        objvideo.isS3 = 1;
                    else
                        objvideo.isS3 = 0;
                    objvideo.ViTri = null;
                    objvideo.Position = i.Position;
                    objvideo.Status = i.Status;
                    lstvideo.Add(objvideo);

                }
                objrealestate.VideoBDS = lstvideo;

                objrealestate.ViTri = objBC.ToaDo;
                objrealestate.SoNha = objBC.SoNha;
                try
                {
                    objrealestate.TenDuong = objBC.Duong.TenDuong;
                }
                catch { }
                objrealestate.MaDuong = objBC.MaDuong;
                objrealestate.MaXa = objBC.MaXa;
                objrealestate.MaHuyen = objBC.MaHuyen;
                objrealestate.MaTinh = objBC.MaTinh;
                objrealestate.MaLbds = objBC.MaLBDS;
                objrealestate.ThanhTien = objBC.ThanhTien;
                objrealestate.DienTich = objBC.DienTich;
                objrealestate.SoTang = objBC.SoTang;
                objrealestate.NgangXD = objBC.NgangXD;
                objrealestate.MaHuong = objBC.MaHuong;
                objrealestate.DuongRong = objBC.DuongRong;
                objrealestate.MaTTNT = objBC.MaTTNT;
                objrealestate.PhongNgu = objBC.PhongNgu;
                objrealestate.PhongVs = objBC.PhongVS;
                objrealestate.SoTangXD = objBC.SoTangXD;
                objrealestate.PhongBep = objBC.PhongBep;
                objrealestate.PhongAn = objBC.PhongAn;
                objrealestate.PhongKhach = objBC.PhongKhach;
                objrealestate.DieuHoa = objBC.DieuHoa;
                objrealestate.NongLanh = objBC.NongLanh;
                objrealestate.IsThangMay = objBC.IsThangMay;
                objrealestate.TangHam = objBC.TangHam;
                objrealestate.MaSan = objBC.MaSan;
                objrealestate.CuaSo = objBC.CuaSo;
                objrealestate.isBanCong = objBC.isBanCong;
                objrealestate.isSan = objBC.isSan;
                objrealestate.isVuon = objBC.isVuon;
                objrealestate.MaDX = objBC.MaDX;
                objrealestate.KhoangCachDX = objBC.KhoangCachDX;
                objrealestate.DienTichDat = objBC.DienTichDat;
                objrealestate.DienTichXd = objBC.DienTichXD;
                objrealestate.DaiXD = objBC.DaiXD;
                objrealestate.SauXD = objBC.SauXD;
                objrealestate.SauKV = objBC.SauKV;
                objrealestate.NgangKV = objBC.NgangKV;
                objrealestate.MaLd = objBC.MaLD;
                try
                {
                    objrealestate.TenLD = db.LoaiDuongs.FirstOrDefault(p => p.MaLD == objBC.MaLD).TenLD;
                }
                catch
                {

                }
                objrealestate.PhapLyBDS = null;
                objrealestate.GhiChu = objBC.GhiChu;
                objrealestate.NamXayDung = objBC.NamXayDung;
                objrealestate.DaiKV = objBC.DaiKV; // sau thong thuy




                try
                {
                    objrealestate.AppMaDT = objBC.MaNVKD.ToString();
                }
                catch
                {
                }

                objrealestate.MaBC = objBC.MaBC;
                objrealestate.syncId = objBC.syncId;

                try
                {
                    if (this.MaBC == 0 || this.MaBC == null)
                    {
                        objrealestate.AppMaBC = null;

                    }
                    else
                    {
                        objrealestate.AppMaBC = objBC.AppMaBC.ToString();

                    }
                }
                catch
                {
                }

                objrealestate.isUpdate = true;
                var client = new RestClient("http://api-gw.hoaland.com.vn:8085/api/RealEstate/sync-queue-realestate");
                //var client = new RestClient("http://192.168.1.5:8085/api/RealEstate/sync-queue-realestate");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                var body = JsonConvert.SerializeObject(objrealestate);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                var log = new QueueHistory();
                log.CreateDate = DateTime.Now;
                log.Messger = body;
                log.Response = response.Content;
                db.QueueHistories.InsertOnSubmit(log);
                db.SubmitChanges();
                #endregion
            }
            catch 
            {

                DialogBox.Error("Lỗi đồng bộ kafka, vui lòng thử lại");
                return;
            }
            

            db.SubmitChanges();
            this.IdHistory = objNhatKy.ID;

            if (objNhatKy.FileRecord == null || objNhatKy.FileRecord == "")
            {
                Thread tid1 = new Thread(new ThreadStart(AsyncFIle));
                tid1.Start();

            }

            btnSaveHis.Enabled = false;
            itemEditHistory.Enabled = true;

            this.IsSave = true;
            DialogBox.Infomation("Xử lý thành công!");
            db.Dispose();

        }
        public void AsyncFIle()
        {
            VOIPSETUP.Utils.Common.GetFile(this.IdHistory);

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //var id = (Int64?)gridView1.GetFocusedRowCellValue("ID");
            //txtNoiDung.Text = db.khCall_Histories.FirstOrDefault(p => p.ID == id).GhiChu;
        }

        private void btnTinhHuyenXa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //using (var frm = new BEE.THUVIEN.Other.frmTinhHuyenXa())
            //{
            //    frm.MaTinh = objKH.MaTinh;
            //    frm.MaHuyen = objKH.MaHuyen;
            //    frm.MaXa = objKH.MaXa;
            //    frm.ShowDialog();
            //    if (frm.DialogResult == DialogResult.OK)
            //    {
            //        btnTinhHuyenXa.Text = frm.Result;
            //        objKH.MaXa = frm.MaXa;
            //        objKH.MaHuyen = frm.MaHuyen;
            //        objKH.MaTinh = frm.MaTinh;
            //    }
            //}
        }
        private void lookUpNhomKH_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                BEE.KhachHang.NhomKH_frm frm = new BEE.KhachHang.NhomKH_frm();
                frm.ShowDialog();
                if (frm.IsUpdate)
                {
                    it.NhomKHCls o = new it.NhomKHCls();
                    lookUpNhomKH.Properties.DataSource = o.Select();

                }
            }
        }
        private void btnLapLichHen_Click(object sender, EventArgs e)
        {
            try
            {

                if (MaKH > 0)
                {
                    var frm = new BEEREMA.CongViec.LichHen.AddNew_frm(null, null, MaKH, null, null, null);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Vui lòng lưu khách hàng trước khi lập lịch hẹn, xin cảm ơn!");
                }

            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnTinhHuyenXa_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //using (var frm = new BEE.THUVIEN.Other.frmTinhHuyenXa())
            //{
            //    frm.MaTinh = objKH.MaTinh;
            //    frm.MaHuyen = objKH.MaHuyen;
            //    frm.MaXa = objKH.MaXa;
            //    frm.ShowDialog();
            //    if (frm.DialogResult == DialogResult.OK)
            //    {
            //        btnTinhHuyenXa.Text = frm.Result;
            //        objKH.MaXa = frm.MaXa;
            //        objKH.MaHuyen = frm.MaHuyen;
            //        objKH.MaTinh = frm.MaTinh;
            //    }
            //}
        }

        private void btnCoHoiKD_Click(object sender, EventArgs e)
        {

            //if (this.MaKH == null)
            //{
            //    DialogBox.Error("Vui lòng lưu thông tin khách hàng trước khi chuyển cơ hội");
            //    return;
            //}


            //using (var frmnc = new BEE.NHUCAU.frmEdit())
            //{
            //    frmnc.MaKH = this.MaKH;
            //    frmnc.ShowDialog();
            //}

            //if (rows.Length <= 0)
            //{
            //    DialogBox.Error("Vui lòng chọn khách hang");
            //    return;
            //}

            //var frm = new BEE.KHACHHANG.frmProcess();
            //frm.MaKH = this.MaKH;
            //frm.ShowDialog();

            //if (frm.DialogResult != DialogResult.OK) return;

            //var db = new MasterDataContext();
            //var wait = DialogBox.WaitingForm();
            //try
            //{
            //    string msgError = "";

            //    var objNC = new ncNhuCau();
            //    objNC.NgayCN = frm.objNK.NgayXL;
            //  //  objNC.MaTT = frm.MaTT;
            //    var objTT = db.ncTrangThais.FirstOrDefault(p => p.MaTT == objNC.MaTT);
            //    objNC.TiemNang = objTT.TiemNang;
            //    objNC.MaKH = this.MaKH;
            //    objNC.MaNVQL = frm.objNK.MaNVQL ?? Common.MaNV;
            //    objNC.DienGiai = frm.objNK.DienGiai;
            //    objNC.SoNC = db.ncNhuCau_TaoSoNC();
            //    objNC.MaNVN = Common.MaNV;
            //    objNC.NgayNhap = db.getDate();
            //    db.ncNhuCaus.InsertOnSubmit(objNC);

            //    db.SubmitChanges();

            //    using (var frmnc = new BEE.NHUCAU.frmEdit())
            //    {
            //        frmnc.MaKH = this.MaKH;
            //        frmnc.ShowDialog();
            //    }

            //    if (msgError != "")
            //    {
            //        DialogBox.Infomation("Đã xử lý xong, một số khách hàng đã được xử lý trước đó: " + msgError);
            //    }
            //    else
            //    {
            //        DialogBox.Infomation("Đã xử lý thành công");
            //    }



            //}
            //catch (Exception ex)
            //{
            //    DialogBox.Error(ex.Message);
            //}
            //finally
            //{
            //    db.Dispose();
            //    wait.Close();
            //}
        }

        private void btnTinhHuyenXa_ButtonClick_2(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //using (var frm = new BEE.THUVIEN.Other.frmTinhHuyenXa())
            //{
            //    frm.MaTinh = objKH.MaTinh;
            //    frm.MaHuyen = objKH.MaHuyen;
            //    frm.MaXa = objKH.MaXa;
            //    frm.ShowDialog();
            //    if (frm.DialogResult == DialogResult.OK)
            //    {
            //        btnTinhHuyenXa.Text = frm.Result;
            //        objKH.MaXa = frm.MaXa;
            //        objKH.MaHuyen = frm.MaHuyen;
            //        objKH.MaTinh = frm.MaTinh;
            //    }
            //}
        }

        private void itemChiTietGiaoDich_Click(object sender, EventArgs e)
        {
            BEE.HoatDong.MGL.Ban.frmEdit frm = new BEE.HoatDong.MGL.Ban.frmEdit();
            frm.MaBC = this.MaBC;
            frm.ShowDialog();
            //    this.Close();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            var db = new MasterDataContext();
            if (this.MaBC == null)
            {
                gcMuaThue.DataSource = null;
                gcNhatKy.DataSource = null;

                return;
            }
            switch (xtraTabControl1.SelectedTabPageIndex)
            {
                case 3:
                    var listNhatKy = (from p in db.mglbcNhatKyXuLies
                                      join nv in db.NhanViens on p.MaNVG equals nv.MaNV into nhanvien
                                      from nv in nhanvien.DefaultIfEmpty()
                                      join nv1 in db.NhanViens on p.MaNVN equals nv1.MaNV into nhanvien1
                                      from nv1 in nhanvien1.DefaultIfEmpty()
                                      join pt in db.PhuongThucXuLies on p.MaPT equals pt.MaPT into phuongthuc
                                      from pt in phuongthuc.DefaultIfEmpty()
                                      where p.MaBC == this.MaBC
                                      orderby p.NgayXL descending
                                      select new
                                      {
                                          p.ID,
                                          p.NgayXL,
                                          p.TieuDe,
                                          p.NoiDung,
                                          //p.PhuongThucXuLy.TenPT,
                                          p.MaNVG,
                                          HoTenNVG = nv.HoTen,
                                          HoTenNVN = nv1.HoTen,
                                          p.KetQua,
                                          pt.TenPT,
                                          p.MaTT
                                      }).ToList();
                    gcNhatKy.DataSource = listNhatKy;
                    break;


            }

        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SelectPosition_frm frm = new SelectPosition_frm();
            frm.MaXa = objNDD.MaXa.GetValueOrDefault();
            frm.ShowDialog();
            if (frm.MaXa != 0)
            {
                objNDD.Xa = db.Xas.Single(p => p.MaXa == frm.MaXa);
                btnDCTT.Text = frm.Result;
            }
        }

        private void buttonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            NghiepVuKhac.SelectPosition_frm frm = new NghiepVuKhac.SelectPosition_frm();
            frm.MaXa = objNDD.MaXa2.GetValueOrDefault();
            frm.ShowDialog();
            if (frm.MaXa != 0)
            {
                objNDD.Xa1 = db.Xas.Single(p => p.MaXa == frm.MaXa);
                btnDCLH.Text = frm.Result;
            }
        }

        private void itemSuaKH_Click(object sender, EventArgs e)
        {
            if (objKH == null)
            {
                DialogBox.Error("Không tồn tại khách hàng, vui lòng lưu thông tin Khách trước khi sửa.");
                return;
            }
            BEE.KhachHang.KhachHang_frm frm = new BEE.KhachHang.KhachHang_frm();
            frm.MaKH = objKH.MaKH;
            frm.IsPersonal = true;
            frm.ShowDialog();
        }

        private void frmCall_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsSave == null || IsSave == false)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    DialogResult result = MessageBox.Show("Bạn có muốn lưu không?", "Thông báo", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        var obj = new LichSuDongFormInCall();
                        obj.MaNV = Common.StaffID;
                        obj.NgayDong = db.getDate();
                        obj.MaKH = this.MaKH;
                        obj.DiDong = this.SDT;
                        db.LichSuDongFormInCalls.InsertOnSubmit(obj);
                        db.SubmitChanges();
                        return;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                {
                    e.Cancel = true;
                }
            }
            else
            {
                return;
            }
        }





        private void lookAddress_EditValueChanged(object sender, EventArgs e)
        {
            var mabc = lookAddress.EditValue;
            if (mabc != null)
            {
                LoadNhatKy((int)mabc);
            }
        }
        private void LoadNhatKy(int mabc)
        {
            var listNhatKy = (from p in db.mglbcNhatKyXuLies
                              join nv in db.NhanViens on p.MaNVG equals nv.MaNV into nhanvien
                              from nv in nhanvien.DefaultIfEmpty()
                              join nv1 in db.NhanViens on p.MaNVN equals nv1.MaNV into nhanvien1
                              from nv1 in nhanvien1.DefaultIfEmpty()
                              join pt in db.PhuongThucXuLies on p.MaPT equals pt.MaPT into phuongthuc
                              from pt in phuongthuc.DefaultIfEmpty()
                              where p.MaBC == mabc
                              orderby p.NgayXL descending
                              select new
                              {
                                  p.ID,
                                  p.NgayXL,
                                  p.TieuDe,
                                  p.NoiDung,
                                  p.MaNVG,
                                  HoTenNVG = nv.HoTen,
                                  HoTenNVN = nv1.HoTen,
                                  p.KetQua,
                                  pt.TenPT,
                                  p.MaTT
                              }).ToList();

            gcNhatKy.DataSource = listNhatKy;

            LoadMaBC(mabc);
        }

        private void picImg_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (this.MaBC == 0 || this.MaBC == null)
            //    {
            //        DialogBox.Error("Vui lòng chọn bất động sản");
            //        return;
            //    }
            //    var frm = new PhotoViewer.Form2();
            //    frm.MaBC = this.MaBC;
            //    frm.ShowDialog();
            //    frm.Dispose();
            //    Thread thrd = new Thread(Delete);
            //    thrd.IsBackground = true;
            //    thrd.Start();
            //}
            //catch (Exception ex)
            //{
            //    DialogBox.Error(ex.Message);

            //}


            try
            {
                var frm = new PhotoViewer.Form2();
                frm.MaBC = this.MaBC;
                frm.ShowDialog();
                frm.Dispose();
                Thread thrd = new Thread(Delete);
                thrd.IsBackground = true;
                thrd.Start();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);

            }

        }
        public static void Delete()
        {


            System.IO.DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "\\Images");
            foreach (FileInfo file in di.GetFiles())
            {
                try
                {

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    file.Delete();
                }
                catch (Exception ex)
                {

                }


            }

        }
        private void picVideo_Click(object sender, EventArgs e)
        {

            var t = new Thread(Video);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }
        public void Video()
        {
            using (var frm = new HoatDong.MGL.Ban.frmVideo())
            {
                frm.MaBC = this.MaBC;
                frm.ShowDialog();
            }
        }


        private void itemCreateNew_Click(object sender, EventArgs e)
        {
            using (var frm = new HoatDong.MGL.Ban.frmEdit())
            {
                frm.MaKH = this.MaKH;
                frm.mucdich = false;
                frm.ShowDialog();
            }
        }

        private void LoadMaBC(int mabc)
        {
            this.MaBC = mabc;
            if (mabc > 0)
            {
                var objBC = db.mglbcBanChoThues.SingleOrDefault(p => p.MaBC == this.MaBC);
                txtSoDangKy.Text = objBC.SoDK;
                lkLoaiBDS.EditValue = objBC.MaLBDS;
                lookTrangThai.EditValue = lookBefore.EditValue = objBC.MaTT;
                lookAddress.EditValue = this.MaBC;

                picImg.Image = BEE.VOIPSETUP.Properties.Resources._58_image;
                picImg.Enabled = db.mglbcAnhbds.FirstOrDefault(p => p.MaBC == this.MaBC) == null ? false : true;

                picVideo.Image = BEE.VOIPSETUP.Properties.Resources.youtube;
                picVideo.Enabled = db.mglbcVideobds.FirstOrDefault(p => p.MaBC == this.MaBC) == null ? false : true;
            }
        }

        private void itemEditHistory_Click(object sender, EventArgs e)
        {
            btnSaveHis.Enabled = true;
            itemEditHistory.Enabled = false;
        }
    }
}
