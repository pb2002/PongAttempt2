using System;
using Microsoft.Xna.Framework.Audio;

namespace Pong
{
    public class AudioClip
    {
        private SoundEffect sfx;
        private SoundEffectInstance instance;
        private Timer fadeTimer;
        private bool isFadedOut = true;
        public float Volume => instance.Volume;
        public AudioClip(SoundEffect sfx)
        {
            this.sfx = sfx;
            // store an instance of the sound effect
            instance = sfx.CreateInstance();
            fadeTimer = new Timer(0);
        }

        public void Play(float volume = 1f, bool loop = false)
        {
            instance.Volume = volume;
            instance.IsLooped = loop;
            // prevents the sound effect from playing multiple times
            if (instance.State != SoundState.Playing) instance.Play();
        }
        
        /// <summary>
        /// Fade in the audio clip
        /// </summary>
        /// <param name="time">duration of the fade-in in seconds.</param>
        public void FadeIn(float time)
        {
            if (!isFadedOut) return;
            // reset the timer
            fadeTimer.MaxTime = time;
            fadeTimer.Reset();
            isFadedOut = false;
        }
        
        /// <summary>
        /// Fade out the audio clip
        /// </summary>
        /// <param name="time">duration of the fade-out in seconds.</param>
        public void FadeOut(float time)
        {
            if(isFadedOut) return;
            
            fadeTimer.MaxTime = time;
            fadeTimer.Reset();
            isFadedOut = true;
        }
        
        public void Update(float dt)
        {
            if (fadeTimer.IsRunning)
            {
                // reverse fade curve when fading in (1-t vs t)
                float t = isFadedOut 
                    ? fadeTimer.Time / fadeTimer.MaxTime 
                    : 1f - fadeTimer.Time / fadeTimer.MaxTime;
                
                // quadratic taper for natural fading
                instance.Volume = t * t;
            }
            fadeTimer.Update(dt);
        }

    }
}