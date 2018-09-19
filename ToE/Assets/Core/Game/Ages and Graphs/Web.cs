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
    public Dictionary<int, Dictionary<int, ILevel>> groups;

    //Инициализация сетки
    public Web () {
        groups = new Dictionary<int, Dictionary<int, ILevel>> ();
        groups.Add (0, new Dictionary<int, ILevel> ());
    }

    public int LevelsCount = 0;

    public void AddLevel (ILevel level) {
        int levelGroup = FindPlaceForLevel (level);
        // Добавление уровня в сетку
        if (groups.Count <= levelGroup)
            groups.Add (levelGroup, new Dictionary<int, ILevel> ());
        groups[levelGroup].Add (groups[levelGroup].Count, level);
        LevelsCount++;
        // Перестроение потомков уровня
        MoveChildren (level, levelGroup);
    }

    private int FindPlaceForLevel (ILevel level) {
        int levelGroup = 0;
        for (int i = 0; i < groups.Count; i++)
            for (int j = 0; j < groups[i].Count; j++) {
                for (int k = 0; k < level.Parents.Count; k++) {
                    if (groups[i][j] == level.Parents[k])
                        if (levelGroup <= i) {
                            levelGroup = i + 1;
                        }
                }
            }
        return levelGroup;
    }

    private void MoveChildren (ILevel level, int parentGroup) {
        for (int i = 0; i <= parentGroup; i++) {
            for (int j = 0; j < groups[i].Count; j++) {
                for (int k = 0; k < groups[i][j].Parents.Count; k++) {
                    if (groups[i][j].Parents[k] == level) {
                        UnityEngine.Debug.Log ("Нашёл родителей для перестроения дерева");
                        if (groups.Count <= parentGroup + 1)
                            groups.Add (parentGroup + 1, new Dictionary<int, ILevel> ());
                        groups[parentGroup + 1].Add (groups[parentGroup + 1].Count, groups[i][j]);
                        groups[i].Remove (j);
                        MoveChildren (groups[parentGroup + 1][groups[parentGroup + 1].Count], parentGroup + 1);
                    }
                }
            }
        }
    }
    public Vector2 FindLevelPos (ILevel level) {
        Vector2 levelGroup = new Vector2 (0, 0);
        for (int i = 0; i < groups.Count; i++) {
            for (int j = 0; j < groups[i].Count; j++)
                if (groups[i][j] == level) {
                    levelGroup = new Vector2 (i, j);
                    return levelGroup;
                }
        }
        UnityEngine.Debug.Log (levelGroup);
        return levelGroup;
    }

    public void SortWeb () {
        for (int i = 1; i < groups.Count; i++) {
            Dictionary<float, ILevel> dic = new Dictionary<float, ILevel> ();

            for (int j = 0; j < groups[i].Count; j++) {
                float pos = FindLevelParents (groups[i][j]) * (float) groups[i].Count / (float) groups[i][j].Parents.Count;
                while (dic.ContainsKey (pos))
                    pos += Single.MinValue;
                dic.Add (pos, groups[i][j]);
            }
            int l = 0;
            foreach (var _level in dic) {
                groups[i][l] = _level.Value;
                l++;
            }
        }

    }

    private float FindLevelParents (ILevel level) {
        float pos = 0;
        for (int k = 0; k < level.Parents.Count; k++)
            for (int i = 0; i < groups.Count; i++)
                for (int j = 0; j < groups[i].Count; j++) {
                    if (level.Parents[k] == groups[i][j]) {
                        pos += (float) j / (float) groups[i].Count;

                    }
                }
        return pos;
    }
}