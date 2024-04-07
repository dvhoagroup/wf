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
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit()
        {
            InitializeComponent();
        }

        public int? MaTL { get; set; }
        public int? LinkID { get; set; }
        public int? FormID { get; set; }

        MasterDataContext db = new MasterDataContext();
        docTaiLieu objDoc;

        private void frmEdit_Load(object sender, EventArgs e)
        {
            BEE.NgonNgu.Language.TranslateControl(this);

            lookLoaiTaiLieu.Properties.DataSource = db.docLoaiTaiLieus.OrderBy(p => p.STT);
            chkNhanVienXem.Properties.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.HoTen });

            if (this.MaTL != null)
            {
                objDoc = db.docTaiLieus.Single(p => p.MaTL == this.MaTL);
                txtKyHieu.EditValue = objDoc.KyHieu;
                txtTenTL.EditValue = objDoc.TenTL;
                lookLoaiTaiLieu.EditValue = objDoc.MaLTL;
                txtDienGiai.EditValue = objDoc.DienGiai;
                string nv = "";
                foreach (var i in objDoc.docNhanVienXems)
                    nv += i.MaNV + ", ";
                nv = nv.TrimEnd(' ').TrimEnd(',');
                chkNhanVienXem.SetEditValue(nv);
            }
            else
            {
                string kyHieu = "";
                db.docTaiLieu_TaoKyHieu(ref kyHieu);
                txtKyHieu.Text = kyHieu;
            chkNhanVienXem.SetEditValue(Common.StaffID);
            }
        }

        private void txtTenTL_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //var frm = new Web.frmUploadFile();
            //if (frm.SelectFile(false))
            //{
            //    txtTenTL.Tag = frm.ClientPath;
            //    if (txtTenTL.Text.Trim() == "")
            //        txtTenTL.Text = frm.FileName;
            //}
            //frm.Dispose();
            var frm = new FTP.frmUploadFile();
            if (frm.SelectFile(false))
            {
                txtTenTL.Tag = frm.ClientPath;
                if (txtTenTL.Text.Trim() == "")
                    txtTenTL.Text = frm.FileName;
            }
            frm.Dispose();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTenTL.Text.Trim() == "")
                {
                    DialogBox.Error("Vui lòng nhập tên tài liệu");
                    txtTenTL.Focus();
                    return;
                }

                if (this.MaTL == null)
                {
                    objDoc = new docTaiLieu();
                    objDoc.FormID = this.FormID;
                    objDoc.LinkID = this.LinkID;
                }

                if (txtTenTL.Tag != null)
                {
                    var frm = new FTP.frmUploadFile();
                    frm.Folder = "documents/";// +DateTime.Now.ToString("yyyy/MM/dd");
                    frm.ClientPath = txtTenTL.Tag.ToString();
                    frm.ShowDialog();
                    if (frm.DialogResult != DialogResult.OK) return;
                    objDoc.DuongDan = frm.FileName;
                }

                objDoc.KyHieu = txtKyHieu.Text;
                objDoc.TenTL = txtTenTL.Text;
                objDoc.MaLTL = (short?)lookLoaiTaiLieu.EditValue;
                objDoc.NhanVienXem = chkNhanVienXem.Text;
                objDoc.DienGiai = txtDienGiai.Text;
                objDoc.NgayTao = DateTime.Now;
                objDoc.MaNV = BEE.ThuVien.Common.StaffID;
                string[] nv = chkNhanVienXem.EditValue != null ? chkNhanVienXem.EditValue.ToString().Split(',') : null;

                if (this.MaTL == null)
                    db.docTaiLieus.InsertOnSubmit(objDoc);
                else
                {
                    if (nv != null)
                    {
                        foreach (var i in objDoc.docNhanVienXems)
                        {
                            if (nv.Where(p => p == i.MaNV.ToString()).Count() <= 0)
                            {
                                db.docNhanVienXems.DeleteOnSubmit(i);
                            }
                        }
                    }
                }

                if (nv[0] != "")
                {
                    foreach (var i in nv)
                    {
                        if (objDoc.docNhanVienXems.Where(p => p.MaNV.ToString() == i).Count() <= 0)
                        {
                            var objNV = new docNhanVienXem();
                            objNV.MaNV = int.Parse(i);
                            objDoc.docNhanVienXems.Add(objNV);
                        }
                    }
                }
                db.SubmitChanges();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}