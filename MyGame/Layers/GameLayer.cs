using System;
using System.Collections.Generic;
using System.Drawing;

namespace MyGame.Layers
{
    /// <summary>
    /// The main layer for representing gameplay objects. Game logic should be implemented here.
    /// </summary>
    class GameLayer : BaseLayer
    {
        public const int MaxBullets = 5;
        public const int MaxAsteroids = 10;
        public const int MaxAsteroidSpeed = 10;
        public const int MinAsteroidSize = 10;
        public const int MaxAsteroidSize = 20;

        private List<GameObjects.Bullet> _bullets;
        private List<GameObjects.Asteroid> _asteroids;
        private GameObjects.Ship _ship;

        public GameLayer(Core.DebugLog debug) : base(debug)
        {
            debug("Game started");

            _asteroids = new List<GameObjects.Asteroid>(MaxAsteroids);
            _bullets = new List<GameObjects.Bullet>(MaxBullets);

            for (var i = 0; i < MaxAsteroids; i++) _asteroids.Add(GenerateAsteroid());

            _ship = new GameObjects.Ship(new Point(10, 400), new Point(10, 10), new Size(30, 30));
            _ship.ShootDone += _ship_ShootDone;
            _ship.Died += (obj) =>
            {
                debug("Game over");
                Game.GameOver();
            };
        }

        private void _ship_ShootDone(Point shootingPos)
        {
            if (_bullets.Count == MaxBullets)
            {
                this.debug("Tried to shoot, but bullet limit reached");
                return;
            }

            GameObjects.Bullet bullet = new GameObjects.Bullet(shootingPos, new Point(20, 0), new Size(10, 4));
            bullet.Died += (obj) =>
            {
                GameObjects.Bullet diedObj = obj as GameObjects.Bullet;
                _bullets.Remove(diedObj);
            };

            _bullets.Add(bullet);
            this.debug("Shoot done");
        }

        private GameObjects.Asteroid GenerateAsteroid()
        {
            // new asteroids should not appear near the ship
            Point pos = new Point(Game.rnd.Next(50) + (Game.Width - 50), Game.rnd.Next(Game.Height));
            Point dir = new Point(Game.rnd.Next(MaxAsteroidSpeed * 2) - MaxAsteroidSpeed, Game.rnd.Next(MaxAsteroidSpeed * 2) - MaxAsteroidSpeed);
            int d = Game.rnd.Next(MaxAsteroidSize - MinAsteroidSize) + MinAsteroidSize;
            Size size = new Size(d, d);

            GameObjects.Asteroid asteroid = new GameObjects.Asteroid(pos, dir, size);

            asteroid.Died += (obj) =>
            {
                GameObjects.Asteroid diedObj = obj as GameObjects.Asteroid;
                AsteroidShooted(diedObj);
            };

            return asteroid;
        }

        private void AsteroidShooted(GameObjects.Asteroid asteroid)
        {
            _asteroids.Remove(asteroid);
            _asteroids.Add(GenerateAsteroid());
            this.debug("Asteroid destroyed");
        }

        public override void Draw()
        {
            foreach (GameObjects.Asteroid asteroid in _asteroids)
                asteroid.Draw();
            foreach (GameObjects.Bullet bullet in _bullets)
                bullet.Draw();
            _ship?.Draw();
        }

        public override void Update()
        {
            _ship?.Update();

            // processing bullets movements
            // making copy of bullets collection, becase it may change while it being iterated
            GameObjects.Bullet[] tmp_bullets = _bullets.ToArray();
            for (int i = 0; i < tmp_bullets.Length; i++)
                tmp_bullets[i].Update();

            // processing collisions with asteroids
            GameObjects.Asteroid[] tmp_asteroids = _asteroids.ToArray();
            for (int j = 0; j < tmp_asteroids.Length; j++)
            {
                GameObjects.Asteroid asteroid  = tmp_asteroids[j];
                asteroid.Update();

                // collision with ship
                if (_ship != null && asteroid.Collision(_ship))
                    _ship.CollisionOccured(asteroid);

                // collision with bullet
                tmp_bullets = _bullets.ToArray();
                for (int i = 0; i < tmp_bullets.Length; i++)
                    if (asteroid.Collision(tmp_bullets[i]))
                    {
                        asteroid.CollisionOccured(tmp_bullets[i]);
                        tmp_bullets[i].CollisionOccured(asteroid);
                    }
            }
        }
    }
}
