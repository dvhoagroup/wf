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
using System.Net.Mail;

namespace BEE.NhanVien
{
    public partial class frmExpiredLock : DevExpress.XtraEditors.XtraForm
    {
        public int KeyID = 0;
        public bool IsUpdate = false;
        public frmExpiredLock()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (memoComment.Text.Trim() == "")
            {
                DialogBox.Warning("Vui lòng nhập [Ghi chú], xin cảm ơn.");
                memoComment.Focus();
                return;
            }
            if ((int?)rdoStatusLock.EditValue == 5 && dateExprired.EditValue==null)
            {
                DialogBox.Warning("Vui lòng chọn [Ngày hết hạn], xin cảm ơn.");
                dateExprired.Focus();
                return;
            }
            try
            {
                using (var db = new MasterDataContext())
                {
                    var objNv = db.NhanViens.FirstOrDefault(p => p.MaNV == KeyID);
                    objNv.Lock = false;
                    objNv.StatusLock = (int?)rdoStatusLock.EditValue;
                    var number = Convert.ToInt32(spinNumber.EditValue);
                    switch (objNv.StatusLock)
                    {
                        case 1://khóa theo giờ
                            objNv.CreateLock = DateTime.Now.AddHours(number);
                            break;
                        case 2://khóa theo ngày
                            objNv.CreateLock = DateTime.Now.AddDays(number);
                            break;
                        case 3://khóa theo tháng
                            objNv.CreateLock = DateTime.Now.AddMonths(number);
                            break;
                        case 4://khóa theo năm
                            objNv.CreateLock = DateTime.Now.AddYears(number);
                            break;
                        case 5://đến ngày
                            objNv.CreateLock = (DateTime)dateExprired.EditValue;
                            break;
                        default://Vô thời hạn
                            objNv.CreateLock = null;
                            break;
                    }

                    NhanVien_LichSu objHis = new NhanVien_LichSu();
                    objHis.MaNV = Common.StaffID;
                    objHis.GhiChu = memoComment.Text.Trim();
                    objHis.RefID = objNv.MaNV;

                    db.NhanVien_LichSus.InsertOnSubmit(objHis);
                    db.SubmitChanges();
                }
                DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
                DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void frmExpiredLock_Load(object sender, EventArgs e)
        {
            dateExprired.Enabled = false;
            spinNumber.EditValue = 1;
            spinNumber.Enabled = false;
            if (KeyID != 0)
            {
                using (var db = new MasterDataContext())
                {
                    var objNv = db.NhanViens.FirstOrDefault(p => p.MaNV == KeyID);
                    rdoStatusLock.EditValue = objNv.StatusLock;
                    dateExprired.EditValue = objNv.CreateLock;
                    if (objNv.StatusLock == 5)
                    {
                        dateExprired.Enabled = true;
                    }
                }
            }
        }

        private void rdoStatusLock_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((int?)rdoStatusLock.EditValue == 5)
            {
                dateExprired.Enabled = true;
                spinNumber.Enabled = false;
                spinNumber.EditValue = 0;
            }
            else if (rdoStatusLock.EditValue == null)
            {
                dateExprired.Enabled = false;
                dateExprired.EditValue = null;
                spinNumber.Enabled = false;
                spinNumber.EditValue = 0;
            }
            else
            {
                dateExprired.Enabled = false;
                dateExprired.EditValue = null;
                spinNumber.Enabled = true;
                spinNumber.EditValue = 1;
            }
        }
    }
}