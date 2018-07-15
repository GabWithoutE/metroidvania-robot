using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeEmptyAbilityTrackForDurationState : MonoBehaviour {
	public string attackLockName;
	private ICharacterStateManager stateManager;
	private CharacterState attackLockState;

	private void Awake()
	{
		stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
		attackLockState = new CharacterState(attackLockName, false);
		stateManager.RegisterCharacterState(attackLockState.name, attackLockState);
	}
    
}
