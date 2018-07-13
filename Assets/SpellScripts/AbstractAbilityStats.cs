using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAbilityStats : MonoBehaviour {

	public float damage;
	public float cooldownTime;
    
	public float GetCooldownTime(){
		return cooldownTime;
	}
	public float GetDamage(){
		return damage;
	}
}
