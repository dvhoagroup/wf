using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Threading;
using BEE.ThuVien;

namespace BEE.ThuVien
{
    public static class ManagerSchedule
    {
        public static DateTime? NgayTinh, NgayBanGiao;
        public static decimal? LaiSuat = 0.05M;
        /// <summary>
        /// DepositID is MaPGC
        /// </summary>
        public static void Reset(int DepositID)
        {
            MasterDataContext db;
            List<ItemData> lstLichTT = new List<ItemData>();
            db = new MasterDataContext();
            var lstLTTUp = db.pgcLichThanhToans.Where(p => p.MaPGC == DepositID).OrderBy(p => p.DotTT).ToList();
            var lstLTT = lstLTTUp.AsEnumerable()
                .Select(p => new ItemLichTT()
                {
                    ID = p.ID,
                    DotTT = p.DotTT ?? 0,
                    SoTien = p.SoTien ?? 0,
                    NgayTT = p.NgayTT,
                    DaThu = 0M,
                    LaiMuon = p.LaiMuon ?? (p.pgcPhieuGiuCho.LaiMuonKH ?? 0),
                    SoNgayGiam = p.SoNgayGiam ?? 0
                }).ToList();

            var lstPTUp = db.pgcPhieuThus.Where(p => p.MaPGC == DepositID).OrderBy(p => p.NgayThu).ToList();
            foreach (var item in lstPTUp)
            {
                var list = lstLTT.Where(p => p.DaThu < p.SoTien).ToList();
                var sotien = item.TienTT ?? 0;
                var tien = 0M;
                int i = 0;
                if (list.Count > 0)
                {
                    try
                    {
                        while (sotien > 0)
                        {
                            var objltt = list[i++];
                            ItemData temp = new ItemData();
                            temp.ID = objltt.ID;
                            temp.NgayTT = objltt.NgayTT;
                            temp.MaPT = item.MaPT;
                            temp.NgayThu = item.NgayThu;
                            temp.LaiMuonDot = item.LaiMuon;
                            temp.SoNgayGiam = objltt.SoNgayGiam;
                            if (sotien >= (objltt.SoTien - objltt.DaThu))
                            {
                                tien = (objltt.SoTien - objltt.DaThu);
                                sotien = sotien - tien;
                                objltt.DaThu = objltt.SoTien;
                            }
                            else
                            {
                                tien = sotien;
                                objltt.DaThu = objltt.DaThu + tien;
                                sotien = 0;
                            }
                            temp.SoTien = tien;
                            temp.LaiSuat = TinhLai(temp.NgayTT, temp.NgayThu, tien, objltt.LaiMuon, temp.SoNgayGiam);
                            lstLichTT.Add(temp);
                        }
                    }
                    catch { }
                }
            }

            lstLTTUp.ForEach(p => p.LaiSuat = 0M);

            db.SubmitChanges();
            decimal tienLai = 0M;
            foreach (var item in lstLichTT)
            {
                try
                {
                    var temp = lstLTTUp.Single(p => p.ID == item.ID);
                    temp.LaiSuat += item.LaiSuat;
                    tienLai += item.LaiSuat;
                }
                catch { }
            }

            //
            var objPGC = db.pgcPhieuGiuChos.SingleOrDefault(p => p.MaPGC == DepositID);
            if (objPGC != null)
            {
                objPGC.TienLai = tienLai;
            }
            db.SubmitChanges();
        }

        public static void ResetAll()
        {
            using (var db = new MasterDataContext())
            {
                var lstPGC = db.pgcPhieuGiuChos.Select(p => new { p.MaPGC }).ToList();

                for (int lk = 0; lk < (lstPGC.Count / 10) + 1; lk++)
                {
                    for (int i = 0; i < 10 & (lk * 10 + i) < lstPGC.Count; i++)
                    {
                        Reset(lstPGC[lk * 10 + i].MaPGC);
                    }
                    Thread.Sleep(50);
                }
            }
        }

