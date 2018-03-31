using System;
using System.Collections.Generic;
using System.Drawing;

namespace MyGame.Layers
{
    /// <summary>
    /// The main layer for representing gameplay objects.
    /// </summary>
    class GameLayer : BaseLayer
    {
        public const int MaxBullets = 5;

        public const int MaxAsteroids = 10;
        public const int MaxAsteroidSpeed = 10;
        public const int MinAsteroidSize = 10;
        public const int MaxAsteroidSize = 20;

        public const int MaxAids = 2;
        public const int MaxAidSpeed = 10;

        private List<GameObjects.Bullet> _bullets;
        private List<GameObjects.Asteroid> _asteroids;
        private List<GameObjects.Aid> _aids;

        private GameObjects.Ship _ship;

        public event Core.GameOverHandler GameOverOccured;
        public event GameObjects.ShipEnergyChangeHandler ShipEnergyChanged;
        public event GameObjects.AsteroidShootHandler AsteroidShoot;

        public GameLayer(Core.DebugLog debug) : base(debug)
        {
            debug("Game started");

            _asteroids = new List<GameObjects.Asteroid>(MaxAsteroids);
            _bullets = new List<GameObjects.Bullet>(MaxBullets);
            _aids = new List<GameObjects.Aid>(MaxAids);

            for (var i = 0; i < MaxAsteroids; i++) _asteroids.Add(GenerateAsteroid());
            for (var i = 0; i < MaxAids; i++) _aids.Add(GenerateAid());

            _ship = new GameObjects.Ship(new Point(10, 400), new Point(10, 10), new Size(30, 30));
            _ship.ShootDone += _ship_ShootDone;
            _ship.Died += (obj) =>
            {
                debug("Game over");
                GameOverOccured?.Invoke();
            };
            _ship.EnergyChanged += () => ShipEnergyChanged?.Invoke();
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
                AsteroidShot(diedObj);
            };

            return asteroid;
        }

        private GameObjects.Aid GenerateAid()
        {
            // new aids should not appear near the ship
            Point pos = new Point(Game.rnd.Next(50) + (Game.Width - 50), Game.rnd.Next(Game.Height));
            Point dir = new Point(-10, Game.rnd.Next(MaxAidSpeed * 2) - MaxAidSpeed);
            Size size = new Size(20, 20);

            GameObjects.Aid aid = new GameObjects.Aid(pos, dir, size);

            aid.Died += (obj) => 
            {
                GameObjects.Aid diedObj = obj as GameObjects.Aid;
                AidDied(diedObj);
            };

            return aid;
        }

        private void AidDied(GameObjects.Aid aid)
        {
            _aids.Remove(aid);
            _aids.Add(GenerateAid());
        }

        private void AsteroidShot(GameObjects.Asteroid asteroid)
        {
            AsteroidShoot?.Invoke(asteroid);
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
            foreach (GameObjects.Aid aid in _aids)
                aid.Draw();
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
                GameObjects.Asteroid asteroid = tmp_asteroids[j];
                asteroid.Update();

                // collision with ship
                if (_ship != null && asteroid.Collision(_ship))
                {
                    _ship.CollisionOccured(asteroid);
                    asteroid.CollisionOccured(_ship);
                }

                // collision with bullet
                tmp_bullets = _bullets.ToArray();
                for (int i = 0; i < tmp_bullets.Length; i++)
                    if (asteroid.Collision(tmp_bullets[i]))
                    {
                        asteroid.CollisionOccured(tmp_bullets[i]);
                        tmp_bullets[i].CollisionOccured(asteroid);
                    }
            }

            // processing collisions with aids
            GameObjects.Aid[] tmp_aids = _aids.ToArray();
            GameObjects.Aid aid;
            for (int i = 0; i < tmp_aids.Length; i++)
            {
                aid = tmp_aids[i];
                aid.Update();

                // collision with ship
                if (_ship != null && aid.Collision(_ship))
                {
                    aid.CollisionOccured(_ship);
                    _ship.CollisionOccured(aid);
                }

                // collision with bullet
                tmp_bullets = _bullets.ToArray();
                for (int j = 0; j < tmp_bullets.Length; j++)
                    if (aid.Collision(tmp_bullets[j]))
                    {
                        aid.CollisionOccured(tmp_bullets[j]);
                        tmp_bullets[j].CollisionOccured(aid);
                    }
            }
        }

        public int ShipEnergy() => _ship?.Energy ?? 0;
    }
}
