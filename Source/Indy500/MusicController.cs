using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Indy500
{
    class MusicController
    {
        public enum MusicInput
        {
            PlayNextSong,
            PlayRandomSong,
            StartMusic,
            PauseMusic,
            StopMusic,
            Undefined
        }
        public MusicInput Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.N)) // Play next song in list
                return MusicInput.PlayNextSong;

            if (Keyboard.GetState().IsKeyDown(Keys.R)) // Play random song from list
                return MusicInput.PlayRandomSong;

            if (Keyboard.GetState().IsKeyDown(Keys.S)) // Play next song in list
                return MusicInput.StartMusic;

            if (Keyboard.GetState().IsKeyDown(Keys.End)) // Play next song in list
                return MusicInput.StopMusic;

            if (Keyboard.GetState().IsKeyDown(Keys.P)) // Play next song in list
                return MusicInput.PauseMusic;

            return MusicInput.Undefined;
        }
    }
}
