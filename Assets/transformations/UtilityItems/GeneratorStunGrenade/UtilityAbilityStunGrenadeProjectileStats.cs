using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityAbilityStunGrenadeProjectileStats : AbstractAbilityStats {

	public float range;
	public float stunTime;
	public float projectileSpeed;

	public float GetRange(){
		return range;
	}

	public float GetStunTime(){
		return stunTime;
	}

	public float GetProjectileSpeed(){
		return projectileSpeed;
	}
}
