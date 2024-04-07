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

namespace BEE.HoatDong.MGL
{
    public partial class frmDuyet : DevExpress.XtraEditors.XtraForm
    {
        public int MaLS { get; set; }
        public int[] DatCocs { get; set; }
        public byte MaTT { get; set; }

        MasterDataContext db;
       mglLichSu objLS;

        public frmDuyet()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmDuyet_Load(object sender, EventArgs e)
        {
            if (MaLS > 0)
            {
                objLS = db.mglLichSus.Single(p => p.MaLS == this.MaLS);
                txtLyDo.Text = objLS.DienGiai;
            }
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            if (MaLS <= 0)
            {
                foreach (int maDC in DatCocs)
                {
                    objLS = new mglLichSu();
                    objLS.MaTT = this.MaTT;
                    objLS.MaDC = maDC;
                    objLS.NgayCN = DateTime.Now;
                    objLS.DienGiai = txtLyDo.Text;
                    objLS.MaNV = Common.StaffID;
                    db.mglLichSus.InsertOnSubmit(objLS);
                }
            }
            else
            {
                objLS.DienGiai = txtLyDo.Text;
                objLS.MaNV = Common.StaffID;
            }

            db.SubmitChanges();
            
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}