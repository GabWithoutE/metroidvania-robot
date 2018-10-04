using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetHammerMachineAnimationController : MonoBehaviour, IMagnetHammerMachineryAnimationTriggers{
	private Animator hammerMachineAnimator;

	private const string unlockTrigger = "Unlock";
	private const string magnetUpTrigger = "MagnetUp";
	private const string magnetLockinTrigger = "MagnetLockin";
	private const string resetTrigger = "Reset";


	void Awake(){
		hammerMachineAnimator = GetComponent<Animator>();
	}

    public void TriggerUnlockMachinery(){
		hammerMachineAnimator.SetTrigger(unlockTrigger);
	}
    public void TriggerRaiseMagnet(){
		hammerMachineAnimator.SetTrigger(magnetUpTrigger);
	}
    public void TriggerMagnetLockin(){
		hammerMachineAnimator.SetTrigger(magnetLockinTrigger);
	}
    public void TriggerReset(){
		hammerMachineAnimator.SetTrigger(resetTrigger);
	}
}
