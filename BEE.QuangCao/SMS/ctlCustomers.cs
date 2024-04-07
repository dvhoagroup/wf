using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BEE.QuangCao.SMS
{
    public partial class ctlCustomers : DevExpress.XtraEditors.XtraUserControl
    {
        public ctlCustomers()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this);
        }
    }
}
