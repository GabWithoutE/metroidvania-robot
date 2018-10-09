using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class DirectionState : MonoBehaviour {
	/*
     * Direction state is based on the previous velocity state (for horizontal)
     * and on vertical input.
     */
	private ICharacterStateManager stateManager;
	private float[] directionValues;
	private CharacterState directionState;
	public float[] displayDirectionStateValues;

	private void Awake()
	{
		stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
		directionValues = new float[] { 0, 0 };
		directionState = new CharacterState(ConstantStrings.DIRECTION, directionValues);
		stateManager.RegisterCharacterState(directionState.name, directionState);
	}
	
	// Update is called once per frame
	void Update () {
		float[] velocityValues = (float[])stateManager.GetCharacterStateValue(ConstantStrings.VELOCITY);
		float yAxisValues = CrossPlatformInputManager.GetAxisRaw(ConstantStrings.UI.Input.INPUT_VERTICAL);
        // if not moving horizontal values, then retain the horizontal values. else change them.
		if (velocityValues[0] != 0){
			directionValues[0] = velocityValues[0];
			directionValues[1] = yAxisValues;
		}

		directionValues[1] = yAxisValues;

		directionState.SetState(directionValues);
		displayDirectionStateValues = (float[])directionState.GetStateValue();

	}
}
