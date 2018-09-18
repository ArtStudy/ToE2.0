using Assets.Core.Volutes;
using Assets.Core;
public class Character
{
    /// <summary>
    /// Номер персонажа
    /// </summary>
    int ID { get; }
    /// <summary>
    /// Имя персонажа
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Здоровье персонажа
    /// </summary>
    int Health { get; }
    /// <summary>
    /// Деньги 
    /// </summary>
    Money Cash { get; }

    /// <summary>
    /// Тело персонажа
    /// </summary>
    Look Body { get; }

    /// <summary>
    /// Уровень персонажа
    /// </summary>
    int Tier { get; }
}
