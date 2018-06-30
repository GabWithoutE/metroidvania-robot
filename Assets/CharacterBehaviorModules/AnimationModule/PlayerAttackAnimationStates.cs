using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationLocation{
	NotStarted,
    Started,
    Finished
}

public class PlayerAttackAnimationStates : MonoBehaviour {
	private ICharacterStateManager stateManager;

	private CharacterState lightAttackAnimationState;
	private CharacterState heavyAttackAnimationState;
	private CharacterState utilityAbilityAnimationState;
	private CharacterState idleHorizontalAnimationState;
	private CharacterState idleVerticalAnimationState;
	private CharacterState runningHorizontalAnimationState;
	private CharacterState runningVerticalAnimationState;

	private void Awake()
	{
		stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;

		lightAttackAnimationState = new CharacterState(
			ConstantStrings.PlayerAnimatorStates.LIGHT_ATTACK_ANIMATION,
            AnimationLocation.NotStarted
		);

		heavyAttackAnimationState = new CharacterState(
			ConstantStrings.PlayerAnimatorStates.HEAVY_ATTACK_ANIMATION,
            AnimationLocation.NotStarted
        );

		utilityAbilityAnimationState = new CharacterState(
			ConstantStrings.PlayerAnimatorStates.UTILITY_ABILITY_ANIMATION,
            AnimationLocation.NotStarted
        );

		idleHorizontalAnimationState = new CharacterState(
			ConstantStrings.PlayerAnimatorStates.IDLE_HORIZONTAL,
			AnimationLocation.NotStarted
		);

		idleVerticalAnimationState = new CharacterState(
			ConstantStrings.PlayerAnimatorStates.IDLE_VERTICAL,
            AnimationLocation.NotStarted
        );



		stateManager.RegisterCharacterState(lightAttackAnimationState.name,
		                                    lightAttackAnimationState);
		stateManager.RegisterCharacterState(heavyAttackAnimationState.name,
		                                    heavyAttackAnimationState);
		stateManager.RegisterCharacterState(utilityAbilityAnimationState.name,
		                                    utilityAbilityAnimationState);
		stateManager.RegisterCharacterState(idleHorizontalAnimationState.name,
		                                    idleHorizontalAnimationState);
		stateManager.RegisterCharacterState(idleHorizontalAnimationState.name,
                                            idleHorizontalAnimationState);

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
