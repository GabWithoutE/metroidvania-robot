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

public class MagnetHammer : AbstractSynchronizableEnvironmentPiece {
	public bool Ready = true;

	// Trigger multiple magnet effects at the same time (store them here)
	private IMagnetHammerEffectAnimationTriggers[] effectAnimationTriggerInterfaces;
	private IMagnetHammerMachineryAnimationTriggers machineryAnimationTriggerInterface;
	private HammerFallController hammerFallController;
	private MagnetHammerAnimationController animationController;

	void Awake(){
		effectAnimationTriggerInterfaces = 
			GetComponentsInChildren<IMagnetHammerEffectAnimationTriggers>();
		print (effectAnimationTriggerInterfaces.Length);
		print(effectAnimationTriggerInterfaces);
		machineryAnimationTriggerInterface =
			GetComponentInChildren(typeof(IMagnetHammerMachineryAnimationTriggers)) as
			IMagnetHammerMachineryAnimationTriggers;
		hammerFallController =
			GetComponentInChildren<HammerFallController>();
		animationController = new MagnetHammerAnimationController(effectAnimationTriggerInterfaces, machineryAnimationTriggerInterface);
	}

	public IMagnetHammerAnimationTriggers AnimationController{
		get{
			return (IMagnetHammerAnimationTriggers) animationController;
		}
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

	// Interface Implementations
	// Begin the hammer drop cycle
	public override void BeginAction(){
		UnlockHammer();
		Ready = false;
	}

	public override float ActionCycleTime(){
		return 0;
	}
	
	
	private void UnlockHammer(){
		// machineryAnimationTriggerInterface.TriggerUnlockMachinery();
		hammerFallController.BeginObstacleAction();
	}
}
