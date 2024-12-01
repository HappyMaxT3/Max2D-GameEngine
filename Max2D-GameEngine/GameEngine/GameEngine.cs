using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
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

    public abstract class GameEngine
    {
        public Vector2 ScreenSize = new Vector2(512, 512);
        private string Title = "GameTitle";
        private Canvas Window = null;
        private Thread GameLoopThread = null;

        public static List<Shape2D> AllShapes = new List<Shape2D>();
        public static List<Sprite2D> AllSprites = new List<Sprite2D>();

        public Color BackgroundColor = Color.Beige; 

        public Vector2 CameraPosition = Vector2.Zero();
        public float CameraAngle = 0.0f;

        public GameEngine(Vector2 screenSize, string title)
        {
            Log.Info("Game is starting...");

            this.ScreenSize = screenSize;
            this.Title = title;

            Window = new Canvas
            {
                Size = new Size((int)this.ScreenSize.X, (int)this.ScreenSize.Y),
                Text = this.Title
            };
            Window.Paint += Renderer;
            Window.KeyDown += Window_KeyDown;
            Window.KeyUp += Window_KeyUp;

            GameLoopThread = new Thread(GameLoop) { IsBackground = true }; 
            GameLoopThread.Start();

            Application.Run(Window);
        }

        private void Window_KeyDown(object? sender, KeyEventArgs e)
        {
            GetKeyDown(e);
        }

        private void Window_KeyUp(object? sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }

        public static void RegisterShape(Shape2D shape)
        {
            AllShapes.Add(shape);
        }

        public static void UnRegisterShape(Shape2D shape)
        {
            AllShapes.Remove(shape);
        }

        public static void RegisterSprite(Sprite2D sprite)
        {
            AllSprites.Add(sprite);
        }

        public static void UnRegisterSprite(Sprite2D sprite)
        {
            AllSprites.Remove(sprite);
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
                    Thread.Sleep(16); 
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

            g.Clear(BackgroundColor); 

            g.TranslateTransform(CameraPosition.X, CameraPosition.Y);
            g.RotateTransform(CameraAngle);

            foreach (Shape2D shape in AllShapes)
            {
                g.FillRectangle(new SolidBrush(Color.Red), shape.Position.X, shape.Position.Y, shape.Scale.X, shape.Scale.Y);
            }

            for (int i = 0; i < AllSprites.Count; i++)
            {
                Sprite2D sprite = AllSprites[i];
                g.DrawImage(sprite.Sprite, sprite.Position.X, sprite.Position.Y, sprite.Scale.X, sprite.Scale.Y);
            }


        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnDraw();

        public abstract void GetKeyDown(KeyEventArgs e);
        public abstract void GetKeyUp(KeyEventArgs e);

    }

}
