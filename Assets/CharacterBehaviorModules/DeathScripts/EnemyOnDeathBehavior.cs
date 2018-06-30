using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnDeathBehavior : MonoBehaviour
{
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

	private void OnDeathSendScore(object state)
	{
		bool isDead = (bool)state;
		if (isDead != currentDeathState){
			currentDeathState = true;
			hitBy.BroadcastMessage("EnemyKilled", transform.root.gameObject);
		}
	}
}
