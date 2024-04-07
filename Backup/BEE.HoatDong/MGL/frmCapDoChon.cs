using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;
using BEE.ThuVien;

namespace BEE.HoatDong.MGL
{
    public partial class frmCapDoChon : DevExpress.XtraEditors.XtraForm
    {
        public short MaCD { get; set; }

        public frmCapDoChon()
        {
            InitializeComponent();
        }

        private void frmCapDoChon_Load(object sender, EventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                lookCapDo.Properties.DataSource = db.mglCapDos;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (lookCapDo.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn cấp độ");
                return;
            }

            this.MaCD = (short)lookCapDo.EditValue;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}