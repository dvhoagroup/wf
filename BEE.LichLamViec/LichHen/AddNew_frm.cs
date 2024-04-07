using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.UI;
using System.Linq;
using BEE.ThuVien;

namespace BEEREMA.CongViec.LichHen
{
    public partial class AddNew_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaLH = 0;
        public bool IsUpdate = false, IsEdit = true, IsAdd = true;
        public int MaNC = 0, MaHD = 0, MaKH = 0, MaNVu = 0;
        public string NhiemVu = "", KhachHang = "", CoHoi = "", HopDong = "";
        int MaNV = Common.StaffID;

        public int? MaBCSP { get; set; }
        public int? MaLoai { get; set; }
        SchedulerControl control;
        Appointment apt;
        bool openRecurrenceForm = false;
        int suspendUpdateCount;
        MyAppointmentFormController controller;
        protected AppointmentStorage Appointments { get { return control.Storage.Appointments; } }

        public AddNew_frm(int? maLH, int? maNVu, int? maKH, int? maNC, int? maHD, string hoTenKH)
        {
            InitializeComponent();

            this.control = new SchedulerControl();
            this.control.Storage = new SchedulerStorage();
            this.control.Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaLH", "MaLH"));
            this.control.Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaKH", "MaKH"));
            this.control.Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaNC", "MaNC"));
            this.control.Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaHD", "MaHD"));
            this.control.Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaNVu", "MaNVu"));
            this.control.Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("HoTenKH", "HoTenKH"));
            this.apt = this.control.Storage.CreateAppointment(AppointmentType.Normal);
            if (maLH != null)
            {
                this.apt.CustomFields["MaLH"] = maLH;
            }
            else
            {
                this.apt.CustomFields["MaKH"] = maKH;
                this.apt.CustomFields["MaNC"] = maNC;
                this.apt.CustomFields["MaHD"] = maHD;
                this.apt.CustomFields["MaNVu"] = maNVu;
                MaNC = maNC ?? 0;
                MaHD = maHD ?? 0;
                MaKH = maKH ?? 0;
                MaNVu = maNVu ?? 0;
                KhachHang = hoTenKH;
                this.apt.CustomFields["HoTenKH"] = hoTenKH;
                this.apt.Start = DateTime.Now;
                this.apt.End = DateTime.Now.AddDays(1);
            }
            this.control.Storage.Appointments.Add(this.apt);
            this.controller = new MyAppointmentFormController(control, apt);

