using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace BEE.SoQuy.PhieuThuBanHang
{
    public partial class rptDetail : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDetail(int[] ltPhieuThu)
        {
            InitializeComponent();

            #region DataBindings setting
            cellSoPT.DataBindings.Add("Text", null, "SoPhieu", "Số: {0}");
            lblNgayThu.DataBindings.Add("Text", null, "NgayThu", "Ngày {0:dd} tháng {0:MM} năm {0:yyyy}");
            cellNoTK.DataBindings.Add("Text", null, "TKNo");
            cellCoTK.DataBindings.Add("Text", null, "TKCo");
            cellNguoiNop.DataBindings.Add("Text", null, "NguoiNop");
            //cellSoCMND.DataBindings.Add("Text", null, "SoCMND");
            //cellNoiCap.DataBindings.Add("Text", null, "NoiCap");
            //cellNgayCap.DataBindings.Add("Text", null, "NgayCap", "{0:dd/MM/yyyy}");
            cellDiaChi.DataBindings.Add("Text", null, "DiaChi");
            cellLyDo.DataBindings.Add("Text", null, "DienGiai");
            cellSoTien.DataBindings.Add("Text", null, "SoTien", "{0:#,0.##} VNĐ");
            cellBangChu.DataBindings.Add("Text", null, "TienBC", "{0}");
            cDonVI.DataBindings.Add("Text", null, "DonVi");
            //lblKemTheo.DataBindings.Add("Text", null, "ChungTuGoc", "Kèm theo {0}   chứng từ gốc");
            //lblNhanDu.DataBindings.Add("Text", null, "SoTien", "Đã nhận đủ số tiền: {0:#,0.##} VNĐ");
            //  lblNhanDuChu.DataBindings.Add("Text", null, "TienBC", "   {0}");
            #endregion
           
            
        }
        //public string gethote(BEE.DULIEU.MasterDataContext db, int? makh)
        //{
        //    var rk = db.KhachHangs.Where(xx => xx.MaKH == makh).FirstOrDefault();
        //    return rk.HoKH + " " + rk.TenKH;
        //}
    }
}
