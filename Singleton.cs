namespace PongAttempt2
{
    public class Singleton<T> where T : new()
    {
        private static T _instance;
        public static T instance => _instance ??= new T();
    }
}