using NAudio.Wave;
using System.Collections.Generic;

namespace Max2D_GameEngine.GameEngine
{
    public static class AudioManager
    {
        private static Dictionary<string, (WaveOutEvent player, AudioFileReader reader)> soundEffects = new();
        private static WaveOutEvent backgroundMusicPlayer = null;
        private static AudioFileReader backgroundMusicReader = null;

        private static string GetAudioPath(string directory)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.Combine(basePath, @"..\..\..\Assets\");

            return Path.Combine(projectPath, $"{directory}.wav");
        }

        public static void LoadSound(string fileName, float volume, string tag)
        {
            string filePath = GetAudioPath(fileName);

            if (!File.Exists(filePath))
            {
                Log.Error($"[AUDIO] - Sound file not found: {filePath}");
                return;
            }

            if (soundEffects.ContainsKey(tag))
            {
                Log.Warning($"[AUDIO]({tag}) - Sound already loaded.");
                return;
            }

            try
            {
                var reader = new AudioFileReader(filePath);
                var player = new WaveOutEvent();

                reader.Volume = Math.Clamp(volume, 0.0f, 1.0f);
                player.Volume = Math.Clamp(volume, 0.0f, 1.0f);

                player.Init(reader);
                soundEffects[tag] = (player, reader);

                Log.Info($"[AUDIO]({tag})({volume}) - Sound loaded successfully.");
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
                Log.Warning($"[AUDIO]({tag}) - Sound not found by this tag.");
                return;
            }

            var (player, reader) = soundEffects[tag];

            if (player.PlaybackState != PlaybackState.Stopped)
            {
                player.Stop();
            }

            reader.Position = 0;

            player.Volume = reader.Volume;

            player.Play();
            Log.Info($"[AUDIO]({tag}) - Started playing audio.");
        }

        public static void LoadBackgroundMusic(string fileName, float volume, string tag)
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
                backgroundMusicPlayer = new WaveOutEvent();

                backgroundMusicReader.Volume = Math.Clamp(volume, 0.0f, 1.0f);
                backgroundMusicPlayer.Volume = Math.Clamp(volume, 0.0f, 1.0f);

                backgroundMusicPlayer.Init(backgroundMusicReader);

                backgroundMusicPlayer.PlaybackStopped += (sender, args) =>
                {
                    RestartBackgroundMusic();
                };

                Log.Info($"[AUDIO]({tag})({volume}) - Background music loaded successfully.");
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

            backgroundMusicPlayer.Volume = backgroundMusicReader.Volume;

            backgroundMusicPlayer.Play();
            Log.Info("[AUDIO] - Start playing background music.");
        }

        public static void StopBackgroundMusic()
        {
            backgroundMusicPlayer?.Stop();
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
