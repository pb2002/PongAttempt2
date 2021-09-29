using Microsoft.Xna.Framework.Audio;

namespace Pong
{
    public class AudioClip
    {
        private SoundEffect sfx;
        private SoundEffectInstance instance;
        public AudioClip(SoundEffect sfx)
        {
            this.sfx = sfx;
            this.instance = sfx.CreateInstance();
        }

        public void Play()
        {
            if (instance.State != SoundState.Playing) instance.Play();
        }

    }
}