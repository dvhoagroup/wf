using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.HoatDong.MGL
{
    public partial class frmCaiDatNhanMailCV : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public frmCaiDatNhanMailCV()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void gvCaiDat_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvCaiDat.SetRowCellValue(0,"MaNV", Common.StaffID);
        }

        private void btnTHoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            db.SubmitChanges();

            DialogBox.Infomation("Dữ liệu đã lưu thành công");
            this.Close();
        }

        private void frmCaiDatNhanMailCV_Load(object sender, EventArgs e)
        {
            try
            {
                lookNhanVien.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.HoTen });
               // gcCaiDat.DataSource = db.mglmMailDangKyNhans.Where(p => p.MaNV == Common.StaffID);
                gcCaiDat.DataSource = db.mglmMailDangKyNhans;
            }
            catch
            {
            }
        }
    }
}