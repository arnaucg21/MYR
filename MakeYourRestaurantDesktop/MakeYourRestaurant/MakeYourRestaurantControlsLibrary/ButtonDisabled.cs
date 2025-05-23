using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourRestaurantControlsLibrary
{
    public partial class ButtonDisabled : Button
    {
        private Color disabledTextColor = Color.Gray; 

        public Color DisabledTextColor
        {
            get { return disabledTextColor; }
            set { disabledTextColor = value; Invalidate(); } 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!Enabled)
            {
                using (SolidBrush brush = new SolidBrush(disabledTextColor))
                {
                    e.Graphics.DrawString(Text, Font, brush, ClientRectangle);
                }
            }
        }
    }
}
