using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public enum CharacterAbilityCastingStates
{
    LightAttackCastingState,
    HeavyAttackCastingState,
    UtilityCastingState
}

public class AbilityCastingState : MonoBehaviour {

    private CharacterStatesManager statesManager;
    private CharacterState abilityCastingState;

    public CharacterAbilityCastingStates abilityCastingStateName;
	public string firekey;
	public string buttonName;

	private void Awake()
	{
        statesManager = GetComponentInParent<CharacterStatesManager>();
        abilityCastingState = new CharacterState(abilityCastingStateName.ToString(), false);
        statesManager.RegisterCharacterState(abilityCastingState.name, abilityCastingState);
    }

	private void Update()
	{
    
		if (Input.GetKeyDown(firekey))
		{
			abilityCastingState.SetState(true);
			abilityCastingState.SetState(false);
		}
		//print(CrossPlatformInputManager.GetButton(buttonName));
		if (CrossPlatformInputManager.GetButtonDown(buttonName)){
			abilityCastingState.SetState(true);
			abilityCastingState.SetState(false);
		}

	}

}

