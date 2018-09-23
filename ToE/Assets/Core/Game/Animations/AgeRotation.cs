using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgeRotation : MonoBehaviour {

	private Vector3 mousePosition;
	private Vector3 deltaRotation;
	public float moveSpeed = 20000f;
	private float X, Y, Z;
	void Update () {
		if (Input.GetMouseButton (0)) {

			X += Input.GetAxis ("Mouse X") * moveSpeed * Time.deltaTime;
			Y += Input.GetAxis ("Mouse Y") * Time.deltaTime;
			if (Input.GetKey (KeyCode.Q))
				Z += 1f * moveSpeed * Time.deltaTime;
			if (Input.GetKey (KeyCode.E))
				Z -= 1f * moveSpeed * Time.deltaTime;

			transform.rotation = Quaternion.Euler (Y, X, Z);
		}
	}
}