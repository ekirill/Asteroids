using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Scenes
{
    /// <summary>
    /// Main game scene with. Game logic should be implemented here.
    /// </summary>
    class GameScene : BaseScene
    {
        private Layers.Space background;
        private Layers.GameLayer game;
        private Layers.StatusLine status;

        private int _score;

        public GameScene(Core.DebugLog debug) : base(debug)
        {
            _score = 0;

            background = new Layers.Space(debug);
            game = new Layers.GameLayer(debug);
            status = new Layers.StatusLine(debug, game.ShipEnergy(), _score);

            game.GameOverOccured += () => Game.GameOver();
            game.ShipEnergyChanged += () => status.UpdateEnergy(game.ShipEnergy());
            game.AsteroidShoot += (GameObjects.Asteroid asteroid) =>
            {
                _score += asteroid.Power;
                status.UpdateScore(_score);
            };
        }

        public override void Draw()
        {
            background.Draw();
            game.Draw();
            status.Draw();
        }

        public override void Update()
        {
            background.Update();
            game.Update();
            status.Update();
        }
    }
}
