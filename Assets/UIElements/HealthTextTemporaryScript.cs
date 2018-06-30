using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTextTemporaryScript : PlayerUIElement {
	private UnityEngine.UI.Text healthDisplay;
	private CharacterState.CharacterStateSubscription healthChangeSubscription;
	// Use this for initialization
	void Start () {
		healthDisplay = GetComponent<UnityEngine.UI.Text>();
		float health = (float)playerStatesManager.GetCharacterStateValue(characterStateName);
		healthDisplay.text = health.ToString();
		healthChangeSubscription =
			playerStatesManager.GetCharacterStateSubscription(characterStateName);
		healthChangeSubscription.OnStateChanged += UpdateHealthDisplay;
	}

	private void UpdateHealthDisplay(object newState){
		float newHealth = (float)newState;
		healthDisplay.text = newHealth.ToString();
	}
	// Update is called once per frame
	void Update () {
		
	}
}
