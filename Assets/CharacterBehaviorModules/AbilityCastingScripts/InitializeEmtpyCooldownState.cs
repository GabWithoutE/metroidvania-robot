using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeEmtpyCooldownState : MonoBehaviour {


	private void Awake()
	{
		ICharacterStateManager statesManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
		CharacterState lightAttackCooldownState = new CharacterState(ConstantStrings.LIGHT_ATTACK_COOLDOWN, new float[] { 0f, 0f });
		CharacterState heavyAttackCooldownState = new CharacterState(ConstantStrings.HEAVY_ATTACK_COOLDOWN, new float[] { 0f, 0f });
		CharacterState utilityAbilityCooldownState = new CharacterState(ConstantStrings.UTILITY_COOLDOWN, new float[] { 0f, 0f });

		statesManager.RegisterCharacterState(lightAttackCooldownState.name, lightAttackCooldownState);
		statesManager.RegisterCharacterState(heavyAttackCooldownState.name, heavyAttackCooldownState);
		statesManager.RegisterCharacterState(utilityAbilityCooldownState.name, utilityAbilityCooldownState);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
