using System;
using System.Drawing;

namespace MyGame.GameObjects
{
    /// <summary>
    /// An asteroid object.
    /// </summary>
    public class Asteroid : Core.BaseGameObject, ICollision, ICloneable, IComparable<Asteroid>, IDie
    {
        public int Power { get; set; } = 3; // Начиная с версии C# 6.0 была добавлена такая функциональность, как инициализация автосвойств
        static private Bitmap image; // placing image at static var for memory improvement

        static Asteroid()
        {
            image = new Bitmap(@"..\\..\\asteroid.png");
        }

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="pos">Initial position on the form</param>
        /// <param name="dir">The directional vector. Each update call updates position using this vector.</param>
        /// <param name="size">Size of an asteroid.</param>
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = (size.Width * Size.Height) / 10;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            // Asteroid bumps from the screen borders
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }

        // IDie implementation
        public event DieHandler Died;
        public void Die() {
            Died?.Invoke(this);
        }

        // ICollision implementation
        public void CollisionOccured(ICollision obj)
        {
            if (obj is Bullet || obj is Ship)
            {
                this.Die();
            }
        }
        public Rectangle Rect => new Rectangle(Pos, Size);
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        // ICloneable implementation
        /// <summary>
        /// Creates a copy of this asteroid
        /// </summary>
        /// <returns>Asteroid</returns>
        public object Clone()
        {
            Asteroid asteroid = new Asteroid(new Point(Pos.X, Pos.Y), new Point(Dir.X, Dir.Y),
            new Size(Size.Width, Size.Height))
            { Power = Power };
            return asteroid;
        }

        // IComparable implementation
        int IComparable<Asteroid>.CompareTo(Asteroid obj)
        {
            if (Power > obj.Power)
                return 1;
            if (Power < obj.Power)
                return -1;
            return 0;
        }
    }
}
