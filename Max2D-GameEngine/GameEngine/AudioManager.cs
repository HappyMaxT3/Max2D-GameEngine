using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;

namespace Max2D_GameEngine.GameEngine
{
    public abstract class AudioPlayer
    {
        protected WaveOutEvent player;
        protected AudioFileReader reader;

        public float Volume
        {
            get => reader?.Volume ?? 0.0f;
            set
            {
                if (reader != null)
                    reader.Volume = Math.Clamp(value, 0.0f, 1.0f);

                if (player != null)
                    player.Volume = Math.Clamp(value, 0.0f, 1.0f);
            }
        }

        public void Load(string filePath, float volume)
        {
            if (!File.Exists(filePath))
                Log.Error($"[AUDIO] - Audio file not found at {filePath}");

            reader = new AudioFileReader(filePath);
            player = new WaveOutEvent();
            Volume = volume;

            player.Init(reader);
        }

        public abstract void Play();

        public void Stop()
        {
            player?.Stop();
        }
    }

    public class SoundEffectPlayer : AudioPlayer
    {
        public override void Play()
        {
            if (player == null || reader == null)
            {
                Log.Warning("[AUDIO] - No sound loaded to play.");
                return;
            }

            if (player.PlaybackState != PlaybackState.Stopped)
                player.Stop();

            reader.Position = 0;
            player.Play();
            Log.Info("[AUDIO] - Sound effect played.");
        }
    }

    public class BackgroundMusicPlayer : AudioPlayer
    {
        public override void Play()
        {
            if (player == null || reader == null)
            {
                Log.Warning("[AUDIO] - No background music loaded to play.");
                return;
            }

            player.PlaybackStopped += (sender, args) => Restart();
            player.Play();
            Log.Info("[AUDIO] - Background music started.");
        }

        private void Restart()
        {
            if (reader != null)
            {
                reader.Position = 0;
                player.Play();
                Log.Info("[AUDIO] - Background music restarted.");
            }
        }
    }

    public static class AudioManager
    {
        private static Dictionary<string, SoundEffectPlayer> soundEffects = new();
        private static BackgroundMusicPlayer backgroundMusicPlayer;

        private static string GetAudioPath(string directory)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.Combine(basePath, @"..\..\..\Assets\");

            return Path.Combine(projectPath, $"{directory}.wav");
        }

        public static void LoadSound(string fileName, float volume, string tag)
        {
            string filePath = GetAudioPath(fileName);

            if (soundEffects.ContainsKey(tag))
            {
                Log.Warning($"[AUDIO] - Sound already loaded for tag: {tag}.");
                return;
            }

            try
            {
                var soundPlayer = new SoundEffectPlayer();
                soundPlayer.Load(filePath, volume);
                soundEffects[tag] = soundPlayer;

                Log.Info($"[AUDIO]({tag}) - Sound effect loaded successfully.");
            }
            catch (Exception ex)
            {
                Log.Error($"[AUDIO]({tag}) - Error loading sound effect: {ex.Message}");
            }
        }

        public static void PlaySound(string tag)
        {
            if (!soundEffects.ContainsKey(tag))
            {
                Log.Warning($"[AUDIO]({tag}) - Sound not found for tag.");
                return;
            }

            soundEffects[tag].Play();
        }

        public static void LoadBackgroundMusic(string fileName, float volume, string tag)
        {
            string filePath = GetAudioPath(fileName);

            if (backgroundMusicPlayer != null)
            {
                Log.Warning($"[AUDIO] - Background music already loaded. Overwriting for {tag}.");
            }

            try
            {
                backgroundMusicPlayer = new BackgroundMusicPlayer();
                backgroundMusicPlayer.Load(filePath, volume);

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
                Log.Warning("[AUDIO] - No background music loaded to play.");
                return;
            }

            backgroundMusicPlayer.Play();
        }

        public static void StopBackgroundMusic()
        {
            if (backgroundMusicPlayer == null)
            {
                Log.Warning("[AUDIO] - No background music loaded to stop.");
                return;
            }

            backgroundMusicPlayer.Stop();
        }
    }
}
