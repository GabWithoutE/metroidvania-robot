using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeEmptyUtilityResourceChargeState : MonoBehaviour {
	private ICharacterStateManager stateManager;
	private CharacterState emptyUtilityResourceChargeState;
	private void Awake()
	{
		stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
		emptyUtilityResourceChargeState = new CharacterState(ConstantStrings.UTILITY_RESOURCE_STATE, 0f);
		stateManager.RegisterCharacterState(emptyUtilityResourceChargeState.name, emptyUtilityResourceChargeState);
	}

}
