using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Net.Mail;
using it;
using System.Threading;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.HoatDong.MGL.GiaoDich
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int MaBC, MaMT, MaGD;
        MasterDataContext db = new MasterDataContext();
        mglgdGiaoDich objGD;
        public bool IsEdit = true;
        public mglBCCongViec objCV;

        public frmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            btnThucHien.Enabled = IsEdit;
            lookNhanVien.DataSource = db.NhanViens.Where(p => p.MaTinhTrang == 1).Select(p => new { p.MaNV, p.HoTen });
            lookTrangThai.Properties.DataSource = db.mglgdTrangThais.Where(p => p.MaTT == 1 || p.MaTT == 2);
            if (this.MaGD > 0)
            {
                objGD = db.mglgdGiaoDiches.Single(p => p.MaGD == this.MaGD);
                txtSoGD.EditValue = objGD.SoGD;
                dateNgayGD.EditValue = objGD.NgayGD;
                dateNgayDatCoc.EditValue = objGD.NgayDatCoc;
                spinThoiHan.EditValue = objGD.ThoiHan;
                spinTienCoc.EditValue = objGD.TienCoc;
                dateNgayKyHD.EditValue = objGD.NgayKyHD;
                lookTrangThai.EditValue = objGD.MaTT;
                spinTienHoaHongB.EditValue = objGD.TienMGBenBan ?? 0;
                spinTienHoaHongMua.EditValue = objGD.TienMGBenMua ?? 0;
                spinPMG.EditValue = objGD.TienMG ?? 0;
                txtDienGiai.Text = objGD.DienGiai;
                gcNhanVien.Enabled = false;
            }
            else
            {
               
                objGD = new mglgdGiaoDich();
                db.mglgdGiaoDiches.InsertOnSubmit(objGD);
                string soGD = "";
                db.mglgdGiaoDich_TaoSoPhieu(ref soGD);
                lookTrangThai.EditValue = 1;
                txtSoGD.EditValue = soGD;
                dateNgayGD.EditValue = DateTime.Now;
                if (objCV != null)
                {
                    spinTienHoaHongB.EditValue = objCV.TienMGBenBan ?? 0;
                    spinTienHoaHongMua.EditValue = objCV.TienMGBenMua ?? 0;
                    spinPMG.EditValue = (objCV.TienMGBenMua ?? 0) + (objCV.TienMGBenBan ?? 0);
                }
            }
            gcNhanVien.DataSource = objGD.mglgdHHNhanViens;
            if (this.MaGD <= 0 && objCV != null)
            {
                foreach (var p in objCV.mglcvNhanViens)
                {
                    gvNhanVien.AddNewRow();
                    gvNhanVien.SetFocusedRowCellValue("TongTien", p.TongTien);
                    gvNhanVien.SetFocusedRowCellValue("MaNV", p.MaNV);
                    gvNhanVien.SetFocusedRowCellValue("TyLe", p.TyLe);
                    gvNhanVien.SetFocusedRowCellValue("SoTien", p.SoTien);
                    gvNhanVien.SetFocusedRowCellValue("DienGiai", p.DienGiai);

                }
            }
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            if (txtSoGD.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập số phiếu");
                txtSoGD.Focus();
                return;
            }
            else
            {
                int rowCount = db.mglgdGiaoDiches.Where(p => p.SoGD.ToLower() == txtSoGD.Text.ToLower() & p.MaGD != this.MaGD).Count();
                if (rowCount > 0)
                {
                    DialogBox.Error("Số phiếu đã có trong hệ thống, vui lòng nhập lại");
                    txtSoGD.Focus();
                    return;
                }
            }
            if (lookTrangThai.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn tráng thái giao dịch!");
                lookTrangThai.Focus();
                return;
            }
            if (dateNgayGD.Text == "")
            {
                DialogBox.Error("Vui lòng nhập ngày giao dịch");
                dateNgayGD.Focus();
                return;
            }

            objGD.SoGD = txtSoGD.Text;
            objGD.NgayGD = dateNgayGD.DateTime;
            objGD.ThoiHan = Convert.ToInt32(spinThoiHan.Value);
            objGD.TienCoc = spinTienCoc.Value;
            objGD.NgayKyHD = (DateTime?)dateNgayKyHD.EditValue;
            objGD.DienGiai = txtDienGiai.Text.Trim();
            objGD.NgayDatCoc = (DateTime?)dateNgayDatCoc.EditValue;
            objGD.TienMGBenBan = spinTienHoaHongB.Value;
            objGD.TienMGBenMua = spinTienHoaHongMua.Value;
            objGD.MaNV = Common.StaffID;
            objGD.TienMG = spinPMG.Value;
            objGD.MaTT =(byte?)lookTrangThai.EditValue;
            if (this.MaGD == 0)
            {
                var objBC = db.mglbcBanChoThues.FirstOrDefault(p=>p.MaBC == MaBC);
                var objMT = db.mglmtMuaThues.FirstOrDefault(p => p.MaMT == MaMT);
                objGD.MaBC = MaBC;
                objGD.MaMT = MaMT;
                objGD.MaNVBC = objBC.MaNVKD;
                objGD.MaNVMT = objMT.MaNVKD;
                objGD.MaNV = Common.StaffID;
                if (objGD.MaTT == 1)
                {
                    objBC.MaTT = 1;
                    objMT.MaTT = 1;
                }
                if (objGD.MaTT == 2)
                {
                    objBC.MaTT = 4;
                    objMT.MaTT = 4;
                }
            }
            db.SubmitChanges();
            DialogBox.Infomation("Giao dịch đã được thực hiện. Vui lòng chờ duyệt");

            this.DialogResult = DialogResult.OK;
            this.Close();

            //
            #region gửi mail
            try
            {
                Thread th = new Thread(new ThreadStart(ThreadSendMail));
                th.ApartmentState = ApartmentState.MTA;
                th.IsBackground = true;
                th.Start();
            }
            catch { }
            #endregion
        }

        private void ThreadSendMail()
        {
            var objCOmpany = db.Companies.FirstOrDefault();
            var objListMailTo = db.mglmMailDangKyNhans.Where(p => p.IsThemKH == true);
            if (objListMailTo == null)
                return;
            var objConfig = db.mailConfigs.FirstOrDefault(p => p.IsNoiBo == true);
            if (objConfig == null)
                return;
            foreach (var m in objListMailTo)
            {
                try
                {
                    MailProviderCls objMail = new MailProviderCls();
                    var objMailForm = new MailAddress(objConfig.Email, objConfig.Username);
                    objMail.MailAddressFrom = objMailForm;
                    var objMailTo = new MailAddress(m.NhanVien.Email, m.NhanVien.HoTen);
                    objMail.MailAddressTo = objMailTo;
                    objMail.SmtpServer = objConfig.Server;
                    objMail.EnableSsl = objConfig.EnableSsl.Value;
                    objMail.Port = objConfig.Port ?? 465;
                    objMail.PassWord = EncDec.Decrypt(objConfig.Password);
                    objMail.Subject = objCOmpany == null ? "Nhân viên (nhập mới/chỉnh sửa) giao dịch" : string.Format("Nhân viên công ty {0} (thêm mới/chỉnh sửa) giao dịch.", objCOmpany.TenCT);
                    string Content = string.Format(" Nhân viên xử lý : {0} \r\n Nội dung công việc: Thêm mới/chỉnh sửa giao dịch BDS  \r\n Số phiếu: {1} \r\n Loại BDS: {2} \r\n Mã BDS: {3} \r\n Ngày xử lý: {4:dd/MM/yyyy-hh:mm:ss tt} \r\n Bên mua: {5} \r\n Bên bán: {6}",
                        objGD.NhanVien.HoTen, objGD.SoGD, objGD.mglbcBanChoThue.LoaiBD.TenLBDS, objGD.mglbcBanChoThue.KyHieu, objGD.NgayGD, objGD.mglmtMuaThue.KhachHang.IsPersonal == true ? objGD.mglmtMuaThue.KhachHang.HoKH + " " + objGD.mglmtMuaThue.KhachHang.TenKH : objGD.mglmtMuaThue.KhachHang.TenCongTy,
                        objGD.mglbcBanChoThue.KhachHang.IsPersonal == true ? objGD.mglbcBanChoThue.KhachHang.HoKH + " " + objGD.mglbcBanChoThue.KhachHang.TenKH : objGD.mglbcBanChoThue.KhachHang.TenCongTy);
                    objMail.Content = Content;
                    objMail.SendMailV3();
                    Thread.Sleep(2);
                }
                catch { }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvNhanVien_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvNhanVien.SetFocusedRowCellValue("TongTien", (decimal?)spinPMG.EditValue);
        }

        private void gvNhanVien_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "TyLe" || e.Column.FieldName=="TongTien")
            {
                var sotien=(decimal?)gvNhanVien.GetFocusedRowCellValue("TyLe")*(decimal?)gvNhanVien.GetFocusedRowCellValue("TongTien")/100;
                gvNhanVien.SetFocusedRowCellValue("SoTien", sotien);
            }
        }
    }
}