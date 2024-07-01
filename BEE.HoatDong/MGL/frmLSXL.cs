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

namespace BEE.HoatDong.MGL
{
    public partial class frmLSXL : DevExpress.XtraEditors.XtraForm
    {
        public int? ID { get; set; }
        public int? MaMT { get; set; }
        public bool isSave { get; set; }
        public string NoiDung { get; set; }
        public byte MaLoaiTruoc { get; set; }
        public byte MaLoai { get; set; }
        public string TieuDe { get; set; }
        public DateTime NgayXL { get; set; }
        public byte? MaPT { get; set; }
        public frmLSXL()
        {
            InitializeComponent();
        }

        private void frmDuyet_Load(object sender, EventArgs e)
        {
            txtTieuDe.Focus();
            dateNgayXL.EditValue = DateTime.Now;
            using (var db = new MasterDataContext())
            {
                //var lstTT = db.mglTrangThaiGiaoDiches.OrderBy(p => p.Ord);
                //lookTrangthaiHT.Properties.DataSource = lstTT;
                //lookTrangThai.Properties.DataSource = lstTT;
                lookPhuongThuc.Properties.DataSource = db.PhuongThucXuLies;

                //lookTrangthaiHT.EditValue = this.MaLoaiTruoc;
                //lookTrangThai.EditValue = this.MaLoai;
            }
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            if (txtTieuDe.Text == null || txtTieuDe.Text == "")
            {
                DialogBox.Error("Vui lòng nhập thông tin tiêu đề");
                txtTieuDe.Focus();
                return;
            }

            using (MasterDataContext db = new MasterDataContext())
            {
                // lấy giá trị xử lý cuối cùng
                var objlsxlend = db.mglLichSuXuLyGiaoDich_MTs.Where(p => p.MaMTCV == this.ID).OrderByDescending(p=>p.ID).FirstOrDefault();
                var objXL = db.mglMTSanPhams.SingleOrDefault(p => p.ID == this.ID);
                var objls = new mglLichSuXuLyGiaoDich_MT();
                objls.MaNV = Common.StaffID;
                objls.NoiDung = txtLyDo.Text;
                if (objlsxlend != null)
                {
                    objls.MaLoaiTruoc = objlsxlend.MaLoaiTruoc;
                    objls.MaLoai = objXL.MaLoai;

                    // thay đổi : them tay thì đều lấy 1 trạng thái thôi, giống nhau
                    objls.MaLoaiTruoc = (byte?)objXL.MaLoai;
                    objls.MaLoai = objXL.MaLoai;
                }
                else
                {
                    objls.MaLoaiTruoc = null;
                    objls.MaLoai = objXL.MaLoai;
                }
                objls.MaMT = objXL.MaMT;
                objls.MaMTCV = ID;
                objls.Title = txtTieuDe.Text;
                objls.NgayXL = (DateTime?)dateNgayXL.EditValue;
                objls.MaPT = (byte?)lookPhuongThuc.EditValue;
                // cập nhật lại ngày cập nhật 
                objXL.UpdateDate = db.getDate();
                db.mglLichSuXuLyGiaoDich_MTs.InsertOnSubmit(objls);
                db.SubmitChanges();
                DialogBox.Infomation("Dữ liệu đã được lưu!");
                this.Close();
            }

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}