using System.Text.Json;

namespace Utils
{
    public class JsonUtil
    {
        public static string WriteJsonItem<T>(T item)
        {
            var option = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
            };
            string json = JsonSerializer.Serialize(item, option);
            return json;
        }

        public static T ReadJsonItem<T>(string json)
        {
            var option = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
            };
            T item = JsonSerializer.Deserialize<T>(json, option);
            return item;
        }
    }
}
