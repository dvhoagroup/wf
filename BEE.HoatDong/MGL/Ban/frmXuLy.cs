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
using BEE.HoatDong.MGL.Ban.Models.MessageQueue;
using RestSharp;
using Newtonsoft.Json;

namespace BEE.HoatDong.MGL.Ban
{
    public partial class frmXuLy : DevExpress.XtraEditors.XtraForm
    {
        public int? MaMT { get; set; }
        public frmXuLy()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lookTrangThai.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn trạng thái!");
                lookTrangThai.Focus();
                return;
            }
            var wait = DialogBox.WaitingForm();
            try
            {
                using (var db = new MasterDataContext())
                {
                    var obj = new mglbcNhatKyXuLy();
                    var objMT = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == MaMT);
                    var tt = objMT.mglbcTrangThai.TenTT;
                    var objTT = db.mglbcTrangThais.First(p => p.MaTT == (byte?)lookTrangThai.EditValue);
                    obj.TieuDe = string.Format("Chuyển trạng thái sản phẩm từ {0} thành {1}", tt, objTT.TenTT);
                    obj.NoiDung = txtNoiDung.Text.Trim();
                    obj.MaNVN = obj.MaNVG = Common.StaffID;
                    obj.NgayXL = (DateTime?)dateNgayXL.EditValue;
                    var pt = Convert.ToByte(lookTrangThai.EditValue);
                    obj.MaTTBefore = (byte)lookStatusNow.EditValue;
                    obj.MaTT = (byte)lookTrangThai.EditValue;
                    objMT.mglbcTrangThai = db.mglbcTrangThais.FirstOrDefault(p => p.MaTT == (byte?)lookTrangThai.EditValue);
                    objMT.mglbcNhatKyXuLies.Add(obj);
                    db.SubmitChanges();

                    DialogBox.Infomation("Dữ liệu đã được lưu!");

                    #region day du lieu sang kafka
                    try
                    {
                        #region kafka
                        // push data kafka
                        var objrealestate = new mglbcBanChoThueModel();
                        // khách hàng
                        var objkhadd = new List<BEE.HoatDong.MGL.Ban.Models.MessageQueue.KhachHang>();
                        var objitemkh = new BEE.HoatDong.MGL.Ban.Models.MessageQueue.KhachHang();

                        objitemkh.HoTen = objMT.KhachHang.HoKH + " " + objMT.KhachHang.TenKH;
                        objitemkh.DiDong = objMT.KhachHang.DiDong;
                        objitemkh.MaKH = objMT.KhachHang.MaKH;
                     
                        objitemkh.DiDong2 = objMT.KhachHang.DiDong2;
                        objitemkh.DiDong3 = objMT.KhachHang.DiDong3;
                        objitemkh.DiDong4 = objMT.KhachHang.DiDong4;
                        try
                        {
                            objitemkh.AppMaKH = objMT.KhachHang.AppMaKH.ToString();  // thiếu trường
                        }
                        catch
                        {

                        }

                        objkhadd.Add(objitemkh);
                        objrealestate.KhachHang = objkhadd;
                        // 
                        objrealestate.MaBC = objMT.MaBC;
                        objrealestate.MaTT = objMT.MaTT;
                        objrealestate.IsBan = objMT.IsBan;
                        // ảnh bds
                        var lstimg = new List<mglbcAnhbdsModel>();
                        foreach (var i in objMT.mglbcAnhbds)
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
                        foreach (var i in db.mglbcVideobds.Where(p => p.MaBC == objMT.MaBC).ToList())
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

                        objrealestate.ViTri = objMT.ToaDo;
                        objrealestate.SoNha = objMT.SoNha;
                        try
                        {
                            objrealestate.TenDuong = objMT.Duong.TenDuong;
                        }catch{ }
                        
                        objrealestate.MaDuong = objMT.MaDuong;
                        objrealestate.MaXa = objMT.MaXa;
                        objrealestate.MaHuyen = objMT.MaHuyen;
                        objrealestate.MaTinh = objMT.MaTinh;
                        objrealestate.MaLbds = objMT.MaLBDS;
                        objrealestate.ThanhTien = objMT.ThanhTien;
                        objrealestate.DienTich = objMT.DienTich;
                        objrealestate.SoTang = objMT.SoTang;
                        objrealestate.NgangXD = objMT.NgangXD;
                        objrealestate.MaHuong = objMT.MaHuong;
                        objrealestate.DuongRong = objMT.DuongRong;
                        objrealestate.MaTTNT = objMT.MaTTNT;
                        objrealestate.PhongNgu = objMT.PhongNgu;
                        objrealestate.PhongVs = objMT.PhongVS;
                        objrealestate.SoTangXD = objMT.SoTangXD;
                        objrealestate.PhongBep = objMT.PhongBep;
                        objrealestate.PhongAn = objMT.PhongAn;
                        objrealestate.PhongKhach = objMT.PhongKhach;
                        objrealestate.DieuHoa = objMT.DieuHoa;
                        objrealestate.NongLanh = objMT.NongLanh;
                        objrealestate.IsThangMay = objMT.IsThangMay;
                        objrealestate.TangHam = objMT.TangHam;
                        objrealestate.MaSan = objMT.MaSan;
                        objrealestate.CuaSo = objMT.CuaSo;
                        objrealestate.isBanCong = objMT.isBanCong;
                        objrealestate.isSan = objMT.isSan;
                        objrealestate.isVuon = objMT.isVuon;
                        objrealestate.MaDX = objMT.MaDX;
                        objrealestate.KhoangCachDX = objMT.KhoangCachDX;
                        objrealestate.DienTichDat = objMT.DienTichDat;
                        objrealestate.DienTichXd = objMT.DienTichXD;
                        objrealestate.DaiXD = objMT.DaiXD;
                        objrealestate.SauXD = objMT.SauXD;
                        objrealestate.SauKV = objMT.SauKV;
                        objrealestate.NgangKV = objMT.NgangKV;
                        objrealestate.MaLd = objMT.MaLD;
                        try
                        {
                            objrealestate.TenLD = db.LoaiDuongs.FirstOrDefault(p => p.MaLD == objMT.MaLD).TenLD;
                        }
                        catch 
                        {

                        }
                      
                        objrealestate.PhapLyBDS = null;
                        objrealestate.GhiChu = objMT.GhiChu;
                        objrealestate.NamXayDung = objMT.NamXayDung;
                        objrealestate.DaiKV = objMT.DaiKV; // sau thong thuy
                        try
                        {
                            objrealestate.AppMaDT = objMT.MaNVKD.ToString();
                        }
                        catch
                        {
                        }

                        objrealestate.MaBC = objMT.MaBC;
                        objrealestate.syncId = objMT.syncId;

                        try
                        {
                            if (objMT.MaBC == 0 || objMT.MaBC == null)
                            {
                                objrealestate.AppMaBC = null;

                            }
                            else
                            {
                                objrealestate.AppMaBC = objMT.AppMaBC.ToString();

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
                    catch (Exception ex)
                    {

                        DialogBox.Error("có lỗi trong quá trình đẩy dữ liệu kafka, vui lòng thử lại");
                    }

                    #endregion

                    this.Close();
                }

            }
            catch (Exception ex)
            {
                DialogBox.Infomation("Lưu bị lỗi: " + ex.Message);
            }
            finally
            {
                wait.Close();
            }
        }

        private void frmXuLy_Load(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                lookTrangThai.Properties.DataSource = lookStatusNow.Properties.DataSource = db.mglbcTrangThais;
                dateNgayXL.EditValue = DateTime.Now;
                var objMT = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == MaMT);
                lookTrangThai.EditValue = objMT.MaTT;
                lookStatusNow.EditValue = objMT.MaTT;
            }
        }
    }
}