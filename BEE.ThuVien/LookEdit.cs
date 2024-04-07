using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace LandSoft.Library
{
    class LookEdit: LookUpEdit
    {
         public LookEdit()
        {
            this.Width = 100;
            this.Height = 20;

            this.Properties.NullText = "";
            this.Properties.ValueMember = "ID";
            this.Properties.DisplayMember = "MaLCV";
            this.Properties.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Combo });
            this.Properties.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Glyph, Image = LandSoft.Library.Properties.Resources.add });

            this.KeyUp += new System.Windows.Forms.KeyEventHandler(LookEdit_KeyUp);
        }

        void LookEdit_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Delete)
            {
                this.EditValue = null;
            }
        }
    }
}
