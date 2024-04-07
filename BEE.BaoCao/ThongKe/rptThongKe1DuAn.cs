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
    public partial class rptThong1KeDuAn : DevExpress.XtraReports.UI.XtraReport
    {
        public rptThong1KeDuAn(int? MaDA, string  MaKhu, DateTime? tungay, DateTime? denngay)
        {
            InitializeComponent();
            try
            {
                using (var db = new MasterDataContext())
                {
                    var list = db.getReport1DuAn(tungay, denngay, "," + MaKhu + ",").ToList();
                    if (list == null) return;
                    var duan = db.DuAns.Single(p => p.MaDA == MaDA);
                    cTenDuAn.Text = duan.TenDA;
                    lbNgay.Text = String.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", tungay, denngay);

                    cTenKhu.DataBindings.Add("Text", null,"TenKhu");
                    cTongSPBanDuoc.DataBindings.Add("Text", null,"TongSPDaBan","{0:#,0.#}");
                    cTongGTBanDuoc.DataBindings.Add("Text", null, "DaKyDauKyGiaTri", "{0:#,0.#}");

                    cSoLuongTongSP.DataBindings.Add("Text", null,"TongSP","{0:#,0.#}");
                    cSoLuongTongSPGiaTri.DataBindings.Add("Text", null,"GiaTriSP","{0:#,0.#}");

                    cSoLuongDaKyDenNgay.DataBindings.Add("Text", null, "TongSPDaBan", "{0:#,0.#}");
                    cGiaTriDaKyDenNgay.DataBindings.Add("Text", null,"DaKyDauKyGiaTri","{0:#,0.#}");

                    cTongDaBanTrongky.DataBindings.Add("Text", null,"DaKyTrongKy","{0:#,0.#}");
                    cGiaTriTongDaBanTrongky.DataBindings.Add("Text", null,"DaKyTrongKyGiaTri","{0:#,0.#}");

                    cDatCocTrongKy.DataBindings.Add("Text", null,"DatCocKy","{0:#,0.#}");
                    cGiaTriDatCocTrongKy.DataBindings.Add("Text", null,"DatCocKyGiaTri","{0:#,0.#}");

                    cGiuChoTrongKy.DataBindings.Add("Text", null,"GiuChoTrongKy","{0:#,0.#}");
                    cGiaTriGiuChoTrongKy.DataBindings.Add("Text", null, "GiuChoTrongKyGiaTri", "{0:#,0.#}");
                    this.DataSource = list;
                }
            }
            catch
            { }
        }

    }
}
