using System;
using System.Drawing;

namespace MyGame.UIComponents
{
    /// <summary>
    /// Class represents immutable text label
    /// </summary>
    class BaseLabel: Core.BaseInterfaceElement
    {
        private Brush textBrush;
        private Brush bgBrush;

        private String text;
        private Font textFont = new System.Drawing.Font("Arial", 16);
        private StringFormat stringFormat;

        private Rectangle rect;

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="rect">Position of label</param>
        /// <param name="bgColor">Background color</param>
        /// <param name="textColor">Text color</param>
        /// <param name="text">Text to display</param>
        public BaseLabel(Rectangle rect, Color bgColor, Color textColor, String text) : base()
        {
            textBrush = new SolidBrush(textColor);
            bgBrush = new SolidBrush(bgColor);
            this.rect = rect;
            this.text = text;

            stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillRectangle(bgBrush, rect);
            Game.Buffer.Graphics.DrawString(text, textFont, textBrush, rect, stringFormat);
        }

        public override void Update() { }
    }
}
