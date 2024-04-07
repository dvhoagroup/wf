using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LandSoft.NghiepVu.HDGopVon
{
    public partial class NhacNo_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaHDGV = 0;
        public byte DotTT = 0, LanNN = 0;
        string SoNN = "";
        public bool IsUpdate = false;
        public NhacNo_frm()
        {
            InitializeComponent();
        }

        void TaoSoPhieu()
        {
            SoNN = "";
            it.hdgvNhacNoCls o = new it.hdgvNhacNoCls();
            SoNN = txtSoNN.Text = o.TaoSoPhieu();
        }

        private void NhacNo_frm_Load(object sender, EventArgs e)
        {
            dateNgayNhac.DateTime = DateTime.Now;
            txtLanNN.Text = LanNN.ToString();
            spinGiaHan.EditValue = 3;

            it.hdgvLichThanhToanCls o = new it.hdgvLichThanhToanCls();
            o.HDGV.MaHDGV = MaHDGV;
            o.SelectNextPay();
            txtDotTT.Text = o.DotTT.ToString();
            txtTieuDe.Text = "V/v đến hạn thanh toán tiền góp vốn căn hộ cao cấp đợt " + txtDotTT.Text;

            TaoSoPhieu();
        }

        private void btnLuuDong_Click(object sender, EventArgs e)
        {
        doo:
            it.hdgvNhacNoCls o = new it.hdgvNhacNoCls();
            o.DotTT = byte.Parse(txtDotTT.Text);
            o.GiaHan = int.Parse(spinGiaHan.EditValue.ToString());
            o.LanGui = 0;
            o.LanNN = LanNN;
            o.MaNV = LandSoft.Library.Common.StaffID;
            o.MaHDGV = MaHDGV;
            o.NoiDung = txtNoiDung.Text;
            o.NgayNN = dateNgayNhac.DateTime;
            o.SoNN = txtSoNN.Text;
            o.TieuDe = txtTieuDe.Text;
            o.LoaiNN = LanNN;
            try
            {
                o.Insert();
                IsUpdate = true;
                this.Close();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Cannot insert duplicate key row in object 'dbo.hdgvNhacNo' with unique index 'IX_hdgvNhacNo'.\r\nThe statement has been terminated.")
                {
                    TaoSoPhieu();
                    goto doo;
                }
            }
        }

        private void btnLuuIn_Click(object sender, EventArgs e)
        {
            btnLuuDong_Click(sender, e);
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            btnLuuDong_Click(sender, e);
        }
    }
}