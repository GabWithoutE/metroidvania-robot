using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemyState : MonoBehaviour, IReceiveEnemyHit{
	private CharacterStatesManager statesManager;
	private CharacterState hitEnemyState;
	//private CharacterState killedEnemyState;
    

	public void EnemyHit(bool isDead){
		if (!isDead){
			HitEnemy();
		}
	}
    

	private void HitEnemy(){
		hitEnemyState.SetState(true);
		hitEnemyState.SetState(false);
	}

	void Awake(){
		statesManager = GetComponentInParent<CharacterStatesManager>();
		hitEnemyState = new CharacterState(ConstantStrings.HIT_ENEMY, false);

		statesManager.RegisterCharacterState(hitEnemyState.name, hitEnemyState);
        

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
