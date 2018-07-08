using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class EnemyVelocityState : AbstractCharacterDirectionState
{

    // Update is called once per frame
    void Update()
    {
        float horizontalAxisValue = 0.0f;
        float verticalAxisValue = 0.0f;
        bool a = false;
        //If player is detected, chase after player
        if(a)
        {

        }
        //Otherwise stand still
        else
        {
            horizontalAxisValue = 0.0f;
            verticalAxisValue = 0.0f;
        }
        directionState.SetState(new Vector2(horizontalAxisValue, verticalAxisValue));
        //print((Vector2)directionState.GetStateValue());
    }
}
