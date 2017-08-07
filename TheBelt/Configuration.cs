using System;
using System.Collections.Generic;
using System.Text;

namespace TheBelt
{
    public class Configuration
    {
        private Dictionary<string, string> Config { get; } = new Dictionary<string, string>();

        public string GetConfigValue(string key) => Config[key];

        public void SetConfigValue(string key, string value) => Config[key] = value;

        public string this[string key]
        {
            get { return GetConfigValue(key); }
            set { SetConfigValue(key, value); }
        }
    }
}
