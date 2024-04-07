using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace BEE.SoQuy.PhieuThanhToan
{
    public partial class rptDetail : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDetail(int[] ltPhieuThu)
        {
            InitializeComponent();

            #region DataBindings setting
            cellSoPT.DataBindings.Add("Text", null, "SoPhieu", "Số: {0}");
            lblNgayThu.DataBindings.Add("Text", null, "NgayThu", "Ngày {0:dd/MM/yyyy}");
            cellNoTK.DataBindings.Add("Text", null, "TKNo");
            cellCoTK.DataBindings.Add("Text", null, "TKCo");
            cellNguoiNop.DataBindings.Add("Text", null, "NguoiNop");
            cellSoCMND.DataBindings.Add("Text", null, "SoCMND");
            cellNoiCap.DataBindings.Add("Text", null, "NoiCap");
            cellNgayCap.DataBindings.Add("Text", null, "NgayCap", "{0:dd/MM/yyyy}");
            cellDiaChi.DataBindings.Add("Text", null, "DiaChi");
            cellLyDo.DataBindings.Add("Text", null, "DienGiai");
            cellSoTien.DataBindings.Add("Text", null, "SoTien", "{0:#,0.##} VNĐ");
            cellBangChu.DataBindings.Add("Text", null, "TienBC", "Viết bằng chữ: {0}");
            cellKemTheo.DataBindings.Add("Text", null, "ChungTuGoc", "{0}   chứng từ gốc");
            lblNhanDu.DataBindings.Add("Text", null, "TienBC", "Đã nhận đủ số tiền (viết bằng chữ): {0}");
            #endregion

            var objTien = new BEE.TienTeCls();

            using (var db = new BEE.DULIEU.MasterDataContext())
            {
                this.DataSource = db.pgcPhieuThus
                    .Where(p => ltPhieuThu.Contains(p.MaPT))
                    .Select(p => new
                    {
                        p.SoPhieu,
                        p.NgayThu,
                        p.TKCo,
                        p.TKNo,
                        p.NguoiNop,
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
