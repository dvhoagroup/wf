using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEEREMA;
using BEE.ThuVien;

namespace BEE.HoatDong.MGL.Ban
{
    public partial class frmDuong : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public frmDuong()
        {
            InitializeComponent();
        }

        private void frmDuong_Load(object sender, EventArgs e)
        {
            gcDuong.DataSource = db.Duongs;
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (grvDuong.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Vui lòng chọn <Đường>, xin cảm ơn.");
                return;
            }

            if (DialogBox.Question() == DialogResult.Yes)
            {
                try
                {
                    ThuVien.Duong obj = db.Duongs.Single(p => p.MaDuong == Convert.ToInt32(grvDuong.GetFocusedRowCellValue("MaDuong")));
                    db.Duongs.DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    grvDuong.DeleteSelectedRows();
                }
                catch { }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}