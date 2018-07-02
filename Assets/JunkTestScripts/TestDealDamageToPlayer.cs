using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDealDamageToPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.root.BroadcastMessage("TakeDamage", 60);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
