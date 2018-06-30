using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilledEnemyState : MonoBehaviour {
	private ICharacterStateManager stateManager;
	private CharacterState killedEnemyState;

	public void EnemyKilled(GameObject enemy){
		OnEnemyKilled(enemy);
	}

	private void OnEnemyKilled(GameObject enemy){
		killedEnemyState.SetState(true);
		killedEnemyState.SetState(false);
	}

	private void Awake()
	{
		stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
		killedEnemyState = new CharacterState(ConstantStrings.KILLED_ENEMY, false);

		stateManager.RegisterCharacterState(killedEnemyState.name, killedEnemyState);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
