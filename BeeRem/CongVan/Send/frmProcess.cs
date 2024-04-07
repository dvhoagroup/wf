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

namespace LandSoft.CongVan.Send
{
    public partial class frmProcess : DevExpress.XtraEditors.XtraForm
    {
        public int? ID, TienDo;
        string FileName = "";
        MasterDataContext db;
        CongVanDi objCV;
        cvNhatKyXuLy xl;
        public frmProcess()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void btnFileAttach_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var frm = new FTP.frmUploadFile();
            if (frm.SelectFile(false))
            {
                btnFileAttach.Tag = frm.ClientPath;
                if (btnFileAttach.Text.Trim() == "")
                    btnFileAttach.Text = frm.FileName;
            }
            frm.Dispose();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (btnFileAttach.Tag != null)
            {
                var frm = new FTP.frmUploadFile();
                frm.Folder = "congvan/di/" + DateTime.Now.ToString("yyyy/MM/dd");
                frm.ClientPath = btnFileAttach.Tag.ToString();
                frm.ShowDialog();
                if (frm.DialogResult != DialogResult.OK) return;
                this.FileName = frm.FileName;
            } 
            
            Save();
        }

        private void frmProcess_Load(object sender, EventArgs e)
        {
            ctlTrinhTrangCV1.LoadData();
            LoadConngVan();
        }

        void Save() 
        {
            #region Rang buoc du lieu
            if (ctlTrinhTrangCV1.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn trình trạng công văn");
                ctlTrinhTrangCV1.Focus();
                return;
            }

            if (txtNoiDung.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập nội dung công văn");
                txtNoiDung.Focus();
                return;
            }
            #endregion

            xl.MaTT = ctlTrinhTrangCV1.EditValue.ToString();
            xl.TienDo = (int?)spinTienDo.Value ?? 0;
            xl.DienGiai = txtNoiDung.Text.Trim();
            xl.NgayXL = DateTime.Now;
            xl.ThoiHan = (DateTime?)dateThoiHan.EditValue;
            xl.F = FileName;
            xl.MaTT = ctlTrinhTrangCV1.EditValue.ToString();
            objCV.TienDo = (int?)spinTienDo.Value ?? 0;
            objCV.MaTT = ctlTrinhTrangCV1.EditValue.ToString();

            try
            {
                db.cvNhatKyXuLies.InsertOnSubmit(xl);
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
            try
            {
                if (this.ID == null)
                {
                    dateThoiHan.DateTime = DateTime.Now.AddDays(7);
                    spinTienDo.EditValue = TienDo;
                }
                else
                {
                    objCV = db.CongVanDis.Single(p => p.ID == this.ID);
                    xl = new cvNhatKyXuLy();
                    xl.MaCV = objCV.ID;
                    xl.MaNV = 1;
                    // xl.STT = 
                    //string c=    (db.cvNhatKyXuLies.Where(i => i.MaCV == this.ID).Max(i => (int?)i.STT??0)).ToString();
                    //(db.cvNhatKyXuLies.Where(i => i.ID == this.ID).Max(i => (int?)ID) ?? 0) + 1;
                    spinTienDo.Value = (decimal)objCV.TienDo;
                    ctlTrinhTrangCV1.EditValue = objCV.MaTT;
                }
            } 
            catch { }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}