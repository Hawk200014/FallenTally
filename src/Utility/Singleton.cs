using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallenTally.Utility
{
    public class Singleton
    {
        private static Singleton instance = null;

        private Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();

        public Singleton()
        {
            instance = this;
        }

        #region Getters
        public static Singleton GetInstance()
        {
            if (instance is null)
            {
                instance = new Singleton();
            }
            return instance;
        }

        public string[] GetKeys()
        {
            return keyValuePairs.Keys.ToArray();
        }

        public object GetValue(string key)
        {
            if (keyValuePairs.ContainsKey(key))
            {
                return keyValuePairs[key];
            }
            return null;
        }

        #endregion
        #region Setters



        #endregion

        public void Add(string key, object value)
        {
            if (keyValuePairs.ContainsKey(key))
            {
                keyValuePairs[key] = value;
            }
            else
            {
                keyValuePairs.Add(key, value);
            }
        }
    }
}
