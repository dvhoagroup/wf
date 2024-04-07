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
using BEE.HoatDong.MGL.Ban;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.VOIPSETUP.Record
{
    public partial class frmRecordFTP : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public int? id { get; set; }
        public string didong { get; set; }
        public frmRecordFTP()
        {
            InitializeComponent();
        }

        private void btnPlay_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            var url = gridView1.GetFocusedRowCellValue("Source");
            if (url != null && url != "")
            {
                var frm = new frmViewMedia();
                frm.filepath = url.ToString();
                frm.ShowDialog();

            }

        }

        private void frmRecordFTP_Load(object sender, EventArgs e)
        {
            if(didong != "" && didong != null)
            {
                gcFile.DataSource = VOIPSETUP.Utils.Common.GetALLFile(id);
            }
            else
            {
                gcFile.DataSource = VOIPSETUP.Utils.Common.GetALLFileDiDongNull(id);
            }
            
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var url = gridView1.GetFocusedRowCellValue("Source");
            if (url == null || url == "")
            {
                DialogBox.Error("Vui lòng chọn bản ghi để đồng bộ file");
                return;
            }

            try
            {
                var obj = db.mglbcNhatKyXuLies.FirstOrDefault(p => p.ID == id);
                obj.FileRecord = url.ToString();
                db.SubmitChanges();
                DialogBox.Infomation("Đồng bộ thành công");
                this.Close();
            }
            catch (Exception)
            {
                DialogBox.Error("Lỗi, vui lòng thử lại");
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();

        }
    }
}