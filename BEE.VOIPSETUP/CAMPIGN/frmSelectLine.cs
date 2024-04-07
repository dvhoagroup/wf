using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.DULIEU;
using BEE.THUVIEN;
using System.Linq;
using System.Collections;
using DIPCRM;


namespace BEE.VOIPSETUP.CAMPAIGN
{
    public partial class frmSelectLine : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public List<int> lstNV = new List<int>();
        public frmSelectLine()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmSelectLine_Load(object sender, EventArgs e)
        {
            gcLine.DataSource = db.voipLineConfigs.Select(p => new { p.LineName, p.NhanVien.HoTen, p.MaNV ,p.LineNumber});
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvLine.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn line");
                return;
            }
            var listID = gvLine.GetSelectedRows();
            foreach (var i in listID)
            {
                lstNV.Add((int)gvLine.GetRowCellValue(i,"MaNV"));
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}