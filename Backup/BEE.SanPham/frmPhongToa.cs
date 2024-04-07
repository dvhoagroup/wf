using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.SanPham
{
    public partial class frmPhongToa : DevExpress.XtraEditors.XtraForm
    {
        public DateTime NgayPT;
        public string LyDo;

        public frmPhongToa()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            if (dateNgayPT.Text == "")
            {
                DialogBox.Error("Vui lòng chọn ngày phong tỏa");
                return;
            }

            this.NgayPT = dateNgayPT.DateTime;
            this.LyDo = txtLyDo.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}