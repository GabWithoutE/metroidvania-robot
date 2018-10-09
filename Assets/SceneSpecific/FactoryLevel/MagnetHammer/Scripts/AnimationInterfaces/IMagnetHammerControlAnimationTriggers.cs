using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMagnetHammerAnimationTriggers {
	void Effect_TriggerDropping();
	void Effect_TriggerMagnetGrounded_NoEffect();
	void Effect_TriggerRaisingMagnet();
	void Effect_TriggerReset();

	void Mach_TriggerUnlockMachinery();
	void Mach_TriggerMagnetUp();
	void Mach_TriggerMagnetLockin();
	void Mach_TriggerReset();
	MagnetHammerMachineAnimationController.AnimationCatchupSubscription Mach_GetAnimationCatchupSubscription();
}
