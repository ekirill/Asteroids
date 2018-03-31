using System;
using System.Drawing;

namespace MyGame.Background
{
    /// <summary>
    /// Represents a star at space layer
    /// </summary>
    class Star: RightToLeft
    {
        private Pen pen;

        public Star(Point pos, Point dir, Size size, Pen color) : base(pos, dir, size)
        {
            pen = color;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(pen, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(pen, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }
    }
}
