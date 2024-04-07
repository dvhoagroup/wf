using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.Linq.SqlClient;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.HoatDong.MGL.NguoiMG
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public BEE.ThuVien.NhanVien objnhanvien;
        public byte? MaTN;
        public bool IsUpdate = false;

        public frmImport()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        bool getSex(string val)
        {
            try
            {
                return Convert.ToBoolean(val.Trim());
            }
            catch { }

            return false;
        }

        private void btnChonTapTin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Excel file (*.xls)|*.xls";
            if (f.ShowDialog() == DialogResult.OK)
            {
                var wait = DialogBox.WaitingForm();
                try
                {
                    var book = new LinqToExcel.ExcelQueryFactory(f.FileName);
                    var item = book.Worksheet(0).Select(p => new
                    {
                        TenMG = p[0].ToString().Trim(),
                        SDT = p[1].ToString().Trim(),
                        DiaChi = p[2].ToString().Trim()
                    });

                    List<ImportItem> newlist = new List<ImportItem>();
                    foreach (var it in item)
                    {
                        ImportItem importitem = new ImportItem()
                        {
                            TenMG = it.TenMG,
                            SDT = it.SDT,
                            DiaChi = it.DiaChi,
                        };
                        newlist.Add(importitem);
                    }
                    gcCaNhan.DataSource = newlist;
                }
                catch(Exception ex)
                {
                    DialogBox.Infomation("Mẫu excel dùng để import dữ liệu không đúng định dạng, vui lòng xem lại.\r\nCode: " + ex.Message);
                }

                wait.Close();
                wait.Dispose();
            }
        }

        private DateTime? MyConvert(LinqToExcel.Cell value)
        {
            try
            {
                //return value.Cast<DateTime>(); 
                return DateTime.FromOADate(Convert.ToInt64(value));
            }
            catch
            {
                return null;
            }
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            if (IsUpdate)
                this.Text = "Cập nhật thông tin (Nhà môi giới)";
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCaNhan.DataSource == null)
            {
                DialogBox.Infomation("Không có dữ liệu lưu");
                return;
            }

            if (IsUpdate)
                Update();
            else
                Insert();
        }

        void Insert()
        {
            var date = db.GetSystemDate();
            List<mglNguoiMoiGioi> listMB = new List<mglNguoiMoiGioi>();
            for (int i = 0; i < grvCaNhan.RowCount; i++)
            {
                var obj = new mglNguoiMoiGioi();
                obj.SDT = grvCaNhan.GetRowCellValue(i, "SDT").ToString();
                obj.TenMG = grvCaNhan.GetRowCellValue(i, "TenMG").ToString();
                obj.DiaChi = grvCaNhan.GetRowCellValue(i, "DiaChi").ToString();
                obj.MaNV = Common.StaffID;
                obj.NgayNhap = date;
                listMB.Add(obj);

            }
            var wait = DialogBox.WaitingForm();
            try
            {
                db.mglNguoiMoiGiois.InsertAllOnSubmit(listMB);
                db.SubmitChanges();
                wait.Close();
                wait.Dispose();
                DialogBox.Infomation("Đã lưu");
            }
            catch
            {
                wait.Close();
                wait.Dispose();
            }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        void Update()
        {
            for (int i = 0; i < grvCaNhan.RowCount; i++)
            {
                //if (obj != null)
                //{
                //    obj.DCLL = grvCaNhan.GetRowCellValue(i, colDiaChi).ToString();
                //    obj.DCTT = grvCaNhan.GetRowCellValue(i, colDiaChiThuongTru).ToString();
                //    obj.HoKH = grvCaNhan.GetRowCellValue(i, colHo).ToString();
                   
                //    try
                //    {
                //        db.SubmitChanges();                
                //    }
                //    catch { }
                //}
            }

            var wait = DialogBox.WaitingForm();
            try
            {
                wait.Close();
                wait.Dispose();
                DialogBox.Infomation("Đã lưu");
            }
            catch
            {
                wait.Close();
                wait.Dispose();
            }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void btnXoaDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvCaNhan.DeleteSelectedRows();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }

    class ImportItem
    {
        public string TenMG { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
    }
}