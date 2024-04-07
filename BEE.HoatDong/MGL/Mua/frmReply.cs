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
    public partial class frmReply : DevExpress.XtraEditors.XtraForm
    {
        public int ID;

        private MasterDataContext db = new MasterDataContext();
        private mglmtNhatKyXuLy objNhatKy;

        public frmReply()
        {
            InitializeComponent();
        }

        private void frmReply_Load(object sender, EventArgs e)
        {
            objNhatKy = db.mglmtNhatKyXuLies.Single(p => p.ID == this.ID);
            txtTieuDe.EditValue = objNhatKy.TieuDe;
            txtNoiDung.EditValue = objNhatKy.NoiDung;
            txtTenPTXL.EditValue = objNhatKy.PhuongThucXuLy.TenPT;
            txtSoDK.EditValue = objNhatKy.mglmtMuaThue.SoDK;
            txtNguoiGui.EditValue = objNhatKy.NhanVien.HoTen;
            txtKetQua.EditValue = objNhatKy.KetQua;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {            
            if (txtKetQua.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập nội dung giải đáp. Xin cảm ơn");
                txtKetQua.Focus();
                return;
            }

            objNhatKy.KetQua = txtKetQua.Text;
            db.SubmitChanges();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}