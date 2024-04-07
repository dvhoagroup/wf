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
    public partial class Reply_frm : DevExpress.XtraEditors.XtraForm
    {
        public bool IsUpdate = false;
        public int MaDL = 0, STT = 0;
        public string MaSo = "", HoTenNV = "";
        public int MaGD = 0;
        public Reply_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {            
            if (txtReply.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập nội dung giải đáp. Xin cảm ơn");
                txtReply.Focus();
                return;
            }

            it.pgdNhatKyXuLyCls o = new it.pgdNhatKyXuLyCls();
            o.KetQua = txtReply.Text;
            o.STT = STT;
            o.GiaoDich.MaGD = MaGD;
            o.Reply();
            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã được lưu.");
            this.Close();
        }

        void LoadData()
        {
            it.pgdNhatKyXuLyCls o = new it.pgdNhatKyXuLyCls(MaGD, STT);
            txtNoiDung.Text = o.NoiDung;
            txtTieuDe.Text = o.TieuDe;
            txtReply.Text = o.KetQua;
            lookUpPhuongThuc.EditValue = o.PhuongThuc.MaPT;
        }

        private void YeuCauHoTro_frm_Load(object sender, EventArgs e)
        {
            it.PhuongThucXuLyCls o = new it.PhuongThucXuLyCls();
            lookUpPhuongThuc.Properties.DataSource = o.Select();
            lookUpPhuongThuc.ItemIndex = 0;

            LoadData();
            txtMaBDS.Text = MaSo;
            txtNguoiGui.Text = HoTenNV;
        }
    }
}