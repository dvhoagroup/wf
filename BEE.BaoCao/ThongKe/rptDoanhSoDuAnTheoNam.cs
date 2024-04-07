using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BEE.ThuVien;
using System.Linq;
using System.Data.Linq;

namespace BEE.BaoCao.ThongKe
{
    public partial class rptDoanhSoDuAnTheoNam : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDoanhSoDuAnTheoNam(int? MaDA, string MaCT, int Year)
        {
            InitializeComponent();
            cSTT.DataBindings.Add("Text", null, "STT");
            cTenCongTrinh.DataBindings.Add("Text", null, "TenKhu");
            cSoLuong1.DataBindings.Add("Text", null, "SoLuong1");
            cSoLuong2.DataBindings.Add("Text", null, "SoLuong2");
            cSoLuong3.DataBindings.Add("Text", null, "SoLuong3");
            cSoLuong4.DataBindings.Add("Text", null, "SoLuong4");
            cSoLuong5.DataBindings.Add("Text", null, "SoLuong5");
            cSoLuong6.DataBindings.Add("Text", null, "SoLuong6");
            cSoLuong7.DataBindings.Add("Text", null, "SoLuong7");
            cSoLuong8.DataBindings.Add("Text", null, "SoLuong8");
            cSoLuong9.DataBindings.Add("Text", null, "SoLuong9");
            cSoLuong10.DataBindings.Add("Text", null, "SoLuong10");
            cSoLuong11.DataBindings.Add("Text", null, "SoLuong11");
            cSoLuong12.DataBindings.Add("Text", null, "SoLuong12");

            cDoanhSo1.DataBindings.Add("Text", null, "DoanhSo1", "{0:n0}");
            cDoanhSo2.DataBindings.Add("Text", null, "DoanhSo2", "{0:n0}");
            cDoanhSo3.DataBindings.Add("Text", null, "DoanhSo3", "{0:n0}");
            cDoanhSo4.DataBindings.Add("Text", null, "DoanhSo4", "{0:n0}");
            cDoanhSo5.DataBindings.Add("Text", null, "DoanhSo5", "{0:n0}");
            cDoanhSo6.DataBindings.Add("Text", null, "DoanhSo6", "{0:n0}");
            cDoanhSo7.DataBindings.Add("Text", null, "DoanhSo7", "{0:n0}");
            cDoanhSo8.DataBindings.Add("Text", null, "DoanhSo8", "{0:n0}");
            cDoanhSo9.DataBindings.Add("Text", null, "DoanhSo9", "{0:n0}");
            cDoanhSo10.DataBindings.Add("Text", null, "DoanhSo10", "{0:n0}");
            cDoanhSo11.DataBindings.Add("Text", null, "DoanhSo11", "{0:n0}");
            cDoanhSo12.DataBindings.Add("Text", null, "DoanhSo12", "{0:n0}");

            cGiaBan1.DataBindings.Add("Text", null, "GiaBan1", "{0:n0}");
            cGiaBan2.DataBindings.Add("Text", null, "GiaBan2", "{0:n0}");
            cGiaBan3.DataBindings.Add("Text", null, "GiaBan3", "{0:n0}");
            cGiaBan4.DataBindings.Add("Text", null, "GiaBan4", "{0:n0}");
            cGiaBan5.DataBindings.Add("Text", null, "GiaBan5", "{0:n0}");
            cGiaBan6.DataBindings.Add("Text", null, "GiaBan6", "{0:n0}");
            cGiaBan7.DataBindings.Add("Text", null, "GiaBan7", "{0:n0}");
            cGiaBan8.DataBindings.Add("Text", null, "GiaBan8", "{0:n0}");
            cGiaBan9.DataBindings.Add("Text", null, "GiaBan9", "{0:n0}");
            cGiaBan10.DataBindings.Add("Text", null, "GiaBan10", "{0:n0}");
            cGiaBan11.DataBindings.Add("Text", null, "GiaBan11", "{0:n0}");
            cGiaBan12.DataBindings.Add("Text", null, "GiaBan12", "{0:n0}");

            cTongSoLuong.DataBindings.Add("Text", null, "TongSoLuong");
            cTongDoanhThu.DataBindings.Add("Text", null, "TongDoanhSo", "{0:n0}");
            cTongGiaBan.DataBindings.Add("Text", null, "TongGiaBan", "{0:n0}");

            try
            {
                lbTitle.Text = String.Format("DOANH THU BÁN HÀNG DỰ ÁN NĂM {0}", Year);
                using (var db = new MasterDataContext())
                {
                    var list = db.getReportTongHopDuAnTheoNam(MaDA, Year, "," + MaCT + ",").ToList();
                    var duan = db.DuAns.Single(p => p.MaDA == MaDA);
                    cTenDuAn.Text = duan.TenDA;
                    this.DataSource = list;
                }
            }
            catch
            { }
        }

    }
}
