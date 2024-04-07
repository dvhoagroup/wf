using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using System.Linq;
using BEEREMA;

namespace BEE.NhanVien.Agency
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public Int64 MaNV { get; set; }
        public bool IsUpdate = false;
        MasterDataContext db;
        ThuVien.DoiTacDangKy objNV;
        //bool error = false;
        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadInfo()
        {

            lookUpNhomKD.Properties.DataSource = db.NhomKinhDoanhs.Where(p=>p.isAgent == true);
            lookUpPhongBan.Properties.DataSource = db.PhongBans.Where(p => p.isAgent == true);
        }

        private void Huong_frm_Load(object sender, EventArgs e)
        {
            BEE.NgonNgu.Language.TranslateControl(this);
            LoadInfo();
            //it.NhanVienCls o;
            if (this.MaNV != 0)
            {
                objNV = db.DoiTacDangKies.Single(p => p.ID == this.MaNV);
                txtHoTen.Text = objNV.Name;
                txtDienThoai.Text = objNV.Phone;
                txtEmail.Text = objNV.Email;
                txtMaSo.Text = objNV.Username;
                lookUpNhomKD.EditValue = objNV.MaNKD;
                lookUpPhongBan.EditValue = objNV.MaPB;
                chkClock.Checked = (bool)objNV.isLock;
            }
            else
            {
                objNV = new ThuVien.DoiTacDangKy();

                lookUpPhongBan.ItemIndex = 0;
                lookUpNhomKD.ItemIndex = 0;
                txtMaSo.EditValue = db.DinhDang(8, (db.NhanViens.Max(p => (int?)p.MaNV) ?? 0) + 1);
            }

            txtHoTen.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {

            if (txtHoTen.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập [Họ tên], xin cảm ơn");
                txtHoTen.Focus();
                return;
            }
            var maSo = txtMaSo.Text.Trim();
            if (maSo == "")
            {
                DialogBox.Infomation("Vui lòng nhập [Username], xin cảm ơn");
                txtMaSo.Focus();
                return;
            }
            else
            {
                var count = db.DoiTacDangKies.Where(p => p.Username == maSo & p.ID != objNV.ID).Count();
                if (count > 0)
                {
                    DialogBox.Error("Trùng [Username]. Vui lòng kiểm tra lại!");
                    txtMaSo.Focus();
                    return;
                }
            }


            var email = txtEmail.Text.Trim();
            if (email != "")
            {
                var count = db.DoiTacDangKies.Where(p => p.Email == email & p.ID != objNV.ID).Count();
                if (count > 0)
                {
                    DialogBox.Error("Trùng [Email]. Vui lòng kiểm tra lại!");
                    txtMaSo.Focus();
                    return;
                }
            }


            objNV.Name = txtHoTen.Text;
            objNV.isLock = chkClock.Checked;
            objNV.Phone = txtDienThoai.Text;
            objNV.Email = txtEmail.Text;
            objNV.MaNKD = (byte?)lookUpNhomKD.EditValue;
            objNV.MaPB = (byte?)lookUpPhongBan.EditValue;
            objNV.Username = txtMaSo.Text;
            objNV.DateCreate = DateTime.Now;
            if (MaNV == 0)
            {
                objNV.Password = it.CommonCls.MaHoa(txtMaSo.Text.Trim());
                db.DoiTacDangKies.InsertOnSubmit(objNV);
            }

            db.SubmitChanges();
            this.MaNV = objNV.ID;
            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }

        private void picSignature_DoubleClick(object sender, EventArgs e)
        {

        }

        private void picAvatar_DoubleClick(object sender, EventArgs e)
        {

        }

    }
}