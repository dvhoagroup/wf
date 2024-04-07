using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;
using BEE.ThuVien;
using System.Linq;

namespace BEE.HoatDong.MGL.Setting
{
    public partial class ctlTrangThaiBDSv2 : XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();
        public ctlTrangThaiBDSv2()
        {
            InitializeComponent();

           // BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
        }

        void LoadData()
        {
            try
            {
                db = new MasterDataContext();
                gridControl1.DataSource = db.mglbcTrangThais.OrderBy(p => p.STT).ToList();
            }
            catch
            {

            }
          

        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = BEE.ThuVien.Common.PerID;
            o.AccessData.Form.FormID = 226;
            DataTable tblAction = o.SelectBy();
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            btnThem.Enabled = true;
                            break;
                        case 2:
                            btnSua.Enabled = true;
                            break;
                        case 3:
                            btnXoa.Enabled = true;
                            break;
                    }
                }
            }
        }

       
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnNap_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmTrangThaiBDSv2 frm = new frmTrangThaiBDSv2();
            frm.flagAddnew = true;
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvTrangThaiGD.GetFocusedRowCellValue(colMaHuong) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa trạng thái: <" + grvTrangThaiGD.GetFocusedRowCellValue(colTenHuong).ToString() + "> ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    try
                    {
                        var db = new MasterDataContext();
                        var matt = (byte)grvTrangThaiGD.GetFocusedRowCellValue("MaTT");
                        var obj = db.mglbcTrangThais.FirstOrDefault(p => p.MaTT == matt);
                        db.mglbcTrangThais.DeleteOnSubmit(obj);
                        db.SubmitChanges();

                        //it.NhomKHCls o = new it.NhomKHCls();
                        //o.MaNKH = byte.Parse(grvTrangThaiGD.GetFocusedRowCellValue(colMaHuong).ToString());
                        //o.Delete();
                        
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì trạng thái  <" + grvTrangThaiGD.GetFocusedRowCellValue(colTenHuong).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                        return;
                    }
                    grvTrangThaiGD.DeleteSelectedRows();
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn trạng thái cần xóa. Xin cảm ơn");
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        void Edit()
        {
           
            var matt = (byte)grvTrangThaiGD.GetFocusedRowCellValue("MaTT");
            if (grvTrangThaiGD.GetFocusedRowCellValue(colMaHuong) != null)
            {
                frmTrangThaiBDSv2 frm = new frmTrangThaiBDSv2();
                frm.MaTT = matt;
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn trạng thái cần sửa. Xin cảm ơn");
        }

        private void ctlTrangThaiBDSv2_Load(object sender, EventArgs e)
        {
            
            LoadData();
            LoadPermission();
        }
    }
}
