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

namespace BEE.HoatDong.MGL.Mua
{
    public partial class frmSend : DevExpress.XtraEditors.XtraForm
    {
        public int ID;
        public int MaMT;

        private MasterDataContext db = new MasterDataContext();
        private mglmtNhatKyXuLy objNhatKy;

        public frmSend()
        {
            InitializeComponent();
        }

        private void frmSend_Load(object sender, EventArgs e)
        {
            lookPhuongThuc.Properties.DataSource = db.PhuongThucXuLies;

            if (this.ID > 0)
            {
                objNhatKy = db.mglmtNhatKyXuLies.Single(p => p.ID == this.ID);
                txtTieuDe.EditValue = objNhatKy.TieuDe;
                txtNoiDung.EditValue = objNhatKy.NoiDung;
                lookPhuongThuc.EditValue = objNhatKy.MaPT;
                txtSoDK.EditValue = objNhatKy.mglmtMuaThue.SoDK;
                txtNguoiNhan.EditValue = objNhatKy.NhanVien1.HoTen;
            }
            else
            {
                objNhatKy = new mglmtNhatKyXuLy();
                objNhatKy.mglmtMuaThue = db.mglmtMuaThues.Single(p => p.MaMT == this.MaMT);
                txtSoDK.EditValue = objNhatKy.mglmtMuaThue.SoDK;
                txtNguoiNhan.EditValue = objNhatKy.mglmtMuaThue.NhanVien.HoTen;
                lookPhuongThuc.ItemIndex = 0;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTieuDe.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tiêu đề. Xin cảm ơn");
                txtTieuDe.Focus();
                return;
            }

            if (txtNoiDung.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập nội dung. Xin cảm ơn");
                txtNoiDung.Focus();
                return;
            }

            if (lookPhuongThuc.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn phương thức xử lý. Xin cảm ơn");
                lookPhuongThuc.Focus();
                return;
            }

            objNhatKy.NgayXL = DateTime.Now;
            objNhatKy.TieuDe = txtTieuDe.Text;
            objNhatKy.NoiDung = txtNoiDung.Text;
            objNhatKy.MaPT = (byte)lookPhuongThuc.EditValue;
            objNhatKy.MaNVG = Common.StaffID;

            if (this.ID == 0)
            {
                objNhatKy.MaNVN = objNhatKy.mglmtMuaThue.MaNVKD;
                db.mglmtNhatKyXuLies.InsertOnSubmit(objNhatKy);
            }
            db.SubmitChanges();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }        
    }
}