using Assets.Core.Levels;
using Assets.Core.LevelsStructureInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayUI : MonoBehaviour, IWay {

    public ILevel Parent { get; }
    public ILevel Child { get; }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
