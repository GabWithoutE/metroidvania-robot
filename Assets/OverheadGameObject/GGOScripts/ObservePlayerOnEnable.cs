using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ObservePlayerOnEnable : MonoBehaviour {
	private GameObject player;
	private CharacterStatesManager playerStatesManager;
	private TransitionalUIDisplayer uiDisplayer;
	public GameObject deathScreen;

	private void OnEnable()
	{
		uiDisplayer = GameObject.FindWithTag("TransitionalUIDisplayer").GetComponent<TransitionalUIDisplayer>();
		SceneManager.sceneLoaded += OnGameSceneLoaded;
	}

	private void OnGameSceneLoaded(Scene scene, LoadSceneMode mode){
		if (scene.name == ConstantStrings.MAINGAME_SCENE_NAME){
			player = GameObject.Find("Player");
            playerStatesManager = player.GetComponent<CharacterStatesManager>();
            CharacterState.CharacterStateSubscription deathSubscription = playerStatesManager.GetCharacterStateSubscription(ConstantStrings.DEATH_STATE);
            deathSubscription.OnStateChanged += OnPlayerDeath;
		}
	}

	private void OnPlayerDeath(object playerDeathState){
		bool isDead = (bool)playerDeathState;
		if (isDead){
			Time.timeScale = 0;
			uiDisplayer.DisplayUIElement(deathScreen);
		}
	}
	
}
