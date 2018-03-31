using System;
using System.Drawing;

namespace MyGame.GameObjects
{
    /// <summary>
    /// Class represents the bullet
    /// </summary>
    class Bullet : Core.BaseGameObject, ICollision, IDie
    {
        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="pos">Initial position on the form</param>
        /// <param name="dir">The directional vector. Each update call updates position using this vector.</param>
        /// <param name="size">Size of a bullet.</param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            // bullet should be aligned horozontally by the center of position
            Pos.Y -= Size.Height / 2;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillRectangle(Brushes.LightSkyBlue, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            // removing bullets outside visible area
            if (IsOutsideScreen()) Die();
        }

        public bool IsOutsideScreen()
        {
            return Pos.X > Game.Width;
        }

        // ICollision implementation
        public void CollisionOccured(ICollision obj)
        {
            if (obj is Asteroid)
            {
                Die();
            }
        }
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect => new Rectangle(Pos, Size);

        // IDie implementation
        public event DieHandler Died;
        public void Die()
        {
            Died?.Invoke(this);
        }
    }
}
