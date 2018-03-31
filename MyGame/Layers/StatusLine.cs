using System;
using System.Drawing;

namespace MyGame.Layers
{
    class StatusLine : BaseLayer
    {
        private UIComponents.BaseLabel _energyLabel;
        private UIComponents.BaseLabel _scoreLabel;

        public StatusLine(Core.DebugLog debug, int energy, int score): base(debug)
        {
            UpdateEnergy(energy);
            UpdateScore(score);
        }

        public void UpdateEnergy(int energy)
        {
            _energyLabel = new UIComponents.BaseLabel(new Rectangle(Game.Width - 200, Game.Height - 50, 200, 20), Color.Transparent, Color.Violet, $"Щиты: {energy.ToString()}");
        }

        public void UpdateScore(int score)
        {
            _scoreLabel = new UIComponents.BaseLabel(new Rectangle(Game.Width - 200, Game.Height - 30, 200, 20), Color.Transparent, Color.Pink, $"Счет: {score.ToString()}");
        }

        public override void Draw()
        {
            _energyLabel.Draw();
            _scoreLabel.Draw();
        }

        public override void Update()
        {
            _energyLabel.Update();
            _scoreLabel.Update();
        }
    }
}
