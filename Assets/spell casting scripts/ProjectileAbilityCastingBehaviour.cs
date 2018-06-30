using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAbilityCastingBehaviour : MonoBehaviour {

	/**
	 * The cooldown[0] position contains the cooldown time while the 1 position contains the
	 * current cooldown's state
	 */ 
	public GameObject lightAttack;
	private AbilitySpellDirectionalSpawnLocations lightAttackLocations;
	private float lightAttackCooldownTime;
	private CharacterState lightAttackCooldownState;

	public GameObject heavyAttack;
	private AbilitySpellDirectionalSpawnLocations heavyAttackLocations;
	private float heavyAttackCooldownTime;
	private CharacterState heavyAttackCooldownState;

    public GameObject utilityAbility;
	private AbilitySpellDirectionalSpawnLocations utilityAbilityLocations;
	private float utilityAbilityCooldownTime;
	private CharacterState utilityAbilityCooldownState;

    private CharacterStatesManager statesManager;
    private CharacterState.CharacterStateSubscription moveSetStateSubscription;
    private CharacterState.CharacterStateSubscription lightAttackStateSubscription;
    private CharacterState.CharacterStateSubscription heavyAttackStateSubscription;
    private CharacterState.CharacterStateSubscription utilityAbilityStateSubscription;

    private GameObject currentMoveSetObject;
    private MoveSet moveSet;

	private bool registered = false;

	void Awake()
	{
        statesManager = GetComponentInParent<CharacterStatesManager>();

	}

	// Use this for initialization
	void Start () {
        InitializeMoveSet();
	}

	// Update is called once per frame
	void Update () {
		if (!registered) {
            lightAttackStateSubscription = statesManager.GetCharacterStateSubscription(
                CharacterAbilityCastingStates.LightAttackCastingState.ToString());
            heavyAttackStateSubscription = statesManager.GetCharacterStateSubscription(
                CharacterAbilityCastingStates.HeavyAttackCastingState.ToString());
            utilityAbilityStateSubscription = statesManager.GetCharacterStateSubscription(
                CharacterAbilityCastingStates.UtilityCastingState.ToString());
            //moveSetStateSubscription = statesManager.GetCharacterStateSubscription(
                //ConstantStrings.MOVE_SET);

            moveSetStateSubscription.OnStateChanged += MoveSetChanged;

            lightAttackStateSubscription.OnStateChanged += CastLightAttack;
            heavyAttackStateSubscription.OnStateChanged += CastHeavyAttack;
			utilityAbilityStateSubscription.OnStateChanged += CastUtilityAbility;
			registered = true;
		}
		UpdateCooldowns ();

	}

	private void InitializeMoveSet()
	{
        //currentMoveSetObject = (GameObject)statesManager.GetCharacterStateValue(ConstantStrings.MOVE_SET);
        moveSet = currentMoveSetObject.GetComponent<MoveSet>();
        SetAllMoveSetObjects();
        SetAllAbilitySpawnLocations();
        GetAllAbilityTimers();
    }

	void MoveSetChanged(object newMoveSet){
        currentMoveSetObject = (GameObject)newMoveSet;
        moveSet = currentMoveSetObject.GetComponent<MoveSet>();
        SetAllMoveSetObjects();
        SetAllAbilitySpawnLocations();
        GetAllAbilityTimers();
        CastOneOff();
    }


	private void SetAllMoveSetObjects(){
        lightAttack = moveSet.GetLightAttack();
        heavyAttack = moveSet.GetHeavyAttack();
        utilityAbility = moveSet.GetUtilityAbility ();
	}

	private void SetAllAbilitySpawnLocations (){
		lightAttackLocations = lightAttack.GetComponent<AbilitySpellDirectionalSpawnLocations> ();
		heavyAttackLocations = heavyAttack.GetComponent<AbilitySpellDirectionalSpawnLocations> ();
		utilityAbilityLocations = utilityAbility.GetComponent<AbilitySpellDirectionalSpawnLocations> ();
	}

	private void GetAllAbilityTimers(){
		lightAttackCooldownTime = lightAttack.GetComponent<SpellStats>().GetCooldownTime();
		heavyAttackCooldownTime = heavyAttack.GetComponent<SpellStats>().GetCooldownTime();
		utilityAbilityCooldownTime = utilityAbility.GetComponent<SpellStats>().GetCooldownTime();

		if (lightAttackCooldownState == null)
		{
			lightAttackCooldownState = new CharacterState(
				ConstantStrings.LIGHT_ATTACK_COOLDOWN,
				new float[] { lightAttackCooldownTime, 0 });
			statesManager.RegisterCharacterState(lightAttackCooldownState.name,
			                                     lightAttackCooldownState);

			heavyAttackCooldownState = new CharacterState(
				ConstantStrings.HEAVY_ATTACK_COOLDOWN,
				new float[] { lightAttackCooldownTime, 0 });
			statesManager.RegisterCharacterState(heavyAttackCooldownState.name,
			                                     heavyAttackCooldownState);

			utilityAbilityCooldownState = new CharacterState(
				ConstantStrings.UTILITY_COOLDOWN,
				new float[] { lightAttackCooldownTime, 0 });
			statesManager.RegisterCharacterState(utilityAbilityCooldownState.name,
			                                     utilityAbilityCooldownState);
		}
		else
		{
			lightAttackCooldownState.SetState(
				new float[] { lightAttackCooldownTime, 0 });
			heavyAttackCooldownState.SetState(
				new float[] { heavyAttackCooldownTime, 0 });
			utilityAbilityCooldownState.SetState(
				new float[] { utilityAbilityCooldownTime, 0 });
		}



		//lightAttackCooldown = 
		//	new double[]{lightAttack.GetComponent<SpellStats> ().GetCooldownTime (), 0};
		//heavyAttackCooldown = 
		//	new double[]{heavyAttack.GetComponent<SpellStats> ().GetCooldownTime (), 0};
		//utilityAbilityCooldown = 
			//new double[]{utilityAbility.GetComponent<SpellStats> ().GetCooldownTime (), 0};		
	}
    


	private void UpdateCooldowns() {
		UpdateCooldown (lightAttackCooldownState);
		UpdateCooldown (heavyAttackCooldownState);
		UpdateCooldown (utilityAbilityCooldownState);
	}

	private void UpdateCooldown(CharacterState cooldownState){
		float [] cdStateValue = (float []) cooldownState.GetStateValue();
		if (cdStateValue[1] >= 0) {
			cdStateValue[1] -= Time.fixedDeltaTime;
			cooldownState.SetState(cdStateValue);
			//cooldownState[1] -= Time.fixedDeltaTime;
		}
	}

	private void ResetCooldown(CharacterState cooldownState){
		float[] cdStateValue = (float[])cooldownState.GetStateValue();
		cdStateValue[1] = cdStateValue[0];
		cooldownState.SetState(cdStateValue);
	}

	private void CastOneOff(){
		GameObject oneOffAbility = moveSet.GetOneOffAbility ();
		if (oneOffAbility != null) {
			Instantiate (oneOffAbility, transform.position, Quaternion.identity);
		}
	}

	void CastLightAttack(object castState){
        if (!(bool)castState) {
			return;
		}
		if (((float[])lightAttackCooldownState.GetStateValue())[1] > 0)
        {
            return;
        }
        CardinalDirections direction = 
            (CardinalDirections)statesManager.GetCharacterStateValue(ConstantStrings.DIRECTION);
		Vector2 spawnLocation = new Vector2 (0, 0);
		switch (direction) {
            case (CardinalDirections.Up):
			spawnLocation = lightAttackLocations.GetUpSpawnLocation();
			break;
            case(CardinalDirections.Dur):
			spawnLocation = lightAttackLocations.GetUpRightSpawnLocation();
			break;
            case (CardinalDirections.Right):
			spawnLocation = lightAttackLocations.GetRightSpawnLocation ();
			break;
            case(CardinalDirections.Ddr):
			spawnLocation = lightAttackLocations.GetDownRightSpawnLocation();
			break;
            case(CardinalDirections.Down):
			spawnLocation = lightAttackLocations.GetDownSpawnLocation();
			break;
            case(CardinalDirections.Ddl):
			spawnLocation = lightAttackLocations.GetDownLeftSpawnLocation();
			break;
            case(CardinalDirections.Left):
			spawnLocation = lightAttackLocations.GetLeftSpawnLocation();
			break;
            case(CardinalDirections.Dul):
			spawnLocation = lightAttackLocations.GetUpLeftSpawnLocation();
			break;
		}
		GameObject spell = Instantiate (lightAttack, spawnLocation, Quaternion.identity);
		spell.GetComponent<ProjectileStraightMovement> ().SetDirection (spawnLocation);
		gameObject.transform.parent.gameObject.BroadcastMessage("AbilityCasted", spell);
		ResetCooldown (lightAttackCooldownState);
		//print(spell.tag);
	}

	void CastHeavyAttack(object castState){
        if (!(bool)castState)
        {
            return;
        }
		if (((float[]) heavyAttackCooldownState.GetStateValue())[1] > 0 ){
			return;
		}
        CardinalDirections direction =
            (CardinalDirections)statesManager.GetCharacterStateValue(ConstantStrings.DIRECTION);
        Vector2 spawnLocation = new Vector2 (0, 0);
		switch (direction) {
            case (CardinalDirections.Up):
			spawnLocation = heavyAttackLocations.GetUpSpawnLocation();
			break;
            case(CardinalDirections.Dur):
			spawnLocation = heavyAttackLocations.GetUpRightSpawnLocation();
			break;
            case (CardinalDirections.Right):
			spawnLocation = heavyAttackLocations.GetRightSpawnLocation ();
			break;
            case(CardinalDirections.Ddr):
			spawnLocation = heavyAttackLocations.GetDownRightSpawnLocation();
			break;
            case(CardinalDirections.Down):
			spawnLocation = heavyAttackLocations.GetDownSpawnLocation();
			break;
            case(CardinalDirections.Ddl):
			spawnLocation = heavyAttackLocations.GetDownLeftSpawnLocation();
			break;
            case(CardinalDirections.Left):
			spawnLocation = heavyAttackLocations.GetLeftSpawnLocation();
			break;
            case(CardinalDirections.Dul):
			spawnLocation = heavyAttackLocations.GetUpLeftSpawnLocation();
			break;
		}
		GameObject spell = Instantiate (heavyAttack, spawnLocation, Quaternion.identity);
		spell.GetComponent<ProjectileStraightMovement> ().SetDirection (spawnLocation);
		gameObject.transform.parent.gameObject.BroadcastMessage("AbilityCasted", spell);
		ResetCooldown (heavyAttackCooldownState);
	}

	void CastUtilityAbility(object castState){
        if (!(bool)castState)
        {
            return;
        }
		if (((float[])utilityAbilityCooldownState.GetStateValue())[1] > 0)
        {
            return;
        }
        CardinalDirections direction =
            (CardinalDirections)statesManager.GetCharacterStateValue(ConstantStrings.DIRECTION);
		Vector2 spawnLocation = new Vector2 (0, 0);
		switch (direction) {
            case (CardinalDirections.Up):
			spawnLocation = utilityAbilityLocations.GetUpSpawnLocation();
			break;
            case(CardinalDirections.Dur):
			spawnLocation = utilityAbilityLocations.GetUpRightSpawnLocation();
			break;
            case (CardinalDirections.Right):
			spawnLocation = utilityAbilityLocations.GetRightSpawnLocation ();
			break;
            case(CardinalDirections.Ddr):
			spawnLocation = utilityAbilityLocations.GetDownRightSpawnLocation();
			break;
            case(CardinalDirections.Down):
			spawnLocation = utilityAbilityLocations.GetDownSpawnLocation();
			break;
            case(CardinalDirections.Ddl):
			spawnLocation = utilityAbilityLocations.GetDownLeftSpawnLocation();
			break;
            case(CardinalDirections.Left):
			spawnLocation = utilityAbilityLocations.GetLeftSpawnLocation();
			break;
            case(CardinalDirections.Dul):
			spawnLocation = utilityAbilityLocations.GetUpLeftSpawnLocation();
			break;
		}
		GameObject spell = Instantiate (utilityAbility, spawnLocation, Quaternion.identity);
		spell.GetComponent<ProjectileStraightMovement> ().SetDirection (spawnLocation);
		gameObject.transform.parent.gameObject.BroadcastMessage("AbilityCasted", spell);
		ResetCooldown (utilityAbilityCooldownState);

	}
		
}
