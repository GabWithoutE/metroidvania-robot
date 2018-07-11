using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterMovementBehavior : AbstractCharacterMovementBehavior
{    
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        float[] velocity = (float[])stateObserver.GetCharacterStateValue(ConstantStrings.VELOCITY);
        float[] speedScaleValues = (float[])stateObserver.GetCharacterStateValue(ConstantStrings.SPEED_SCALE);
        float runSpeedScaleValue = speedScaleValues[0];
        float jumpSpeedScaleValue = speedScaleValues[1];
        float fallSpeedScaleValue = speedScaleValues[2];
        float verticalScaleValue = 0;
        characterRigidbody.position +=
                              new Vector2(velocity[0] * runSpeedScaleValue, velocity[1] * verticalScaleValue) * Time.deltaTime;
    }    
}
