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

namespace BEE.HoatDong.MGL.Mua.GiaoDich
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int MaBC, MaMT, MaGD, MaKH;
        public decimal? Tien { get; set; }
        MasterDataContext db = new MasterDataContext();
        GiaoDichj objGD;
        public bool IsEdit = true;
        public mglBCCongViec objCV;

        public frmEdit()
        {
            InitializeComponent();
            spTyLe.EditValueChanged += new EventHandler(spTyLe_EditValueChanged);
            gvNhanVien.KeyUp += new KeyEventHandler(gvNhanVien_KeyUp);
        }

        void gvNhanVien_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.Question() == System.Windows.Forms.DialogResult.No) return;
                gvNhanVien.DeleteSelectedRows();
            }
        }

        void spTyLe_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                gvNhanVien.SetFocusedRowCellValue("TyLe", ((SpinEdit)sender).Value);
                var pt_tt = (decimal?)gvNhanVien.GetFocusedRowCellValue("TyLe");
                var t = (decimal?)spinTongTien.EditValue *pt_tt * 0.01M;
                gvNhanVien.SetFocusedRowCellValue("SoTien", t);
               
            }
            catch
            {
            }
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            btnThucHien.Enabled = IsEdit;
            lookNhanVien.DataSource = db.NhanViens.Where(p => p.MaTinhTrang == 1).Select(p => new { p.MaNV, p.HoTen });
            lkXa.DataSource = db.Xas;
            lkHuyen.DataSource = db.Huyens;
           // txtHoTenKH.Properties.DataSource = db.KhachHangs.Select(p => new { p.MaKH, TenKH = p.HoKH + " " + p.TenKH,DiaChi=p.DCLL });
            txtHoTenKH.Properties.DataSource = db.mglmtMuaThues.Select(p => new {p.MaMT, p.MaKH, DiaChi = p.KhachHang.NguyenQuan, TenKH = p.KhachHang.HoKH + " " + p.KhachHang.TenKH ,p.KhachHang.DiDong}).ToList();
            if (this.MaGD > 0)
            {
                objGD = db.GiaoDichjs.Single(p => p.ID == this.MaGD);
                txtSoGD.EditValue = objGD.SoPhieu;
                dateNgayGD.EditValue = objGD.NgayGD;
                
                spinTongTien.EditValue = objGD.SoTien;

                //spinPhucLoi.EditValue = objGD.TienPhucLoi ?? 0;
                //spinHoaHongNV.EditValue = objGD.HHNhanVien ?? 0;
                spinCongTy.EditValue = objGD.TienCongTy ?? 0;
                txtDienGiai.Text = objGD.GhiChu;
                //spinNguon.EditValue = objGD.Nguon;
                //spinNVVP.EditValue = objGD.NVVP;
                //spinNVKD.EditValue = objGD.NVKD;
                txtHoTenKH.EditValue = objGD.MaKH;
                //gcNhanVien.Enabled = false;
              
            }
            else
            {
                objGD = new GiaoDichj();
                txtSoGD.Text = db.DinhDang(47, (db.GiaoDichjs.Max(p => (int?)p.ID) ?? 0) + 1);
                spinTongTien.EditValue = this.Tien;
                if (this.MaKH != null)
                {
                    txtHoTenKH.EditValue = this.MaKH;
                }
               
            }
            gcNhanVien.DataSource = objGD.mglgdHHNhanViens;
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
                int rowCount = db.GiaoDichjs.Where(p => p.SoPhieu.ToLower() == txtSoGD.Text.ToLower() & p.ID != this.MaGD).Count();
                if (rowCount > 0)
                {
                    DialogBox.Error("Số phiếu đã có trong hệ thống, vui lòng nhập lại");
                    txtSoGD.Focus();
                    return;
                }
            }
            
            if (dateNgayGD.Text == "")
            {
                DialogBox.Error("Vui lòng nhập ngày giao dịch");
                dateNgayGD.Focus();
                return;
            }
            if (txtHoTenKH.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                txtHoTenKH.Focus();
                return;

            }
            
            objGD.SoPhieu = txtSoGD.Text;
            objGD.NgayGD = dateNgayGD.DateTime;
            objGD.SoTien = (decimal?)spinTongTien.EditValue;
            objGD.GhiChu = txtDienGiai.Text.Trim();
            
            //objGD.TienPhucLoi = spinPhucLoi.Value;
            //objGD.HHNhanVien = spinHoaHongNV.Value;
            objGD.MaNV = Common.StaffID;
            objGD.TienCongTy = spinCongTy.Value;
            //objGD.Nguon = spinNguon.Value;
            //objGD.NVKD = spinNVKD.Value;
            objGD.NVVP = objGD.NVVP;
            //objGD.NVVP = spinNVVP.Value;
            objGD.MaBC = this.MaBC;
            objGD.MaKH = (int?)txtHoTenKH.EditValue;
         
            if (this.MaGD == 0)
            {
                objGD.MaMT = this.MaMT;
               
                db.GiaoDichjs.InsertOnSubmit(objGD);
            }
            db.SubmitChanges();
            DialogBox.Infomation("Giao dịch đã được thực hiện. Vui lòng chờ duyệt");

            this.DialogResult = DialogResult.OK;
            this.Close();

            //
            //#region gửi mail
            //try
            //{
            //    Thread th = new Thread(new ThreadStart(ThreadSendMail));
            //    th.ApartmentState = ApartmentState.MTA;
            //    th.IsBackground = true;
            //    th.Start();
            //}
            //catch { }
            //#endregion
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
                    //MailProviderCls objMail = new MailProviderCls();
                    //var objMailForm = new MailAddress(objConfig.Email, objConfig.Username);
                    //objMail.MailAddressFrom = objMailForm;
                    //var objMailTo = new MailAddress(m.NhanVien.Email, m.NhanVien.HoTen);
                    //objMail.MailAddressTo = objMailTo;
                    //objMail.SmtpServer = objConfig.Server;
                    //objMail.EnableSsl = objConfig.EnableSsl.Value;
                    //objMail.Port = objConfig.Port ?? 465;
                    //objMail.PassWord = EncDec.Decrypt(objConfig.Password);
                    //objMail.Subject = objCOmpany == null ? "Nhân viên (nhập mới/chỉnh sửa) giao dịch" : string.Format("Nhân viên công ty {0} (thêm mới/chỉnh sửa) giao dịch.", objCOmpany.TenCT);
                    //string Content = string.Format(" Nhân viên xử lý : {0} \r\n Nội dung công việc: Thêm mới/chỉnh sửa giao dịch BDS  \r\n Số phiếu: {1} \r\n Loại BDS: {2} \r\n Mã BDS: {3} \r\n Ngày xử lý: {4:dd/MM/yyyy-hh:mm:ss tt} \r\n Bên mua: {5} \r\n Bên bán: {6}",
                    //    objGD.NhanVien.HoTen, objGD.SoGD, objGD.mglbcBanChoThue.LoaiBD.TenLBDS, objGD.mglbcBanChoThue.KyHieu, objGD.NgayGD, objGD.mglmtMuaThue.KhachHang.IsPersonal == true ? objGD.mglmtMuaThue.KhachHang.HoKH + " " + objGD.mglmtMuaThue.KhachHang.TenKH : objGD.mglmtMuaThue.KhachHang.TenCongTy,
                    //    objGD.mglbcBanChoThue.KhachHang.IsPersonal == true ? objGD.mglbcBanChoThue.KhachHang.HoKH + " " + objGD.mglbcBanChoThue.KhachHang.TenKH : objGD.mglbcBanChoThue.KhachHang.TenCongTy);
                    //objMail.Content = Content;
                    //objMail.SendMailV3();
                    //Thread.Sleep(2);
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
            try
            {
                decimal? s = 0;
                for (int i = 0; i <gvNhanVien.RowCount-1; i++)
                {
                     s+= (decimal?)gvNhanVien.GetRowCellValue(i, "SoTien");
               
                }
                spinCongTy.EditValue = spinTongTien.Value - s;
            }
            catch
            {
            }
        }

        private void gvNhanVien_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.Column.FieldName == "TyLe" || e.Column.FieldName=="TongTien")
            //{
            //    var sotien=(decimal?)gvNhanVien.GetFocusedRowCellValue("TyLe")*(decimal?)gvNhanVien.GetFocusedRowCellValue("TongTien")/100;
            //    gvNhanVien.SetFocusedRowCellValue("SoTien", sotien);
            //}
        }

        private void spinTongTien_EditValueChanged(object sender, EventArgs e)
        {
           // spinCongTy.EditValue = spinTongTien.Value * 0.3M;
            //spinPhucLoi.EditValue = spinTongTien.Value*0.1M;
            //spinHoaHongNV.EditValue = spinTongTien.Value * 0.6M;
        }

        private void txtHoTenKH_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var ID = (int)searchLookUpEdit1View.GetFocusedRowCellValue("MaMT");
                this.MaMT = ID;
            }
            catch { }
        }

        private void gvNhanVien_RowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            //try
            //{
               
            //    for (int i = 0; i < gvNhanVien.RowCount-1; i++)
            //    {
            //        var s = (decimal?)gvNhanVien.GetRowCellValue(i, "TyLe");
            //        var t =  (decimal?)spinTongTien.EditValue / (s*0.1M);
            //        gvNhanVien.SetRowCellValue(i, "SoTien", t);
            //    }
            //}
            //catch
            //{
            //}
          

        }

        private void gvNhanVien_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {

                for (int i = 0; i < gvNhanVien.RowCount - 1; i++)
                {
                    var s = (decimal?)gvNhanVien.GetRowCellValue(i, "TyLe");
                    var t = (decimal?)spinTongTien.EditValue *s * 0.01M;
                    gvNhanVien.SetRowCellValue(i, "SoTien", t);
                }
            }
            catch
            {
            }
        }

        private void txtHoTenKH_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 1:
                    using (var frm = new Mua.frmEdit())
                    {
                        frm.isMua = true;
                        frm.ShowDialog();
                       
                            txtHoTenKH.Properties.DataSource = db.mglmtMuaThues.Select(p => new {p.MaMT, p.MaKH, DiaChi = p.KhachHang.NguyenQuan, TenKH = p.KhachHang.HoKH + " " + p.KhachHang.TenKH ,p.KhachHang.DiDong}).ToList();
                            txtHoTenKH.EditValue = frm.MaKH;
                       
                    }
                    break;
            }
           
           
        }
    }
}