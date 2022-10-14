using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace pous.Models
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static List<Data> GetObject(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(List<Data>) : JsonConvert.DeserializeObject<List<Data>>(value);
        }
    }
}
