using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Timers;

namespace Max2D_GameEngine.GameEngine
{
    public class AnimatedSprite2D : Sprite2D
    {
        private List<Bitmap> frames; 
        private int currentFrameIndex;
        private int frameDuration;
        private System.Timers.Timer animationTimer;

        public AnimatedSprite2D(string directory, int frameDuration, Vector2 position, Vector2 scale, string tag)
            : base()
        {
            this.Position = position;
            this.Scale = scale;
            this.Tag = tag;
            this.frameDuration = frameDuration;

            frames = LoadFramesFromDirectory(directory);
            if (frames.Count > 0)
            {
                this.Sprite = frames[0]; 
            }

            InitializeTimer();
            GameEngine.RegisterSprite(this);
            Log.Info($"[ANIMATEDSPRITE2D]({Tag}) - Registered with {frames.Count} frames.");
        }

        private List<Bitmap> LoadFramesFromDirectory(string directory)
        {
            var loadedFrames = new List<Bitmap>();
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.Combine(basePath, @"..\..\..\Assets\", directory);

            if (!System.IO.Directory.Exists(projectPath))
            {
                Log.Error($"[ANIMATEDSPRITE2D] Directory '{projectPath}' not found.");
                return loadedFrames;
            }

            var frameFiles = System.IO.Directory.GetFiles(projectPath, "*.png");
            Array.Sort(frameFiles); 

            foreach (var frameFile in frameFiles)
            {
                try
                {
                    using (Image temp = Image.FromFile(frameFile))
                    {
                        loadedFrames.Add(new Bitmap(temp));
                    }
                    Log.Info($"[ANIMATEDSPRITE2D]({frameFile}) - Frame loaded.");
                }
                catch (Exception ex)
                {
                    Log.Error($"[ANIMATEDSPRITE2D]({frameFile}) - Error loading frame: {ex.Message}");
                }
            }

            return loadedFrames;
        }

        private void InitializeTimer()
        {
            animationTimer = new System.Timers.Timer(frameDuration);
            animationTimer.Elapsed += UpdateFrame;
            animationTimer.AutoReset = true;
            animationTimer.Enabled = true;
        }

        private void UpdateFrame(object sender, ElapsedEventArgs e)
        {
            if (frames.Count == 0) return;

            currentFrameIndex = (currentFrameIndex + 1) % frames.Count;
            this.Sprite = frames[currentFrameIndex];
        }

        public void StopAnimation()
        {
            animationTimer?.Stop();
            Log.Info($"[ANIMATEDSPRITE2D]({Tag}) - Animation stopped.");
        }

        public void StartAnimation()
        {
            animationTimer?.Start();
            Log.Info($"[ANIMATEDSPRITE2D]({Tag}) - Animation started.");
        }

        ~AnimatedSprite2D()
        {
            StopAnimation();
            animationTimer.Dispose();
            foreach (var frame in frames)
            {
                frame.Dispose();
            }
            Log.Info($"[ANIMATEDSPRITE2D]({Tag}) - Resources released.");
        }

    }

}
