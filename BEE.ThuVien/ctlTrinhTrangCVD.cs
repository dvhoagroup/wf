using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
namespace LandSoft.Library
{
    class ctlTrinhTrangCVD: LookEdit
        {
        public ctlTrinhTrangCVD()
            {
                this.ButtonClick += new ButtonPressedEventHandler(ctlTrinhTrangCV_ButtonClick);

                this.Properties.DisplayMember = "TenTT";
                this.Properties.ValueMember = "ID";
                this.Properties.ShowLines = true;
            }

        void ctlTrinhTrangCV_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
          //switch (e.Button.Index)
          //      {
          //          case 1:
          //              using (var frm = new Other.frmTrangThiCV(0))
          //              {
          //                  frm.ShowDialog();
          //                  if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
          //                  {
          //                      this.LoadData();
          //                  }
          //              }
          //              break;
          //      }
        }
            public void LoadData()
            {
                this.Properties.Columns.Clear();
                this.Properties.Columns.Add(new LookUpColumnInfo("MaTT", 30, "Mã TT"));
                this.Properties.Columns.Add(new LookUpColumnInfo("TenTT", 70, "Tên TT"));
                this.Properties.ShowHeader = false;
                using (var db = new MasterDataContext())
                {
                    this.Properties.DataSource = null;
                    this.Properties.DataSource = db.cvdTrinhTrangs.ToList();
                }
            }
        
    }
}
