using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace NP.Helpers.Web
{
    public static class SessionExtensions
    {
        /// <summary>
        /// Get a value from the Session storage
        /// </summary>
        /// <typeparam name="T">A class</typeparam>
        /// <param name="session">Interface to the session object</param>
        /// <param name="key">The key for the value</param>
        /// <returns>The value retrieved</returns>
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Store value in the S storageession
        /// </summary>
        /// <typeparam name="T">A class</typeparam>
        /// <param name="session">Interface to the session object</param>
        /// <param name="key">The key for the value</param>
        /// <param name="value">The value to store</param>
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

    }
}
