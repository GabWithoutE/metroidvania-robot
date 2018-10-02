using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerVelocityState : AbstractCharacterVelocityState {

	public float jumpMaxTime = 1;
	public float currentJumpTime;
	public float jumpMaxHeight;
	private float jumpStartHeight;
	private float currentJumpHeight;
    private CharacterState magnetState;
    private CharacterState disableMoveState;
    private bool disableMove;

    private void Awake()
	{
		//currentJumpTime = jumpMaxTime;
		currentJumpHeight = jumpMaxHeight;
		base.Awake();
		directionState.SetState(new float[] { 0, 0, jumpMaxTime });		
	}

    void Start()
    {
        disableMove = (bool)statesManager.GetCharacterStateValue(ConstantStrings.DISABLE_MOVE_STATE);
        CharacterState.CharacterStateSubscription disableMoveSubscription = statesManager.GetCharacterStateSubscription(ConstantStrings.DISABLE_MOVE_STATE);
        disableMoveSubscription.OnStateChanged += CheckMovementState;
        CharacterState.CharacterStateSubscription groundedSubscription = statesManager.GetCharacterStateSubscription(ConstantStrings.GROUNDED);
        groundedSubscription.OnStateChanged += StopJumpOnHeadHit;
        //Create magnet state if it doesn't exist
        if (statesManager.ExistsState(ConstantStrings.MAGNET_STATE))
        {
            magnetState = statesManager.GetExistingCharacterState(ConstantStrings.MAGNET_STATE);
        }
        else
        {
            magnetState = new CharacterState(ConstantStrings.MAGNET_STATE, false);
            statesManager.RegisterCharacterState(magnetState.name, magnetState);
        }
    }

	// Update is called once per frame
	void Update () {
        float horizontalAxisValue;
        float verticalValues;
        if(!disableMove)
        {
            horizontalAxisValue = CrossPlatformInputManager.GetAxisRaw(
                        ConstantStrings.UI.Input.INPUT_HORIZONTAL);
        }
        else
        {
            horizontalAxisValue = 0;
        }

        verticalValues = 0;
        bool isGrounded = ((bool[])statesManager.GetCharacterStateValue(ConstantStrings.GROUNDED))[1];
        bool headHit = ((bool[])statesManager.GetCharacterStateValue(ConstantStrings.GROUNDED))[0];
        if (!(bool)statesManager.GetCharacterStateValue(ConstantStrings.MAGNET_STATE))
        {
            if (isGrounded)
            {
                if (!CrossPlatformInputManager.GetButton(ConstantStrings.UI.Input.INPUT_JUMP))
                {
                    currentJumpTime = 0;
                    //currentJumpHeight = 0;
                    //jumpStartHeight = transform.position.y;
                }
                verticalValues = 0;
            }
            if(!disableMove)
            {
                if (CrossPlatformInputManager.GetButton(ConstantStrings.UI.Input.INPUT_JUMP) && currentJumpTime < jumpMaxTime)
                {
                    verticalValues = CrossPlatformInputManager.GetAxisRaw(ConstantStrings.UI.Input.INPUT_JUMP);
                    currentJumpTime += Time.deltaTime;
                    if (headHit)
                    {
                        currentJumpTime = jumpMaxTime;
                        verticalValues = -1;
                    }
                }
            }            

            //if (CrossPlatformInputManager.GetButton(ConstantStrings.UI.Input.INPUT_JUMP) && currentJumpHeight < jumpMaxHeight){
            //	verticalValues = CrossPlatformInputManager.GetAxisRaw(ConstantStrings.UI.Input.INPUT_JUMP);
            //	currentJumpHeight = transform.position.y - jumpStartHeight;
            //}

            if (!CrossPlatformInputManager.GetButton(ConstantStrings.UI.Input.INPUT_JUMP))
            {
                if (!isGrounded)
                {
                    //currentJumpHeight = jumpMaxHeight;
                    currentJumpTime = jumpMaxTime;
                    verticalValues = -1;
                }
            }

            if (currentJumpTime >= jumpMaxTime && !isGrounded)
            {
                verticalValues = -1;
            }
        } 

		//if (currentJumpHeight >= jumpMaxHeight && !isGrounded){
		//	verticalValues = -1;
		//}

		directionState.SetState(new float[] { horizontalAxisValue, verticalValues , jumpMaxTime});
		//directionState.SetState(new float[] { horizontalAxisValue, verticalValues , jumpMaxHeight});
	}  

	private void StopJumpOnHeadHit(object groundedState){
		bool isHitGroundOnHead = ((bool[])groundedState)[0];
		if (isHitGroundOnHead)
        {
			//currentJumpTime = jumpMaxTime;
			currentJumpHeight = jumpMaxHeight;
        }
	}
    
    private void CheckMovementState(object moveState)
    {
        disableMove = (bool)moveState;
    }
}
