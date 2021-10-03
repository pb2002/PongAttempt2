using Microsoft.Xna.Framework.Audio;
using SharpDX.Direct3D9;

namespace Pong
{
    public class AudioClip
    {
        private SoundEffect sfx;
        private SoundEffectInstance instance;
        private Timer fadeOutTimer;
        private bool fadingOut = false;
        public AudioClip(SoundEffect sfx)
        {
            this.sfx = sfx;
            this.instance = sfx.CreateInstance();
            fadeOutTimer = new Timer(0);
        }

        public void Play(bool loop = false)
        {
            if (instance.State != SoundState.Playing) instance.Play();
            instance.IsLooped = loop;
        }

        public void FadeOut(float time)
        {
            fadeOutTimer.MaxTime = time;
            fadeOutTimer.Reset();
            fadingOut = true;
        }

        public void Update(float dt)
        {
            fadeOutTimer.Update(dt);
            if (fadingOut)
            {
                instance.Volume = fadeOutTimer.Time/fadeOutTimer.MaxTime;
            }

            if (!fadeOutTimer) instance.Stop();
        }

    }
}