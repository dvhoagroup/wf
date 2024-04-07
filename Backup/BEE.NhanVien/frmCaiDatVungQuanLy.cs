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
using System.Threading;

namespace BEE.NhanVien
{
    public partial class frmCaiDatVungQuanLy : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public int MaNV { get; set; }

        public frmCaiDatVungQuanLy()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmCaiDatVungQuanLy_Load(object sender, EventArgs e)
        {
            try
            {
                LoadTreeViewRegion();
                SetDataTreeViewRegion();
            }
            catch { }
        }

        #region treeViewAuthority_AfterCheck
        private void treeViewRegion_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    //Thread.Sleep(300);
                    CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }

        }
        #endregion

        #region ckbCheckAll_CheckedChanged
        private void ckbCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (TreeNode node in treeViewRegion.Nodes)
            {
                node.Checked = ckbCheckAll.Checked;
                CheckAllChildNodes(node, ckbCheckAll.Checked);
            }
        }
        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }
        #endregion

        #region LoadTreeViewRegion
        public void LoadTreeViewRegion()
        {
            treeViewRegion.CheckBoxes = true;
            treeViewRegion.Nodes.Clear();

            var dataTinh = db.Tinhs.Where(o => o.MaTinh != 1);
            foreach (var matinh in dataTinh)
            {
                TreeNode tnParent = new TreeNode(matinh.TenTinh);
                tnParent.Tag = matinh.MaTinh;
                treeViewRegion.Nodes.Add(tnParent);

                var dataHuyen = db.Huyens.Where(o => o.MaHuyen != 1 && o.MaTinh == matinh.MaTinh);
                foreach (var mahuyen in dataHuyen)
                {
                    TreeNode tnChild = new TreeNode(mahuyen.TenHuyen);
                    tnChild.Tag = mahuyen.MaHuyen;
                    tnParent.Nodes.Add(tnChild);
                }
            }

        }
        #endregion

        #region SetDataTreeViewRegion
        public void SetDataTreeViewRegion()
        {
            var dataTinh = db.crlHuyenQuanLies.Where(o => o.MaNV == MaNV).AsEnumerable()
                    .Select((p, index) => new { p.MaTinh }).Distinct();

            foreach (var itemTinh in dataTinh)
            {
                var NodeTinh = treeViewRegion.Nodes.OfType<TreeNode>()
                            .FirstOrDefault(node => node.Tag.Equals(itemTinh.MaTinh));
                if (NodeTinh != null)
                {
                    int count = 0;
                    var dataHuyen = db.crlHuyenQuanLies.Where(o => o.MaNV == MaNV && o.MaTinh.ToString() == NodeTinh.Tag.ToString()).AsEnumerable()
                    .Select((p, index) => new { p.MaHuyen }).Distinct();
                    foreach (var itemHuyen in dataHuyen)
                    {
                        var NodeHuyen = NodeTinh.Nodes.OfType<TreeNode>()
                            .FirstOrDefault(node => node.Tag.Equals(itemHuyen.MaHuyen));
                        if (NodeHuyen != null)
                        {
                            count++;
                            NodeHuyen.Checked = true;
                        }
                    }
                    if (count == NodeTinh.Nodes.Count)
                        NodeTinh.Checked = true;
                }
            }

        }
        #endregion

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ckhThuGonVung_CheckedChanged(object sender, EventArgs e)
        {
            if (ckhThuGonVung.Checked)
            {
                treeViewRegion.CollapseAll();
                ckhThuGonVung.Text = "Mở rộng tất cả";
            }
            else
            {
                treeViewRegion.ExpandAll();
                ckhThuGonVung.Text = "Thu gọn tất cả";
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                //xoa du lieu cu
                var dataOld = db.crlHuyenQuanLies.Where(o => o.MaNV == MaNV);
                db.crlHuyenQuanLies.DeleteAllOnSubmit(dataOld);

                var NodeTinh = treeViewRegion.Nodes.OfType<TreeNode>();
                foreach (var itemTinh in NodeTinh)
                {
                    var NodeHuyen = itemTinh.Nodes.OfType<TreeNode>()
                                    .Where(node => node.Checked);
                    foreach (var itemHuyen in NodeHuyen)
                    {
                        crlHuyenQuanLy objHQL = new crlHuyenQuanLy();
                        objHQL.MaNV = MaNV;
                        objHQL.MaTinh = (byte)itemTinh.Tag;
                        objHQL.MaHuyen = (short)itemHuyen.Tag;
                        db.crlHuyenQuanLies.InsertOnSubmit(objHQL);
                    }
                }

                db.SubmitChanges();

                DialogBox.Infomation("Lưu thành công !");
                //this.Close();
            }
            catch (Exception ex)
            { DialogBox.Error(ex.Message); }
        }
    }
}
