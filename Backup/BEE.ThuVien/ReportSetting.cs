using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraReports.UI;
using  DevExpress.XtraPrinting;

namespace BEE.ThuVien.Report
{
    public class Field
    {
        public Field()
        {
        }

        public Field(Field f)
        {
            this.BGColor = f.BGColor;
            this.Description = f.Description;
            this.FieldName = f.FieldName;
            this.HeaderAlignment = f.HeaderAlignment;
            this.HeaderBGColor = f.HeaderBGColor;
            this.HeaderColor = f.HeaderColor;
            this.HeaderSize = f.HeaderSize;
            this.Order = f.Order;
            this.TextAlignment = f.TextAlignment;
            this.TextColor = f.TextColor;
            this.TextSize = f.TextSize;
            this.Width = f.Width;
            this.SummaryRunning = f.SummaryRunning;
            this.SummaryFunc = f.SummaryFunc;
            this.SummaryText = f.SummaryText;
        }

        public Field(int order, string fieldName, string description, float width)
        {
            this.Order = order;
            this.FieldName = fieldName;
            this.Description = description;
            this.Width = width;
        }

        public int Order { get; set; }
        public string FieldName { get; set; }
        public string FormatString { get; set; }
        public string Description { get; set; }
        public float Width { get; set; }

        public TextAlignment TextAlignment { get; set; }
        public int TextColor { get; set; }
        public int BGColor { get; set; }
        public float TextSize { get; set; }

        public TextAlignment HeaderAlignment { get; set; }
        public int HeaderColor { get; set; }
        public int HeaderBGColor { get; set; }
        public float HeaderSize { get; set; }

        public SummaryRunning SummaryRunning { get; set; }
        public SummaryFunc SummaryFunc { get; set; }
        public string SummaryText { get; set; }
    }

    public class Setting
    {
        public System.Drawing.Printing.PaperKind PaperKind { get; set; }
        public bool Orientation { get; set; }
        public int Top { get; set; }
        public int Bottom { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
        public List<Field> Fields { get; set; }

        public Setting Copy()
        {
            var st = new Setting();
            st.Bottom = this.Bottom;
            st.Left = this.Left;
            st.Orientation = this.Orientation;
            st.PaperKind = this.PaperKind;
            st.Right = this.Right;
            st.Top = this.Top;
            st.Fields = new List<Field>();
            foreach (var f in this.Fields)
            {
                st.Fields.Add(new Field(f));
            }
            return st;
        }
    }
}
