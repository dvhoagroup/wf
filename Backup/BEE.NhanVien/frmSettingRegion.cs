using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using System.Linq;
using BEEREMA;

namespace BEE.NhanVien
{

    public partial class frmSettingRegion : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public int MaNV { get; set; }
        byte? MaTinh { get; set; }
        ThuVien.NhanVien objNV { get; set; }

        public frmSettingRegion()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmSettingRegion_Load(object sender, EventArgs e)
        {
            try
            {
                lookTinh.DataSource = db.Tinhs;
                objNV = db.NhanViens.FirstOrDefault(p => p.MaNV == MaNV);
                //if (objNV.crlHuyenQuanLies.Count() > 0)
                //    itemTinh.EditValue = objNV.crlHuyenQuanLies.First().Huyen.MaTinh;
                //else
                if (itemTinh.EditValue != null)
                    gcQuanHuyen.DataSource = objNV.crlHuyenQuanLies.Where(p => p.MaTinh == MaTinh);
            }
            catch { }
        }

        private void itemTinh_EditValueChanged(object sender, EventArgs e)
        {
            if (itemTinh.EditValue == null)
                return;
            MaTinh = Convert.ToByte(itemTinh.EditValue);
            lookHuyen.DataSource = db.Huyens.Where(p => p.MaTinh == MaTinh);
            //if (objNV.crlHuyenQuanLies.Where(p => p.MaTinh == MaTinh).Count() > 0)
            //gcQuanHuyen.DataSource = objNV.crlHuyenQuanLies.Where(p => p.MaTinh == MaTinh).ToList();
            //else
            gcQuanHuyen.DataSource = db.crlHuyenQuanLies.Where(p => p.MaTinh == MaTinh & p.MaNV == MaNV);
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvQuanHuyen.FocusedRowHandle < 0)
                return;
            gvQuanHuyen.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
                DialogBox.Infomation("Dữ liệu đã được lưu!");
            }
            catch (Exception ex)
            {
                DialogBox.Error("Lỗi không thể lưu:" + ex.Message);
            }
        }

        private void lookHuyen_EditValueChanged(object sender, EventArgs e)
        {
            gvQuanHuyen.SetFocusedRowCellValue("MaTinh", itemTinh.EditValue);
            gvQuanHuyen.SetFocusedRowCellValue("MaNV", MaNV);
        }
    }
}