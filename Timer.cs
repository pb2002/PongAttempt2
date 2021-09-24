namespace PongAttempt2
{
    public class Timer
    {
        public float Time { get; private set; }
        public float MaxTime { get; set; }

        public Timer(float maxTime)
        {
            Time = maxTime;
            MaxTime = maxTime;
        }
        public void Update(float dt)
        {
            if (Time > 0) Time -= dt;
        }

        public void Reset()
        {
            Time = MaxTime;
        }
        
        public static implicit operator Timer(float t) => new Timer(t);
        public static implicit operator bool(Timer t) => t.Time < 0;
        
    }
}