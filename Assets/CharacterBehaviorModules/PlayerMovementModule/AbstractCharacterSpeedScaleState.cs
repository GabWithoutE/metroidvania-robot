using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractCharacterSpeedScaleState : MonoBehaviour {
	protected ICharacterStateManager stateManager;
	protected CharacterState speedScaleState;

	public float speedScale = 1;

	private void Awake()
	{
		stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;

		if (stateManager.ExistsState(ConstantStrings.SPEED_SCALE)){
			speedScaleState = stateManager.GetExistingCharacterState(ConstantStrings.SPEED_SCALE);
			speedScaleState.SetState(speedScale);
		}else {
			speedScaleState = new CharacterState(ConstantStrings.SPEED_SCALE, speedScale);

			stateManager.RegisterCharacterState(speedScaleState.name, speedScaleState);
		}
	}

	public void IncreaseSpeedByFactorOfForTime(float factor, float time){
		speedScaleState.SetState(speedScale * factor);
	}

	IEnumerator DescreaseSpeedScaleAfterTime(float time){
		yield return new WaitForSeconds(time);
		print("speed returned to normal");
		speedScaleState.SetState(speedScale);
	}
}
