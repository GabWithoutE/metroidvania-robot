using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeEmptySpeedScaleState : MonoBehaviour {
    
	// Use this for initialization
	void Awake () {
		ICharacterStateManager stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
        CharacterState speedScaleState = new CharacterState(ConstantStrings.SPEED_SCALE, 0f);

		stateManager.RegisterCharacterState(speedScaleState.name, speedScaleState);
	}

}
