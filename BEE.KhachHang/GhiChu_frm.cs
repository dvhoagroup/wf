using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.KhachHang
{
    public partial class GhiChu_frm : DevExpress.XtraEditors.XtraForm
    {
        public bool IsUpdate = false;
        int MaKH = 0;
        public GhiChu_frm(int _MaKH)
        {
            InitializeComponent();
            MaKH = _MaKH;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuuDong_Click(object sender, EventArgs e)
        {
            it.KhachHang_GhiChuCls o = new it.KhachHang_GhiChuCls();
            o.MaKH = MaKH;
            o.MaNV = BEE.ThuVien.Common.StaffID;
            o.NoiDung = txtNoiDung.Text;
            o.TieuDe = txtTieuDe.Text;
            o.Insert();

            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã được cập nhật thành công.");
            this.Close();
        }
    }
}