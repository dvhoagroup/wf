using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;
using System.Linq;
using BEE.ThuVien;

namespace BEE.DuAn
{
    public partial class DuAn_frm : DevExpress.XtraEditors.XtraForm
    {
        public bool IsUpdate = false;
        public int MaDA = 0;
        string OldFileName = "";
        MasterDataContext db = new MasterDataContext();
        ThuVien.DuAn objDA;

        public DuAn_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadDA()
        {
            objDA = db.DuAns.Single(p => p.MaDA == this.MaDA);
            txtDiaChi.Text = objDA.DiaChi;
            txtMoTa.Text = objDA.TomTat;
            txtTenDA.Text = objDA.TenDA;
            txtTenThuongMai.Text = objDA.TenThuongMai;
            lookUpLoaiDA.EditValue = objDA.LoaiDA.MaLoaiDA;
            txtTenVietTat.Text = objDA.TenVietTat;
            txtNoiCap.Text = objDA.NoiCap;
            txtCodeSun.Text = objDA.CodeSUN;
            dateNgayCap.EditValue = objDA.NgayCap;
            txtSoGiayPhep.Text = objDA.SoGiayPhep;
            spinDienTich.EditValue = objDA.DienTich;
            btnMaXa.Tag = objDA.MaXa;
            if (objDA.IsProject == true)
            {
                chkIsProject.Checked = true;
            }
            else chkIsProject.Checked = false;
            spinSoLuongCot.EditValue = objDA.AmountColumn;
            SpinDoRongCot.EditValue = objDA.WidthCell;

            try
            {
                btnMaXa.Text = it.CommonCls.Row("Xa_getXaHuyenTinh " + objDA.MaXa)["DiaChi"].ToString();
            }
            catch { btnMaXa.Text = ""; }
        }

        private void DuAn_frm_Load(object sender, EventArgs e)
        {
            lookUpLoaiDA.Properties.DataSource = db.LoaiDAs;

            if (MaDA != 0)
            {
                LoadDA();
            }
            else
                objDA = new ThuVien.DuAn();

            //BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void btnMaXa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BEE.NghiepVuKhac.SelectPosition_frm frm = new BEE.NghiepVuKhac.SelectPosition_frm();
            try
            {
                frm.MaXa = int.Parse(btnMaXa.Tag.ToString());
            }
            catch { }
            frm.ShowDialog();
            if (frm.Result != "")
            {
                btnMaXa.Tag = frm.MaXa;
                btnMaXa.Text = frm.Result;
            }
        }

        private void btnFileAttach_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            Save();
        }

        void Save()
        {
            if (txtTenDA.Text == "")
            {
                DialogBox.Infomation("Please type [Tên dự án/ Project Name]. Thanks!");
                txtTenDA.Focus();
                return;
            }

            if (txtTenVietTat.Text == "")
            {
                DialogBox.Infomation("Please type [Tên viết tắt/ Short Name]. Thanks!");
                txtTenVietTat.Focus();
                return;
            }

            if (lookUpLoaiDA.Text == "")
            {
                DialogBox.Infomation("Please select [Loại dự án/ Type of Project]. Thanks!");
                lookUpLoaiDA.Focus();
                return;
            }

            if (txtDiaChi.Text == "")
            {
                DialogBox.Infomation("Please type [Địa chỉ/ Address]. Thanks!");
                txtDiaChi.Focus();
                return;
            }

            if (btnMaXa.Text == "")
            {
                DialogBox.Infomation("Please select [Xã (Commune), Huyện (District), Tỉnh (Province)]. Thanks!");
                btnMaXa.Focus();
                return;
            }

            if (DialogBox.Question("Bạn có muốn lưu dữ liệu không? \r\n\r\nAre you want to save data?") == System.Windows.Forms.DialogResult.No) return;

            #region GiayPhep
            //if (txtSoGiayPhep.Text == "")
            //{
            //    DialogBox.Infomation("Vui lòng nhập số giấy phép phê duyệt dự án. Xin cảm ơn");
            //    txtSoGiayPhep.Focus();
            //    return;
            //}

            //if (dateNgayCap.Text == "")
            //{
            //    DialogBox.Infomation("Vui lòng nhập ngày cấp giấy phép phê duyệt dự án. Xin cảm ơn");
            //    dateNgayCap.Focus();
            //    return;
            //}

            //if (txtNoiCap.Text == "")
            //{
            //    DialogBox.Infomation("Vui lòng nhập nơi cấp giấy phép phê duyệt dự án. Xin cảm ơn");
            //    txtNoiCap.Focus();
            //    return;
            //}
            #endregion

            //it.DuAnCls o = new it.DuAnCls();
            if (btnMaXa.Text != null)
                objDA.MaXa = int.Parse(btnMaXa.Tag.ToString());
            objDA.MaNV = Common.StaffID;
            objDA.MaLoaiDA = (byte?)lookUpLoaiDA.EditValue;
            objDA.AttachFile = "";
            objDA.DiaChi = txtDiaChi.Text;
            objDA.TenDA = txtTenDA.Text;
            objDA.TenThuongMai = txtTenThuongMai.Text;
            objDA.TomTat = txtMoTa.Text;
            objDA.TenVietTat = txtTenVietTat.Text;
            objDA.SoGiayPhep = txtSoGiayPhep.Text;
            objDA.NoiCap = txtNoiCap.Text;
            objDA.CodeSUN = txtCodeSun.Text.Trim();
            objDA.NgayCap = (DateTime?)dateNgayCap.EditValue;
            objDA.DienTich = double.Parse(spinDienTich.EditValue.ToString());
            objDA.IsProject = chkIsProject.Checked ? true : false;
            objDA.AmountColumn = Convert.ToInt32(spinSoLuongCot.EditValue);
            objDA.WidthCell = Convert.ToInt32(SpinDoRongCot.EditValue);
            try
            {
                if (MaDA == 0)
                {
                    objDA.NgayDang = DateTime.Now;
                    db.DuAns.InsertOnSubmit(objDA);
                }
                db.SubmitChanges();

                IsUpdate = true;
                DialogBox.Infomation("Dữ liệu đã được cập nhật thành công.");
            }
            catch (Exception ex)
            {
                DialogBox.Error("Code error: " + ex.Message);
            }
            this.Close();
        }

        private void DuAn_frm_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void DuAn_frm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
                Save();
            else
            {
                if (e.KeyCode == Keys.Escape)
                    this.Close();
            }
        }
    }
}