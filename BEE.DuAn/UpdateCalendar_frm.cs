using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;
using BEE.ThuVien;

namespace BEE.DuAn
{
    public partial class UpdateCalendar_frm : DevExpress.XtraEditors.XtraForm
    {
        public bool IsUpdate = false;
        public int MaDA = 0;
        public byte DotTT = 0;
        public string DienGiai = "", OldFileName = "";
        public DateTime NgayHT = DateTime.Now;
        public UpdateCalendar_frm()
        {
            InitializeComponent();
        }

        private void btnBoQua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnLuuDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dateNgayCN.Text == "")
            {
                DialogBox.Infomation("Vui òng nhập ngày hoàn thành tiến độ dự án. Xin cảm ơn.");
                dateNgayCN.Focus();
                return;
            }
            it.DuAn_TienDoCls o = new it.DuAn_TienDoCls();
            o.MaDA = MaDA;
            o.MaNV = Common.StaffID;
            o.NgayCN = DateTime.Now;
            o.NgayHT = dateNgayCN.DateTime;
            o.DienGiai = txtDienGiai.Text;
            o.DotTT = DotTT;
            o.FileAttach = btnFileAttach.Text;
            try
            {
                o.Insert();

                //Cap nhat lich thanh toan
                it.pgcPhieuGiuChoCls objPGC = new it.pgcPhieuGiuChoCls();
                DataTable tblPGC = objPGC.LichTTByProject(MaDA);
                it.pgcLichThanhToanCls objLich;
                foreach (DataRow r in tblPGC.Rows)
                {
                    objLich = new it.pgcLichThanhToanCls();
                    objLich.NgayTT = dateNgayCN.DateTime;
                    objLich.DotTT = DotTT;
                    objLich.PGC.MaPGC = int.Parse(r["MaPGC"].ToString());
                    objLich.DienGiai = txtDienGiai.Text;
                    objLich.UpdateNgayTT();
                }

                it.hdmbLichThanhToanCls obj = new it.hdmbLichThanhToanCls();
                tblPGC = obj.LichTTByProject(MaDA);
                foreach (DataRow r in tblPGC.Rows)
                {
                    obj = new it.hdmbLichThanhToanCls();
                    obj.NgayTT = dateNgayCN.DateTime;
                    obj.DotTT = DotTT;
                    obj.HDMB.MaHDMB = int.Parse(r["MaHDMB"].ToString());
                    obj.DienGiai = txtDienGiai.Text;
                    obj.UpdateNgayTT();
                }

                if (OldFileName != btnFileAttach.Text.Trim())
                {
                    //Xoa file cu
                    it.FTPCls objFTP = new it.FTPCls();
                    objFTP.Delete(OldFileName);
                }

                IsUpdate = true;
                DialogBox.Infomation("Đã lập lịch thanh toán thành công.");
                this.Close();
            }
            catch (Exception ex){
                DialogBox.Infomation(ex.Message);
            }
        }

        void LoadTienDo()
        {
            it.DuAn_TienDoCls o = new it.DuAn_TienDoCls();
            o.MaDA = MaDA;
            o.DotTT = DotTT;
            gridControl1.DataSource = o.SelectBy();
        }

        private void UpdateCalendar_frm_Load(object sender, EventArgs e)
        {
            dateNgayCN.DateTime = NgayHT;
            txtDienGiai.Text = DienGiai;
            LoadTienDo();
        }

        private void btnFileAttach_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //OldFileName = btnFileAttach.Text;
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Title = "Chọn file";
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    BEE.NghiepVuKhac.InsertImage_frm frm = new BEE.NghiepVuKhac.InsertImage_frm();
            //    frm.FileName = ofd.FileName;
            //    frm.Directory = "httpdocs/upload/calendar";
            //    frm.ShowDialog();
            //    btnFileAttach.Text = frm.FileName;
            //}
        }

        private void hplDownload_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                if (gridView1.GetFocusedRowCellValue(colFileAttach).ToString() != "")
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.Description = "Chọn thư mục để lưu file tải về";
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        it.FTPCls objFTP = new it.FTPCls();
                        objFTP.GetAccountFTP();
                        objFTP.Download(fbd.SelectedPath, gridView1.GetFocusedRowCellValue(colFileAttach).ToString());
                    }
                }
                else
                    DialogBox.Infomation("Dự án này không có file đính kèm nên không thể tải về.");
            }
        }
    }
}