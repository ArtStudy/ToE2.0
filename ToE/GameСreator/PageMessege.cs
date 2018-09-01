using GameСreator.API.Boss;
using GameСreator.ToePac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameСreator
{
    public class NavigatorPageMessege
    {
        public String NamePage { get;  }
        public NavigatorPageMessege(string name) => NamePage = name;
    }

    public class MessegeResourse<T>
    {
        public T Value { get; }
        public ActionItem Action { get; }
        public MessegeResourse(T value, ActionItem action)
        {
            Value = value;
            Action = action;
        }
    }

    public enum ActionItem
    {
        Add
    }
}
