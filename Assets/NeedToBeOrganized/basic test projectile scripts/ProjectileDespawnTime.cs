using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDespawnTime : MonoBehaviour {
	public float despawnTime;

	// Use this for initialization
	void Start () {
		
	}

    public float GetRemainingTime () {
        return despawnTime;
    }
	
	// Update is called once per frame
	void Update () {
		despawnTime -= Time.fixedDeltaTime;
		if (IsTimeToDespawn()) {
			Destroy (gameObject);
		}
	}

	private bool IsTimeToDespawn(){
		return despawnTime <= 0;
	}
}
