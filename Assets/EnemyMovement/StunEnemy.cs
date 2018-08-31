using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEnemy : MonoBehaviour {
    private ICharacterStateManager stateManager;
    private CharacterState stunState;

    void OnTriggerEnter2D(Collider2D col)
    {
        //If enemy is within stun range
        if(col.gameObject.tag == "Enemy")
        {
            //Get enemy's stateManager and set stun state to true momentarily to trigger subscription
            stateManager = col.gameObject.GetComponentInParent<ICharacterStateManager>();
            stunState = stateManager.GetExistingCharacterState(ConstantStrings.Enemy.STUN_STATE);
            stunState.SetState(true);
            stunState.SetState(false);
        }
    }
}
