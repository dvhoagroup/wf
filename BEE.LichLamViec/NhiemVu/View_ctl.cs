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
using BEE.ThuVien;

namespace BEEREMA.CongViec.NhiemVu
{
    public partial class View_ctl : DevExpress.XtraEditors.XtraUserControl
    {
        bool KT = false, KT1 = false;
        bool IsAdd = false, IsEdit = false, IsDelete = false;
        int MaNVu = 0;
        DateTime dateStart;
        bool IsZoom = true;
        DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
        DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
        public View_ctl()
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

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = BEE.ThuVien.Common.PerID;
            o.AccessData.Form.FormID = 66;
            DataTable tblAction = o.SelectBy();
            btnAdd.Enabled = false;
          //  btnSua.Enabled = false;
          //  btndelete.Enabled = false;
       //     btnFinish.Enabled = false;
            btnGiaoViec.Enabled = false;

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
            //LoadPermission();
            //LoadPermissionScheduler();
            IsAdd = true;
            IsEdit = true;
            IsDelete = true;
            LoadDictionary();
            cmbKyBC.SelectedIndex = 3;
            LoadData();
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
                if (DialogBox.Question("Bạn có chắc chắn xóa nhiệm vụ này không") == DialogResult.Yes)
                {
                    try
                    {
                        it.NhiemVuCls nv = new it.NhiemVuCls();
                        nv.MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                        nv.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì: nhiệm vụ này đã được sử dụng");
                    }
                }
            }
            else
            {
                DialogBox.Infomation("Vui lòng chọn nhiệm vụ cần xóa");
            }
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
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
                DialogBox.Infomation("Vui lòng chọn nhiệm vụ cần sửa.Xin cảm ơn!");
            }
        }

        private void cmbKyBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetToDate();
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

        private void btnGiaoViec_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                if (gridView1.GetFocusedRowCellValue(colTenTT).ToString() == "Hoàn thành")
                    DialogBox.Infomation("<Nhiệm vụ> này đã hoàn thành. Vui lòng kiểm tra lại, xin cảm ơn.");
                else
                {
                    SelectObject_frm frm = new SelectObject_frm();
                    frm.MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                    frm.ShowDialog();
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn nhiềm vụ muốn giao việc, xin cảm ơn.");
        }

        private void itemCategory_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
