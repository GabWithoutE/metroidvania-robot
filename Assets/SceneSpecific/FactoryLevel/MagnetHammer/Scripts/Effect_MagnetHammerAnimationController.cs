using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_MagnetHammerAnimationController : MonoBehaviour, IMagnetHammerEffectAnimationTriggers{
	private Animator effectHammerMachineAnimator;

	private const string droppingEffectTrigger = "Dropping Effect"; 
	private const string groundedTrigger = "Grounded";
	private const string raisingTrigger = "Raising";
	private const string resetTrigger = "Reset";


	void Awake(){
		effectHammerMachineAnimator = GetComponent<Animator>();
	}

	public void TriggerDroppingEffect(){
		effectHammerMachineAnimator.SetTrigger(droppingEffectTrigger);
	}
	public void TriggerMagnetGrounded(){
		effectHammerMachineAnimator.SetTrigger(groundedTrigger);
	}
	public void TriggerRaisingMagnet(){
		effectHammerMachineAnimator.SetTrigger(raisingTrigger);
	}
	public void TriggerReset(){
		effectHammerMachineAnimator.SetTrigger(resetTrigger);
	}
}
