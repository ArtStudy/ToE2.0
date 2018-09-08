using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data.Cultures
{
   public class LocalizationKeyValuePair 
    {
        public LocalizationKeyValuePair()
        {

        }
        public LocalizationKeyValuePair(String key, String value)
        {
            this.Key = key;
            this.Value = value;
        }

        public String Key { get; set; }
        public String Value { get; set; }

    }
}
