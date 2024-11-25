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

		public GameEngine(Vector2 ScreenSize, string Title)
		{
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
                    Thread.Sleep(5);
                }
                catch
                {
                    Console.WriteLine("game is loading...");
                }

            }

        }

        private void Renderer(object? sender, PaintEventArgs e)
        {
            Graphics g =e.Graphics;
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnDraw();

	}

}

