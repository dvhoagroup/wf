using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LandSoft.NghiepVu.GiaoDich
{
    public partial class YeuCauHoTro_frm : DevExpress.XtraEditors.XtraForm
    {
        public bool IsUpdate = false;
        public string MaBDS = "", MaSo = "", NhanVien = "";
        public int MaNV = 0, MaGD = 0;
        public YeuCauHoTro_frm()
        {
            InitializeComponent();
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

            if (lookUpPhuongThuc.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn phương thức xử lý. Xin cảm ơn");
                lookUpPhuongThuc.Focus();
                return;
            }

            it.pgdNhatKyXuLyCls o = new it.pgdNhatKyXuLyCls();
            o.TieuDe = txtTieuDe.Text;
            o.NoiDung = txtNoiDung.Text;
            o.PhuongThuc.MaPT = byte.Parse(lookUpPhuongThuc.EditValue.ToString());
            o.GiaoDich.MaGD = MaGD;
            o.NVPT.MaNV = MaNV;
            o.NVXL.MaNV = LandSoft.Library.Common.StaffID;
            o.KetQua = "";
            o.NgayXL = DateTime.Now;
            o.Insert();
            IsUpdate = true;
            DialogBox.Infomation("Đã gửi yêu cầu thành công.");
            this.Close();
        }

        private void YeuCauHoTro_frm_Load(object sender, EventArgs e)
        {
            it.pgdNhatKyXuLyCls o = new it.pgdNhatKyXuLyCls();
            lookUpPhuongThuc.Properties.DataSource = o.PhuongThuc.Select();
            lookUpPhuongThuc.ItemIndex = 0;

            txtMaBDS.Text = MaSo;
            txtNguoiNhan.Text = NhanVien;
        }
    }
}