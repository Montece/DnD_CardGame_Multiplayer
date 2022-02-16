using Newtonsoft.Json;

public static class Helper
{
    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

    public static bool IsType<T>(object obj)
    {
        if (obj != null && obj.GetType().Equals(typeof(T))) return true;
        return false;
    }
}
