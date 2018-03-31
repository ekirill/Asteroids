using System;
using System.Drawing;

namespace MyGame.Layers
{
    /// <summary>
    /// Layer represents the background of the Game. None of its object should interact with gameplay objects.
    /// </summary>
    class Space: BaseLayer
    {
        private static Core.BaseGameObject[] _objs;

        public Space(Core.DebugLog debug) : base(debug)
        {
            _objs = new Core.BaseGameObject[20];
            Point dir;
            Size size;
            Pen pen = Pens.WhiteSmoke;
            int speed;
            int d;

            // Чуть-чуть планет
            for (int i = 0; i < 2; i++)
            {
                Point pos = new Point(Game.rnd.Next(Game.Width), Game.rnd.Next(Game.Height));
                speed = Game.rnd.Next(4) + 1;
                d = 10 + Game.rnd.Next(15);
                dir = new Point(-speed, 0);
                size = new Size(d, d);
                _objs[i] = new Background.Planet(pos, dir, size, Color.FromArgb(60, 60, 60) );
            }

            for (int i = 2; i < _objs.Length; i++)
            {
                Point pos = new Point(Game.rnd.Next(Game.Width), Game.rnd.Next(Game.Height));
                speed = 5;
                d = 2;
                // Три уровня удаленности от звезд. Те что ближе - больше и быстрее.
                switch (Game.rnd.Next(3))
                {
                    case 0:
                        speed += 10 + Game.rnd.Next(10);
                        d += 4 + Game.rnd.Next(2);
                        pen = Pens.WhiteSmoke;
                        break;
                    case 1:
                        speed += 5 + Game.rnd.Next(10);
                        d += 2 + Game.rnd.Next(2);
                        pen = Pens.Gray;
                        break;
                    case 2:
                        speed += Game.rnd.Next(10);
                        d += Game.rnd.Next(2);
                        pen = Pens.DarkGray;
                        break;
                }

                dir = new Point(-speed, 0);
                size = new Size(d, d);

                _objs[i] = new Background.Star(pos, dir, size, pen);
            }
        }

        public override void Draw()
        {
            foreach (Core.BaseGameObject obj in _objs)
                obj.Draw();
        }

        public override void Update()
        {
            foreach (Core.BaseGameObject obj in _objs)
                obj.Update();
        }
    }
}
