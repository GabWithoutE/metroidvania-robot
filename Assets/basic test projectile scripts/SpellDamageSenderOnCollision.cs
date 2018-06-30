using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpellStats))]
public class SpellDamageSender : MonoBehaviour {
	private float damageAmount;
	void Start(){
		damageAmount = GetComponent<SpellStats> ().GetSpellDamage ();
	}

	void OnTriggerEnter2D(Collider2D col){
		col.SendMessage ("TakeDamage", damageAmount);
	}
}
