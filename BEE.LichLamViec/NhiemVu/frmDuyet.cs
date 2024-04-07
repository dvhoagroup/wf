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

namespace BEEREMA.CongViec.NhiemVu
{
    public partial class frmDuyet : DevExpress.XtraEditors.XtraForm
    {
        public int MaNVu { get; set; }

        MasterDataContext db = new MasterDataContext();

        public frmDuyet()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        void LoadDictionary()
        {
            it.NhiemVu_TinhTrangCls objStatus = new it.NhiemVu_TinhTrangCls();
            lookUpStatus.Properties.DataSource = objStatus.Select();
            lookUpStatus.ItemIndex = 0;

            //lookTienDo.Properties.DataSource = db.NhiemVu_TienDos;
            //lookTienDo.ItemIndex = 0;
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            it.NhiemVuCls o = new it.NhiemVuCls();
            o.MaNVu = MaNVu;
            o.TinhTrang.MaTT = Convert.ToInt32(lookUpStatus.EditValue);
            //o.TienDo.MaTD = Convert.ToInt16(lookTienDo.EditValue);
            o.PhanTramHT = Convert.ToDecimal(spinHoanThanh.EditValue);
            o.NhanVien.MaNV = BEE.ThuVien.Common.StaffID;
            o.DienGiai = txtDienGiai.Text;

            o.UpdateProcess();
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDuyet_Load(object sender, EventArgs e)
        {
            LoadDictionary();
            if (MaNVu != 0)
            {
                it.NhiemVuCls o = new it.NhiemVuCls(MaNVu);
                //try
                //{
                //    lookTienDo.EditValue = o.TienDo.MaTD;
                //}
                //catch { }
                lookUpStatus.EditValue = o.TinhTrang.MaTT;
                spinHoanThanh.EditValue = o.PhanTramHT;
            }
        }
    }
}