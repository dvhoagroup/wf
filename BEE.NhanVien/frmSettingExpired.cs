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
using System.Linq;

namespace BEE.NhanVien
{
    public partial class frmSettingExpired : DevExpress.XtraEditors.XtraForm
    {
        public byte KeyID = 0;
        public bool IsUpdate = false;
        public frmSettingExpired()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void btnDongY_Click(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                var objNv = db.SettingExpireds.FirstOrDefault();
                if (objNv == null)
                {
                    objNv = new SettingExpired();
                    db.SettingExpireds.InsertOnSubmit(objNv);
                }
                objNv.NumberDay = Convert.ToInt32(spinExpiredNum.EditValue);
                db.SubmitChanges();
            }
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }

        private void frmSettingExpired_Load(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                var objE = db.SettingExpireds.FirstOrDefault();
                if (objE != null)
                {
                    spinExpiredNum.EditValue = objE.NumberDay;
                }
            }
        }
    }
}