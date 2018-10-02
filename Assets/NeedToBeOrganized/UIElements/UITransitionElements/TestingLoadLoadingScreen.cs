using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingLoadLoadingScreen : MonoBehaviour {
	public GameObject loadingScreen;
	// Use this for initialization
	void Start () {
		GameObject globalObject = GameObject.Find("GlobalGameObject") as GameObject;
		globalObject.GetComponentInChildren<GlobalGameObject>().DisplayUIElement(loadingScreen);
		//Instantiate(loadingScreen);

	}

}
 