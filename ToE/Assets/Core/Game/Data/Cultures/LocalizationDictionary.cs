using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data.Cultures
{
    public class LocalizationDictionary : List<LocalizationKeyValuePair>
    {
      public string this[string key] {
            get => this.Find((item) => item.Key == key).Value;
            set
            {
                var result = this.Find((item) => item.Key == key);
                if (result == null)
                {
                    this.Add(new LocalizationKeyValuePair(key, value));
                }
                else
                {
                    result.Value = value;
                }
            
            }
        }
        public LocalizationDictionary()
        {

        }
        public LocalizationDictionary(Dictionary<string, string> dictionary)
        {
            for (int i = 0; i < dictionary.Count; i++)
            {
                var item = dictionary.ElementAt(i);
                this.Add(new LocalizationKeyValuePair(item.Key, item.Value));
            }
        }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            for(int i = 0; i < this.Count; i++)
            {
                result.Add(this[i]?.Key, this[i]?.Value);
            }
            return result;
        }

    }
}
