using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace SV22T1020142.Shop.AppCodes
{
    public static class SessionExtensions
    {
        public static void SetSessionData<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? GetSessionData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (string.IsNullOrEmpty(data))
                return default;

            return JsonSerializer.Deserialize<T>(data);
        }
    }
}