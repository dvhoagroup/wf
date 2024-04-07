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

namespace BEE.HoatDong.MGL.XuLy
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? MaCV { get; set; }
        public int? MaCoHoiMT { get; set; }
        public List<int> ListSP;
        mglBCCongViec objSave;
        MasterDataContext db;
        mglbcBanChoThue objBC;
        mglmtMuaThue objMT;

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ThreadSendMail()
        {
            var objCOmpany = db.Companies.FirstOrDefault();
            var objListMailTo = db.mglmMailDangKyNhans.Where(p => p.IsThemSP == true);
            if (objListMailTo == null)
                return;
            var objConfig = db.mailConfigs.FirstOrDefault(p => p.IsNoiBo == true);
            if (objConfig == null)
                return;
            foreach (var m in objListMailTo)
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
                objMail.Subject = objCOmpany == null ? "Nhân viên nhập mớ báo cáo công việc" : string.Format("Nhân viên công ty {0} nhập mới báo cáo công việc.", objCOmpany.TenCT);
                string Content = string.Format(" Nhân viên xử lý : {0} \r\n Nội dung công việc: Thêm mới báo cáo công việc  \r\n Khách hàng: {1} \r\n Ngày xử lý: {2:dd/MM/yyyy-hh:mm:ss tt}",
                    objSave.NhanVien.HoTen, objSave.mglmtMuaThue.KhachHang.IsPersonal == true ? objSave.mglmtMuaThue.KhachHang.HoKH + " " + objSave.mglmtMuaThue.KhachHang.TenKH : objSave.mglmtMuaThue.KhachHang.TenCongTy, objSave.NgayNhap);
                objMail.Content = Content;
                objMail.SendMailV3();
                Thread.Sleep(2);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                objSave.NgayXuLy = (DateTime?)dateNgayXL.EditValue;
                objSave.DienGiai = txtNoiDung.Text;
                objSave.TienMGBenMua = (decimal?)spinTienMGMua.EditValue;
                objSave.TienMGBenBan = (decimal?)spinTienMGBan.EditValue;
                objSave.TongTienMG = (decimal?)spinTongTienMG.EditValue;
                if (MaCV == null)
                {
                    objSave.MaNV = Common.StaffID;
                    objSave.NgayNhap = db.GetSystemDate();
                    objSave.MaCoHoiMT = MaCoHoiMT;
                    db.SubmitChanges();
                    var list = new List<mglBCSanPham>();
                    foreach (var sp in ListSP)
                    {
                        var obj = new mglBCSanPham();
                        obj.MaCV = objSave.ID;
                        obj.MaSP = sp;
                        obj.MaTT = 1;
                        list.Add(obj);
                    }
                    db.mglBCSanPhams.InsertAllOnSubmit(list);
                   // objSave.mglmtMuaThue.MaTT = 2;
                    objMT.MaTT = 2;
                    foreach (var p in objSave.mglBCSanPhams)
                    {
                        var obj = db.mglbcBanChoThues.FirstOrDefault(t => t.MaBC == p.MaSP);
                        obj.MaTT = 2;
                    }
                }
                else
                {
                    objSave.NgayXuLy = (DateTime?)dateNgayXL.EditValue;
                    objSave.DienGiai = txtNoiDung.Text;
                }

                #region Lập lịch hẹn cho nhân vên cùng nhóm
                //it.LichHenCls o = new it.LichHenCls();
                //o.KhachHang.MaKH = (int)objSave.mglmtMuaThue.MaKH;
                //o.TieuDe = string.Format("Nhân viên lập báo cáo đưa khách đi xem");
                //o.DienGiai = "Báo cáo cho trưởng phòng và quản lý!";
                //o.DiaDiem = "Hệ thống phần mềm nội bộ";
                //o.NgayBD = db.GetSystemDate();
                //o.NgayKT = db.GetSystemDate().AddDays(1);
                //o.NhanVien.MaNV = (int)objSave.MaNV;
                //o.IsNhac = true;
                //o.TimeID = Convert.ToByte(10);
                //o.NhiemVu.MaNVu = 0;
                //o.Rings = "";
                //o.ChuDe.TenCD = "Cần chuẩn bị";
                //o.ChuDe.MaCD = -2572328;
                //o.ThoiDiem.TenTD = "Dự kiến";
                //o.ThoiDiem.MaTD = -256;
                //o.IsRepeat = true;
                //o.TimeID2 = Convert.ToByte(10);
                //var ListNV = db.NhanViens.Where(p => p.PerID == Common.GroupID);
                //var ListNVInsert = new List<Library.LichHen_NhanVien>();
                //if (ListNV != null)
                //{
                //    string stNV = "";
                //    foreach (var nv in ListNV)
                //    {
                //        stNV += nv.HoTen + "; ";

                //    }
                //    o.NhanVienStr = stNV;
                //}
                //var MaLH = o.Insert();
                //foreach (var nv in ListNV)
                //{
                //    var obj = new Library.LichHen_NhanVien();
                //    obj.MaLH = MaLH;
                //    obj.MaNV = nv.MaNV;
                //    obj.IsMain = false;
                //    obj.IsNhac = true;
                //    obj.DaNhac = false;
                //    obj.NgayNhac = db.GetSystemDate();
                //    ListNVInsert.Add(obj);
                //}
                //db.LichHen_NhanViens.InsertAllOnSubmit(ListNVInsert);
                #endregion
                db.SubmitChanges();
                DialogBox.Infomation("Dữ liệu đã được lưu!");
                this.Close();
                //gửi mail
                Thread th = new Thread(new ThreadStart(ThreadSendMail));
                th.ApartmentState = ApartmentState.MTA;
                th.IsBackground = true;
                th.Start();
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
            lookNhanVien.DataSource = db.NhanViens.Where(p=>p.MaTinhTrang == 1).Select(p => new { p.MaNV, p.HoTen });
            if (MaCV != null)
            {
                objSave = db.mglBCCongViecs.FirstOrDefault(p => p.ID == MaCV);
                objMT = db.mglmtMuaThues.FirstOrDefault(p => p.MaMT == objSave.MaCoHoiMT);
                objBC = objSave.mglBCSanPhams.FirstOrDefault().mglbcBanChoThue;
                txtNoiDung.Text = objSave.DienGiai;
                dateNgayXL.EditValue = objSave.NgayXuLy;
                spinTienMGBan.EditValue = objSave.TienMGBenBan ?? 0;
                spinTienMGMua.EditValue = objSave.TienMGBenMua ?? 0;

            }
            else
            {
                objMT = db.mglmtMuaThues.FirstOrDefault(p => p.MaMT == MaCoHoiMT);
                objBC = db.mglbcBanChoThues.First(p => p.MaBC == ListSP.First());
                objSave = new mglBCCongViec();
                db.mglBCCongViecs.InsertOnSubmit(objSave);
                dateNgayXL.EditValue = DateTime.Now;
                spinTienMGMua.EditValue = objMT.PhiMG ?? 0;
                spinTienMGBan.EditValue = objBC.PhiMG ?? 0;
            }
            gcNhanVien.DataSource = objSave.mglcvNhanViens;
        }

        private void spinTienMGMua_EditValueChanged(object sender, EventArgs e)
        {
            spinTongTienMG.Value = spinTienMGBan.Value + spinTienMGMua.Value;
        }

        private void gvNhanVien_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "TyLe" || e.Column.FieldName == "TongTien")
            {
                var sotien = Convert.ToDecimal(gvNhanVien.GetFocusedRowCellValue("TyLe")) * Convert.ToDecimal(gvNhanVien.GetFocusedRowCellValue("TongTien")) / 100;
                gvNhanVien.SetFocusedRowCellValue("SoTien", sotien);
            }
        }

        private void gvNhanVien_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvNhanVien.SetFocusedRowCellValue("TongTien", (decimal?)spinTongTienMG.EditValue);
            gvNhanVien.SetFocusedRowCellValue("TyLe", 0);
        }
    }
}