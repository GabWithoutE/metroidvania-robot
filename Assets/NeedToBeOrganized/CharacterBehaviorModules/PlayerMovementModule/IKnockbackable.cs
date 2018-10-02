using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockbackable {
    void KnockbackStraight(Vector2 direction, float amount, AttackSide attackSide);
    void KnockbackAngled(Vector2 direction, float amount);
   
}
