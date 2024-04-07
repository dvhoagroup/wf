using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.KhachHang
{
    public partial class frmLevel : DevExpress.XtraEditors.XtraForm
    {
        public int? MaKH { get; set; }
        MasterDataContext db;
        public frmLevel()
        {
            InitializeComponent();
            db = new MasterDataContext();
            lookUpReason.Properties.DataSource = db.cdReasons.ToList();
            lookUpLevel.Properties.DataSource = db.cdLevels.ToList();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lookUpReason.EditValue == null)
            {
                DialogBox.Error("Reason is not selected.");
                lookUpReason.Focus();
                return;
            }
            if (lookUpLevel.EditValue == null)
            {
                DialogBox.Error("Level is not selected.");
                lookUpLevel.Focus();
                return;
            }
            if (txtComment.Text.Trim().Length <= 0)
            {
                DialogBox.Error("Commmet is not blank.");
                txtComment.Focus();
                return;
            }
            updateLevel();
        }

        private void updateLevel()
        {
            using (var db = new MasterDataContext())
            {
                try
                {
                    var objKH = db.KhachHangs.SingleOrDefault(o => o.MaKH == MaKH);
                    objKH.LevelID = (int?)lookUpLevel.EditValue;

                    var objHis = new KhachHang_NhatKy();
                    objHis.MaKH = MaKH;
                    objHis.DienGiai = Common.StaffName +" change Level: "+txtComment.Text;
                    objHis.MaNV = BEE.ThuVien.Common.StaffID;
                    //objHis.MaNKH = Convert.ToByte(lookUpNhomKH.EditValue);
                    db.KhachHang_NhatKies.InsertOnSubmit(objHis);

                    db.SubmitChanges();

                    DialogBox.Infomation();
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                catch (Exception ex)
                {
                    DialogBox.Error("Code: " + ex.Message);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLevel_Load(object sender, EventArgs e)
        {

        }
    }
}
