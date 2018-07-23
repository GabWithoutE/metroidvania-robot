using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInitCooldownStates : MonoBehaviour {

    private void Awake()
    {
        ICharacterStateManager statesManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
        CharacterState meleeAttackCooldownState = new CharacterState(ConstantStrings.LIGHT_ATTACK_COOLDOWN, new float[] { 0f, 0f });
        CharacterState hammerThrowCooldownState = new CharacterState(ConstantStrings.HEAVY_ATTACK_COOLDOWN, new float[] { 0f, 0f });
       
        statesManager.RegisterCharacterState(meleeAttackCooldownState.name, meleeAttackCooldownState);
        statesManager.RegisterCharacterState(hammerThrowCooldownState.name, hammerThrowCooldownState);
    }
}
