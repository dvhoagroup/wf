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
    public partial class frmSendSMS : DevExpress.XtraEditors.XtraForm
    {
        // public int ID;
        public int MaBC { get; set; }
        public BEE.ThuVien.KhachHang objKH;
        public mglbcNhatKyXuLy objBC { get; set; }
        public mglmtNhatKyXuLy objMT { get; set; }
        public string noidung { get; set; }

        public bool? isDaGui { get; set; }

        public List<Dictionary<BEE.ThuVien.KhachHang, string>> dicSms;


        private MasterDataContext db = new MasterDataContext();
        //   private mglbcNhatKyXuLy objNhatKy;

        public frmSendSMS()
        {
            InitializeComponent();
        }

        private void frmSend_Load(object sender, EventArgs e)
        {
            if (dicSms != null)
            {
                var objDic = dicSms.FirstOrDefault();
                objKH = objDic.Keys.FirstOrDefault();
                txtKhachHang.Text = objKH.IsPersonal == true ? objKH.HoKH + " " + objKH.TenKH : objKH.TenCongTy;
                //txtEmail.Text = objKH.Email;
                htmlEditorControl1.BodyHtml = objDic.Values.FirstOrDefault();
                lookMailGui.Properties.DataSource = db.mailConfigs.Where(p => p.StaffID == Common.StaffID);
            }
            else
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
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            //if (txtEmail.Text.Trim() == "")
            //{
            //    DialogBox.Infomation("Vui lòng nhập Email cho khách hàng này!");
            //    return;
            //}
            #region gửi

            this.isDaGui = true;
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
            db.SubmitChanges();
            db.Dispose();
            MessageBox.Show("Gửi thành công");
            #endregion

            this.Close();
        }

    }
}