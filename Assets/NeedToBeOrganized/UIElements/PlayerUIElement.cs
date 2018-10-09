using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIElement : MonoBehaviour{
	protected CharacterStatesManager playerStatesManager;
	protected string characterStateName;
    
	public void SetPlayerStatesManager(CharacterStatesManager statesManager){
		playerStatesManager = statesManager;
	}

	public void SetCharacterStateName(string stateName){
		characterStateName = stateName;
	}

}
