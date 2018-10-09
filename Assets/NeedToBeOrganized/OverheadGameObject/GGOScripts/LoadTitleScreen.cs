using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTitleScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<GlobalGameObject>().LoadTitleScreen();
	}
}
