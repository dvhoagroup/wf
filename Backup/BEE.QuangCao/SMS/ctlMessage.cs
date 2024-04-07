using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.QuangCao.SMS
{
    public partial class ctlMessage : DevExpress.XtraEditors.XtraUserControl
    {
        SmsConfig objConfig;

        public ctlMessage()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);

            objConfig = new SmsConfig();
            objConfig.getAccount();

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBC);

            cmbStatus.SelectedIndex = 1;
            SMS_Load();
        }

        void SMS_Load()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                DIPSMS.Hotting sms = new DIPSMS.Hotting(objConfig.ClientNo, objConfig.ClientPass);
                gridSMS.DataSource = sms.GetListSMSMessage(dateFrom.DateTime, dateTo.DateTime, cmbStatus.SelectedIndex + 1, "");
            }
            catch
            {
                gridSMS.DataSource = null;
            }
            finally
            {
                wait.Close();
            }
        }

        void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            dateFrom.EditValueChanged -= new EventHandler(dateFrom_EditValueChanged);
            dateFrom.EditValue = objKBC.DateFrom;
            dateTo.EditValue = objKBC.DateTo;
            dateFrom.EditValueChanged += new EventHandler(dateFrom_EditValueChanged);
        }

        private void dateFrom_EditValueChanged(object sender, EventArgs e)
        {
            SMS_Load();
        }

        private void dateTo_EditValueChanged(object sender, EventArgs e)
        {
            SMS_Load();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SMS_Load();
        }
    }
}
