using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using System.Linq;
using BEE.ThuVien;

namespace BEEREMA.CongViec.NhiemVu
{
    public partial class List_ctl : DevExpress.XtraEditors.XtraUserControl
    {
        bool KT = false, KT1 = false;
        bool IsAdd = false, IsEdit = false, IsDelete = false;
        int MaNVu = 0;
        DateTime dateStart;
        MasterDataContext db;
        public List_ctl()
        {
            InitializeComponent();
            db = new MasterDataContext();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBC);
        }

        void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            dateTuNgay.EditValueChanged -= new EventHandler(dateTuNgay_EditValueChanged);
            dateTuNgay.EditValue = objKBC.DateFrom;
            dateDenNgay.EditValue = objKBC.DateTo;
            dateTuNgay.EditValueChanged += new EventHandler(dateTuNgay_EditValueChanged);
        }

        private void cmbKyBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate(cmbKyBC.SelectedIndex);
        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadDictionary()
        {
            it.NhiemVuCls o = new it.NhiemVuCls();
            lookUpStatus.Properties.DataSource = o.TinhTrang.SelectAll();
            lookUpStatus.ItemIndex = 0;
            lookUpMucDo.Properties.DataSource = o.MucDo.SelectAll();
            lookUpMucDo.ItemIndex = 0;
            lookUpLoaiNVu.Properties.DataSource = o.LoaiNV.SelectAll();
            lookUpLoaiNVu.ItemIndex = 0;
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();

            it.NhiemVuCls o = new it.NhiemVuCls();
            switch (it.CommonCls.GetAccessData(BEE.ThuVien.Common.PerID, 66))
            {
                case 1://Tat ca
                    o.NgayBD = dateTuNgay.DateTime;
                    o.NgayHH = dateDenNgay.DateTime;
                    o.LoaiNV.TenLNV = lookUpLoaiNVu.Text == "<Tất cả>" ? "%%" : lookUpLoaiNVu.EditValue.ToString();
                    o.MucDo.TenMD = lookUpMucDo.Text == "<Tất cả>" ? "%%" : lookUpMucDo.EditValue.ToString();
                    o.TinhTrang.TenTT = lookUpStatus.Text == "<Tất cả>" ? "%%" : lookUpStatus.EditValue.ToString();
                    o.NhanVien.MaNV = BEE.ThuVien.Common.StaffID;
                    switch (itemCategory.EditValue.ToString())
                    {
                        case "Nhiệm vụ của tôi":
                            gridControl1.DataSource = o.SelectByStaff();
                            break;
                        case "Nhiệm vụ được giao":
                            gridControl1.DataSource = o.SelectDuocGiao();
                            break;
                        default:
                            gridControl1.DataSource = o.SelectAll();
                            break;
                    }
                    break;
                case 2://Theo phong ban
                    o.NgayBD = dateTuNgay.DateTime;
                    o.NgayHH = dateDenNgay.DateTime;
                    o.NhanVien.PhongBan.MaPB = BEE.ThuVien.Common.DepartmentID;
                    o.LoaiNV.TenLNV = lookUpLoaiNVu.Text == "<Tất cả>" ? "%%" : lookUpLoaiNVu.EditValue.ToString();
                    o.MucDo.TenMD = lookUpMucDo.Text == "<Tất cả>" ? "%%" : lookUpMucDo.EditValue.ToString();
                    o.TinhTrang.TenTT = lookUpStatus.Text == "<Tất cả>" ? "%%" : lookUpStatus.EditValue.ToString();
                    o.NhanVien.MaNV = BEE.ThuVien.Common.StaffID;
                    switch (itemCategory.EditValue.ToString())
                    {
                        case "Nhiệm vụ của tôi":
                            gridControl1.DataSource = o.SelectByStaff();
                            break;
                        case "Nhiệm vụ được giao":
                            gridControl1.DataSource = o.SelectDuocGiao();
                            break;
                        default:
                            gridControl1.DataSource = o.SelectByDepartment();
                            break;
                    }
                    break;
                case 3://Theo nhom
                    o.NgayBD = dateTuNgay.DateTime;
                    o.NgayHH = dateDenNgay.DateTime;
                    o.NhanVien.NKD.MaNKD = BEE.ThuVien.Common.GroupID;
                    o.LoaiNV.TenLNV = lookUpLoaiNVu.Text == "<Tất cả>" ? "%%" : lookUpLoaiNVu.EditValue.ToString();
                    o.MucDo.TenMD = lookUpMucDo.Text == "<Tất cả>" ? "%%" : lookUpMucDo.EditValue.ToString();
                    o.TinhTrang.TenTT = lookUpStatus.Text == "<Tất cả>" ? "%%" : lookUpStatus.EditValue.ToString();
                    o.NhanVien.MaNV = BEE.ThuVien.Common.StaffID;
                    switch (itemCategory.EditValue.ToString())
                    {
                        case "Nhiệm vụ của tôi":
                            gridControl1.DataSource = o.SelectByStaff();
                            break;
                        case "Nhiệm vụ được giao":
                            gridControl1.DataSource = o.SelectDuocGiao();
                            break;
                        default:
                            gridControl1.DataSource = o.SelectByGroup();
                            break;
                    }
                    break;                
                case 4://Theo nhan vien
                    o.NgayBD = dateTuNgay.DateTime;
                    o.NgayHH = dateDenNgay.DateTime;
                    o.NhanVien.MaNV = BEE.ThuVien.Common.StaffID;
                    o.LoaiNV.TenLNV = lookUpLoaiNVu.Text == "<Tất cả>" ? "%%" : lookUpLoaiNVu.EditValue.ToString();
                    o.MucDo.TenMD = lookUpMucDo.Text == "<Tất cả>" ? "%%" : lookUpMucDo.EditValue.ToString();
                    o.TinhTrang.TenTT = lookUpStatus.Text == "<Tất cả>" ? "%%" : lookUpStatus.EditValue.ToString();                    
                    switch (itemCategory.EditValue.ToString())
                    {
                        case "Nhiệm vụ được giao":
                            gridControl1.DataSource = o.SelectDuocGiao();
                            break;
                        default:
                            gridControl1.DataSource = o.SelectByStaff();
                            break;
                    }
                    break;
                default:
                    gridControl1.DataSource = null;
                    break;
            }
            gridView1.FocusedRowHandle = 1;
            gridView1.FocusedRowHandle = 0;
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

        void LoadNhanVien()
        {
            it.NhiemVu_NhanVienCls o = new it.NhiemVu_NhanVienCls();
            o.MaNVu = MaNVu;
            o.MaNV = BEE.ThuVien.Common.StaffID;
            gridControl3.DataSource = o.SelectBy();
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = BEE.ThuVien.Common.PerID;
            o.AccessData.Form.FormID = 66;
            DataTable tblAction = o.SelectBy();
            btnAdd.Enabled = false;
            btnSua.Enabled = false;
            btndelete.Enabled = false;
            btnFinish.Enabled = false;
            btnGiaoViec.Enabled = false;
            itemProcess.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            btnAdd.Enabled = true;
                            break;
                        case 2:
                            btnSua.Enabled = true;
                            break;
                        case 3:
                            btndelete.Enabled = true;
                            break;
                        case 43://Giao viec
                            btnGiaoViec.Enabled = true;
                            break;
                        case 44://Hoan thanh nhiem vu
                            btnFinish.Enabled = true;
                            break;
                        case 45://Xu ly
                            itemProcess.Enabled = true;
                            break;
                    }
                }
            }
        }

        void LoadPermissionScheduler()
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

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CongViec.NhiemVu.AddNew_frm frm = new CongViec.NhiemVu.AddNew_frm();
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void NhiemVu_ctl_Load(object sender, EventArgs e)
        {
            LoadPermission();
            LoadPermissionScheduler();
            LoadDictionary();
            cmbKyBC.SelectedIndex = 4;
            timer1.Start();
        }

        private void btnRefech_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btndelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn xóa [Nhiệm vụ] này không?") == DialogResult.Yes)
                {
                    try
                    {
                        if (int.Parse(gridView1.GetFocusedRowCellValue("MaNV").ToString()) == BEE.ThuVien.Common.StaffID)
                        {
                            it.NhiemVuCls nv = new it.NhiemVuCls();
                            nv.MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                            nv.Delete();
                            gridView1.DeleteSelectedRows();
                        }
                        else
                        {
                            if (BEE.ThuVien.Common.PerID == 1)
                            {
                                it.NhiemVuCls nv = new it.NhiemVuCls();
                                nv.MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                                nv.Delete();
                                gridView1.DeleteSelectedRows();
                            }
                            else
                                DialogBox.Infomation("[Nhiệm vụ] này không do bạn quản lý. Vui lòng kiểm tra lại, xin cảm ơn.");
                        }
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì: [Nhiệm vụ] này đã được sử dụng.");
                    }
                }
            }
            else
            {
                DialogBox.Infomation("Vui lòng chọn [Nhiệm vụ] cần xóa, xin cảm ơn.");
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gridView1_RowStyle_1(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (gridView1.GetRowCellValue(i, colTenTT).ToString() == "Hoàn thành")
                    gridView1.SetRowCellValue(i, colThoiGian, "Hoàn thành");
                else
                {
                    if (int.Parse(gridView1.GetRowCellValue(i, colTime).ToString()) > 0)
                    {
                        gridView1.SetRowCellValue(i, colThoiGian, it.ConvertDateTimeCls.StringDateTime(int.Parse(gridView1.GetRowCellValue(i, colTime).ToString())));
                        gridView1.SetRowCellValue(i, colTime, int.Parse(gridView1.GetRowCellValue(i, colTime).ToString()) - 1);
                    }
                    else
                        gridView1.SetRowCellValue(i, colThoiGian, "Hết hạn");
                }
            }
            timer1.Start();
        }

        private void gridView1_RowStyle_2(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //try
            //{
            //    e.Appearance.BackColor = Color.FromArgb(int.Parse(gridView1.GetRowCellValue(e.RowHandle, colTinhTrang).ToString()));
            //}
            //catch { }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                dateStart = Convert.ToDateTime(gridView1.GetFocusedRowCellValue(colNgayBD));
            }
            else
            {
                MaNVu = 0;
                dateStart = dateTuNgay.DateTime;
            }

            LoadGeneral();
        }

        void LoadHistory()
        {
            it.NhiemVuCls o = new it.NhiemVuCls();
            o.MaNVu = MaNVu;
            gcHistory.DataSource = o.SelectHistory();
        }

        void LoadGeneral()
        {
            switch (xtraTabControl1.SelectedTabPageIndex)
            {
                case 0:
                    LoadHistory();
                    break;
                case 1:
                    LoadLichHen();
                    break;
                case 2:
                    LoadLichHenScheduler();
                    break;
                case 3:
                    LoadNhanVien();
                    break;
                case 4:
                    ctlTaiLieu1.FormID = 66;
                    ctlTaiLieu1.LinkID = (int?)gridView1.GetFocusedRowCellValue("MaNVu");
                    ctlTaiLieu1.MaNV = (int?)gridView1.GetFocusedRowCellValue("MaNV");
                    ctlTaiLieu1.TaiLieu_Load();
                    break;
                case 5:
                    ctlNoteHistory1.FormID = 66;
                    ctlNoteHistory1.LinkID = (int?)gridView1.GetFocusedRowCellValue("MaNVu");
                    ctlNoteHistory1.MaNV = (int?)gridView1.GetFocusedRowCellValue("MaNV");
                    ctlNoteHistory1.NoteHistory_Load();
                    break;
            }
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                CongViec.NhiemVu.AddNew_frm frm = new CongViec.NhiemVu.AddNew_frm();
                frm.MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
            {
                DialogBox.Infomation("Vui lòng chọn [Nhiệm vụ] cần sửa, xin cảm ơn.");
            }
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            
        }

        private void gridView1_DoubleClick_1(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                CongViec.NhiemVu.AddNew_frm frm = new CongViec.NhiemVu.AddNew_frm();
                frm.MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
            {
                DialogBox.Infomation("Vui lòng chọn [Nhiệm vụ] cần sửa, xin cảm ơn.");
            }
        }

        void LoadLichHen()
        {
            it.LichHenCls o = new it.LichHenCls();
            o.NhiemVu.MaNVu = MaNVu;
            gcScheduler.DataSource = o.SelectBy();
        }

        void LoadLichHenScheduler()
        {
            var wait = DialogBox.WaitingForm();

            schedulerControl1.Start = dateStart;
            it.LichHenCls o = new it.LichHenCls();
            o.NhiemVu.MaNVu = MaNVu;
            this.schedulerStorage1.Appointments.DataSource = o.SelectBy();

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
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("StatusId", "StatusId"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("LabelId", "LabelId"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaLH", "MaLH"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaKH", "MaKH"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("HoTenKH", "HoTenKH"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("TenNVu", "TenNVu"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaNVu", "MaNVu"));

            schedulerStorage1.EnableReminders = true;

            wait.Close();
        }

        private void schedulerControl1_EditAppointmentFormShowing(object sender, AppointmentFormEventArgs e)
        {
            Appointment apt = e.Appointment;

            bool openRecurrenceForm = apt.IsRecurring && schedulerStorage1.Appointments.IsNewAppointment(apt);
            int? maKh = 0;
            try { maKh = (int?)gridView1.GetFocusedRowCellValue("MaKH"); }
            catch { }
            var maNVu = (int?)gridView1.GetFocusedRowCellValue("MaNVu");
            
            string hotenKH = gridView1.GetFocusedRowCellValue("HoTenKH").ToString();
            string tieuDe = gridView1.GetFocusedRowCellValue("TieuDe").ToString();
            LichHen.AddNew_frm f = new LichHen.AddNew_frm((SchedulerControl)sender, apt, openRecurrenceForm);
            f.NhiemVu = tieuDe;
            f.MaKH = maKh ?? 0;
            f.MaNVu = maNVu ?? 0;
            f.KhachHang = hotenKH;
            f.IsEdit = IsEdit;
            f.IsAdd = IsAdd;
            f.LookAndFeel.ParentLookAndFeel = this.LookAndFeel.ParentLookAndFeel;
            e.DialogResult = f.ShowDialog();
            e.Handled = true;

            if (f.IsUpdate)
                LoadLichHenScheduler();
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
                o.UpdateTime(); e.ToString();
                // LoadData();
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
                //LoadData();
            }
            catch { }
            //}
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
            }
            else
                MaNVu = 0;

            LoadGeneral();
        }

        private void btnFinish_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                if (gridView1.GetFocusedRowCellValue(colTenTT).ToString() == "Hoàn thành")
                    DialogBox.Infomation("[Nhiệm vụ] này đã hoàn thành. Vui lòng kiểm tra lại, xin cảm ơn.");
                else
                {
                    if (DialogBox.Question("Bạn có chắc chắn muốn xác nhận hoàn thành nhiệm vụ này không?") == DialogResult.Yes)
                    {
                        it.NhiemVuCls o = new it.NhiemVuCls();
                        o.MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                        o.NhanVien = new it.NhanVienCls() { MaNV = Common.StaffID };
                        o.UpdateFinish();

                        LoadData();
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn nhiệm vụ, xin cảm ơn.");
        }

        private void btnLoadScheduler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadLichHenScheduler();
        }

        private void btnAddScheduler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void btnEditScheduler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormShow(new AppointmentFormEventArgs(this.schedulerControl1.SelectedAppointments(apt)));
        }

        private void btnDeleteScheduler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void schedulerControl1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            switch (e.Menu.Id)
            {
                case SchedulerMenuItemId.DefaultMenu:
                    SchedulerMenuItem add = e.Menu.GetMenuItemById(SchedulerMenuItemId.NewAppointment);
                    if (add != null)
                    {
                        add.Caption = "Thêm lịch hẹn";
                        //add.Image
                    }
                    if (!IsAdd)
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
                    break;
                case SchedulerMenuItemId.AppointmentMenu:
                    // Find the "Label As" item of the appointment popup menu and corresponding submenu.        
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
                    if (!IsDelete)
                        e.Menu.RemoveMenuItem(SchedulerMenuItemId.DeleteAppointment);

                    if (!IsEdit)
                    {
                        e.Menu.RemoveMenuItem(SchedulerMenuItemId.LabelSubMenu);
                        e.Menu.RemoveMenuItem(SchedulerMenuItemId.StatusSubMenu);
                        e.Menu.RemoveMenuItem(SchedulerMenuItemId.OpenAppointment);
                    }
                    break;
                case SchedulerMenuItemId.AppointmentDragMenu:
                    //SchedulerMenuItem cancel = e.Menu.GetMenuItemById(SchedulerMenuItemId.AppointmentDragCancel);
                    //if (cancel != null)
                    //    cancel.Caption = "Bỏ qua";

                    //SchedulerMenuItem copy = e.Menu.GetMenuItemById(SchedulerMenuItemId.AppointmentDragCopy);
                    //if (copy != null)
                    //    copy.Caption = "Sao chép";

                    //SchedulerMenuItem move = e.Menu.GetMenuItemById(SchedulerMenuItemId.AppointmentDragMove);
                    //if (move != null)
                    //    move.Caption = "Di chuyển";
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.AppointmentDragCancel);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.AppointmentDragCopy);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.AppointmentDragMove);
                    break;
            }
        }

        private void schedulerControl1_PreparePopupMenu(object sender, PreparePopupMenuEventArgs e)
        {

            Point p = schedulerControl1.PointToClient(Form.MousePosition);

            SchedulerHitInfo hitInfo = schedulerControl1.ActiveView.ViewInfo.CalcHitInfo(p, true);

            if (hitInfo.HitTest == SchedulerHitTest.ResourceHeader)
            {

                DialogBox.Infomation(hitInfo.ViewInfo.Resource.Caption);

                e.Menu.Items.Clear();

            }
        }

        private void btnGiaoViec_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                if (gridView1.GetFocusedRowCellValue(colTenTT).ToString() == "Hoàn thành")
                    DialogBox.Infomation("[Nhiệm vụ] này đã hoàn thành. Vui lòng kiểm tra lại, xin cảm ơn.");
                else
                {
                    SelectObject_frm frm = new SelectObject_frm();
                    frm.MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                    frm.ShowDialog();
                    LoadNhanVien();
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn nhiềm vụ muốn giao việc, xin cảm ơn.");
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
                        it.LichHenCls o = new it.LichHenCls();
                        o.MaLH = int.Parse(app.CustomFields["MaLH"].ToString());
                        o.Delete();
                        LoadData();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì lịch hẹn này đã được sử dụng. Vui lòng kiểm tra lại, xin cảm ơn.");
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

        private void btnXoaNTH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (gridView3.GetFocusedRowCellValue(colMaNV) != null)
            //{
            //    if (DialogBox.Question() == DialogResult.Yes)
            //    {
            //        it.NhiemVu_NhanVienCls o = new it.NhiemVu_NhanVienCls();
            //        o.MaNV = int.Parse(gridView3.GetFocusedRowCellValue(colMaNV).ToString());
            //        o.MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
            //        try
            //        {
            //            o.Delete();
            //        }
            //        catch
            //        {
            //            DialogBox.Infomation("Xóa không thành công vì: <Người thực hiện> đã có phát sinh [Lịch hẹn]\r\nVui lòng kiểm tra lại, xin cảm ơn.");
            //        }
            //    }
            //}
            //else
            //    DialogBox.Infomation("Vui lòng chọn <Người thực hiện> muốn xóa. Xin cảm ơn.");
        }

        private void itemProcess_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                var f = new frmDuyet();
                f.MaNVu = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colMaNVu));
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn [Nhiềm vụ] muốn giao việc, xin cảm ơn.");            
        }

        private void itemAddSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Vui lòng chọn [Nhiệm vụ], xin cảm ơn.");
                return;
            }
            try
            {
                int? maKh = 0;
                try { maKh = (int?)gridView1.GetFocusedRowCellValue("MaKH"); }
                catch { }
                var maNVu = (int?)gridView1.GetFocusedRowCellValue("MaNVu");
                string hotenKH = gridView1.GetFocusedRowCellValue("HoTenKH").ToString();

                var frm = new CongViec.LichHen.AddNew_frm(null, maNVu, maKh ?? 0, 0, 0, hotenKH);
                frm.NhiemVu = gridView1.GetFocusedRowCellValue("TieuDe").ToString();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemEditSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvScheduler.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Vui lòng chọn [Lịch hẹn], xin cảm ơn.");
                return;
            }
            try
            {
                int? maKh = 0;
                try { maKh = (int?)gridView1.GetFocusedRowCellValue("MaKH"); }
                catch { }
                var maNVu = (int?)gridView1.GetFocusedRowCellValue("MaNVu");
                var maLH = (int?)gvScheduler.GetFocusedRowCellValue("MaLH");
                string hotenKH = gridView1.GetFocusedRowCellValue("HoTenKH").ToString();

                var frm = new CongViec.LichHen.AddNew_frm(maLH, maNVu, maKh ?? 0, 0, 0, hotenKH);
                frm.NhiemVu = gridView1.GetFocusedRowCellValue("TieuDe").ToString();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemDeleteSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvScheduler.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Vui lòng chọn [Lịch hẹn], xin cảm ơn.");
                return;
            }

            if (DialogBox.Question() == DialogResult.Yes)
            {
                using (MasterDataContext db = new MasterDataContext())
                {
                    var objLH = db.LichHens.Single(p => p.MaLH == (int?)gvScheduler.GetFocusedRowCellValue("MaLH"));
                    try
                    {
                        db.LichHens.DeleteOnSubmit(objLH);
                        db.SubmitChanges();

                        gvScheduler.DeleteSelectedRows();
                    }
                    catch { }
                }
            }
        }

        private void itemCategory_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
