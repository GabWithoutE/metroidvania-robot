using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetHammerAnimationController : IMagnetHammerAnimationTriggers {
	private IMagnetHammerEffectAnimationTriggers[] effectAnimationInterfaces;
	private IMagnetHammerMachineryAnimationTriggers machineryAnimationInterface; 
	public MagnetHammerAnimationController(IMagnetHammerEffectAnimationTriggers[] effectAnimationInterfaces,
		IMagnetHammerMachineryAnimationTriggers machineryAnimationInterface){
		this.effectAnimationInterfaces = effectAnimationInterfaces;
		this.machineryAnimationInterface = machineryAnimationInterface;
	}

	public void Effect_TriggerDropping(){
		foreach(IMagnetHammerEffectAnimationTriggers trig in effectAnimationInterfaces){
			trig.TriggerDroppingEffect();
		}
	}
	public void Effect_TriggerMagnetGrounded_NoEffect(){
		foreach(IMagnetHammerEffectAnimationTriggers trig in effectAnimationInterfaces){
			trig.TriggerMagnetGrounded();
		}
	}
	public void Effect_TriggerRaisingMagnet(){
		foreach(IMagnetHammerEffectAnimationTriggers trig in effectAnimationInterfaces){
			trig.TriggerRaisingMagnet();
		}
	}
	public void Effect_TriggerReset(){
		foreach(IMagnetHammerEffectAnimationTriggers trig in effectAnimationInterfaces){
			trig.TriggerReset();
		}
	}
	public void Mach_TriggerUnlockMachinery(){
		machineryAnimationInterface.TriggerUnlockMachinery();
	}
	public void Mach_TriggerMagnetUp(){
		machineryAnimationInterface.TriggerRaiseMagnet();
	}
	public void Mach_TriggerMagnetLockin(){
		machineryAnimationInterface.TriggerMagnetLockin();
	}
	public void Mach_TriggerReset(){
		machineryAnimationInterface.TriggerReset();
	}

	public MagnetHammerMachineAnimationController.AnimationCatchupSubscription Mach_GetAnimationCatchupSubscription(){
		return machineryAnimationInterface.GetAnimationCatchupSubscription();
	}
	
}
