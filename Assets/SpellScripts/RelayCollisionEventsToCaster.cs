using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelayCollisionEventsToCaster : MonoBehaviour {
	private GameObject myCaster;

	public void SetCaster(GameObject caster){
		myCaster = caster;
	}

	public void EnemyKilledType(string enemyType)
    {
		myCaster.BroadcastMessage("EnemyKilledType", enemyType);
    }

	public void EnemyKilledScore(float scoreAmount){
		myCaster.BroadcastMessage("EnemyKilledScore", scoreAmount);
	}
    
}
