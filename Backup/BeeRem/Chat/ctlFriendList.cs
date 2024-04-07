using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;

namespace BEEREMA.Chat
{
    public partial class ctlFriendList : DevExpress.XtraEditors.XtraUserControl
    {
        public static List<frmSendMessage> ltMessBox = new List<frmSendMessage>();
        public static List<int> ltOnline;

        MasterDataContext db = new MasterDataContext();

        public ctlFriendList()
        {
            InitializeComponent();
            this.Load += new EventHandler(ctlFriendList_Load);
            grvNV.DoubleClick += new EventHandler(grvNV_DoubleClick);
            timerCheckOnline.Tick += new EventHandler(timerCheckOnline_Tick);
        }

        void ctlFriendList_Load(object sender, EventArgs e)
        {
            try
            {
                gcNV.DataSource = db.NhanVien_getFriendList(Properties.Settings.Default.StaffID);
            }
            catch { }

            timerCheckOnline.Start();
        }

        void timerCheckOnline_Tick(object sender, EventArgs e)
        {
            timerCheckOnline.Stop();
            if (ltOnline != null)
            {
                for (int i = 0; i < grvNV.RowCount; i++)
                {
                    var maNV = (int)grvNV.GetRowCellValue(i, "MaNV");
                    if (ltOnline.IndexOf(maNV) >= 0)
                    {
                        grvNV.SetRowCellValue(i, "MaTT", 2);
                    }
                    else
                    {
                        grvNV.SetRowCellValue(i, "MaTT", 1);
                    }
                }
            }
            timerCheckOnline.Start();
        }

        void grvNV_DoubleClick(object sender, EventArgs e)
        {
            int? maNV = (int?)grvNV.GetFocusedRowCellValue("MaNV");
            if (maNV == null) return;
            if (ltMessBox.Where(p => p.Name == maNV.ToString()).Count() <= 0)
            {
                var frm = new frmSendMessage(maNV.Value, grvNV.GetFocusedRowCellValue("HoTen").ToString(), 
                    (byte)grvNV.GetFocusedRowCellValue("MaTT"));
                frm.Name = maNV.ToString();
                frm.Show();
                ltMessBox.Add(frm);
            }
            else
            {
                var frm = ltMessBox.Single(p => p.Name == maNV.ToString());
                frm.WindowState = FormWindowState.Normal;
                frm.Focus();
            }
        }
    }
}
