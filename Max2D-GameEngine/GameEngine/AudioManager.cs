using NAudio.Wave;
using System.Collections.Generic;

namespace Max2D_GameEngine.GameEngine
{
    public static class AudioManager
    {
        private static Dictionary<string, WaveOutEvent> soundEffects = new();
        private static WaveOutEvent backgroundMusicPlayer = null;
        private static AudioFileReader backgroundMusicReader = null;

        private static string GetAudioPath(string directory)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.Combine(basePath, @"..\..\..\Assets\");

            return Path.Combine(projectPath, $"{directory}.wav");
        }

        public static void LoadSound(string fileName, string tag)
        {
            string filePath = GetAudioPath(fileName);

            if (!File.Exists(filePath))
            {
                Log.Warning($"[AUDIO] - Sound file not found: {filePath}");
                return;
            }

            if (soundEffects.ContainsKey(tag))
            {
                Log.Warning($"[AUDIO] - Sound with tag '{tag}' already loaded.");
                return;
            }

            try
            {
                var reader = new AudioFileReader(filePath);
                var player = new WaveOutEvent { Volume = 0.5f };
                player.Init(reader);
                soundEffects[tag] = player;

                Log.Info($"[AUDIO]({tag}) - Sound loaded successfully.");
            }
            catch (Exception ex)
            {
                Log.Error($"[AUDIO]({tag}) - Error loading sound: {ex.Message}");
            }
        }

        public static void PlaySound(string tag)
        {
            if (!soundEffects.ContainsKey(tag))
            {
                Log.Warning($"[AUDIO] - Sound '{tag}' not found.");
                return;
            }

            Log.Info($"[AUDIO]({tag}) - Start playing.");
            soundEffects[tag].Play();
        }

        public static void LoadBackgroundMusic(string fileName, string tag)
        {
            string filePath = GetAudioPath(fileName);

            if (!File.Exists(filePath))
            {
                Log.Warning($"[AUDIO] - Background music file not found: {filePath}");
                return;
            }

            try
            {
                backgroundMusicReader = new AudioFileReader(filePath);
                backgroundMusicPlayer = new WaveOutEvent { Volume = 0.3f };
                backgroundMusicPlayer.Init(backgroundMusicReader);

                backgroundMusicPlayer.PlaybackStopped += (sender, args) =>
                {
                    RestartBackgroundMusic(); 
                };

                Log.Info($"[AUDIO]({tag}) - Background music loaded successfully.");
            }
            catch (Exception ex)
            {
                Log.Error($"[AUDIO]({tag}) - Error loading background music: {ex.Message}");
            }
        }

        public static void PlayBackgroundMusic()
        {
            if (backgroundMusicPlayer == null)
            {
                Log.Warning("[AUDIO] - No background music loaded.");
                return;
            }

            Log.Info("[AUDIO] - Start playing background music.");
            backgroundMusicPlayer.Play();
        }

        public static void StopBackgroundMusic()
        {
            backgroundMusicPlayer?.Stop();
            // backgroundMusicReader?.Dispose();
        }

        private static void RestartBackgroundMusic()
        {
            if (backgroundMusicReader != null)
            {
                backgroundMusicReader.Position = 0; 
                backgroundMusicPlayer?.Play(); 
                Log.Info("[AUDIO] - Background music restarted.");
            }
        }
    }
}
