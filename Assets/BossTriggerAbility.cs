using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerAbility : MonoBehaviour {
    private CharacterStatesManager statesManager;
    
    // Use this for initialization
    void Start () {
        CharacterState.CharacterStateSubscription meleeStateSubscription = statesManager.GetCharacterStateSubscription("meleeAttackCastState");
        meleeStateSubscription.OnStateChanged += CheckMeleeState;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CheckMeleeState(object meleeAttack)
    {
        if ((bool)meleeAttack)
        {
            
        }
    }
}
