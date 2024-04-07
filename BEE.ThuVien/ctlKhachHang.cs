using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
namespace LandSoft.Library
{
    class ctlKhachHang : LookEdit
        {
        public ctlKhachHang()
            {
                this.ButtonClick += new ButtonPressedEventHandler(ctlLoaiTienEdit_ButtonClick);

                this.Properties.DisplayMember = "TenKH";
                this.Properties.ValueMember = "MaKH";
                this.Properties.ShowLines = true;
            }

            void ctlLoaiTienEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
            {
                //switch (e.Button.Index)
                //{
                //    case 1:
                //        using (var frm = new Other.frmKhachHang())
                //        {
                //            frm.ShowDialog();
                //            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                //            {
                //                this.LoadData();
                //            }
                //        }
                //        break;
                //}
            }

            public void LoadData()
            {
                this.Properties.Columns.Clear();
               this.Properties.Columns.Add(new LookUpColumnInfo("MaKH", 30, "Mã Khách Hàng"));
                this.Properties.Columns.Add(new LookUpColumnInfo("TenKH", 70, "Tên Khách Hàng"));
                this.Properties.ShowHeader = false;
                using (var db = new MasterDataContext())
                {
                    this.Properties.DataSource = null;
                    this.Properties.DataSource = db.KhachHangs.ToList();
                }
            }
        
    }
}
