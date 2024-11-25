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

            Image temp = Image.FromFile($"Assets/Sprites/{Directory}.png");
            Bitmap sprite =  new Bitmap(temp, (int)this.Scale.X, (int)this.Scale.Y);
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
