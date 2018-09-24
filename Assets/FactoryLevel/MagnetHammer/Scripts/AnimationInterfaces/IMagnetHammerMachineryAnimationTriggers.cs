﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMagnetHammerMachineryAnimationTriggers {
    void TriggerUnlockMachinery();
    void TriggerStill_postunlock();
    void TriggerRaiseMagnet();
    void TriggerMagnetLockin();
    void TriggerReset();
}
