using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Net.Mail;
using it;
using System.Threading;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.HoatDong.MGL.Mua.GiaoDich
{
   
    public partial class frmDV : DevExpress.XtraEditors.XtraForm
    {
        public int? MaGD { get; set; }
        MasterDataContext db = new MasterDataContext();

        public frmDV()
        {
            InitializeComponent();
            spDonGia.EditValueChanged += new EventHandler(spDonGia_EditValueChanged);
            spSoLuong.EditValueChanged += new EventHandler(spSoLuong_EditValueChanged);
            lkLoaiDV.EditValueChanged += new EventHandler(lkLoaiDV_EditValueChanged);
        }

        void lkLoaiDV_EditValueChanged(object sender, EventArgs e)
        {
            var sp = (LookUpEdit)sender;
            if (sp.EditValue == null) return;
            var obj = db.LoaiDichVus.SingleOrDefault(p => p.ID == (int?)sp.EditValue);
            gvLoaiDuong.SetFocusedRowCellValue("DonGia", obj.DonGia);

        }

        void spSoLuong_EditValueChanged(object sender, EventArgs e)
        {
            gvLoaiDuong.SetFocusedRowCellValue("SoLuong", ((SpinEdit)sender).Value);
          //  gvLoaiDuong.SetFocusedRowCellValue("SoLuong", 0);
            tinhtien();
        }

        void spDonGia_EditValueChanged(object sender, EventArgs e)
        {
            gvLoaiDuong.SetFocusedRowCellValue("DonGia", ((SpinEdit)sender).Value);
           // gvLoaiDuong.SetFocusedRowCellValue("DonGia", 0);
            tinhtien();
        }

        private void frmNguon_Load(object sender, EventArgs e)
        {
            try
            {
                lkLoaiDV.DataSource = db.LoaiDichVus;
                gcLoaiDuong.DataSource = db.DichVUs.Where(p=>p.GDID==this.MaGD);
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                db.SubmitChanges();
                this.Close();
            }
            catch (Exception ex) {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void tinhtien()
        {
            var sl = (decimal?)gvLoaiDuong.GetFocusedRowCellValue("SoLuong") ?? 0;
            var dg = (decimal?)gvLoaiDuong.GetFocusedRowCellValue("DonGia") ?? 0;
            var tt = sl * dg;
            gvLoaiDuong.SetFocusedRowCellValue("ThanhTien", tt);

        }
        private void gvLoaiDuong_KeyUp(object sender, KeyEventArgs e)
        {
            if (gvLoaiDuong.FocusedRowHandle < 0)
                return;
            if (e.KeyCode == Keys.Delete)
            {
                gvLoaiDuong.DeleteSelectedRows();
            }
        }

        private void gvLoaiDuong_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvLoaiDuong.SetFocusedRowCellValue("GDID",this.MaGD);
        }


    }
}