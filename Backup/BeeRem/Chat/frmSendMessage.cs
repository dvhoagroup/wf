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

namespace BEEREMA.Chat
{
    public partial class frmSendMessage : DevExpress.XtraEditors.XtraForm
    {
        private int MaNhan { get; set; }
        private string TenNhan { get; set; }
        private bool itMe = false;

        MasterDataContext db = new MasterDataContext();

        public frmSendMessage(int _MaNhan, string _HoTen, byte _MaTT)
        {
            InitializeComponent();

            this.MaNhan = _MaNhan;
            this.Text = _HoTen;
            lblHoTen.Text = _HoTen;
            picStatus.Image = _MaTT == 2 ? Properties.Resources.Online : Properties.Resources.Offline;
            TenNhan = _HoTen.Substring(0, _HoTen.IndexOf('(') - 1);

            this.Load += new EventHandler(frmMessage_Load);
            this.FormClosing += new FormClosingEventHandler(frmMessage_FormClosing);
            btnGui.Click += new EventHandler(btnGui_Click);
        }

        void frmMessage_Load(object sender, EventArgs e)
        {
            timerCallMess.Start();
        }

        void SendMessage()
        {
            try
            {
                if (txtSend.Text.Trim() == "") return;

                var objTN = new chatTinNhan();
                objTN.NoiDung = txtSend.Text.Trim();
                objTN.MaGui = Properties.Settings.Default.StaffID;
                objTN.MaNhan = this.MaNhan;
                db.chatTinNhans.InsertOnSubmit(objTN);
                db.SubmitChanges();

                var htmlMess = "";
                if (!itMe)
                {
                    htmlMess = "<b style='color:Gray'>" + Properties.Settings.Default.StaffName + ": </b>";
                    itMe = true;
                }
                htmlMess += txtSend.Text.Trim();
                htmlMess = string.Format("<span style='font-family:Arial; font-size: 9pt; line-height:15pt'>{0}<br/></span>", htmlMess);
                htmlContent.DocumentText += htmlMess;
                txtSend.Text = "";
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        void frmMessage_FormClosing(object sender, FormClosingEventArgs e)
        {
            ctlFriendList.ltMessBox.Remove(this);
        }

        void btnGui_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void txtSend_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter: SendMessage(); break;
                case Keys.Up: txtSend.Undo(); break;
            }
        }

        private void timerCallMess_Tick(object sender, EventArgs e)
        {
            timerCallMess.Stop();

            var ltMess = db.chatTinNhan_SelectOnline(MaNhan, Properties.Settings.Default.StaffID).ToList();
            if (ltMess.Count > 0)
            {
                string htmlMess = "";
                if (itMe || htmlContent.DocumentText == "")
                {
                    htmlMess = "<b style='color:Blue'>" + TenNhan + ": </b>";
                    itMe = false;
                }
                string htmNoiDung = "";
                foreach (var i in ltMess)
                {
                    htmNoiDung += i.NoiDung + "<br/>";
                }
                htmlMess = string.Format("<span style='font-family:Arial; font-size: 9pt; line-height:15pt'>{0}{1}</span>", htmlMess, htmNoiDung);
                htmlContent.DocumentText += htmlMess;

                if (this.Handle != it.CommonCls.GetForegroundWindow())
                {
                    alMess.Show(this, this.TenNhan, htmNoiDung.Replace("<br/>", "\n"));
                    it.CommonCls.FlashWindow(this.Handle, false);
                }
            }

            timerCallMess.Start();
        }

        private void htmlContent_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            htmlContent.Document.Window.ScrollTo(int.MaxValue, int.MaxValue);
        }

        private void alMess_AlertClick(object sender, DevExpress.XtraBars.Alerter.AlertClickEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Focus();
        }
    }
}