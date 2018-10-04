using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Core.Game.Ages_and_Graphs;
using Assets.Core.Game.Data;
using Assets.Core.Game.Data.Age;
using Assets.Core.Game.Sorting;
using Assets.Core.Game.Sorting;
using Assets.Core.Levels;
using Assets.Core.Save;
using Assets.Core.ToePac;
using UnityEngine;

public class AgeObj : MonoBehaviour {
    public Age Age;
    public GameObject LevelPrefab;
    public GameObject WayPrefab;
    private Save save;
    public Dictionary<int, GameObject> Levels = new Dictionary<int, GameObject> ();
    private List<GameObject> Ways = new List<GameObject> ();

    void Awake () {
        Age = new Age ();
        Age.FillGrid (GameData.Default.Levels);
    }

    void Start () {

        LoadLevels ();
    }

    void Update () {

    }

    void LoadLevels () {
        for (int i = 0; i < Age.levelsGrid.levels.Count; i++) {
            GameObject level = Instantiate (LevelPrefab, FindLevelPos (i), Quaternion.identity, this.transform);
            level.name = Age.levelsGrid.levels[i].Name;
            LevelObj levelObj = level.GetComponent<LevelObj> ();
            levelObj.level = Age.levelsGrid.levels[i];
            Levels.Add (i, level);
        }

        for (int i = 0; i < Age.levelsGrid.levels.Count; i++) {
            for (int j = 0; j < Age.levelsGrid.levels[i].Parents.Count; j++) {
                GameObject way = Instantiate (WayPrefab);
                way.transform.parent = this.transform;
                WayObj wayobj = way.GetComponent<WayObj> ();
                wayobj.childPos = Levels[i].transform.position;
                for (int k = 0; k < Levels.Count; k++) {
                    if (Levels[k].GetComponent<LevelObj> ().level == Levels[i].GetComponent<LevelObj> ().level.Parents[j])
                        wayobj.parentPos = Levels[k].transform.position;
                }
                Ways.Add (way);
            }
        }
    }

    Vector3 FindLevelPos (int i) {

        Vector3 pos = Age.levelsGrid.levelsPositions[i] * LevelPrefab.transform.localScale.x;
        return pos;
    }
}