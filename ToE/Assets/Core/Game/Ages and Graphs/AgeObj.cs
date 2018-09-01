using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Core.Game.Ages_and_Graphs;

public class AgeObj : MonoBehaviour
{
    public Age Age;
    public List<GameObject> Levels;
    public List<GameObject> Ways;

    void Start ()
    {
        SortLevels.Sorting();
        LoadLevels();
        TestWay();
    }
	
	void Update ()
    {
        
    }

    void LoadLevels()
    {     
        for (int i = 0; i < Levels.Count; i++) 
        {
            Levels[i] = Instantiate(Levels[i]);
            Levels[i].transform.parent = this.transform;
            Levels[i].transform.position = SortLevels.randomFirstLevelPos(this.transform.localScale.x / 2);
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
