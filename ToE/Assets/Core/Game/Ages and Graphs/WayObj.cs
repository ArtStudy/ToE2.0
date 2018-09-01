using Assets.Core.Game.Ages_and_Graphs;
using Assets.Core.Levels;
using Assets.Core.LevelsStructureInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayObj : MonoBehaviour {

    public LevelObj ParentLevel { get; set; }
    public LevelObj ChildLevel { get; set; }

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        WayDrawer.DrawWay(this);
    }

}
