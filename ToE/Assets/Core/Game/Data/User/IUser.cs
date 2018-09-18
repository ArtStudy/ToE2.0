using Assets.Core.Game.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data.User
{
    public interface IUser : IBase
    {
        string  CorrectName { get; set; }
    }
}
