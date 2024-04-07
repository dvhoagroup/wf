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
using BEEREMA;

namespace BEE.TaiLieu
{
    public partial class frmChuyenNhanVien : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public int? MaTL { get; set; }
        docTaiLieu objTL;
        byte SDBID = 0;

        public frmChuyenNhanVien()
        {
            InitializeComponent();

        }

        private void frmChuyenNhanVien_Load(object sender, EventArgs e)
        {
            chkNhanVienXem.Properties.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.HoTen });

            if (this.MaTL != null)
            {
                objTL = db.docTaiLieus.Single(p => p.MaTL == this.MaTL);
                string nv = "";
                foreach (var i in objTL.docNhanVienXems)
                {
                    nv += i.MaNV + ", ";
                }
                nv = nv.TrimEnd(' ').TrimEnd(',');
                chkNhanVienXem.SetEditValue(nv);
            }
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            string[] nv = chkNhanVienXem.EditValue != null ? chkNhanVienXem.EditValue.ToString().Split(',') : null;
            objTL.NhanVienXem = chkNhanVienXem.Text;
            if (this.MaTL != null)
            {
                if (nv != null)
                    foreach (var i in objTL.docNhanVienXems)
                    {
                        if (nv.Where(p => p == i.MaNV.ToString()).Count() <= 0)
                        {
                            db.docNhanVienXems.DeleteOnSubmit(i);
                        }
                    }
            }

            if (nv[0] != "")
            {
                foreach (var i in nv)
                {
                    if (objTL.docNhanVienXems.Where(p => p.MaNV.ToString() == i).Count() <= 0)
                    {
                        var objNV = new docNhanVienXem();
                        objNV.MaNV = int.Parse(i);
                        objTL.docNhanVienXems.Add(objNV);
                    }
                }
            }
            //
            db.SubmitChanges();

            DialogBox.Infomation();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}