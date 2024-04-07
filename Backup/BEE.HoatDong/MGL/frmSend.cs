using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Net.Mail;
using System.Data.Linq.SqlClient;
using it;
using BEE.ThuVien;
using BEEREMA;
using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit.API.Native;

namespace BEE.HoatDong.MGL
{
    public partial class frmSend : DevExpress.XtraEditors.XtraForm
    {
        // public int ID;
        public int MaBC { get; set; }
        public BEE.ThuVien.KhachHang objKH;
        public mglbcNhatKyXuLy objBC { get; set; }
        public mglmtNhatKyXuLy objMT { get; set; }
        public string noidung { get; set; }

        private MasterDataContext db = new MasterDataContext();
        //   private mglbcNhatKyXuLy objNhatKy;

        public frmSend()
        {
            InitializeComponent();
        }
       
        private void frmSend_Load(object sender, EventArgs e)
        {
            if (objKH == null)
                return;
            txtKhachHang.Text = objKH.IsPersonal == true ? objKH.HoKH + " " + objKH.TenKH : objKH.TenCongTy;
            txtEmail.Text = objKH.Email;
            //rtbNoiDung.HtmlText = this.noidung;
            htmlEditorControl1.BodyHtml = this.noidung;
            lookMailGui.Properties.DataSource = db.mailConfigs.Where(p => p.StaffID == Common.StaffID);

            //DocumentRange range = rtbNoiDung.Document.Selection;
            //SubDocument doc = range.BeginUpdateDocument();
            //ParagraphProperties parprop = doc.BeginUpdateParagraphs(range);
            //parprop.LineSpacingType = ParagraphLineSpacing.Double;
            //doc.EndUpdateParagraphs(parprop);
            //range.EndUpdateDocument(doc);

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
            {
                DialogBox.Infomation("Vui lòng nhập Email cho khách hàng này!");
                return;
            }
            #region gửi mail
            // var ListSend = db.mrkListMailSends.Where(p => SqlMethods.DateDiffDay(DateTime.Now, p.DateSend) == 0 && p.CountMail < 500).Select(p => p.MailConfig);
            //   var ListSend = db.mrkListMailSends.Where(p=> p.CountMail < 500).Select(p => p.MailConfig);
            var objConfig = db.mailConfigs.FirstOrDefault(p => p.ID == (int?)lookMailGui.EditValue);
            // var objConfig = db.mailConfigs.FirstOrDefault(p => ListSend.Contains(p.ID) == true && p.IsNoiBo == true);
            //if (objConfig == null)
            //  return;
            MailProviderCls objMail = new MailProviderCls();
            var objMailForm = new MailAddress(objConfig.Email, objConfig.Username);
            objMail.MailAddressFrom = objMailForm;
            var objMailTo = new MailAddress(txtEmail.Text, txtKhachHang.Text);
            objMail.MailAddressTo = objMailTo;
            objMail.SmtpServer = objConfig.Server;
            objMail.EnableSsl = objConfig.EnableSsl.Value;
            objMail.Port = objConfig.Port ?? 465;
            objMail.PassWord = EncDec.Decrypt(objConfig.Password);
            objMail.Subject = txtTieuDe.Text;
            //objMail.Content = rtbNoiDung.HtmlText;
            objMail.Content = htmlEditorControl1.BodyHtml;
            objMail.SendMailV3();
            MessageBox.Show("Gửi mail thành công, xin cảm ơn");

            //var objNhatKy = new mglm();
            //objNhatKy.NgayXL = DateTime.Now;
            //objNhatKy.TieuDe = txtTieuDe.Text;
            //objNhatKy.NoiDung = txtNoiDung.Text;
            //objNhatKy.MaPT = (byte)lookPhuongThuc.EditValue;
            //objNhatKy.MaNVG = BEEREMA.Library.Common.StaffID;

            //if (this.ID == 0)
            //{
            //    objNhatKy.MaNVN = objNhatKy.mglbcBanChoThue.MaNVKD;
            //    db.mglbcNhatKyXuLies.InsertOnSubmit(objNhatKy);
            //}
            //db.SubmitChanges();
            #endregion

            this.Close();
        }

    }
}