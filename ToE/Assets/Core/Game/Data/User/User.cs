
using Assets.Core.Game.Data;
using Assets.Core.ToePac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Assets.Core.Game.Data.User
{
    /// <summary>
    /// Класс Пользователя
    /// </summary>
    [TypeDataAttribute(FileTypes.User)]
    public class User : IUser, ISaveData
    {
        string _CorrectName;
        public ulong ID
        {
            get
            {
                return SaveGame.IDUserData;
            }
            set
            {

            }
        }
        public string Name
        {
            get
            {
                return Transliteration.Back(CorrectName);
            }
            set
            {

            }
        }


       public string CorrectName
        {
            get => _CorrectName;
            set
            {
                _CorrectName = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CorrectName"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
