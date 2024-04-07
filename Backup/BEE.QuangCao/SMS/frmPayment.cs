using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.QuangCao.SMS
{
    public partial class frmPayment : DevExpress.XtraEditors.XtraForm
    {
        public string PayUrl { get; set; }

        DevExpress.Utils.WaitDialogForm wait;

        public frmPayment()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            wait = DialogBox.WaitingForm();
            webPayment.Navigate(PayUrl);
        }

        private void webPayment_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            wait.Close();
        }
    }
}