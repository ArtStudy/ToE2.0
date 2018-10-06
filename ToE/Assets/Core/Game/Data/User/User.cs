
using Assets.Core.BindingData;
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
    public class User : ChangeNotifier, IUser, ISaveData
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


       public string CorrectName
        {
            get => SaveGame.GetValue(this, string.Empty);
            set
            {
                this.SetProperty(() => SaveGame.SetValue(this, value));
              
            }
        }

    
    }
}
