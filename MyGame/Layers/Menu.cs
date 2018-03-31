using System;
using System.Drawing;

namespace MyGame.Layers
{
    /// <summary>
    /// Layer represents menu buttons
    /// </summary>
    class Menu: BaseLayer
    {
        private int buttonHeight = 30;
        private int buttonWidth = 200;
        private int buttonPadding = 10;

        private UIComponents.BaseButton[] UIComponents = new UIComponents.BaseButton[3];
        private UIComponents.BaseLabel copyrightLabel;

        public Menu(Core.DebugLog debug) : base(debug)
        {
            // Placing the middle button at screen center
            Rectangle btnRect_1 = new Rectangle(
                Game.Width / 2 - buttonWidth / 2, Game.Height / 2 - buttonHeight / 2,
                buttonWidth, buttonHeight
            );
            // Other buttons use middle button as an anchor
            Rectangle btnRect_0 = new Rectangle(
                btnRect_1.Left, btnRect_1.Top - buttonPadding - buttonHeight,
                buttonWidth, buttonHeight
            );
            Rectangle btnRect_2 = new Rectangle(
                btnRect_1.Left, btnRect_1.Bottom + buttonPadding,
                buttonWidth, buttonHeight
            );
            UIComponents[0] = new UIComponents.BaseButton(btnRect_0, Color.LightBlue, Color.Black, "Начало игры");
            UIComponents[1] = new UIComponents.BaseButton(btnRect_1, Color.LightBlue, Color.Black, "Рекорды");
            UIComponents[2] = new UIComponents.BaseButton(btnRect_2, Color.LightBlue, Color.Black, "Выход");

            copyrightLabel = new UIComponents.BaseLabel(new Rectangle(Game.Width - 300, Game.Height - 100, 300, 30), Color.Transparent, Color.Blue, "by Kirill Emelyanov");
        }

        public override void Draw()
        {
            foreach (UIComponents.BaseButton btn in UIComponents)
                btn.Draw();

            copyrightLabel.Draw();
        }

        public override void Update()
        {
            foreach (UIComponents.BaseButton btn in UIComponents)
                btn.Update();
        }
    }
}
