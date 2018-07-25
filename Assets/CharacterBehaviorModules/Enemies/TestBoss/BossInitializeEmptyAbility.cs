using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInitializeEmptyAbility : MonoBehaviour {

    protected ICharacterStateManager stateManager;
    protected CharacterState abilityCastingState;

    public CharacterAbilityCastingStates abilityCastingStateName;
    //public string firekey;
    public string inputName;

    protected void Awake()
    {
        stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;

        if (stateManager.ExistsState(inputName))
        {
            abilityCastingState = stateManager.GetExistingCharacterState(inputName);
            abilityCastingState.SetState(false);
        }
        else
        {
            Vector2 temp = new Vector2(0, 0);
            abilityCastingState = new CharacterState(inputName, temp);
            stateManager.RegisterCharacterState(abilityCastingState.name, abilityCastingState);
        }
    }
}
