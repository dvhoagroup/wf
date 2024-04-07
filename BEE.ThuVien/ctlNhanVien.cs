using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
namespace LandSoft.Library
{
    class ctlNhanVien : LookEdit
    {
        public byte? MaPB { get; set; }
        public ctlNhanVien()
        {
            this.ButtonClick += new ButtonPressedEventHandler(ctlLoaiTienEdit_ButtonClick);

            this.Properties.DisplayMember = "HoTen";
            this.Properties.ValueMember = "MaNV";
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
            //        break;
            //}
        }

        public void LoadData()
        {
            this.Properties.Columns.Clear();
            this.Properties.Columns.Add(new LookUpColumnInfo("MaNV", 30, "Mã Nhân Viên"));
            this.Properties.Columns.Add(new LookUpColumnInfo("HoTen", 70, "Tên Nhân Viên"));
            this.Properties.ShowHeader = false;
            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = null;
                if (this.MaPB != null)
                    this.Properties.DataSource = db.NhanViens.Where(p => p.MaPB == this.MaPB).OrderBy(p => p.MaNV).ToList();
                else
                    this.Properties.DataSource = db.NhanViens.OrderBy(p => p.MaNV).ToList();
            }
        }

    }
}
