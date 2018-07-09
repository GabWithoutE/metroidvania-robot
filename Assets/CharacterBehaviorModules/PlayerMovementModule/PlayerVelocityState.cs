using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerVelocityState : AbstractCharacterDirectionState {
	
	// Update is called once per frame
	void Update () {
		float horizontalAxisValue = CrossPlatformInputManager.GetAxisRaw(
			ConstantStrings.UI.Input.INPUT_HORIZONTAL);

		float verticalAxisValue =
			CrossPlatformInputManager.GetAxisRaw(ConstantStrings.UI.Input.INPUT_VERTICAL);

		directionState.SetState(new Vector2(horizontalAxisValue, verticalAxisValue));

	}
}
