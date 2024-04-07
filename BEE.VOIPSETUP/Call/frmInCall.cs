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
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.IO;

namespace BEE.VOIPSETUP.Call
{
    public partial class frmInCall : DevExpress.XtraEditors.XtraForm
    {
        public int? MaNC { get; set; }
        public int? ID { get; set; }
        public long? IdkhCall { get; set; }
        public int? IdNKMT { get; set; }
        public bool? IsSave { get; set; }
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
        public int type { get; set; }

        public DateTime StartDate { get; set; }



        public frmInCall()
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


        private void Ban_Load(int makh)
        {

            //int MaNV = Common.StaffID;
            //var PerID = Common.PerID;

            //try
            //{

            //    switch (GetAccessDataBan())
            //    {
            //        case 1://Tat ca
            //            gcBan.DataSource = db.mglbcBanChoThueByKH_VOIP(true, -1, -1, -1, -1, MaNV, PerID,makh).ToList();

            //            break;
            //        case 2://Theo phong ban 
            //            gcBan.DataSource = db.mglbcBanChoThueByKH_VOIP(true, -1, -1, -1, Common.DepartmentID, MaNV, PerID,makh).ToList();//.Where(p => p.IsBan.GetValueOrDefault() == true);
            //            break;
            //        case 3://Theo nhom
            //            gcBan.DataSource = db.mglbcBanChoThueByKH_VOIP(true, -1, -1, Common.GroupID, -1, MaNV, PerID,makh);//.Where(p => p.IsBan.GetValueOrDefault() == true);
            //            break;
            //        case 4://Theo nhan vienbreak;
            //            gcBan.DataSource = db.mglbcBanChoThueByKH_VOIP(true, MaNV, MaNV, -1, -1, MaNV, PerID,makh);//.Where(p => p.IsBan.GetValueOrDefault() == true);
            //            break;
            //        default:
            //            gcBan.DataSource = null;
            //            break;
            //    }
            //    try
            //    {
            //        for (int i = 0; i <= grvBan.RowCount; i++)
            //        {
            //            var objPQ = db.spPhanQuyens.Single(p => p.PerID == Common.PerID & (p.isMua == false | p.isMua == null));
            //            var s = (int?)grvBan.GetRowCellValue(i, "MaNVQL");
            //            if (Common.StaffID != s)
            //            {
            //                if (objPQ.DiaChi != true)
            //                {
            //                    grvBan.SetRowCellValue(i, "DiaChi", null);
            //                    // colDiChi.Visible = true;
            //                }
            //                //else
            //                //{
            //                //    colDiChi.Visible = false;
            //                //}
            //                if (objPQ.DienThoai1 != true)
            //                {
            //                    grvBan.SetRowCellValue(i, "DienThoai", null);
            //                }

            //                if (objPQ.SoNha != true)
            //                {
            //                    grvBan.SetRowCellValue(i, "SoNha", null);
            //                }
            //            }
            //        }
            //    }
            //    catch
            //    {
            //    }
            //    //for (int i = 0; i < grvBan.RowCount; i++)
            //    //{
            //    //    string phiMG = grvBan.GetRowCellValue(i, "TyLeMG") != null ?
            //    //        string.Format("{0:#,0.##}%", grvBan.GetRowCellValue(i, "TyLeMG")) :
            //    //        string.Format("{0:#,0.##}", grvBan.GetRowCellValue(i, "PhiMG"));
            //    //    grvBan.SetRowCellValue(i, "PhiMoiGioi", phiMG);
            //    //}
            //}
            //catch { }
            //finally
            //{

            //}
        }

        void MuaThue_Load(int? makh)
        {

            //int MaNV = Common.StaffID;
            //try
            //{

            //    switch (GetAccessDataCanMua())
            //    {
            //        case 1://Tat ca
            //            gcMuaThue.DataSource = db.mglmtMuaThueByKH_VOIP( true, -1, -1, -1, -1, MaNV, Common.PerID,makh);//.Where(p => p.IsMua.GetValueOrDefault() == true);
            //            break;
            //        case 2://Theo phong ban 
            //            gcMuaThue.DataSource = db.mglmtMuaThueByKH_VOIP(true, -1, -1, -1, Common.DepartmentID, MaNV, Common.PerID,makh);//.Where(p => p.IsMua.GetValueOrDefault() == true);
            //            break;
            //        case 3://Theo nhom
            //            gcMuaThue.DataSource = db.mglmtMuaThueByKH_VOIP(true, -1, -1, Common.GroupID, -1, MaNV, Common.PerID,makh);//.Where(p => p.IsMua.GetValueOrDefault() == true);
            //            break;
            //        case 4://Theo nhan vienbreak;
            //            gcMuaThue.DataSource = db.mglmtMuaThueByKH_VOIP(true, MaNV, MaNV, -1, -1, MaNV, Common.PerID,makh);//.Where(p => p.IsMua.GetValueOrDefault() == true);
            //            break;
            //        default:
            //            gcMuaThue.DataSource = null;
            //            break;
            //    }
            //    for (int i = 0; i <= grvMuaThue.RowCount; i++)
            //    {
            //        var objPQ = db.spPhanQuyens.Single(p => p.PerID == Common.PerID & p.isMua == true);
            //        var s = (int?)grvMuaThue.GetRowCellValue(i, "MaNVKT");
            //        if (Common.StaffID != s)
            //        {
            //            if (objPQ.DienThoai1 != true)
            //            {
            //                grvMuaThue.SetRowCellValue(i, "DienThoai", null);
            //            }
            //        }
            //    }
            //    //for (int i = 0; i < grvMuaThue.RowCount; i++)
            //    //{
            //    //    string khoangGia = string.Format("{0:#,0.##} -> {1:#,0.##} {2}", grvMuaThue.GetRowCellValue(i, "GiaTu"),
            //    //        grvMuaThue.GetRowCellValue(i, "GiaDen"), grvMuaThue.GetRowCellValue(i, "TenLoaiTien"));
            //    //    grvMuaThue.SetRowCellValue(i, "KhoangGia", khoangGia);
            //    //    string dienTich = string.Format("{0:#,0.##} -> {1:#,0.##}",
            //    //        grvMuaThue.GetRowCellValue(i, "DienTichTu"), grvMuaThue.GetRowCellValue(i, "DienTichDen"));
            //    //    grvMuaThue.SetRowCellValue(i, "DienTich", dienTich);
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    DialogBox.Error(ex.Message);
            //}
            //finally
            //{

            //}

        }

