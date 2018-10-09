using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetHammerMachineAnimationController : MonoBehaviour, IMagnetHammerMachineryAnimationTriggers{
	private Animator hammerMachineAnimator;

	private const string unlockTrigger = "Unlock";
	private const string magnetUpTrigger = "MagnetUp";
	private const string magnetLockinTrigger = "MagnetLockin";
	private const string resetTrigger = "Reset";
	private AnimationCatchupSubscription subscription;

	public delegate void AnimationTiming();
	public event AnimationTiming onAnimationCaughtUp; 

	void Awake(){
		hammerMachineAnimator = GetComponent<Animator>();
		subscription = new AnimationCatchupSubscription(this);
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

	public void OnAnimationCaughtupEvent(){
		if (onAnimationCaughtUp == null){
			print ("boob");
		}else {
			onAnimationCaughtUp();
		}
	}

	public AnimationCatchupSubscription GetAnimationCatchupSubscription(){
		return subscription;
	}

	public class AnimationCatchupSubscription{
		private MagnetHammerMachineAnimationController controller;
		public AnimationCatchupSubscription(MagnetHammerMachineAnimationController controller){
			this.controller = controller;
		}
		public event AnimationTiming OnAnimationCaughtUp{
			add{
				controller.onAnimationCaughtUp += value;
			}
			remove{
				controller.onAnimationCaughtUp -= value;
			}
		}
	}
}
