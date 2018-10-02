using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorParameters : MonoBehaviour
{
	private Animator playerAnimator;
	private ICharacterStateObserver stateObserver;
	private int runningHash;
	private int lightAttackHash;
	private int heavyAttackHash;
	private int fallingHash;
	private int jumpingHash;

	private void Awake()
	{
		playerAnimator = GetComponent<Animator>();
		stateObserver = GetComponentInParent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;
		runningHash = Animator.StringToHash("Running");
		lightAttackHash = Animator.StringToHash("LightAttack");
		fallingHash = Animator.StringToHash("Falling");
		jumpingHash = Animator.StringToHash("Jumping");
		heavyAttackHash = Animator.StringToHash("HeavyAttack");
	}

	private void Start()
	{
		CharacterState.CharacterStateSubscription lightAttackLockSub =
			stateObserver.GetCharacterStateSubscription(ConstantStrings.LIGHT_ATTACK_CAST);
		lightAttackLockSub.OnStateChanged += OnLightAttackLockSetTrigger;
	}



	// Update is called once per frame
	void Update()
	{
		float[] playerVelocity = (float[])stateObserver.GetCharacterStateValue(ConstantStrings.VELOCITY);
		float playerXVelocity = playerVelocity[0];
		float playerYVelocity = playerVelocity[1];

		if (playerXVelocity != 0)
		{
			playerAnimator.SetBool(runningHash, true);
		}
		else
		{
			playerAnimator.SetBool(runningHash, false);
		}
		if (playerYVelocity < 0)
		{
			playerAnimator.SetBool(fallingHash, true);
		}
		else
		{
			playerAnimator.SetBool(fallingHash, false);
		}
		if (playerYVelocity > 0)
		{
			playerAnimator.SetBool(jumpingHash, true);
		}
		else
		{
			playerAnimator.SetBool(jumpingHash, false);
		}


		playerAnimator.SetBool(heavyAttackHash,
							   (bool)stateObserver.GetCharacterStateValue(ConstantStrings.HEAVY_ATTACK_CAST));

	}

	private void OnLightAttackLockSetTrigger(object lockState)
	{
		if ((bool)lockState)
		{
			playerAnimator.SetTrigger(lightAttackHash);
		}

	}
}