            this.control.Storage.Appointments.Statuses.Clear();
            DataTable tblTable1 = it.CommonCls.Table("Select * from LichHen_ThoiDiem order by STT");
            foreach (DataRow r1 in tblTable1.Rows)
                this.control.Storage.Appointments.Statuses.Add(Color.FromArgb((int)r1["MaTD"]), r1["TenTD"].ToString());
            this.control.Storage.Appointments.Labels.Clear();
            DataTable tblTable = it.CommonCls.Table("select * from LichHen_ChuDe order by STT");
            foreach (DataRow r in tblTable.Rows)
                this.control.Storage.Appointments.Labels.Add(Color.FromArgb((int)r["MaCD"]), r["TenCD"].ToString());
        }

        public AddNew_frm(int? maLH, int? maKH, string hoTenKH)
        {
            InitializeComponent();

            this.control = new SchedulerControl();
            this.control.Storage = new SchedulerStorage();
            this.control.Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaLH", "MaLH"));
            this.control.Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaKH", "MaKH"));
            this.control.Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("HoTenKH", "HoTenKH"));
            this.apt = this.control.Storage.CreateAppointment(AppointmentType.Normal);
            if (maLH != null)
            {
                this.apt.CustomFields["MaLH"] = maLH;
            }
            else
            {
                this.apt.CustomFields["MaKH"] = maKH;
                this.apt.CustomFields["HoTenKH"] = hoTenKH;
                MaKH = maKH ?? 0;
                KhachHang = hoTenKH;
                this.apt.Start = DateTime.Now;
                this.apt.End = DateTime.Now.AddDays(1);
            }
            this.control.Storage.Appointments.Add(this.apt);
            this.controller = new MyAppointmentFormController(control, apt);

            this.control.Storage.Appointments.Statuses.Clear();
            DataTable tblTable1 = it.CommonCls.Table("Select * from LichHen_ThoiDiem order by STT");
            foreach (DataRow r1 in tblTable1.Rows)
                this.control.Storage.Appointments.Statuses.Add(Color.FromArgb((int)r1["MaTD"]), r1["TenTD"].ToString());
            this.control.Storage.Appointments.Labels.Clear();
            DataTable tblTable = it.CommonCls.Table("select * from LichHen_ChuDe order by STT");
            foreach (DataRow r in tblTable.Rows)
                this.control.Storage.Appointments.Labels.Add(Color.FromArgb((int)r["MaCD"]), r["TenCD"].ToString());
        }

        public AddNew_frm(SchedulerControl control, Appointment apt, bool openRecurrenceForm)
        {
            InitializeComponent();

            this.openRecurrenceForm = openRecurrenceForm;
            this.controller = new MyAppointmentFormController(control, apt);
            this.apt = apt;
            this.control = control;
        }

        void LoadDictionary()
        {
            lookUpRemine.Properties.DataSource = it.CommonCls.Table("select * from Times");
            lookUpRemine.ItemIndex = 0;
            lookUpRepeat.Properties.DataSource = it.CommonCls.Table("select * from Times");
            lookUpRepeat.ItemIndex = 0;
        }

        void LoadData()
        {
            var db = new MasterDataContext();
            try
            {
                var objLH = db.LichHens.Single(p => p.MaLH == this.MaLH);

                lblNguoiCapNhat.Caption = string.Format("Người cập nhật: {0} | {1:dd/MM/yyyy}", Common.StaffName, DateTime.Now);
                lblNguoiTao.Caption = string.Format("Người tạo: {0} | {1:dd/MM/yyyy}", objLH.NhanVien.HoTen, objLH.NgayBD);
                txtTieuDe.EditValue = objLH.TieuDe;
                txtDienGiai.EditValue = objLH.DienGiai;
                txtDiaDiem.EditValue = objLH.DiaDiem;
                dateNgayBD.EditValue = objLH.NgayBD;
                dateNgayKT.EditValue = objLH.NgayKT;

                lookUpStatus.Storage = control.Storage;
                lookUpLabel.Storage = control.Storage;
                lookUpStatus.Status = Appointments.Statuses[objLH.LichHen_ThoiDiem.STT.GetValueOrDefault()];
                lookUpLabel.Label = Appointments.Labels[objLH.LichHen_ChuDe.STT.GetValueOrDefault()];

                btnRing.Tag = objLH.Rings;
                if (objLH.NhiemVu != null)
                {
                    btnNhiemVu.Tag = objLH.MaNVu;
                    btnNhiemVu.Text = objLH.NhiemVu.TieuDe;
                }
                else
                {
                    btnNhiemVu.Text = "";
                }
                if (objLH.KhachHang != null)
                {
                    btnKhachHang.Tag = objLH.MaKH;
                    btnKhachHang.EditValue = objLH.KhachHang.IsPersonal.Value ? (objLH.KhachHang.HoKH + " " + objLH.KhachHang.TenKH) : objLH.KhachHang.TenCongTy;
                }
                else
                {
                    btnKhachHang.Text = "";
                }

                chkRemine.EditValue = objLH.IsNhac;
                chkIsRepeat.EditValue = objLH.IsRepeat;
                if (objLH.IsNhac.GetValueOrDefault())
                {
                    lookUpRemine.Enabled = true;
                    lookUpRemine.EditValue = objLH.TimeID;
                }
                else
                    lookUpRemine.Enabled = false;
                if (objLH.IsRepeat.GetValueOrDefault())
                {
                    lookUpRepeat.Enabled = true;
                    lookUpRepeat.EditValue = objLH.TimeID;
                }
                else
                    lookUpRepeat.Enabled = false;
                MaKH = objLH.MaKH ?? 0;
                MaNVu = objLH.MaNVu ?? 0;
                MaNV = objLH.MaNV ?? Common.StaffID;

                string str = "";
                var list = db.LichHen_NhanViens.Where(p => p.MaLH == MaLH && !p.IsMain.GetValueOrDefault()).ToList();
                foreach (var l in list)
                    str += l.MaNV + "; ";
                str = str.TrimEnd(' ').TrimEnd(';');
                cmbNhanVien.SetEditValue(str);

                btnCategory.Visible = false;
                lblCateName.Visible = false;

                if (objLH.MaNV != Common.StaffID)
                    this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Infomation(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        int GetLabel(int _MaCD)
        {
            it.LichHen_ChuDeCls o = new it.LichHen_ChuDeCls(_MaCD);
            return o.STT;
        }

        int GetStatus(int _MaTD)
        {
            it.LichHen_ThoiDiemCls o = new it.LichHen_ThoiDiemCls(_MaTD);
            return o.STT;
        }

        string GetNhiemVu(int _MaNVu)
        {
            it.NhiemVuCls o = new it.NhiemVuCls(_MaNVu);
            o.MaNVu = _MaNVu;
            return o.GetNhiemVu();
        }

        string GetCustumer(int _MaKH)
        {
            it.KhachHangCls o = new it.KhachHangCls();
            o.MaKH = _MaKH;
            return o.GetCustomer();
        }

        private void AddNew_frm_Load(object sender, EventArgs e)
        {
            LoadDictionary();
            lookUpLabel.Storage = control.Storage;
            lookUpStatus.Storage = control.Storage;

            using (var db = new MasterDataContext())
            {
                cmbNhanVien.Properties.DataSource = db.NhanViens.Where(p => p.MaNV != Common.StaffID && p.MaTT == 1).Select(p => new { p.MaNV, p.HoTen }).ToList();
            }

            if (this.apt.CustomFields["MaLH"] != null)
            {
                MaLH = int.Parse(this.apt.CustomFields["MaLH"].ToString());
                if (!IsEdit)
                    this.Close();
                LoadData();
            }
            else
            {
                if (!IsAdd)
                    this.Close();
                dateNgayBD.DateTime = apt.Start;
                dateNgayKT.DateTime = apt.End.AddDays(-1).AddHours(3);
                lblThoiHan.Visible = false;
                lookUpLabel.SelectedIndex = 0;
                lookUpStatus.SelectedIndex = 0;
                lookUpRemine.Enabled = false;
                lblNguoiCapNhat.Caption = string.Format("Người cập nhật: {0} | {1:dd/MM/yyyy}", Common.StaffName, DateTime.Now);
                lblNguoiTao.Caption = string.Format("Người tạo: {0} | {1:dd/MM/yyyy}", Common.StaffName, DateTime.Now);
                lookUpRemine.Enabled = false;
                lookUpRepeat.Enabled = false;

                if (MaKH != 0)
                    btnKhachHang.Text = KhachHang;

                if (MaNVu != 0)
                    btnNhiemVu.Text = NhiemVu;

                btnCategory.Visible = false;
                lblCateName.Visible = false;
                btnNhiemVu.Width = 666;
            }
        }

        public class MyAppointmentFormController : AppointmentFormController
        {
            public string CustomName { get { return (string)EditedAppointmentCopy.CustomFields["CustomName"]; } set { EditedAppointmentCopy.CustomFields["CustomName"] = value; } }
            public string CustomStatus { get { return (string)EditedAppointmentCopy.CustomFields["CustomStatus"]; } set { EditedAppointmentCopy.CustomFields["CustomStatus"] = value; } }

            string SourceCustomName { get { return (string)SourceAppointment.CustomFields["CustomName"]; } set { SourceAppointment.CustomFields["CustomName"] = value; } }
            string SourceCustomStatus { get { return (string)SourceAppointment.CustomFields["CustomStatus"]; } set { SourceAppointment.CustomFields["CustomStatus"] = value; } }

            public MyAppointmentFormController(SchedulerControl control, Appointment apt)
                : base(control, apt)
            {
            }

            public override bool IsAppointmentChanged()
            {
                if (base.IsAppointmentChanged())
                    return true;
                return SourceCustomName != CustomName ||
                    SourceCustomStatus != CustomStatus;
            }

            protected override void ApplyCustomFieldsValues()
            {
                SourceCustomName = CustomName;
                SourceCustomStatus = CustomStatus;
            }
        }

        private void btnKhachHang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                var frm = new BEE.KhachHang.Find_frm();
                frm.ShowDialog();
                if (frm.MaKH != 0)
                {
                    btnKhachHang.Tag = frm.MaKH;
                    btnKhachHang.Text = frm.HoTen;
                    MaKH = frm.MaKH;
                }
            }
            else
            {
                btnKhachHang.Tag = 0;
                btnKhachHang.Text = "";
                MaKH = 0;
            }
        }

        private void chkRemine_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit _New = (CheckEdit)sender;
            if (_New.Checked)
                lookUpRemine.Enabled = true;
            else
                lookUpRemine.Enabled = false;
        }

        private void txtTieuDe_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit _New = (TextEdit)sender;
            this.Text = _New.Text + " - Lịch hẹn";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            #region "Header Form"
            if (txtTieuDe.Text.Trim() == "")
            {
                DialogBox.Infomation("Vui lòng nhập [Chủ đề], xin cảm ơn.");
                txtTieuDe.Focus();
                return;
            }
            #endregion

            #region "Add LichHen"
            it.LichHenCls o = new it.LichHenCls();
            o.KhachHang.MaKH = MaKH;
            o.TieuDe = txtTieuDe.Text;
            o.DienGiai = txtDienGiai.Text;
            o.DiaDiem = txtDiaDiem.Text;
            o.NgayBD = dateNgayBD.DateTime;
            o.NgayKT = dateNgayKT.DateTime;
            o.NhanVien.MaNV = MaNV;
            o.IsNhac = chkRemine.Checked;
            o.MaBCSP = this.MaBCSP ?? 0;
            o.MaLoai = this.MaLoai ?? 0;
            o.TimeID = byte.Parse(lookUpRemine.EditValue.ToString());
            try
            {
                o.NhiemVu.MaNVu = MaNVu;
            }
            catch { o.NhiemVu.MaNVu = 0; }
            o.ChuDe.TenCD = lookUpLabel.EditValue.ToString();
            o.ChuDe.MaCD = o.ChuDe.GetID();
            o.ThoiDiem.TenTD = lookUpStatus.EditValue.ToString();
            o.ThoiDiem.MaTD = o.ThoiDiem.GetID();
            if (btnRing.Tag != null)
                o.Rings = btnRing.Tag.ToString();
            else
                o.Rings = "";
            o.IsRepeat = chkIsRepeat.Checked;
            o.TimeID2 = byte.Parse(lookUpRepeat.EditValue.ToString());
            o.NhanVienStr = cmbNhanVien.Text;
            if (MaLH == 0)
                MaLH = o.Insert();
            else
            {
                o.MaLH = MaLH;
                o.MaNVCN = Common.StaffID;
                o.Update();
            }
            #endregion

            using (var db = new MasterDataContext())
            {
                var list = db.LichHen_NhanViens.Where(p => p.MaLH == MaLH && !p.IsMain.GetValueOrDefault());
                db.LichHen_NhanViens.DeleteAllOnSubmit(list);
                db.SubmitChanges();

                string[] lbds = cmbNhanVien.Properties.GetCheckedItems().ToString().Split(';');
                if (lbds[0] != "")
                {
                    foreach (var i in lbds)
                    {
                        var obj = new LichHen_NhanVien();
                        obj.MaLH = MaLH;
                        obj.MaNV = int.Parse(i);                                                
                        obj.IsMain = false;
                        obj.IsNhac = chkRemine.Checked;
                        obj.DaNhac = false;
                        obj.NgayNhac = dateNgayBD.DateTime;

                        db.LichHen_NhanViens.InsertOnSubmit(obj);
                    }
                    db.SubmitChanges();
                }
            }

            controller.SetStatus(lookUpStatus.Status);
            controller.SetLabel(lookUpLabel.Label);
            controller.Start = dateNgayBD.DateTime;
            controller.End = dateNgayKT.DateTime;
            controller.Description = txtDienGiai.Text;
            controller.Subject = txtTieuDe.Text;

            controller.ApplyChanges();
            IsUpdate = true;
            DialogBox.Infomation();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRing_Click(object sender, EventArgs e)
        {
            NhiemVu.Rings_frm frm = new NhiemVu.Rings_frm();
            if (btnRing.Tag != null)
                frm.Rings = btnRing.Tag.ToString();
            frm.ShowDialog();
            if (frm.IsUpdate)
                btnRing.Tag = frm.Rings;
        }

        private void chkIsRepeat_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit _New = (CheckEdit)sender;
            if (_New.Checked)
                lookUpRepeat.Enabled = true;
            else
                lookUpRepeat.Enabled = false;
        }

        private void btnNhiemVu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                NhiemVu.Select_frm frm = new BEEREMA.CongViec.NhiemVu.Select_frm();
                frm.ShowDialog();
                if (frm.MaNVu != 0)
                {
                    MaNVu = frm.MaNVu;
                    btnNhiemVu.Text = frm.TieuDe;
                }
            }
            else
            {
                MaNVu = 0;
                btnNhiemVu.Text = "";
            }
        }

        private void txtDienGiai_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}