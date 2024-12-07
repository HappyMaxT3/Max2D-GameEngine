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

        private float playerSpeed = 3.5f;

        private Vector2 lastPos = Vector2.Zero();

        private string[,] Map =
        {
            {"g", "g", "g", "g", "g", "g", "g", "g", "g", "g" },
            {"g", ".", ".", "g", ".", "g", ".", ".", ".", "g" },
            {"g", ".", ".", "g", ".", ".", ".", ".", "c", "g" },
            {"g", ".", ".", ".", ".", "g", ".", ".", ".", "g" },
            {"g", ".", ".", "g", "g", "g", ".", ".", ".", "g" },
            {"g", ".", "c", "g", ".", ".", "c", ".", ".", "g" },
            {"g", ".", "c", "g", "p", ".", ".", ".", ".", "g" },
            {"g", "g", "g", "g", "g", "g", "g", "g", "g", "g" },
        };

        public DemoGame() : base(new Vector2(615, 515), "DemoGame") { }
 
        public override void OnLoad()
        {
            BackgroundColor = Color.Aqua;
            CameraZoom = new Vector2(1.5f, 1.5f);

            Sprite2D groundRef = new Sprite2D("Sprites/Tiles/GroundTile");
            Sprite2D starRef = new Sprite2D("Sprites/Items/star");

            AudioManager.LoadBackgroundMusic("Media/sample-15s", 0.2f, "BackgroundMusic");
            AudioManager.PlayBackgroundMusic();
            AudioManager.LoadSound("Media/sample-3s", 0.9f, "CollectSound");

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
                        player = new AnimatedSprite2D("Sprites/Player/SubmarineFrames", 200, new Vector2(i * 15, j * 15), new Vector2(40, 24), "Player");
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
                player.Position.Y -= playerSpeed;
            }
            if (down)
            {
                player.Position.Y += playerSpeed;
            }
            if (left)
            {
                player.Position.X -= playerSpeed;
            }
            if (right)
            {
                player.Position.X += playerSpeed;
            }

            Sprite2D star = player.IsColliding("Collectible");
            if (star != null)
            {
                AudioManager.PlaySound("CollectSound");
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

            Log.Info($"[KEYCODE]({e.KeyCode})");
        }

    }

}