using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using System.Linq;
using BEEREMA;

namespace CrawlerWebNew
{
    
    public partial class frmChuyenMuc : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public int MaNV { get; set; }
        byte? MaTinh { get; set; }
        NhanVien objNV { get; set; }

        public frmChuyenMuc()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmSettingRegion_Load(object sender, EventArgs e)
        {
            lookNhanVien.DataSource = db.NhanViens.Where(p => p.MaTinhTrang == 1).Select(p => new
            {
                p.MaNV,
                p.HoTen,
                p.PhongBan.TenPB,
                p.ChucVu.TenCV
            }).OrderBy(p => p.TenPB);
            gcChuyenMuc.DataSource = db.crlHangMucTins;
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvChuyenMuc.FocusedRowHandle < 0)
                return;
            gvChuyenMuc.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
                DialogBox.Infomation("Dữ liệu đã được lưu!");
            }
            catch (Exception ex)
            {
                DialogBox.Error("Lỗi không thể lưu:" + ex.Message);
            }
        }

        private void gvChuyenMuc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvChuyenMuc.FocusedRowHandle < 0)
            {
                gcNhanVien.DataSource = null;
                return;
            }
            if (gvChuyenMuc.GetFocusedRowCellValue("ID") == null)
                return;
            var objCM = db.crlHangMucTins.FirstOrDefault(p => p.ID == (short?)gvChuyenMuc.GetFocusedRowCellValue("ID"));
            if (objCM != null)
                gcNhanVien.DataSource = objCM.crlNhanVien_HangMucTins;
        }

        private void itemDeleteNV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvNhanVien.FocusedRowHandle < 0)
                return;
            gvNhanVien.DeleteSelectedRows();
        }
    }
}