using System;
using System.Drawing;

namespace MyGame.Scenes
{
    /// <summary>
    /// Scene represents the first menu screen of the game
    /// </summary>
    class SplashScene : BaseScene
    {
        private Layers.Space background;
        private Layers.Menu menu;

        public SplashScene(Core.DebugLog debug) : base(debug)
        {
            background = new Layers.Space(debug);
            menu = new Layers.Menu(debug);
        }

        public override void Draw()
        {
            background.Draw();
            menu.Draw();
        }

        public override void Update()
        {
            background.Update();
            menu.Update();
        }
    }
}
