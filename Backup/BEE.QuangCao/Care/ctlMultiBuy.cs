using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.QuangCao.Care
{
    public partial class ctlMultiBuy : UserControl
    {
        MasterDataContext db;
        public ctlMultiBuy()
        {
            InitializeComponent();

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize2(cmbKyBaoCao);
        }

        void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate2();

            itemTuNgay.EditValueChanged -= new EventHandler(itemDenNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemDenNgay_EditValueChanged);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                DateTime tuNgay = itemTuNgay.EditValue != null ? (DateTime)itemTuNgay.EditValue : DateTime.Now;
                DateTime denNgay = itemDenNgay.EditValue != null ? (DateTime)itemDenNgay.EditValue : DateTime.Now;
                byte maNKH = itemNhomKH.EditValue != null ? (byte)itemNhomKH.EditValue : (byte)0;
                int ageFrom = 0;
                int ageTo = 0;
                GetAge(ref ageFrom, ref ageTo);
                //gcHappyBirthday.DataSource = db.KhachHang_getBirthday(tuNgay, denNgay, maNKH, ageFrom, ageTo);
            }
            catch { }
            wait.Close();
        }

        void GetAge(ref int ageFrom, ref int ageTo)
        {
            if (itemAge.EditValue == null)
            {
                ageFrom = 0;
                ageTo = 0;
            }
            else
            {
                switch (itemAge.EditValue.ToString())
                {
                    case "Tất cả":
                        ageFrom = 0;
                        ageTo = 0;
                    break;
                    case "20 - 29":
                        ageFrom = 20;
                        ageTo = 29;
                        break;
                    case "30 - 39":
                        ageFrom = 30;
                        ageTo = 39;
                        break;
                    case "40 - 49":
                        ageFrom = 40;
                        ageTo = 49;
                        break;
                    case ">= 50":
                        ageFrom = 50;
                        ageTo = 10000000;
                        break;
                }
            }
        }

        private void ctlHappyBirthday_Load(object sender, EventArgs e)
        {
            SetDate(3);

            db = new MasterDataContext();
            lookUpNhomKH.DataSource = db.NhomKHs;
            replookUpNhomKH.DataSource = db.NhomKHs;

            LoadData();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemNhomKH_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemAge_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemGift_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = gvHappyBirthday.GetSelectedRows();

            if (rows.Length <= 0)
            {
                DialogBox.Infomation("Vui lòng chọn <Khách hàng>, xin cảm ơn.");
                return;
            }

            List<int> listCustomer = new List<int>();
            foreach (int r in rows)
            {
                listCustomer.Add(Convert.ToInt32(gvHappyBirthday.GetRowCellValue(r, "MaKH")));
            }

            var f = new frmHappyBirthday();
            f.CateID = 1;
            f.listCustomer = listCustomer;
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                LoadData();
        }

        private void itemMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] Rows = gvHappyBirthday.GetSelectedRows();
            if (Rows.Length <= 0)
            {
                DialogBox.Infomation("Vui lòng chọn <Khách hàng>, xin cảm ơn.");
                return;
            }

            var fConfirm = new frmConfirm();
            fConfirm.ShowDialog();
            if (fConfirm.DialogResult == DialogResult.OK)
            {
                if (fConfirm.IsNow)
                {
                    //tao danh sach nguoi nhan
                    var objGroup = new mailReceive();
                    objGroup.DateCreate = DateTime.Now;
                    objGroup.DateModify = DateTime.Now;
                    objGroup.StaffID = BEE.ThuVien.Common.StaffID;
                    objGroup.StaffModify = BEE.ThuVien.Common.StaffID;
                    db.mailReceives.InsertOnSubmit(objGroup);
                    objGroup.ReceName = "Tập đoàn BIM gửi mail chúc mừng sinh nhật";
                    objGroup.Description = "Hệ thống tự động tạo danh sách người nhận theo yêu cầu của " + BEE.ThuVien.Common.StaffName;
                    db.mailReceives.InsertOnSubmit(objGroup);

                    db.SubmitChanges();

                    SetHappyBirthday setHB = db.SetHappyBirthdays.Single(p => p.SetID == 5);
                    //add CareCustomer
                    CareCustomer objCare;
                    foreach (int i in Rows)
                    {
                        objCare = new CareCustomer();
                        objCare.CustomerID = Convert.ToInt32(gvHappyBirthday.GetRowCellValue(i, "MaKH"));
                        objCare.CateID = 2;
                        objCare.Description = setHB.NoiDung;
                        objCare.StaffID = BEE.ThuVien.Common.StaffID;
                        objCare.StatusID = 1;
                        db.CareCustomers.InsertOnSubmit(objCare);
                        db.SubmitChanges();

                        gvHappyBirthday.SetRowCellValue(i, "CareID", objCare.KeyID);
                    }

                    //add KH vao danh sach nguoi nhan vua tao
                    foreach (int i in Rows)
                    {
                        var o = new mailReceiveList();
                        o.CusID = Convert.ToInt32(gvHappyBirthday.GetRowCellValue(i, "MaKH"));
                        o.CareID = Convert.ToInt32(gvHappyBirthday.GetRowCellValue(i, "CareID"));
                        objGroup.mailReceiveLists.Add(o);
                    }
                    db.SubmitChanges();

                    //Tao viec gui
                    var objSend = new mailSending();
                    objSend.Title = "Tập đoàn BIM gửi mail chúc mừng sinh nhật";
                    objSend.Active = false;
                    objSend.DateSend = DateTime.Now;
                    objSend.DateCreate = DateTime.Now;
                    objSend.DateModify = DateTime.Now;
                    objSend.StaffModify = BEE.ThuVien.Common.StaffID;
                    objSend.StaffID = BEE.ThuVien.Common.StaffID;
                    
                    objSend.Contents = setHB.NoiDung;
                    objSend.MailID = setHB.MailID;

                    db.mailSendings.InsertOnSubmit(objSend);

                    //SMSGroupReceive_Sendings
                    var objGR = new mailSendingReceive();
                    objGR.ReceID = objGroup.ReceID;
                    objSend.mailSendingReceives.Add(objGR);

                    db.SubmitChanges();

                    DialogBox.Infomation("Dữ liệu đã được cập nhật. Hệ thống sẽ gửi mail trong thời gian sớm nhất.");
                }
                else//Tao danh sach nguoi nhan
                {
                    var ListReminder = new List<bdsSanPham>();
                    bdsSanPham o;
                    foreach (int r in Rows)
                    {
                        o = new bdsSanPham();
                        o.MaKH = Convert.ToInt32(gvHappyBirthday.GetRowCellValue(r, "MaKH"));
                        o.ThanhTien = 0;
                        ListReminder.Add(o);
                    }

                    var f = new QuangCao.Mail.frmGroupReminders();
                    f.IsCare = true;
                    f.ListReminder = ListReminder;
                    f.ShowDialog();
                    ListReminder = null;
                    System.GC.Collect();
                }
            }
        }

        private void itemSMS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] Rows = gvHappyBirthday.GetSelectedRows();
            if (Rows.Length <= 0)
            {
                DialogBox.Infomation("Vui lòng chọn <Khách hàng>, xin cảm ơn.");
                return;
            }

            var fConfirm = new frmConfirm();
            fConfirm.ShowDialog();
            if (fConfirm.DialogResult == DialogResult.OK)
            {
                if (fConfirm.IsNow)
                {
                    //tao danh sach nguoi nhan
                    var objGroup = new SMSGroupReceive();
                    objGroup.DateCreate = DateTime.Now;
                    objGroup.DateModify = DateTime.Now;
                    objGroup.StaffID = BEE.ThuVien.Common.StaffID;
                    objGroup.StaffModify = BEE.ThuVien.Common.StaffID;
                    db.SMSGroupReceives.InsertOnSubmit(objGroup);
                    objGroup.GroupName = "Tập đoàn BIM gửi SMS chúc mừng sinh nhật";
                    objGroup.Description = "Hệ thống tự động tạo danh sách người nhận theo yêu cầu của " + BEE.ThuVien.Common.StaffName;
                    db.SMSGroupReceives.InsertOnSubmit(objGroup);

                    db.SubmitChanges();

                    SetHappyBirthday setHB = db.SetHappyBirthdays.Single(p => p.SetID == 6);
                    //add CareCustomer
                    CareCustomer objCare;
                    foreach (int i in Rows)
                    {
                        objCare = new CareCustomer();
                        objCare.CustomerID = Convert.ToInt32(gvHappyBirthday.GetRowCellValue(i, "MaKH"));
                        objCare.CateID = 3;
                        objCare.Description = setHB.NoiDung;
                        objCare.StaffID = BEE.ThuVien.Common.StaffID;
                        objCare.StatusID = 1;
                        db.CareCustomers.InsertOnSubmit(objCare);
                        db.SubmitChanges();

                        gvHappyBirthday.SetRowCellValue(i, "CareID", objCare.KeyID);
                    }

                    //add KH vao danh sach nguoi nhan vua tao
                    //SMSListOfRecipient o;
                    foreach (int i in Rows)
                    {
                        var o = new SMSListOfRecipient();
                        //o.GroupID = objGroup.GroupID;
                        o.CustomerID = Convert.ToInt32(gvHappyBirthday.GetRowCellValue(i, "MaKH"));
                        objGroup.SMSListOfRecipients.Add(o);
                    }
                    db.SubmitChanges();

                    //Tao viec gui
                    var objSend = new SMSSending();
                    objSend.Title = "Tập đoàn BIM gửi SMS chúc mừng sinh nhật";
                                        
                    var Sendernames = db.SetHappyBirthdays.Where(p => p.SetID == 6).Select(p => p.NoiDung).ToList();
                    objSend.Sendername = setHB.Sendername;
                    objSend.Contents = setHB.NoiDung;

                    objSend.IsActive = false;
                    objSend.DateSend = DateTime.Now;
                    objSend.DateCreate = DateTime.Now;
                    objSend.DateModify = DateTime.Now;
                    objSend.StaffModify = BEE.ThuVien.Common.StaffID;
                    objSend.StaffID = BEE.ThuVien.Common.StaffID;

                    db.SMSSendings.InsertOnSubmit(objSend);

                    //SMSGroupReceive_Sendings
                    var objGR = new SMSGroupReceive_Sending();
                    objGR.GroupID = objGroup.GroupID;
                    objSend.SMSGroupReceive_Sendings.Add(objGR);

                    db.SubmitChanges();

                    DialogBox.Infomation("Dữ liệu đã được cập nhật. Hệ thống sẽ gửi SMS trong thời gian sớm nhất.");
                }
                else//Tao danh sach nguoi nhan
                {
                    var ListReminder = new List<bdsSanPham>();
                    bdsSanPham o;
                    foreach (int r in Rows)
                    {
                        o = new bdsSanPham();
                        o.MaKH = Convert.ToInt32(gvHappyBirthday.GetRowCellValue(r, "MaKH"));
                        o.ThanhTien = 0;
                        ListReminder.Add(o);
                    }

                    var f = new QuangCao.SMS.frmGroupReminders();
                    f.IsCare = true;
                    f.ListReminder = ListReminder;
                    f.ShowDialog();
                    ListReminder = null;
                    System.GC.Collect();
                }
            }
        }

        private void gvHappyBirthday_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvHappyBirthday.FocusedRowHandle >= 0)
            {
                DateTime denNgay = itemDenNgay.EditValue != null ? (DateTime)itemDenNgay.EditValue : DateTime.Now;
                gcQTTH.DataSource = db.CareCustomer_getByCustomer(Convert.ToInt32(gvHappyBirthday.GetFocusedRowCellValue("MaKH")), denNgay.Year);
            }
        }

        private void itemProcess_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = gvHappyBirthday.GetSelectedRows();

            if (rows.Length <= 0)
            {
                DialogBox.Infomation("Vui lòng chọn <Khách hàng> đã gửi quà, xin cảm ơn.");
                return;
            }

            List<int> listCustomer = new List<int>();
            foreach (int r in rows)
            {
                if (int.Parse(gvHappyBirthday.GetFocusedRowCellValue("IsGift").ToString()) != 0)
                    listCustomer.Add(Convert.ToInt32(gvHappyBirthday.GetRowCellValue(r, "MaKH")));
            }

            if (listCustomer.Count <= 0)
            {
                DialogBox.Infomation("Vui lòng chọn <Khách hàng> đã gửi quà, xin cảm ơn.");
                return;
            }

            var f = new frmHappyBirthday();
            f.IsProcess = true;
            f.CateID = 1;
            f.listCustomer = listCustomer;
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                LoadData();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}
