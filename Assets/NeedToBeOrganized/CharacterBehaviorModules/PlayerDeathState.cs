using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : MonoBehaviour {
	public bool dead = false;
	private CharacterStatesManager statesManager;
	private CharacterState deathState;
	// Use this for initialization
	void Awake()
	{
		statesManager = GetComponentInParent<CharacterStatesManager>();
        deathState = new CharacterState(ConstantStrings.DEATH_STATE, dead);

        statesManager.RegisterCharacterState(deathState.name, deathState);
	}

	void Start () {
		CharacterState.CharacterStateSubscription healthStateSubscription = statesManager.GetCharacterStateSubscription(ConstantStrings.CURRENT_HEALTH);
		healthStateSubscription.OnStateChanged += CheckHealthState;
	}
	
	// Update is called once per frame
	void Update () {
		dead = (bool) deathState.GetStateValue();
	}

	private void CheckHealthState(object currentHealth){
		if ((float) currentHealth <= 0){
			deathState.SetState(true);
		}
	}
}
