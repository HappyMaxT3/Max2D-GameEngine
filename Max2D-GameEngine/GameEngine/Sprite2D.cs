using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

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

        public Sprite2D()
        {
            this.Position = new Vector2(0, 0);
            this.Scale = new Vector2(1, 1);
            this.Tag = "DefaultSprite";
            this.Sprite = null;
            this.IsReference = false;
        }

        public Sprite2D(string Directory)
        {
            this.IsReference = true;
            this.Directory = Directory;

            string spritePath = GetSpritePath(Directory);

            if (spritePath == null || !File.Exists(spritePath))
            {
                Log.Warning($"[SPRITE2D]({Directory}) not found at path '{spritePath}'");
                return;
            }

            Sprite = LoadSprite(spritePath);
            Log.Info($"[SPRITE2D] Sprite loaded from '{spritePath}'.");
        }

        public Sprite2D(Vector2 Position, Vector2 Scale, Sprite2D reference, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;

            Sprite = reference.Sprite;

            GameEngine.RegisterSprite(this);
            Log.Info($"[SPRITE2D]({Tag}) - has been registered.");
        }

        public Sprite2D(Sprite2D other)
        {
            this.Position = new Vector2(other.Position.X, other.Position.Y);
            this.Scale = new Vector2(other.Scale.X, other.Scale.Y);
            this.Directory = other.Directory;
            this.Tag = other.Tag;
            this.Sprite = other.Sprite;
            this.IsReference = other.IsReference;
        }

        ~Sprite2D()
        {
            if (Sprite != null)
            {
                Sprite = null;
                Log.Info($"[SPRITE2D]({this.Tag}) resources released.");
            }
        }

        private string GetSpritePath(string directory)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.Combine(basePath, @"..\..\..\Assets\");

            return Path.Combine(projectPath, $"{directory}.png");
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
                        Log.Info($"[SPRITE2D]({b.Tag}) at {b.Position.Vector2ToString()} - collided");
                        return b;
                    }
                }
            }

            return null;
        }

        public void DestroySelf()
        {
            Log.Warning($"[SPRITE2D]({this.Tag}) at {this.Position.Vector2ToString()} - deleted.");
            GameEngine.UnRegisterSprite(this);

            if (Sprite != null)
            {
                Sprite = null;
            }
        }

    }

}
