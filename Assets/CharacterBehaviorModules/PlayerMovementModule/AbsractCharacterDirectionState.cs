using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractCharacterDirectionState : MonoBehaviour {
	protected ICharacterStateManager statesManager;
	protected CharacterState directionState;

	private void Awake()
	{
		statesManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
		Vector2 emptyDirection = new Vector2(0, 0);
		directionState = new CharacterState(ConstantStrings.VELOCITY, emptyDirection);

		statesManager.RegisterCharacterState(directionState.name, directionState);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
