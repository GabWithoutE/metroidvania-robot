using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameObjectControl: MonoBehaviour {
	private GlobalGameObject globalGameObject;
	public GameObject destroyOnLoad;
	// Use this for initialization
	void Awake () {
		globalGameObject = GameObject.Find("GlobalGameObject").GetComponent<GlobalGameObject>();

	}
	
	public void PlayAgain(){
		globalGameObject.StartMainGame();
		if (destroyOnLoad != null){
			Destroy(destroyOnLoad);
		}
	}

	public void ReturnToTitleScreen(){
		globalGameObject.LoadTitleScreen();
		if (destroyOnLoad != null)
        {
			Destroy(destroyOnLoad);
        }
	}
}
