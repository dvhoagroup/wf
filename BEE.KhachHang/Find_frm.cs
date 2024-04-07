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
using System.Data.Linq.SqlClient;
using BEEREMA;

namespace BEE.KhachHang
{
    public partial class Find_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaKH = 0, MaNV = 0;
        public string HoTen = "";
        int SDBID = 5;
        public bool TimNV = false;
        public int? LoaiNC { get; set; } // 1 cần bán cho thuê, 2 cần mua thue
        public Find_frm()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(BEE.ThuVien.Common.PerID, 19);

            return o.SDB.SDBID;
        }

        private void txtKey_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaKH) != null)
            {
                MaKH = MaNV = int.Parse(gridView1.GetFocusedRowCellValue(colMaKH).ToString());
                try
                {
                    HoTen = gridView1.GetFocusedRowCellValue(colHoKH).ToString();
                }
                catch
                {

                    
                }
               

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }

            this.Close();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            btnChon_Click(sender, e);
        }

        private void Find_frm_Load(object sender, EventArgs e)
        {
            SDBID = GetAccessData();
        }

        private void Find_frm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                LoadData();
        }

        void LoadData()
        {
            //var wait = DialogBox.WaitingForm();
            it.KhachHangCls o = new it.KhachHangCls();
            string queryString = txtKey.Text;
            var db = new MasterDataContext();
            if (this.LoaiNC == 1)
            {
                var obj = db.mglbcPhanQuyens.Single(p => p.MaNV == Common.StaffID);
                if (TimNV == false)
                {
                    if (queryString != "")
                    {
                        switch (SDBID)
                        {

                            case 1://Tat ca

                                #region  if (obj.DienThoai == false)
                                if (obj.DienThoai == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonal(queryString, (int)radioOption.EditValue).Select(p => new
                                    {
                                        DienThoai = Common.Right(p.DienThoai, 3),
                                        DiDong2 = Common.Right(p.DiDong2, 3),
                                        DiDong3 = Common.Right(p.DiDong3, 3),
                                        DiDong4 = Common.Right(p.DiDong4, 3),
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                #region else if (obj.DienThoai3Dau == false)
                                else if (obj.DienThoai3Dau == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonal(queryString, (int)radioOption.EditValue).Select(p => new
                                    {
                                        DienThoai = Common.Right1(p.DienThoai, 3),
                                        DiDong2 = Common.Right1(p.DiDong2, 3),
                                        DiDong3 = Common.Right1(p.DiDong3, 3),
                                        DiDong4 = Common.Right1(p.DiDong4, 3),
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                #region else if (obj.dienthoaian == false)
                                else if (obj.DienThoaiAn == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonal(queryString, (int)radioOption.EditValue).Select(p => new
                                    {
                                        DienThoai = "",
                                        DiDong2 = "",
                                        DiDong3 = "",
                                        DiDong4 = "",
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion
                                #region else
                                else
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonal(queryString, (int)radioOption.EditValue).Select(p => new
                                    {
                                        p.DienThoai,
                                        DiDong2 = p.DiDong2,
                                        DiDong3 = p.DiDong3,
                                        DiDong4 = p.DiDong4,
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                break;

                            case 2://Theo phong ban

                                o.NhanVien.PhongBan.MaPB = BEE.ThuVien.Common.DepartmentID;
                                #region  if (obj.DienThoai == false)
                                if (obj.DienThoai == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByDepartment(queryString, (int)radioOption.EditValue, o.NhanVien.PhongBan.MaPB).Select(p => new
                                    {
                                        DienThoai = Common.Right(p.DienThoai, 3),
                                        DiDong2 = Common.Right(p.DiDong2, 3),
                                        DiDong3 = Common.Right(p.DiDong3, 3),
                                        DiDong4 = Common.Right(p.DiDong4, 3),
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                #region else if (obj.DienThoai3Dau == false)
                                else if (obj.DienThoai3Dau == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByDepartment(queryString, (int)radioOption.EditValue, o.NhanVien.PhongBan.MaPB).Select(p => new
                                    {
                                        DienThoai = Common.Right1(p.DienThoai, 3),
                                        DiDong2 = Common.Right1(p.DiDong2, 3),
                                        DiDong3 = Common.Right1(p.DiDong3, 3),
                                        DiDong4 = Common.Right1(p.DiDong4, 3),
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                #region else if (obj.dienthoaian == false)
                                else if (obj.DienThoaiAn == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByDepartment(queryString, (int)radioOption.EditValue, o.NhanVien.PhongBan.MaPB).Select(p => new
                                    {
                                        DienThoai = "",
                                        DiDong2 = "",
                                        DiDong3 = "",
                                        DiDong4 = "",
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion
                                #region else
                                else
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByDepartment(queryString, (int)radioOption.EditValue, o.NhanVien.PhongBan.MaPB).Select(p => new
                                    {
                                        p.DienThoai,
                                        p.DiDong2,
                                        p.DiDong3,
                                        p.DiDong4,
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion


                                break;
                            case 3://Theo nhom
                                o.NhanVien.NKD.MaNKD = BEE.ThuVien.Common.GroupID;
                                #region  if (obj.DienThoai == false)
                                if (obj.DienThoai == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByGroup(queryString, (int)radioOption.EditValue, o.NhanVien.NKD.MaNKD).Select(p => new
                                    {
                                        DienThoai = Common.Right(p.DienThoai, 3),
                                        DiDong2 = Common.Right(p.DiDong2, 3),
                                        DiDong3 = Common.Right(p.DiDong3, 3),
                                        DiDong4 = Common.Right(p.DiDong4, 3),
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                #region else if (obj.DienThoai3Dau == false)
                                else if (obj.DienThoai3Dau == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByGroup(queryString, (int)radioOption.EditValue, o.NhanVien.NKD.MaNKD).Select(p => new
                                    {
                                        DienThoai = Common.Right1(p.DienThoai, 3),
                                        DiDong2 = Common.Right1(p.DiDong2, 3),
                                        DiDong3 = Common.Right1(p.DiDong3, 3),
                                        DiDong4 = Common.Right1(p.DiDong4, 3),
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                #region else if (obj.dienthoaian == false)
                                else if (obj.DienThoaiAn == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByGroup(queryString, (int)radioOption.EditValue, o.NhanVien.NKD.MaNKD).Select(p => new
                                    {
                                        DienThoai = "",
                                        DiDong2 = "",
                                        DiDong3 = "",
                                        DiDong4 = "",
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion
                                #region else
                                else
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByGroup(queryString, (int)radioOption.EditValue, o.NhanVien.NKD.MaNKD).Select(p => new
                                    {
                                        p.DienThoai,
                                        p.DiDong2,
                                        p.DiDong3,
                                        p.DiDong4,
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                break;
                            case 4://Theo nhan vien

                                o.NhanVien.MaNV = BEE.ThuVien.Common.StaffID;
                                #region  if (obj.DienThoai == false)
                                if (obj.DienThoai == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByStaff(queryString, (int)radioOption.EditValue, o.NhanVien.MaNV).Select(p => new
                                    {
                                        DienThoai = Common.Right(p.DienThoai, 3),
                                        DiDong2 = Common.Right(p.DiDong2, 3),
                                        DiDong3 = Common.Right(p.DiDong3, 3),
                                        DiDong4 = Common.Right(p.DiDong4, 3),
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                #region else if (obj.DienThoai3Dau == false)
                                else if (obj.DienThoai3Dau == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByStaff(queryString, (int)radioOption.EditValue, o.NhanVien.MaNV).Select(p => new
                                    {
                                        DienThoai = Common.Right1(p.DienThoai, 3),
                                        DiDong2 = Common.Right1(p.DiDong2, 3),
                                        DiDong3 = Common.Right1(p.DiDong3, 3),
                                        DiDong4 = Common.Right1(p.DiDong4, 3),
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                #region else if (obj.dienthoaian == false)
                                else if (obj.DienThoaiAn == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByStaff(queryString, (int)radioOption.EditValue, o.NhanVien.MaNV).Select(p => new
                                    {
                                        DienThoai = "",
                                        DiDong2 = "",
                                        DiDong3 = "",
                                        DiDong4 = "",
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion
                                #region else
                                else
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByStaff(queryString, (int)radioOption.EditValue, o.NhanVien.MaNV).Select(p => new
                                    {
                                        p.DienThoai,
                                        p.DiDong2,
                                        p.DiDong3,
                                        p.DiDong4,
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion


                                break;
                            default:
                                gridControl1.DataSource = null;
                                break;

                        }
                    }
                }
            }
            // cần thue
            else if (LoaiNC == 2)
            {
                var obj = db.mglmtPhanQuyens.Single(p => p.MaNV == Common.StaffID);
                if (TimNV == false)
                {
                    if (queryString != "")
                    {
                        switch (SDBID)
                        {

                            case 1://Tat ca

                                #region  if (obj.DienThoai == false)
                                if (obj.DienThoai == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonal(queryString, (int)radioOption.EditValue).Select(p => new
                                    {
                                        DienThoai = Common.Right(p.DienThoai, 3),
                                        DiDong2 = Common.Right(p.DiDong2, 3),
                                        DiDong3 = Common.Right(p.DiDong3, 3),
                                        DiDong4 = Common.Right(p.DiDong4, 3),
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                #region else if (obj.DienThoai3Dau == false)
                                else if (obj.DienThoai3Dau == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonal(queryString, (int)radioOption.EditValue).Select(p => new
                                    {
                                        DienThoai = Common.Right1(p.DienThoai, 3),
                                        DiDong2 = Common.Right1(p.DiDong2, 3),
                                        DiDong3 = Common.Right1(p.DiDong3, 3),
                                        DiDong4 = Common.Right1(p.DiDong4, 3),
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                #region else if (obj.dienthoaian == false)
                                else if (obj.DienThoaiAn == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonal(queryString, (int)radioOption.EditValue).Select(p => new
                                    {
                                        DienThoai = "",
                                        DiDong2 = "",
                                        DiDong3 = "",
                                        DiDong4 = "",
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion
                                #region else
                                else
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonal(queryString, (int)radioOption.EditValue).Select(p => new
                                    {
                                        p.DienThoai,
                                        p.DiDong2,
                                        p.DiDong3,
                                        p.DiDong4,
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                break;

                            case 2://Theo phong ban

                                o.NhanVien.PhongBan.MaPB = BEE.ThuVien.Common.DepartmentID;
                                #region  if (obj.DienThoai == false)
                                if (obj.DienThoai == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByDepartment(queryString, (int)radioOption.EditValue, o.NhanVien.PhongBan.MaPB).Select(p => new
                                    {
                                        DienThoai = Common.Right(p.DienThoai, 3),
                                        DiDong2 = Common.Right(p.DiDong2, 3),
                                        DiDong3 = Common.Right(p.DiDong3, 3),
                                        DiDong4 = Common.Right(p.DiDong4, 3),
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                #region else if (obj.DienThoai3Dau == false)
                                else if (obj.DienThoai3Dau == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByDepartment(queryString, (int)radioOption.EditValue, o.NhanVien.PhongBan.MaPB).Select(p => new
                                    {
                                        DienThoai = Common.Right1(p.DienThoai, 3),
                                        DiDong2 = Common.Right1(p.DiDong2, 3),
                                        DiDong3 = Common.Right1(p.DiDong3, 3),
                                        DiDong4 = Common.Right1(p.DiDong4, 3),
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                #region else if (obj.dienthoaian == false)
                                else if (obj.DienThoaiAn == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByDepartment(queryString, (int)radioOption.EditValue, o.NhanVien.PhongBan.MaPB).Select(p => new
                                    {
                                        DienThoai = "",
                                        DiDong2 = "",
                                        DiDong3 = "",
                                        DiDong4 = "",
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion
                                #region else
                                else
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByDepartment(queryString, (int)radioOption.EditValue, o.NhanVien.PhongBan.MaPB).Select(p => new
                                    {
                                        p.DienThoai,
                                        p.DiDong2,
                                        p.DiDong3,
                                        p.DiDong4,
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion


                                break;
                            case 3://Theo nhom
                                o.NhanVien.NKD.MaNKD = BEE.ThuVien.Common.GroupID;
                                #region  if (obj.DienThoai == false)
                                if (obj.DienThoai == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByGroup(queryString, (int)radioOption.EditValue, o.NhanVien.NKD.MaNKD).Select(p => new
                                    {
                                        DienThoai = Common.Right(p.DienThoai, 3),
                                        DiDong2 = Common.Right(p.DiDong2, 3),
                                        DiDong3 = Common.Right(p.DiDong3, 3),
                                        DiDong4 = Common.Right(p.DiDong4, 3),
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                #region else if (obj.DienThoai3Dau == false)
                                else if (obj.DienThoai3Dau == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByGroup(queryString, (int)radioOption.EditValue, o.NhanVien.NKD.MaNKD).Select(p => new
                                    {
                                        DienThoai = Common.Right1(p.DienThoai, 3),
                                        DiDong2 = Common.Right1(p.DiDong2, 3),
                                        DiDong3 = Common.Right1(p.DiDong3, 3),
                                        DiDong4 = Common.Right1(p.DiDong4, 3),
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                #region else if (obj.dienthoaian == false)
                                else if (obj.DienThoaiAn == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByGroup(queryString, (int)radioOption.EditValue, o.NhanVien.NKD.MaNKD).Select(p => new
                                    {
                                        DienThoai = "",
                                        DiDong2 = "",
                                        DiDong3 = "",
                                        DiDong4 = "",
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion
                                #region else
                                else
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByGroup(queryString, (int)radioOption.EditValue, o.NhanVien.NKD.MaNKD).Select(p => new
                                    {
                                        p.DienThoai,
                                        p.DiDong2,
                                        p.DiDong3,
                                        p.DiDong4,
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                break;
                            case 4://Theo nhan vien

                                o.NhanVien.MaNV = BEE.ThuVien.Common.StaffID;
                                #region  if (obj.DienThoai == false)
                                if (obj.DienThoai == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByStaff(queryString, (int)radioOption.EditValue, o.NhanVien.MaNV).Select(p => new
                                    {
                                        DienThoai = Common.Right(p.DienThoai, 3),
                                        DiDong2 = Common.Right(p.DiDong2, 3),
                                        DiDong3 = Common.Right(p.DiDong3, 3),
                                        DiDong4 = Common.Right(p.DiDong4, 3),
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                #region else if (obj.DienThoai3Dau == false)
                                else if (obj.DienThoai3Dau == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByStaff(queryString, (int)radioOption.EditValue, o.NhanVien.MaNV).Select(p => new
                                    {
                                        DienThoai = Common.Right1(p.DienThoai, 3),
                                        DiDong2 = Common.Right1(p.DiDong2, 3),
                                        DiDong3 = Common.Right1(p.DiDong3, 3),
                                        DiDong4 = Common.Right1(p.DiDong4, 3),
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion

                                #region else if (obj.dienthoaian == false)
                                else if (obj.DienThoaiAn == false)
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByStaff(queryString, (int)radioOption.EditValue, o.NhanVien.MaNV).Select(p => new
                                    {
                                        DienThoai = "",
                                        DiDong2 = "",
                                        DiDong3 = "",
                                        DiDong4 = "",
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion
                                #region else
                                else
                                {
                                    gridControl1.DataSource = db.KhachHang_findAllPersonalByStaff(queryString, (int)radioOption.EditValue, o.NhanVien.MaNV).Select(p => new
                                    {
                                        p.DienThoai,
                                        p.DiDong2,
                                        p.DiDong3,
                                        p.DiDong4,
                                        p.SoCMND,
                                        p.HoTenKH,
                                        p.MaKH,
                                        p.Email,

                                    }).ToList();
                                }
                                #endregion


                                break;
                            default:
                                gridControl1.DataSource = null;
                                break;

                        }
                    }
                }
            }



        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}