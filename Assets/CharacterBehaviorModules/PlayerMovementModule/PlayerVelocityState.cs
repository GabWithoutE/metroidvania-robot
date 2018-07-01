using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerVelocityState : AbstractCharacterDirectionState {
	
	// Update is called once per frame
	void Update () {
		float horizontalAxisValue = CrossPlatformInputManager.GetAxis(
			ConstantStrings.UI.Input.JOYSTICK_HORIZONTAL);

		float verticalAxisValue =
            CrossPlatformInputManager.GetAxis(ConstantStrings.UI.Input.JOYSTICK_VERTICAL);

		directionState.SetState(new Vector2(horizontalAxisValue, verticalAxisValue));
		//print((Vector2)directionState.GetStateValue());

	}
}
