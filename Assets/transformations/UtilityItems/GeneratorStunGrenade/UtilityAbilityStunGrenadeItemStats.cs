using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityAbilityStunGrenadeItemStats : AbstractAbilityStats {
    /*
     * Amount of charge dispelled per unit of time.
     */ 
	public float resourceDechargeRateInRatio;
    /*
     * Amount of charge per each second of movement.
     */ 
	public float resourceChargeRateInRatio;

	public float resourceChargePerHitInRatio;
    

	public float GetResourceDechargeRate(){
		return resourceDechargeRateInRatio;
	}

	public float GetResourceChargeRate(){
		return resourceChargeRateInRatio;
	} 

	public float GetResourceChargePerHit(){
		return resourceChargePerHitInRatio;
	}


}
