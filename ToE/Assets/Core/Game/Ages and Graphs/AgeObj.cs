using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Core.Game.Ages_and_Graphs;

public class AgeObj : MonoBehaviour
{
    public Age Age;
    public List<GameObject> Levels;

    public SortLevels sortLevels;
    public Vector3 levelPos;

    void Start ()
    {


        sortLevels = new SortLevels();
        LoadLevels();
        sortLevels.Sorting();
	}
	
	void Update ()
    {

    }


    

    void LoadLevels()
    {

        /*   var r = GameObject.Find("Level");
           Levels.Add(GameObject.Find("Level"));
           Levels.Add(GameObject.Find("Level"));
           Levels.Add(GameObject.Find("Level"));

           Levels.Add(GameObject.Find("Level"));
           Levels.Add(GameObject.Find("Level"));
           Levels.Add(GameObject.Find("Level"));
           Levels.Add(GameObject.Find("Level"));
           Levels.Add(GameObject.Find("Level"));
           Levels.Add(GameObject.Find("Level"));
           Levels.Add(GameObject.Find("Level"));
           Levels.Add(GameObject.Find("Level"));
           Levels.Add(GameObject.Find("Level"));
           Levels.Add(GameObject.Find("Level"));*/

        var level = new GameObject("Level");
        level.AddComponent<SphereCollider>();
        level.AddComponent<MeshRenderer>();
        level.AddComponent<MeshFilter>();
        level.AddComponent<LevelObj>();

        Levels.Add(level);


         for (int i = 0; i < Levels.Count; i++) 
        {
            Levels[i] = Instantiate(Levels[i]);
            Levels[i].transform.parent = this.transform;
            Levels[i].transform.position = sortLevels.randomFirstLevelPos(this.transform.localScale.x / 2);
           // Debug.Log(Levels[i].transform.parent.name);
        }
        //sortLevels.StartSorting();
    }
}
