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
    public partial class ctlStatistic : DevExpress.XtraEditors.XtraUserControl
    {
        SmsConfig objConfig;

        public ctlStatistic()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
        }

        void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        void Message_Load()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                DateTime fromDate = itemTuNgay.EditValue != null ? (DateTime)itemTuNgay.EditValue : DateTime.Now.AddDays(-90);
                DateTime toDate = itemDenNgay.EditValue != null ? (DateTime)itemDenNgay.EditValue : DateTime.Now;
                byte status = (byte)(itemTrangThai.EditValue == "Đang xử lý" ? 1 : itemTrangThai.EditValue == "Gửi thành công" ? 2 : 3);

                var sms = new SMSService.ServiceSoapClient("ServiceSoap");
                gcMessage.DataSource = sms.getListSMS(objConfig.ClientNo, objConfig.ClientPass, fromDate, toDate, status, "");
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                wait.Close();
            }
        }

        private void ctlStatistic_Load(object sender, EventArgs e)
        {
            objConfig = new SmsConfig();
            objConfig.getAccount();

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(0);
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            Message_Load();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            Message_Load();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Message_Load();
        }

        private void itemTrangThai_EditValueChanged(object sender, EventArgs e)
        {
            Message_Load();
        }

        private void itemExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            it.CommonCls.ExportExcel(gcMessage);
        }
    }
}
