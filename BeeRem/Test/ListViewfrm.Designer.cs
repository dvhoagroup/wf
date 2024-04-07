namespace LandSoft.Test
{
    partial class ListViewfrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ComponentOwl.BetterListView.BetterListViewItem betterListViewItem1;
            ComponentOwl.BetterListView.BetterListViewItem betterListViewItem2;
            ComponentOwl.BetterListView.BetterListViewItem betterListViewItem3;
            ComponentOwl.BetterListView.BetterListViewItem betterListViewItem4;
            ComponentOwl.BetterListView.BetterListViewItem betterListViewItem5;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListViewfrm));
            ComponentOwl.BetterListView.BetterListViewSubItem betterListViewSubItem1;
            this.betterListView1 = new ComponentOwl.BetterListView.BetterListView();
            betterListViewItem1 = new ComponentOwl.BetterListView.BetterListViewItem();
            betterListViewItem2 = new ComponentOwl.BetterListView.BetterListViewItem();
            betterListViewItem3 = new ComponentOwl.BetterListView.BetterListViewItem();
            betterListViewItem4 = new ComponentOwl.BetterListView.BetterListViewItem();
            betterListViewItem5 = new ComponentOwl.BetterListView.BetterListViewItem();
            betterListViewSubItem1 = new ComponentOwl.BetterListView.BetterListViewSubItem();
            ((System.ComponentModel.ISupportInitialize)(this.betterListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // betterListViewItem1
            // 
            betterListViewItem1.Image = global::LandSoft.Properties.Resources.LandSoft;
            betterListViewItem1.Name = "betterListViewItem1";
            betterListViewItem1.Text = "betterListViewItem1";
            // 
            // betterListViewItem2
            // 
            betterListViewItem2.Image = global::LandSoft.Properties.Resources.LandSoft;
            betterListViewItem2.Name = "betterListViewItem2";
            betterListViewItem2.Text = "betterListViewItem2";
            // 
            // betterListViewItem3
            // 
            betterListViewItem3.Image = global::LandSoft.Properties.Resources.LandSoft;
            betterListViewItem3.Name = "betterListViewItem3";
            betterListViewItem3.Text = "betterListViewItem3";
            // 
            // betterListViewItem4
            // 
            betterListViewItem4.Image = global::LandSoft.Properties.Resources.LandSoft;
            betterListViewItem4.Name = "betterListViewItem4";
            betterListViewItem4.Text = "betterListViewItem4";
            // 
            // betterListViewItem5
            // 
            betterListViewItem5.Image = global::LandSoft.Properties.Resources.LandSoft;
            betterListViewItem5.Name = "betterListViewItem5";
            betterListViewItem5.SubItems.AddRange(new object[] {
            betterListViewSubItem1});
            betterListViewItem5.Text = "betterListViewItem5";
            // 
            // betterListViewSubItem1
            // 
            betterListViewSubItem1.Name = "betterListViewSubItem1";
            betterListViewSubItem1.Text = "betterListViewSubItem1";
            // 
            // betterListView1
            // 
            this.betterListView1.AllowDrag = true;
            this.betterListView1.AllowDrop = true;
            this.betterListView1.AllowedDragEffects = ((System.Windows.Forms.DragDropEffects)(((System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.Move) 
            | System.Windows.Forms.DragDropEffects.Scroll)));
            this.betterListView1.AutoSizeItemsInDetailsView = true;
            this.betterListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.betterListView1.GridLines = ComponentOwl.BetterListView.BetterListViewGridLines.Grid;
            this.betterListView1.HideSelectionMode = ComponentOwl.BetterListView.BetterListViewHideSelectionMode.Disable;
            this.betterListView1.Items.AddRange(new object[] {
            betterListViewItem1,
            betterListViewItem2,
            betterListViewItem3,
            betterListViewItem4,
            betterListViewItem5});
            this.betterListView1.Location = new System.Drawing.Point(0, 0);
            this.betterListView1.Name = "betterListView1";
            this.betterListView1.SearchSettings = new ComponentOwl.BetterListView.BetterListViewSearchSettings(ComponentOwl.BetterListView.BetterListViewSearchMode.PrefixOrSubstring, ((ComponentOwl.BetterListView.BetterListViewSearchOptions)((((ComponentOwl.BetterListView.BetterListViewSearchOptions.FirstWordOnly | ComponentOwl.BetterListView.BetterListViewSearchOptions.PlaySound) 
                | ComponentOwl.BetterListView.BetterListViewSearchOptions.PrefixPreference) 
                | ComponentOwl.BetterListView.BetterListViewSearchOptions.WordSearch))), new int[] {
            0});
            this.betterListView1.Size = new System.Drawing.Size(630, 347);
            this.betterListView1.TabIndex = 0;
            // 
            // ListViewfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 347);
            this.Controls.Add(this.betterListView1);
            this.Name = "ListViewfrm";
            this.Text = "ListViewfrm";
            ((System.ComponentModel.ISupportInitialize)(this.betterListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}