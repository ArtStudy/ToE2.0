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
    public List<GameObject> Levels = new List<GameObject> ();
    private List<LevelObj> LevelsObj = new List<LevelObj> ();
    private List<GameObject> Ways = new List<GameObject> ();

    void Awake () {
        Age = new Age ();
    }

    void Start () {
        CreateWeb ();
        WebDrawer.ResizeAge (this);
        LoadLevels ();
    }

    void Update () {

    }

    void LoadLevels () {
        var levels = GameData.Default.Levels;
        for (int i = 0; i < levels.Count; i++) {
            GameObject level = Instantiate (LevelPrefab);
            level.name = levels[i].Name;
            level.transform.parent = this.transform;
            LevelObj levelObj = level.GetComponent<LevelObj> ();
            levelObj.level = levels[i];
            LevelsObj.Add (levelObj);
            WebDrawer.DrawLevel (level, this);
            Levels.Add (level);
        }
        for (int i = 0; i < LevelsObj.Count; i++) {
            LevelObj levelObj = LevelsObj[i];
            for (int j = 0; j < levelObj.level.Parents.Count; j++) {
                GameObject way = Instantiate (WayPrefab);
                way.transform.parent = this.transform;
                WayObj wayobj = way.GetComponent<WayObj> ();
                wayobj.ParentLevel = LevelsObj.Find ((item) => item.GetComponent<LevelObj> ().level == levelObj.level.Parents[j]);
                wayobj.name = wayobj.ParentLevel.level.Name + " - " + levelObj.level.Name;
                wayobj.ChildLevel = levelObj;
                Ways.Add (way);
            }
        }
    }

    void TestWay () {
        Ways[0] = Instantiate (Ways[0]);
        Ways[0].transform.parent = this.transform;
        WayObj way = Ways[0].GetComponent<WayObj> ();
        way.ParentLevel = Levels[0].GetComponent<LevelObj> ();
        way.ChildLevel = Levels[1].GetComponent<LevelObj> ();
        WayObj testWay = Ways[0].GetComponent<WayObj> ();
    }

    void CreateWeb () {
        Age.Web = new Web ();
        var levels = GameData.Default.Levels;
        for (int i = 0; i < levels.Count; i++) {

            //Кодом ниже я тестировал рабосу сохранений для файлов данных, пожалуйста не убирай
            /*   Debug.Log("1: "  + levels[i].StateLevel.ToString());
               levels[i].StateLevel = Assets.Core.Game.Data.Level.StateLevel.Boss;
               Debug.Log("2: " + levels[i].StateLevel.ToString());   
               */
            Age.Web.AddLevel (levels[i]);
        }
        Age.Web.SortWeb ();
    }
}