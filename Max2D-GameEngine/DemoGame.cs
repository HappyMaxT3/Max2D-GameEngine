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
        private bool isPlayerAnimationPlaying = false;
        private float playerSpeed = 3.5f;
        private int collectibleItems = 5;

        private Vector2 lastPos = Vector2.Zero();

        private string[,] Map =
        {
            {"b", ".", ".", "f", "t", "r", "r", ".", ".", "r", "r", "o", "o", "o", "r" },
            {"l", "p", " ", ".", ".", "r", "t", ".", ".", "r", "t", "f", "o", "o", "r" },
            {"r", ".", ".", ".", ".", "r", ".", ".", ".", "r", ".", ".", ".", "f", "r" },
            {"r", ".", ".", ".", ".", "t", ".", ".", ".", "t", ".", ".", "c", "h", "r" },
            {"r", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "o", "o", "r" },
            {"r", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f", "r" },
            {"r", ".", ".", ".", ".", "l", ".", ".", ".", ".", ".", ".", ".", ".", "r" },
            {"r", ".", "c", ".", "g", "r", ".", ".", ".", ".", ".", ".", ".", ".", "r" },
            {"r", ".", "l", "w", "r", "r", ".", ".", "l", ".", ".", "l", ".", ".", "r" },
            {"r", "h", "r", "l", "r", "r", "c", "w", "r", "l", "w", "r", "g", "w", "r" },
            {"s", "s", "s", "s", "s", "s", "s", "s", "s", "s", ".", ".", ".", ".", "." },
        };

        public DemoGame() : base(new Vector2(740, 550), "Treasure Hunter - DemoGame") { }
 
        public override void OnLoad()
        {

            BackgroundColor = Color.Aquamarine;
            CameraZoom = new Vector2(1.22f, 1.22f);

            Sprite2D backgroundRef = new Sprite2D("Sprites/Backgrounds/background");
            Sprite2D sandRef = new Sprite2D("Sprites/Tiles/sand");
            Sprite2D rockRef = new Sprite2D("Sprites/Tiles/rock");
            Sprite2D cliffRef = new Sprite2D("Sprites/Tiles/cliff");
            Sprite2D cliffCornerRef = new Sprite2D("Sprites/Tiles/cliffCorner");
            Sprite2D horRockRef = new Sprite2D("Sprites/Tiles/horizontalRock");
            Sprite2D shellRef = new Sprite2D("Sprites/Tiles/shell");
            Sprite2D stalactiteRef = new Sprite2D("Sprites/Tiles/stalactite");
            Sprite2D chestRef = new Sprite2D("Sprites/Items/chest");
            Sprite2D goldRef = new Sprite2D("Sprites/Items/gold");

            AudioManager.LoadBackgroundMusic("Media/underwaterSound", 0.9f, "BackgroundMusic");
            AudioManager.PlayBackgroundMusic();
            AudioManager.LoadSound("Media/collectSound", 0.5f, "CollectSound");
            AudioManager.LoadSound("Media/bang", 0.6f, "BangSound");
            AudioManager.LoadSound("Media/winnerSound", 0.7f, "WinnerSound");

            for (int i = 0;  i < Map.GetLength(1); i++)
            {
                for(int j = 0; j < Map.GetLength(0); j++)
                {
                    if (Map[j, i] == "b")
                    {
                        new Sprite2D(new Vector2(i, j), new Vector2(740, 550), backgroundRef, "NonInteractive");
                    }

                    if (Map[j, i] == "r")
                    {
                        new Sprite2D(new Vector2(i * 40, j * 40), new Vector2(40, 45), rockRef, "Obstacle");
                    }

                    if (Map[j, i] == "t")
                    {
                        new Sprite2D(new Vector2(i * 40, j * 40), new Vector2(40, 75), stalactiteRef, "Obstacle");
                    }

                    if (Map[j, i] == "l")
                    {
                        new Sprite2D(new Vector2(i * 40, j * 40), new Vector2(40, 45), cliffRef, "Obstacle");
                    }

                    if (Map[j, i] == "f")
                    {
                        new Sprite2D(new Vector2(i * 40, j * 40), new Vector2(50, 40), cliffCornerRef, "Obstacle");
                    }

                    if (Map[j, i] == "o")
                    {
                        new Sprite2D(new Vector2(i * 40 + 5, j * 40), new Vector2(50, 45), horRockRef, "Obstacle");
                    }

                    if (Map[j, i] == "s") 
                    {
                        new Sprite2D(new Vector2(i * 60, j * 40), new Vector2(100, 20), sandRef, "Obstacle");
                    }

                    if (Map[j, i] == "h")
                    {
                        new Sprite2D(new Vector2(i * 40 + 8, j * 40 + 28), new Vector2(28, 18), shellRef, "Obstacle");
                    }

                    if (Map[j, i] == "c")
                    {
                        new Sprite2D(new Vector2(i * 40 + 5, j * 40 + 28), new Vector2(25, 20), chestRef, "Collectible");
                    }

                    if (Map[j, i] == "g")
                    {
                        new Sprite2D(new Vector2(i * 40 + 5, j * 40 + 18), new Vector2(35, 25), goldRef, "Collectible");
                    }

                    if (Map[j, i] == "p")
                    {
                        player = new AnimatedSprite2D("Sprites/Player/SubmarineFrames", 200, new Vector2(i + 50, j + 10), new Vector2(50, 30), "Player");
                    }

                    if (Map[j, i] == "w")
                    {
                        new AnimatedSprite2D("Sprites/Tiles/Seaweed", 700, new Vector2(i * 40 + 5, j * 40 + 5), new Vector2(22, 40), "NonInteractive");
                    }

                }

            }

        }

        public override void OnDraw()
        {
            
        }

        public override void OnUpdate()
        {
            if (player == null) return;

            bool isMoving = false;

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

                isMoving = true;
            }
            if (right)
            {
                player.Position.X += playerSpeed;

                isMoving = true;
            }

            if (isMoving && !isPlayerAnimationPlaying)
            {
                ((AnimatedSprite2D)player).StartAnimation();
                isPlayerAnimationPlaying = true;
            }
            else if (!isMoving && isPlayerAnimationPlaying)
            {
                ((AnimatedSprite2D)player).StopAnimation();
                isPlayerAnimationPlaying = false;
            }

            Sprite2D collectible = player.IsColliding("Collectible");
            if (collectible != null)
            {
                AudioManager.PlaySound("CollectSound");
                collectible.DestroySelf();

                collectibleItems -= 1;
                if (collectibleItems == 0)
                {
                    DisplayWinnerImage();
                    return;
                }

            }

            player.Position.Y += 0.4f;

            if (player.IsColliding("Obstacle") != null)
            {
                AudioManager.PlaySound("BangSound");

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

        private void DisplayWinnerImage()
        {
            Sprite2D winnerImage = new Sprite2D(
                new Vector2((ScreenSize.X - 600) / 2, (ScreenSize.Y - 400) / 2),
                new Vector2(400, 300),
                new Sprite2D("UI/winner"),
                "UI"
            );

            AudioManager.PlaySound("WinnerSound");

            var timer = new System.Timers.Timer(5000);
            timer.Elapsed += (s, e) =>
            {
                winnerImage.DestroySelf();

                timer.Stop();
                timer.Dispose();
            };
            timer.AutoReset = false;
            timer.Start();
        }


    }

}