using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthContainerDisplay : MonoBehaviour {
	public GameObject emptyContainer;
	public GameObject filledContainer;

	public float spaceBetweenContainers;

	public Sprite[] healthContainerSprites;

	private ICharacterStateObserver stateObserver;
	private int currentHealth;
	private int maxHealth;

	private List<GameObject> healthContainers;
    
	private void Awake()
	{
		stateObserver = GetComponentInParent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;	
	}
	// Use this for initialization
	void Start () {
		currentHealth = (int)(float) stateObserver.GetCharacterStateValue(ConstantStrings.CURRENT_HEALTH);
		maxHealth = (int)(float)stateObserver.GetCharacterStateValue(ConstantStrings.MAX_HEALTH);
		SubscribeToHealthChanges();

		healthContainers = new List<GameObject>();
		DrawInitialContainers();

	}

	private void SubscribeToHealthChanges(){
		CharacterState.CharacterStateSubscription currHealthSub = stateObserver.GetCharacterStateSubscription(ConstantStrings.CURRENT_HEALTH);
        CharacterState.CharacterStateSubscription maxHealthSub = stateObserver.GetCharacterStateSubscription(ConstantStrings.MAX_HEALTH);
		currHealthSub.OnStateChanged += OnCurrHealthChange;
	}

	private void DrawInitialContainers(){
		int numberOfContainers = maxHealth;
		int numberOfFilledContainers = currentHealth;
		for (int i = 0; i < numberOfContainers; i++){
			if (i < numberOfFilledContainers){
				healthContainers.Add(Instantiate(filledContainer, transform));
			} else {
				healthContainers.Add(Instantiate(emptyContainer, transform));
			}
		}
	}

	private void OnCurrHealthChange(object health){
		int newCurrentHealth = (int)(float)health;
		int healthDiff = currentHealth - newCurrentHealth;
		if (healthDiff > 0){
			for (int i = currentHealth; i > newCurrentHealth; i--){
				healthContainers[currentHealth - 1].GetComponent<UnityEngine.UI.Image>().sprite = healthContainerSprites[0];
			}
		}
		if (healthDiff < 0) {
			for (int i = currentHealth; i < newCurrentHealth; i++){
				healthContainers[currentHealth].GetComponent<UnityEngine.UI.Image>().sprite = healthContainerSprites[1];
			}
		}

		currentHealth = newCurrentHealth;
	}


}
