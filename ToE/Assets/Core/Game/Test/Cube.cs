using Assets.Core.Game.Animations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // MenuAnimations.Rotation(this);
        this.transform.Rotate(Vector3.up * Time.deltaTime, Space.World);
    }
}
