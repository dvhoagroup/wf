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

namespace BEEREMA.Project.Promotion
{
    public partial class ctlPromotion : DevExpress.XtraEditors.XtraUserControl
    {
        public int? ProjectID { get; set; }
        public ctlPromotion()
        {
            InitializeComponent();

            LoadPermission();
        }

        void LoadPermission()
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    var ltAction = db.ActionDatas.Where(p => p.PerID == BEE.ThuVien.Common.PerID & p.FormID == 148).Select(p => p.FeatureID).ToList();
                    itemAdd.Enabled = ltAction.Contains(1);
                    itemEdit.Enabled = ltAction.Contains(2);
                    itemDelete.Enabled = ltAction.Contains(3);

                    BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
                }
            }
            catch { }
        }

        public void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                using (var db = new MasterDataContext())
                {
                    gcPromotion.DataSource = db.daKhuyenMais.Where(p => p.MaDA == ProjectID)
                        .Select(q => new
                        {
                            q.ID,
                            q.MaDA,
                            q.TenKhuyenMai,
                            q.TenQuaTang,
                            q.TuNgay,
                            q.DenNgay,
                            q.TyLe,
                            q.GiaTri,
                            q.DienGiai,
                            q.NhanVien.HoTen,
                            HoTen1 = q.NhanVien1.HoTen,
                            q.NgayTao,
                            q.NgayCN
                        }).ToList();
                }
            }
            catch
            {
                gcPromotion.DataSource = null;
            }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        public void ClearData()
        {
            gcPromotion.DataSource = null;
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ProjectID == null)
            {
                DialogBox.Infomation("Vui lòng chọn [Dự án] để thực hiện chức năng này. Xin cảm ơn!\r\n\r\nPlease select [Project]. Thanks!");
                return;
            }

            var frm = new BEE.DuAn.Promotion.frmEdit();
            frm.MaDA = ProjectID;
            frm.ShowDialog();
            if (frm.IsSave)
                LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvPromotion.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Vui lòng chọn [Khuyến mãi] để thực hiện chức năng này. Xin cảm ơn!\r\n\r\nPlease select [Promotion]. Thanks!");
                return;
            }

            var frm = new BEE.DuAn.Promotion.frmEdit();
            frm.MaCD = (int?)gvPromotion.GetFocusedRowCellValue("ID");
            frm.ShowDialog();
            if (frm.IsSave)
                LoadData();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvPromotion.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Vui lòng chọn [Khuyến mãi] để thực hiện chức năng này. Xin cảm ơn!\r\n\r\nPlease select [Promotion]. Thanks!");
                return;
            }

            if (DialogBox.Question() == DialogResult.No) return;

            using (var db = new MasterDataContext())
            {
                try
                {
                    var obj = db.daKhuyenMais.SingleOrDefault(p => p.ID == (int?)gvPromotion.GetFocusedRowCellValue("ID"));
                    db.daKhuyenMais.DeleteOnSubmit(obj);
                    db.SubmitChanges();

                    gvPromotion.DeleteSelectedRows();
                }
                catch (Exception ex)
                {
                    DialogBox.Error("Đã có lỗi xảy ra. Code: " + ex.Message);
                }
            }
        }
    }
}
