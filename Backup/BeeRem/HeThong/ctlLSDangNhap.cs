using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BEE.ThuVien;
using DevExpress.XtraEditors;

namespace BEEREMA.HeThong
{
    public partial class ctlLSDangNhap : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db;
        public ctlLSDangNhap()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        void LoadData()
        {
            gcLichSu.DataSource = db.LichSuDangNhapNVs
                  .Select(p => new
                  {
                      p.NhanVien.HoTen,
                      p.NhanVien.MaSo,
                      p.ThoiGian,
                      p.DiaChiIP,
                      p.ComputerName,
                      p.Online
                  })
                  .OrderByDescending(p => p.ThoiGian);
        }

        private void ctlLSDangNhap_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}
