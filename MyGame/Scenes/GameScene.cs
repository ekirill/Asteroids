using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Scenes
{
    /// <summary>
    /// Main game scene with
    /// </summary>
    class GameScene : BaseScene
    {
        private Layers.Space background;
        private Layers.GameLayer game;

        public GameScene(Core.DebugLog debug) : base(debug)
        {
            background = new Layers.Space(debug);
            game = new Layers.GameLayer(debug);
        }

        public override void Draw()
        {
            background.Draw();
            game.Draw();
        }

        public override void Update()
        {
            background.Update();
            game.Update();
        }
    }
}
