public class Singleton<T> where T : class, new()
{
    private static T _instance;
    private static object obj = new object();

    public static T GetInstance()
    {
        if (_instance == null)
        {
            lock (obj)
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
            }
        }
        return _instance;

    }
}

