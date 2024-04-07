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
                HoTen = gridView1.GetFocusedRowCellValue(colHoKH).ToString();

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
            var wait = DialogBox.WaitingForm();
            

            it.KhachHangCls o = new it.KhachHangCls();
            string queryString = txtKey.Text;
            if (TimNV == false)
            {
                if (queryString != "")
                {
                    switch (SDBID)
                    {
                        case 1://Tat ca
                            gridControl1.DataSource = o.FindPersonal(queryString, (int)radioOption.EditValue);
                            break;
                        case 2://Theo phong ban
                            o.NhanVien.PhongBan.MaPB = BEE.ThuVien.Common.DepartmentID;
                            gridControl1.DataSource = o.FindPersonalByDepartment(queryString, (int)radioOption.EditValue);
                            break;
                        case 3://Theo nhom
                            o.NhanVien.NKD.MaNKD = BEE.ThuVien.Common.GroupID;
                            gridControl1.DataSource = o.FindPersonalByGroup(queryString, (int)radioOption.EditValue);
                            break;
                        case 4://Theo nhan vien
                            o.NhanVien.MaNV = BEE.ThuVien.Common.StaffID;
                            gridControl1.DataSource = o.FindPersonalByStaff(queryString, (int)radioOption.EditValue);
                            break;
                        default:
                            gridControl1.DataSource = null;
                            break;
                    }
                }
                else
                    gridControl1.DataSource = null;
            }
            else
            { 
                MasterDataContext db = new MasterDataContext();

                if (queryString != null)
                {
                    switch ((int)radioOption.EditValue)
                    {
                        case 0:
                            gridControl1.DataSource = from p in db.NhanViens
                                                      where SqlMethods.Like(p.HoTen, "%"+queryString+"%")
                                                      select new
                                                      {
                                                          MaKH = p.MaNV,
                                                          HoTenKH = p.HoTen,
                                                          SoCMND = p.SoCMND,
                                                          DienThoai = p.DienThoai,
                                                          CodeSUN = p.KeyCode
                                                      };
                            break;
                        case 1:
                            var ltNV = from p in db.NhanViens
                                       where SqlMethods.Like(p.SoCMND, "%" + queryString + "%")
                                                      select new
                                                      {
                                                          MaKH = p.MaNV,
                                                          HoTenKH = p.HoTen,
                                                          SoCMND = p.SoCMND,
                                                          DienThoai = p.DienThoai,
                                                          CodeSUN = p.KeyCode
                                                      };
                            gridControl1.DataSource = ltNV;
                            break;
                        case 2:
                            gridControl1.DataSource = from p in db.NhanViens
                                                      where SqlMethods.Like(p.DienThoai, "%" + queryString + "%")
                                                      select new
                                                      {
                                                          MaKH = p.MaNV,
                                                          HoTenKH = p.HoTen,
                                                          SoCMND = p.SoCMND,
                                                          DienThoai = p.DienThoai,
                                                          CodeSUN = p.KeyCode
                                                      };
                            break;
                        case 3:
                            gridControl1.DataSource = from p in db.NhanViens
                                                      where SqlMethods.Like(p.KeyCode, "%" + queryString + "%")
                                                      select new
                                                      {
                                                          MaKH = p.MaNV,
                                                          HoTenKH = p.HoTen,
                                                          SoCMND = p.SoCMND,
                                                          DienThoai = p.DienThoai,
                                                          CodeSUN = p.KeyCode
                                                      };
                            break;
                        default:
                            gridControl1.DataSource = null;
                            break;
                    }
                }
                else
                    gridControl1.DataSource = null;
            }

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