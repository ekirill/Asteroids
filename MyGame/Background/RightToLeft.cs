using System;
using System.Drawing;

namespace MyGame.Background
{
    /// <summary>
    /// Base class for objects, that move from right to left and should appear at the right edge of the screen
    /// when it reaches the left edge
    /// </summary>
    class RightToLeft : Core.BaseGameObject
    {
        public RightToLeft(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        public override void Update()
        {
            base.Update();
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }
}
