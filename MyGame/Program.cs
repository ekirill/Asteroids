using System;
using System.Windows.Forms;

namespace MyGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form
            {
                Width = 800,
                Height = 600
            };

            Game.Init(form);
            Game.SetScene(new Scenes.GameScene(Game.Logger.Debug));

            form.Show();

            Application.Run(form);
        }
    }
}

