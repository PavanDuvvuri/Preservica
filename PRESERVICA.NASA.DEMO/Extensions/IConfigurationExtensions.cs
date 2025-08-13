using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PRESERVICA.NASA.DEMO.Extensions
{
    public static class IConfigurationExtensions
    {
        public static string BaseUrl(this IConfiguration config) => GetConfigValue<string>(config, "BaseUrl");

        public static string APIHost(this IConfiguration config) => GetConfigValue<string>(config, "APIHost");

        public static string APIKey(this IConfiguration config) => GetConfigValue<string>(config, "APIKey");

        public static T GetConfigValue<T>(this IConfiguration config, string key)
        {   
             ArgumentNullException.ThrowIfNull(config, nameof(config)); 
                ArgumentNullException.ThrowIfNull(key, nameof(key));    
            var value = config[key];
            if (value == null)
            {
                throw new KeyNotFoundException($"Configuration key '{key}' not found.");
            }
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException($"Cannot convert configuration value '{value}' to type '{typeof(T).Name}'.");
            }
        }
    }
}
