using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * TODO:
 * 1. trigger the unlock and drop the hammer. keep track of starting height
 * 2. detect when the hammer hits the ground. delay for a couple of seconds, then raise the hammer
 *    triggering the necessary things.
 * 3. when the hammer reaches the original height, trigger the lockin animation
 * 
 */ 

public class MagnetHammerControl : AbstractSynchronizableEnvironmentPiece{
	public bool Ready = true;

	// Trigger multiple magnet effects at the same time (store them here)
	private IMagnetHammerEffectAnimationTriggers[] effectAnimationTriggerInterfaces;
	private IMagnetHammerMachineryAnimationTriggers machineryAnimationTriggerInterface;

	/*
	 * Begin the hammer drop cycle
	 */
	public override void BeginAction(){
		UnlockHammer();
	}


	public override float ActionCycleTime(){
		return 0;
	}

	private void UnlockHammer(){
		machineryAnimationTriggerInterface.TriggerUnlockMachinery();
	}

	void Awake(){
		effectAnimationTriggerInterfaces = 
			GetComponentsInChildren(typeof(IMagnetHammerEffectAnimationTriggers)) as
			IMagnetHammerEffectAnimationTriggers[];
		machineryAnimationTriggerInterface =
			GetComponentInChildren(typeof(IMagnetHammerMachineryAnimationTriggers)) as
			IMagnetHammerMachineryAnimationTriggers;
	}

	/**
	 * if no synchronizationController is assigned, it means this is a standalone
	 *	piece that has no synchronization.
	 */
	void Start () {
		if (synchronizationController == null){
			BeginAction();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
