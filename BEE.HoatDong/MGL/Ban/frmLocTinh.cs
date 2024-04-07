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
    public partial class frmLocTinh : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        ThuVien.mglbcLocTheoTinh objT;
        public int? ID { get; set; }

        public frmLocTinh()
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

        private void frmLocTinh_Load(object sender, EventArgs e)
        {
            chkTinh.Properties.DataSource = db.Tinhs;
            if (db.mglbcLocTheoTinhs.Count() > 0)
            {
                objT = db.mglbcLocTheoTinhs.First();
                string nv = "";
                foreach (var i in db.mglbcLocTheoTinhs)
                {
                    nv += i.MaTinh + ", ";
                }
                nv = nv.TrimEnd(' ').TrimEnd(',');
                chkTinh.SetEditValue(nv);
            }
            else
            {
                objT = new mglbcLocTheoTinh();
                db.mglbcLocTheoTinhs.InsertOnSubmit(objT);
                db.SubmitChanges();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            objT.MaTinh = chkTinh.EditValue + "";

            db.SubmitChanges();
            try
            {
                MainForm.setFillter(objT.MaTinh);
            }
            catch { }
            try
            {
                MainFormCT.setFillter(objT.MaTinh);
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