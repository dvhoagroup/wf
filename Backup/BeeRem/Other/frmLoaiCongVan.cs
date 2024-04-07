using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BEE.ThuVien;

namespace BEEREMA.Other
{
    public partial class frmLoaiCongVan : DevExpress.XtraEditors.XtraForm
    {
        int ID;
        public frmLoaiCongVan(int _ID)
        {
            InitializeComponent();
            this.ID = _ID;
        }
        MasterDataContext db;
        private void frmLoaiCongVan_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            if (ID == 1)
                gridControl1.DataSource = db.LoaiCongVans;
            else
                gridControl1.DataSource = db.LoaiCongVanDens;
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            try
            {
                db.SubmitChanges();

                DialogBox.Infomation();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }
    }
}