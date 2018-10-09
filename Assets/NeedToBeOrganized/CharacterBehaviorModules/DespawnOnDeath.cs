using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnOnDeath : MonoBehaviour {
	private CharacterStatesManager statesManager;
	public GameObject destroyOnDeath;
	// Use this for initialization
	void Awake () {
		statesManager = GetComponentInParent<CharacterStatesManager>();
	}

	void Start(){
		statesManager.GetCharacterStateSubscription(ConstantStrings.DEATH_STATE).OnStateChanged += DestroyOnDeath;	
	}

	private void DestroyOnDeath (object newState){
		bool isDead = (bool)newState;
		if (isDead){
			Destroy(destroyOnDeath);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