        private void frmCall_Load(object sender, EventArgs e)
        {

            db = new MasterDataContext();
            itemThemNhuCau.Visible = false;

            // timeChangeColor.Start();
            // 
            dateNgayGhiNhan.EditValue = db.getDate();
            lookPhuongThuc.Properties.DataSource = db.PhuongThucXuLies;
            lookTinh.Properties.DataSource = db.Tinhs;
            lookHuyen.Properties.DataSource = db.Huyens;
            lookXa.Properties.DataSource = db.Xas;
            lkLoaiBDS.Properties.DataSource = db.LoaiBDs;
            lookTrangThai.Properties.DataSource = lkTrangThai.Properties.DataSource = lookBefore.Properties.DataSource = db.mglbcTrangThais;
            lookNCD.DataSource = lookNgCDTuVan.Properties.DataSource = db.NhanViens.Select(p => new { p.HoTen, p.MaNV });
            lookNhuCau.Properties.DataSource = db.NhuCaus;
            lookUpQuyDanh.Properties.DataSource = db.QuyDanhs;
            lookUpNhomKH.Properties.DataSource = db.NhomKHs;
            lookNguonDen1.Properties.DataSource = db.mglNguons.Select(p => new { HowToKnowID = p.MaNguon, Name = p.TenNguon });
            lookStatusCall.Properties.DataSource = db.bdsStatusCalls;

            LoadPermissionKH();
            lkNhanVien.DataSource = db.NhanViens.Select(p => new { p.HoTen, p.MaNV });
            if (this.MaBC > 0)
            {
                var objBC = db.mglbcBanChoThues.SingleOrDefault(p => p.MaBC == this.MaBC);
                txtSoDangKy.Text = objBC.SoDK;
                lkLoaiBDS.EditValue = objBC.MaLBDS;
                lookTrangThai.EditValue = objBC.MaTT;
                lookTinh.EditValue = objBC.MaTinh;
                lookHuyen.EditValue = objBC.MaHuyen;
                lookXa.EditValue = objBC.MaXa;
                xtraTabPage1.PageVisible = false;

            }
            itemEditHistory.Enabled = false;
            if (SDT != null || Unique != null)
            {
                var obj = db.KhachHangs.Where(p => p.DiDong == SDT || p.DienThoaiCT == SDT || p.DiDong == SDT || p.DiDong2 == SDT || p.DiDong3 == SDT || p.DiDong4 == SDT).ToList();
                if (obj.Count() > 0)
                {
                    this.objKH = db.KhachHangs.FirstOrDefault((p => p.DiDong == SDT || p.DienThoaiCT == SDT || p.DiDong == SDT || p.DiDong2 == SDT || p.DiDong3 == SDT || p.DiDong4 == SDT));
                    txtUnique.Text = Unique;
                    txtLine.Text = line;
                    txtKhachHang.Text = KhachHang;
                    dateNgayGhiNhan.EditValue = db.getDate();
                    lbNotice.Text = " - KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";
                    lbNotice.BackColor = Color.Green;
                    lbNotice.ForeColor = Color.White;
                    KhachHangLoad(objKH.MaKH, type);
                    if (type == 1)
                    {
                        if (objKH.DiDong == SDT)
                        {
                            txtDiDong.Text = SDT;
                            txtDiDong2.Text = null;
                            txtDienThoai3.Text = null;
                            txtDienThoai4.Text = null;
                        }
                        else if (objKH.DiDong2 == SDT)
                        {
                            txtDiDong.Text = null;
                            txtDiDong2.Text = SDT;
                            txtDienThoai3.Text = null;
                            txtDienThoai4.Text = null;
                        }
                        else if (objKH.DiDong3 == SDT)
                        {
                            txtDiDong.Text = null;
                            txtDiDong2.Text = null;
                            txtDienThoai3.Text = SDT;
                            txtDienThoai4.Text = null;
                        }

                        else if (objKH.DiDong4 == SDT)
                        {
                            txtDiDong.Text = null;
                            txtDiDong2.Text = null;
                            txtDienThoai3.Text = null;
                            txtDienThoai4.Text = SDT;
                        }

                    }
                    else
                    {
                        txtDiDong.Text = null;
                        txtDiDong2.Text = null;
                        txtDienThoai3.Text = null;
                        txtDienThoai4.Text = null;
                    }


                    var objkhbc = db.Check_NhuCauBanChoThueVOIP(objKH.MaKH).ToList();
                    var objmt = db.Check_NhuCauMuaThueVOIP(objKH.MaKH).ToList();
                    lbSLBanChoThue.Text = "(" + objkhbc.Count() + ")";
                    lbSLMuaThue.Text = "(" + objmt.Count() + ")";
                    if (objkhbc.Count() > 0)
                    {
                        lookKHBanChoThue.Properties.DataSource = objkhbc;

                    }
                    if (objmt.Count() > 0)
                    {
                        lookKHMuaThue.Properties.DataSource = objmt;

                    }



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
                    btnSaveHis.Enabled = false;


                }

            }

        }

