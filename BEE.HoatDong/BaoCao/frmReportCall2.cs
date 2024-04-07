using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;

namespace BEE.HoatDong.BaoCao
{
    public partial class frmReportCall2 : DevExpress.XtraEditors.XtraForm
    {
        public frmReportCall2()
        {
            InitializeComponent();
        }
        public class item
        {
            public int id { get; set; }
            public bool check { get; set; }
            public int val { get; set; }
        }
       

        private void frmReportCall2_Load(object sender, EventArgs e)
        {
            var lst = new List<item>();
            for(int i = 0; i<=10; i++)
            {
                var item = new item();
                item.id = i++;
                item.check = true;
                item.val = i;
                lst.Add(item);
            }
            gridControl1.DataSource = lst;
          //  MasterDataContext db = new MasterDataContext();
         //  gcReportCall.DataSource = db.rpReportCallDetail(DateTime.Now.AddYears(-10), DateTime.Now, ",,", -1, -1, -1);
        }
    }
}