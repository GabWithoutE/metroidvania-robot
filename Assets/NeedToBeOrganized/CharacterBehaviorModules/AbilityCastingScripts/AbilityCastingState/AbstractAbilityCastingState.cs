using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAbilityCastingState : MonoBehaviour {

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
            abilityCastingState = new CharacterState(inputName, false);
			stateManager.RegisterCharacterState(abilityCastingState.name, abilityCastingState);
        }
    }

	protected abstract void Update();
}
