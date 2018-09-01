using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Core.User
{
    /// <summary>
    /// Класс Пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// Номер пользователя
        /// </summary>
        int ID { get; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Email пользователя
        /// </summary>
        string Email { get; }
        /// <summary>
        /// Пароль
        /// </summary>
        string Password { get; }

        /// <summary>
        /// Персонаж
        /// </summary>
        Character UserCharacter { get; }
    }
}
