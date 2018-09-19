using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data.Inventor
{
    public class InventoryItem : IInventoryItem
    {
        private string _name;

        public Enhancements ImprovingHealth { get; set; } =  new Enhancements();

        public Enhancements ImproveResponseTime  { get; set; } = new Enhancements();

        public UInt64 ID { get; set; }
        public string Name
        {
            get => _name; set
            {
                _name = value;
                if (String.IsNullOrWhiteSpace(TranslationIdentifier))
                {
                    TranslationIdentifier = "InventoryItem." + value;
                }
            }
        }

        public string TranslationIdentifier { get; set; }

        public string[] BasicLocalizationFields => new string[] { "Name", "Description" };

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
