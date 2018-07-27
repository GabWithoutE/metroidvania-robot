using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x + 8 * Time.deltaTime, transform.position.y, transform.position.z);
	}
}
