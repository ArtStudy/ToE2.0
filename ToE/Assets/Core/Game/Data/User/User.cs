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
    public class User : IUser
    {
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


        public string CorrectName { get => SaveGame.GetValue(this, "User"); set => SaveGame.SetValue(this, value); }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