        public void LoadNhuCau()
        {
            // kiểm tra thông tin nhu cầu kh
            var db = new MasterDataContext();
            var objkhbc = db.Check_NhuCauBanChoThueVOIP(objKH.MaKH).ToList();
            var objmt = db.Check_NhuCauMuaThueVOIP(objKH.MaKH).ToList();
            lbSLBanChoThue.Text = "(" + objkhbc.Count() + ")";
            lbSLMuaThue.Text = "(" + objmt.Count() + ")";
            if (objkhbc.Count() > 0)
            {
                lookKHBanChoThue.Properties.DataSource = objkhbc;

            }
            if (objmt.Count() > 0)
            {
                lookKHMuaThue.Properties.DataSource = objmt;

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
            txtHoKH.EditValue = null;
            txtDiaChiCT1.EditValue = null;
            // txtDienThoaiCT.EditValue = SDT;
            txtEmailCT.EditValue = null;
            txtGhiChu.EditValue = null;
            txtDiDong.EditValue = SDT;
            lookNgCDTuVan.EditValue = Common.StaffID;

        }
        void KhachHangLoad(int? makh, int? type)
        {
            db = new MasterDataContext();
            try
            {
                objKH = db.KhachHangs.FirstOrDefault(p => p.MaKH == makh);
                this.MaKH = makh;
            }
            catch (Exception)
            {

            }
            //
            txtHoKH.Text = objKH.HoKH;
            txtTenKH.Text = objKH.TenKH;
            lookUpQuyDanh.EditValue = objKH.MaQD;
            txtDiaChiCT1.EditValue = objKH.DiaChiCT;
            var xht = (from t in db.Tinhs
                       join h in db.Huyens on objKH.MaHuyen equals h.MaHuyen
                       join x in db.Xas on objKH.MaXa equals x.MaXa
                       where t.MaTinh == objKH.MaTinh
                       select new { x.TenXa, h.TenHuyen, t.TenTinh }).FirstOrDefault();
            if (xht != null)
            {
                btnTinhHuyenXa.Text = (objKH.MaXa != null ? xht.TenXa + ", " : "") +
                    (objKH.MaHuyen != null ? xht.TenHuyen + ", " : "") +
                    (objKH.MaTinh != null ? xht.TenTinh + ", " : "");
            }

            txtDienThoaiCT.EditValue = objKH.DienThoaiCT;
            txtEmail.EditValue = objKH.Email;
            txtDiDong.EditValue = objKH.DiDong;
            txtDiDong2.Text = objKH.DienThoaiCT;
            txtGhiChuKH.Text = objKH.GhiChu;
            txtEmailCT.Text = objKH.Email;
            txtFax.Text = objKH.FaxCT;
            txtEmailCT.Text = objKH.Email;
            txtMaSoThueCT.Text = objKH.MaSoThueCT;
            txtDiDong2.Text = objKH.DiDong2;
            txtDienThoai3.Text = objKH.DiDong3;
            txtDienThoai4.Text = objKH.DiDong4;
            txtThuongTru.Text = objKH.ThuongTru;
            txtDiaChi.Text = objKH.DiaChi;

            lookUpNhomKH.EditValue = objKH.MaNKH;
            lookNguonDen1.EditValue = (short?)objKH.HowToKnowID;

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
            var qd = "";
            if (type == 1)
            {

                try
                {

                    qd = objKH.QuyDanh.TenQD;
                }
                catch (Exception)
                {
                }
                txtTieuDe.Text = "Cuộc gọi đi: " + qd + " " + objKH.HoKH + " " + objKH.TenKH;
            }
            else
            {
                try
                {

                    qd = objKH.QuyDanh.TenQD;
                }
                catch (Exception)
                {

                }
                txtTieuDe.Text = "Cuộc gọi đến: " + qd + " " + objKH.HoKH + " " + objKH.TenKH;
            }

        }

        private void btnDCTT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BEE.NghiepVuKhac.SelectPosition_frm frm = new BEE.NghiepVuKhac.SelectPosition_frm();
            frm.ShowDialog();
            if (frm.Result != "")
            {
                btnDCTT.Tag = frm.MaXa;
                btnDCTT.Text = frm.Result;
                MaHuyen = frm.MaHuyen;
                MaTinh = frm.MaTinh;
            }
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

                //objKH.GioiTinh = (int?)rdGTinh.EditValue;
                //objKH.KyHieu = db.DinhDang(13, (db.KhachHangs.Max(p => (int?)p.MaKH) ?? 0) + 1);
                objKH.HoKH = txtHoKH.Text;
                objKH.TenKH = txtTenKH.Text;
                objKH.MaQD = (byte?)lookUpQuyDanh.EditValue;
                objKH.DiaChiCT = txtDiaChiCT1.Text;
                objKH.IsPersonal = true;

                objKH.DienThoaiCT = txtDienThoaiCT.Text;
                objKH.Email = txtEmail.Text;
                objKH.MaSoThueCT = txtMaSoThueCT.Text;
                objKH.MaTT = 2;


                objKH.GhiChu = txtGhiChuKH.Text;
                if (objKH.DiDong == null | objKH.DiDong == "")
                {
                    objKH.DiDong = txtDiDong.Text;
                }
                if (objKH.DiDong2 == null | objKH.DiDong2 == "")
                {
                    objKH.DiDong2 = txtDiDong2.Text;
                }
                if (objKH.DiDong3 == null | objKH.DiDong3 == "")
                {
                    objKH.DiDong3 = txtDienThoai3.Text;
                }
                if (objKH.DiDong4 == null | objKH.DiDong4 == "")
                {
                    objKH.DiDong4 = txtDienThoai4.Text;
                }




                objKH.ThuongTru = txtThuongTru.Text;

                objKH.DiaChi = txtDiaChi.Text;
                if (btnDCTT.Text != "")
                    objKH.Xa = db.Xas.First(p => p.MaXa == int.Parse(btnDCTT.Tag.ToString()));
                if (btnDCLH.Text != "")
                    objKH.Xa1 = db.Xas.First(p => p.MaXa == int.Parse(btnDCTT.Tag.ToString()));
                objKH.MaHuyen = (short?)MaHuyen;
                objKH.MaHuyen2 = (short?)MaHuyen2;
                objKH.MaTinh = MaTinh;
                objKH.MaNKH = (byte?)lookUpNhomKH.EditValue;

                if (lookNguonDen1.ItemIndex != null)
                {
                    objKH.HowToKnowID = (short?)lookNguonDen1.EditValue;
                }
                else
                {
                    DialogBox.Error("Bạn chưa chọn nguồn đến!");
                    return;
                }

                if (objKH.MaKH == 0)
                {
                    var a = (db.KhachHangs.Max(p => (int?)p.MaKH) ?? 0) + 1;
                    //  objKH.KyHieu = db.DinhDang(13, a);

                    objKH.MaNV = Common.StaffID;
                    objKH.NgayDangKy = db.getDate();
                    //objKH.IsVisible = true;
                    db.KhachHangs.InsertOnSubmit(objKH);
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


        }

        private void btnSaveHis_Click(object sender, EventArgs e)
        {
            var db = new MasterDataContext();
            var mabc = (int?)lookKHBanChoThue.EditValue;
            var mamt = (int?)lookKHMuaThue.EditValue;
            var manc = (int?)lookNhuCau.EditValue;
            if (manc == null)
            {
                DialogBox.Infomation("Vui lòng loại nhu cầu xin cảm ơn");
                lookNhuCau.Focus();
                return;
            }
            if (lookBefore.EditValue == null)
            {
                DialogBox.Infomation("Vui lòng chọn trạng thái trước khi gọi. Xin cảm ơn");
                lookBefore.Focus();
                return;
            }

            if (lkTrangThai.EditValue == null)
            {
                DialogBox.Infomation("Vui lòng chọn trạng thái sau khi gọi. Xin cảm ơn");
                lkTrangThai.Focus();
                return;
            }

            if (lookStatusCall.EditValue == null)
            {
                DialogBox.Infomation("Vui lòng chọn trạng thái. Xin cảm ơn");
                lookStatusCall.Focus();
                return;
            }

            if (lookPhuongThuc.EditValue == null)
            {
                DialogBox.Infomation("Vui lòng chọn phương thức. Xin cảm ơn");
                lookPhuongThuc.Focus();
                return;
            }

            if ((manc == 1 | manc == 2))
            {

                if (mabc == null)
                {
                    DialogBox.Error("Vui lòng chọn nhu cầu");
                    lookKHBanChoThue.Focus();
                    return;

                }
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

                mglbcNhatKyXuLy objnkb = new mglbcNhatKyXuLy();
                if (this.ID != null)
                {
                    objnkb = db.mglbcNhatKyXuLies.FirstOrDefault(p => p.ID == this.ID);
                    if (objnkb == null) return;
                }
                objnkb.MaBC = mabc;
                objnkb.NgayXL = db.getDate();
                objnkb.TieuDe = txtTieuDe.Text;
                objnkb.NoiDung = txtNoiDung.Text;
                objnkb.MaPT = (byte?)lookPhuongThuc.EditValue;
                objnkb.MaNVG = Common.StaffID;
                objnkb.DiDong = this.SDT;
                objnkb.MaTTBefore = (byte?)lookBefore.EditValue;
                objnkb.MaTTGoi = (byte?)lookStatusCall.EditValue;
                objnkb.MaTT = (byte?)lookTrangThai.EditValue;
                objnkb.StartDate = this.StartDate;
                objnkb.Enddate = db.getDate();
                if(this.LoaiCG == 0)
                {
                    objnkb.LoaiCG = this.LoaiCG;
                }
                else
                {
                    objnkb.LoaiCG = null;
                }
               
                objnkb.MaNVN = db.mglbcBanChoThues.SingleOrDefault(p => p.MaBC == mabc).MaNVKD;
                if (this.ID == null)
                {
                    db.mglbcNhatKyXuLies.InsertOnSubmit(objnkb);
                }
                db.SubmitChanges();
                this.ID = objnkb.ID;
                this.MaBC = mabc;
                if (objnkb.FileRecord == null || objnkb.FileRecord == "")
                {
                    Thread tid1 = new Thread(new ThreadStart(AsyncFIle));
                    tid1.Start();

                }
                this.IsSave = true;
                DialogBox.Infomation("Xử lý thành công!");


            }
            else if (manc == 5)
            {
                try
                {
                    var obj = new khCall_History();
                    if (this.IdkhCall != null)
                    {
                        obj = db.khCall_Histories.FirstOrDefault(p => p.ID == this.IdkhCall);
                        if (obj == null) return;
                    }
                    obj.MaKH = this.MaKH;
                    obj.MaNV = Common.StaffID;
                    obj.NgayGui = (DateTime?)dateNgayGhiNhan.EditValue;
                    obj.TenKH = txtKhachHang.Text.Trim();
                    obj.SDT = txtSDT.Text;
                    obj.Line = txtLine.Text.Trim();
                    obj.UniqueID = txtUnique.Text;
                    obj.XuLy = txtXuLy.Text;
                    obj.GhiChu = txtTieuDe.Text;
                    obj.XuLy = txtNoiDung.Text;
                    if (this.IdkhCall == null)
                    {
                        db.khCall_Histories.InsertOnSubmit(obj);
                    }

                    db.SubmitChanges();
                    this.IdkhCall = obj.ID;
                    this.IsSave = true;

                    DialogBox.Infomation("Xử lý thành công!");
                    this.Close();

                }
                catch
                {

                }

            }

            if ((manc == 3 | manc == 4))
            {
                if (mamt == null)
                {
                    DialogBox.Error("Vui lòng chọn nhu cầu ");
                    lookKHMuaThue.Focus();
                    return;

                }

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
                mglmtNhatKyXuLy objnkbmt = new mglmtNhatKyXuLy();
                if (this.IdNKMT != null)
                {
                    objnkbmt = db.mglmtNhatKyXuLies.FirstOrDefault(p => p.ID == this.IdNKMT);
                    if (objnkbmt == null) return;
                }
                objnkbmt.MaMT = mamt;
                objnkbmt.NgayXL = db.getDate();
                objnkbmt.TieuDe = txtTieuDe.Text;
                objnkbmt.NoiDung = txtNoiDung.Text;
                objnkbmt.MaPT = (byte?)lookPhuongThuc.EditValue;
                objnkbmt.MaNVG = Common.StaffID;
                objnkbmt.StartDate = this.StartDate;
                objnkbmt.EndDate = db.getDate();
                if (this.LoaiCG == 0)
                {
                    objnkbmt.LoaiCG = this.LoaiCG;
                }
                else
                {
                    objnkbmt.LoaiCG = null;
                }
                objnkbmt.MaNVN = db.mglmtMuaThues.SingleOrDefault(p => p.MaMT == MaMT).MaNVKD;
                if (this.IdNKMT == null)
                {
                    db.mglmtNhatKyXuLies.InsertOnSubmit(objnkbmt);
                }

                db.SubmitChanges();
                this.MaMT = mamt;
                this.IdNKMT = objnkbmt.ID;
                this.IsSave = true;
                if (objnkbmt.FileRecord == null || objnkbmt.FileRecord == "")
                {
                    Thread tid1 = new Thread(new ThreadStart(AsyncFIle));
                    tid1.Start();

                }
                DialogBox.Infomation("Xử lý thành công!");
                this.Close();
                //  this.Close();
            }
            else if (manc == 5)
            {
                try
                {
                    var obj = new khCall_History();
                    if (this.IdkhCall != null)
                    {
                        obj = db.khCall_Histories.FirstOrDefault(p => p.ID == this.IdkhCall);
                        if (obj == null) return;
                    }
                    obj.MaKH = this.MaKH;
                    obj.MaNV = Common.StaffID;
                    obj.NgayGui = (DateTime?)dateNgayGhiNhan.EditValue;
                    obj.TenKH = txtKhachHang.Text.Trim();
                    obj.SDT = txtSDT.Text;
                    obj.Line = txtLine.Text.Trim();
                    obj.UniqueID = txtUnique.Text;
                    obj.XuLy = txtXuLy.Text;
                    obj.GhiChu = txtTieuDe.Text;
                    obj.XuLy = txtNoiDung.Text;

                    if (this.IdkhCall == null)
                    {
                        db.khCall_Histories.InsertOnSubmit(obj);
                    }
                    db.SubmitChanges();
                    this.IdkhCall = obj.ID;
                    this.IsSave = true;

                    DialogBox.Infomation("Xử lý thành công!");
                    this.Close();

                }
                catch
                {

                }

            }
            btnSaveHis.Enabled = false;
            itemEditHistory.Enabled = true;

        }
        
        public void AsyncFIle()
        {
            if (this.IdNKMT > 0)
                VOIPSETUP.Utils.Common.GetFile(this.IdNKMT);
            else
                VOIPSETUP.Utils.Common.GetFile(this.ID);

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
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

        void SaveCoHoi()
        {

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
            //    objNC.NgayNhap =db.getDate();
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

        }
        public void LoadLichSu()
        {
            var db = new MasterDataContext();
            gcLichSu.DataSource = db.khCall_Histories.Where(p => p.MaKH == this.MaKH).OrderByDescending(p => p.NgayGui)
                        .Select(p => new
                        {
                            p.ID,
                            p.IsSucces,
                            p.MaKH,
                            p.NgayGui,
                            p.SDT,
                            p.GhiChu,
                            p.HieuQua,
                            p.XuLy,
                            p.TinhTrang,
                            p.LieuDung,
                            p.NguoiChiDinh,
                            p.MaNCD,
                            p.MaPH,
                            p.LieuTrinh,
                            p.TieuDeHen,
                            p.MaNV,
                            p.NgayHen,
                            //   p.CaiThien,
                            p.KhongCaiThien,
                            p.MaNT,
                            // HoTen = p.MaNV == null ? "" : p.NhanVien.HoTen
                        }).ToList();
        }
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {


            switch (gcNhatKyMT.SelectedTabPageIndex)
            {
                case 0:
                    LoadLichSu();
                    break;


            }

        }


        public void addNhuCau()
        {
            try
            {
                if (this.MaKH == null)
                {
                    DialogBox.Error("Vui lòng lưu khách hàng trước khi thực hiện thao tác.");
                    return;
                }
                if ((int?)lookNhuCau.EditValue == 1)
                {
                    //  itemThemNhuCau.Visible = true;
                    BEE.HoatDong.MGL.Ban.frmEdit frm = new BEE.HoatDong.MGL.Ban.frmEdit();
                    frm.MaKH = this.MaKH;
                    frm.mucdich = true;
                    frm.ShowDialog();

                    // lookKHBanChoThue.EditValue = frm.MaBC;

                }
                else if ((int?)lookNhuCau.EditValue == 2)
                {
                    // itemThemNhuCau.Visible = true;
                    BEE.HoatDong.MGL.Ban.frmEdit frm = new BEE.HoatDong.MGL.Ban.frmEdit();
                    frm.mucdich = false;
                    frm.MaKH = this.MaKH;
                    frm.ShowDialog();

                    // lookKHBanChoThue.EditValue = frm.MaBC;
                }
                else if ((int?)lookNhuCau.EditValue == 3)
                {
                    //  itemThemNhuCau.Visible = true;
                    BEE.HoatDong.MGL.Mua.frmEdit frm = new BEE.HoatDong.MGL.Mua.frmEdit();
                    frm.NhuCau = true;
                    frm.MaKH = this.MaKH;
                    frm.ShowDialog();

                    // lookKHMuaThue.EditValue = frm.MaMT;
                }
                else if ((int?)lookNhuCau.EditValue == 4)
                {
                    //   itemThemNhuCau.Visible = true;
                    BEE.HoatDong.MGL.Mua.frmEdit frm = new BEE.HoatDong.MGL.Mua.frmEdit();
                    frm.NhuCau = false;
                    frm.MaKH = this.MaKH;
                    frm.ShowDialog();

                    // lookKHMuaThue.EditValue = frm.MaMT;
                }
                else if ((int?)lookNhuCau.EditValue == 5)
                {
                    txtNoiDung.Focus();
                    // itemThemNhuCau.Visible = false;
                }
                else
                {
                    txtNoiDung.Focus();
                    // itemThemNhuCau.Visible = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }

        }

        private void itemThemNhuCau_Click(object sender, EventArgs e)
        {

            if ((int?)lookNhuCau.EditValue == 1)
            {
                itemThemNhuCau.Visible = true;


            }
            else if ((int?)lookNhuCau.EditValue == 2)
            {
                itemThemNhuCau.Visible = true;

            }
            else if ((int?)lookNhuCau.EditValue == 3)
            {
                itemThemNhuCau.Visible = true;

            }
            else if ((int?)lookNhuCau.EditValue == 4)
            {
                itemThemNhuCau.Visible = true;

            }
            else if ((int?)lookNhuCau.EditValue == 5)
            {
                txtNoiDung.Focus();
                itemThemNhuCau.Visible = false;
            }
            else
            {
                txtNoiDung.Focus();
                itemThemNhuCau.Visible = false;
            }

            Thread t = new Thread(addNhuCau);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            LoadNhuCau();



            //try
            //{
            //    var t2 = new Thread(addNhuCau);
            //    t2.SetApartmentState(ApartmentState.STA);
            //    t2.Start();
            //}
            //catch 
            //{
            //    addNhuCau();

            //}
        }

        private void lookNhuCau_EditValueChanged(object sender, EventArgs e)
        {
            //if (lookNhuCau.EditValue != null)
            //{
            //    timeChangeColor.Stop();
            //}

            if (this.MaKH == null)
            {
                DialogBox.Error("Vui lòng lưu khách hàng trước khi thực hiện thao tác.");
                return;
            }
            if ((int?)lookNhuCau.EditValue == 1 | (int?)lookNhuCau.EditValue == 2 | (int?)lookNhuCau.EditValue == 3 | (int?)lookNhuCau.EditValue == 4)
            {
                itemThemNhuCau.Visible = true;

            }

            else if ((int?)lookNhuCau.EditValue == 5)
            {
                txtNoiDung.Focus();
                itemThemNhuCau.Visible = false;
            }
            else
            {
                txtNoiDung.Focus();
                itemThemNhuCau.Visible = false;
            }


        }

        private void timeChangeColor_Tick(object sender, EventArgs e)
        {
            Thread.Sleep(500);
            lookNhuCau.BackColor = Color.GreenYellow;
            Thread.Sleep(500);
            lookNhuCau.BackColor = Color.LightGreen;
            Thread.Sleep(500);
            lookNhuCau.BackColor = Color.Yellow;
            Thread.Sleep(500);
            lookNhuCau.BackColor = Color.White;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }


        public void EditBanChoThue()
        {
            var mabc = (int?)lookKHBanChoThue.EditValue;
            if (mabc == null)
            {
                DialogBox.Infomation("Vui lòng chọn nhu cầu cần sửa");
                return;
            }
            BEE.HoatDong.MGL.Ban.frmEdit frm = new BEE.HoatDong.MGL.Ban.frmEdit();
            frm.MaBC = mabc;
            frm.ShowDialog();
        }

        private void itemEditBanChoThue_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(EditBanChoThue);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }


        public void EditCanMuaThue()
        {
            var mamt = (int?)lookKHMuaThue.EditValue ?? 0;
            if (mamt == 0)
            {
                DialogBox.Infomation("Vui lòng chọn nhu cầu cần sửa");
                return;
            }
            BEE.HoatDong.MGL.Mua.frmEdit frm = new BEE.HoatDong.MGL.Mua.frmEdit();
            frm.MaMT = mamt;
            frm.ShowDialog();
        }

        private void itemEditMuaThue_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(EditCanMuaThue);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }

        private void lookKHBanChoThue_EditValueChanged(object sender, EventArgs e)
        {
            PhanQuyenSDT(1);
            gcNhatKyMT.SelectedTabPage = xtraTabPage4;
            var db = new MasterDataContext();
            var mabc = (int?)lookKHBanChoThue.EditValue;
            this.MaBC = mabc;
            if (mabc != null)
            {
                LoadMaBC((int)mabc);
            }
            lookKHMuaThue.EditValue = null;
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
                                  //p.PhuongThucXuLy.TenPT,
                                  p.MaNVG,
                                  HoTenNVG = nv.HoTen,
                                  HoTenNVN = nv1.HoTen,
                                  p.KetQua,
                                  pt.TenPT
                              }).ToList();
            gcNhatKy.DataSource = listNhatKy;
        }

