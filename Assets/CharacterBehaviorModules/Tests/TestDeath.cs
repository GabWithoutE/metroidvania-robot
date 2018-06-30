using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDeath : MonoBehaviour {
	private bool damageDealt = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!damageDealt){
			GetComponentInParent<HealthState>().TakeDamage(100);
			damageDealt = true;
		}
	}
}
