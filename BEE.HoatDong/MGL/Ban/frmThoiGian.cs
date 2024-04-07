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

namespace BEE.HoatDong.MGL.Ban
{
    public partial class frmThoiGian : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        ThuVien.mglbcLocNgay objN;

        public frmThoiGian()
        {
            InitializeComponent();
        }

        public HoatDong.MGL.Ban.ctlManager MainForm
        {
            set;
            get;
        }

        public HoatDong.MGL.Ban.ctlManagerChoThue MainFormCT
        {
            set;
            get;
        }

        private void frmThoiGian_Load(object sender, EventArgs e)
        {
            lookThoiGian.Properties.DataSource = db.mglbcKyBaoCaos;
            if (db.mglbcLocNgays.Count() > 0)
            {
                objN = db.mglbcLocNgays.First();
                lookThoiGian.EditValue = objN.MaBC;
            }
            else
            {
                objN = new mglbcLocNgay();
                db.mglbcLocNgays.InsertOnSubmit(objN);
                db.SubmitChanges();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            objN.MaBC = (short?)lookThoiGian.EditValue;

            db.SubmitChanges();
            try
            {
                MainForm.SetDate(Convert.ToInt32(objN.MaBC));
            }
            catch { }
            try
            {
                MainFormCT.SetDate(Convert.ToInt32(objN.MaBC));
            }
            catch { }
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}