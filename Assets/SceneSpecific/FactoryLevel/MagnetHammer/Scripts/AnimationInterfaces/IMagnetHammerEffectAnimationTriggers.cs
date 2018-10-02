using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMagnetHammerEffectAnimationTriggers  {
    void TriggerDroppingEffect();
    void TriggerMagnetGrounded();
    void TriggerRaisingMagnet();
    void TriggerReset();
}
