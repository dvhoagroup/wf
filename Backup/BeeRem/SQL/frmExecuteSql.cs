using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BEEREMA.SQL
{
    public partial class frmExecuteSql : DevExpress.XtraEditors.XtraForm
    {
        public frmExecuteSql()
        {
            InitializeComponent();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemExecute_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (txtQuery.Text.Trim().Length > 10)
                {
                    if (txtQuery.Text.Trim().ToLower().IndexOf("select") >= 0)
                    {
                        gvResult.Columns.Clear();
                        gcResult.DataSource = null;
                        var result = it.CommonCls.Table(txtQuery.Text.Trim());
                        gcResult.DataSource = result;
                    }
                    else
                        it.CommonCls.sqlCommand(txtQuery.Text.Trim());
                    txtException.Text = "Execute successful.";
                }
                else
                    txtException.Text = "Please type query.";
            }
            catch (Exception ex) { txtException.Text = ex.Message; }
        }

        private void itemPass_EditValueChanged(object sender, EventArgs e)
        {
            if (itemPass.EditValue.ToString() == "songthanasia")
                txtQuery.Properties.ReadOnly = false;
            else
                txtQuery.Properties.ReadOnly = true;
        }
    }
}
