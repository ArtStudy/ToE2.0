using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Core.Game.Ages_and_Graphs;
using UnityEngine.UI;

public class AgeObj : MonoBehaviour
{
    public Age Age;
    public List<GameObject> Levels;
    public GameObject prefabLevel;
    public SortLevels sortLevels;
    public Vector3 levelPos;
    public Canvas renderCanvas;

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
        Resources.

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


         for (int i = 0; i < 5; i++) 
        {
            var r = Instantiate(this.prefabLevel);
    
       //     Text tx =  (Text)r.GetComponent(typeof(Text));
         
       
      //      tx.text = i.ToString();
            Levels.Add(r);
            r.transform.parent = this.transform;
            r.transform.position = sortLevels.randomFirstLevelPos(this.transform.localScale.x / 2);
           // Debug.Log(Levels[i].transform.parent.name);
        }
        //sortLevels.StartSorting();
    }
}
