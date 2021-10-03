using System;
using Microsoft.Xna.Framework.Audio;
using SharpDX.Direct3D9;

namespace Pong
{
    public class AudioClip
    {
        private SoundEffect sfx;
        private SoundEffectInstance instance;
        private Timer fadeTimer;
        private bool fadingOut = false;
        public AudioClip(SoundEffect sfx)
        {
            this.sfx = sfx;
            this.instance = sfx.CreateInstance();
            fadeTimer = new Timer(0);
        }

        public void Play(bool loop = false, float volume = 1f)
        {
            instance.Volume = volume;
            instance.IsLooped = loop;
            if (instance.State != SoundState.Playing) instance.Play();
        }
        public void FadeIn(float time)
        {
            if (instance.Volume >= 0.95f) return;
            fadeTimer.MaxTime = time;
            fadeTimer.Reset();
            fadingOut = false;
        }
        public void FadeOut(float time)
        {
            fadeTimer.MaxTime = time;
            fadeTimer.Reset();
            fadingOut = true;
        }

        public void Update(float dt)
        {
            if (fadeTimer)
            {
                var t = fadingOut ? fadeTimer.Time / fadeTimer.MaxTime : 1f - (fadeTimer.Time / fadeTimer.MaxTime);
                instance.Volume = t * t;
            }
            fadeTimer.Update(dt);
        }

    }
}