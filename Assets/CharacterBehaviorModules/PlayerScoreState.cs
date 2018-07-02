using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreState : MonoBehaviour {
	private ICharacterStateManager statesManager;
	private CharacterState scoreState;
	public float score;
	//public bool testScoreStateToDisplayConnection = false;
    
	private void Awake()
	{
		statesManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
		scoreState = new CharacterState(ConstantStrings.SCORE, (float)0);
		statesManager.RegisterCharacterState(scoreState.name, scoreState);

	}
	// Use this for initialization
	void Start () {
	}

	public void EnemyKilledScore(float score){
		scoreState.SetState((float)scoreState.GetStateValue() + score);
	}

	//private void KilledEnemy(object killedEnemy){
	//	bool ke = (bool)killedEnemy; 
	//	if (ke) {
	//		scoreState.SetState((float) scoreState.GetStateValue() + 1);
	//	}
	//}
	
	// Update is called once per frame
	void Update () {
		score = (float) scoreState.GetStateValue();
	}
}
