using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace BEE.CongCu
{
    public partial class Paging : UserControl
    {
        private int currentPage = 1;
        private int pagesCount = 1;
        private int pageRows = 10;
        private int totalRecords = 0;

        public delegate void PageClick(object sender, PageClickEventHandler e);
        public event PageClick PageClicked;

        public delegate void CmbClick(object sender, PageClickEventHandler e);
        public event CmbClick CmbClicked;

        #region Paging
        public Paging()
        {
            InitializeComponent();
        }
        #endregion

        #region TotalRecords
        public int TotalRecords
        {
            get { return totalRecords; }

            set { totalRecords = value; }
        }
        #endregion

        #region CurrentPage
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; }
        }
        #endregion

        #region PageRows
        public int PageRows
        {
            get { return pageRows; }
            set { pageRows = value; }
        }
        #endregion

        #region Paging_Load
        private void Paging_Load(object sender, EventArgs e)
        {
            this.btnFirst.Click += new System.EventHandler(this.ToolStripButtonClick);
            this.btnBackward.Click += new System.EventHandler(this.ToolStripButtonClick);
            this.toolStripButton1.Click += new System.EventHandler(this.ToolStripButtonClick);
            this.toolStripButton2.Click += new System.EventHandler(this.ToolStripButtonClick);
            this.toolStripButton3.Click += new System.EventHandler(this.ToolStripButtonClick);
            this.toolStripButton4.Click += new System.EventHandler(this.ToolStripButtonClick);
            this.toolStripButton5.Click += new System.EventHandler(this.ToolStripButtonClick);
            this.btnForward.Click += new System.EventHandler(this.ToolStripButtonClick);
            this.btnLast.Click += new System.EventHandler(this.ToolStripButtonClick);
            this.cmbPageRows.SelectedIndexChanged += new System.EventHandler(this.cmbPageRows_SelectedIndexChanged);

            cmbPageRows.SelectedIndex = 0;
            lblTongSo.Text = "trong tổng số " + string.Format("{0:#,0.##}", totalRecords) + " bản ghi";
        }
        #endregion

        #region RefreshPagination
        public void RefreshPagination()
        {
            lblTongSo.Text = "trong tổng số " + string.Format("{0:#,0.##}", totalRecords) + " bản ghi";
            pagesCount = Convert.ToInt32(Math.Ceiling(totalRecords * 1.0 / pageRows));

            ToolStripButton[] items = new ToolStripButton[] { toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton4, toolStripButton5 };

            //pageStartIndex contains the first button number of pagination.
            int pageStartIndex = 1;

            if (pagesCount > 5 && currentPage > 2)
                pageStartIndex = currentPage - 2;

            if (pagesCount > 5 && currentPage > pagesCount - 2)
                pageStartIndex = pagesCount - 4;

            for (int i = pageStartIndex; i < pageStartIndex + 5; i++)
            {
                if (i > pagesCount)
                {
                    items[i - pageStartIndex].Visible = false;
                }
                else
                {
                    items[i - pageStartIndex].Visible = true;

                    //Changing the page numbers
                    items[i - pageStartIndex].Text = i.ToString(CultureInfo.InvariantCulture);

                    //Setting the Appearance of the page number buttons
                    if (i == currentPage)
                    {
                        items[i - pageStartIndex].BackColor = Color.Black;
                        items[i - pageStartIndex].ForeColor = Color.White;
                    }
                    else
                    {
                        items[i - pageStartIndex].BackColor = Color.White;
                        items[i - pageStartIndex].ForeColor = Color.Black;
                    }
                }
            }

            //Enabling or Disalbing pagination first, last, previous , next buttons
            if (currentPage == 1)
                btnBackward.Enabled = btnFirst.Enabled = false;
            else
                btnBackward.Enabled = btnFirst.Enabled = true;

            if (currentPage == pagesCount)
                btnForward.Enabled = btnLast.Enabled = false;
            else
                btnForward.Enabled = btnLast.Enabled = true;


            if (totalRecords == 0)
            {
                btnBackward.Enabled = btnFirst.Enabled = false;
                btnForward.Enabled = btnLast.Enabled = false;
            }
        }
        #endregion

        #region ToolStripButtonClick
        //Method that handles the pagination button clicks
        private void ToolStripButtonClick(object sender, EventArgs e)
        {
            try
            {

                ToolStripButton ToolStripButton = ((ToolStripButton)sender);

                //Determining the current page
                if (ToolStripButton == btnBackward)
                    currentPage--;
                else if (ToolStripButton == btnForward)
                    currentPage++;
                else if (ToolStripButton == btnLast)
                    currentPage = pagesCount;
                else if (ToolStripButton == btnFirst)
                    currentPage = 1;
                else
                    currentPage = Convert.ToInt32(ToolStripButton.Text, CultureInfo.InvariantCulture);

                if (currentPage < 1)
                    currentPage = 1;
                else if (currentPage > pagesCount)
                    currentPage = pagesCount;

                //Change the pagiantions buttons according to page number
                RefreshPagination();

                // Delegate the event to the caller
                if (PageClicked != null)
                    PageClicked(this, new PageClickEventHandler(currentPage));


            }
            catch (Exception) { }
        }
        #endregion

        #region cmbPageRows_SelectedIndexChanged
        private void cmbPageRows_SelectedIndexChanged(object sender, EventArgs e)
        {
            pageRows = int.Parse(cmbPageRows.Text.ToString());
            currentPage = 1;
            //Change the pagiantions buttons according to page number
            RefreshPagination();

            // Delegate the event to the caller
            if (CmbClicked != null)
                CmbClicked(this, new PageClickEventHandler(currentPage));
        }
        #endregion

    }

    #region Custom Events
    public class PageClickEventHandler : EventArgs
    {
        private int __Page = 0;

        #region Property Declarations
        public int SelectedPage
        {
            get { return __Page; }
        }
        #endregion

        #region Constructor
        public PageClickEventHandler(int Page)
        {
            __Page = Page;
        }
        #endregion
    }
    #endregion

}
