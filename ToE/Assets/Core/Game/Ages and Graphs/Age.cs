﻿using Assets.Core.Ages;
using Assets.Core.Volutes;
using System.Collections.Generic;
using Assets.Core.Levels;
using Assets.Core.Game.Sorting;

public class Age : IAges
{
    // Реализация интерфейса
    public string Name { get; set; }
    public int ID { get; set; }
    public Money Price { get; set; }
    public List<ILevel> Levels { get; }
    public bool Availability { get; set; }
    public Web web { get; set; }

}
