using Assets.Core.ToePac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data.User
{
    [TypeDataAttribute(FileTypes.SaveData)]
    public class SaveUserData : IBase
    {
        public ulong ID { get => SaveGame.IDUserData; set { } }
        public string Name { get => "SaveserData"; set { } }

        public Dictionary<ulong, string> CorrectName { get; } = new Dictionary<ulong, string>();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
