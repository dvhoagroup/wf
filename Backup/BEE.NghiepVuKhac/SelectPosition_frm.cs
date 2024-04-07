using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.NghiepVuKhac
{
    public partial class SelectPosition_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaXa { get; set; }
        public short MaHuyen { get; set; }
        public byte MaTinh { get; set; }
        public string Result { get; set; }

        public SelectPosition_frm()
        {
            InitializeComponent();
        }

        void LoadTinh()
        {
            it.TinhCls objTinh = new it.TinhCls();
            lookUpTinh.Properties.DataSource = objTinh.Select();
            lookUpTinh.ItemIndex = 0;
        }

        void LoadHuyen()
        {
            MaTinh = (byte)lookUpTinh.EditValue;
            it.HuyenCls objHuyen = new it.HuyenCls();
            lookUpHuyen.Properties.DataSource = objHuyen.Select2(MaTinh);
            lookUpHuyen.ItemIndex = 0;
        }

        void LoadXa()
        {
            MaHuyen = Convert.ToInt16(lookUpHuyen.EditValue);
            it.XaCls objXa = new it.XaCls();
            lookUpXa.Properties.DataSource = objXa.Select2(MaHuyen);
            lookUpXa.ItemIndex = 0;
        }

        private void SelectPosition_frm_Load(object sender, EventArgs e)
        {
            LoadTinh();
            if (this.MaXa > 0)
            {
                DataRow r = it.CommonCls.Table("select x.MaHuyen, h.MaTinh " +
                        "from Xa x inner join Huyen h on x.MaHuyen=h.MaHuyen " +
                        "where x.MaXa=" + this.MaXa).Rows[0];
                lookUpTinh.EditValue = (byte)r["MaTinh"];
                lookUpHuyen.EditValue = Convert.ToInt32(r["MaHuyen"]);
                lookUpXa.EditValue = this.MaXa;
            }
        }

        private void lookUpTinh_EditValueChanged(object sender, EventArgs e)
        {
            LoadHuyen();
        }

        private void lookUpHuyen_EditValueChanged(object sender, EventArgs e)
        {
            LoadXa();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (lookUpTinh.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn [Tỉnh], xin cảm ơn.\r\n\r\nPlease select [Province]. Thanks!");
                lookUpTinh.Focus();
                return;
            }

            MaXa = int.Parse(lookUpXa.EditValue.ToString());
            MaHuyen = short.Parse(lookUpHuyen.EditValue.ToString());
            MaTinh = byte.Parse(lookUpTinh.EditValue.ToString());
            Result = string.Format("{0}{1}{2}",
                lookUpXa.Text == "[Không xác định]" ? "" : ", " + lookUpXa.Text,
                lookUpHuyen.Text == "[Không xác định]" ? "" : ", " + lookUpHuyen.Text,
                ", " + lookUpTinh.Text);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}