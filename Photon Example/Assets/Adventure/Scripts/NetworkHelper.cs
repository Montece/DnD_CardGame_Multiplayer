using Newtonsoft.Json;
using System.Text;

public static class NetworkHelper
{
    public const byte CODE_UI_SHOW_REWARD = 0;
    public const byte CODE_UI_SHOW_INVENTORY = 1;

    public static byte[] Serialize(object obj)
    {
        return Encoding.Default.GetBytes(JsonConvert.SerializeObject(obj, Formatting.None, Helper.Settings));
    }

    public static object Deserialize(byte[] data)
    {
        return JsonConvert.DeserializeObject(Encoding.Default.GetString(data), Helper.Settings);
    }
}
