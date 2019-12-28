using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Indy500
{
    public class DefaultBackgroundMusicPlaybackService : IBackgroundMusicPlaybackService
    {
        public DefaultBackgroundMusicPlaybackService(ContentManager contentManager)
        {
            Content = contentManager;
        }

        private ContentManager Content { get; set; }
        private Song CurrentSong { get; set; }

        #region IBackgroundMusicPlaybackService Members
        public void StartBackgroundMusic(string assetName)
        {
            if (CurrentSong != null)
            {
                StopBackgroundMusic();
                CurrentSong.Dispose();
                CurrentSong = null;
            }

            // As we're doing more inline game transitions, maybe we should
            // be doing this in a background thread?
            CurrentSong = Content.Load<Song>("Music//" + assetName);
            MediaPlayer.Play(CurrentSong);
        }

        public void PauseBackgroundMusic()
        {
            if (CurrentSong == null) return;
            if (MediaPlayer.State != MediaState.Playing) return;
            MediaPlayer.Pause();
        }

        public void StopBackgroundMusic()
        {
            if (CurrentSong == null) return;
            if (MediaPlayer.State == MediaState.Stopped) return;
            MediaPlayer.Stop();
        }

        public void ResumeBackgroundMusic()
        {
            if (CurrentSong == null) return;
            if (MediaPlayer.State == MediaState.Stopped) return;
            MediaPlayer.Stop();
        }

        public float Volume
        {
            get { return MediaPlayer.Volume; }
            set { MediaPlayer.Volume = value; }
        }
        #endregion
    }
}