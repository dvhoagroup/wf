using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.NghiepVuKhac
{
    public partial class ChoiceStaff_frm : DevExpress.XtraEditors.XtraForm
    {
        public bool IsUpdate = false;
        public List<int> Customers = new List<int>();
        public List<string> Products = new List<string>();
        public byte CateID = 0;
        public int MaNV = 0, MaDL = 0;
        public ChoiceStaff_frm()
        {
            InitializeComponent();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNV) == null)
            {
                DialogBox.Infomation("Vui lòng chọn [Nhân viên] tiếp nhận, xin cảm ơn.");
                return;
            }
            else
            {
                MaNV = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colMaNV));
                MaDL = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colMaDL));
            }
            string str = "chuyển quyền";
            if (CateID == 3)
                str = "thêm quyền";
            if (DialogBox.Question("Bạn có chắc chắn muốn " + str + " cho [Nhân viên] này không?") == System.Windows.Forms.DialogResult.No) return;

            if ((CateID == 1 | CateID == 2) & radioGroup1.SelectedIndex == 1)
            {
                DialogBox.Infomation("Bạn không được phép chuyển quyền cho [Nhân viên sàn].\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                return;
            }

            if (CateID != 4)
            {
                switch (CateID)
                {
                    case 1://Khach hang
                        if (radioGroup1.SelectedIndex == 0)
                            SaveCustomers();
                        else
                            SaveCustomersAgent();
                        break;
                    case 2://San pham
                        SaveProducts();
                        break;
                    case 3://Them
                        if (radioGroup1.SelectedIndex == 0)
                            AddNew();
                        else
                            AddNewAgent();
                        break;
                }
                DialogBox.Infomation();                
            }
            IsUpdate = true;
            this.Close();
        }

        void SaveCustomers()
        {
            it.KhachHangCls o;
            it.khNotesTransferCls objNote;
            it.Staff_CustomerCls objStaff;
            foreach (int item in Customers)
            {
                objNote = new it.khNotesTransferCls();
                objNote.MaKH = item;
                objNote.MaNVC = BEE.ThuVien.Common.StaffID;
                objNote.MaNVNew = MaNV;
                objNote.Insert();

                o = new it.KhachHangCls();
                o.MaKH = item;
                o.NhanVien.MaNV = MaNV;
                o.UpdateStaff();

                objStaff = new it.Staff_CustomerCls();
                objStaff.CustomerID = item;
                objStaff.StaffID = MaNV;
                objStaff.Insert();
            }
        }

        void SaveCustomersAgent()
        {
            it.khNotesTransferCls objNote;
            it.aCustomerCls objStaff;
            foreach (int item in Customers)
            {
                objNote = new it.khNotesTransferCls();
                objNote.MaKH = item;
                objNote.MaNVC = BEE.ThuVien.Common.StaffID;
                objNote.MaNVNew = MaNV;
                objNote.IsAgent = radioGroup1.SelectedIndex == 0 ? false : true;
                objNote.Insert();

                objStaff = new it.aCustomerCls();
                objStaff.CustomerID = item;
                objStaff.StaffID = MaNV;
                objStaff.Share();
            }
        }

        void SaveProducts()
        {
            it.BatDongSanCls o;
            it.bdsNotesTransferCls objNote;
            foreach (string item in Products)
            {
                objNote = new it.bdsNotesTransferCls();
                objNote.MaBDS = item;
                objNote.MaNVC = BEE.ThuVien.Common.StaffID;
                objNote.MaNVNew = MaNV;
                objNote.Insert();

                o = new it.BatDongSanCls();
                o.MaBDS = item;
                o.NhanVien.MaNV = MaNV;
                o.UpdateStaff();                
            }
        }

        void AddNew()
        {
            it.Staff_CustomerCls o;
            //it.khNotesTransferCls objNote;
            foreach (int item in Customers)
            {
                //objNote = new it.khNotesTransferCls();
                //objNote.MaKH = item;
                //objNote.MaNVC = BEE.ThuVien.Common.StaffID;
                //objNote.MaNVNew = MaNV;
                //objNote.Insert();

                o = new it.Staff_CustomerCls();
                o.CustomerID = item;
                o.StaffID = MaNV;
                o.Insert();
            }
        }

        void AddNewAgent()
        {
            it.aCustomerCls o;
            //it.khNotesTransferCls objNote;
            foreach (int item in Customers)
            {
                //objNote = new it.khNotesTransferCls();
                //objNote.MaKH = item;
                //objNote.MaNVC = BEE.ThuVien.Common.StaffID;
                //objNote.MaNVNew = MaNV;
                //objNote.IsAgent = radioGroup1.SelectedIndex == 0 ? false : true;
                //objNote.Insert();

                o = new it.aCustomerCls();
                o.CustomerID = item;
                o.StaffID = MaNV;
                o.StaffIDConfirm = BEE.ThuVien.Common.StaffID;
                o.Share();
            }
        }

        private void ChoiceStaff_frm_Load(object sender, EventArgs e)
        {
            LoadStaff();
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
                LoadStaff();
            else
                LoadStaffAgent();
        }

        void LoadStaff()
        {
            it.NhanVienCls o = new it.NhanVienCls();
            gridControl1.DataSource = o.SelectChoiceChange();
        }

        void LoadStaffAgent()
        {
            it.NhanVienDaiLyCls o = new it.NhanVienDaiLyCls();
            gridControl1.DataSource = o.SelectChoiceChange();
        }
    }
}