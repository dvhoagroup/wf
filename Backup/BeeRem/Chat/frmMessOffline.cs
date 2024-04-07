using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;

namespace BEEREMA.Chat
{
    public partial class frmMessOffline : DevExpress.XtraEditors.XtraForm
    {
        public int MessCount { get; set; }

        MasterDataContext db = new MasterDataContext();

        public frmMessOffline()
        {
            InitializeComponent();
            
            var ltMess = db.chatTinNhan_SelectOffline(Properties.Settings.Default.StaffID).ToList();
            gcMess.DataSource = ltMess;
            grvMess.ExpandAllGroups();
            this.MessCount = ltMess.Count;
        }

        private void grvMess_DoubleClick(object sender, EventArgs e)
        {
            int? maNV = (int?)grvMess.GetFocusedRowCellValue("MaNV");
            if (maNV == null) return;
            if (ctlFriendList.ltMessBox.Where(p => p.Name == maNV.ToString()).Count() <= 0)
            {
                var frm = new frmSendMessage(maNV.Value, grvMess.GetFocusedRowCellValue("HoTen").ToString(),
                    (byte)grvMess.GetFocusedRowCellValue("MaTT"));
                frm.Name = maNV.ToString();
                frm.Show();
                ctlFriendList.ltMessBox.Add(frm);
            }
            else
            {
                var frm = ctlFriendList.ltMessBox.Single(p => p.Name == maNV.ToString());
                frm.WindowState = FormWindowState.Normal;
                frm.Focus();
            }
        }
    }
}