        static decimal TinhLai(DateTime? DateFrom, DateTime? DateTo, decimal SoTien, decimal TyLe, int SoNgayGiam)
        {
            try
            {
                var songay = SqlMethods.DateDiffDay(DateFrom, DateTo) ?? 0;
                songay -= SoNgayGiam;
                if (songay < 0) songay = 0;
                return Math.Round(songay * SoTien * TyLe / 100, 2, MidpointRounding.AwayFromZero);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// DepositID is MaPGC
        /// </summary>
        public static List<ItemSchedule> GetListSchedule(int DepositID)
        {
            var listResut = new List<ItemSchedule>();
            MasterDataContext db;

            List<ItemData> lstLichTT = new List<ItemData>();
            db = new MasterDataContext();
            var listCN = db.pgcLichThanhToan_Select(DepositID).ToList();

            var lstLTTUp = db.pgcLichThanhToans.Where(p => p.MaPGC == DepositID).OrderBy(p => p.DotTT).ToList();
            var lstLTT = lstLTTUp.AsEnumerable()
                .Select(p => new ItemLichTT()
                {
                    ID = p.ID,
                    DotTT = p.DotTT ?? 0,
                    SoTien = p.SoTien ?? 0,
                    NgayTT = p.NgayTT,
                    DaThu = 0M,
                    LaiMuon = p.LaiMuon ?? (p.pgcPhieuGiuCho.LaiMuonKH ?? 0),
                    DotTTText = "Đợt " + p.DotTT.ToString(),
                    SoNgayGiam = p.SoNgayGiam ?? 0
                }).ToList();

            var lstPTUp = db.pgcPhieuThus.Where(p => p.MaPGC == DepositID).OrderBy(p => p.NgayThu).ToList();
            foreach (var item in lstPTUp)
            {
                var list = lstLTT.Where(p => p.DaThu < p.SoTien).ToList();
                var sotien = item.TienTT ?? 0;
                var tien = 0M;
                int i = 0;
                if (list.Count > 0)
                {
                    try
                    {
                        while (sotien > 0)
                        {
                            var objltt = list[i++];
                            ItemData temp = new ItemData();
                            temp.ID = objltt.ID;
                            temp.DotTT = objltt.DotTT.Value;
                            temp.NgayTT = objltt.NgayTT;
                            temp.MaPT = item.MaPT;
                            temp.NgayThu = item.NgayThu;
                            temp.LaiMuonDot = item.LaiMuon;
                            temp.SoNgayGiam = objltt.SoNgayGiam;
                            if (sotien >= (objltt.SoTien - objltt.DaThu))
                            {
                                tien = (objltt.SoTien - objltt.DaThu);
                                sotien = sotien - tien;
                                objltt.DaThu = objltt.SoTien;
                            }
                            else
                            {
                                tien = sotien;
                                objltt.DaThu = objltt.DaThu + tien;
                                sotien = 0;
                            }
                            temp.SoTien = tien;
                            temp.LaiSuat = TinhLai(temp.NgayTT, temp.NgayThu, tien, objltt.LaiMuon, temp.SoNgayGiam);
                            lstLichTT.Add(temp);
                        }
                    }
                    catch { }
                }
            }

            foreach (var item in lstLichTT)
            {
                var objIS = new ItemSchedule();
                objIS.DotTT = item.DotTT;
                objIS.DotTTText = "";
                objIS.GhiChu = "";
                objIS.DaNop = item.SoTien;
                objIS.HanNop = item.NgayTT;
                objIS.NgayNop = item.NgayThu;
                objIS.LaiMuon = item.LaiSuat;
                objIS.LaiMuonDot = item.LaiMuonDot;
                var day = (SqlMethods.DateDiffDay(item.NgayTT, item.NgayThu) ?? 0) - item.SoNgayGiam;
                objIS.SoNgayNopMuon = day < 0 ? 0 : day;
                objIS.IsSchedule = false;
                objIS.IsSum = false;
                objIS.IDOrder = 2;
                //objIS.LaiMuon = item.SoTien;
                //objIS.SoTienNoDot = item.SoTien;

                listResut.Add(objIS);
            }

            foreach (var item in listCN)
            {
                var objIS = new ItemSchedule();
                objIS.DotTT = item.DotTT;
                objIS.DotTTText = "Đợt " + item.DotTT.ToString();
                objIS.GhiChu = "";
                objIS.GiaTriPhaiThanhToan = item.SoTien;
                objIS.DaThu = item.DaThu;
                objIS.HanNop = item.NgayTT;
                objIS.LaiMuon = 0;// item.LaiMuon;
                objIS.SoNgayNopMuon = 0;
                objIS.LaiMuonBG = 0;//item.LaiMuon;
                objIS.LaiSuat = 0;
                objIS.LaiMuonDot = item.LaiMuonKH;
                var day = (SqlMethods.DateDiffDay(item.NgayTT, NgayTinh) ?? 0);
                objIS.SoTienNoDot = objIS.GiaTriPhaiThanhToan.GetValueOrDefault() - objIS.DaThu.GetValueOrDefault();

                if (objIS.SoTienNoDot > 0)
                {
                    objIS.SoNgayNoDot = item.SoNgayCham < 0 ? 0 : item.SoNgayCham;// day < 0 ? 0 : day;
                    objIS.LaiMuonNoDot = Math.Round((decimal)(objIS.SoNgayNoDot * objIS.SoTienNoDot * objIS.LaiMuonDot / 100), 2, MidpointRounding.AwayFromZero);//item.LaiMuon;
                }
                else
                    objIS.LaiMuonNoDot = 0;

                objIS.IsSchedule = true;
                objIS.IDOrder = 1;

                listResut.Add(objIS);

                //SUM
                var objISUM = new ItemSchedule();
                objISUM.DotTT = item.DotTT;
                objISUM.DotTTText = "";
                objISUM.GhiChu = "";
                objISUM.DaNop = listResut.Where(p => p.DotTT == item.DotTT).Sum(p => p.DaThu);
                objISUM.LaiMuon = listResut.Where(p => p.DotTT == item.DotTT).Sum(p => p.LaiMuon);
                objISUM.IsSchedule = false;
                objISUM.IsSum = true;
                objISUM.IDOrder = 3;
                objISUM.LaiMuonNoDot = listResut.Where(p => p.DotTT == item.DotTT).Sum(p => p.LaiMuonNoDot);
                //objIS.LaiMuon = item.SoTien;
                //objIS.SoTienNoDot = item.SoTien;

                listResut.Add(objISUM);
            }

            //SUMToTal
            var obj = new ItemSchedule();
            obj.DotTT = 100;
            obj.DotTTText = "Tổng";
            obj.GhiChu = "";
            obj.DaNop = listResut.Where(p => p.IsSum).Sum(p => p.DaNop);
            obj.IsSchedule = false;
            obj.IsSum = true;
            obj.LaiMuonNoDot = (listResut.Where(p => p.IsSum).Sum(p => p.LaiMuonNoDot) ?? 0) + (listResut.Where(p => p.IsSum).Sum(p => p.LaiMuon) ?? 0);
            //objIS.LaiMuon = item.SoTien;
            //objIS.SoTienNoDot = item.SoTien;

            listResut.Add(obj);

            return listResut.OrderBy(p => p.DotTT).ThenBy(p => p.IDOrder).ToList();
        }
    }

    public class ItemLichTT
    {
        public int ID { get; set; }
        public byte? DotTT { get; set; }
        public string DotTTText { get; set; }
        public DateTime? NgayTT { get; set; }
        public decimal SoTien { get; set; }
        public decimal DaThu { get; set; }
        public decimal LaiMuon { get; set; }
        public int SoNgayGiam { get; set; }
    }

    public class ItemData
    {
        public int ID { get; set; }
        public int MaPT { get; set; }
        public int DotTT { get; set; }
        public DateTime? NgayTT { get; set; }
        public DateTime? NgayThu { get; set; }
        public decimal SoTien { get; set; }
        public decimal LaiSuat { get; set; }
        public decimal? LaiMuonDot { get; set; }
        public int SoNgayGiam { get; set; }
    }

    public class ItemSchedule
    {
        public int? DotTT { get; set; }
        public string DotTTText { get; set; }
        public decimal? GiaTriPhaiThanhToan { get; set; }
        public decimal? DaNop { get; set; }
        public decimal? DaThu { get; set; }
        public DateTime? HanNop { get; set; }
        public DateTime? NgayNop { get; set; }
        public int? SoNgayNopMuon { get; set; }
        public decimal? LaiSuat { get; set; }
        public decimal? LaiMuon { get; set; }
        public decimal? LaiMuonDot { get; set; }
        public decimal? SoTienNoDot { get; set; }
        public int? SoNgayNoDot { get; set; }
        public decimal? LaiMuonNoDot { get; set; }
        public DateTime? HanBanGiao { get; set; }
        public int? SoNgayChamBG { get; set; }
        public decimal? LaiMuonBG { get; set; }
        public string GhiChu { get; set; }
        public bool IsSchedule { get; set; }
        public bool IsSum { get; set; }
        public int SoNgayGiam { get; set; }

        public byte IDOrder { get; set; }
    }
}