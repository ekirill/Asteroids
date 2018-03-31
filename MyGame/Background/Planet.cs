using System;
using System.Drawing;

namespace MyGame.Background
{
    /// <summary>
    /// Represents a planet at space layer
    /// </summary>
    class Planet: RightToLeft
    {
        private Brush brush;

        public Planet(Point pos, Point dir, Size size, Color color) : base(pos, dir, size)
        {
            brush = new SolidBrush(color);
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(brush, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
    }
}
