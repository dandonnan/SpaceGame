using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public class AudioManager
    {
        Song currentBGM;

        public AudioManager()
        {
        }

        public void PlaySFX(SoundEffect sfx)
        {
            sfx.Play();
        }

        public void PlayBGM(Song bgm)
        {
            currentBGM = bgm;
            MediaPlayer.Stop();
            MediaPlayer.Play(currentBGM);
        }

        public bool IsBGMPlaying()
        {
            if (MediaPlayer.State == MediaState.Playing)
                return true;

            return false;
        }

        public bool IsBGMPaused()
        {
            if (MediaPlayer.State == MediaState.Paused)
                return true;

            return false;
        }

        public bool IsBGMStopped()
        {
            if (MediaPlayer.State == MediaState.Stopped)
                return true;

            return false;
        }

        public void ResumeBGM()
        {
            MediaPlayer.Resume();
        }

        public void PauseBGM()
        {
            MediaPlayer.Pause();
        }
    }
}