        public void PhanQuyenSDT(int typeid)
        {
            int? sbid;
            if (typeid == 1)
            {
                it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 175);
                sbid = o.SDB.SDBID;

                var obj = db.mglbcPhanQuyens.Single(p => p.MaNV == Common.StaffID);

                switch (sbid)
                {
                    case 1:
                        if (obj.DienThoai == false)
                        {
                            txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                            txtDienThoai3.Text = Common.Right(objKH.DiDong3, 3);
                            txtDienThoai4.Text = Common.Right(objKH.DiDong4, 3);
                            lbNotice.Text = Common.Right(SDT, 3) + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";

                        }
                        else if (obj.DienThoai3Dau == false)
                        {
                            txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                            txtDienThoai3.Text = Common.Right1(objKH.DiDong3, 3);
                            txtDienThoai4.Text = Common.Right1(objKH.DiDong4, 3);
                            lbNotice.Text = Common.Right(SDT, 3) + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";
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
                            lbNotice.Text = SDT + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";

                        }
                        break;

                    case 2:
                        if (obj.DienThoai == false)
                        {
                            txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                            txtDienThoai3.Text = Common.Right(objKH.DiDong3, 3);
                            txtDienThoai4.Text = Common.Right(objKH.DiDong4, 3);
                            lbNotice.Text = Common.Right(SDT, 3) + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";
                        }
                        else if (obj.DienThoai3Dau == false)
                        {
                            txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                            txtDienThoai3.Text = Common.Right1(objKH.DiDong3, 3);
                            txtDienThoai4.Text = Common.Right1(objKH.DiDong4, 3);
                            lbNotice.Text = Common.Right1(SDT, 3) + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";
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
                            lbNotice.Text = SDT + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";

                        }

                        break;
                    case 3:
                        if (obj.DienThoai == false)
                        {
                            txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                            txtDienThoai3.Text = Common.Right(objKH.DiDong3, 3);
                            txtDienThoai4.Text = Common.Right(objKH.DiDong4, 3);
                            lbNotice.Text = Common.Right(SDT, 3) + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";
                        }
                        else if (obj.DienThoai3Dau == false)
                        {
                            txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                            txtDienThoai3.Text = Common.Right1(objKH.DiDong3, 3);
                            txtDienThoai4.Text = Common.Right1(objKH.DiDong4, 3);
                            lbNotice.Text = Common.Right1(SDT, 3) + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";
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
                            lbNotice.Text = SDT + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";

                        }

                        break;
                    case 4:
                        if (obj.DienThoai == false)
                        {
                            txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                            txtDienThoai3.Text = Common.Right(objKH.DiDong3, 3);
                            txtDienThoai4.Text = Common.Right(objKH.DiDong4, 3);
                            lbNotice.Text = Common.Right(SDT, 3) + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";
                        }
                        else if (obj.DienThoai3Dau == false)
                        {
                            txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                            txtDienThoai3.Text = Common.Right1(objKH.DiDong3, 3);
                            txtDienThoai4.Text = Common.Right1(objKH.DiDong4, 3);
                            lbNotice.Text = Common.Right1(SDT, 3) + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";
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
                            lbNotice.Text = SDT + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";

                        }
                        break;


                }


            }
            else
            {
                it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 177);
                sbid = o.SDB.SDBID;
                var obj = db.mglmtPhanQuyens.Single(p => p.MaNV == Common.StaffID);
                switch (sbid)
                {
                    case 1:
                        if (obj.DienThoai == false)
                        {
                            txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                            txtDienThoai3.Text = Common.Right(objKH.DiDong3, 3);
                            txtDienThoai4.Text = Common.Right(objKH.DiDong4, 3);
                            lbNotice.Text = Common.Right(SDT, 3) + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";
                        }
                        else if (obj.DienThoai3Dau == false)
                        {
                            txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                            txtDienThoai3.Text = Common.Right1(objKH.DiDong3, 3);
                            txtDienThoai4.Text = Common.Right1(objKH.DiDong4, 3);
                            lbNotice.Text = Common.Right1(SDT, 3) + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";
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
                            lbNotice.Text = SDT + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";

                        }
                        break;

                    case 2:
                        if (obj.DienThoai == false)
                        {
                            txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                            txtDienThoai3.Text = Common.Right(objKH.DiDong3, 3);
                            txtDienThoai4.Text = Common.Right(objKH.DiDong4, 3);
                            lbNotice.Text = Common.Right(SDT, 3) + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";
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
                        else if (obj.DienThoai3Dau == false)
                        {
                            txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                            txtDienThoai3.Text = Common.Right1(objKH.DiDong3, 3);
                            txtDienThoai4.Text = Common.Right1(objKH.DiDong4, 3);
                            lbNotice.Text = Common.Right1(SDT, 3) + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";
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
                            lbNotice.Text = SDT + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";

                        }

                        break;
                    case 3:
                        if (obj.DienThoai == false)
                        {
                            txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                            txtDienThoai3.Text = Common.Right(objKH.DiDong3, 3);
                            txtDienThoai4.Text = Common.Right(objKH.DiDong4, 3);
                            lbNotice.Text = Common.Right(SDT, 3) + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";
                        }
                        else if (obj.DienThoai3Dau == false)
                        {
                            txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                            txtDienThoai3.Text = Common.Right1(objKH.DiDong3, 3);
                            txtDienThoai4.Text = Common.Right1(objKH.DiDong4, 3);
                            lbNotice.Text = Common.Right1(SDT, 3) + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";
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
                            lbNotice.Text = SDT + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";

                        }

                        break;
                    case 4:
                        if (obj.DienThoai == false)
                        {
                            txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                            txtDienThoai3.Text = Common.Right(objKH.DiDong3, 3);
                            txtDienThoai4.Text = Common.Right(objKH.DiDong4, 3);
                            lbNotice.Text = Common.Right(SDT, 3) + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";
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
                        else if (obj.DienThoai3Dau == false)
                        {
                            txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                            txtDienThoai3.Text = Common.Right1(objKH.DiDong3, 3);
                            txtDienThoai4.Text = Common.Right1(objKH.DiDong4, 3);
                            lbNotice.Text = Common.Right1(SDT, 3) + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";
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
                            lbNotice.Text = SDT + "KHÁCH HÀNG ĐÃ CÓ TRONG HỆ THỐNG";

                        }
                        break;



                }
            }


            if (objKH.DiDong != "")
            {
                txtDiDong.Enabled = false;
            }
            else
            {
                txtDiDong.Enabled = true;
            }
            if (objKH.DiDong2 != "")
            {
                txtDiDong2.Enabled = false;
            }
            else
            {
                txtDiDong2.Enabled = true;
            }
            if (objKH.DiDong3 != "")
            {
                txtDienThoai3.Enabled = false;
            }
            else
            {
                txtDienThoai3.Enabled = true;
            }
            if (objKH.DiDong4 != "")
            {
                txtDienThoai4.Enabled = false;
            }
            else
            {
                txtDienThoai4.Enabled = true;
            }

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


        private void lookKHMuaThue_EditValueChanged(object sender, EventArgs e)
        {
            PhanQuyenSDT(2);

            if (txtDiDong2.Text == "" | txtDiDong2 == null)
            {
                txtDiDong2.Enabled = true;
            }
            if (txtDienThoai3.Text == "" | txtDienThoai3 == null)
            {
                txtDienThoai3.Enabled = true;
            }
            if (txtDienThoai4.Text == "" | txtDienThoai4 == null)
            {
                txtDienThoai4.Enabled = true;

            }
            gcNhatKyMT.SelectedTabPage = xtraTabPage5;
            var db = new MasterDataContext();
            var mamt = (int?)lookKHMuaThue.EditValue ?? 0;
            this.MaMT = mamt;
            if (mamt != 0)
            {
                LoadMaMT(mamt);
            }

            lookKHBanChoThue.EditValue = null;

            var listNhatKymt = (from p in db.mglmtNhatKyXuLies
                                join nv in db.NhanViens on p.MaNVG equals nv.MaNV into nhanvien
                                from nv in nhanvien.DefaultIfEmpty()
                                join nv1 in db.NhanViens on p.MaNVN equals nv1.MaNV into nhanvien1
                                from nv1 in nhanvien1.DefaultIfEmpty()
                                join pt in db.PhuongThucXuLies on p.MaPT equals pt.MaPT into phuongthuc
                                from pt in phuongthuc.DefaultIfEmpty()
                                where p.MaMT == this.MaMT
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
                                    pt.TenPT
                                }).ToList();
            gcLichSuMuaThue.DataSource = listNhatKymt;
        }


