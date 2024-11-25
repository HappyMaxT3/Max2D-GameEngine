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

		public GameEngine(Vector2 ScreenSize, string Title)
		{
			this.ScreenSize = ScreenSize;
			this.Title = Title;

			Window = new Canvas();
			Window.Size = new Size((int)this.ScreenSize.X, (int)this.ScreenSize.Y);
			Window.Text = this.Title;

			Application.Run(Window);
		}

	}

}

