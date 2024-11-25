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
	}

	public abstract class GameEngineClass
	{
		public Vector2 ScreenSize = new Vector2(512, 512);
		private string Title = "jd";

		public GameEngine(Vector2 ScreenSize, string Title)
		{
			this.ScreenSize = ScreenSize;
			this.Title = Title;
		}

	}

}

