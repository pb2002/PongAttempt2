namespace Pong
{
    /// <summary>
    /// Timer class used for delays and timed behaviour
    /// </summary>
    public class Timer
    {
        public float Time { get; private set; }
        public float MaxTime { get; set; }

        public Timer(float maxTime)
        {
            Time = maxTime;
            MaxTime = maxTime;
        }
        /// <summary>
        /// Updates the timer
        /// </summary>
        /// <param name="dt">the duration of the last frame in seconds.</param>
        public void Update(float dt)
        {
            // subtract dt from time while timer is running (time > 0)
            if (Time > 0) Time -= dt;
            if (Time < 0) Time = 0;
        }
        /// <summary>
        /// Resets the timer to MaxTime.
        /// </summary>
        public void Reset()
        {
            Time = MaxTime;
        }
        
        /// <summary>
        /// Is the timer currently running
        /// </summary>
        public bool IsRunning => Time > 0;
        public static implicit operator bool(Timer t) => t.IsRunning;
        
    }
}