using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BEE.ThuVien
{
    public class ctlNhanVienCheckListEdit : CheckLookEdit
    {
        public string []  MAPB;
        public ctlNhanVienCheckListEdit()
        {
            this.Properties.DisplayMember = "HoTen";
            this.Properties.ValueMember = "MaNV";
            this.Properties.SelectAllItemVisible = false;
        }
        public void LoadData()
        {
            for (int i = 0; i < MAPB.Count(); i++) {
                MAPB[i] = MAPB[i].Trim();
            }
                using (var db = new MasterDataContext())
                {
                    this.Properties.DataSource = null;
                    this.Properties.DataSource = this.Properties.DataSource = db.NhanViens.Where(p => MAPB.Contains(p.MaPB.ToString())).ToList();
                }
        }

    }
}
