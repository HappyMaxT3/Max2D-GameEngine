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

            //player = new Shape2D(new Vector2(10, 10), new Vector2(10, 10), "TestPlayer");

            player = new Sprite2D(new Vector2(10, 10), new Vector2(20, 20), "Player/karatel", "Player");
        }

        public override void OnDraw()
        {
            
        }

        public override void OnUpdate()
        {

        }

    }
}