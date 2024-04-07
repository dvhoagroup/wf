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
using BEEREMA;

namespace BEE.QuangCao.SMS
{
    public partial class frmSelectObject : DevExpress.XtraEditors.XtraForm
    {
        public int GroupID = 0;
        public bool IsUpdate = false;

        int Step = 0;
        int RadioIndex = 0;

        MasterDataContext db = new MasterDataContext();
        SMSListOfRecipient objLOR;

        public frmSelectObject()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        void Import_Load()
        {
            OpenFileDialog frm = new OpenFileDialog();
            frm.Title = "Mở file Excel";
            frm.Filter = "File excel(.xls, .xlsx)|*.xls;*.xlsx";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dip.cmdExcel cmd = new dip.cmdExcel(frm.FileName);
                    DataTable tblList = cmd.ExcelSelect("SMSList$").Tables[0];
                    tblList.Columns.Add("IsCheck", typeof(bool));
                    tblList.Columns["IsCheck"].DefaultValue = false;
                    gcCustomers.DataSource = tblList;
                }
                catch
                {
                    DialogBox.Error("File không đúng định dạng");
                    return;
                }
            }
        }

        void Customer_Load()
        {
            byte? maNKH = (byte?)lookLoaiKH.EditValue;
            if (maNKH == null)
            {
                gcCustomers.DataSource = null;
            }
            gcCustomers.DataSource = db.SMSGroupReceives_getCustomer(maNKH);
        }

        void Customers_Save()
        {
            for (int i = 0; i < gridCustomers.RowCount; i++)
            {
                if ((bool)gridCustomers.GetRowCellValue(i, "IsCheck"))
                {
                    var keyID = Convert.ToInt32(gridCustomers.GetRowCellValue(i, "KeyID"));
                    if (db.SMSListOfRecipients.Where(p => p.GroupID == GroupID & p.CustomerID == keyID).Count() > 0)
                        continue;
                    objLOR = new SMSListOfRecipient();
                    objLOR.GroupID = GroupID;
                    objLOR.CustomerID = keyID;
                    db.SMSListOfRecipients.InsertOnSubmit(objLOR);
                }
            }
            db.SubmitChanges();
        }

        void Staff_Load()
        {
            gcCustomers.DataSource = db.SMSGroupReceives_getStaff();
        }

        void Staff_Save()
        {
            for (int i = 0; i < gridCustomers.RowCount; i++)
            {
                if ((bool)gridCustomers.GetRowCellValue(i, "IsCheck"))
                {
                    var keyID = Convert.ToInt32(gridCustomers.GetRowCellValue(i, "KeyID"));
                    if (db.SMSListOfRecipients.Where(p => p.GroupID == GroupID & p.StaffID == keyID).Count() > 0)
                        continue;
                    objLOR = new SMSListOfRecipient();
                    objLOR.GroupID = GroupID;
                    objLOR.StaffID = keyID;
                    db.SMSListOfRecipients.InsertOnSubmit(objLOR);
                }
            }
            db.SubmitChanges();
        }

        void Import_Save()
        {
            for (int i = 0; i < gridCustomers.RowCount; i++)
            {
                if(((bool?)gridCustomers.GetRowCellValue(i, "IsCheck")).GetValueOrDefault())
                {
                    objLOR = new SMSListOfRecipient();
                    objLOR.GroupID = GroupID;
                    objLOR.Vocative = gridCustomers.GetRowCellValue(i, "Vocative").ToString();
                    objLOR.FullName = gridCustomers.GetRowCellValue(i, "FullName").ToString();
                    try
                    {
                        string[] ns = gridCustomers.GetRowCellValue(i, "BirthDate").ToString().Trim().Split('/');
                        if (ns.Length == 3)
                        {
                            if (int.Parse(ns[0]) > 12)
                                objLOR.BirthDate = new DateTime(int.Parse(ns[2]), int.Parse(ns[0]), int.Parse(ns[1]));
                            else
                                objLOR.BirthDate = new DateTime(int.Parse(ns[2]), int.Parse(ns[1]), int.Parse(ns[0]));
                        }
                        else
                        {
                            objLOR.BirthDate = new DateTime(int.Parse(ns[ns.Length - 1]), 1, 1);
                        }
                    }
                    catch { }
                    objLOR.Mobile = gridCustomers.GetRowCellValue(i, "Mobile").ToString();
                    objLOR.JobTitle = gridCustomers.GetRowCellValue(i, "JobTitle").ToString();
                    objLOR.Department = gridCustomers.GetRowCellValue(i, "Department").ToString();
                    objLOR.HomeAddress = gridCustomers.GetRowCellValue(i, "HomeAddress").ToString();
                    objLOR.CompanyName = gridCustomers.GetRowCellValue(i, "CompanyName").ToString();
                    objLOR.BusinessAddress = gridCustomers.GetRowCellValue(i, "BusinessAddress").ToString();
                    db.SMSListOfRecipients.InsertOnSubmit(objLOR);
                }
            }
            db.SubmitChanges();
        }

        void CheckAll()
        {
            if (chkSelectAll.Checked)
            {
                for (int i = 0; i < gridCustomers.RowCount; i++)
                    gridCustomers.SetRowCellValue(i, "IsCheck", true);
            }
            else
            {
                for (int i = 0; i < gridCustomers.RowCount; i++)
                        gridCustomers.SetRowCellValue(i, "IsCheck", false);
            }
        }

        int CheckCount()
        {
            int count = 0;
            for (int i = 0; i < gridCustomers.RowCount; i++)
                if (((bool?)gridCustomers.GetRowCellValue(i, "IsCheck")).GetValueOrDefault())
                    count++;
            return count;
        }

        private void frmSelectObject_Load(object sender, EventArgs e)
        {
            var list = db.NhomKHs.ToList();
            var nkh = new NhomKH();
            nkh.MaNKH = 0;
            nkh.TenNKH = "[Tất cả]";
            list.Add(nkh);
            lookLoaiKH.Properties.DataSource = list.Select(p => new { p.MaNKH, p.TenNKH }).OrderBy(p => p.MaNKH).ToList();
            btnAction.Enabled = false;
            btnPrevious.Enabled = false;
            lookLoaiKH.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            RadioIndex = radioGroupObject.SelectedIndex;

            Step++;
            radioGroupObject.Visible = false;
            btnOpenFile.Enabled = false;
            if (Step == 1)
            {
                btnPrevious.Enabled = true;
                gcCustomers.Visible = true;
                lblAmountSelect.Visible = false;
                lblStep.Text = "Bước 2/3: Chọn người nhận";
                chkSelectAll.Visible = true;
                chkSelectAll.Checked = false;
                lookLoaiKH.Visible = false;
                switch (RadioIndex)
                {
                    case 0:
                        lookLoaiKH.Visible = true;
                        Customer_Load();
                        break;
                    case 1:
                        Staff_Load();
                        break;
                    case 2:
                        btnOpenFile.Enabled = true;
                        Import_Load();
                        break;
                }
            }
            else
            {
                lblAmountSelect.Text = string.Format("Có {0} người nhận được chọn để đưa vào danh sách. Click nút \"Thực hiện\" để hoàn thành quá trình.", CheckCount());
                btnPrevious.Enabled = true;
                gcCustomers.Visible = false;
                lblAmountSelect.Visible = true;
                btnNext.Enabled = false;
                btnAction.Enabled = true;
                lblStep.Text = "Bước 3/3: Kết thúc quá trình";
                chkSelectAll.Visible = false;
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Step--;
            btnNext.Enabled = true;
            btnAction.Enabled = false;
            lblAmountSelect.Visible = false;
            lookLoaiKH.Visible = false;
            if (Step == 1)
            {
                btnPrevious.Enabled = true;
                gcCustomers.Visible = true;
                lblStep.Text = "Bước 2/3: Chọn người nhận";
                chkSelectAll.Visible = true;
                if (RadioIndex == 0)
                    lookLoaiKH.Visible = true;
                else if (RadioIndex == 2)
                    btnOpenFile.Enabled = true;
            }
            else
            {
                btnPrevious.Enabled = false;
                gcCustomers.Visible = false;
                lblStep.Text = "Bước 1/3: Chọn nguồn";
                radioGroupObject.Visible = true;
                chkSelectAll.Visible = false;
                btnOpenFile.Enabled = false;
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckAll();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            Import_Load();
        }

        private void chkCheck_EditValueChanged(object sender, EventArgs e)
        {
            CheckEdit ckb = (CheckEdit)sender;
            gridCustomers.SetFocusedRowCellValue("IsCheck", ckb.Checked);
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            if (CheckCount() == 0)
            {
                DialogBox.Infomation("Vui lòng chọn người nhận, xin cảm ơn.");
                return;
            }

            try
            {
                switch (RadioIndex)
                {
                    case 0:
                        Customers_Save();
                        break;
                    case 1:
                        Staff_Save();
                        break;
                    case 2:
                        Import_Save();
                        break;
                }
                DialogBox.Infomation();
                IsUpdate = true;
                this.Close();
            }
            catch { }
        }

        private void lookLoaiKH_EditValueChanged(object sender, EventArgs e)
        {
            Customer_Load();
        }
    }
}