using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Core.Game.Ages_and_Graphs;
using Assets.Core.ToePac;
using Assets.Core.Game.Data;
using Assets.Core.Levels;
using Assets.Core.Save;
using Assets.Core.Game.Sorting;

public class AgeObj : MonoBehaviour
{
    public Age Age;
    public GameObject LevelPrefab;
    public GameObject WayPrefab;
    private Save save;
    private List<GameObject> Levels = new List<GameObject>();
    private List<LevelObj> LevelsObj = new List<LevelObj>();
    private List<GameObject> Ways = new List<GameObject>();

    void Start ()
    {
        LoadLevels();
       // TestWay();
    }
	
	void Update ()
    {
        
    }

    void LoadLevels()
    {
        var levels = Game.gameData.Levels;
        for (int i = 0; i < levels.Count; i++) 
        {

            GameObject level =  Instantiate(LevelPrefab);
            level.transform.parent = this.transform;
            LevelObj levelObj = level.GetComponent<LevelObj>();
            levelObj.level = levels[i];
            LevelsObj.Add(levelObj);
            if (i==0)
                level.transform.position = SortLevels.randomFirstLevelPos(this.transform.localScale.x / 2);
            else
            {
                level.transform.position = SortLevels.randomFirstLevelPos(this.transform.localScale.x / 2);
            }
            Levels.Add(level);
        }
        for(int i = 0; i < LevelsObj.Count; i ++)
        {
            LevelObj levelObj = LevelsObj[i];
             for (int j = 0; j < levelObj.level.Parents.Count; j++)
            {
                GameObject way = Instantiate(WayPrefab);
                way.transform.parent = this.transform;
                WayObj wayobj = way.GetComponent<WayObj>();
                wayobj.ParentLevel = LevelsObj.Find((item) => item.GetComponent<LevelObj>().level == levelObj.level.Parents[j]) ;
                wayobj.ChildLevel = levelObj;
                Ways.Add(way);
            }
        }
    }

    void TestWay()
    { 
        Ways[0] = Instantiate(Ways[0]);
        Ways[0].transform.parent = this.transform;
        WayObj way = Ways[0].GetComponent<WayObj>();
        way.ParentLevel = Levels[0].GetComponent<LevelObj>();
        way.ChildLevel = Levels[1].GetComponent<LevelObj>();
        WayObj testWay = Ways[0].GetComponent<WayObj>();        
    }

}
