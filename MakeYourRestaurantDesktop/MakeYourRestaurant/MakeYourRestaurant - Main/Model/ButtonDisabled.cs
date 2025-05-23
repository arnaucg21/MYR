using System;
using System.Drawing;
using System.Windows.Forms;

namespace MakeYourRestaurant___Main
{
    public partial class ButtonDisabled : Button
    {
        private Color disabledTextColor = Color.Gray;
        private string modifiedText = "";

        public Color DisabledTextColor
        {
            get { return disabledTextColor; }
            set { disabledTextColor = value; Invalidate(); }
        }

        public string ModifiedText
        {
            get { return modifiedText; }
            set { modifiedText = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!Enabled && !string.IsNullOrEmpty(modifiedText))
            {
                using (SolidBrush brush = new SolidBrush(disabledTextColor))
                {
                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    e.Graphics.DrawString(modifiedText, Font, brush, ClientRectangle, format);
                }
            }
        }
    }
}
