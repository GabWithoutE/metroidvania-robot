using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservePlayerStates : MonoBehaviour {
	private GameObject player;
	private CharacterStatesManager statesManager;
	public GameObject lightAttackLoadingRing;
	public GameObject heavyAttackLoadingRing;
	public GameObject utilityAbilityLoadingRing;
	private LoadingRingRespondToCooldownState lightAttackLoadingScript;
	private LoadingRingRespondToCooldownState heavyAttackLoadingScript;
	private LoadingRingRespondToCooldownState utilityAbilityLoadingScript;

	public GameObject healthDisplay;
	private HealthTextTemporaryScript healthDisplayScript;

	public GameObject scoreDisplay;
	private HealthTextTemporaryScript scoreDisplayScript;
    

	// Use this for initialization
	void Awake() {
		player = GameObject.FindWithTag("Player");
		statesManager = player.GetComponent<CharacterStatesManager>();

		SetupCDRings();

		SetupHealthDisplay();

		SetupScoreDisplay();
	}

	private void SetupScoreDisplay(){
		scoreDisplayScript = scoreDisplay.GetComponent<HealthTextTemporaryScript>();
		scoreDisplayScript.SetCharacterStateName(ConstantStrings.SCORE);
		scoreDisplayScript.SetPlayerStatesManager(statesManager);
	}

	private void SetupHealthDisplay(){
		healthDisplayScript = healthDisplay.GetComponent<HealthTextTemporaryScript>();
		healthDisplayScript.SetCharacterStateName(ConstantStrings.CURRENT_HEALTH);
		healthDisplayScript.SetPlayerStatesManager(statesManager);
	}

	private void SetupCDRings(){
		lightAttackLoadingScript =
            lightAttackLoadingRing.GetComponent<LoadingRingRespondToCooldownState>();
        heavyAttackLoadingScript =
            heavyAttackLoadingRing.GetComponent<LoadingRingRespondToCooldownState>();
        utilityAbilityLoadingScript =
            utilityAbilityLoadingRing.GetComponent<LoadingRingRespondToCooldownState>();

        lightAttackLoadingScript.SetPlayerStatesManager(statesManager);
        lightAttackLoadingScript.SetCharacterStateName(
            ConstantStrings.LIGHT_ATTACK_COOLDOWN);

        heavyAttackLoadingScript.SetPlayerStatesManager(statesManager);
        heavyAttackLoadingScript.SetCharacterStateName(
            ConstantStrings.HEAVY_ATTACK_COOLDOWN);

        utilityAbilityLoadingScript.SetPlayerStatesManager(statesManager);
        utilityAbilityLoadingScript.SetCharacterStateName(
            ConstantStrings.UTILITY_COOLDOWN);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
