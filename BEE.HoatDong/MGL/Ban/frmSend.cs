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
using BEEREMA;
using BEE.HoatDong.MGL.Ban.Models.MessageQueue;
using RestSharp;
using Newtonsoft.Json;

namespace BEE.HoatDong.MGL.Ban
{
    public partial class frmSend : DevExpress.XtraEditors.XtraForm
    {
        public int ID;
        public int MaBC;
        public byte? MaTT { get; set; }

        private MasterDataContext db = new MasterDataContext();
        private mglbcNhatKyXuLy objNhatKy;

        public frmSend()
        {
            InitializeComponent();
        }

        private void frmSend_Load(object sender, EventArgs e)
        {
            lookPhuongThuc.Properties.DataSource = db.PhuongThucXuLies;
            lkTrangthai.Properties.DataSource = lookStatusNow.Properties.DataSource = db.mglbcTrangThais;
            lookStatusCall.Properties.DataSource = db.bdsStatusCalls;
            lkTrangthai.EditValue = this.MaTT;
            if (this.ID > 0)
            {
                objNhatKy = db.mglbcNhatKyXuLies.Single(p => p.ID == this.ID);
                txtTieuDe.EditValue = objNhatKy.TieuDe;
                txtNoiDung.EditValue = objNhatKy.NoiDung;
                lookPhuongThuc.EditValue = objNhatKy.MaPT;
                txtSoDK.EditValue = objNhatKy.mglbcBanChoThue.SoDK;
                txtNguoiNhan.EditValue = objNhatKy.NhanVien1.HoTen;
                lkTrangthai.EditValue = objNhatKy.MaTT;
                lookStatusNow.EditValue = objNhatKy.MaTTBefore;
                lookStatusCall.EditValue = objNhatKy.MaTTGoi;
                dateStartDate.EditValue = objNhatKy.StartDate;
                dateEndDate.EditValue = objNhatKy.Enddate;
                

            }
            else
            {
                var objBc = db.mglbcBanChoThues.Single(p => p.MaBC == this.MaBC);
                objNhatKy = new mglbcNhatKyXuLy();
                objNhatKy.mglbcBanChoThue = objBc;
                txtSoDK.EditValue = objBc.SoDK;
                txtNguoiNhan.EditValue = objBc.NhanVien.HoTen;
                lookPhuongThuc.ItemIndex = 0;
                lookStatusNow.EditValue = objBc.MaTT;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
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

            if (lookPhuongThuc.EditValue == null || string.IsNullOrEmpty(lookPhuongThuc.Text.Trim()))
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


            objNhatKy.NgayXL = DateTime.Now;
            objNhatKy.TieuDe = txtTieuDe.Text;
            objNhatKy.NoiDung = txtNoiDung.Text;
            objNhatKy.MaPT = (byte)lookPhuongThuc.EditValue;
            objNhatKy.MaNVG = Common.StaffID;
            objNhatKy.MaTT = (byte?)lkTrangthai.EditValue;
            objNhatKy.MaTTBefore = (byte)lookStatusNow.EditValue;
            objNhatKy.MaTTGoi = (byte)lookStatusCall.EditValue;
            objNhatKy.LoaiCG = 0;
            objNhatKy.StartDate = dateStartDate.EditValue != null ? (DateTime?)dateStartDate.EditValue : DateTime.Now;
            objNhatKy.Enddate = dateEndDate.EditValue != null ? (DateTime?)dateEndDate.EditValue : DateTime.Now;

            var objBC = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == MaBC);
            objBC.MaTT = (byte?)lkTrangthai.EditValue;

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

            if (this.ID == 0)
            {
                try
                {
                    objNhatKy.MaNVN = objNhatKy.mglbcBanChoThue.MaNVKD;
                    db.mglbcNhatKyXuLies.InsertOnSubmit(objNhatKy);
                }
                catch (Exception ex)
                {

                    
                }
               
            }
            db.SubmitChanges();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}