using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.ThongKe.BDS
{
    public partial class LichSuGiaoDich_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public LichSuGiaoDich_rpt(string MaBDS)
        {
            InitializeComponent();
            nhanVien_getAllShowTableAdapter.Fill(bdS_src1.NhanVien_getAllShow);
            blocksTableAdapter.Fill(bdS_src1.Blocks);
            duAn_getAllShowTableAdapter.Fill(bdS_src1.DuAn_getAllShow);
            BEE_BatDongSan_LichSuGiaoDich_rptTableAdapter.Fill(bdS_src1._BEE_BatDongSan_LichSuGiaoDich_rpt, MaBDS);            
        }
    }
}
