using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Net;
using System.Linq;
using BEE.ThuVien;
using System.Collections;

namespace BEEREMA.HeThong
{
    public partial class Login_frm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        bool click = false, first = true;
        short LangID = 0;
        public int? manv { get; set; }
        public Login_frm()
        {
            InitializeComponent();
            defaultLookAndFeel1.LookAndFeel.SkinName = Common.Skins;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            Application.Exit();
        }

        private void Login_frm_Load(object sender, EventArgs e)
        {
            LangID = BEE.NgonNgu.Language.LangID;

            if (LangID != 1)
                itemForgotPassword.Caption = "Forgot your password?";

            if (Properties.Settings.Default.SavePassword)
            {
                chkGhiNho.Checked = true;
                txtMaSo.Text = Properties.Settings.Default.UserName;
                txtMatKhau.Text = it.CommonCls.GiaiMa(Properties.Settings.Default.Password);
            }

            using (var db = new BEE.ThuVien.MasterDataContext())
            {
                lookUpLanguage.Properties.DataSource = db.lgLanguages;
                lookUpLanguage.ItemIndex = 0;

                lookUpLanguage.EditValue = LangID;
            }
            first = false;

            BEE.NgonNgu.Language.TranslateControl(this);
            this.Text = "LOGIN - " + "HÒA LAND";
        }

        //Duoc quyen duyet PGC
        void LoadPermissionConfirmPlaceHolder()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Properties.Settings.Default.PerID;
            o.AccessData.Form.FormID = 27;
            DataTable tblAction = o.SelectBy(7);

            if (tblAction.Rows.Count > 0)
                Properties.Settings.Default.ConfirmPlaceHolder = true;
            else
                Properties.Settings.Default.ConfirmPlaceHolder = false;
        }

        //Duoc quyen duyet PDC
        void LoadPermissionConfirmDeposit()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Properties.Settings.Default.PerID;
            o.AccessData.Form.FormID = 28;
            DataTable tblAction = o.SelectBy(7);

            if (tblAction.Rows.Count > 0)
                Properties.Settings.Default.ConfirmDeposit = true;
            else
                Properties.Settings.Default.ConfirmDeposit = false;
        }

        //Duoc quyen duyet nhac no
        void LoadPermissionConfrimConsider()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Properties.Settings.Default.PerID;
            o.AccessData.Form.FormID = 29;
            DataTable tblAction = o.SelectBy(10);

            if (tblAction.Rows.Count > 0)
                Properties.Settings.Default.ConfrimConsider = true;
            else
                Properties.Settings.Default.ConfrimConsider = false;
        }

        //Duoc quyen duyet YCHTro
        void LoadPermissionConfirmRequest()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Properties.Settings.Default.PerID;
            o.AccessData.Form.FormID = 23;
            DataTable tblAction = o.SelectBy(7);

            if (tblAction.Rows.Count > 0)
                Properties.Settings.Default.ConfirmRequest = true;
            else
                Properties.Settings.Default.ConfirmRequest = false;
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            string s = txtMaSo.Text.Trim().ToLower().Replace("delete", "").Replace("update", "").Replace("alter", "").Replace("drop", "").Replace("select", "").Replace("--", "").Replace("/*", "").Replace("*/", "");
            DataRow r = it.CommonCls.Row("NhanVien_Login '" + s + "'");
            if (r == null)
            {
                DialogBox.Error("[Mã số] hoặc [Mật khẩu] không chính xác.");
                return;
            }

            if (txtMatKhau.Text.Trim() != it.CommonCls.GiaiMa(r["MatKhau"].ToString()))
            {
                DialogBox.Error("[Mã số] hoặc [Mật khẩu] không chính xác.");
                return;
            }
            manv = BEE.ThuVien.Common.StaffID;

            BEE.ThuVien.Common.StaffID = int.Parse(r["MaNV"].ToString());
            BEE.ThuVien.Common.StaffName = r["HoTen"].ToString();
            Properties.Settings.Default.SavePassword = chkGhiNho.Checked;
            Properties.Settings.Default.UserName = txtMaSo.Text;
            Properties.Settings.Default.Password = it.CommonCls.MaHoa(txtMatKhau.Text);
            BEE.ThuVien.Common.PerID = int.Parse(r["PerID"].ToString());
            BEE.ThuVien.Common.PerName = r["PerName"].ToString();
            BEE.ThuVien.Common.DepartmentID = byte.Parse(r["MaPB"].ToString());
            BEE.ThuVien.Common.GroupID = byte.Parse(r["MaNKD"].ToString());
            Properties.Settings.Default.IsCDT = (bool)r["IsCDT"];

            LoadPermissionConfirmDeposit();
            LoadPermissionConfirmPlaceHolder();
            LoadPermissionConfrimConsider();
            LoadPermissionConfirmRequest();
            //LS dang Nhap

            using (var db = new MasterDataContext())
            {
                var cname = Environment.MachineName;
                var address = Dns.GetHostEntry(cname).AddressList;
                string iplocal = string.Empty;
                for (int i = 0; i < address.Length; i++)
                {
                    iplocal += "==" + address[i].ToString();
                }
                var objLSDN = new LichSuDangNhapNV();
                objLSDN.ComputerName = Environment.MachineName;
                objLSDN.ThoiGian = DateTime.Now;
                objLSDN.MaNV = Common.StaffID;
                objLSDN.DiaChiIP = iplocal;
                objLSDN.Online = true;
                db.LichSuDangNhapNVs.InsertOnSubmit(objLSDN);
               // db.SubmitChanges();
                Properties.Settings.Default.IDLogin = objLSDN.ID;
            }
            //

            Properties.Settings.Default.Save();

            BEE.NgonNgu.Language.LangID = Convert.ToInt16(lookUpLanguage.EditValue);

            this.DialogResult = DialogResult.OK;
        }

        private void chkGhiNho_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SavePassword = chkGhiNho.Checked;
            Properties.Settings.Default.Save();
        }

        private void hyperLinkEdit1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://beesky.vn");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Connect_frm frm = new Connect_frm();
            frm.ShowDialog();
        }

        private void itemForgotPassword_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var f = new frmForgotPassword())
            {
                f.ShowDialog();
            }
        }

        private void lookUpLanguage_EditValueChanged(object sender, EventArgs e)
        {
            BEE.NgonNgu.Language.LangID = Convert.ToInt16(lookUpLanguage.EditValue);

            if (!first)
            {
                if (BEE.NgonNgu.Language.LangID != 1)
                {
                    BEE.NgonNgu.Language.TranslateControl(this);

                    itemForgotPassword.Caption = "Forgot your password?";
                }
                else
                {
                    lblMaSo.Text = lblMaSo.Tag.ToString();
                    lblMatKhau.Text = lblMatKhau.Tag.ToString();
                    lblNgonNgu.Text = lblNgonNgu.Tag.ToString();
                    btnConnect.Text = btnConnect.Tag.ToString();
                    btnDongY.Text = btnDongY.Tag.ToString();
                    btnHuy.Text = btnHuy.Tag.ToString();
                    itemForgotPassword.Caption = "Quên mật khẩu?";
                    chkGhiNho.Text = chkGhiNho.Tag.ToString();
                    this.Text = "Đăng nhập - " + Properties.Settings.Default.SoftName;
                }
            }
        }
    }
}