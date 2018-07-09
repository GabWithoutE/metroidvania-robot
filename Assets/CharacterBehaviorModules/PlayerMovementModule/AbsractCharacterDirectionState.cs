using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCharacterDirectionState : MonoBehaviour {
	protected ICharacterStateManager statesManager;
	protected CharacterState directionState;

	protected void Awake()
	{
		statesManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
		float[] emptyDirection = new float[] { 0, 0 };
		directionState = new CharacterState(ConstantStrings.VELOCITY, emptyDirection);

		statesManager.RegisterCharacterState(directionState.name, directionState);
	}

}
