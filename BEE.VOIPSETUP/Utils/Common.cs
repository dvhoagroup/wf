using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEE.ThuVien;
using System.Net;
using System.IO;
using static BEE.HoatDong.BaoCao.frmReportCall;
using System.Data.Linq.SqlClient;

namespace BEE.VOIPSETUP.Utils
{
    public class Common
    {

        public static void GetFile(int? id)
        {
            MasterDataContext db = new MasterDataContext();
            try
            {
                var objn = (from nk in db.mglbcNhatKyXuLies
                            join bc in db.mglbcBanChoThues on nk.MaBC equals bc.MaBC
                            join kh in db.KhachHangs on bc.MaKH equals kh.MaKH
                            join nv in db.NhanViens on nk.MaNVG equals nv.MaNV
                            join line in db.voipLineConfigs on nv.MaNV equals line.MaNV
                            where nk.ID == id
                            select new
                            {
                                nk.DiDong,
                                line.LineNumber,
                                nk.StartDate,
                                nk.Enddate,
                                nk.LoaiCG,
                                nk.NgayXL

                            }
                            ).FirstOrDefault();

                if (objn != null)
                {
                    var objconfig = db.tblConfigs.FirstOrDefault(p => p.TypeID == 3);

                    var datepath = string.Format("{0:yyyy-MM}", objn.NgayXL);
                    string ftppath = objconfig.FtpUrl + datepath;
                    FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftppath);
                    ftpRequest.Credentials = new NetworkCredential(objconfig.FtpUser, it.CommonCls.GiaiMa(objconfig.FtpPass));
                    //ftpRequest.Timeout = Timeout.Infinite;
                    //ftpRequest.KeepAlive = true;
                    ftpRequest.Timeout = 60000;
                    ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                    FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                    StreamReader streamReader = new StreamReader(response.GetResponseStream());
                    List<itemRecordIncall> directoriesIn = new List<itemRecordIncall>();

                    List<itemRecordOut> directoriesOut = new List<itemRecordOut>();
                    string line = streamReader.ReadLine();

                    DateTime timelq = (DateTime)objn.StartDate;
                    while (!string.IsNullOrEmpty(line))
                    {

                        if (line.Contains("6501") == false)
                        {
                            try
                            {
                                var arrayfile = line.Replace(datepath + "/", "").Split('-');
                                // kiểm tra định dạnh file


                                if (arrayfile[3].Replace(".wav", "").Length > 9 && objn.LoaiCG == 0) // số điện thoải phải > 9, cuộc gọi ra
                                {
                                    DateTime time = UnixTimeStampToDateTime(Convert.ToDouble(arrayfile[1])); // 

                                    TimeSpan tp = (DateTime)objn.StartDate - time;
                                    if (time.Year == timelq.Year && time.Month == timelq.Month && time.Day == timelq.Day) // dung ngay
                                    {
                                        try
                                        {
                                            var itemfile = new itemRecordOut();
                                            itemfile.type = arrayfile[0]; // 
                                            itemfile.timespan = time;
                                            itemfile.line = arrayfile[2]; // 
                                            itemfile.phone = arrayfile[3].Replace(".wav", ""); // 
                                            itemfile.Source = line;
                                            directoriesOut.Add(itemfile);
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                    }



                                }

                                else if (arrayfile[3].Replace(".wav", "").Length <= 4 && objn.LoaiCG == null)  // gọi vào line ở cuối 
                                {
                                    DateTime time = UnixTimeStampToDateTime(Convert.ToDouble(arrayfile[1])); // 
                                    TimeSpan tp = (DateTime)objn.StartDate - time;
                                    var t = tp.Days;
                                    var tday = (int)tp.TotalDays;
                                    //var a = arrayfile[2];
                                    //if(a == "0919263686")
                                    //{
                                    //    var c = "";
                                    //}

                                    if (time.Year == timelq.Year && time.Month == timelq.Month && time.Day == timelq.Day) // dung ngay
                                    {
                                        try
                                        {
                                            var itemfile = new itemRecordIncall();
                                            itemfile.type = arrayfile[0]; // 
                                            itemfile.timespan = UnixTimeStampToDateTime(Convert.ToDouble(arrayfile[1])); // 
                                            itemfile.phone = arrayfile[2]; // 
                                            itemfile.line = arrayfile[3].Replace(".wav", ""); // 
                                            itemfile.Source = line;
                                            directoriesIn.Add(itemfile);
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                            }

                        }
                        line = streamReader.ReadLine();
                    }
                    streamReader.Close();
                    // kết thúc lặp
                    var objresultRecordout = new List<itemRecordOut>();
                    var objresultRecordIn = new List<itemRecordIncall>();
                    var objnkupdate = db.mglbcNhatKyXuLies.FirstOrDefault(p => p.ID == id);
                    if (objn.LoaiCG == 0)
                    {
                        //    SqlMethods.DateDiffDay((DateTime)itemTuNgay.EditValue, p.NgayGD) >= 0 &
                        //    SqlMethods.DateDiffDay(p.NgayGD, (DateTime)itemDenNgay.EditValue) >= 0)
                        objresultRecordout = directoriesOut.Where(p => p.phone == objn.DiDong && p.line == objn.LineNumber).ToList();
                        objresultRecordout = objresultRecordout.Where(p => SqlMethods.DateDiffMinute(objn.StartDate, p.timespan) >= 0 & SqlMethods.DateDiffMinute(p.timespan, objn.Enddate) >= 0).ToList();
                        if (objresultRecordout.Count > 0)
                        {
                            var path = objresultRecordout.FirstOrDefault().Source;
                            objnkupdate.FileRecord = path;
                            db.SubmitChanges();

                            // open file media

                            //var url = path;
                            //var frm = new frmViewMedia();
                            //frm.filepath = url;
                            //frm.ShowDialog();

                        }
                        else
                        {
                            /// DialogBox.Error("Không tồn tại file ghi âm, vui lòng kiểm tra lại");
                            return;
                        }

                    }
                    else
                    {
                        objresultRecordIn = directoriesIn.Where(p => p.phone == objn.DiDong && p.line == objn.LineNumber).ToList();
                        objresultRecordIn = objresultRecordIn.Where(p => SqlMethods.DateDiffMinute(Convert.ToDateTime(objn.StartDate).AddSeconds(-15), p.timespan) >= 0 & SqlMethods.DateDiffMinute(p.timespan, objn.Enddate) >= 0).ToList();

                        if (objresultRecordIn.Count > 0)
                        {
                            objnkupdate.FileRecord = objresultRecordIn.FirstOrDefault().Source;
                            db.SubmitChanges();
                        }
                        else
                        {
                            //DialogBox.Error("Không tồn tại file ghi âm, vui lòng kiểm tra lại");
                            return;
                        }
                    }
                    // cập nhật vào db xong



                }// objn





            }
            catch (Exception exx)
            {

                return;

            }
        }
        public class DiDongModel
        {
            public int? MaKH { get; set; }
            public string DiDong { get; set; }
            public string DiDong2 { get; set; }
            public string DiDong3 { get; set; }
            public string DiDong4 { get; set; }
            public string DTDD { get; set; }

        }

        public static List<itemRecordIncall> GetALLFile(int? id)
        {
            var lst = new List<itemRecordIncall>();
            MasterDataContext db = new MasterDataContext();
            try
            {
                var objn = (from nk in db.mglbcNhatKyXuLies
                            join bc in db.mglbcBanChoThues on nk.MaBC equals bc.MaBC
                            join kh in db.KhachHangs on bc.MaKH equals kh.MaKH
                            join nv in db.NhanViens on nk.MaNVG equals nv.MaNV
                            join line in db.voipLineConfigs on nv.MaNV equals line.MaNV
                            where nk.ID == id
                            select new
                            {
                                nk.DiDong,
                                line.LineNumber,
                                nk.StartDate,
                                nk.Enddate,
                                nk.LoaiCG,
                                nk.NgayXL

                            }
                            ).FirstOrDefault();

                if (objn != null)
                {
                    var objconfig = db.tblConfigs.FirstOrDefault(p => p.TypeID == 3);

                    var datepath = string.Format("{0:yyyy-MM}", objn.NgayXL);
                    string ftppath = objconfig.FtpUrl + datepath;
                    FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftppath);
                    ftpRequest.Credentials = new NetworkCredential(objconfig.FtpUser, it.CommonCls.GiaiMa(objconfig.FtpPass));
                    //ftpRequest.Timeout = Timeout.Infinite;
                    //ftpRequest.KeepAlive = true;
                    ftpRequest.Timeout = 60000;
                    ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                    FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                    StreamReader streamReader = new StreamReader(response.GetResponseStream());
                    List<itemRecordIncall> directoriesIn = new List<itemRecordIncall>();

                    string line = streamReader.ReadLine();

                    DateTime timelq = (DateTime)objn.StartDate;
                    while (!string.IsNullOrEmpty(line))
                    {

                        if (line.Contains("6501") == false)
                        {
                            try
                            {
                                var arrayfile = line.Replace(datepath + "/", "").Split('-');
                                // kiểm tra định dạnh file


                                if (arrayfile[3].Replace(".wav", "").Length > 9 && objn.LoaiCG == 0) // số điện thoải phải > 9, cuộc gọi ra
                                {
                                    DateTime time = UnixTimeStampToDateTime(Convert.ToDouble(arrayfile[1])); // 

                                    TimeSpan tp = (DateTime)objn.StartDate - time;
                                    if (time.Year == timelq.Year && time.Month == timelq.Month && time.Day == timelq.Day) // dung ngay
                                    {
                                        try
                                        {
                                            var itemfile = new itemRecordIncall();
                                            itemfile.type = arrayfile[0]; // 
                                            itemfile.timespan = time;
                                            itemfile.line = arrayfile[2]; // 
                                            itemfile.phone = arrayfile[3].Replace(".wav", ""); // 
                                            itemfile.Source = line;
                                            directoriesIn.Add(itemfile);
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                    }



                                }

                                else if (arrayfile[3].Replace(".wav", "").Length <= 4 && objn.LoaiCG == null)  // gọi vào line ở cuối 
                                {
                                    DateTime time = UnixTimeStampToDateTime(Convert.ToDouble(arrayfile[1])); // 
                                    TimeSpan tp = (DateTime)objn.StartDate - time;
                                    var t = tp.Days;
                                    var tday = (int)tp.TotalDays;
                                    //var a = arrayfile[2];
                                    //if(a == "0919263686")
                                    //{
                                    //    var c = "";
                                    //}

                                    if (time.Year == timelq.Year && time.Month == timelq.Month && time.Day == timelq.Day) // dung ngay
                                    {
                                        try
                                        {
                                            var itemfile = new itemRecordIncall();
                                            itemfile.type = arrayfile[0]; // 
                                            itemfile.timespan = UnixTimeStampToDateTime(Convert.ToDouble(arrayfile[1])); // 
                                            itemfile.phone = arrayfile[2]; // 
                                            itemfile.line = arrayfile[3].Replace(".wav", ""); // 
                                            itemfile.Source = line;
                                            directoriesIn.Add(itemfile);
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                            }

                        }
                        line = streamReader.ReadLine();
                    }
                    streamReader.Close();
                    // kết thúc lặp
                    var objresultRecordIn = new List<itemRecordIncall>();
                    objresultRecordIn = directoriesIn.Where(p => p.phone == objn.DiDong && p.line == objn.LineNumber).ToList();
                    lst.AddRange(objresultRecordIn);

                }// objn
                return lst;

            }
            catch (Exception ex)
            {

                return null;

            }
        }
        public static List<itemRecordIncall> GetALLFileDiDongNull(int? id)
        {
            var lst = new List<itemRecordIncall>();
            var dd = new List<DiDongModel>();
            MasterDataContext db = new MasterDataContext();
            try
            {
                var objn = (from nk in db.mglbcNhatKyXuLies
                            join bc in db.mglbcBanChoThues on nk.MaBC equals bc.MaBC
                            join kh in db.KhachHangs on bc.MaKH equals kh.MaKH
                            join nv in db.NhanViens on nk.MaNVG equals nv.MaNV
                            join line in db.voipLineConfigs on nv.MaNV equals line.MaNV

                            where nk.ID == id
                            select new
                            {

                                nk.DiDong,
                                line.LineNumber,
                                nk.StartDate,
                                nk.Enddate,
                                nk.LoaiCG,
                                nk.NgayXL

                            }
                            ).FirstOrDefault();


                var objDiDong = (from nk in db.mglbcNhatKyXuLies
                                 join bc in db.mglbcBanChoThues on nk.MaBC equals bc.MaBC
                                 join kh in db.KhachHangs on bc.MaKH equals kh.MaKH
                                 join nv in db.NhanViens on nk.MaNVG equals nv.MaNV
                                 join line in db.voipLineConfigs on nv.MaNV equals line.MaNV
                                 join nlh in db.NguoiDaiDiens on kh.MaKH equals nlh.MaKH into nguoilh
                                 from nlh in nguoilh.DefaultIfEmpty()
                                 where nk.ID == id
                                 select new DiDongModel
                                 {
                                     MaKH = kh.MaKH,
                                     DiDong = kh.DiDong,
                                     DiDong2 = kh.DiDong2,
                                     DiDong3 = kh.DiDong3,
                                     DiDong4 = kh.DiDong4,
                                     DTDD = nlh.DTDD
                                 }
                            ).ToList();

                if (objn != null)
                {
                    var objconfig = db.tblConfigs.FirstOrDefault(p => p.TypeID == 3);

                    var datepath = string.Format("{0:yyyy-MM}", objn.NgayXL);
                    string ftppath = objconfig.FtpUrl + datepath;
                    FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftppath);
                    ftpRequest.Credentials = new NetworkCredential(objconfig.FtpUser, it.CommonCls.GiaiMa(objconfig.FtpPass));
                    //ftpRequest.Timeout = Timeout.Infinite;
                    //ftpRequest.KeepAlive = true;
                    ftpRequest.Timeout = 60000;
                    ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                    FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                    StreamReader streamReader = new StreamReader(response.GetResponseStream());
                    List<itemRecordIncall> directoriesIn = new List<itemRecordIncall>();

                    string line = streamReader.ReadLine();

                    DateTime timelq = (DateTime)objn.StartDate;
                    while (!string.IsNullOrEmpty(line))
                    {

                        if (line.Contains("6501") == false)
                        {
                            if(line.Contains("214") == true)
                            {
                                var asddfsfdfs = "";
                            }
                            try
                            {
                                var arrayfile = line.Replace(datepath + "/", "").Split('-');
                                // kiểm tra định dạnh file


                                if (arrayfile[3].Replace(".wav", "").Length > 9) // số điện thoải phải > 9, cuộc gọi ra
                                {
                                    DateTime time = UnixTimeStampToDateTime(Convert.ToDouble(arrayfile[1])); // 

                                    TimeSpan tp = (DateTime)objn.StartDate - time;
                                    if (time.Year == timelq.Year && time.Month == timelq.Month) // dung ngay
                                    {
                                        try
                                        {
                                            var itemfile = new itemRecordIncall();
                                            itemfile.type = arrayfile[0]; // 
                                            itemfile.timespan = time;
                                            itemfile.line = arrayfile[2]; // 
                                            itemfile.phone = arrayfile[3].Replace(".wav", ""); // 
                                            itemfile.Source = line;
                                            directoriesIn.Add(itemfile);
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                    }



                                }

                                 if (arrayfile[3].Replace(".wav", "").Length <= 4)  // gọi vào line ở cuối 
                                {
                                    DateTime time = UnixTimeStampToDateTime(Convert.ToDouble(arrayfile[1])); // 
                                    TimeSpan tp = (DateTime)objn.StartDate - time;
                                    var t = tp.Days;
                                    var tday = (int)tp.TotalDays;

                                    if (time.Year == timelq.Year && time.Month == timelq.Month) // dung ngay
                                    {
                                        try
                                        {
                                            var itemfile = new itemRecordIncall();
                                            itemfile.type = arrayfile[0]; // 
                                            itemfile.timespan = UnixTimeStampToDateTime(Convert.ToDouble(arrayfile[1])); // 
                                            itemfile.phone = arrayfile[2]; // 
                                            itemfile.line = arrayfile[3].Replace(".wav", ""); // 
                                            itemfile.Source = line;
                                            directoriesIn.Add(itemfile);
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                            }

                        }
                        line = streamReader.ReadLine();
                    }
                    streamReader.Close();
                    // kết thúc lặp
                    var objresultRecordIn = new List<itemRecordIncall>();
                    objresultRecordIn = directoriesIn.Where(p => p.line == objn.LineNumber).ToList();
                    lst.AddRange(objresultRecordIn);

                }// objn
                var objend = new List<itemRecordIncall>();
                foreach (var i in objDiDong)
                {
                    var item = new itemRecordIncall();
                    var objcheck = lst.Where(p => p.phone == i.DiDong || p.phone == i.DiDong2 || p.phone == i.DiDong3 || p.phone == i.DiDong4).ToList();
                    objend.AddRange(objcheck);
                }

                return objend;

            }
            catch (Exception ex)
            {

                return null;

            }
        }

    }
}
