using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyMeleeAbilityStats : MeleeAbilityStats
{

	public float xMaxScaleFactor;
	public float yMaxScaleFactor;
	public float damageMaxScaleFactor;
	public float heavyAttackChargeTime;

	public float GetXMaxScaleFactor()
	{
		return xMaxScaleFactor;
	}

	public float GetYMaxScaleFactor(){
		return yMaxScaleFactor;
	}
	public float GetDamageMaxScaleFactor(){
		return damageMaxScaleFactor;
	}
	public float GetHeavyAttackChargeTime(){
		return heavyAttackChargeTime;
	}
    
}
