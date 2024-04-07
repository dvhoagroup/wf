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

namespace BEE.QuangCao.SMS
{
    public partial class frmChoiceGroup : DevExpress.XtraEditors.XtraForm
    {
        public List<int> Selection { get; set; }

        MasterDataContext db = new MasterDataContext();

        public frmChoiceGroup()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void frmChoiceGroup_Load(object sender, EventArgs e)
        {
            gcGroupReceive.DataSource = db.SMSGroupReceives.AsEnumerable()
                .Select((p, index) => new
                {
                    STT = index + 1,
                    p.GroupID,
                    p.GroupName,
                    p.Description,
                    p.DateModify,
                    p.DateCreate,
                    HoTenA = p.NhanVien == null ? "" : p.NhanVien.HoTen,
                    HoTenB = p.NhanVien1 == null ? "" : p.NhanVien1.HoTen
                }).ToList();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            var indexs = gridView1.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn danh sách");
                return;
            }

            Selection = new List<int>();
            foreach (var i in indexs)
                Selection.Add((int)gridView1.GetRowCellValue(i, "GroupID"));

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}