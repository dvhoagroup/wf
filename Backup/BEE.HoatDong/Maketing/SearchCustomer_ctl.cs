using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.HoatDong.Maketing
{
    public partial class SearchCustomer_ctl : UserControl
    {
        bool KT = false, KT1 = false;
        byte MaTinh = 0;
        short MaHuyen = 0;
        int MaDA = 0, BlockID = 0, MaKH = 0;
        string KhachHang = "";

        public SearchCustomer_ctl()
        {
            InitializeComponent();
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

        private void cmbKyBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetToDate();
            if (cmbKyBC.SelectedIndex == 34)
                dateTuNgay.DateTime = DateTime.Parse("01/01/2000");
        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbKyBC.SelectedIndex == 34)
            {
                dateDenNgay.Enabled = true;
                dateTuNgay.Enabled = true;                
            }
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbKyBC.SelectedIndex == 34)
            {
                dateDenNgay.Enabled = true;
                dateTuNgay.Enabled = true;
            }
        }

        void LoadDuAn()
        {
            it.DuAnCls o = new it.DuAnCls();
            lookUpDuAn.Properties.DataSource = o.SelectShow2();
            lookUpDuAn2.Properties.DataSource = o.SelectShow2();
            lookUpDuAn2.ItemIndex = 0;
        }

        void LoadLoaiBDS()
        {
            it.LoaiBDSCls o = new it.LoaiBDSCls();
            lookUpLoaiBDS.Properties.DataSource = o.SelectAll();
        }

        void LoadTinhTrangBDS()
        {
            it.TinhTrangBDSCls o = new it.TinhTrangBDSCls();
            lookUpTinhTrang.Properties.DataSource = o.SelectAll();
        }

        void LoadNgheNhgiep()
        {
            it.NgheNghiepCls o = new it.NgheNghiepCls();
            lookUpNgheNghiep.Properties.DataSource = o.SelectAll();
            lookUpNgheNghiep2.Properties.DataSource = o.SelectAll();
        }

        void LoadQuyDanh()
        {
            it.QuyDanhCls o = new it.QuyDanhCls();
            lookUpGioiTinh.Properties.DataSource = o.SelectAll();
        }

        void LoadPTTT()
        {
            lookUpPTTT.Properties.DataSource = it.CommonCls.Table("PhuongThucThanhToan_getAll");
            lookUpPTTT.ItemIndex = 0;
        }

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(BEE.ThuVien.Common.PerID, 39);

            return o.SDB.SDBID;
        }

        string Transaction()
        {
            string QueryString = "KhachHang_Search ";
            QueryString += string.Format("'{0}'", dateTuNgay.DateTime.ToString("MM/dd/yyyy"));
            QueryString += string.Format(",'{0}'", dateDenNgay.DateTime.ToString("MM/dd/yyyy"));
            QueryString += lookUpDuAn.ItemIndex == 0 ? ",'%%'" : "," + lookUpDuAn.EditValue.ToString();
            QueryString += lookUpLoaiBDS.ItemIndex == 0 ? ",'%%'" : "," + lookUpLoaiBDS.EditValue.ToString();
            QueryString += lookUpTinhTrang.ItemIndex == 0 ? ",'%%'" : "," + lookUpTinhTrang.EditValue.ToString();
            QueryString += lookUpXa.ItemIndex == 0 ? ",'%%'" : "," + lookUpXa.EditValue.ToString();
            QueryString += lookUpHuyen.ItemIndex == 0 ? ",'%%'" : "," + lookUpHuyen.EditValue.ToString();
            QueryString += lookUpTinh.ItemIndex == 0 ? ",'%%'" : "," + lookUpTinh.EditValue.ToString();
            QueryString += lookUpGioiTinh.ItemIndex == 0 ? ",'%%'" : "," + lookUpGioiTinh.EditValue.ToString();
            QueryString += lookUpTangNha.ItemIndex == 0 ? ",'%%'" : "," + lookUpTangNha.EditValue.ToString();
            QueryString += lookUpLoaiKH2.ItemIndex == 0 ? ",'%%'" : "," + lookUpLoaiKH2.EditValue.ToString();
            QueryString += lookUpNgheNghiep2.ItemIndex == 0 ? ",'%%'" : "," + lookUpNgheNghiep2.EditValue.ToString();
            return QueryString;
        }

        string NotTransaction()
        {
            string QueryString = "KhachHang_Search2 ";
            QueryString += lookUpDuAn2.ItemIndex == 0 ? "'%%'" : lookUpDuAn2.EditValue.ToString();
            QueryString += lookUpLoaiBDS.ItemIndex == 0 ? ",'%%'" : "," + lookUpLoaiBDS.EditValue.ToString();
            QueryString += lookUpTinhTrang.ItemIndex == 0 ? ",'%%'" : "," + lookUpTinhTrang.EditValue.ToString();
            QueryString += lookUpNhomKH.ItemIndex == 0 ? ",'%%'" : "," + lookUpNhomKH.EditValue.ToString();
            //QueryString += lookUpXa.ItemIndex == 0 ? ",'%%'" : "," + lookUpXa.EditValue.ToString();
            QueryString += lookUpHuyen2.ItemIndex == 0 ? ",'%%'" : "," + lookUpHuyen2.EditValue.ToString();
            QueryString += lookUpTinh2.ItemIndex == 0 ? ",'%%'" : "," + lookUpTinh2.EditValue.ToString();
            QueryString += lookUpGioiTinh.ItemIndex == 0 ? ",'%%'" : "," + lookUpGioiTinh.EditValue.ToString();
            QueryString += lookUpNgheNghiep.ItemIndex == 0 ? ",'%%'" : "," + lookUpNgheNghiep2.EditValue.ToString();
            QueryString += lookUpTangNha.ItemIndex == 0 ? ",'%%'" : "," + lookUpTangNha.EditValue.ToString();
            QueryString += lookUpPTTT.ItemIndex == 0 ? ",'%%'" : "," + lookUpPTTT.EditValue.ToString();
            QueryString += "," + double.Parse(spinGiaTu.EditValue.ToString());
            QueryString += "," + (double.Parse(spinGiaTu.EditValue.ToString()) == 0 ? 10000000000 : double.Parse(spinGiaTu.EditValue.ToString()));
            QueryString += "," + double.Parse(spinDienTich.EditValue.ToString());
            QueryString += "," + double.Parse(spinSoPhongNgu.EditValue.ToString());
            QueryString += "," + double.Parse(spinMucThuNhap.EditValue.ToString());
            QueryString += "," + double.Parse(spinThanhVien.EditValue.ToString());

            return QueryString;
        }

        void LoadData()
        {
            string QueryString = "";

            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {                
                xtraTabControl2.TabPages[0].Text = "Sản phẩm đã giao dịch";

                switch (GetAccessData())
                {
                    case 1://Tat ca
                        QueryString = Transaction();
                        break;
                    case 2://Theo nhom
                        QueryString = Transaction().Replace("KhachHang_Search", "KhachHang_SearchByGroup") + "," + BEE.ThuVien.Common.GroupID;
                        break;
                    case 3://Theo phong ban
                        QueryString = Transaction().Replace("KhachHang_Search", "KhachHang_SearchByDepartment") + "," + BEE.ThuVien.Common.DepartmentID;
                        break;
                    case 4://Theo nhan vien
                        QueryString = Transaction().Replace("KhachHang_Search", "KhachHang_SearchByStaff") + "," + BEE.ThuVien.Common.StaffID;
                        break;
                    default:
                        QueryString = "";
                        break;
                }
            }
            else
            {
                switch (GetAccessData())
                {
                    case 1://Tat ca
                        QueryString = NotTransaction();
                        break;
                    case 2://Theo nhom
                        QueryString = NotTransaction().Replace("KhachHang_Search2", "KhachHang_Search2ByGroup") + "," + BEE.ThuVien.Common.GroupID;
                        break;
                    case 3://Theo phong ban
                        QueryString = NotTransaction().Replace("KhachHang_Search2", "KhachHang_Search2ByDepartment") + "," + BEE.ThuVien.Common.DepartmentID;
                        break;
                    case 4://Theo nhan vien
                        QueryString = NotTransaction().Replace("KhachHang_Search2", "KhachHang_Search2ByStaff") + "," + BEE.ThuVien.Common.StaffID;
                        break;
                    default:
                        QueryString = "";
                        break;
                }
                xtraTabControl2.TabPages[0].Text = "Sản phẩm quan tâm";
            }

            gridControl1.DataSource = it.CommonCls.Table(QueryString);

            it.KhachHangCls o = new it.KhachHangCls();
            lookupQuyDanh.DataSource = o.QuyDanh.Select();
            lookUpNhomKH1.DataSource = o.NhomKH.Select();
        }

        void LoadTinh()
        {
            it.TinhCls objTinh = new it.TinhCls();
            lookUpTinh.Properties.DataSource = objTinh.SelectAll();
            lookUpTinh.ItemIndex = 0;
            lookUpTinh2.Properties.DataSource = objTinh.SelectAll();
            lookUpTinh2.ItemIndex = 0;
        }

        void LoadHuyen()
        {
            it.HuyenCls objHuyen = new it.HuyenCls();
            lookUpHuyen.Properties.DataSource = objHuyen.SelectAll(MaTinh);
            lookUpHuyen.ItemIndex = 0;
            lookUpHuyen2.Properties.DataSource = objHuyen.SelectAll(MaTinh);
            lookUpHuyen2.ItemIndex = 0;
        }

        void LoadXa()
        {
            it.XaCls objXa = new it.XaCls();
            lookUpXa.Properties.DataSource = objXa.SelectAll(MaHuyen);
            lookUpXa.ItemIndex = 0;
        }

        void LoadNhomKH()
        {
            it.NhomKHCls o = new it.NhomKHCls();
            lookUpNhomKH.Properties.DataSource = o.SelectAll();
            lookUpLoaiKH2.Properties.DataSource = o.SelectAll();
        }

        private void SelectPosition_frm_Load(object sender, EventArgs e)
        {
            LoadTinh();
            LoadHuyen();
            LoadXa();
        }

        private void lookUpTinh_EditValueChanged(object sender, EventArgs e)
        {
            MaTinh = byte.Parse(lookUpTinh.EditValue.ToString());
            LoadHuyen();
            LoadXa();
        }

        private void lookUpHuyen_EditValueChanged(object sender, EventArgs e)
        {
            MaHuyen = short.Parse(lookUpHuyen.EditValue.ToString());
            LoadXa();
        }

        void LoadTiemNang()
        {
            it.KhachHangCls o = new it.KhachHangCls();
            o.MaKH = MaKH;
            gridControlTiemNang.DataSource = o.SelectTiemNang();
        }

        void LoadDaGD()
        {
            it.KhachHangCls o = new it.KhachHangCls();
            o.MaKH = MaKH;
            gridControlTiemNang.DataSource = o.GiaoDich2(MaKH);
        }

        private void SearchCustomer_ctl_Load(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 1;
            xtraTabControl1.SelectedTabPageIndex = 0;
            cmbKyBC.SelectedIndex = 34;
            LoadDuAn();
            lookUpDuAn.ItemIndex = 0;
            LoadTinh();
            lookUpTinh.ItemIndex = 0;
            LoadLoaiBDS();
            lookUpLoaiBDS.ItemIndex = 0;
            LoadTinhTrangBDS();
            lookUpTinhTrang.ItemIndex = 0;
            LoadNhomKH();
            lookUpNhomKH.ItemIndex = 0;
            lookUpLoaiKH2.ItemIndex = 0;  
            LoadQuyDanh();
            lookUpGioiTinh.ItemIndex = 0;
            LoadNgheNhgiep();
            lookUpNgheNghiep.ItemIndex = 0;
            lookUpNgheNghiep2.ItemIndex = 0;  
            LoadPTTT();
            //LoadData();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Gui mail
            int[] rows = gridCaNhan.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Infomation("Vui lòng chọn một khách hàng. Xin cảm ơn.");
                return;
            }            

            List<string> ListEmail = new List<string>();
            foreach (int i in rows)
            {
                if (gridCaNhan.GetFocusedRowCellValue(colEmail).ToString() != "")
                    ListEmail.Add(gridCaNhan.GetFocusedRowCellValue(colEmail).ToString() + ":" + gridCaNhan.GetFocusedRowCellValue(colHoKH).ToString() + " " + gridCaNhan.GetFocusedRowCellValue(colTenKH).ToString());
            }
            
            Content_frm frm = new Content_frm();
            frm.ListEmail = ListEmail;
            frm.ShowDialog();
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = gridCaNhan.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Infomation("Vui lòng chọn một khách hàng muốn in. Xin cảm ơn.");
                return;
            }
            KhachHang = "";
            foreach (int i in rows)
            {
                if (gridCaNhan.GetRowCellValue(i, colMaKH).ToString() != "")
                    KhachHang += "-" + gridCaNhan.GetRowCellValue(i, colMaKH).ToString() + "-";
            }

            BieuMau_frm frm = new BieuMau_frm();
            frm.KhachHang = KhachHang;
            frm.ShowDialog();
        }

        void LoadBlock()
        {
            it.BlocksCls o = new it.BlocksCls();
            lookUpBlock.Properties.DataSource = o.SelectAll(MaDA);
            lookUpBlock.ItemIndex = 0;
            lookUpBlock2.Properties.DataSource = o.SelectAll(MaDA);
            lookUpBlock2.ItemIndex = 0;
        }

        private void lookUpDuAn_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit _DA = (LookUpEdit)sender;
            MaDA = int.Parse(_DA.EditValue.ToString());
            LoadBlock();
            LoadTangNha();
        }

        void LoadTangNha()
        {
            it.TangNhaCls o = new it.TangNhaCls();
            lookUpTangNha.Properties.DataSource = o.SelectAll(BlockID);
            lookUpTangNha.ItemIndex = 0;
            lookUpTangNha2.Properties.DataSource = o.SelectAll(BlockID);
            lookUpTangNha2.ItemIndex = 0;
        }

        private void lookUpBlock_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit _Block = (LookUpEdit)sender;
            BlockID = int.Parse(_Block.EditValue.ToString());
            LoadTangNha();
        }

        private void gridCaNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridCaNhan.GetFocusedRowCellValue(colMaKH) != null)
                MaKH = int.Parse(gridCaNhan.GetFocusedRowCellValue(colMaKH).ToString());
            else
                MaKH = 0;
            if (xtraTabControl1.SelectedTabPageIndex == 1)
                LoadTiemNang();
            else
                LoadDaGD();
        }

        private void btnTheoVungMien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Chart_frm frm = new Chart_frm();
            frm.Title = "VÙNG MIỀN";
            if(xtraTabControl1.SelectedTabPageIndex == 0)
                frm.QueryString = Transaction().Replace("KhachHang_Search", "KhachHang_chartArea");
            else
                frm.QueryString = NotTransaction().Replace("KhachHang_Search2", "KhachHang_chartArea2");
            frm.ShowDialog();
        }

        private void lookUpTinh2_EditValueChanged(object sender, EventArgs e)
        {
            MaTinh = byte.Parse(lookUpTinh2.EditValue.ToString());
            LoadHuyen();
            LoadXa();
        }

        private void btnTheoDuAn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Chart_frm frm = new Chart_frm();
            frm.Title = "DỰ ÁN";
            if (xtraTabControl1.SelectedTabPageIndex == 0)
                frm.QueryString = Transaction().Replace("KhachHang_Search", "KhachHang_chartProject");
            else
                frm.QueryString = NotTransaction().Replace("KhachHang_Search2", "KhachHang_chartProject2");
            frm.ShowDialog();
        }

        private void btnTheoLoaiKH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Chart_frm frm = new Chart_frm();
            frm.Title = "LOẠI KHÁCH HÀNG";
            if (xtraTabControl1.SelectedTabPageIndex == 0)
                frm.QueryString = Transaction().Replace("KhachHang_Search", "KhachHang_chartCategoryCus");
            else
                frm.QueryString = NotTransaction().Replace("KhachHang_Search2", "KhachHang_chartCategoryCus2");
            frm.ShowDialog();
        }

        private void btnTheoNgheNghiep_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Chart_frm frm = new Chart_frm();
            frm.Title = "NGHỀ NGHIỆP";
            if (xtraTabControl1.SelectedTabPageIndex == 0)
                frm.QueryString = Transaction().Replace("KhachHang_Search", "KhachHang_chartJob");
            else
                frm.QueryString = NotTransaction().Replace("KhachHang_Search2", "KhachHang_chartJob2");
            frm.ShowDialog();
        }

        private void btnTheoLoaiBDS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                DialogBox.Infomation("Khách hàng tiềm năng không lọc theo loại bất động sản, nên không thể xem biểu đồ. Vui lòng kiểm tra lại, xin cảm ơn.");
                return;                
            }
            Chart_frm frm = new Chart_frm();
            frm.Title = "LOẠI BẤT ĐỘNG SẢN";
            frm.QueryString = Transaction().Replace("KhachHang_Search", "KhachHang_chartLoaiBDS");
            
            frm.ShowDialog();
        }

        private void btnExportDoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "File Excel(.xlsx)|*.xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
                gridCaNhan.ExportToXlsx(sfd.FileName);
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            gridControl1.DataSource = null;
        }
    }
}
