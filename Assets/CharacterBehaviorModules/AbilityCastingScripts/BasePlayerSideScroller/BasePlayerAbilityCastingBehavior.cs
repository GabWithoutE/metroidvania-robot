using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerAbilityCastingBehavior : AbstractAbilityCastingBehaviour {

	/*
     * TODO:
     * spawn the gameobjects for the melee attacks as children
     * on cast, activate the colliders
     * 
     */
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


	private GameObject heavyAttackInstanceRight;
	private GameObject heavyAttackInstanceLeft;
	private GameObject heavyAttackInstanceUpRight;
	private GameObject heavyAttackInstanceDownRight;

	private HeavyAttackTrigger heavyAttackInstanceRightTrigger;
	private HeavyAttackTrigger heavyAttackInstanceLeftTrigger;
	private HeavyAttackTrigger heavyAttackInstanceUpRightTrigger;
	private HeavyAttackTrigger heavyAttackInstanceDownRightTrigger;

	private bool previousHeavyAttackCastState;
	private float[] currentHeavyAttackChargeDirection;
        
    /*
     * Preinstantiate the objects for light and heavy melee attacks
     * 
     */ 
	private void Awake()
	{
		stateObserver = GetComponentInParent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;
		moveSet = GetComponent<MoveSet>();
		GetAttackPrefabs();

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

		SetTriggers();

        
	}

	private void Start()
	{
		previousHeavyAttackCastState = (bool) stateObserver.GetCharacterStateValue(ConstantStrings.UI.Input.INPUT_HEAVY_ATTACK);
		base.Start();

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

	}

	private void GetAttackPrefabs(){
		lightAttackHorizontalRight = moveSet.GetLightAttackHorizontalRight();
		lightAttackUpRight = moveSet.GetLightAttackUpRight();
		lightAttackDownRight = moveSet.GetLightAttackDownRight();

		heavyAttackHorizontalRight = moveSet.GetHeavyAttackHorizontalRight();
		heavyAttackUpRight = moveSet.GetHeavyAttackUpRight();
		heavyAttackDownRight = moveSet.GetHeavyAttackDownRight();
        

	}

    /*
     * TODO: flip the vertical attacks based on the horizontal direction the player is facing.
     */ 
	protected override void CastLightAttack(object castState){
		if (!isCasting(castState)){
			return;
		}
		if (((float[])stateObserver.GetCharacterStateValue(ConstantStrings.LIGHT_ATTACK_COOLDOWN))[1] > 0)
		{
			return;
		}
		float[] playerDirection = GetPlayerDirectionFromManager();
        
        /*
         * do less checks by mixing some of these together
         */ 

        // Facing right and attacking up
		if (playerDirection[1] > 0){
			lightAttackInstanceUpRightTrigger.TriggerAttack();
		} 
		else if (playerDirection[1] < 0){
			lightAttackInstanceDownRightTrigger.TriggerAttack();
		} 
		else if (playerDirection[0] > 0 && playerDirection[1] == 0){
			lightAttackInstanceRightTrigger.TriggerAttack();
		} 
		else if (playerDirection[0] < 0 && playerDirection[1] == 0){
			lightAttackInstanceLeftTrigger.TriggerAttack();
    	}
	}

	protected override void CastHeavyAttack(object castState){
		if (((float[])stateObserver.GetCharacterStateValue(ConstantStrings.HEAVY_ATTACK_COOLDOWN))[1] > 0)
        {
            return;
        }
		float[] playerDirection = GetPlayerDirectionFromManager();

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
                //print("hello");
                //print(Time.deltaTime);
                heavyAttackInstanceRightTrigger.ScaleHeavyAttackByHolding(Time.deltaTime);
            }
			else if (currentHeavyAttackChargeDirection[0] < 0 && currentHeavyAttackChargeDirection[1] == 0)
            {
                heavyAttackInstanceLeftTrigger.ScaleHeavyAttackByHolding(Time.deltaTime);
            }
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
        }




		previousHeavyAttackCastState = (bool)castState;
	}

	protected override void CastUtilityAbility(object castState){
		
	}

	private float[] GetPlayerDirectionFromManager(){
		return ((float[])stateObserver.GetCharacterStateValue(ConstantStrings.DIRECTION));
	}

	private void Update(){
		/*
		 * override to do nothing.
		 */
	}
    
}
