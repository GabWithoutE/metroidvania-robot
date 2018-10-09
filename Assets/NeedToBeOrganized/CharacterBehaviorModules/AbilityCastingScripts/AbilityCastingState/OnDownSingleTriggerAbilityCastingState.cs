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

public class OnDownSingleTriggerAbilityCastingState : AbstractAbilityCastingState {

	private void Awake()
	{
		base.Awake();
	}


	protected override void Update()
	{
    
		//if (Input.GetKeyDown(firekey))
		//{
		//	abilityCastingState.SetState(true);
		//	abilityCastingState.SetState(false);
		//}
		//print(CrossPlatformInputManager.GetButton(buttonName));
		if (CrossPlatformInputManager.GetButtonDown(inputName)){
			abilityCastingState.SetState(true);
			abilityCastingState.SetState(false);
		}

	}

}

