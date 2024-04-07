using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEEREMA;
using BEE.ThuVien;

namespace BEE.HoatDong.MGL.Ban
{
    public partial class frmDuong : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        int? MaDuong = 0;
        public frmDuong()
        {
            InitializeComponent();
        }

        private void frmDuong_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDuong.Text))
                {
                    DialogBox.Error("Bạn phải nhập tên đường.");
                    txtDuong.Focus();
                    return;
                }

                var objD = db.Duongs.FirstOrDefault(p => p.MaDuong == MaDuong);
                if (objD != null)
                {
                    objD.TenDuong = txtDuong.Text;
                    objD.CreateDate = DateTime.Now;
                    objD.CreateBy = Common.StaffID;
                }
                else
                {
                    objD = new Duong();
                    objD.TenDuong = txtDuong.Text;
                    objD.CreateDate = DateTime.Now;
                    objD.CreateBy = Common.StaffID;
                    db.Duongs.InsertOnSubmit(objD);
                }
                db.SubmitChanges();

                DialogBox.Infomation();
                LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (grvDuong.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Vui lòng chọn <Đường>, xin cảm ơn.");
                return;
            }

            if (DialogBox.Question() == DialogResult.Yes)
            {
                try
                {
                    ThuVien.Duong obj = db.Duongs.Single(p => p.MaDuong == Convert.ToInt32(grvDuong.GetFocusedRowCellValue("MaDuong")));
                    db.Duongs.DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    grvDuong.DeleteSelectedRows();
                }
                catch { }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvDuong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            MaDuong = (int?)grvDuong.GetFocusedRowCellValue("MaDuong");
            if (grvDuong.GetFocusedRowCellValue("TenDuong") != null)
            {
                txtDuong.Text = grvDuong.GetFocusedRowCellValue("TenDuong").ToString();
            }
            else
            {
                txtDuong.Text = string.Empty;
            }
        }

        private void itemAdd_Click(object sender, EventArgs e)
        {
            txtDuong.Text = string.Empty;
            MaDuong = 0;
        }

        public void LoadData()
        {
            gcDuong.DataSource = from p in db.Duongs
                                 from nv in db.NhanViens.Where(nv => nv.MaNV == p.CreateBy).DefaultIfEmpty()
                                 select new
                                 {
                                     p.MaDuong,
                                     p.TenDuong,
                                     p.CreateBy,
                                     p.CreateDate,
                                     HoTen = nv.HoTen
                                 };
        }
    }
}