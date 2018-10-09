using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMagnetHammerMachineryAnimationTriggers {
    void TriggerUnlockMachinery();
    void TriggerRaiseMagnet();
    void TriggerMagnetLockin();
    void TriggerReset();	
    MagnetHammerMachineAnimationController.AnimationCatchupSubscription GetAnimationCatchupSubscription();
}

