using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public enum CardinalDirections {
    Up,
    Dur,
    Right,
    Ddr,
    Down,
    Ddl,
    Left,
    Dul

}

public class PlayerCardinalDirectionState : MonoBehaviour
{
	private float PI_OVER_EIGHT = Mathf.PI / 8;
	private float THREE_PI_OVER_EIGHT = 3 * Mathf.PI / 8;

	private ICharacterStateManager statesManager;
    private CharacterState direction;

	// Use this for initialization

	void Awake()
	{
		statesManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
        //statesManager = GetComponentInParent<CharacterStatesManager>();
        direction = new CharacterState(ConstantStrings.CARDINAL_DIRECTION, CardinalDirections.Down);
        statesManager.RegisterCharacterState(direction.name, direction);

	}

	void Start()
    {
        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("w"))
        {
            direction.SetState(CardinalDirections.Up);
        }
        else
        if (Input.GetKeyDown("e"))
        {
            direction.SetState(CardinalDirections.Dur);
        }
        else
        if (Input.GetKeyDown("d"))
        {
            direction.SetState(CardinalDirections.Right);
        }
        else
        if (Input.GetKeyDown("c"))
        {
            direction.SetState(CardinalDirections.Ddr);
        }
        else
        if (Input.GetKeyDown("x"))
        {
            direction.SetState(CardinalDirections.Down);
        }
        else
        if (Input.GetKeyDown("z"))
        {
            direction.SetState(CardinalDirections.Ddl); 
        }else
        if (Input.GetKeyDown("a"))
        {
            direction.SetState(CardinalDirections.Left);                      
        }else
        if(Input.GetKeyDown("q"))
        {
            direction.SetState(CardinalDirections.Dul);
        }

		float horizontalAxisValue =
			((Vector2)statesManager.GetCharacterStateValue(ConstantStrings.VELOCITY)).x;

		float verticalAxisValue =
			((Vector2)statesManager.GetCharacterStateValue(ConstantStrings.VELOCITY)).y;

		if (horizontalAxisValue != 0f){
			float angle = Mathf.Abs(Mathf.Atan(verticalAxisValue / horizontalAxisValue));
			// Second QUADRANT
			if (horizontalAxisValue < 0 && verticalAxisValue > 0){
				if (angle < PI_OVER_EIGHT){
					direction.SetState(CardinalDirections.Left);                      
				}
				else if (angle < THREE_PI_OVER_EIGHT){
					direction.SetState(CardinalDirections.Dul);

				} else if (angle > THREE_PI_OVER_EIGHT){
					direction.SetState(CardinalDirections.Up);

				}
                //First Quadrant
			} else if (horizontalAxisValue > 0 && verticalAxisValue > 0) {
				//3
				if ( angle > THREE_PI_OVER_EIGHT){
					direction.SetState(CardinalDirections.Up);

				} else if (angle > PI_OVER_EIGHT){
					direction.SetState(CardinalDirections.Dur);

				} else if (angle < PI_OVER_EIGHT){
					direction.SetState(CardinalDirections.Right);

				}
                
			} else if (horizontalAxisValue > 0 && verticalAxisValue < 0){
				if (angle < PI_OVER_EIGHT){
					direction.SetState(CardinalDirections.Right);

				} else if (angle < THREE_PI_OVER_EIGHT){
					direction.SetState(CardinalDirections.Ddr);

				} else if (angle > THREE_PI_OVER_EIGHT){
					direction.SetState(CardinalDirections.Down);

				}

			} else {
				if (angle > THREE_PI_OVER_EIGHT){
					direction.SetState(CardinalDirections.Down);

				}else if (angle > PI_OVER_EIGHT){
					direction.SetState(CardinalDirections.Ddl); 

				} else if (angle <PI_OVER_EIGHT){
					direction.SetState(CardinalDirections.Left);                      

				}
			}
			//print((CardinalDirections)direction.GetStateValue());
		}


	}

}
