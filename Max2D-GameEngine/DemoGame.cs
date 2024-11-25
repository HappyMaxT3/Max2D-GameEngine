using System;
using System.Drawing;
using Max2D_GameEngine.GameEngine;

namespace Max2D_GameEngine
{
    class DemoGame : GameEngine.GameEngine
    {
        private Shape2D player;
        public DemoGame() : base(new Vector2(615, 515), "2d Game DEMO") { }
 
        public override void OnLoad()
        {
            backgroungColor = Color.Black;
        }

        public override void OnDraw()
        {
            player = new Shape2D(new Vector2(10, 10), new Vector2(10, 10), "Test");
        }

        public override void OnUpdate()
        {

        }

    }
}