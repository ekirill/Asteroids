using System;
using System.Windows.Forms;
using System.Drawing;


namespace MyGame
{


    static class Game
    {
        public static Random rnd;

        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }

        private static Timer _timer = new Timer();

        public static event Core.MoveKeyHandler MoveKeyPressed;
        public static event Core.MoveKeyHandler MoveKeyReleased;
        public static event Core.ShootKeyHandler ShootKeyPressed;

        public static Core.Logger Logger { get; }
        private static Scenes.BaseScene scene;

        static Game()
        {
            rnd = new Random();
            Logger = new Core.Logger();
        }

        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики
            Graphics g;
            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();// Создаём объект - поверхность рисования и связываем его с формой
                                      // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            if ( !(1 <= Width && Width <= 1000) || !(1 <= Height && Height <= 1000)) {
                throw new ArgumentOutOfRangeException();
            }

            // Связываем буфер в памяти с графическим объектом.
            // для того, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));


            form.KeyDown += Form_KeyDown;            form.KeyUp += Form_KeyUp;
            _timer = new Timer { Interval = 50 };
            _timer.Start();
            _timer.Tick += Timer_Tick;
        }

        public static void SetScene(Scenes.BaseScene newScene) => scene = newScene;

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) ShootKeyPressed?.Invoke();
            if (e.KeyCode == Keys.Up) MoveKeyPressed?.Invoke(Core.MoveDirection.Up);
            if (e.KeyCode == Keys.Down) MoveKeyPressed?.Invoke(Core.MoveDirection.Down);
        }

        private static void Form_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) MoveKeyReleased?.Invoke(Core.MoveDirection.Up);
            if (e.KeyCode == Keys.Down) MoveKeyReleased?.Invoke(Core.MoveDirection.Down);
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            scene?.Draw();
            Buffer.Render();
        }

        public static void Update()
        {
            scene?.Update();
        }

        public static void GameOver()
        {
            _timer.Stop();
            UIComponents.BaseLabel label = new UIComponents.BaseLabel(new Rectangle(0, Height / 2 - 50, Width, 100), Color.Black, Color.Red, "The End");
            label.Draw();
            Buffer.Render();
        }
    }
}
