using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgeUI : MonoBehaviour
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
        for (int i = 0; i < Levels.Count; i++) 
        {
            Levels[i] = Instantiate(Levels[i]);
            Levels[i].transform.parent = this.transform;
            Levels[i].transform.position = sortLevels.randomFirstLevelPos(this.transform.localScale.x / 2);
        }
        //sortLevels.StartSorting();
    }
}
