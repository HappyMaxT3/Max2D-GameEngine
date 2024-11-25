using System;
using System.Drawing;
using Max2D_GameEngine.GameEngine;

namespace Max2D_GameEngine
{
    class DemoGame : GameEngine.GameEngine
    {
        public DemoGame() : base(new Vector2(615, 515), "2d Game DEMO") { }
 
        public override void OnLoad()
        {
            backgroungColor = Color.Chocolate;
        }

        public override void OnDraw()
        {
            Shape2D player = new Shape2D(new Vector2(10, 10), new Vector2(10, 10), "Test");
        }

        int frame = 0;
        public override void OnUpdate()
        {
            Console.WriteLine($"frame count: {frame}");
            frame++;
        }

    }
}