        private void btnMaXa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BEE.NghiepVuKhac.SelectPosition_frm frm = new BEE.NghiepVuKhac.SelectPosition_frm();
            frm.ShowDialog();
            if (frm.Result != "")
            {
                btnDCLH.Tag = frm.MaXa;
                btnDCLH.Text = frm.Result;
                MaHuyen2 = frm.MaHuyen;
                MaTinh2 = frm.MaTinh;
            }
        }

        private void btnMaXa2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BEE.NghiepVuKhac.SelectPosition_frm frm = new BEE.NghiepVuKhac.SelectPosition_frm();
            frm.ShowDialog();
            if (frm.Result != "")
            {

                MaHuyen2 = frm.MaHuyen;
                MaTinh2 = frm.MaTinh;
            }
        }

        private void btnDCTT_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BEE.NghiepVuKhac.SelectPosition_frm frm = new BEE.NghiepVuKhac.SelectPosition_frm();
            frm.ShowDialog();
            if (frm.Result != "")
            {
                btnDCTT.Tag = frm.MaXa;
                btnDCTT.Text = frm.Result;
                MaHuyen = frm.MaHuyen;
                MaTinh = frm.MaTinh;
            }
        }

        private void btnDCLH_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BEE.NghiepVuKhac.SelectPosition_frm frm = new BEE.NghiepVuKhac.SelectPosition_frm();
            frm.ShowDialog();
            if (frm.Result != "")
            {
                btnDCLH.Tag = frm.MaXa;
                btnDCLH.Text = frm.Result;
                MaHuyen2 = frm.MaHuyen;
                MaTinh2 = frm.MaTinh;
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

        private void frmInCall_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (IsSave == null || IsSave == false)
            {
                if (DialogBox.Question("Bạn có muốn lưu không") == DialogResult.Yes)
                {
                    return;
                }
                else
                {
                    var obj = new LichSuDongFormInCall();
                    obj.MaNV = Common.StaffID;
                    obj.NgayDong = db.getDate();
                    obj.MaKH = this.MaKH;
                    obj.DiDong = this.SDT;
                    db.LichSuDongFormInCalls.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    this.Close();
                }

            }

        }


        private void frmInCall_FormClosing(object sender, FormClosingEventArgs e)
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

        private void btnGhiAm1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var filerrecord = (sender as ButtonEdit).Text ?? "";
            if (filerrecord.Trim() == "") // sẽ đồng bộ dữ liệu
            {
                var objconfig = db.tblConfigs.FirstOrDefault(p => p.TypeID == 3);
                var request = WebRequest.Create(objconfig.FtpUrl);
                string mk = it.CommonCls.GiaiMa(objconfig.FtpPass);
                request.Credentials = new NetworkCredential(objconfig.FtpUser, it.CommonCls.GiaiMa(objconfig.FtpPass));
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var img = Bitmap.FromStream(stream))
                {
                    if (!System.IO.Directory.Exists("images"))
                        System.IO.Directory.CreateDirectory("images");
                    // var pp = Application.StartupPath + "\\images" + "/" + i.ID + "_" +db.getDate().ToString("ddyyyyMMHHmmssffffff") + ".jpg";
                    // img.Save(pp, System.Drawing.Imaging.ImageFormat.Png);


                }
            }

            else
            {

            }

        }


        private void itemCreateNew_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(CreateNew);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        public void CreateNew()
        {
            using (var frm = new HoatDong.MGL.Ban.frmEdit())
            {
                frm.MaKH = this.MaKH;
                frm.mucdich = false;
                frm.ShowDialog();
            }
        }


        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(CreateNewMT);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }
        public void CreateNewMT()
        {
            using (var frm = new HoatDong.MGL.Mua.frmEdit())
            {
                frm.MaKH = this.MaKH;
                frm.ShowDialog();
            }
        }

        private void itemEditHistory_Click(object sender, EventArgs e)
        {
            btnSaveHis.Enabled = true;
            itemEditHistory.Enabled = false;
        }

        private void picImg_Click(object sender, EventArgs e)
        {

            try
            {
                if (this.MaBC == 0 || this.MaBC == null)
                {
                    DialogBox.Error("Vui lòng chọn bất động sản");
                    return;
                }
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
        private void LoadMaBC(int mabc)
        {
            if (mabc > 0)
            {
                var objBC = db.mglbcBanChoThues.SingleOrDefault(p => p.MaBC == this.MaBC);
                txtSoDangKy.Text = objBC.SoDK;
                lkLoaiBDS.EditValue = objBC.MaLBDS;
                lookTrangThai.EditValue = lookBefore.EditValue = objBC.MaTT;

                picImg.Image = BEE.VOIPSETUP.Properties.Resources._58_image;
                picImg.Enabled = db.mglbcAnhbds.FirstOrDefault(p => p.MaBC == this.MaBC) == null ? false : true;

                picVideo.Image = BEE.VOIPSETUP.Properties.Resources.youtube;
                picVideo.Enabled = db.mglbcVideobds.FirstOrDefault(p => p.MaBC == this.MaBC) == null ? false : true;
            }
        }

        private void LoadMaMT(int mamt)
        {
            if (mamt > 0)
            {
                var objBC = db.mglmtMuaThues.SingleOrDefault(p => p.MaMT == mamt);
                lookTrangThai.EditValue = lookBefore.EditValue = objBC.MaTT;

                picImg.Image = BEE.VOIPSETUP.Properties.Resources._58_image;
                picImg.Enabled = db.mglbcAnhbds.FirstOrDefault(p => p.MaBC == this.MaBC) == null ? false : true;

                picVideo.Image = BEE.VOIPSETUP.Properties.Resources.youtube;
                picVideo.Enabled = db.mglbcVideobds.FirstOrDefault(p => p.MaBC == this.MaBC) == null ? false : true;
            }
        }
        public void Video()
        {
            using (var frm = new HoatDong.MGL.Ban.frmVideo())
            {
                frm.MaBC = this.MaBC;
                frm.ShowDialog();
            }
        }

        private void picVideo_Click(object sender, EventArgs e)
        {
            var t = new Thread(Video);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

        }

        //private void lookLoaiSP_EditValueChanged(object sender, EventArgs e)
        //{
        //    var mlsp = (int?)lookLoaiSP.EditValue;
        //    ckSanPham.Properties.DataSource = db.SanPhams.Where(p => p.MaLSP == mlsp).ToList();
        //}
    }
}
