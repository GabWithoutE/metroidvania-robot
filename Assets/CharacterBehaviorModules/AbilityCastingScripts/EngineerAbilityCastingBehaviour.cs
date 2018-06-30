using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerAbilityCastingBehaviour : AbstractAbilityCastingBehaviour {
	public int turretLimit;
	public int currentTurretNumber;
	Queue<GameObject> turretQueue;

	private void Awake()
	{
		base.Awake();
		currentTurretNumber = 0;
		turretQueue = new Queue<GameObject>();
	}

	/*
     * TODO: REGISTER TURRET KILLS FOR POINTS
     * REGISTER A TURRET LIMIT STATE
     * 
     */
	protected override void CastLightAttack(object castState){
		base.DefaultCastLightAttack(castState);
	}

	protected override void CastHeavyAttack(object castState){

		if (currentTurretNumber > turretLimit)
        {
            GameObject oldestTurret = turretQueue.Dequeue();
            Destroy(oldestTurret);
            currentTurretNumber -= 1;
            return;
        }
		//base.DefaultCastHeavyAttack(castState);
		if (!(bool) castState){
			return;
		}
		if (((float[]) stateObserver.GetCharacterStateValue(ConstantStrings.HEAVY_ATTACK_COOLDOWN))[1] >0 ){
			return;
		}

		GameObject turret = Instantiate(heavyAttack, abilitySpawnLocation, Quaternion.identity, transform);
		gameObject.transform.root.gameObject.BroadcastMessage("AbilityCasted", turret);
		turretQueue.Enqueue(turret);
		currentTurretNumber++;

	}

	protected override void CastUtilityAbility(object castState){
		if (!(bool) castState){
			return;
		}
		if (((float[])stateObserver.GetCharacterStateValue(ConstantStrings.UTILITY_COOLDOWN))[1] > 0)
		{
			return;
		}

		GameObject explosionTrigger = Instantiate(utilityAbility, transform.localPosition, Quaternion.identity, transform);
		gameObject.transform.root.BroadcastMessage("AbilityCasted", explosionTrigger);
		explosionTrigger.GetComponent<BlowUpTurretsForDamage>().BlowTurretsUpForDamage(turretQueue);
		currentTurretNumber = 0;

	}
}
