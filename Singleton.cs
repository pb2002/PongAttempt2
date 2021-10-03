namespace Pong
{
    /// <summary>
    ///  Generic singleton class
    /// </summary>
    /// <typeparam name="T">The inheriting class (e.g. Foo : Singleton&#60;Foo&#62;)</typeparam>
    public class Singleton<T> where T : new()
    {
        private static T instance; // hidden instance variable
        
        // get-only property automatically creates singleton instance upon being accessed
        public static T Instance => instance ??= new T();
    }
}