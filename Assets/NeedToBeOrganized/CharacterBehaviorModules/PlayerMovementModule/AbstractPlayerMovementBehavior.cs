using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlayerMovementBehavior : AbstractCharacterMovementBehavior {

	// Update is called once per frame
	void FixedUpdate () {
		float[] velocity = (float[])stateObserver.GetCharacterStateValue(ConstantStrings.VELOCITY);
        float[] speedScaleValues = (float[])stateObserver.GetCharacterStateValue(ConstantStrings.SPEED_SCALE);
        float runSpeedScaleValue = speedScaleValues[0];
        float jumpSpeedScaleValue = speedScaleValues[1];
        float fallSpeedScaleValue = speedScaleValues[2];
        float verticalScaleValue = 0;
        if (velocity[1] > 0)
        {
            verticalScaleValue = jumpSpeedScaleValue;
        }
        else
        if (velocity[1] < 0)
        {
            verticalScaleValue = fallSpeedScaleValue;
        }

        if (!((bool[]) stateObserver.GetCharacterStateValue(ConstantStrings.RECOIL_STATE))[0]){
            movementHandler.AddToXPosition(velocity[0] * runSpeedScaleValue * Time.deltaTime);
        }
        if (!((bool[])stateObserver.GetCharacterStateValue(ConstantStrings.RECOIL_STATE))[1]){
            movementHandler.AddToYPosition(velocity[1] * verticalScaleValue * Time.deltaTime);
        }
        //characterRigidbody.position +=
                              //new Vector2(velocity[0] * runSpeedScaleValue, velocity[1] * verticalScaleValue) * Time.deltaTime;
	}
}
