using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Core.Game.Data.Level;
using Assets.Core.LevelsStructureInterfaces;
using UnityEngine;

public class Web {
    public Dictionary<int, List<ILevel>> groups;

    //Инициализация сетки
    public Web () {
        groups = new Dictionary<int, List<ILevel>> ();
        groups.Add (0, new List<ILevel> ());
    }

    public int LevelsCount = 0;

    // Добавление нового уровня в сетку
    public void AddLevel (ILevel level) {
        int levelGroup = FindPlaceForLevel (level);
        // Добавление уровня в сетку
        if (groups.Count <= levelGroup)
            groups.Add (levelGroup, new List<ILevel> ());
        groups[levelGroup].Add (level);
        LevelsCount++;
        // Перестроение потомков уровня
        MoveChildren (level, levelGroup);
        //UnityEngine.Debug.Log ("Добавлен уровень в группу " + levelGroup);
    }

    // Сдвиг потомков добавленного уровня
    private void MoveChildren (ILevel level, int parentGroup) {
        for (int i = 0; i <= parentGroup; i++) {
            foreach (var _level in groups[i]) {
                for (int k = 0; k < _level.Parents.Count; k++) {
                    if (_level.Parents[k] == level) {
                        UnityEngine.Debug.Log ("Нашёл родителей для перестроение дерева");
                        if (groups.Count <= parentGroup + 1)
                            groups.Add (parentGroup + 1, new List<ILevel> ());
                        groups[parentGroup + 1].Add (_level);
                        groups[i].Remove (_level);
                        MoveChildren (_level, parentGroup + 1);

                    }
                }
            }
        }

    }

    // Поиск группы добавляемого уровня
    private int FindPlaceForLevel (ILevel level) {
        int levelGroup = 0;
        for (int i = 0; i < groups.Count; i++)
            foreach (var _level in groups[i]) {
                for (int k = 0; k < level.Parents.Count; k++) {
                    if (_level == level.Parents[k])
                        if (levelGroup <= i) {
                            levelGroup = i + 1;
                        }
                }
            }
        return levelGroup;
    }

    //Поиск номера группы уровня
    public int FindLevelGroup (ILevel level) {
        int levelGroup = 0;
        for (int i = 0; i < groups.Count; i++) {
            foreach (var _level in groups[i])
                if (_level == level) {
                    levelGroup = i;
                    return i;
                }
        }
        UnityEngine.Debug.Log (levelGroup);
        return levelGroup;
    }

}