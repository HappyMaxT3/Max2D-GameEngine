using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Max2D_GameEngine.GameEngine
{
    public class Sprite2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Directory = "";
        public string Tag = "";
        public Bitmap Sprite = null;

        public Sprite2D(Vector2 Position, Vector2 Scale, string Directory, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Directory = Directory;
            this.Tag = Tag;

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = System.IO.Path.Combine(basePath, @"..\..\..\Assets\");

            string spritePath = System.IO.Path.Combine(projectPath, $"{Directory}.png");

            if (!System.IO.File.Exists(spritePath))
                Log.Error($"[SPRITE2D]({Tag}) not found at path '{spritePath}'.");

            Image temp = Image.FromFile(spritePath);
            Bitmap sprite =  new Bitmap(temp);
            Sprite = sprite;

            Log.Info($"[SPRITE2D]({Directory}) - has been registered.");

            GameEngine.RegisterSprite(this);
        }

        public bool IsColliding(string tag)
        {
            /*
            if (a.Position.X < b.Position.X + b.Scale.X &&
                a.Position.X + a.Scale.X > b.Position.X &&
                a.Position.Y < b.Position.Y + b.Scale.Y &&
                a.Position.Y + a.Scale.Y > b.Position.Y)
            {
                return true;
            }
            */
            foreach(Sprite2D b in GameEngine.AllSprites)
            {
                if (b.Tag == tag)
                {
                    if (Position.X < b.Position.X + b.Scale.X &&
                        Position.X + Scale.X > b.Position.X &&
                        Position.Y < b.Position.Y + b.Scale.Y &&
                        Position.Y + Scale.Y > b.Position.Y)
                    {
                        return true;
                    }
                }

            }

            return false;
        }

        public void DestroySelf()
        {
            GameEngine.UnRegisterSprite(this);
        }
    }
}
