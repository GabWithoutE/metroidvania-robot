using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public abstract class AbstractCharacterJumpingState : MonoBehaviour {
	protected ICharacterStateManager stateManager;
	protected CharacterState jumpingState;

	public float jumpMaxTime = 1;
	private float currentJumpTime = 0;

	private void Awake()
	{
		stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
		if (stateManager.ExistsState(ConstantStrings.JUMPING)){
			jumpingState = stateManager.GetExistingCharacterState(ConstantStrings.JUMPING);
			jumpingState.SetState(false);
		} else {
			jumpingState = new CharacterState(ConstantStrings.JUMPING, false);
			stateManager.RegisterCharacterState(jumpingState.name, jumpingState);
		}
	}
	
	// Update is called once per frame
	void Update () {
		bool jumping = false;
        
		if (CrossPlatformInputManager.GetButton(ConstantStrings.UI.Input.INPUT_JUMP)){
			jumping = true;
			currentJumpTime += Time.deltaTime;
		}

		if (!CrossPlatformInputManager.GetButton(ConstantStrings.UI.Input.INPUT_JUMP)){
			jumping = false;
		} else if (currentJumpTime > jumpMaxTime){
			jumping = false;
		}

		jumpingState.SetState(jumping);

	}

	private void FixedUpdate()
	{
		
	}


}
