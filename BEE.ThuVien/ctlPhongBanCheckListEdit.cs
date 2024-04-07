using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
namespace LandSoft.Library
{
    class ctlPhongBanCheckListEdit:   CheckLookEdit
    {

        public ctlPhongBanCheckListEdit()
        {
            this.Properties.DisplayMember = "TenPB";
            this.Properties.ValueMember = "MaPB";
        }
        public void LoadData()
        {
            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = null;
                this.Properties.DataSource = db.PhongBans.ToList();
            }
        }

    }
}


