using Assets.Core.ToePac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data.Level
{
    [TypeDataAttribute(FileTypes.SaveData)]
    public class SaveLevelData : IBase
    {
        public ulong ID { get => SaveGame.IDLevelData; set { } }
        public string Name { get => "SaveLevelData"; set { } }

        public Dictionary<ulong, StateLevel> StateLevel { get; } = new Dictionary<ulong, StateLevel>();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
