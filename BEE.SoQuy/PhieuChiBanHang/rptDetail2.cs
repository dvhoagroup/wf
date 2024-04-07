using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using BEEREM;

namespace BEE.SoQuy.PhieuChiBanHang
{
    public partial class rptDetail2 : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDetail2(int maPC)
        {
            InitializeComponent();

            var objTien = new BEE.TienTeCls();

            using (var db = new BEE.DULIEU.MasterDataContext())
            {
                try
                {
                    var pc = db.pgcPhieuChis.SingleOrDefault(p => p.MaPC == maPC);
                    if (pc != null)
                    {
                        cellSoPhieuChi.Text = pc.SoPhieu;
                        cellTKNo.Text = pc.TKNo;
                        cellTKCo.Text = pc.TKCo;
                        lblHoTen.Text = pc.NguoiNhan;
                        lblCMND.Text = pc.SoCMND;
                        lblCapNgay.Text = string.Format("{0:dd/MM/yyyy}", pc.NgayCap);
                        lblNoiCap.Text = pc.NoiCap;
                        lblDiaChi.Text = pc.DiaChi;
                        lblNoiDung.Text = pc.DienGiai;
                        lblSoTien.Text = string.Format("{0:#,0.#} đồng", pc.SoTien ?? 0);
                        lblSoTienChu.Text = objTien.DocTienBangChu(pc.SoTien.Value, "đồng");
                        lblChungTuKem.Text = pc.ChungTuGoc == "" ? "........................" : pc.ChungTuGoc;
                        lblNguoiNhan.Text = pc.NguoiNhan;
                        lblNguoiThu.Text = pc.NhanVien.HoTen;
                        lblSoTienNhan.Text = lblSoTienChu.Text;
                    }

                    var dateNow = db.GetSystemDate();
                    lblNgay.Text = string.Format(lblNgay.Text, dateNow.Day, dateNow.Month, dateNow.Year);
                }
                catch (Exception ex){
                    BEE.DialogBox.Error("Error: " + ex.Message);
                }
            }

        }
    }
}
