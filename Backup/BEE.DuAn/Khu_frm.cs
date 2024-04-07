using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.DuAn
{
    public partial class Khu_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaDA = 0, MaKhu = 0, MaPK = 0, BlockID = 0;
        public Khu_frm()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            it.KhuCls o = new it.KhuCls();
            o.MaDA = MaDA;
            gridControlKhu.DataSource = o.SelectByMaDA();
        }

        void LoadPhanKhu()
        {
            it.PhanKhuCls o = new it.PhanKhuCls();
            o.Khu.MaKhu = MaKhu;
            gridControlPhanKhu.DataSource = o.SelectByKhu();

            if (gridPhanKhu.GetFocusedRowCellValue(colMaPK) != null)
                MaPK = int.Parse(gridPhanKhu.GetFocusedRowCellValue(colMaPK).ToString());
            else
                MaPK = 0;
            LoadBlock();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddKhu_frm frm = new AddKhu_frm();
            frm.MaDA = MaDA;
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        void Edit()
        {
            if (gridKhu.GetFocusedRowCellValue(colMaKhu) != null)
            {
                AddKhu_frm frm = new AddKhu_frm();
                frm.MaDA = MaDA;
                frm.MaKhu = int.Parse(gridKhu.GetFocusedRowCellValue(colMaKhu).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn dự án để thực hiện chức năng này. Xin cảm ơn.");
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridKhu.GetFocusedRowCellValue(colMaKhu) != null)
            {
                it.KhuCls o = new it.KhuCls();
                o.MaKhu =  int.Parse(gridKhu.GetFocusedRowCellValue(colMaKhu).ToString());
                try
                {
                    o.Delete();
                    gridKhu.DeleteSelectedRows();
                }
                catch
                {
                    DialogBox.Infomation("Xóa không thành công vì: Khu này đã được sử dụng. Vui lòng kiểm tra lại, xin cảm ơn.");
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn dự án để thực hiện chức năng này. Xin cảm ơn.");
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridKhu.GetFocusedRowCellValue(colMaKhu) != null)
            {
                MaKhu = int.Parse(gridKhu.GetFocusedRowCellValue(colMaKhu).ToString());
                LoadPhanKhu();
            }
            else
                gridControlPhanKhu.DataSource = null;
        }

        private void btmThemPK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridKhu.GetFocusedRowCellValue(colMaKhu) != null)
            {
                AddPhanKhu_frm frm = new AddPhanKhu_frm();
                frm.MaKhu = int.Parse(gridKhu.GetFocusedRowCellValue(colMaKhu).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadPhanKhu();
            }
            else
                DialogBox.Infomation("Vui lòng chọn Khu muốn phân khu. Xin cảm ơn.");
        }

        void EditPhanKhu()
        {
            if (gridPhanKhu.GetFocusedRowCellValue(colMaPK) != null)
            {
                AddPhanKhu_frm frm = new AddPhanKhu_frm();
                frm.MaKhu = int.Parse(gridKhu.GetFocusedRowCellValue(colMaKhu).ToString());
                frm.MaPK = int.Parse(gridPhanKhu.GetFocusedRowCellValue(colMaPK).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadPhanKhu();
            }
            else
                DialogBox.Infomation("Vui lòng chọn phân khu muốn sửa. Xin cảm ơn.");
        }

        private void btnSuaPK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditPhanKhu();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            EditPhanKhu();
        }

        private void btnXoaPK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridPhanKhu.GetFocusedRowCellValue(colMaPK) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa phân khu này không?") == DialogResult.Yes)
                {
                    it.PhanKhuCls o = new it.PhanKhuCls();
                    o.MaPK = int.Parse(gridPhanKhu.GetFocusedRowCellValue(colMaPK).ToString());
                    try
                    {
                        o.Delete();
                        gridPhanKhu.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì: Phân khu này đã được sử dụng. Vui lòng kiểm tra lại, xin cảm ơn.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn dự án để thực hiện chức năng này. Xin cảm ơn.");
        }

        private void btnNapPK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadPhanKhu();
        }

        private void Khu_frm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadBlock()
        {
            it.BlocksCls o = new it.BlocksCls();
            o.PhanKhu.MaPK = MaPK;
            gridControlBlock.DataSource = o.SelectByPK2();

            if (gridBlocks.GetFocusedRowCellValue(colBlockID) != null)
                BlockID = int.Parse(gridBlocks.GetFocusedRowCellValue(colBlockID).ToString());
            else
                BlockID = 0;
            LoadLo();
        }

        void LoadLo()
        {
            it.LoCls o = new it.LoCls();
            o.Blocks.BlockID = BlockID;
            gridControlLo.DataSource = o.SelectByBlock();
        }

        private void btnThemBlock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridPhanKhu.GetFocusedRowCellValue(colMaPK) != null)
            {
                Blocks_frm frm = new Blocks_frm();
                frm.MaPK = int.Parse(gridPhanKhu.GetFocusedRowCellValue(colMaPK).ToString());
                frm.MaDA = MaDA;
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadBlock();
            }
            else
                DialogBox.Infomation("Vui lòng chọn phân khu muốn thêm block. Xin cảm ơn.");
        }

        private void btnXoaBlock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridBlocks.GetFocusedRowCellValue(colBlockID) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa Block này không?") == DialogResult.Yes)
                {
                    it.BlocksCls o = new it.BlocksCls();
                    o.BlockID = int.Parse(gridBlocks.GetFocusedRowCellValue(colBlockID).ToString());
                    try
                    {
                        o.Delete();
                        gridBlocks.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì: Block này đã được sử dụng. Vui lòng kiểm tra lại, xin cảm ơn.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn phân khu muốn thêm block. Xin cảm ơn.");
        }

        void EditBlock()
        {
            if (gridBlocks.GetFocusedRowCellValue(colBlockID) != null)
            {
                Blocks_frm frm = new Blocks_frm();
                frm.KeyID = int.Parse(gridBlocks.GetFocusedRowCellValue(colBlockID).ToString());
                frm.MaPK = int.Parse(gridPhanKhu.GetFocusedRowCellValue(colMaPK).ToString());
                frm.MaDA = MaDA;
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadBlock();
            }
            else
                DialogBox.Infomation("Vui lòng chọn phân khu muốn thêm block. Xin cảm ơn.");
        }

        private void btnSuaBlock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditBlock();
        }

        private void btnNapBlock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadBlock();
        }

        private void btnThemLo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridBlocks.GetFocusedRowCellValue(colBlockID) != null)
            {
                AddLo_frm frm = new AddLo_frm();
                frm.BlockID = int.Parse(gridBlocks.GetFocusedRowCellValue(colBlockID).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadLo();
            }
            else
                DialogBox.Infomation("Vui lòng chọn block muốn thêm lô. Xin cảm ơn.");
        }

        private void btnXoaLo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridLo.GetFocusedRowCellValue(colMaLo) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa Lô này ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    it.LoCls o = new it.LoCls();
                    o.MaLo = int.Parse(gridLo.GetFocusedRowCellValue(colMaLo).ToString());
                    try
                    {
                        o.Delete();
                        gridLo.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì: Lô này đã được sử dụng. Vui lòng kiểm tra lại, xin cảm ơn.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn Block muốn thêm lô. Xin cảm ơn.");
        }

        private void btnSuaLo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridBlocks.GetFocusedRowCellValue(colBlockID) != null)
            {
                AddLo_frm frm = new AddLo_frm();
                frm.BlockID = int.Parse(gridBlocks.GetFocusedRowCellValue(colBlockID).ToString());
                frm.MaLo = int.Parse(gridLo.GetFocusedRowCellValue(colMaLo).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadLo();
            }
            else
                DialogBox.Infomation("Vui lòng chọn Block muốn thêm lô. Xin cảm ơn.");
        }

        private void btnNapLo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadLo();
        }

        private void btnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gridBlocks_DoubleClick(object sender, EventArgs e)
        {
            EditBlock();
        }

        private void gridPhanKhu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridPhanKhu.GetFocusedRowCellValue(colMaPK) != null)
                MaPK = int.Parse(gridPhanKhu.GetFocusedRowCellValue(colMaPK).ToString());
            else
                MaPK = 0;
            LoadBlock();
        }

        private void gridBlocks_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridBlocks.GetFocusedRowCellValue(colBlockID) != null)
                BlockID = int.Parse(gridBlocks.GetFocusedRowCellValue(colBlockID).ToString());
            else
                BlockID = 0;
            LoadLo();
        }

        private void btnThemNhieuLo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridBlocks.GetFocusedRowCellValue(colBlockID) != null)
            {
                AddMultiLo_frm frm = new AddMultiLo_frm();
                frm.BlockID = int.Parse(gridBlocks.GetFocusedRowCellValue(colBlockID).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadLo();
            }
            else
                DialogBox.Infomation("Vui lòng chọn block muốn thêm lô. Xin cảm ơn.");
        }
    }
}