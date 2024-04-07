using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using BEEREM;

namespace BEE.SoQuy.PhieuThuBanHang
{
    public partial class rptDetail21 : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDetail21(int maPT)
        {
            InitializeComponent();

            var objTien = new BEE.TienTeCls();

            using (var db = new BEE.DULIEU.MasterDataContext())
            {
                try
                {
                    var pt = db.pgcPhieuThus.FirstOrDefault(p => p.MaPT == maPT);
                    if (pt != null)
                    {
                        var kh = db.KhachHangs.FirstOrDefault(x => x.MaKH == pt.MaKH);
                        lblSoPT.Text = pt.SoPhieu;
                        lblHoTen.Text = pt.NguoiNop;
                        lblCmt.Text = pt.DiaChi;
                        lbldiengiai.Text = pt.DienGiai;
                        lbldienthoai.Text = kh.DienThoaiCT ?? kh.DiDong;
                        lblCmt.Text = pt.SoCMND;// kh.SoCMND + "";
                        lblcaptai.Text = pt.NoiCap;
                        try
                        {
                            lbllodatthamchieu.Text = db.HopDongMuaBans.FirstOrDefault(x => x.MaHDMB == pt.MaPGC).MaBDS;
                        }
                        catch { lbllodatthamchieu.Text = ""; }
                        lblSoTien.Text = string.Format("{0:#,0.#} đồng", pt.SoTien ?? 0);
                        lblSoTien1.Text = string.Format("{0:#,0.#} đồng", pt.SoTien ?? 0);
                        lblSoTienChu.Text = objTien.DocTienBangChu(pt.SoTien.Value, "đồng");
                        lblChungTuKem.Text = pt.ChungTuGoc == "" ? "........................" : pt.ChungTuGoc;
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
