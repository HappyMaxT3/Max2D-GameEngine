using System;
using Max2D_GameEngine.GameEngine;

namespace Max2D_GameEngine
{
    class DemoGame : GameEngine.GameEngine
    {
        public DemoGame() : base(new Vector2(615, 515), "2d Game DEMO") { }

        public override void OnDraw()
        {
            Console.WriteLine("OnDraw working");
        }

        public override void OnLoad()
        {
            Console.WriteLine("OnLoad working");
        }

        int frame = 0;
        public override void OnUpdate()
        {
            Console.WriteLine($"frame count: {frame}");
            frame++;
        }

    }
}