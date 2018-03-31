using System;
using System.Drawing;

namespace MyGame.GameObjects
{
    /// <summary>
    /// Represents players space ship
    /// </summary>
    public class Ship: Core.BaseGameObject, ICollision, IDie
    {
        private const int MaxEnergy = 100;

        private Core.MoveDirection? moving;
        private int _energy = MaxEnergy;
        public int Energy => _energy;

        static private Bitmap image; // placing image at static var for memory improvement

        public event ShipShootingHandler ShootDone;
        public event ShipEnergyChangeHandler EnergyChanged;


        static Ship()
        {
            image = new Bitmap(@"..\\..\\ship.png");
        }

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="pos">Initial position</param>
        /// <param name="dir">Initial moving vector</param>
        /// <param name="size">Ship size</param>
        public Ship(Point pos, Point dir, Size size): base(pos, dir, size) {
            // handling movements. Moving until moving key relaeased
            Game.MoveKeyPressed += (direction) =>
            {
                if (this.moving == null) this.moving = direction;
            };
            Game.MoveKeyReleased += (direction) =>
            {
                if (this.moving == direction) this.moving = null;
            };

            // handling shooting
            Game.ShootKeyPressed += () => ShootDone?.Invoke(new Point(this.Pos.X + this.Size.Width, this.Pos.Y + this.Size.Height / 2));
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            switch (moving)
            {
                case Core.MoveDirection.Up:
                    Up();
                    break;
                case Core.MoveDirection.Down:
                    Down();
                    break;
            }
        }

        /// <summary>
        /// Decrease ship energy by n
        /// </summary>
        public void EnergyDecrease(int n)
        {
            _energy -= n;
            if (_energy < 1) _energy = 0;
            EnergyChanged?.Invoke();
            if (_energy == 0) Die();
        }

        /// <summary>
        /// Increase ship energy by n
        /// </summary>
        public void EnergyIncrease(int n)
        {
            _energy += n;
            if (_energy > MaxEnergy) _energy = MaxEnergy;
            EnergyChanged?.Invoke();
        }

        /// <summary>
        /// Move up
        /// </summary>
        public void Up()
        {
            if (Pos.Y - Dir.Y >= 0) Pos.Y = Pos.Y - Dir.Y;
        }

        /// <summary>
        /// Move down
        /// </summary>
        public void Down()
        {
            if (Pos.Y + Size.Height + Dir.Y <= Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }

        // IDie implementation
        public event DieHandler Died;
        public void Die()
        {
            Died?.Invoke(this);
        }

        // ICollision implementation
        public void CollisionOccured(ICollision obj)
        {
            if (obj is Asteroid)
            {
                Asteroid asteroid = obj as Asteroid;
                EnergyDecrease(asteroid.Power);
            }
            else if (obj is Aid)
            {
                Aid aid = obj as Aid;
                EnergyIncrease(aid.Power);
            }
        }
        public Rectangle Rect => new Rectangle(Pos, Size);
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
    }
}
