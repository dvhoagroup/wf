using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
using System.Linq;
using BEE.ThuVien;

namespace BEEREMA.CongViec.LichHen
{
    public partial class SchedulerList_ctl : DevExpress.XtraEditors.XtraUserControl
    {
        bool KT = false, KT1 = false;
        bool IsAdd = false, IsEdit = false, IsDelete = false;
        MasterDataContext db;

        public SchedulerList_ctl()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
        }

        int ThangDauCuaQuy(int Thang)
        {
            if (Thang <= 3)
                return 1;
            else if (Thang <= 6)
                return 4;
            else if (Thang <= 9)
                return 7;
            else
                return 10;
        }

        void SetToDate()
        {
            KT = false;
            KT1 = false;
            dateDenNgay.Enabled = false;
            dateTuNgay.Enabled = false;
            DateTime dateHachToan = DateTime.Now.Date;
            switch (cmbKyBC.SelectedIndex)
            {
                case 0: //Ngay nay
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = dateHachToan;

                    break;
                case 1: //Tuan nay
                    dateDenNgay.DateTime = dateHachToan.AddDays(7 - (int)dateHachToan.DayOfWeek);
                    dateTuNgay.DateTime = dateHachToan.AddDays(1 - (int)dateHachToan.DayOfWeek);

                    break;
                case 2: //Dau tuan den hien tai
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = dateHachToan.AddDays(1 - (int)dateHachToan.DayOfWeek);

                    break;
                case 3: //Thang nay
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddMonths(1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1);

                    break;
                case 4: //Dau thang den hien tai
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1);

                    break;
                case 5: //Quy nay
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month) + 2, 1).AddMonths(1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month), 1);

                    break;
                case 6: //Dau quy den hien tai
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month), 1);

                    break;
                case 7: //Nam nay
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 1, 1);

                    break;
                case 8: //Dau nam den hien tai
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 1, 1);

                    break;
                case 9: //Thang 1
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 2, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 1, 1);

                    break;
                case 10: //Thang 2
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 3, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 2, 1);

                    break;
                case 11: //Thang 3
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 3, 1);

                    break;
                case 12: //Thang 4
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 5, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1);

                    break;
                case 13: //Thang 5
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 6, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 5, 1);

                    break;
                case 14: //Thang 6
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 6, 1);

                    break;
                case 15: //Thang 7
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 8, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1);

                    break;
                case 16: //Thang 8
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 9, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 8, 1);

                    break;
                case 17: //Thang 9
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 9, 1);

                    break;
                case 18: //Thang 10
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 11, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1);

                    break;
                case 19: //Thang 11
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 11, 1);

                    break;
                case 20: //Thang 12
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 12, 1);

                    break;
                case 21: //Quy I
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 1, 1);

                    break;
                case 22: //Quy II
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1);

                    break;
                case 23: //Quy III
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1);

                    break;
                case 24: //Quy IV
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1);

                    break;
                case 25: //Tuan truoc
                    dateDenNgay.DateTime = dateHachToan.AddDays(-(int)dateHachToan.DayOfWeek);
                    dateTuNgay.DateTime = dateHachToan.AddDays(-(int)dateHachToan.DayOfWeek - 6);

                    break;
                case 26: //Thang truoc
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddMonths(-1);

                    break;
                case 27: //Quy truoc
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month), 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month), 1).AddMonths(-3);

                    break;
                case 28: //Nam truoc
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year - 1, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year - 1, 1, 1);

                    break;
                case 29: //Tuan sau
                    dateDenNgay.DateTime = dateHachToan.AddDays(14 - (int)dateHachToan.DayOfWeek);
                    dateTuNgay.DateTime = dateHachToan.AddDays(8 - (int)dateHachToan.DayOfWeek);

                    break;
                case 30: //Bon tuan sau
                    dateDenNgay.DateTime = dateHachToan.AddDays(35 - (int)dateHachToan.DayOfWeek);
                    dateTuNgay.DateTime = dateHachToan.AddDays(8 - (int)dateHachToan.DayOfWeek);

                    break;
                case 31: //Thang sau
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddMonths(2).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddMonths(1);

                    break;
                case 32: //Quy sau
                    switch (ThangDauCuaQuy(dateHachToan.Month))
                    {
                        case 10:
                            dateDenNgay.DateTime = new DateTime(dateHachToan.Year + 1, 4, 1).AddDays(-1);
                            dateTuNgay.DateTime = new DateTime(dateHachToan.Year + 1, 1, 1);
                            break;

                        case 1:
                            dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1).AddDays(-1);
                            dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1);
                            break;
                        case 4:

                            dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1).AddDays(-1);
                            dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1);
                            break;
                        case 7:

                            dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 31);
                            dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1);
                            break;
                    }
                    break;

                case 33: //Nam sau
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year + 1, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year + 1, 1, 1);

                    break;
                case 34: //Tu chon
                    dateDenNgay.Enabled = true;
                    dateTuNgay.Enabled = true;
                    KT = true;
                    KT1 = true;
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = dateHachToan;

                    break;
            }
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            DataTable tblResult;
            it.LichHenCls o = new it.LichHenCls();
            switch (it.CommonCls.GetAccessData(BEE.ThuVien.Common.PerID, 67))
            {
                case 1://Tat ca
                    o.NgayBD = dateTuNgay.DateTime;
                    o.NgayKT = dateDenNgay.DateTime;
                    o.ChuDe.TenCD = lookUpChuDe.Text == "<Tất cả>" ? "%%" : lookUpChuDe.EditValue.ToString();
                    o.ThoiDiem.TenTD = lookUpThoiDiem.Text == "<Tất cả>" ? "%%" : lookUpThoiDiem.EditValue.ToString();
                    o.NhanVien.MaNV = lookUpNhanVien.Text == "<Tất cả>" ? 0 : Convert.ToInt32(lookUpNhanVien.EditValue);
                    tblResult = o.SelectAll();
                    this.schedulerStorage1.Appointments.DataSource = tblResult;
                    gcScheduler.DataSource = tblResult;
                    break;
                case 2://Theo phong ban
                    o.NgayBD = dateTuNgay.DateTime;
                    o.NgayKT = dateDenNgay.DateTime;
                    o.NhanVien.PhongBan.MaPB = BEE.ThuVien.Common.DepartmentID;
                    o.ChuDe.TenCD = lookUpChuDe.Text == "<Tất cả>" ? "%%" : lookUpChuDe.EditValue.ToString();
                    o.ThoiDiem.TenTD = lookUpThoiDiem.Text == "<Tất cả>" ? "%%" : lookUpThoiDiem.EditValue.ToString();
                    o.NhanVien.MaNV = lookUpNhanVien.Text == "<Tất cả>" ? 0 : Convert.ToInt32(lookUpNhanVien.EditValue);
                    tblResult = o.SelectByDepartment();
                    this.schedulerStorage1.Appointments.DataSource = tblResult;
                    gcScheduler.DataSource = tblResult;
                    break;
                case 3://Theo nhom
                    o.NgayBD = dateTuNgay.DateTime;
                    o.NgayKT = dateDenNgay.DateTime;
                    o.NhanVien.NKD.MaNKD = BEE.ThuVien.Common.GroupID;
                    o.ChuDe.TenCD = lookUpChuDe.Text == "<Tất cả>" ? "%%" : lookUpChuDe.EditValue.ToString();
                    o.ThoiDiem.TenTD = lookUpThoiDiem.Text == "<Tất cả>" ? "%%" : lookUpThoiDiem.EditValue.ToString();
                    o.NhanVien.MaNV = lookUpNhanVien.Text == "<Tất cả>" ? 0 : Convert.ToInt32(lookUpNhanVien.EditValue);
                    tblResult = o.SelectByGroup();
                    this.schedulerStorage1.Appointments.DataSource = tblResult;
                    gcScheduler.DataSource = tblResult;
                    break;
                case 4://Theo nhan vien
                    o.NgayBD = dateTuNgay.DateTime;
                    o.NgayKT = dateDenNgay.DateTime;
                    o.NhanVien.MaNV = BEE.ThuVien.Common.StaffID;
                    o.ChuDe.TenCD = lookUpChuDe.Text == "<Tất cả>" ? "%%" : lookUpChuDe.EditValue.ToString();
                    o.ThoiDiem.TenTD = lookUpThoiDiem.Text == "<Tất cả>" ? "%%" : lookUpThoiDiem.EditValue.ToString();
                    tblResult = o.SelectByStaff();
                    this.schedulerStorage1.Appointments.DataSource = tblResult;
                    gcScheduler.DataSource = tblResult;
                    break;
                default:
                    this.schedulerStorage1.Appointments.DataSource = null;
                    break;
            }

            schedulerStorage1.Appointments.Statuses.Clear();
            DataTable tblTable1 = it.CommonCls.Table("Select * from LichHen_ThoiDiem order by STT");
            foreach (DataRow r1 in tblTable1.Rows)
                schedulerStorage1.Appointments.Statuses.Add(Color.FromArgb((int)r1["MaTD"]), r1["TenTD"].ToString());

            schedulerStorage1.Appointments.Labels.Clear();
            DataTable tblTable = it.CommonCls.Table("select * from LichHen_ChuDe order by STT");
            foreach (DataRow r in tblTable.Rows)
                schedulerStorage1.Appointments.Labels.Add(Color.FromArgb((int)r["MaCD"]), r["TenCD"].ToString());

            
            this.schedulerStorage1.Appointments.Mappings.Start = "NgayBD";
            this.schedulerStorage1.Appointments.Mappings.End = "NgayKT";
            this.schedulerStorage1.Appointments.Mappings.Subject = "TieuDe";
            this.schedulerStorage1.Appointments.Mappings.Description = "DienGiai";
            this.schedulerStorage1.Appointments.Mappings.Label = "LabelId";
            this.schedulerStorage1.Appointments.Mappings.Location = "HoTenKH";
            this.schedulerStorage1.Appointments.Mappings.Status = "StatusId";
            this.schedulerStorage1.Appointments.Mappings.Status = "NhanVienStr";
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("StatusId", "StatusId"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("LabelId", "LabelId"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaLH", "MaLH"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaKH", "MaKH"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("HoTenKH", "HoTenKH"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("TenNVu", "TenNVu"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaNVu", "MaNVu"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("NhanVienStr", "NhanVienStr"));

            schedulerStorage1.EnableReminders = true;

            try
            {
                wait.Close();
            }
            catch { }
            finally
            {
                o = null;
                System.GC.Collect();
            }
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = BEE.ThuVien.Common.PerID;
            o.AccessData.Form.FormID = 67;
            DataTable tblAction = o.SelectBy();            

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            IsAdd = true;
                            break;
                        case 2:
                            IsEdit = true;
                            break;
                        case 3:
                            IsDelete = true;
                            break;
                    }
                }
            }
        }

        void LoadDictionary()
        {
            it.LichHen_ChuDeCls objCD = new it.LichHen_ChuDeCls();
            lookUpChuDe.Properties.DataSource = objCD.SelectAll();
            lookUpChuDe.ItemIndex = 0;
            objCD = null;

            it.LichHen_ThoiDiemCls objTD = new it.LichHen_ThoiDiemCls();
            lookUpThoiDiem.Properties.DataSource = objTD.SelectAll();
            lookUpThoiDiem.ItemIndex = 0;
            objTD = null;

            it.NhanVienCls objNV = new it.NhanVienCls();
            lookUpNhanVien.Properties.DataSource = objNV.SelectShowAll();
            lookUpNhanVien.ItemIndex = 0;
            objNV = null;
        }

        void LoadHistory(int maLH)
        {
            try
            {
                db = new MasterDataContext();

                var ls = db.lhLichSus.Where(o => o.MaLH == maLH)
                        .AsEnumerable()
                        .OrderByDescending(o => o.NgayNhap)
                        .Select((o, index) => new
                        {
                            STT = index + 1,
                            ID = o.ID,
                            MaLH = o.MaLH,
                            NgayNhap = o.NgayNhap,
                            HoTenNV = o.NhanVien.HoTen,
                            DienGiai = o.DienGiai,
                        }).ToList();
                gcLichSu.DataSource = ls;
            }
            catch { }
        }

        void LoadDataByPage()
        {
            try
            {
                int? MaLH = (int?)gvScheduler.GetFocusedRowCellValue("MaLH");
                LoadHistory(MaLH ?? 0);
            }
            catch { }
        }

        private void Sheduler_LichHen_ctl_Load(object sender, EventArgs e)
        {
            LoadDictionary();
            LoadPermission();
            cmbKyBC.SelectedIndex = 3;            
            LoadData();
            //LoadDataByPage();
        }

        private void schedulerControl1_EditAppointmentFormShowing(object sender, AppointmentFormEventArgs e)
        {
            Appointment apt = e.Appointment;

            bool openRecurrenceForm = apt.IsRecurring && schedulerStorage1.Appointments.IsNewAppointment(apt);
            
            AddNew_frm f = new AddNew_frm((SchedulerControl)sender, apt, openRecurrenceForm);
            f.IsEdit = IsEdit;
            f.IsAdd = IsAdd;
            f.LookAndFeel.ParentLookAndFeel = this.LookAndFeel.ParentLookAndFeel;
            e.DialogResult = f.ShowDialog();
            e.Handled = true;

            if (f.IsUpdate)
                LoadData();
        }

        private void schedulerControl1_AppointmentDrop(object sender, AppointmentDragEventArgs e)
        {
            //if (DialogBox.Infomation("Dữ liệu có thay đổi bạn có muốn lưu lại không?") == DialogResult.Yes)
            //{
            try
            {
                Appointment _New = (Appointment)e.EditedAppointment;
                it.LichHenCls o = new it.LichHenCls();
                o.NgayBD = _New.Start;
                o.NgayKT = _New.End;
                o.MaLH = int.Parse(_New.CustomFields["MaLH"].ToString());
                o.UpdateTime();
                // Load_Data();
            }
            catch { }
            //}
        }

        private void schedulerControl1_AppointmentResized(object sender, AppointmentResizeEventArgs e)
        {
            try
            {
                Appointment _New = (Appointment)e.EditedAppointment;
                it.LichHenCls o = new it.LichHenCls();
                o.NgayBD = _New.Start;
                o.NgayKT = _New.End;
                o.MaLH = int.Parse(_New.CustomFields["MaLH"].ToString());
                o.UpdateTime();
            }
            catch { }
        }

        private void schedulerStorage1_AppointmentsChanged(object sender, PersistentObjectsEventArgs e)
        {
            //if (DialogBox.Infomation("Dữ liệu có thay đổi bạn có muốn lưu lại không?") == DialogResult.Yes)
            //{
            try
            {
                Appointment app = (Appointment)e.Objects[0];
                it.LichHenCls o = new it.LichHenCls();
                o.TieuDe = app.Subject;
                o.MaLH = int.Parse(app.CustomFields["MaLH"].ToString());
                o.ThoiDiem.STT = (byte)app.StatusId;
                o.ChuDe.STT = (byte)app.LabelId;
                o.UpdateSubject();
                //Load_Data();
            }
            catch { }
            //}
        }

        private void cmbKyBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetToDate();
            try
            {
                schedulerControl1.Start = dateTuNgay.DateTime;
            }
            catch { }
        }

        private void schedulerControl1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            switch (e.Menu.Id)
            {
                case SchedulerMenuItemId.DefaultMenu:
                    SchedulerMenuItem add = e.Menu.GetMenuItemById(SchedulerMenuItemId.NewAppointment);
                    if (BEE.NgonNgu.Language.LangID == 1)
                    {
                        if (add != null)
                        {
                            add.Caption = "Thêm lịch hẹn";
                            //add.Image
                        }
                    }

                    if(!IsAdd)
                        e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewAppointment);

                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringEvent);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoToday);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoDate);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoThisDay);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewAllDayEvent);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringAppointment);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.TimeScaleEnable);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.TimeScaleVisible);
                    //e.Menu.RemoveMenuItem(SchedulerMenuItemId.SwitchViewMenu);
                    if (BEE.NgonNgu.Language.LangID == 1)
                    {
                        SchedulerPopupMenu switchs = e.Menu.GetPopupMenuById(SchedulerMenuItemId.SwitchViewMenu);
                        if (switchs != null)
                        {
                            switchs.Caption = "Kiểu lịch";
                            switchs.Items[0].Caption = "Lịch ngày";
                            switchs.Items[1].Caption = "Lịch tuần làm việc";
                            switchs.Items[2].Caption = "Lịch tuần";
                            switchs.Items[3].Caption = "Lịch tháng";
                            switchs.Items[4].Caption = "Lịch dòng thời gian";
                        }
                    }
                    break;
                case SchedulerMenuItemId.AppointmentMenu:
                    // Find the "Label As" item of the appointment popup menu and corresponding submenu.        
                    if (BEE.NgonNgu.Language.LangID == 1)
                    {
                        SchedulerPopupMenu label = e.Menu.GetPopupMenuById(SchedulerMenuItemId.LabelSubMenu);
                        if (label != null)
                        {
                            // Rename the item of the appointment popup menu.             
                            label.Caption = "Loại lịch hẹn";
                            // Rename the first item of the submenu.            
                            //submenu.Items[0].Caption = "Label 1";      
                        }

                        // Find the "Status As" item of the appointment popup menu and corresponding submenu.        
                        SchedulerPopupMenu status = e.Menu.GetPopupMenuById(SchedulerMenuItemId.StatusSubMenu);
                        if (status != null)
                            status.Caption = "Thời điểm liên hệ";

                        SchedulerMenuItem open = e.Menu.GetMenuItemById(SchedulerMenuItemId.OpenAppointment);
                        if (open != null)
                            open.Caption = "Xem thông tin";

                        SchedulerMenuItem delete = e.Menu.GetMenuItemById(SchedulerMenuItemId.DeleteAppointment);
                        if (delete != null)
                            delete.Caption = "Xóa lịch hẹn";
                    }

                    if(!IsDelete)
                        e.Menu.RemoveMenuItem(SchedulerMenuItemId.DeleteAppointment);

                    if (!IsEdit)
                    {
                        e.Menu.RemoveMenuItem(SchedulerMenuItemId.LabelSubMenu);
                        e.Menu.RemoveMenuItem(SchedulerMenuItemId.StatusSubMenu);
                        e.Menu.RemoveMenuItem(SchedulerMenuItemId.OpenAppointment);
                    }
                    break;
                case SchedulerMenuItemId.AppointmentDragMenu:
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.AppointmentDragCancel);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.AppointmentDragCopy);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.AppointmentDragMove);
                    break;
            }
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void schedulerStorage1_AppointmentDeleting(object sender, PersistentObjectCancelEventArgs e)
        {
            if (!IsDelete)
                e.Cancel = true;
            else
            {
                if (DialogBox.Question() == DialogResult.Yes)
                {
                    try
                    {
                        Appointment app = (Appointment)e.Object;
                        it.LichHenCls o = new it.LichHenCls(int.Parse(app.CustomFields["MaLH"].ToString()));
                        if (o.NhanVien.MaNV == BEE.ThuVien.Common.StaffID)
                        {
                            o.Delete();
                            LoadData();
                        }
                        else
                            DialogBox.Infomation("[Lịch hẹn] này không do bạn quản lý. Vui lòng kiểm trả lại, xin cảm ơn.");
                    }
                    catch {
                        DialogBox.Infomation("Xóa không thành công vì [Lịch hẹn] này đã được sử dụng. Vui lòng kiểm tra lại, xin cảm ơn.");
                    }
                }
                else
                    e.Cancel = true;
            }
        }

        private void schedulerStorage1_AppointmentChanging(object sender, PersistentObjectCancelEventArgs e)
        {
            if (!IsEdit)
                e.Cancel = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            schedulerControl1.Start = dateTuNgay.DateTime;
            LoadData();
            LoadDataByPage();
        }

        private void gcScheduler_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (gvScheduler.FocusedRowHandle < 0)
                {
                    DialogBox.Error("Vui lòng chọn [Lịch hẹn], xin cảm ơn.");
                    return;
                }
                var f = new BEE.LichLamViec.frmProcess();
                f.MaLH = (int)gvScheduler.GetFocusedRowCellValue("MaLH");
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                    LoadDataByPage();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            
        }

        private void gvScheduler_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDataByPage();
        }
    }
}
