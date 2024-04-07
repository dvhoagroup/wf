using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEEREMA.BaoCao
{
    public partial class rptLichSuGiaoDich : DevExpress.XtraReports.UI.XtraReport
    {
        public rptLichSuGiaoDich(DateTime? start, DateTime? end, int? maDA)
        {
            InitializeComponent();

            //cellSTT.DataBindings.Add("Text", null, "STT");
            //cellDuAn.DataBindings.Add("Text", null, "TenDA");
            //cellBlock.DataBindings.Add("Text", null, "BlockName");
            //cellNhanVien.DataBindings.Add("Text", null, "NhanVien");
            //cellNgayGD.DataBindings.Add("Text", null, "NgayGD", "{0:dd/MM/yyyy}");
            //cellLoaiGD.DataBindings.Add("Text", null, "LoaiGD");
            //cellKhachHang.DataBindings.Add("Text", null, "HoTenKH");
            //cellMaLo.DataBindings.Add("Text", null, "MaLo");
            //cellDienTich.DataBindings.Add("Text", null, "DienTichXD", "{0:#,0.#}");
            //cellThanhTien.DataBindings.Add("Text", null, "ThanhTienXD", "{0:#,0.#}");
            //cellSoPhieu.DataBindings.Add("Text", null, "SoPhieu");
            //cellGiaBan.DataBindings.Add("Text", null, "DonGiaXD", "{0:#,0.#}");

            //using (var db = new Library.MasterDataContext())
            //{
            //    //db.CommandTimeout = 300;
            //    this.DataSource = db.rptLichSuGiaoDich(start, end, maDA);
            //}
            //rptLichSuGiaoDichTableAdapter.Fill(srcReport1.rptLichSuGiaoDich, start, end, maDA);
        }
    }
}
