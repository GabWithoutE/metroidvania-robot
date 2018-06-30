using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectileCollisionSender : MonoBehaviour {
    
	private SpellStats spellStats;

	void Awake(){
		spellStats = GetComponent<SpellStats>();
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Enemy"){
			col.transform.root.BroadcastMessage("HitBy", transform.root.gameObject);
			col.gameObject.BroadcastMessage("TakeDamage", spellStats.GetSpellDamage());
			transform.root.BroadcastMessage("EnemyHit", (bool)col.gameObject.GetComponent<ICharacterStateObserver>().GetCharacterStateValue(ConstantStrings.DEATH_STATE));
			Destroy(gameObject);
		}
	}

	private void OnDestroy()
	{
		//OnKilledEnemy = null;
	}
}
