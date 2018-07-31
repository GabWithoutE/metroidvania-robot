using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthState : MonoBehaviour , IDamageable{
	public float initMaxHealth;
	public float initCurrentHealth;
	public float maxHealth;
	public float currentHealth;
    private CharacterStatesManager statesManager;
	private CharacterState healthState;
    private CharacterState maxHealthState;
	// Use this for initialization
	/**
	 * Register the states that are managed by each individual script in start.
	 */

	void Awake()
	{
		statesManager = GetComponentInParent<CharacterStatesManager>();
        healthState = new CharacterState(ConstantStrings.CURRENT_HEALTH, initCurrentHealth);
        maxHealthState = new CharacterState(ConstantStrings.MAX_HEALTH, initMaxHealth);

        statesManager.RegisterCharacterState(healthState.name, healthState);
        statesManager.RegisterCharacterState(maxHealthState.name, maxHealthState);
	}

	void Start () {
     
   	}

	// Update is called once per frame
	/**
	 * Get references to other states and subscribe to events here.
	 */
	void Update () {
		currentHealth = (float) healthState.GetStateValue();
		maxHealth = (float)maxHealthState.GetStateValue();
	    
	}

	/**
	 * These are the receivers for messages that might be sent to the HealthModule
	 */ 
    public void TakeDamage(float damageDealt){
        healthState.SetState((float)healthState.GetStateValue() - damageDealt);
	}

	/**
	 * Restores health completely
	 */ 
	public void RestoreHealthToMax(){
        healthState.SetState ((float)maxHealthState.GetStateValue());
	}

    /**
	 * Restores health by specified amount
	 */ 
    public void RestoreHealthBy(float restorationAmount){
        if (restorationAmount + (float)healthState.GetStateValue() > (float)maxHealthState.GetStateValue()) {
            healthState.SetState ((float)maxHealthState.GetStateValue());
		} else {
            healthState.SetState ((float)healthState.GetStateValue() + restorationAmount);
		}
	}

	/**
	 * Increases the maximum amount
	 */ 
    public void IncreaseMaxBy (float maxIncreaseAmount){
        maxHealthState.SetState ((float)maxHealthState.GetStateValue() + maxIncreaseAmount);
	}

}
