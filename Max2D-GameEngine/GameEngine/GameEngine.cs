using System;
using System.Windows.Forms;

namespace Max2D_GameEngine.GameEngine
{
    class Canvas : Form
    {
        public Canvas()
        {
            this.DoubleBuffered = true;
        }

        //private void InitializeComponent()
        //{

        //}
    }

    public abstract class GameEngine
	{
		public Vector2 ScreenSize = new Vector2(512, 512);
		private string Title = "GameTitle";
		private Canvas Window = null;
        private Thread GameLoopThread = null;

        private static List<Shape2D> AllShapes = new List<Shape2D>();

        public Color backgroungColor = Color.Beige;

		public GameEngine(Vector2 ScreenSize, string Title)
		{
            Log.Info("Game is starting...");

			this.ScreenSize = ScreenSize;
			this.Title = Title;

			Window = new Canvas();
			Window.Size = new Size((int)this.ScreenSize.X, (int)this.ScreenSize.Y);
			Window.Text = this.Title;
            Window.Paint += Renderer;

			

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

			Application.Run(Window);
		}

        public static void RegisterShape(Shape2D shape)
        {
            AllShapes.Add(shape);
        }

        public static void UnRegisterShape(Shape2D shape)
        {
            AllShapes.Remove(shape);
        }

        private void GameLoop()
        {
            OnLoad();

            while (GameLoopThread.IsAlive) 
            {
                try
                {
                    OnDraw();
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    OnUpdate();
                    Thread.Sleep(1);
                }
                catch
                {
                    Log.Error("Game has not been found...");
                }

            }

        }

        private void Renderer(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(backgroungColor);

            foreach (Shape2D shape in AllShapes)
            {
                g.FillRectangle(new SolidBrush(Color.Red), shape.Position.X, shape.Position.Y, shape.Scale.X, shape.Scale.Y);
            }

        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnDraw();

	}

}

