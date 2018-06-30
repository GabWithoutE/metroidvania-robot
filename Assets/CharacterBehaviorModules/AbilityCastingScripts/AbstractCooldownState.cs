using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCooldownState : MonoBehaviour, IAbilityCasted
{

	protected CharacterState lightAttackCooldownState;
	protected CharacterState heavyAttackCooldownState;
	protected CharacterState utilityAbilityCooldownState;
	protected ICharacterStateManager statesManager;

	protected MoveSet moveSet;
	protected SpellStats lightAttackStats;
	protected SpellStats heavyAttackStats;
	protected SpellStats utilityAbilityStats;

	protected GameObject lightAttackGO;
	protected GameObject heavyAttackGO;
	protected GameObject utilityAbilityGO;
    

    
	protected void Awake()
	{
		statesManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
		moveSet = GetComponent<MoveSet>();

		lightAttackGO = moveSet.GetLightAttack();
		heavyAttackGO = moveSet.GetHeavyAttack();
		utilityAbilityGO = moveSet.GetUtilityAbility();


		float lightAttackCooldownTime = lightAttackGO.GetComponent<SpellStats>().GetCooldownTime();
		float heavyAttackCooldownTime = heavyAttackGO.GetComponent<SpellStats>().GetCooldownTime();
		float utilityAbilityCooldownTime = utilityAbilityGO.GetComponent<SpellStats>().GetCooldownTime();

        // If this is a new CooldownState after a transformation, then pickup the old cooldown states and just change the values
        // Otherwise just make new ones. 
		if (statesManager.ExistsState(ConstantStrings.LIGHT_ATTACK_COOLDOWN)){
			lightAttackCooldownState = statesManager.GetExistingCharacterState(ConstantStrings.LIGHT_ATTACK_COOLDOWN);
			heavyAttackCooldownState = statesManager.GetExistingCharacterState(ConstantStrings.HEAVY_ATTACK_COOLDOWN);
			utilityAbilityCooldownState = statesManager.GetExistingCharacterState(ConstantStrings.UTILITY_COOLDOWN);

			lightAttackCooldownState.SetState(new float[] { lightAttackCooldownTime, 0f });
			heavyAttackCooldownState.SetState(new float[] { heavyAttackCooldownTime, 0f });
			utilityAbilityCooldownState.SetState(new float[] { utilityAbilityCooldownTime, 0f });


		} else {
			lightAttackCooldownState = new CharacterState(ConstantStrings.LIGHT_ATTACK_COOLDOWN, new float[] { lightAttackCooldownTime, 0f });
            heavyAttackCooldownState = new CharacterState(ConstantStrings.HEAVY_ATTACK_COOLDOWN, new float[] { heavyAttackCooldownTime, 0f });
            utilityAbilityCooldownState = new CharacterState(ConstantStrings.UTILITY_COOLDOWN, new float[] { utilityAbilityCooldownTime, 0f });

            statesManager.RegisterCharacterState(lightAttackCooldownState.name, lightAttackCooldownState);
            statesManager.RegisterCharacterState(heavyAttackCooldownState.name, heavyAttackCooldownState);
            statesManager.RegisterCharacterState(utilityAbilityCooldownState.name, utilityAbilityCooldownState);
		}

	}
    
	protected void Start(){
		
	}

	protected void Update()
	{
		DecrementCooldowns();
	}

	protected void DecrementCooldowns()
	{
		DecrementCooldown(lightAttackCooldownState);
		DecrementCooldown(heavyAttackCooldownState);
		DecrementCooldown(utilityAbilityCooldownState);
	}

	protected void DecrementCooldown(CharacterState cooldownState)
	{
		float[] cdstateValue = (float[]) cooldownState.GetStateValue();
		if (cdstateValue[1] > 0){
			cdstateValue[1] -= Time.fixedDeltaTime;
			cooldownState.SetState(cdstateValue);
		}
	}
    
	protected void ResetCooldown(CharacterState cooldownState)
    {
        float[] cdStateValue = (float[])cooldownState.GetStateValue();
        cdStateValue[1] = cdStateValue[0];
        cooldownState.SetState(cdStateValue);
    }

	public void AbilityCasted(GameObject ability) {
		if (ability.tag == lightAttackGO.tag){
			ResetCooldown(lightAttackCooldownState);
		}
		else if (ability.tag == heavyAttackGO.tag){
			ResetCooldown(heavyAttackCooldownState);
		}
		else if (ability.tag == utilityAbilityGO.tag){
			ResetCooldown(utilityAbilityCooldownState);
		}
		              
	}
    


}