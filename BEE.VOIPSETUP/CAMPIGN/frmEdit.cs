using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.DULIEU;
using BEE.THUVIEN;
using System.Linq;
using System.Collections;
using DIPCRM;


namespace BEE.VOIPSETUP.CAMPAIGN
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public int? CamID { get; set; }
        voipCampaign objCP;
        public List<BEE.KHACHHANG.ItemSelect> ListKH = new List<BEE.KHACHHANG.ItemSelect>();
        public bool IsSave;
        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void btnImportDS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new BEE.KHACHHANG.frmFind();
            frm.ShowDialog();
            if (frm.ListKH.Count() != 0)
            {
                ListKH = frm.ListKH;
                foreach (var p in ListKH)
                {
                    gvDoiTuong.AddNewRow();
                    gvDoiTuong.SetFocusedRowCellValue("PhoneNumber", p.SDT);
                    gvDoiTuong.SetFocusedRowCellValue("MaKH", p.MaKH);
                  //  gvDoiTuong.SetFocusedRowCellValue("TenKH", p.TenKH);
                }
            }
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            slookKhachHang.DataSource = db.KhachHangs.Select(p => new
            {
                p.MaKH,
                HoTen = p.IsPersonal.GetValueOrDefault() == true ? p.HoKH + " " + p.TenKH : p.TenCongTy,//p.HoKH + " " + p.TenKH,// p.HoKH + " " + p.TenKH == " " ? p.TenCongTy : p.HoKH + " " + p.TenKH,
                SDT = p.DiDong == "" ? p.DienThoaiCT : p.DiDong
            });
            slookNhanVien.DataSource = db.NhanViens.Select(p => new
            {
                p.MaNV,
                p.HoTen,
                TenPB = p.MaPB == null ? "" : p.PhongBan.TenPB,
                TenCV = p.MaCV == null ? "" : p.ChucVu.TenCV
            });
            if (CamID == null)
            {
                objCP = new voipCampaign();
                db.voipCampaigns.InsertOnSubmit(objCP);
                dateNgayXL.EditValue = DateTime.Now;
                dateTuNgay.EditValue = dateDenNgay.EditValue = DateTime.Now;
            }
            else
            {
                objCP = db.voipCampaigns.FirstOrDefault(p => p.ID == CamID);
                txtGhiChu.Text = objCP.GhiChu;
                txtTenCD.Text = objCP.Name;
                dateNgayXL.EditValue = objCP.NgayXL;
                dateDenNgay.EditValue = objCP.DenNgay;
                dateTuNgay.EditValue = objCP.TuNgay;
            }
            gcDoiTuong.DataSource = objCP.voipCampaign_ListNumbers;
            gcQuestion.DataSource = objCP.voipCampaign_Questions;
        }

        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CamID == null)
            {
                objCP.MaNVT = Common.MaNV;
                objCP.NgayTao = DateTime.Now;
            }
            else
            {
                objCP.NgayCN = DateTime.Now;
                objCP.MaNVCN = Common.MaNV;
            }
            objCP.GhiChu = txtGhiChu.Text.Trim();
            objCP.Name = txtTenCD.Text.Trim();
            objCP.NgayXL = (DateTime?)dateNgayXL.EditValue;
            objCP.TuNgay = (DateTime?)dateTuNgay.EditValue;
            objCP.DenNgay = (DateTime?)dateDenNgay.EditValue;
            try
            {
                db.SubmitChanges();
                IsSave = true;
                DialogBox.Infomation("Dữ liệu đã lưu thành công!");
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error("Dữ liệu không thể lưu: " + ex.Message);
            }
        }

        private void gvDoiTuong_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvDoiTuong.SetFocusedRowCellValue("MaTT", 1);
        }

        private void btnSelectLine_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<int> lst = new List<int>();
            var frm = new frmSelectLine();
            frm.ShowDialog();
            if (frm.lstNV.Count() != 0)
            {
                var lstNV = frm.lstNV;
                var rows = gvDoiTuong.RowCount;
                if (rows == 0)
                    return;
                var num = rows / lstNV.Count;
                List<int> lstNVNew = lstNV.ToList();
                for (int i = 0; i < num - 1; i++)
                {
                    lstNV.AddRange(lstNVNew);
                }
                for (int i = 0; i < rows - lstNV.Count + 1; i++)
                    lstNV.Add(lstNVNew[0]);
                for (int i = 0; i < rows; i++)
                {
                    gvDoiTuong.SetRowCellValue(i, "MaNV", lstNV[i]);
                }
            }
        }

        private void btnImportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                        TenKH = p[0].ToString().Trim(),
                        SDT = p[1].ToString().Trim()
                    }).ToList();

                    List<BEE.KHACHHANG.ItemSelect> newList = new List<KHACHHANG.ItemSelect>();
                    if (item.Count() == 0)
                        return;
                    foreach (var it in item)
                    {
                        gvDoiTuong.AddNewRow();
                        gvDoiTuong.SetFocusedRowCellValue("TenKH", it.TenKH);
                        gvDoiTuong.SetFocusedRowCellValue("PhoneNumber", it.SDT);
                    }
                }
                catch (Exception ex)
                {
                    DialogBox.Infomation("Mẫu excel dùng để import dữ liệu không đúng định dạng, vui lòng xem lại.\r\nCode: " + ex.Message);
                }
                wait.Close();
                wait.Dispose();
            }
        }
    }
}