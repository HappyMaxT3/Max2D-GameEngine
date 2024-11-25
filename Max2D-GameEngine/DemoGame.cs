using System;
using System.Drawing;
using Max2D_GameEngine.GameEngine;

namespace Max2D_GameEngine
{
    class DemoGame : GameEngine.GameEngine
    {
        private Sprite2D player;
        public DemoGame() : base(new Vector2(615, 515), "2d Game DEMO") { }
 
        public override void OnLoad()
        {
            backgroungColor = Color.Black;

            player = new Sprite2D(new Vector2(10, 10), new Vector2(18, 24), "Player/karatel", "Player");
        }

        public override void OnDraw()
        {
            
        }

        float x = 0.5f;
        public override void OnUpdate()
        {
            player.Position.X += x;
        }

    }
}