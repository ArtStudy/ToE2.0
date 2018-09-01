using Assets.Core.Game.Ages_and_Graphs;
using Assets.Core.Levels;
using Assets.Core.LevelsStructureInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayObj : MonoBehaviour {

    public LevelObj Parent { get; }
    public LevelObj Child { get; }

    // Use this for initialization
    void Start ()
    {
        WayDrawer.DrawWay(this);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
