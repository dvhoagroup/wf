using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.QuangCao.Mail
{
    public partial class frmReceiveSelect : DevExpress.XtraEditors.XtraForm
    {
        public List<int> Selection { get; set; }

        MasterDataContext db = new MasterDataContext();

        public frmReceiveSelect()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void frmReceiveSelect_Load(object sender, EventArgs e)
        {
            gcReceive.DataSource = db.mailReceives.AsEnumerable()
                .Select((p, index) => new
                {
                    STT = index + 1,
                    p.ReceID,
                    p.ReceName,
                    p.Description,
                    p.DateModify,
                    p.DateCreate,
                    StaffCreate = p.NhanVien == null ? "" : p.NhanVien.HoTen,
                    StaffModify = p.NhanVien1 == null ? "" : p.NhanVien1.HoTen
                }).ToList();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            var indexs = grvReceive.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn danh sách");
                return;
            }

            Selection = new List<int>();
            foreach (var i in indexs)
                Selection.Add((int)grvReceive.GetRowCellValue(i, "ReceID"));

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}