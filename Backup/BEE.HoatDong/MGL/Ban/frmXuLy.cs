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
    public partial class frmXuLy : DevExpress.XtraEditors.XtraForm
    {
        public int? MaMT { get; set; }
        public frmXuLy()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lookTrangThai.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn trạng thái!");
                lookTrangThai.Focus();
                return;
            }
            var wait = DialogBox.WaitingForm();
            try
            {
                using (var db = new MasterDataContext())
                {
                    var obj = new mglbcNhatKyXuLy();
                    var objMT = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == MaMT);
                    var tt=objMT.mglbcTrangThai.TenTT;
                    var objTT=db.mglmtTrangThais.First(p=>p.MaTT == (byte?)lookTrangThai.EditValue);
                    obj.TieuDe = string.Format("Chuyển trạng thái sản phẩm từ {0} thành {1}", tt, objTT.TenTT);
                    obj.NoiDung = txtNoiDung.Text.Trim();
                    obj.MaNVN = obj.MaNVG = Common.StaffID;
                    obj.NgayXL = (DateTime?)dateNgayXL.EditValue;
                    var pt = Convert.ToByte(lookTrangThai.EditValue);
                    objMT.mglbcTrangThai = db.mglbcTrangThais.FirstOrDefault(p => p.MaTT == (byte?)lookTrangThai.EditValue);
                    objMT.mglbcNhatKyXuLies.Add(obj);
                    db.SubmitChanges();
                    
                    DialogBox.Infomation("Dữ liệu đã được lưu!");
                    this.Close();
                }
                
            }
            catch (Exception ex)
            {
                DialogBox.Infomation("Lưu bị lỗi: " + ex.Message);
            }
            finally
            {
                wait.Close();
            }
        }

        private void frmXuLy_Load(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                lookTrangThai.Properties.DataSource = db.mglbcTrangThais;
                dateNgayXL.EditValue = DateTime.Now;
                var objMT = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == MaMT);
                lookTrangThai.EditValue = objMT.MaTT;
            }
        }
    }
}