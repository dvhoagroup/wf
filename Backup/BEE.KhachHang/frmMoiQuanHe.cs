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

namespace BEE.KhachHang
{
    public partial class frmMoiQuanHe : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public frmMoiQuanHe()
        {
            InitializeComponent();
        }

        private void frmMoiQuanHe_Load(object sender, EventArgs e)
        {
            gcMoiQuanHe.DataSource = db.MoiQuanHes.OrderBy(p => p.STT);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (grvMoiQuanHe.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Vui lòng chọn <Tình trạng>, xin cảm ơn.");
                return;
            }

            if (DialogBox.Question() == DialogResult.Yes)
            {
                try
                {
                    MoiQuanHe obj = db.MoiQuanHes.Single(p => p.ID == Convert.ToInt32(grvMoiQuanHe.GetFocusedRowCellValue("ID")));
                    db.MoiQuanHes.DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    grvMoiQuanHe.DeleteSelectedRows();
                }
                catch { }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}