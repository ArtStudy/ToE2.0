using Assets.Core.ToePac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core.Game.Data.UI
{
    [TypeDataAttribute(FileTypes.TextStyle)]
    public class TextStyle : ITextStyle
    {


        public UInt64 ID { get; set; }
        public string Name { get; set; }
        public int FontSize { get; set; }
        public Font Font { get; set; }
        public FontStyle FontStyle { get; set; }
    
    }
}
