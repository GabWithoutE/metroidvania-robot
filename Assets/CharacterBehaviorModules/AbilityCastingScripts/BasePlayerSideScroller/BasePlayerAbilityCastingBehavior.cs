using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerAbilityCastingBehavior : MonoBehaviour {

	/*
     * TODO:
     * spawn the gameobjects for the melee attacks as children
     * on cast, activate the colliders
     * 
     */
	private ICharacterStateManager stateManager;
	private MoveSet moveSet;

	private GameObject lightAttackHorizontalRight;
	private GameObject lightAttackUpRight;
	private GameObject lightAttackDownRight;

	private GameObject heavyAttackHorizontalRight;
	private GameObject heavyAttackUpRight;
	private GameObject heavyAttackDownRight;

	private GameObject lightAttackInstanceRight;
	private GameObject lightAttackInstanceLeft;
	private GameObject lightAttackInstanceUpRight;
	private GameObject lightAttackInstanceDownRight;

	private LightMeleeAttackTrigger lightAttackInstanceRightTrigger;
	private LightMeleeAttackTrigger lightAttackInstanceLeftTrigger;
	private LightMeleeAttackTrigger lightAttackInstanceUpRightTrigger;
	private LightMeleeAttackTrigger lightAttackInstanceDownRightTrigger;

	private float lightAttackDuration;
	private CharacterState lightAttackCastState;

	private GameObject heavyAttackInstanceRight;
	private GameObject heavyAttackInstanceLeft;
	private GameObject heavyAttackInstanceUpRight;
	private GameObject heavyAttackInstanceDownRight;

	private HeavyAttackTrigger heavyAttackInstanceRightTrigger;
	private HeavyAttackTrigger heavyAttackInstanceLeftTrigger;
	private HeavyAttackTrigger heavyAttackInstanceUpRightTrigger;
	private HeavyAttackTrigger heavyAttackInstanceDownRightTrigger;

	private GameObject utilityAbility;
	private GameObject utilityAbilityInstance;
	private UtilityStunGrenadeTrigger utilityStunGrenadeTrigger;
	private CharacterState utilityAbilityCastState;

	private float heavyAttackDuration;
	private CharacterState heavyAttackCastState;

	private bool previousHeavyAttackCastState;
	private float[] currentHeavyAttackChargeDirection;

	private bool abilityCastingLock;
        
    /*
     * Preinstantiate the objects for light and heavy melee attacks
     * 
     */ 
	private new void Awake()
	{
		abilityCastingLock = false;
		stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
		moveSet = GetComponent<MoveSet>();
		GetAttackPrefabs();

		InstantiateAbilities();
      
		RegisterAttackLocks();
		SetTriggers();
		SetAttackDurations();
	}

	private void InstantiateAbilities(){
		lightAttackInstanceRight = Instantiate(lightAttackHorizontalRight, transform.position, Quaternion.identity, transform);
        lightAttackInstanceLeft = Instantiate(lightAttackHorizontalRight, transform.position, Quaternion.identity, transform);
        lightAttackInstanceLeft.transform.localScale = new Vector3(-1, 1, 1);

        lightAttackInstanceUpRight = Instantiate(lightAttackUpRight, transform.position, Quaternion.identity, transform);
        lightAttackInstanceDownRight = Instantiate(lightAttackDownRight, transform.position, Quaternion.identity, transform);

        heavyAttackInstanceRight = Instantiate(heavyAttackHorizontalRight, transform.position, Quaternion.identity, transform);
        heavyAttackInstanceLeft = Instantiate(heavyAttackHorizontalRight, transform.position, Quaternion.identity, transform);
        heavyAttackInstanceLeft.transform.localScale = new Vector3(-1, 1, 1);

        heavyAttackInstanceUpRight = Instantiate(heavyAttackUpRight, transform.position, Quaternion.identity, transform);
        heavyAttackInstanceDownRight = Instantiate(heavyAttackDownRight, transform.position, Quaternion.identity, transform);

        utilityAbilityInstance = Instantiate(utilityAbility, transform);
	}

	private void RegisterAttackLocks(){
		if (stateManager.ExistsState(ConstantStrings.LIGHT_ATTACK_CAST)){
			lightAttackCastState = 
				stateManager.GetExistingCharacterState(ConstantStrings.LIGHT_ATTACK_CAST);
			lightAttackCastState.SetState(false);
		} else {
			lightAttackCastState = new CharacterState(ConstantStrings.LIGHT_ATTACK_CAST, false);
			stateManager.RegisterCharacterState(lightAttackCastState.name, lightAttackCastState);
		}
		if (stateManager.ExistsState(ConstantStrings.HEAVY_ATTACK_CAST))
        {
			heavyAttackCastState =
				stateManager.GetExistingCharacterState(ConstantStrings.HEAVY_ATTACK_CAST);
			heavyAttackCastState.SetState(false);
        }
        else
        {
			heavyAttackCastState = new CharacterState(ConstantStrings.HEAVY_ATTACK_CAST, false);
			stateManager.RegisterCharacterState(heavyAttackCastState.name, heavyAttackCastState);
        }

		if(stateManager.ExistsState(ConstantStrings.UTILITY_ABILITY_CAST)){
			utilityAbilityCastState =
				stateManager.GetExistingCharacterState(ConstantStrings.UTILITY_ABILITY_CAST);
			utilityAbilityCastState.SetState(false);
		} else {
			utilityAbilityCastState = new CharacterState(ConstantStrings.UTILITY_ABILITY_CAST, false);
			stateManager.RegisterCharacterState(utilityAbilityCastState.name, utilityAbilityCastState);
		}
	}

	private void Start()
	{
		previousHeavyAttackCastState = (bool) stateManager.GetCharacterStateValue(ConstantStrings.UI.Input.INPUT_HEAVY_ATTACK);
		CharacterState.CharacterStateSubscription lightAttackSub = 
			stateManager.GetCharacterStateSubscription(ConstantStrings.UI.Input.INPUT_LIGHT_ATTACK);
		CharacterState.CharacterStateSubscription heavyAttackSub =
			              stateManager.GetCharacterStateSubscription(ConstantStrings.UI.Input.INPUT_HEAVY_ATTACK);
		CharacterState.CharacterStateSubscription utilitySub =
			              stateManager.GetCharacterStateSubscription(ConstantStrings.UI.Input.INPUT_UTILITY);

		lightAttackSub.OnStateChanged += CastLightAttack;
		heavyAttackSub.OnStateChanged += CastHeavyAttack;
		utilitySub.OnStateChanged += CastUtilityAbility;

	}

	private void SetAttackDurations(){
		lightAttackDuration = 
			lightAttackHorizontalRight.GetComponent<MeleeAbilityStats>().GetAbilityDuration();
		heavyAttackDuration =
			heavyAttackHorizontalRight.GetComponent<HeavyMeleeAbilityStats>().GetAbilityDuration();
	}

	private void SetTriggers(){
		lightAttackInstanceRightTrigger = lightAttackInstanceRight.GetComponent<LightMeleeAttackTrigger>();
		lightAttackInstanceLeftTrigger = lightAttackInstanceLeft.GetComponent<LightMeleeAttackTrigger>();
		lightAttackInstanceUpRightTrigger = lightAttackInstanceUpRight.GetComponent<LightMeleeAttackTrigger>();
		lightAttackInstanceDownRightTrigger = lightAttackInstanceDownRight.GetComponent<LightMeleeAttackTrigger>();

		heavyAttackInstanceRightTrigger = heavyAttackInstanceRight.GetComponent<HeavyAttackTrigger>();
		heavyAttackInstanceLeftTrigger = heavyAttackInstanceLeft.GetComponent<HeavyAttackTrigger>();
		heavyAttackInstanceUpRightTrigger = heavyAttackInstanceUpRight.GetComponent<HeavyAttackTrigger>();
		heavyAttackInstanceDownRightTrigger = heavyAttackInstanceDownRight.GetComponent<HeavyAttackTrigger>();

		utilityStunGrenadeTrigger = utilityAbilityInstance.GetComponent<UtilityStunGrenadeTrigger>();

	}

	private void GetAttackPrefabs(){
		lightAttackHorizontalRight = moveSet.GetLightAttackHorizontalRight();
        lightAttackUpRight = moveSet.GetLightAttackUpRight();
        lightAttackDownRight = moveSet.GetLightAttackDownRight();
        heavyAttackHorizontalRight = moveSet.GetHeavyAttackHorizontalRight();
        heavyAttackUpRight = moveSet.GetHeavyAttackUpRight();
        heavyAttackDownRight = moveSet.GetHeavyAttackDownRight();
        utilityAbility = moveSet.GetUtilityAbility();
        

	}

    /*
     * TODO: flip the vertical attacks based on the horizontal direction the player is facing.
     */ 
	protected void CastLightAttack(object castState){

		if (!isCasting(castState)){
			return;
		}
		if (!abilityCastingLock)
        {
            abilityCastingLock = true;
        }
        else
        {
            print(abilityCastingLock);
            return;
        }

		float[] playerDirection = GetPlayerDirectionFromManager();

		/*
         * do less checks by mixing some of these together
         */
		// Facing right and attacking up
		if (!(bool)lightAttackCastState.GetStateValue()){
            if (playerDirection[1] > 0)
            {
                lightAttackInstanceUpRightTrigger.TriggerAttack();
				lightAttackCastState.SetState(true);
				lightAttackCastState.SetState(false);
                
            }
            else if (playerDirection[1] < 0)
            {
                lightAttackInstanceDownRightTrigger.TriggerAttack();
				lightAttackCastState.SetState(true);
                lightAttackCastState.SetState(false);
            }
            else if (playerDirection[0] > 0 && playerDirection[1] == 0)
            {
                lightAttackInstanceRightTrigger.TriggerAttack();
				lightAttackCastState.SetState(true);
                lightAttackCastState.SetState(false);
            }
            else if (playerDirection[0] < 0 && playerDirection[1] == 0)
            {
                lightAttackInstanceLeftTrigger.TriggerAttack();
				lightAttackCastState.SetState(true);
                lightAttackCastState.SetState(false);
            }
			abilityCastingLock = false;
		}

		//if (casted && !(bool)lightAttackLock.GetStateValue()){
		//	lightAttackLock.SetState(true);
		//	StartCoroutine(GetAttackLock(lightAttackLock, lightAttackDuration));
		//}
	}

	protected void CastHeavyAttack(object castState){
		if (!abilityCastingLock){
			abilityCastingLock = true;
		}

		float[] playerDirection = GetPlayerDirectionFromManager();

		if (playerDirection[0] == 0 && playerDirection[1] == 0)
        {
			return;
        }

		if (isCasting(castState) && !previousHeavyAttackCastState){
            if (playerDirection[1] > 0)
            {
				currentHeavyAttackChargeDirection = new float[] {0, 1};
            }
            else if (playerDirection[1] < 0)
            {
				currentHeavyAttackChargeDirection = new float[] { 0, -1 };
				heavyAttackInstanceDownRightTrigger.ScaleHeavyAttackByHolding(Time.deltaTime);
            }
            else if (playerDirection[0] > 0 && playerDirection[1] == 0)
            {
				currentHeavyAttackChargeDirection = new float[] { 1, 0 };

				heavyAttackInstanceRightTrigger.ScaleHeavyAttackByHolding(Time.deltaTime);
            }
            else if (playerDirection[0] < 0 && playerDirection[1] == 0)
            {
				currentHeavyAttackChargeDirection = new float[] { -1, 0 };
			} 
		}

		if (isCasting(castState) && previousHeavyAttackCastState){
			if (currentHeavyAttackChargeDirection[1] > 0)
            {
                heavyAttackInstanceUpRightTrigger.ScaleHeavyAttackByHolding(Time.deltaTime);
            }
			else if (currentHeavyAttackChargeDirection[1] < 0)
            {
                heavyAttackInstanceDownRightTrigger.ScaleHeavyAttackByHolding(Time.deltaTime);
            }
			else if (currentHeavyAttackChargeDirection[0] > 0 && currentHeavyAttackChargeDirection[1] == 0)
            {
                heavyAttackInstanceRightTrigger.ScaleHeavyAttackByHolding(Time.deltaTime);
            }
			else if (currentHeavyAttackChargeDirection[0] < 0 && currentHeavyAttackChargeDirection[1] == 0)
            {
                heavyAttackInstanceLeftTrigger.ScaleHeavyAttackByHolding(Time.deltaTime);
            }
			heavyAttackCastState.SetState(true);
		}

		if (!isCasting(castState) && previousHeavyAttackCastState)
        {
			if (currentHeavyAttackChargeDirection[1] > 0)
            {
                heavyAttackInstanceUpRightTrigger.TriggerAttack();
            }
			else if (currentHeavyAttackChargeDirection[1] < 0)
            {
                heavyAttackInstanceDownRightTrigger.TriggerAttack();
            }
			else if (currentHeavyAttackChargeDirection[0] > 0 && currentHeavyAttackChargeDirection[1] == 0)
            {
                heavyAttackInstanceRightTrigger.TriggerAttack();
            }
			else if (currentHeavyAttackChargeDirection[0] < 0 && currentHeavyAttackChargeDirection[1] == 0)
            {
                heavyAttackInstanceLeftTrigger.TriggerAttack();
            }
			heavyAttackCastState.SetState(false);
			abilityCastingLock = false;
        }      

		previousHeavyAttackCastState = (bool)castState;
	}

	protected void CastUtilityAbility(object castState){
		if (!isCasting(castState)){
			return;
		}
		if ((float) stateManager.GetCharacterStateValue(ConstantStrings.UTILITY_RESOURCE_STATE) == 1){
			utilityStunGrenadeTrigger.TriggerAttack();
			utilityAbilityCastState.SetState(true);
			utilityAbilityCastState.SetState(false);
		}
	}

	private float[] GetPlayerDirectionFromManager(){
		return ((float[])stateManager.GetCharacterStateValue(ConstantStrings.DIRECTION));
	}

	private void Update(){
		/*
		 * override to do nothing.
		 */
	}

	IEnumerator GetAttackLock(CharacterState attackLock, float duration){
		yield return new WaitForSeconds(duration);
		attackLock.SetState(false);
	}

	protected bool isCasting(object castState)
    {
        return (bool)castState;
    }
    
}
