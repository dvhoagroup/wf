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

namespace BEE.HoatDong.MGL.Mua.GiaoDich
{
    public partial class frmDichVu : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmDichVu()
        {
            InitializeComponent();
        }

        private void frmNguon_Load(object sender, EventArgs e)
        {
            try
            {
                gcLoaiDuong.DataSource = db.LoaiDichVus;
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

        private void gvLoaiDuong_KeyUp(object sender, KeyEventArgs e)
        {
            if (gvLoaiDuong.FocusedRowHandle < 0)
                return;
            if (e.KeyCode == Keys.Delete)
            {
                gvLoaiDuong.DeleteSelectedRows();
            }
        }


    }
}