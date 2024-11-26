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

        private string[,] Map =
        {
            {".", ".", ".", ".", ".", ".", "." , "."},
            {".", ".", ".", ".", ".", ".", "." , "."},
            {".", ".", ".", ".", ".", ".", ".", "." },
            {".", ".", ".", "g", "g", ".", ".", "g" },
            {"g", "g", "g", "g", "g", "g", "g", "g" },
            {".", ".", ".", ".", ".", ".", ".", "." },
        };

        public DemoGame() : base(new Vector2(615, 515), "2d Game DEMO") { }
 
        public override void OnLoad()
        {
            backgroungColor = Color.Black;

            player = new Sprite2D(new Vector2(50, 50), new Vector2(18, 24), "Sprites/Player/karatel", "Player");

            for(int i = 0;  i < Map.GetLength(1); i++)
            {
                for(int j = 0; j < Map.GetLength(0); j++)
                {
                    if (Map[j, i] == "g") 
                    {
                        new Sprite2D(new Vector2(i * 40, j * 40), new Vector2(40, 40), "Sprites/Tiles/GroundTile", "Ground");
                    }
                    
                }
            }


        }

        public override void OnDraw()
        {
            
        }

        public override void OnUpdate()
        {
            if (up)
            {
                player.Position.Y -= 0.5f;
            }
            if (down)
            {
                player.Position.Y += 0.5f;
            }
            if (left)
            {
                player.Position.X -= 0.5f;
            }
            if (right)
            {
                player.Position.X += 0.5f;
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