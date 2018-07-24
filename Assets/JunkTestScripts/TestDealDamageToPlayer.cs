using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDealDamageToPlayer : MonoBehaviour {
	public float damageAmount;

	// Update is called once per frame
	void Update () {
		
	}

	private void OnEnable()
	{
		transform.root.BroadcastMessage("TakeDamage", damageAmount);

	}
}
