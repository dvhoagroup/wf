using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BEE.ThuVien
{
    public partial class MasterDataContext : System.Data.Linq.DataContext
    {
        [Function(Name = "NEWID", IsComposable = true)]
        public Guid Random()
        {
            return Guid.NewGuid();
        }

        [Function(Name = "GetDate", IsComposable = true)]
        public DateTime GetSystemDate()
        {
            MethodInfo mi = MethodBase.GetCurrentMethod() as MethodInfo;

            return (DateTime)this.ExecuteMethodCall(this, mi, new object[] { }).ReturnValue;
        }
    }

    public partial class KhachHang
    {
        static Expression<Func<KhachHang, string>> DisplayNameExpr = t => t.HoKH + " " + t.TenKH;

        public string HoTenKH
        {
            get
            {
                var nameFunc = DisplayNameExpr.Compile(); // compile the expression into a Function
                return nameFunc(this); // call the function using the current object
            }
        }

        public string DCTT
        {
            get
            {
                string dc = this.ThuongTru;
                dc += this.MaXa != null ? ", " + this.Xa.TenXa : "";
                dc += this.MaHuyen != null ? ", " + this.Huyen.TenHuyen : "";
                dc += this.MaTinh != null ? ", " + this.Tinh.TenTinh : "";
                return dc;
            }
        }

        public string DCLL
        {
            get
            {
                string dc = this.DiaChi;
                dc += this.MaXa2 != null ? ", " + this.Xa1.TenXa : "";
                dc += this.MaHuyen2 != null ? ", " + this.Huyen1.TenHuyen : "";
                dc += this.MaTinh2 != null ? ", " + this.Tinh1.TenTinh : "";
                return dc;
            }
        }
    }

    public partial class NguoiDaiDien
    {
        public string DiaChiThuongTru
        {
            get
            {
                string dc = this.DiaChiTT;
                dc += this.MaXa != null ? ", " + this.Xa.TenXa : "";
                
                try
                {
                    dc += this.Xa.Huyen != null ? ", " + this.Xa.Huyen.TenHuyen : "";
                    dc += this.Xa.Huyen.Tinh != null ? ", " + this.Xa.Huyen.Tinh.TenTinh : "";
                }
                catch { }
                return dc;
            }
        }

        public string DiaChiLienLac
        {
            get
            {
                string dc = this.DiaChiLL;
                dc += this.Xa1 != null ? ", " + this.Xa1.TenXa : "";                
                try
                {
                    dc += this.Xa1.Huyen != null ? ", " + this.Xa1.Huyen.TenHuyen : "";
                    dc += this.Xa1.Huyen.Tinh != null ? ", " + this.Xa1.Huyen.Tinh.TenTinh : "";
                }
                catch { }
                return dc;
            }
        }
    }

    public partial class bdsSanPham_SelectResult
    {
        private decimal? _TongGiaBanVAT;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongGiaBanVAT", DbType = "money")]
        public decimal? TongGiaBanVAT
        {
            get { return Math.Round(((this.TongGiaBan ?? 0) + (this.ThueVAT ?? 0)) / 1000, 0, MidpointRounding.AwayFromZero) * 1000; }
        }

        private decimal? _TongGiaTri;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongGiaTri", DbType = "money")]
        public decimal? TongGiaTri
        {
            get { return (this.PhiBaoTri ?? 0) + (this.TongGiaBanVAT ?? 0); }
        }
    }

    public partial class pgcPhieuGiuCho_SelectResult
    {
        private decimal? _TongTien;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongTien", DbType = "money")]
        public decimal? TongTien
        {
            get { return this.ThanhTien + this.PhuThu - this.ChietKhau; }
        }
    }

    public partial class getReportTongHopDuAnResult
    {
        private int? _TongDaBan;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongDaBan", DbType = "int")]
        public int? TongDaBan
        {
            get { return this.DatCocKy + this.DaKyTrongKy + this.GiuChoTrongKy; }
        }
    }

    public partial class pdcPhieuDatCoc_SelectResult
    {
        private decimal? _TongTien;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongTien", DbType = "money")]
        public decimal? TongTien
        {
            get { return this.ThanhTien + this.PhuThu - this.ChietKhau; }
        }

        private decimal? _ConPhaiThu;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ConPhaiThu", DbType = "money")]
        public decimal? ConPhaiThu
        {
            get { return this.TongTien - this.DaThuTien; }
        }
    }

    public partial class vvbhHopDong_SelectResult
    {
        private decimal? _TongTien;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongTien", DbType = "money")]
        public decimal? TongTien
        {
            get { return this.ThanhTien + this.PhuThu - this.ChietKhau; }
        }
    }

    public partial class HopDongMuaBan_SelectResult
    {
        private decimal? _TongTien;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongTien", DbType = "money")]
        public decimal? TongTien
        {
            get { return this.ThanhTien + this.PhuThu - this.ChietKhau; }
        }

        private decimal? _ConPhaiThu;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ConPhaiThu", DbType = "money")]
        public decimal? ConPhaiThu
        {
            get { return this.TongTien - this.DaThuTien; }
        }
    }

    public partial class bgbhBanGiao_SelectResult
    {
        private decimal? _TongTien;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongTien", DbType = "money")]
        public decimal? TongTien
        {
            get { return this.ThanhTien + this.PhuThu - this.ChietKhau; }
        }
    }

    public partial class CongNo_getAllResult
    {
        private decimal? _Cong;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Cong", DbType = "money")]
        public decimal? Cong
        {
            get { return this.ConNo + this.TienTreHan - this.TienCK; }
        }
    }

    public partial class nnLichThanhToan_SelectResult
    {
        private decimal? _Cong;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Cong", DbType = "money")]
        public decimal? Cong
        {
            get { return this.ConNo + this.TienTreHan - this.TienCK; }
        }
    }
    
    public partial class nnLichThanhToan_SelectGroupByKHResult
    {
        private decimal? _Cong;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Cong", DbType = "money")]
        public decimal? Cong
        {
            get { return this.ConNo + this.TienTreHan - this.TienCK; }
        }
    }

    public partial class rptSanPhamTonResult
    {
        private decimal? _BanPhaiThu;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BanPhaiThu", DbType = "Money")]
        public decimal? BanPhaiThu
        {
            get { return this.BanGiaTri - this.BanDaThu; }
        }

        private int? _TonSL;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TonSL", DbType = "int")]
        public int? TonSL
        {
            get { return this.TongSL - this.BanSL; }
        }

        private decimal? _TonDT;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TonDT", DbType = "Decimal(38,2)")]
        public decimal? TonDT
        {
            get { return this.TongDT - this.BanDT; }
        }
    }

    public partial class rptPhanTichTuoiNoResult
    {
        private decimal? _ConNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ConNo", DbType = "Money")]
        public decimal? ConNo
        {
            get { return this.GiaTriHD - this.DaThu; }
        }

        private decimal? _PhaiNop;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PhaiNop", DbType = "Money")]
        public decimal? PhaiNop
        {
            get { return this.ConNo + this.TienLai; }
        }
    }

    public partial class rptCongNoTongHopResult
    {
        private decimal? _ConNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ConNo", DbType = "Money")]
        public decimal? ConNo
        {
            get { return this.DauKy + this.TienPhat - this.ChietKhau - this.DaThu; }
        }

        private decimal? _PhatSinh;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PhatSinh", DbType = "Money")]
        public decimal? PhatSinh
        {
            get { return this.TrongHan + this.Qua0 + this.Qua7 + this.Qua14 + this.Qua30; }
        }
    }

    public partial class KhachHang_getCongNoByHDResult
    {
        private decimal? _LaiSuat;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LaiSuat", DbType = "Money")]
        public decimal? LaiSuat
        {
            get { return this.QuaHan * this.Percentage / 100; }
        }

        private decimal? _TongTien;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongTien", DbType = "Money")]
        public decimal? TongTien
        {
            get { return this.SoTien + this.QuaHan + this.LaiSuat; }
        }
    }

    public partial class DuAnItem
    {
        public int MaDA { get; set; }
        public string TenDA { get; set; }

        public DuAnItem(int maDA, string tenDA)
        {
            this.MaDA = maDA;
            this.TenDA = tenDA;
        }
    }

    public partial class Data
    {
        public int ID { get; set; }
        public decimal Sold { get; set; }
        public decimal Deposit { get; set; }
        public decimal Zero { get; set; }
        public decimal Total { get; set; }
        public string Name { get; set; }

        public Data(int id, string name, decimal sold, decimal deposit, decimal zero)
        {
            this.ID = id;
            this.Name = name;
            this.Sold = sold;
            this.Deposit = deposit;
            this.Zero = zero;
            this.Total = Sold + Deposit + Zero;
        }
    }
}