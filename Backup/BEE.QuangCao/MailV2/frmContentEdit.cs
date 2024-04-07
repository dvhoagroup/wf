using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using System.Data.Linq.SqlClient;
using System.Net.Mail;
using it;
using System.Threading;
using BEEREMA;

namespace BEE.QuangCao.MailV2
{
    public partial class frmContentEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public int? ID { get; set; }
        mrkMailSendWork objMail;
        DateTime Now;
        List<KhachHang.ItemSelect> ListKH = new List<KhachHang.ItemSelect>();

        public frmContentEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmContentEdit_Load(object sender, EventArgs e)
        {
            Now = db.GetSystemDate();
            lookNhanVien.Properties.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.HoTen });
            lookEmail.DataSource = db.mailConfigs;//.Where(p => p.IsNoiBo.GetValueOrDefault() == false);
            if (ID == null)
            {
                objMail = new mrkMailSendWork();
                db.mrkMailSendWorks.InsertOnSubmit(objMail);
                dateNgayNHap.EditValue = Now;
                lookNhanVien.EditValue = Common.StaffID;
            }
            else
            {
                objMail = db.mrkMailSendWorks.FirstOrDefault(p => p.ID == ID);
                txtNoiDung.InnerHtml = objMail.NoiDung;
                txtTieuDe.Text = objMail.TieuDe;
                chkNoiBo.Checked = objMail.IsNoiBo.GetValueOrDefault();
                dateNgayNHap.EditValue = objMail.NgayLap;
                lookNhanVien.EditValue = objMail.MaNV;
            }
            gcMailGui.DataSource = objMail.mrkListMailSends;
            gcMailNhan.DataSource = objMail.mrkListMailRecives;
        }

        private void itemIPKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new KhachHang.FindMulti_frm();
            frm.ShowDialog();
            if (frm.List.Count() != 0)
            {
                ListKH = frm.List.ToList();
                foreach (var p in ListKH)
                {
                    gvMailNhan.AddNewRow();
                    gvMailNhan.SetFocusedRowCellValue("Email", p.Email);
                    gvMailNhan.SetFocusedRowCellValue("MaKH", p.MaKH);
                }
                //gcMailNhan.DataSource = objMail.mrkListMailRecives;
            }
        }

        private void itemIPNhanVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemIPMailNgoai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                        Email = p[0].ToString().Trim(),
                        NoiDung = p[1].ToString().Trim()
                    }).ToList();

                    List<KhachHang.ItemSelect> newlist = new List<KhachHang.ItemSelect>();
                    if (item.Count() == 0)
                        return;
                    foreach (var it in item)
                    {
                        gvMailNhan.AddNewRow();
                        gvMailNhan.SetFocusedRowCellValue("Email", it.Email);
                        gvMailNhan.SetFocusedRowCellValue("NoiDung", it.NoiDung);
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

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gvMailGui_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvMailGui.SetFocusedRowCellValue("CountMail", 0);
            //if(gvMailGui.GetFocusedRowCellValue("Email"))
        }

        private void itemSaveSend_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            objMail.NoiDung = txtNoiDung.InnerHtml;
            objMail.TieuDe = txtTieuDe.Text;
            objMail.MaNV = (int?)lookNhanVien.EditValue;
            objMail.NgayLap = (DateTime?)dateNgayNHap.EditValue;
            objMail.IsNoiBo = chkNoiBo.Checked;
            db.SubmitChanges();
            this.Close();
            Thread th = new Thread(new ThreadStart(ThreadSendMail));
            th.ApartmentState = ApartmentState.MTA;
            th.IsBackground = true;
            th.Start();
            #region
            //try
            //{
            //    objMail.NoiDung = txtNoiDung.InnerText;
            //    objMail.TieuDe = txtTieuDe.Text;
            //    objMail.MaNV = (int?)lookNhanVien.EditValue;
            //    objMail.NgayLap = (DateTime?)dateNgayNHap.EditValue;
            //    objMail.IsNoiBo = chkNoiBo.Checked;
            //    db.SubmitChanges();
            //    var ListSend = objMail.mrkListMailSends.ToList();
            //    var ListRec = objMail.mrkListMailRecives.ToList();
            //    int CountRec = 0;
            //    for (int i = 0; i < ListSend.Count; i++)
            //    {
            //        while (CountRec < ListRec.Count)
            //        {
            //            if (i == ListSend.Count )
            //                i = 0;

            //            #region gửi mail
            //            var objCOmpany = db.Companies.FirstOrDefault();
            //            var MailTo = db.mrkListMailRecives.First(p => p.ID == ListRec[CountRec].ID);
            //            if (MailTo == null)
            //                return;
            //            var objConfig = db.mailConfigs.FirstOrDefault(p => p.ID == ListSend[i].MailConfig);
            //            MailProviderCls obj = new MailProviderCls();
            //            var objMailForm = new MailAddress(objConfig.Email, objConfig.Username);
            //            obj.MailAddressFrom = objMailForm;
            //            var objMailTo = new MailAddress(MailTo.Email);//, MailTo.NoiDung);
            //            obj.MailAddressTo = objMailTo;
            //            obj.SmtpServer = objConfig.Server;
            //            obj.EnableSsl = objConfig.EnableSsl.Value;
            //            obj.Port = objConfig.Port ?? 465;
            //            obj.PassWord = EncDec.Decrypt(objConfig.Password);
            //            obj.Subject = objMail.TieuDe;
            //            obj.Content = objMail.NoiDung;
            //            obj.SendMailV3();
            //            ListSend[i].CountMail = ListSend[i].CountMail + 1;
            //            ListSend[i].DateSend = Now;
            //            ListRec[CountRec].IsSended = true;
            //            #endregion
            //            CountRec++;
            //        }
            //        db.SubmitChanges();
            //        DialogBox.Infomation("Dữ liệu đã lưu thành công!");
            //        this.Close();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    DialogBox.Error(ex.Message);
            //}
            #endregion
        }

        public void ThreadSendMail()
        {
            try
            {
                var ListSend = objMail.mrkListMailSends.ToList();
                var ListRec = objMail.mrkListMailRecives.ToList();
                int CountRec = 0;
                for (int i = 0; i < ListSend.Count; i++)
                {
                    while (CountRec < ListRec.Count)
                    {
                        if (i == ListSend.Count)
                            i = 0;

                        #region gửi mail
                        var objCOmpany = db.Companies.FirstOrDefault();
                        var MailTo = db.mrkListMailRecives.First(p => p.ID == ListRec[CountRec].ID);
                        if (MailTo == null)
                            return;
                        var objConfig = db.mailConfigs.FirstOrDefault(p => p.ID == ListSend[i].MailConfig);
                        MailProviderCls obj = new MailProviderCls();
                        var objMailForm = new MailAddress(objConfig.Email, objConfig.Username);
                        obj.MailAddressFrom = objMailForm;
                        var objMailTo = new MailAddress(MailTo.Email);//, MailTo.NoiDung);
                        obj.MailAddressTo = objMailTo;
                        obj.SmtpServer = objConfig.Server;
                        obj.EnableSsl = objConfig.EnableSsl.Value;
                        obj.Port = objConfig.Port ?? 465;
                        obj.PassWord = EncDec.Decrypt(objConfig.Password);
                        obj.Subject = objMail.TieuDe;
                       
                        
                        obj.Content = txtNoiDung.InnerHtml;
                        obj.SendMailV3();
                        ListSend[i].CountMail = ListSend[i].CountMail + 1;
                        ListSend[i].DateSend = Now;
                        ListRec[CountRec].IsSended = true;
                        #endregion
                        CountRec++;
                        Thread.Sleep(2);
                   //   System.Threading.Thread.Sleep(2);
                    }
                   // db.invoke
                    db.SubmitChanges();
                   // DialogBox.Infomation("Dữ liệu đã lưu thành công!");
                    Thread.Sleep(2);
                    MessageBox.Show("Đã hoàn thành gửi email!");
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }
    }
}
