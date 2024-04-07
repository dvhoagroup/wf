using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.QuangCao.MailV2
{
    public partial class ctlManager : UserControl
    {
        int SDBID = 0;


        public ctlManager()
        {
            InitializeComponent();

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
        }

        void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(BEE.ThuVien.Common.PerID, 197);

            return o.SDB.SDBID;
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            SDBID = GetAccessData();
            SetDate(4);
        }

        void LoadData()
        {
            using (var db = new MasterDataContext())
            {
                DateTime tuNgay = itemTuNgay.EditValue != null ? (DateTime)itemTuNgay.EditValue : DateTime.Now.AddDays(-90);
                DateTime denNgay = itemDenNgay.EditValue != null ? (DateTime)itemDenNgay.EditValue : DateTime.Now;
                switch (SDBID)
                {
                    case 1://Tat ca
                        gcMailWork.DataSource = db.mrkMailWork_select(tuNgay, denNgay, 0, 0, 0);
                        break;
                    case 2://Theo phong ban
                        gcMailWork.DataSource = db.mrkMailWork_select(tuNgay, denNgay, BEE.ThuVien.Common.DepartmentID, 0, 0);
                        break;
                    case 3://Theo nhom
                        gcMailWork.DataSource = db.mrkMailWork_select(tuNgay, denNgay, 0, BEE.ThuVien.Common.GroupID, 0);
                        break;
                    case 4://Theo nhan vien
                        gcMailWork.DataSource = db.mrkMailWork_select(tuNgay, denNgay, 0, 0, BEE.ThuVien.Common.StaffID);
                        break;
                    default://Tat ca
                        gcMailWork.DataSource = null;
                        break;
                }
            }
        }

        private void itmeRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ShowDetail()
        {
            if (gvMailWork.FocusedRowHandle < 0)
            {
                gcMailRec.DataSource = gcMailSend = null;
                return;
            }
            int WorkID = (int)gvMailWork.GetFocusedRowCellValue("ID");
            using (var db=new MasterDataContext())
            {
            switch (xtraTabControl1.SelectedTabPageIndex)
            { 
                case 0:
                    gcMailSend.DataSource = db.mrkListMailSends.Where(p => p.MailWorkID == WorkID).Select
                        (p => new
                        {
                            p.ID,
                            p.MailWorkID,
                            p.Name,
                            p.CountMail,
                            p.DateSend,
                            p.mailConfig1.Email

                        });
                        break;
                case 1:
                        gcMailRec.DataSource = db.mrkListMailRecives.Where(p => p.MailWorkID == WorkID)
                            .Select(p => new
                            {
                                p.ID,
                                p.IsSended,
                                p.MaKH,
                                p.NoiDung,
                                p.MaNV,
                                KhachHang = p.MaKH == null ? "" : (p.KhachHang.IsPersonal == true ? p.KhachHang.HoKH + " " + p.KhachHang.TenKH : p.KhachHang.TenCongTy),
                                p.Email
                            });
                        break;
            }
            }
        }

        private void gvMailWork_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ShowDetail();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            ShowDetail();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmContentEdit())
            {
                frm.ShowDialog();
            }
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvMailWork.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Vui lòng chọn chiến dịch gửi mail để chỉnh sửa!");
                return;
            }
            using (var frm = new frmContentEdit() { ID = (int?)gvMailWork.GetFocusedRowCellValue("ID") })
            {
                frm.ShowDialog();
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvMailWork.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Vui lòng chọn chiến dịch gửi mail để chỉnh sửa!");
                return;
            }
            try
            {
                using (var db = new MasterDataContext())
                {
                    var obj = db.mrkMailSendWorks.FirstOrDefault(p => p.ID == (int?)gvMailWork.GetFocusedRowCellValue("ID"));
                    db.mrkMailSendWorks.DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    DialogBox.Infomation("Dữ liệu đã lưu thành công!");
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }
    }
}
