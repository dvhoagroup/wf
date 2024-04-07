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
    public partial class frmSyncQueue : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        int? MaDuong = 0;
        public frmSyncQueue()
        {
            InitializeComponent();
        }

        private void frmSyncQueue_Load(object sender, EventArgs e)
        {
            int total = 0;
            int success = 0;
            int back = 0;

            total = db.mglbcBanChoThues.Count();
            success = db.mglbcBanChoThues.Where(p => p.isSyncQueueSuccess == true).Count();
            back = total - success;
            lbTongBanGhiBDS.Text = total.ToString();
            lbThanhCong.Text = success.ToString();
            lbNotSync.Text = back.ToString();

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvDuong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void itemSyncError_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Please confirm before proceed" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var obj = db.mglbcBanChoThues.Where(p => p.isSyncQueueSuccess == null || p.isSyncQueueSuccess == false && (p.isNotSync == null || p.isNotSync == false)).ToList();
                foreach (var objBC in obj)
                {
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
                        objitemkh.DiDong2 = objBC.KhachHang.DiDong2;
                        objitemkh.DiDong3 = objBC.KhachHang.DiDong3;
                        objitemkh.DiDong4 = objBC.KhachHang.DiDong4;
                        objitemkh.AppMaKH = objBC.KhachHang.MaKH.ToString();  // thiếu trường
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
                        objrealestate.TenDuong = db.Duongs.FirstOrDefault(p => p.MaDuong == objBC.MaDuong).TenDuong;
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
                        objrealestate.TenLD = db.LoaiDuongs.FirstOrDefault(p => p.MaLD == objBC.MaLD).TenLD;
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
                            if (objBC.MaBC == 0 || objBC.MaBC == null)
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
                        //var client = new RestClient("http://27.72.103.223:8085/api/RealEstate/sync-queue-realestate");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        var body = JsonConvert.SerializeObject(objrealestate);
                        request.AddParameter("application/json", body, ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);
                        Console.WriteLine(response.Content);
                        try
                        {
                            var objresult = JsonConvert.DeserializeObject<ResponseAPI>(response.Content);
                            if (objresult.status == 200) // đồng bộ thành công
                            {
                                objBC.isSyncQueueSuccess = true;
                                db.SubmitChanges();
                            }
                        }
                        catch
                        {
                        }

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

                    }

                }
                DialogBox.Infomation("Hoàn thành đồng bộ");
                return;
            }
            else
            {
                this.Close();
            }

        }
        public class ResponseAPI
        {
            public int? status { get; set; }
            public string message { get; set; }
        }

        private void itemRun_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Please confirm before proceed" + "\n" + "Do you want to Continue ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var obj = new List<mglbcBanChoThue>();
                int sl = (int)spinSoLuong.Value;
                if (sl > 0)
                {
                    obj = db.mglbcBanChoThues.Where(p => p.isSyncQueueSuccess == false || p.isSyncQueueSuccess == null && (p.isNotSync == null || p.isNotSync == false)).Take(sl).ToList();
                }
                else
                {
                    obj = db.mglbcBanChoThues.Where(p => p.isSyncQueueSuccess == false || p.isSyncQueueSuccess == null && (p.isNotSync == null || p.isNotSync == false)).ToList();
                }

                foreach (var objBC in obj)
                {
                    #region kafka
                    // push data kafka
                    var objrealestate = new mglbcBanChoThueModel();
                    // khách hàng
                    var objkhadd = new List<BEE.HoatDong.MGL.Ban.Models.MessageQueue.KhachHang>();
                    var objitemkh = new BEE.HoatDong.MGL.Ban.Models.MessageQueue.KhachHang();
                    objitemkh.HoTen = objBC.KhachHang.HoKH + " " + objBC.KhachHang.TenKH;
                    objitemkh.DiDong = objBC.KhachHang.DiDong;
                    objitemkh.AppMaKH = objBC.KhachHang.MaKH.ToString();  // thiếu trường
                    objitemkh.DiDong2 = objBC.KhachHang.DiDong2;
                    objitemkh.DiDong3 = objBC.KhachHang.DiDong3;
                    objitemkh.DiDong4 = objBC.KhachHang.DiDong4;
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
                        objimgadd.DuongDan = "https://hoaland-legacy-system.s3.ap-southeast-1.amazonaws.com/"+ i.DuongDan;
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
                        objrealestate.TenDuong = db.Duongs.FirstOrDefault(p => p.MaDuong == objBC.MaDuong).TenDuong;
                    }
                    catch 
                    {

                    }
                  
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
                    objrealestate.AppMaBC = objBC.MaBC.ToString();
                    objrealestate.isUpdate = true;
                    objrealestate.syncId = objBC.syncId;

                    var client = new RestClient("http://api-gw.hoaland.com.vn:8085/api/RealEstate/sync-queue-realestate");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    var body = JsonConvert.SerializeObject(objrealestate);
                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    Console.WriteLine(response.Content);
                    try
                    {
                        var objresult = JsonConvert.DeserializeObject<ResponseAPI>(response.Content);
                        if (objresult.status == 200) // đồng bộ thành công
                        {
                            var objupdate = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == objBC.MaBC);
                            objupdate.isSyncQueueSuccess = true;
                            db.SubmitChanges();
                        }
                    }
                    catch(Exception ex)
                    {
                    }

                    var log = new QueueHistory();
                    log.CreateDate = DateTime.Now;
                    log.Messger = body;
                    log.Response = response.Content;
                    db.QueueHistories.InsertOnSubmit(log);
                    db.SubmitChanges();
                    #endregion
                }
            }

            else

            {
                this.Close();
                //do something if NO
            }

        }
    }
}