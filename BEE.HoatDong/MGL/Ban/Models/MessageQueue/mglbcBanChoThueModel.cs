using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEE.HoatDong.MGL.Ban.Models.MessageQueue
{
    public class mglbcBanChoThueModel
    {

        // list khách hàng
        public List<KhachHang> KhachHang { get; set; }
        public int MaBC { get; set; }
        public byte? MaTT { get; set; }
        public bool? IsBan { get; set; } // ok 
        public List<mglbcAnhbdsModel> AnhBDS { get; set; }
        public List<mglbcVideobdsModel> VideoBDS { get; set; }
        // listimage ///////////////////////////////////////////////////////////////// 
        public string ViTri { get; set; } // chưa có
        public string SoNha { get; set; } // ok
        public string TenDuong { get; set; } // ok 
        public int? MaDuong { get; set; }  // ok
        public int? MaXa { get; set; } // ok 
        public short? MaHuyen { get; set; } // ok 
        public byte? MaTinh { get; set; } // ok
        /// list ảnh 360 ////////////////////////////////////////////////////////////////

        /// <summary>
        ///  thông tin chung
        /// </summary>
        /// 
        public short? MaLbds { get; set; }  // ok
        public decimal? ThanhTien { get; set; } // giá // ok 
        public decimal? DienTich { get; set; } // ok 
        public byte? SoTang { get; set; }  // ok 
        public decimal? NgangXD { get; set; } // mặt tiền // ok 
        public short? MaHuong { get; set; } // ok 
        public decimal? DuongRong { get; set; } // ok 


        /// <summary>
        /// /////////////////////////////////////////nội thất
        /// </summary>


        public int? MaTTNT { get; set; } // tình trạng nt  - no
        public byte? PhongNgu { get; set; } // ok
        public byte? PhongVs { get; set; } // ok 
        public int? SoTangXD { get; set; } // ok
        public byte? PhongBep { get; set; } //  - no 
        public byte? PhongAn { get; set; } //  - no
        public byte? PhongKhach { get; set; } // - no 
        public byte? DieuHoa { get; set; } // - no 
        public byte? NongLanh { get; set; } // - no 
        public bool? IsThangMay { get; set; } // ok
        public bool? TangHam { get; set; } // ok
        public int? MaSan { get; set; } // - list - no  *
        /// <summary>
        /// ngoại thất
        /// </summary>

        public byte? CuaSo { get; set; } // - no 
        public bool? isBanCong { get; set; } // - no
        public short? HuongBanCong { get; set; } // ok
        public bool? isSan { get; set; } // - no
        public bool? isVuon { get; set; } // - no     


        /// <summary>
        /// đỗ xe
        /// </summary>

        public int? MaDX { get; set; } // - no
        public decimal? KhoangCachDX { get; set; } // - no

        /// <summary>
        /// kích thước chi tiết
        /// </summary>

        public decimal? DienTichDat { get; set; } // ok 
        public decimal? DienTichXd { get; set; } // diện tích xây dựng // ok 
        public decimal? DaiXD { get; set; } // chiều sâu // ok
        public decimal? DaiKV { get; set; }  //sâu thông thủy // ok 
        public decimal? SauXD { get; set; } // mặt sau // ok 
        public decimal? SauKV { get; set; } // măt sau thông thủy // ok
        public decimal? NgangKV { get; set; } // mặt tiền thông thủy // ok
        /// <summary>
        /// khác
        /// </summary>
        public short? MaLd { get; set; } // ok 
        public string TenLD { get; set; }
        public int? NamXayDung { get; set; } // ok
        public short? MaPl { get; set; } // ok
        ////
        public List<docTaiLieu> PhapLyBDS { get; set; }
        ///
        public string GhiChu { get; set; } // ok

        public string AppMaDT { get; set; }
        public string AppMaBC { get; set; }
        public bool? isUpdate { get; set; }

        public string syncId { get; set; }

        // bo sung 
        public decimal? TyLeMG { get; set; }
        public short? CachTinhPMG { get; set; }
        public decimal? PhiMG { get; set; }

    }

    public class mglbcAnhbdsModel
    {
        public string DuongDan { get; set; }
        public int? isS3 { get; set; }
        public string VỉTri { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }



    }
    public class mglbcVideobdsModel
    {
        public string DuongDan { get; set; }
        public int? isS3 { get; set; }
        public string ViTri { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }



    }
    public class docTaiLieu
    {
        public string DuongDan { get; set; }
    }
    public class KhachHang
    {
        public int? MaKH { get; set; }
        public string HoTen { get; set; }
        public string DiDong { get; set; }
        public string DiDong2 { get; set; }
        public string DiDong3 { get; set; }
        public string DiDong4 { get; set; }
        public string AppMaKH { get; set; }
    }

}
