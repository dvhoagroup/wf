using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using BEEREMA;
using System.Linq;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using System.Net;
using System.Data.Linq.SqlClient;
using BEE.HoatDong.MGL.Ban;

namespace BEE.HoatDong.BaoCao
{
    public partial class frmReportCall : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public frmReportCall()
        {
            InitializeComponent();
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 212;
            DataTable tblAction = o.SelectBy();
            itemExport.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 13:
                            itemExport.Enabled = true;
                            break;
                    }
                }
            }
        }

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 212);

            return o.SDB.SDBID;
        }
        //isSyncFileRecord = p.FileRecord == null ? false : true,
        void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        private void frmReportCall_Load(object sender, EventArgs e)
        {
            cboNhanVien.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.HoTen, IsCheck = false });

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(4);
            LoadPermission();
            LoadPermissionGhiAm();
            itemTuNgay.EditValue = new DateTime(2012, 5, 28);
            //grvReportCall.BestFitColumns();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db = new MasterDataContext();
            Report_Load();
        }

        void LoadPermissionGhiAm()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 175;
            DataTable tblAction = o.SelectBy();

            colGhiAm.Visible = false;
            colDongBoGhiAm.Visible = false;
            colTaiFileGhiAm.Visible = false;
            colDongBoThuCong.Visible = false;
            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {

                        case 93:
                            colGhiAm.Visible = true;
                            colDongBoGhiAm.Visible = true;
                            break;
                        case 94:
                            colTaiFileGhiAm.Visible = true;
                            break;
                        case 95:
                            colDongBoThuCong.Visible = true;
                            break;
                    }
                }
            }
        }
        void Report_Load()
        {
            var tuNgay = (DateTime?)itemTuNgay.EditValue ?? new DateTime(2012, 5, 28);
            var denNgay = (DateTime?)itemDenNgay.EditValue ?? DateTime.Now;
            var strMaNv = (itemNhanVien.EditValue ?? "").ToString().Replace(" ", "");
            var arrManv = "," + strMaNv + ",";
            int MaNV = Common.StaffID;
            var wait = DialogBox.WaitingForm();

            try
            {
                if (itemTuNgay.EditValue == null || itemDenNgay.EditValue == null)
                {
                    gcReportCall.DataSource = null;
                    return;
                }
                switch (GetAccessData())
                {
                    case 1://Tat ca
                        gcReportCall.DataSource = db.rpReportCallDetail(tuNgay, denNgay, arrManv, -1, -1, -1).Select(p => new
                        {

                            p.STT,
                            p.DiaChi,
                            p.StatusBefor,
                            p.StatusAfter,
                            p.HoTen,
                            p.TrangThaiGoi,
                            p.TieuDe,
                            p.NoiDung,
                            p.ThoiGianGoi,
                            p.NgayGoi,
                            p.GioGoi,
                            p.FileRecord,
                            isSyncFileRecord = p.isSyncFileRecord == 1 ? true : false,
                            p.ID

                        }).ToList();
                        break;
                    case 2://Theo phong ban 
                        gcReportCall.DataSource = db.rpReportCallDetail(tuNgay, denNgay, arrManv, -1, -1, Common.DepartmentID).Select(p => new
                        {

                            p.STT,
                            p.DiaChi,
                            p.StatusBefor,
                            p.StatusAfter,
                            p.HoTen,
                            p.TrangThaiGoi,
                            p.TieuDe,
                            p.NoiDung,
                            p.ThoiGianGoi,
                            p.NgayGoi,
                            p.GioGoi,
                            p.FileRecord,
                            isSyncFileRecord = p.isSyncFileRecord == 1 ? true : false,
                            p.ID

                        }).ToList();
                        break;
                    case 3://Theo nhom
                        gcReportCall.DataSource = db.rpReportCallDetail(tuNgay, denNgay, arrManv, -1, Common.GroupID, -1).Select(p => new
                        {

                            p.STT,
                            p.DiaChi,
                            p.StatusBefor,
                            p.StatusAfter,
                            p.HoTen,
                            p.TrangThaiGoi,
                            p.TieuDe,
                            p.NoiDung,
                            p.ThoiGianGoi,
                            p.NgayGoi,
                            p.GioGoi,
                            p.FileRecord,
                            isSyncFileRecord = p.isSyncFileRecord == 1 ? true : false,
                            p.ID

                        }).ToList();
                        break;
                    case 4://Theo nhan vien
                        gcReportCall.DataSource = db.rpReportCallDetail(tuNgay, denNgay, arrManv, MaNV, -1, -1).Select(p => new
                        {

                            p.STT,
                            p.DiaChi,
                            p.StatusBefor,
                            p.StatusAfter,
                            p.HoTen,
                            p.TrangThaiGoi,
                            p.TieuDe,
                            p.NoiDung,
                            p.ThoiGianGoi,
                            p.NgayGoi,
                            p.GioGoi,
                            p.FileRecord,
                            isSyncFileRecord = p.isSyncFileRecord == 1 ? true : false,
                            p.ID

                        }).ToList();
                        break;
                    default:
                        gcReportCall.DataSource = null;
                        break;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            finally
            {
                wait.Close();
            }

        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var saveFile = new SaveFileDialog())
            {
                string filePath = $"Báo cáo cuộc gọi chi tiết_{DateTime.Now.ToString("ddMMyyyyHHmm")}.xls";
                saveFile.Filter = $"Excel (2003)(.xls)|*.xls";
                saveFile.FileName = filePath;
                if (saveFile.ShowDialog() != DialogResult.Cancel)
                {
                    var fileExTen = new FileInfo(filePath).Extension;
                    switch (fileExTen)
                    {
                        case ".xls":
                            gcReportCall.ExportToXls(filePath);
                            break;
                    }

                    Process.Start(filePath);
                }
            }

        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        public class itemRecordIncall
        {
            public string type { get; set; }
            public DateTime? timespan { get; set; }
            public string phone { get; set; }
            public string line { get; set; }
            public string Source { get; set; }

        }
        public class itemRecordOut
        {
            public string type { get; set; }
            public DateTime? timespan { get; set; }
            public string line { get; set; }
            public string phone { get; set; }
            public string Source { get; set; }

        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        private void btnGhiAm_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var id = (int?)grvReportCall.GetFocusedRowCellValue("ID");
          
            try
            {
                var FileRecord = (sender as ButtonEdit).Text ?? "";
                if (FileRecord.Trim() == "") // sẽ đồng bộ dữ liệu
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

                                var url = path;
                                var frm = new frmViewMedia();
                                frm.filepath = url;
                                frm.ShowDialog();

                            }
                            else
                            {
                                DialogBox.Error("Không tồn tại file ghi âm, vui lòng kiểm tra lại");
                                return;
                            }

                        }
                        else
                        {
                            objresultRecordIn = directoriesIn.Where(p => p.phone == objn.DiDong && p.line == objn.LineNumber).ToList();
                            objresultRecordIn = objresultRecordIn.Where(p => SqlMethods.DateDiffMinute(Convert.ToDateTime(objn.StartDate).AddSeconds(-15), p.timespan) >= 0 & SqlMethods.DateDiffSecond(p.timespan, objn.Enddate) >= 0).ToList();

                            if (objresultRecordIn.Count > 0)
                            {
                                objnkupdate.FileRecord = objresultRecordIn.OrderByDescending(p => p.timespan).FirstOrDefault().Source;
                                db.SubmitChanges();
                            }
                            else
                            {
                                DialogBox.Error("Không tồn tại file ghi âm, vui lòng kiểm tra lại");
                                return;
                            }
                        }
                        // cập nhật vào db xong



                    }// objn


                }

                else
                {
                    // lấy luôn file và open
                    // open file media

                    var frm = new frmViewMedia();
                    frm.filepath = FileRecord;
                    frm.ShowDialog();


                }
            }
            catch (Exception exx)
            {
                DialogBox.Error("lỗi: " + exx.Message);
                return;

            }

            //try
            //{
            //    var FileRecord = (sender as ButtonEdit).Text ?? "";
            //    if (FileRecord.Trim() == "") // sẽ đồng bộ dữ liệu
            //    {



            //        var objn = (from nk in db.mglbcNhatKyXuLies
            //                    join bc in db.mglbcBanChoThues on nk.MaBC equals bc.MaBC
            //                    join kh in db.KhachHangs on bc.MaKH equals kh.MaKH
            //                    join nv in db.NhanViens on nk.MaNVG equals nv.MaNV
            //                    join line in db.voipLineConfigs on nv.MaNV equals line.MaNV
            //                    where nk.ID == id
            //                    select new
            //                    {
            //                        nk.DiDong,
            //                        line.LineNumber,
            //                        nk.StartDate,
            //                        nk.EndDate,
            //                        nk.LoaiCG,
            //                        nk.NgayXL

            //                    }
            //                    ).FirstOrDefault();

            //        if (objn != null)
            //        {
            //            var objconfig = db.tblConfigs.FirstOrDefault(p => p.TypeID == 3);

            //            var datepath = string.Format("{0:yyyy-MM}", objn.NgayXL);
            //            string ftppath = objconfig.FtpUrl + datepath;
            //            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftppath);
            //            ftpRequest.Credentials = new NetworkCredential(objconfig.FtpUser, it.CommonCls.GiaiMa(objconfig.FtpPass));
            //            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            //            FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
            //            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            //            List<itemRecordIncall> directoriesIn = new List<itemRecordIncall>();

            //            List<itemRecordOut> directoriesOut = new List<itemRecordOut>();
            //            string line = streamReader.ReadLine();

            //            while (!string.IsNullOrEmpty(line))
            //            {

            //                if (line.Contains("6501") == false)
            //                {
            //                    var arrayfile = line.Replace(datepath + "/", "").Split('-');
            //                    // kiểm tra định dạnh file


            //                    if (arrayfile[3].Replace(".wav", "").Length > 9 && objn.LoaiCG == 0) // số điện thoải phải > 9, cuộc gọi ra
            //                    {
            //                        var itemfile = new itemRecordOut();
            //                        itemfile.type = arrayfile[0]; // 
            //                        itemfile.timespan = UnixTimeStampToDateTime(Convert.ToDouble(arrayfile[1])); // 
            //                        itemfile.line = arrayfile[2]; // 
            //                        itemfile.phone = arrayfile[3].Replace(".wav", ""); // 
            //                        itemfile.Source = line;
            //                        directoriesOut.Add(itemfile);

            //                    }

            //                    else if (arrayfile[3].Replace(".wav", "").Length <= 4 && objn.LoaiCG == null)  // gọi vào line ở cuối 
            //                    {
            //                        var itemfile = new itemRecordIncall();
            //                        itemfile.type = arrayfile[0]; // 
            //                        itemfile.timespan = UnixTimeStampToDateTime(Convert.ToDouble(arrayfile[1])); // 
            //                        itemfile.phone = arrayfile[2]; // 
            //                        itemfile.line = arrayfile[3].Replace(".wav", ""); // 
            //                        itemfile.Source = line;
            //                        directoriesIn.Add(itemfile);
            //                    }
            //                }
            //                line = streamReader.ReadLine();
            //            }
            //            streamReader.Close();
            //            // kết thúc lặp
            //            var objresultRecordout = new List<itemRecordOut>();
            //            var objresultRecordIn = new List<itemRecordIncall>();
            //            var objnkupdate = db.mglbcNhatKyXuLies.FirstOrDefault(p => p.ID == id);
            //            if (objn.LoaiCG == 0)
            //            {
            //                //    SqlMethods.DateDiffDay((DateTime)itemTuNgay.EditValue, p.NgayGD) >= 0 &
            //                //    SqlMethods.DateDiffDay(p.NgayGD, (DateTime)itemDenNgay.EditValue) >= 0)
            //                objresultRecordout = directoriesOut.Where(p => p.phone == objn.DiDong && p.line == objn.LineNumber).ToList();
            //                objresultRecordout = objresultRecordout.Where(p => SqlMethods.DateDiffSecond(objn.StartDate, p.timespan) >= 0 & SqlMethods.DateDiffSecond(p.timespan, objn.EndDate) >= 0).ToList();
            //                if (objresultRecordout.Count > 0)
            //                {
            //                    var path = objresultRecordout.FirstOrDefault().Source;
            //                    objnkupdate.FileRecord = path;
            //                    db.SubmitChanges();

            //                    // open file media

            //                    var url = path;
            //                    var frm = new frmViewMedia();
            //                    frm.filepath = url;
            //                    frm.ShowDialog();

            //                }
            //                else
            //                {
            //                    DialogBox.Error("Không tồn tại file ghi âm, vui lòng kiểm tra lại");
            //                    return;
            //                }

            //            }
            //            else
            //            {
            //                objresultRecordIn = objresultRecordIn.Where(p => SqlMethods.DateDiffSecond(objn.StartDate, p.timespan) >= 0 & SqlMethods.DateDiffSecond(p.timespan, objn.EndDate) >= 0).ToList();

            //                if (objresultRecordIn.Count > 0)
            //                {
            //                    objnkupdate.FileRecord = objresultRecordIn.FirstOrDefault().Source;
            //                    db.SubmitChanges();
            //                }
            //                else
            //                {
            //                    DialogBox.Error("Không tồn tại file ghi âm, vui lòng kiểm tra lại");
            //                    return;
            //                }
            //            }
            //            // cập nhật vào db xong



            //        }// objn


            //    }

            //    else
            //    {
            //        // lấy luôn file và open
            //        // open file media

            //        var frm = new frmViewMedia();
            //        frm.filepath = FileRecord;
            //        frm.ShowDialog();


            //    }
            //}
            //catch (Exception exx)
            //{
            //    DialogBox.Error("lỗi: " + exx.Message);
            //    return;

            //}
        }

        private void itemTaiFileGhiAm_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                var filepath = grvReportCall.GetFocusedRowCellValue("FileRecord");
                if (filepath == null || filepath == "")
                {
                    DialogBox.Error("Không tồn tại file ghi âm, vui lòng kiểm tra lại");
                    return;
                }

                var objconfig = db.tblConfigs.FirstOrDefault(p => p.TypeID == 3);
                string ftppath = objconfig.FtpUrl + filepath;
                string user = objconfig.FtpUser;
                string pass = it.CommonCls.GiaiMa(objconfig.FtpPass);

                WebClient client = new WebClient();
                client.Credentials = new NetworkCredential(user, pass);


                SaveFileDialog savefile = new SaveFileDialog();
                savefile.Title = "Lưu file ghi âm";
                savefile.Filter = "Wav Files|*.wav";
                savefile.ShowDialog();
                if (savefile.FileName != "")
                {
                    try
                    {
                        client.DownloadFile(ftppath, savefile.FileName);
                        DialogBox.Infomation("Hoàn thành tải file");
                    }
                    catch (Exception ex)
                    {

                        DialogBox.Error("Lỗi, vui lòng thử lại:" + ex.Message);
                    }

                }





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void itemSyncALL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var id = (int?)grvReportCall.GetFocusedRowCellValue("ID");

            //try
            //{
            //    var FileRecord = (sender as ButtonEdit).Text ?? "";
            //    if (FileRecord.Trim() == "") // sẽ đồng bộ dữ liệu
            //    {
            //        var objn = (from nk in db.mglbcNhatKyXuLies
            //                    join bc in db.mglbcBanChoThues on nk.MaBC equals bc.MaBC
            //                    join kh in db.KhachHangs on bc.MaKH equals kh.MaKH
            //                    join nv in db.NhanViens on nk.MaNVG equals nv.MaNV
            //                    join line in db.voipLineConfigs on nv.MaNV equals line.MaNV
            //                    where nk.FileRecord != null
            //                    select new
            //                    {
            //                        nk.DiDong,
            //                        line.LineNumber,
            //                        nk.StartDate,
            //                        nk.EndDate,
            //                        nk.LoaiCG,
            //                        nk.NgayXL

            //                    }
            //                    ).ToList();

            //        if (objn.Count > 0)
            //        {
            //            var objconfig = db.tblConfigs.FirstOrDefault(p => p.TypeID == 3);

            //            var datepath = string.Format("{0:yyyy-MM}", objn.NgayXL);
            //            string ftppath = objconfig.FtpUrl + datepath;
            //            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftppath);
            //            ftpRequest.Credentials = new NetworkCredential(objconfig.FtpUser, it.CommonCls.GiaiMa(objconfig.FtpPass));
            //            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            //            FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
            //            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            //            List<itemRecordIncall> directoriesIn = new List<itemRecordIncall>();
            //            List<itemRecordOut> directoriesOut = new List<itemRecordOut>();
            //            string line = streamReader.ReadLine();

            //            while (!string.IsNullOrEmpty(line))
            //            {

            //                if (line.Contains("6501") == false)
            //                {
            //                    var arrayfile = line.Replace(datepath + "/", "").Split('-');
            //                    // kiểm tra định dạnh file


            //                    if (arrayfile[3].Replace(".wav", "").Length > 9 && objn.LoaiCG == 0) // số điện thoải phải > 9, cuộc gọi ra
            //                    {
            //                        var itemfile = new itemRecordOut();
            //                        itemfile.type = arrayfile[0]; // 
            //                        itemfile.timespan = UnixTimeStampToDateTime(Convert.ToDouble(arrayfile[1])); // 
            //                        itemfile.line = arrayfile[2]; // 
            //                        itemfile.phone = arrayfile[3].Replace(".wav", ""); // 
            //                        itemfile.Source = line;
            //                        directoriesOut.Add(itemfile);

            //                    }

            //                    else if (arrayfile[3].Replace(".wav", "").Length <= 4 && objn.LoaiCG == null)  // gọi vào line ở cuối 
            //                    {
            //                        var itemfile = new itemRecordIncall();
            //                        itemfile.type = arrayfile[0]; // 
            //                        itemfile.timespan = UnixTimeStampToDateTime(Convert.ToDouble(arrayfile[1])); // 
            //                        itemfile.phone = arrayfile[2]; // 
            //                        itemfile.line = arrayfile[3].Replace(".wav", ""); // 
            //                        itemfile.Source = line;
            //                        directoriesIn.Add(itemfile);
            //                    }
            //                }
            //                line = streamReader.ReadLine();
            //            }
            //            streamReader.Close();
            //            // kết thúc lặp
            //            var objresultRecordout = new List<itemRecordOut>();
            //            var objresultRecordIn = new List<itemRecordIncall>();
            //            var objnkupdate = db.mglbcNhatKyXuLies.FirstOrDefault(p => p.ID == id);
            //            if (objn.LoaiCG == 0)
            //            {
            //                //    SqlMethods.DateDiffDay((DateTime)itemTuNgay.EditValue, p.NgayGD) >= 0 &
            //                //    SqlMethods.DateDiffDay(p.NgayGD, (DateTime)itemDenNgay.EditValue) >= 0)
            //                objresultRecordout = directoriesOut.Where(p => p.phone == objn.DiDong && p.line == objn.LineNumber).ToList();
            //                objresultRecordout = objresultRecordout.Where(p => SqlMethods.DateDiffSecond(objn.StartDate, p.timespan) >= 0 & SqlMethods.DateDiffSecond(p.timespan, objn.EndDate) >= 0).ToList();
            //                if (objresultRecordout.Count > 0)
            //                {
            //                    var path = objresultRecordout.FirstOrDefault().Source;
            //                    objnkupdate.FileRecord = path;
            //                    db.SubmitChanges();

            //                    // open file media

            //                    var url = path;
            //                    var frm = new frmViewMedia();
            //                    frm.filepath = url;
            //                    frm.ShowDialog();

            //                }
            //                else
            //                {
            //                    DialogBox.Error("Không tồn tại file ghi âm, vui lòng kiểm tra lại");
            //                    return;
            //                }

            //            }
            //            else
            //            {
            //                objresultRecordIn = objresultRecordIn.Where(p => SqlMethods.DateDiffSecond(objn.StartDate, p.timespan) >= 0 & SqlMethods.DateDiffSecond(p.timespan, objn.EndDate) >= 0).ToList();

            //                if (objresultRecordIn.Count > 0)
            //                {
            //                    objnkupdate.FileRecord = objresultRecordIn.FirstOrDefault().Source;
            //                    db.SubmitChanges();
            //                }
            //                else
            //                {
            //                    DialogBox.Error("Không tồn tại file ghi âm, vui lòng kiểm tra lại");
            //                    return;
            //                }
            //            }
            //            // cập nhật vào db xong



            //        }// objn


            //    }

            //    else
            //    {
            //        // lấy luôn file và open
            //        // open file media

            //        var frm = new frmViewMedia();
            //        frm.filepath = FileRecord;
            //        frm.ShowDialog();


            //    }
            //}
            //catch (Exception exx)
            //{
            //    DialogBox.Error("lỗi: " + exx.Message);
            //    return;

            //}
        }

        private void btnAsyncManual_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
           
            string dd = "";
            var id = (int?)grvReportCall.GetFocusedRowCellValue("ID");
            try
            {
                dd = grvReportCall.GetFocusedRowCellValue("DiDong").ToString();

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
    }
}