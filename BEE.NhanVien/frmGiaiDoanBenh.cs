using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.DULIEU;
using BEE.CONGCUINAN;

namespace BEE.KHACHHANG
{
    public partial class frmGiaiDoanBenh : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext db = new MasterDataContext();
        private bool _LoadCompleted = false;

        public frmGiaiDoanBenh()
        {
            InitializeComponent();
            BEE.NGONNGU.Language.TranslateUserControl(this);
        }

        private void frmGaiDoanBenh_Load(object sender, EventArgs e)
        {
            var  cbxLoaiBenh= db.tblLoaiBenhs.OrderBy(a => a.TenLB);
           

            if (cbxLoaiBenh.Items.Count > 0)
            {
                int maLB = int.Parse(cbxLoaiBenh.SelectedValue.ToString());
                var giaiDoanBenhList = db.tblGiaiDoanBenhs.Where(a => a.MaLB == maLB).OrderBy(a => a.TenGDB);
                grdGiaiDoanBenh.DataSource = giaiDoanBenhList;
            }
            else
                grdGiaiDoanBenh.DataSource = db.tblGiaiDoanBenhs.OrderBy(a => a.TenGDB);

            _LoadCompleted = true;
            
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                int executed = 0;
                int maLB = cbxLoaiBenh.Items.Count == 0 || cbxLoaiBenh.SelectedIndex == -1 ? 0 : int.Parse(cbxLoaiBenh.SelectedValue.ToString());
                int total = grvGiaiDoanBenh.RowCount;
                
                for (int i = 0; i < total; i++)
                {
                    BEE.DULIEU.tblGiaiDoanBenh newObject = grvGiaiDoanBenh.GetRow(i) as BEE.DULIEU.tblGiaiDoanBenh;
                    if (newObject == null)
                        continue;

                    newObject.MaLB = maLB;
                    db.SubmitChanges();
                }

            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }
        }

        private void cbxLoaiBenh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!_LoadCompleted)
                    return;

                int maLB = cbxLoaiBenh.Items.Count == 0 || cbxLoaiBenh.SelectedIndex == -1 ? 0 : int.Parse(cbxLoaiBenh.SelectedValue.ToString());
                var giaiDoanBenhList = db.tblGiaiDoanBenhs.Where(a => a.MaLB == maLB).OrderBy(a => a.TenGDB);
                grdGiaiDoanBenh.DataSource = giaiDoanBenhList;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }
    }
}