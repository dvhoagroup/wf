﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace BEE.HoatDong.MGL.SDK.PhotoViewer
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public int? MaBC { get; set; }
        FormState formState = new FormState();
        public Form1()
        {
            InitializeComponent();
            photoEditor1.HideAndShowThumbnail_Changed += PhotoEditor1_HideAndShowThumbnail_Changed;
            photoEditor1.PopupMenuPhotoViewer_Clicked += PhotoEditor1_PopupMenuPhotoViewer_Clicked;
          
        }

        private void PhotoEditor1_PopupMenuPhotoViewer_Clicked(object sender, PopupMenuPhotoViewerClickedEventArgs e)
        {
            if(e.ButtonNameClicked.Name == "btn_close")
            {
                this.Close();
            }else if (e.ButtonNameClicked.Name == "btn_mini")
            {
                this.WindowState = FormWindowState.Minimized;
            }
            else if (e.ButtonNameClicked.Name == "btn_them")
            {
              
            }
            else if (e.ButtonNameClicked.Name == "btn_xoa")
            {
               
            }
        }

        private void PhotoEditor1_HideAndShowThumbnail_Changed(object sender, PhotoViewerHideAndShowThumbnailEventArgs e)
        {
            if (e.Status)
            {
                formState.Maximize(this);
            }
            else
            {
                formState.Restore(this);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            photoEditor1.MaBC = this.MaBC;
            photoEditor1.URL_IMAGE = @"images";
          
           // formState.Maximize(this);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.Controls.Clear();
                System.IO.DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "\\Images");
                foreach (var i in di.GetFiles())
                {
                    var img = Image.FromFile(i.FullName);
                    img.Dispose();

                }
                di.Delete(true);
            }
            catch(Exception ex)
            {
            }
        }
    }



}