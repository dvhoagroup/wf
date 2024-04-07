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

            dateNgayGhiNhan.EditValue = DateTime.Now;
            lookPhuongThuc.Properties.DataSource = db.PhuongThucXuLies;

            lookTinh.Properties.DataSource = db.Tinhs;
            lookHuyen.Properties.DataSource = db.Huyens;
            lookXa.Properties.DataSource = db.Xas;
            lkLoaiBDS.Properties.DataSource = db.LoaiBDs;
            lookTrangThai.Properties.DataSource = db.mglbcTrangThais;
            lookNCD.DataSource = lookNgCDTuVan.Properties.DataSource = db.NhanViens.Select(p => new { p.HoTen, p.MaNV });
         
            lookUpQuyDanh.Properties.DataSource = db.QuyDanhs;
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

            if (SDT != null || Unique != null)
            {
                string dtm9 = SDT.Remove(0, 1);
                var obj = db.KhachHangs.Where(p => p.DiDong == SDT || p.DienThoaiCT == SDT || p.DiDong == SDT || p.DiDong2 == SDT || p.DiDong3 == SDT || p.DiDong4 == SDT).ToList();
                if (obj.Count() > 0)
                {

                    this.objKH = db.KhachHangs.SingleOrDefault((p => p.DiDong == SDT || p.DienThoaiCT == SDT || p.DiDong == SDT || p.DiDong2 == SDT || p.DiDong3 == SDT || p.DiDong4 == SDT));
                    txtSDT.Text = SDT;
                    txtUnique.Text = Unique;
                    txtLine.Text = line;
                    txtKhachHang.Text = KhachHang;
                    dateNgayGhiNhan.EditValue = DateTime.Now;
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
                    txtTieuDe.Text = "Gọi: " + td + " "+ objKH.HoTenKH;
                    //gcLichSu.DataSource = db.NhuCauCall_Histories.Where(p => p.MaKH == objKH.MaKH || p.SDT == SDT).OrderByDescending(p => p.NgayGui)
                    //       .Select(p => new
                    //       {
                    //           p.ID,
                    //           p.IsSucces,
                    //           p.MaKH,
                    //           p.NgayGui,
                    //           p.SDT,
                    //           p.GhiChu,
                    //           p.HieuQua,
                    //           p.XuLy,
                    //           p.TinhTrang,
                    //           p.LieuDung,
                    //           p.NguoiChiDinh,
                    //           p.MaNCD,
                    //           p.MaPH,
                    //           p.LieuTrinh,
                    //           p.TieuDeHen,
                    //           p.NgayHen,
                    //           //   p.CaiThien,
                    //           p.KhongCaiThien,
                    //           p.MaNT,
                    //           // HoTen = p.MaNV == null ? "" : p.NhanVien.HoTen
                    //       });

                    // Ban_Load(objKH.MaKH);
                    //  MuaThue_Load(objKH.MaKH);
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
         
            txtDienThoaiCT.EditValue = SDT;
            txtEmailCT.EditValue = null;
            txtGhiChu.EditValue = null;
            txtDiDong.EditValue = SDT;
            lookNgCDTuVan.EditValue = Common.StaffID;

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
               // objKH.DiaChiCT = txtDiaChiCT1.Text;
                objKH.IsPersonal = true;
                objKH.DiDong = txtDiDong.Text;
                objKH.DienThoaiCT = txtDienThoaiCT.Text;
                objKH.Email = txtEmail.Text;
                objKH.MaSoThueCT = txtMaSoThueCT.Text;

                objKH.GhiChu = txtGhiChuKH.Text;
                objKH.DiDong2 = txtDiDong2.Text;
                objKH.DiDong3 = txtDienThoai3.Text;
                objKH.DiDong4 = txtDienThoai4.Text;
                objKH.ThuongTru = txtThuongTru.Text;

                objKH.DiaChi = txtDiaChi.Text;
                if (btnDCTT.Text != "")
                    objKH.Xa = db.Xas.First(p => p.MaXa == int.Parse(btnDCTT.Tag.ToString()));
                if (btnDCLH.Text != "")
                    objKH.Xa1 = db.Xas.First(p => p.MaXa == int.Parse(btnDCTT.Tag.ToString()));

                objKH.MaHuyen = (short?)MaHuyen;
                objKH.MaHuyen2 = (short?)MaHuyen2;
                objKH.MaTinh = MaTinh;
                objKH.MaTinh2 = MaTinh2;



                if (objKH.MaKH == 0)
                {
                    var a = (db.KhachHangs.Max(p => (int?)p.MaKH) ?? 0) + 1;
                    //  objKH.KyHieu = db.DinhDang(13, a);

                    objKH.MaNV = Common.StaffID;
                    objKH.NgayDangKy = DateTime.Now;
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
            finally
            {
                db.Dispose();
            }

        }
       
        private void btnSaveHis_Click(object sender, EventArgs e)
        {

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

            //if (lookPhuongThuc.Text == "")
            //{
            //    DialogBox.Infomation("Vui lòng chọn phương thức xử lý. Xin cảm ơn");
            //    lookPhuongThuc.Focus();
            //    return;
            //}
            mglbcNhatKyXuLy objNhatKy = new mglbcNhatKyXuLy();
            objNhatKy.MaBC = this.MaBC;
            objNhatKy.NgayXL = DateTime.Now;
            objNhatKy.TieuDe = txtTieuDe.Text;
            objNhatKy.NoiDung = txtNoiDung.Text;
            objNhatKy.MaPT = (byte?)lookPhuongThuc.EditValue;
            objNhatKy.MaNVG = Common.StaffID;
            objNhatKy.MaNVN = db.mglbcBanChoThues.SingleOrDefault(p => p.MaBC == this.MaBC).MaNVKD;
            db.mglbcNhatKyXuLies.InsertOnSubmit(objNhatKy);

            db.SubmitChanges();
            DialogBox.Infomation("Xử lý thành công!");
            this.Close();
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
            //try
            //{
            //    var frm = new BEECRM.CongViec.LichHen.AddNew_frm();
            //    if (MaKH > 0)
            //    {
            //        frm.MaKH = this.MaKH;
            //        frm.ShowDialog();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Vui lòng lưu khách hàng trước khi lập lịch hẹn, xin cảm ơn!");
            //    }

            //}
            //catch (Exception ex)
            //{
            //    DialogBox.Error(ex.Message);
            //}
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
            //    objNC.NgayNhap = DateTime.Now;
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
                                         pt.TenPT
                                     }).ToList();
                    gcNhatKy.DataSource = listNhatKy;
                    break;


            }

        }
        //private void lookLoaiSP_EditValueChanged(object sender, EventArgs e)
        //{
        //    var mlsp = (int?)lookLoaiSP.EditValue;
        //    ckSanPham.Properties.DataSource = db.SanPhams.Where(p => p.MaLSP == mlsp).ToList();
        //}
    }
}
