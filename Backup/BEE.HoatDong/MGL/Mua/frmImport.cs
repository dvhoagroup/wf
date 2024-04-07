using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEEREMA;
using BEE.ThuVien;
using LinqToExcel;

namespace BEE.HoatDong.MGL.Mua
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        public bool IsSave;
        dip.cmdExcel objExcel;

        public frmImport()
        {
            InitializeComponent();
        }

        private void itemFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (OpenFileDialog file = new OpenFileDialog())
            {
                file.Filter = "(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;
                var excel = new ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();
                cmbSheet.Items.Clear();
                foreach (string s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));
                itemSheet.EditValue = null;
                this.Tag = file.FileName;
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.Question() == DialogResult.No) return;
            grvSP.DeleteSelectedRows();
        }

        int KhachHang_Them(BEE.ThuVien.KhachHang objKH)
        {
            int maKH;
            using (MasterDataContext dbKH = new MasterDataContext())
            {
                var listKH = dbKH.KhachHangs.Where(p => (p.DiDong == objKH.DiDong & objKH.DiDong != "")
                    || (p.SoCMND == objKH.SoCMND & objKH.SoCMND != "")).Select(p => p.MaKH).ToList();
                if (listKH.Count > 0)
                {
                    maKH = listKH[0];
                }
                else
                {
                    objKH.MaNV = Common.StaffID;
                    dbKH.KhachHangs.InsertOnSubmit(objKH);
                    dbKH.SubmitChanges();
                    maKH = objKH.MaKH;
                }
            }
            return maKH;
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            MasterDataContext db = new MasterDataContext();
            try
            {
                byte maloaitien;
                short maMD, maNguon, maLBDS;

                for (int i = 0; i < grvSP.RowCount; i++)
                {
                    grvSP.UnselectRow(i);
                    #region ràng buộc
                    if (grvSP.GetRowCellValue(i, "NhuCau").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập nhu cầu");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }

                    if (grvSP.GetRowCellValue(i, "LoaiBDS").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập loại bất động sản");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    else
                    {
                        try
                        {
                            maLBDS = db.LoaiBDs.Single(p => p.TenLBDS == grvSP.GetRowCellValue(i, "LoaiBDS").ToString().Trim()).MaLBDS;
                        }
                        catch
                        {
                            DialogBox.Error("Loại bất động sản không chính xác");
                            grvSP.FocusedRowHandle = i;
                            return;
                        }
                    }

                    if (grvSP.GetRowCellValue(i, "LoaiTien").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập Loại tiền");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    else
                    {
                        try
                        {
                            maloaitien = db.LoaiTiens.Single(p => p.TenLoaiTien == grvSP.GetRowCellValue(i, "LoaiTien").ToString().Trim()).MaLoaiTien;
                        }
                        catch
                        {
                            DialogBox.Error("Loại tiền không chính xác");
                            grvSP.FocusedRowHandle = i;
                            return;

                        }
                    }

                    if (grvSP.GetRowCellValue(i, "HoTenKH").ToString().Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập khách hàng");
                        grvSP.FocusedRowHandle = i;
                        return;
                    }
                    
                    #endregion

                    var objKH = new BEE.ThuVien.KhachHang();
                    string hoTenKH = grvSP.GetRowCellValue(i, "HoTenKH").ToString().Trim();
                    objKH.HoKH = hoTenKH.Substring(0, hoTenKH.LastIndexOf(' '));
                    objKH.TenKH = hoTenKH.Substring(hoTenKH.LastIndexOf(' ') + 1);
                    objKH.DiDong = grvSP.GetRowCellValue(i, "DienThoai").ToString();
                    objKH.SoCMND = grvSP.GetRowCellValue(i, "CMND").ToString();
                    objKH.DiaChi = grvSP.GetRowCellValue(i, "DiaChiKH").ToString();
                    objKH.IsPersonal = true;
                    int maKH = KhachHang_Them(objKH);

                    mglmtMuaThue objMT = new mglmtMuaThue();
                    string soDK = "";
                    db.mglmtMuaThue_TaoSoPhieu(ref soDK);
                    objMT.SoDK = soDK;
                    objMT.MaTT = 0;
                    objMT.NgayDK = DateTime.Now;
                    objMT.NgayCN = DateTime.Now;
                    objMT.ThoiHan = 30;
                    objMT.MaNVKD = Common.StaffID;
                    objMT.MaNVKT = Common.StaffID;
                    objMT.ChiaSe = 0;
                    objMT.TyLeHH = 50;
                    //Bat dong san
                    objMT.GhiChu = grvSP.GetRowCellValue(i, "GhiChu").ToString().Trim();
                    try
                    {
                        objMT.MaMD = db.mglmtMuDichMTs.Single(p => p.MucDich == grvSP.GetRowCellValue(i, "MucDich").ToString()).ID;
                    }
                    catch { }
                    objMT.IsMua = grvSP.GetRowCellValue(i, "NhuCau").ToString().Trim() == "Cần mua" ? true : false;
                    objMT.MaLBDS = maLBDS;
                    if (grvSP.GetRowCellValue(i, "DienTich") != "")
                        objMT.DienTichTu = Convert.ToDecimal(grvSP.GetRowCellValue(i, "DienTich"));
                    if (grvSP.GetRowCellValue(i, "GiaDen") != "")
                        objMT.GiaDen = Convert.ToDecimal(grvSP.GetRowCellValue(i, "GiaDen"));
                    objMT.MaLT = maloaitien;
                    if (grvSP.GetRowCellValue(i, "PhiMG") != "")
                        objMT.PhiMG = Convert.ToDecimal(grvSP.GetRowCellValue(i, "PhiMG"));
                    try
                    {
                        objMT.MaNguon = db.mglNguons.Single(p => p.TenNguon == grvSP.GetRowCellValue(i, "Nguon").ToString()).MaNguon;
                    }
                    catch { }

                    //Khach hang
                    objMT.MaKH = maKH;
                    objMT.MaTTD = 2;
                    db.mglmtMuaThues.InsertOnSubmit(objMT);
                grvSP.SelectRow(i);
                }

                db.SubmitChanges();

                gcSP.DataSource = null;
                this.IsSave = true;
                DialogBox.Infomation("Dữ liệu đã được lưu");

                grvSP.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
                wait.Close();
            }
        }

        private void itemDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (itemSheet.EditValue != null)
            {
                var excel = new ExcelQueryFactory(this.Tag.ToString());
                var list = excel.Worksheet(itemSheet.EditValue.ToString()).Select(p => new
                {
                    HoTenKH = p[0].ToString().Trim(),
                    DienThoai = p[1].ToString().Trim(),
                    CMND = p[2].ToString().Trim(),
                    DiaChiKH = p[3].ToString().Trim(),
                    GhiChu = p[4].ToString().Trim(),
                    MucDich = p[5].ToString().Trim(),
                    NhuCau = p[6].ToString().Trim(),
                    LoaiBDS = p[7].ToString().Trim(),
                    DienTich = p[8].ToString().Trim(),
                    GiaDen = p[9].ToString().Trim(),
                    LoaiTien = p[10].ToString().Trim(),
                    PhiMG = p[11].ToString().Trim(),
                    Nguon = p[12].ToString().Trim(),                  
                    Error = ""
                }).ToList();

                var listCus = new List<Item>();
                foreach (var r in list)
                {
                    var o = new Item();
                    o.HoTenKH = r.HoTenKH;
                    o.DienThoai = r.DienThoai;
                    o.CMND = r.CMND;
                    o.DiaChiKH = r.DiaChiKH;
                    o.GhiChu = r.GhiChu;
                    o.MucDich = r.MucDich;
                    o.NhuCau = r.NhuCau;
                    o.LoaiBDS = r.LoaiBDS;
                    o.DienTich = r.DienTich;
                    o.GiaDen = r.GiaDen;
                    o.LoaiTien = r.LoaiTien;
                    o.PhiMG = r.PhiMG;
                    o.Nguon = r.Nguon;                   
                    o.Error = r.Error;

                    listCus.Add(o);
                }

                gcSP.DataSource = listCus;
            }
            else
            {
                gcSP.DataSource = null;
            }
        }
    }
    class Item
    {
        public string HoTenKH { get; set; }
        public string DienThoai { get; set; }
        public string CMND { get; set; }
        public string DiaChiKH { get; set; }
        public string GhiChu { get; set; }
        public string MucDich { get; set; }
        public string NhuCau { get; set; }
        public string LoaiBDS { get; set; }
        public string DienTich { get; set; }
        public string GiaDen { get; set; }
        public string LoaiTien { get; set; }
        public string PhiMG { get; set; }
        public string Nguon { get; set; }
        public string Error { get; set; }
    }
}