using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
namespace LandSoft.Library
{
    class ctlPhongBan : LookEdit
    {
        public ctlPhongBan()
        {
            this.ButtonClick += new ButtonPressedEventHandler(ctlLoaiTienEdit_ButtonClick);
            this.Properties.DisplayMember = "TenPB";
            this.Properties.ValueMember = "MaPB";
            this.Properties.ShowLines = true;
        }

        void ctlLoaiTienEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //switch (e.Button.Index)
            //{
            //    case 1:
            //        using (var frm = new Other.frmNhanVien())
            //        {
            //            frm.ShowDialog();
            //            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            //            {
            //                this.LoadData();
            //            }
            //        }
            //     break;
            //}
        }
        public void LoadData()
        {
            this.Properties.Columns.Clear();
            this.Properties.Columns.Add(new LookUpColumnInfo("MaPB", 30, "Mã PB"));
            this.Properties.Columns.Add(new LookUpColumnInfo("TenPB", 70, "Tên Phòng Ban"));
            this.Properties.ShowHeader = false;
            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = null;
                this.Properties.DataSource = db.PhongBans.ToList();
            }
        }

    }
}

