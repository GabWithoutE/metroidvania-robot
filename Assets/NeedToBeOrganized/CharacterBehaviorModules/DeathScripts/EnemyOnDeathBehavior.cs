using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnDeathBehavior : MonoBehaviour
{
	public float scoreAmount;
	private ICharacterStateObserver stateObserver;
	private GameObject hitBy;
	private CharacterState.CharacterStateSubscription onDeadSubscription;
	private bool currentDeathState;

	private void Awake()
	{
		stateObserver = GetComponentInParent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;
	}

	private void Start()
	{
		onDeadSubscription = stateObserver.GetCharacterStateSubscription(ConstantStrings.DEATH_STATE);
		onDeadSubscription.OnStateChanged += OnDeathSendScore;
		currentDeathState = (bool)stateObserver.GetCharacterStateValue(ConstantStrings.DEATH_STATE);
	}

	public void HitBy(GameObject validScoringCharacter)
	{
		hitBy = validScoringCharacter;
	}

	private void OnDeath(object state){
		OnDeathSendScore(state);
	}
    
	private void OnDeathSendScore(object state)
	{
		bool isDead = (bool)state;
		if (isDead != currentDeathState){
			currentDeathState = true;
			//print(hitBy);
			hitBy.BroadcastMessage("EnemyKilledType", "");
			hitBy.BroadcastMessage("EnemyKilledScore", scoreAmount);
		}
	}
}
