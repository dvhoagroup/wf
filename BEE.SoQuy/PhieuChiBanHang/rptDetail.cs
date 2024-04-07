using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace BEE.SoQuy.PhieuChiBanHang
{
    public partial class rptDetail : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDetail(int[] ltPhieuChi)
        {
            InitializeComponent();

            #region DataBindings setting
            cellSoPT.DataBindings.Add("Text", null, "SoPhieu", "Số: {0}");
            lblNgayThu.DataBindings.Add("Text", null, "NgayChi", "{0:dd/MM/yyyy}");
            cellNoTK.DataBindings.Add("Text", null, "TKNo");
            cellCoTK.DataBindings.Add("Text", null, "TKCo");
            cellNguoiNop.DataBindings.Add("Text", null, "NguoiNhan");
            cellDiaChi.DataBindings.Add("Text", null, "DiaChi");
            cellLyDo.DataBindings.Add("Text", null, "DienGiai");
            cellSoTien.DataBindings.Add("Text", null, "SoTien", "{0:#,0.##} VNĐ");
            cellBangChu.DataBindings.Add("Text", null, "TienBC", "Viết bằng chữ: {0}");
            cellKemTheo.DataBindings.Add("Text", null, "ChungTuGoc");
            lblNhanDu.DataBindings.Add("Text", null, "TienBC", "Đã nhận đủ số tiền (viết bằng chữ): {0}");
            #endregion

            var objTien = new BEE.TienTeCls();

            using (var db = new BEE.DULIEU.MasterDataContext())
            {
                this.DataSource = db.pgcPhieuChis
                    .Where(p => ltPhieuChi.Contains(p.MaPC))
                    .Select(p => new
                    {
                        p.SoPhieu,
                        p.NgayChi,
                        p.TKCo,
                        p.TKNo,
                        p.NguoiNhan,
                        p.DiaChi,
                        p.DienGiai,
                        p.SoTien,
                        p.SoCMND,
                        p.NgayCap,
                        p.NoiCap,
                        TienBC = objTien.DocTienBangChu(p.SoTien.Value, "đồng"),
                        ChungTuGoc = p.ChungTuGoc == "" ? "........................" : p.ChungTuGoc
                    }).ToList();
            }
        }
    }
}
