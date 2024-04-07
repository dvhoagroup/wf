using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.QuangCao.SMS
{
    public partial class frmText : DevExpress.XtraEditors.XtraForm
    {
        System.Timers.Timer timer;
        bool busy;

        public frmText()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);

            timer = new System.Timers.Timer();
            timer.Interval = 5000;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //if (busy)
            //    return;
            //else
            //    busy = true;
            timer.Stop();
            MasterDataContext db = new MasterDataContext();
            try
            {
                var mail = db.mailSendingLists.Where(p => p.Status == 1 & p.mailSending.Active == true &
                    System.Data.Linq.SqlClient.SqlMethods.DateDiffMinute(p.mailSending.DateSend, DateTime.Now) >= 0)
                    .OrderBy(p => db.Random())
                    .Select(p => new
                    {
                        p.ID,
                        p.SendID,
                        p.mailSending.mailConfig.Server,
                        p.mailSending.mailConfig.EnableSsl,
                        EmailFrom = p.mailSending.mailConfig.Email,
                        p.mailSending.mailConfig.Password,
                        p.mailSending.mailConfig.SendMax,
                        p.mailSending.Title,
                        p.mailSending.Contents,
                        p.Vocative,
                        p.FullName,
                        p.BirthDate,
                        p.Phone,
                        EmailTo = p.Email,
                        p.HomeAddress,
                        p.JobTitle,
                        p.Department,
                        p.CompanyName,
                        p.BusinessAddress
                    }).Take(3).ToList();

                if (mail.Count <= 0) return;

                foreach (var m in mail)
                {
                    var objSMS = db.mailSendingLists.Single(p => p.ID == m.ID);
                    objSMS.DateSend = DateTime.Now;
                    try
                    {
                        string message = m.Contents;
                        message = message.Replace("[Vocative]", m.Vocative);
                        message = message.Replace("[FirstName]", m.FullName.LastIndexOf(' ') >= 0 ? m.FullName.Substring(0, m.FullName.LastIndexOf(' ')) : "");
                        message = message.Replace("[LastName]", m.FullName.Substring(m.FullName.LastIndexOf(' ') + 1));
                        message = message.Replace("[FullName]", m.FullName);
                        message = message.Replace("[BirthDate]", string.Format("{0:dd/MM/yyyy}", m.BirthDate));
                        message = message.Replace("[Phone]", m.Phone);
                        message = message.Replace("[Email]", m.EmailTo);
                        message = message.Replace("[HomeAddress]", m.HomeAddress);
                        message = message.Replace("[JobTitle]", m.JobTitle);
                        message = message.Replace("[Department]", m.Department);
                        message = message.Replace("[CompanyName]", m.CompanyName);
                        message = message.Replace("[BusinessAddress]", m.BusinessAddress);

                        MailProviderCls objMail = new MailProviderCls();
                        objMail.MailTo = m.EmailTo;
                        objMail.Subject = m.Title;
                        objMail.Content = message;
                        objMail.SendMail();

                        objSMS.Status = 2;
                    }
                    catch
                    {
                        objSMS.Status = 3;
                    }
                }

                db.SubmitChanges();

                DialogBox.Infomation("ok");
            }
            catch (Exception ex)
            {
                DialogBox.Infomation(ex.Message);
            }
            finally
            {
                db.Dispose();
                timer.Start();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MasterDataContext db = new MasterDataContext();
            try
            {
                var sms = db.SMSListWaitSendings.Where(p => p.Status == 1 & p.SMSSending.IsActive == true &
                    System.Data.Linq.SqlClient.SqlMethods.DateDiffMinute(p.SMSSending.DateSend, DateTime.Now) >= 0)
                    .OrderBy(p => db.Random())
                    .Select(p => new
                    {
                        p.KeyID,
                        p.SendID,
                        p.SMSSending.Sendername,
                        p.SMSSending.Contents,
                        p.Vocative,
                        p.FullName,
                        p.BirthDate,
                        p.Mobile,
                        p.HomeAddress,
                        p.JobTitle,
                        p.Department,
                        p.CompanyName,
                        p.BusinessAddress
                    }).Take(3).ToList();

                if (sms.Count <= 0) return;

                SmsConfig objConfig = new SmsConfig();
                objConfig.getAccount();
                DIPSMS.Hotting hot = new DIPSMS.Hotting(objConfig.ClientNo, objConfig.ClientPass);

                foreach (var s in sms)
                {
                    try
                    {
                        string message = s.Contents;
                        message = message.Replace("[Vocative]", s.Vocative);
                        message = message.Replace("[FirstName]", s.FullName.LastIndexOf(' ') >= 0 ? s.FullName.Substring(0, s.FullName.LastIndexOf(' ')) : "");
                        message = message.Replace("[LastName]", s.FullName.Substring(s.FullName.LastIndexOf(' ') + 1));
                        message = message.Replace("[FullName]", s.FullName);
                        message = message.Replace("[BirthDate]", string.Format("{0:dd/MM/yyyy}", s.BirthDate));
                        message = message.Replace("[Mobile]", s.Mobile);
                        message = message.Replace("[HomeAddress]", s.HomeAddress);
                        message = message.Replace("[JobTitle]", s.JobTitle);
                        message = message.Replace("[Department]", s.Department);
                        message = message.Replace("[CompanyName]", s.CompanyName);
                        message = message.Replace("[BusinessAddress]", s.BusinessAddress);

                        var result = hot.SendMaskedSMS(s.Sendername, s.Mobile, message, s.KeyID + "|" + s.SendID);

                        var objSMS = db.SMSListWaitSendings.Single(p => p.KeyID == s.KeyID);
                        objSMS.DateSend = DateTime.Now;
                        objSMS.Status = (byte)(result == 200 ? 2 : 3);
                    }
                    catch { }
                }

                db.SubmitChanges();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            timer.Start();
        }
    }
}