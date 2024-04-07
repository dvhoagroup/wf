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
        short? ID = 0;
        public frmMoiQuanHe()
        {
            InitializeComponent();
        }

        private void frmMoiQuanHe_Load(object sender, EventArgs e)
        {
            LoadData();
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
                if (string.IsNullOrEmpty(txtMoiQuanHe.Text))
                {
                    DialogBox.Error("Bạn phải nhập mối quan hệ.");
                    txtMoiQuanHe.Focus();
                    return;
                }

                var objMQH = db.MoiQuanHes.FirstOrDefault(p => p.ID == ID);
                if (objMQH == null)
                {
                    objMQH = new MoiQuanHe();
                    db.MoiQuanHes.InsertOnSubmit(objMQH);
                }
                objMQH.STT = spinStt.EditValue != null ? Convert.ToByte(spinStt.EditValue) : Convert.ToByte(0);
                objMQH.TenQH = txtMoiQuanHe.Text;
                objMQH.DoiUng = txtDoiUng.Text;
                objMQH.CreateDate = DateTime.Now;
                objMQH.CreateBy = Common.StaffID;

                db.SubmitChanges();
                DialogBox.Infomation();
                LoadData();
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

        private void grvMoiQuanHe_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ID = (short?)grvMoiQuanHe.GetFocusedRowCellValue("ID");
            spinStt.EditValue = (byte?)grvMoiQuanHe.GetFocusedRowCellValue("STT");
            txtMoiQuanHe.Text = grvMoiQuanHe.GetFocusedRowCellValue("TenQH") != null ? grvMoiQuanHe.GetFocusedRowCellValue("TenQH").ToString() : string.Empty;
            txtDoiUng.Text = grvMoiQuanHe.GetFocusedRowCellValue("DoiUng") != null ? grvMoiQuanHe.GetFocusedRowCellValue("DoiUng").ToString() : string.Empty;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtDoiUng.Text = txtMoiQuanHe.Text = string.Empty;
            ID = 0;
            var stt = db.MoiQuanHes.Max(p => p.STT);
            spinStt.EditValue = stt + 1;
        }

        public void LoadData()
        {
            gcMoiQuanHe.DataSource = (from p in db.MoiQuanHes
                                      from nv in db.NhanViens.Where(nv => nv.MaNV == p.CreateBy).DefaultIfEmpty()
                                      orderby p.STT
                                      select new
                                      {
                                          p.ID,
                                          p.TenQH,
                                          p.STT,
                                          nv.HoTen,
                                          p.CreateDate,
                                          p.DoiUng
                                      }).ToList();
        }
    }
}