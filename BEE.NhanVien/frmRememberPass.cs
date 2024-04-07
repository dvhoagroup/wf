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
    public partial class frmRememberPass : DevExpress.XtraEditors.XtraForm
    {
        public byte KeyID = 0;
        public bool IsUpdate = false;
        public frmRememberPass()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Huong_frm_Load(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                var objNv = db.RememberPasses.FirstOrDefault();
                if (objNv != null)
                {
                    rdoGhiNho.EditValue = objNv.IsCheck == true ? 1 : 2;
                }
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                var objNv = db.RememberPasses.FirstOrDefault();
                if (objNv == null)
                {
                    objNv = new RememberPass();
                    db.RememberPasses.InsertOnSubmit(objNv);
                }
                objNv.IsCheck = Convert.ToInt32(rdoGhiNho.EditValue) == 1 ? true : false;
                db.SubmitChanges();
            }
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }
    }
}