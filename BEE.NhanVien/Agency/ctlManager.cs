using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using System.Linq;
using BEEREMA;

namespace BEE.NhanVien.Agency
{
    public partial class ctlManager : XtraUserControl
    {
        MasterDataContext db;
        Int64 _MaNV;
        public ctlManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        void LoadData()
        {
            try
            {
                it.Agency.NhanVienCls o = new it.Agency.NhanVienCls();
                gcStaff.DataSource = o.Select();
            }
            catch
            {
                DialogBox.Infomation("Lỗi");
            }
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 15;
            DataTable tblAction = o.SelectBy();
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnKhoa.Enabled = false;
            btnMoKhoa.Enabled = false;
            btnResetPassword.Enabled = false;
            itemCaiDatQL.Enabled = false;
            itemGhiNho.Enabled = false;

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
                        case 11:
                            btnKhoa.Enabled = true;
                            break;
                        case 12:
                            btnMoKhoa.Enabled = true;
                            break;
                        case 42:
                            btnResetPassword.Enabled = true;
                            break;
                        case 84:
                            itemCaiDatQL.Enabled = true;
                            break;
                        case 91:
                            itemGhiNho.Enabled = true;
                            break;
                    }
                }
            }
        }

        void LoadHis(int maNV)
        {
            gcHis.DataSource = db.NhanVien_LichSus.AsEnumerable().Where(o => o.RefID == maNV).Select((o, index) => new
            {
                STT = index + 1,
                NgayTao = o.NgayTao,
                RefID = o.NhanVien.HoTen,
                MaNV = o.NhanVien1.HoTen,
                Comment = o.GhiChu
            }).ToList();
        }

        private void Huong_ctl_Load(object sender, EventArgs e)
        {
            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);

            it.NhanVienCls o = new it.NhanVienCls();
            lookUpNhomKD.DataSource = o.NKD.Select();
            lookUpPhongBan.DataSource = o.PhongBan.Select();

            slookHuyen.DataSource = db.Huyens.Select(p => new { p.MaHuyen, KhuVuc = p.Tinh.TenTinh + " - " + p.TenHuyen });

            LoadData();
            LoadPermission();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (btnSua.Enabled)
                Edit();
        }

        private void btnNap_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmEdit frm = new frmEdit();
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvStaff.GetFocusedRowCellValue(colMaNV) != null)
            {
                if (int.Parse(gvStaff.GetFocusedRowCellValue(colMaNV).ToString()) == Common.StaffID)
                {
                    DialogBox.Infomation("Bạn không thể [Xóa] chình mình. Vui lòng kiểm tra lại, xin cảm ơn.");
                    return;
                }

                if (DialogBox.Question("Bạn có chắc chắn muốn xóa [Nhân viên]: <" + gvStaff.GetFocusedRowCellValue(colTenNV).ToString() + "> ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    try
                    {
                        it.Agency.NhanVienCls o = new it.Agency.NhanVienCls();
                        o.MaNV = Int64.Parse(gvStaff.GetFocusedRowCellValue(colMaNV).ToString());
                        o.Delete();
                        gvStaff.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì [Nhân viên]: <" + gvStaff.GetFocusedRowCellValue(colTenNV).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn [Nhân viên] cần xóa. Xin cảm ơn");
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
            if (gvStaff.GetFocusedRowCellValue(colMaNV) != null)
            {
                frmEdit frm = new frmEdit();
                frm.MaNV = int.Parse(gvStaff.GetFocusedRowCellValue(colMaNV).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn [Nhân viên] cần sửa. Xin cảm ơn");
        }

        private void btnKhoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvStaff.GetFocusedRowCellValue(colMaNV) != null)
            {
                if (int.Parse(gvStaff.GetFocusedRowCellValue(colMaNV).ToString()) == Common.StaffID)
                {
                    DialogBox.Infomation("Bạn không thể [Khóa] chình mình. Vui lòng kiểm tra lại, xin cảm ơn.");
                    return;
                }

                if (DialogBox.Question("Chọn Yes để mở form Xác nhận khóa [Cộng tác viên]?") == DialogResult.Yes)
                {
                    _MaNV = (Int64)gvStaff.GetFocusedRowCellValue(colMaNV);
                    BEE.NhanVien.Agency.frmProcess frm = new BEE.NhanVien.Agency.frmProcess();
                    frm.MaNV = _MaNV;
                    frm.Ktra = 0;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        LoadData();
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn [Nhân viên] cần khóa. Xin cảm ơn");
        }

        private void btnMoKhoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvStaff.GetFocusedRowCellValue(colMaNV) != null)
            {
                if (int.Parse(gvStaff.GetFocusedRowCellValue(colMaNV).ToString()) == Common.StaffID)
                {
                    DialogBox.Infomation("Bạn không thể [Mở khóa] chình mình. Vui lòng kiểm tra lại, xin cảm ơn.");
                    return;
                }

                if (DialogBox.Question("Chọn Yes để mở form Xác nhận mở khóa [Nhân viên]?") == DialogResult.Yes)
                {
                    _MaNV = (Int64)gvStaff.GetFocusedRowCellValue(colMaNV);
                    var frm = new BEE.NhanVien.Agency.frmProcess();
                    frm.MaNV = _MaNV;
                    frm.Ktra = 1;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        LoadData();
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn [Nhân viên] cần mở khóa. Xin cảm ơn");
        }

        private void btnResetPassword_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvStaff.GetFocusedRowCellValue(colMaNV) != null)
            {
                var manv = Convert.ToByte(gvStaff.GetFocusedRowCellValue(colMaNV));
                BEE.NhanVien.Agency.frmResetPass frm = new BEE.NhanVien.Agency.frmResetPass();
                frm.KeyID = manv;
                frm.ShowDialog();
            }
            else
                DialogBox.Infomation("Vui lòng chọn [Cộng tác viên] cần mở khóa. Xin cảm ơn");
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmImport frm = new frmImport();
            frm.ShowDialog();
            LoadData();
        }

        private void gvStaff_CalcPreviewText(object sender, DevExpress.XtraGrid.Views.Grid.CalcPreviewTextEventArgs e)
        {
            try
            {
                e.PreviewText = gvStaff.GetRowCellValue(e.RowHandle, "Description").ToString();
            }
            catch { }
        }

        private void gvStaff_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        public void LoadDetail()
        {
            if (gvStaff.FocusedRowHandle < 0)
            {
                gcHis.DataSource = null;
                gcKhuVuc.DataSource = null;
                return;
            }
            _MaNV = (int)gvStaff.GetFocusedRowCellValue(colMaNV);
            switch (xtraTabControl2.SelectedTabPageIndex)
            {
                case 0:
                    //LoadHis(_MaNV);
                    break;
                case 1:
                    gcKhuVuc.DataSource = db.crlHuyenQuanLies.Where(p => p.MaNV == _MaNV).ToList();
                    break;
            }
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadDetail();
        }

        private void itemCaiDatQL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (gvStaff.FocusedRowHandle < 0)
            //{
            //    DialogBox.Infomation("Vui lòng chọn nhân viên để tiến hành cài đặt!");
            //    return;
            //}
            //using (var frm = new frmCaiDatVungQuanLy() { MaNV = _MaNV })
            //{
            //    frm.ShowDialog();
            //}
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemGhiNho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmRememberPass())
            {
                frm.KeyID = Convert.ToByte(gvStaff.GetFocusedRowCellValue(colMaNV));
                frm.ShowDialog();
            }
        }
    }
}
