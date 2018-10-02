using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoadMainGame : MonoBehaviour {

	// Use this for initialization
	void Start() {
        GetComponent<GlobalGameObject>().StartMainGame();
	}
}
