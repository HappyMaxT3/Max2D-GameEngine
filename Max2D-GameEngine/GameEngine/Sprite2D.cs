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
            string projectPath = System.IO.Path.Combine(basePath, @"..\..\..\Assets\Sprites");

            string spritePath = System.IO.Path.Combine(projectPath, $"{Directory}.png");

            if (!System.IO.File.Exists(spritePath))
                Log.Error($"[SPRITE2D]({Tag}) not found at path '{spritePath}'.");

            Image temp = Image.FromFile(spritePath);
            Bitmap sprite =  new Bitmap(temp);
            Sprite = sprite;

            Log.Info($"[SPRITE2D]({Directory}) - has been registered.");

            GameEngine.RegisterSprite(this);
        }

        public void DestroySelf()
        {
            GameEngine.UnRegisterSprite(this);
        }
    }
}
