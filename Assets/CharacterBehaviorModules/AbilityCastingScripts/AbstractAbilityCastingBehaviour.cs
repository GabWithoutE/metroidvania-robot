using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public abstract class AbstractAbilityCastingBehaviour : MonoBehaviour {

	protected ICharacterStateObserver stateObserver;
	protected GameObject lightAttack;
	protected GameObject heavyAttack;
	protected GameObject utilityAbility;

	protected CharacterState.CharacterStateSubscription lightAttackStateSubscription;
	protected CharacterState.CharacterStateSubscription heavyAttackStateSubscription;
	protected CharacterState.CharacterStateSubscription utilityAbilityStateSubscription;

	protected Vector2 abilitySpawnLocation;
	protected Vector2 abilitySpawnDirection;

	protected MoveSet moveSet;
   	/*
     *  Need a new script that observes the moveset state and determines which ability casting module to spawn in the player
     */

	protected void Awake()
	{
		stateObserver = GetComponentInParent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;
		moveSet = GetComponent<MoveSet>();
		lightAttack = moveSet.GetLightAttackHorizontalRight();
		heavyAttack = moveSet.GetHeavyAttackHorizontalRight();
		utilityAbility = moveSet.GetUtilityAbility();
		abilitySpawnDirection = new Vector2(0, -1);
        // initial default spawnlocation is down.
		abilitySpawnLocation = new Vector2(transform.root.position.x, transform.root.position.y);
	}

	// Use this for initialization
	protected void Start () {
		lightAttackStateSubscription = stateObserver.GetCharacterStateSubscription(
			ConstantStrings.UI.Input.INPUT_LIGHT_ATTACK);
		heavyAttackStateSubscription = stateObserver.GetCharacterStateSubscription(
			ConstantStrings.UI.Input.INPUT_HEAVY_ATTACK);
		utilityAbilityStateSubscription = stateObserver.GetCharacterStateSubscription(
			ConstantStrings.UI.Input.INPUT_UTILITY);

		lightAttackStateSubscription.OnStateChanged += CastLightAttack;
		heavyAttackStateSubscription.OnStateChanged += CastHeavyAttack;
		utilityAbilityStateSubscription.OnStateChanged += CastUtilityAbility;
   	}

	// Update is called once per frame
    protected void Update()
    {
		UpdateSpawnDirection();

    }

	protected void UpdateSpawnDirection (){
		Vector2 joystickDirection = new Vector2(GetHorizontalJoystickValue(), GetVerticalJoystickValue());
		if (joystickDirection != Vector2.zero){
			abilitySpawnLocation = 
				joystickDirection.normalized + new Vector2 (transform.root.position.x, transform.root.position.y);
			abilitySpawnDirection =
				joystickDirection.normalized;
		}
	}

	protected bool isCasting(object castState){
		return (bool)castState;
	}

	protected float GetHorizontalJoystickValue(){
		return CrossPlatformInputManager.GetAxis(ConstantStrings.UI.Input.INPUT_HORIZONTAL);
	}

	protected float GetVerticalJoystickValue(){
		return CrossPlatformInputManager.GetAxis(ConstantStrings.UI.Input.INPUT_VERTICAL);
	}

    /*
     *  Casting defaults just spawn an object in the direction of the joystick
     * 
     */ 
    
	protected void DefaultCastLightAttack(object castState){
		if (!(bool) castState){
			return;
		}      
		if (((float[])stateObserver.GetCharacterStateValue(ConstantStrings.LIGHT_ATTACK_COOLDOWN))[1] > 0)
        {
            return;
        }
		GameObject spell = Instantiate(lightAttack, abilitySpawnLocation, Quaternion.identity);
		spell.GetComponent<CasterReference>().SetCaster(transform.root.gameObject);
		spell.GetComponent<ProjectileStraightMovement>().SetDirection(abilitySpawnDirection);
		gameObject.transform.root.gameObject.BroadcastMessage("AbilityCasted", spell);
	}

	protected void DefaultCastHeavyAttack(object castState)
    {
        if (!(bool)castState)
        {
            return;
        }
		if (((float[])stateObserver.GetCharacterStateValue(ConstantStrings.HEAVY_ATTACK_COOLDOWN))[1] > 0)
        {
            return;
        }
		GameObject spell = Instantiate(heavyAttack, abilitySpawnLocation, Quaternion.identity);
		spell.GetComponent<CasterReference>().SetCaster(transform.root.gameObject);
		spell.GetComponent<ProjectileStraightMovement>().SetDirection(abilitySpawnDirection);
		gameObject.transform.root.gameObject.BroadcastMessage("AbilityCasted", spell);
    }

	protected void DefaultCastUtilityAbility(object castState)
	{
		if (!(bool) castState){
			return;
		}
		if (((float[])stateObserver.GetCharacterStateValue(ConstantStrings.UTILITY_COOLDOWN))[1] > 0)
        {
            return;
        }
		GameObject spell = Instantiate(utilityAbility, abilitySpawnLocation, Quaternion.identity);
		spell.GetComponent<CasterReference>().SetCaster(transform.root.gameObject);
		spell.GetComponent<ProjectileStraightMovement>().SetDirection(abilitySpawnDirection);
		gameObject.transform.root.gameObject.BroadcastMessage("AbilityCasted", spell);
	}

	protected abstract void CastLightAttack(object castState);
	protected abstract void CastHeavyAttack(object castState);
	protected abstract void CastUtilityAbility(object castState);
    

	//protected void OnDestroy()
	//{
	//	lightAttackStateSubscription.OnStateChanged -= CastLightAttack;
 //       heavyAttackStateSubscription.OnStateChanged -= CastHeavyAttack;
 //       utilityAbilityStateSubscription.OnStateChanged -= CastUtilityAbility;
	//}

}
