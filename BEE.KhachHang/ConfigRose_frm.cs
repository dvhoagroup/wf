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
    public partial class ConfigRose_frm : DevExpress.XtraEditors.XtraForm
    {
        public ConfigRose_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (spinRose.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập mức hoa hồng. Xin cảm ơn.");
                spinRose.Focus();
                return;
            }

            it.NguoiMoiGioiCls o = new it.NguoiMoiGioiCls();
            o.SetRose(string.Format("ALTER FUNCTION dbo.funGetRose() RETURNS float AS BEGIN Declare @Re float Set @Re = {0} RETURN @Re END", double.Parse(spinRose.EditValue.ToString())));

            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }

        private void ConfigFTP_frm_Load(object sender, EventArgs e)
        {
            it.NguoiMoiGioiCls o = new it.NguoiMoiGioiCls();
            o.GetRose();
            spinRose.EditValue = o.Rose;
        }
    }
}