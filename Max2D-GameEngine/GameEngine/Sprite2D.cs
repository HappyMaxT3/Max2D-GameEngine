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
        public bool IsReference = false;

        public Sprite2D(Vector2 Position, Vector2 Scale, string Directory, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Directory = Directory;
            this.Tag = Tag;

            string spritePath = GetSpritePath(Directory);

            if (spritePath == null || !System.IO.File.Exists(spritePath))
            {
                Log.Error($"[SPRITE2D]({Tag}) not found at path '{spritePath}'");

                return;
            }

            Sprite = LoadSprite(spritePath);

            Log.Info($"[SPRITE2D]({Directory}) - has been registered.");

            GameEngine.RegisterSprite(this);
        }

        public Sprite2D(string Directory)
        {
            this.IsReference = true;
            this.Directory = Directory;

            string spritePath = GetSpritePath(Directory);

            if (spritePath == null || !System.IO.File.Exists(spritePath))
            {
                Log.Error($"[SPRITE2D]({Tag}) not found at path '{spritePath}'");

                return;
            }

            Sprite = LoadSprite(spritePath);

            Log.Info($"[SPRITE2D]({Directory}) - has been registered.");

            GameEngine.RegisterSprite(this);
        }

        public Sprite2D(Vector2 Position, Vector2 Scale, Sprite2D reference, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;

            Sprite = reference.Sprite;

            Log.Info($"[SPRITE2D]({reference}) - has been registered.");

            GameEngine.RegisterSprite(this);
        }

        private string GetSpritePath(string directory)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = System.IO.Path.Combine(basePath, @"..\..\..\Assets\");

            return System.IO.Path.Combine(projectPath, $"{directory}.png");
        }
        private Bitmap LoadSprite(string spritePath)
        {
            using (Image temp = Image.FromFile(spritePath))
            {
                return new Bitmap(temp);
            }

        }

        public Sprite2D IsColliding(string tag)
        {
            foreach (Sprite2D b in GameEngine.AllSprites)
            {
                if (b.Tag == tag)
                {
                    if (Position.X < b.Position.X + b.Scale.X &&
                        Position.X + Scale.X > b.Position.X &&
                        Position.Y < b.Position.Y + b.Scale.Y &&
                        Position.Y + Scale.Y > b.Position.Y)
                    {
                        return b;
                    }
                }
            }

            return null;
        }

        public void DestroySelf()
        {
            if (Sprite != null)
            {
                Sprite.Dispose();
                Sprite = null;
            }

            GameEngine.UnRegisterSprite(this);

        }
    }


}

