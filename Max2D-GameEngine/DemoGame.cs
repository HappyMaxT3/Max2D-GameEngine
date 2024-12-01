using System;
using System.Drawing;
using Max2D_GameEngine.GameEngine;

namespace Max2D_GameEngine
{
    class DemoGame : GameEngine.GameEngine
    {
        bool right;
        bool left;
        bool up;
        bool down;

        private Sprite2D player;
        private Vector2 lastPos = Vector2.Zero();

        private string[,] Map =
        {
            {"g", "g", "g", "g", "g", "g", "g", "g" },
            {"g", ".", ".", "g", ".", "g", ".", "g" },
            {"g", ".", ".", "g", ".", ".", ".", "g" },
            {"g", ".", ".", ".", ".", "g", ".", "g" },
            {"g", ".", ".", "g", "g", "g", ".", "g" },
            {"g", ".", "c", "g", ".", ".", ".", "g" },
            {"g", ".", "c", "g", "p", ".", ".", "g" },
            {"g", "g", "g", "g", "g", "g", "g", "g" },
        };

        public DemoGame() : base(new Vector2(615, 515), "2d Game DEMO") { }
 
        public override void OnLoad()
        {
            BackgroundColor = Color.Black;

            CameraZoom = new Vector2(0.9f, 0.9f);

            Sprite2D groundRef = new Sprite2D("Sprites/Tiles/GroundTile");
            Sprite2D starRef = new Sprite2D("Sprites/Items/star");
            Sprite2D playerRef = new Sprite2D("Sprites/Player/karatel");

            for (int i = 0;  i < Map.GetLength(1); i++)
            {
                for(int j = 0; j < Map.GetLength(0); j++)
                {
                    if (Map[j, i] == "g") 
                    {
                        new Sprite2D(new Vector2(i * 40, j * 40), new Vector2(40, 40), groundRef, "Ground");
                    }

                    if (Map[j, i] == "c")
                    {
                        new Sprite2D(new Vector2(i * 40, j * 40), new Vector2(15, 15), starRef, "Collectible");
                    }

                    if (Map[j, i] == "p")
                    {
                        player = new Sprite2D(new Vector2(i * 45, j * 40), new Vector2(18, 24), playerRef, "Player");
                    }

                }
            }

        }

        public override void OnDraw()
        {
            
        }

        public override void OnUpdate()
        {
            if (player == null) { return; }

            if (up)
            {
                player.Position.Y -= 1.5f;
            }
            if (down)
            {
                player.Position.Y += 1.5f;
            }
            if (left)
            {
                player.Position.X -= 1.5f;
            }
            if (right)
            {
                player.Position.X += 1.5f;
            }

            Sprite2D star = player.IsColliding("Collectible");
            if (star != null)
            {
                star.DestroySelf();
            }

            if (player.IsColliding("Ground") != null)
            {
                player.Position.X = lastPos.X;
                player.Position.Y = lastPos.Y;
            }
            else
            {
                lastPos.X = player.Position.X;
                lastPos.Y = player.Position.Y;
            }

        }

        public override void GetKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { up = true; }
            if (e.KeyCode == Keys.S) { down = true; }
            if (e.KeyCode == Keys.A) { left = true; }
            if (e.KeyCode == Keys.D) { right = true; }

        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { up = false; }
            if (e.KeyCode == Keys.S) { down = false; }
            if (e.KeyCode == Keys.A) { left = false; }
            if (e.KeyCode == Keys.D) { right = false; }

        }

    }
}