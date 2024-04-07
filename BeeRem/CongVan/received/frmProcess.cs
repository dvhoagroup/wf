using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LandSoft.Library;
using System.Linq;

namespace LandSoft.CongVan.received
{
    public partial class frmProcess : DevExpress.XtraEditors.XtraForm
    {
        public int? ID;
        string FileName = "";
        MasterDataContext db;
        CongVanDen objCV;
        cvdNhatKyXuLy xl;
        public frmProcess()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void btnFileAttach_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //var frm = new FTP.frmUploadFile();
            //if (frm.SelectFile(false))
            //{
            //    btnFileAttach.Tag = frm.ClientPath;
            //    if (btnFileAttach.Text.Trim() == "")
            //        btnFileAttach.Text = frm.FileName;
            //}
            //frm.Dispose();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            Save();
            //if (btnFileAttach.Tag != null)
            //{
            //    var frm = new FTP.frmUploadFile();
            //    frm.Folder = "congvan/di/" + DateTime.Now.ToString("yyyy/MM/dd");
            //    frm.ClientPath = btnFileAttach.Tag.ToString();
            //    frm.ShowDialog();
            //    if (frm.DialogResult != DialogResult.OK) return;
            //    this.FileName = frm.FileName;
            //}
        }

        private void frmProcess_Load(object sender, EventArgs e)
        {
            ctlTrinhTrangCVD1.LoadData();
            LoadConngVan();

        }
        void Save() {
            #region Rang buoc du lieu
            if (ctlTrinhTrangCVD1.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn trình trạng công văn");
                ctlTrinhTrangCVD1.Focus();
                return;
            }
            if (txtNoiDung.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập nội dung công văn");
                txtNoiDung.Focus();
                return;
            }
            #endregion
            xl.MaTT = ctlTrinhTrangCVD1.EditValue.ToString();
            xl.TienDo = (int?)spinTienDo.Value ?? 0;
            xl.DienGiai = txtNoiDung.Text.Trim();
            xl.NgayXL = DateTime.Now;
            xl.MaTT = ctlTrinhTrangCVD1.EditValue.ToString();
            objCV.TienDo = (int?)spinTienDo.Value ?? 0;
            objCV.MaTT = ctlTrinhTrangCVD1.EditValue.ToString();

            try
            {
                db.cvdNhatKyXuLies.InsertOnSubmit(xl);
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
        void LoadConngVan()
        {
            if (this.ID == null) return;
            objCV = db.CongVanDens.Single(p => p.ID == this.ID);
            xl = new cvdNhatKyXuLy();
            xl.MaCV = objCV.ID;
            xl.MaNV = 1;
           // xl.STT = 
            //string c=    (db.cvNhatKyXuLies.Where(i => i.MaCV == this.ID).Max(i => (int?)i.STT??0)).ToString();
                //(db.cvNhatKyXuLies.Where(i => i.ID == this.ID).Max(i => (int?)ID) ?? 0) + 1;
            spinTienDo.Value = (decimal)objCV.TienDo;
            ctlTrinhTrangCVD1.EditValue = objCV.MaTT;
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}