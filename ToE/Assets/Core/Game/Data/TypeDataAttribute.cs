
using Assets.Core.ToePac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.Core.Game.Data
{
    public class TypeDataAttribute : Attribute
    {
        public FileTypes Type { get; }
        public TypeDataAttribute(FileTypes type)
        {
            this.Type = type;
        }
    }
}