using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCharacterSpeedScaleState : MonoBehaviour {
	protected ICharacterStateManager stateManager;
	protected CharacterState speedScaleState;

	public float runSpeedScale = 1;
	public float jumpSpeedScale = 1;
	public float fallSpeedScale = 1;

	private void Awake()
	{
		stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;

		if (stateManager.ExistsState(ConstantStrings.SPEED_SCALE)){
			speedScaleState = stateManager.GetExistingCharacterState(ConstantStrings.SPEED_SCALE);
			speedScaleState.SetState(new float[] {runSpeedScale, jumpSpeedScale, fallSpeedScale});
		}else {
			speedScaleState = new CharacterState(ConstantStrings.SPEED_SCALE, new float[] { runSpeedScale, jumpSpeedScale, fallSpeedScale });

			stateManager.RegisterCharacterState(speedScaleState.name, speedScaleState);
		}
	}



	public void IncreaseSpeedByFactorOfForTime(float factor, float time){
		float[] speedScaleStateValue = (float[])speedScaleState.GetStateValue();
		speedScaleStateValue[0] = runSpeedScale * factor;
		speedScaleState.SetState(speedScaleStateValue);
		StartCoroutine(DescreaseSpeedScaleAfterTime(time, speedScaleStateValue));
	}

	IEnumerator DescreaseSpeedScaleAfterTime(float time, float[] speedScaleValues){
		yield return new WaitForSeconds(time);
		//print("speed returned to normal");
		speedScaleValues[0] = runSpeedScale;
		speedScaleState.SetState(speedScaleValues);
	}
}
