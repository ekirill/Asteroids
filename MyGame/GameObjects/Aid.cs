using System;
using System.Drawing;

namespace MyGame.GameObjects
{
    /// <summary>
    /// Represents an aid, that adds energy to ship after collision.
    /// </summary>
    class Aid: Core.BaseGameObject, ICollision, IDie
    {
        public int Power { get; set; } = 20;
        static private Bitmap image; // placing image at static var for memory improvement

        public event DieHandler Died;

        public Aid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            if (dir.X >= 0) throw new ArgumentOutOfRangeException();

            image = new Bitmap(Properties.Resources.aid);
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;

            // Aid bumps from the top and bottom screen borders            
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;

            // Remove aid if was not caught
            if (Pos.X < 0) Die();
        }

        // ICollision implementation
        public Rectangle Rect => new Rectangle(Pos, Size);
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        public void CollisionOccured(ICollision obj)
        {
            if (obj is Ship || obj is Bullet)
            {
                Die();
            }
        }

        // IDie implementation
        public void Die()
        {
            Died?.Invoke(this);
        }
    }
}
