using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEEREMA;
using BEE.ThuVien;
namespace BEE.HoatDong.MGL.Ban
{
    public partial class frmOther : DevExpress.XtraEditors.XtraForm
    {
        public int? MaMT { get; set; }
        public string NoiDung { get; set; }
        public bool? isSave { get; set; }
        public frmOther()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
         
            var wait = DialogBox.WaitingForm();
            try
            {
                using (var db = new MasterDataContext())
                {
                    this.NoiDung = txtNoiDung.Text;
                    this.isSave = true;
                    DialogBox.Infomation("Dữ liệu đã được lưu!");
                    this.Close();
                }
                
            }
            catch (Exception ex)
            {
                DialogBox.Infomation("Lưu bị lỗi: " + ex.Message);
            }
            finally
            {
                wait.Close();
            }
        }

        private void frmXuLy_Load(object sender, EventArgs e)
        {
            dateNgayXL.EditValue = DateTime.Now;

        }
    }
}