using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.HDMB
{
    public partial class HoaDon_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public HoaDon_rpt(int MaHD)
        {
            InitializeComponent();
            hoaDonGTGT_rptTableAdapter.Fill(hdmB_src1.HoaDonGTGT_rpt, MaHD);

            HoaDonChiTiet_rpt rpt = new HoaDonChiTiet_rpt(MaHD);
            subReport.ReportSource = rpt;

            try
            {
                lblTongTien.Text = it.ConvertMoney.ToString(double.Parse(hoaDonGTGT_rptTableAdapter.GetData(MaHD).Rows[0]["TongTien"].ToString()));
            }
            catch { }
        }
    }
}
