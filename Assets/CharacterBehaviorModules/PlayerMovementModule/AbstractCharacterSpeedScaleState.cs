using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCharacterSpeedScaleState : MonoBehaviour {
	protected ICharacterStateManager stateManager;
	protected CharacterState speedScaleState;

	public float runSpeedScale = 1;
	public float jumpSpeedScale = 1;
	public float fallSpeedScale = 1;

	private float decelerationRate;
	private float currentJumpSpeedScale;

	private float accelerationRate;
	private float currentFallSpeedScale;


	private float currentJumpStartHeight;

	private float currentFallStartHeight;
    

    public float[] scaleStateValue;
	private bool startJump;
	private bool startFall;
    
    private void Awake()
	{
		stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;

		currentJumpSpeedScale = jumpSpeedScale;
		currentFallSpeedScale = 0;
		startJump = false;
		startFall = false;
       
		if (stateManager.ExistsState(ConstantStrings.SPEED_SCALE)){
			speedScaleState = stateManager.GetExistingCharacterState(ConstantStrings.SPEED_SCALE);
			speedScaleState.SetState(new float[] {runSpeedScale, jumpSpeedScale, currentFallSpeedScale, runSpeedScale, jumpSpeedScale, fallSpeedScale});
		}else {
			speedScaleState = new CharacterState(ConstantStrings.SPEED_SCALE, new float[] { runSpeedScale, jumpSpeedScale, currentFallSpeedScale, runSpeedScale, jumpSpeedScale, fallSpeedScale });
			stateManager.RegisterCharacterState(speedScaleState.name, speedScaleState);
		}
	}

	private void Start()
	{
		decelerationRate = jumpSpeedScale / ((float[])stateManager.GetCharacterStateValue(ConstantStrings.VELOCITY))[2];
		print(decelerationRate);
		accelerationRate = decelerationRate;
	}

	void Update()
	{
		float[] playerVelocity = (float[])stateManager.GetCharacterStateValue(ConstantStrings.VELOCITY);
		if (playerVelocity[1] == 1){
			currentJumpSpeedScale = currentJumpSpeedScale - decelerationRate * Time.deltaTime;
		} else {
			currentJumpSpeedScale = jumpSpeedScale;
		}
		if (playerVelocity[1] == -1){
			currentFallSpeedScale = currentFallSpeedScale + accelerationRate * Time.deltaTime;
			currentFallSpeedScale = Mathf.Clamp(currentFallSpeedScale, 0, fallSpeedScale);
		} else {
			currentFallSpeedScale = 0;
		}
		speedScaleState.SetState(new float[] { runSpeedScale, currentJumpSpeedScale, currentFallSpeedScale, runSpeedScale, jumpSpeedScale, fallSpeedScale });
        

    }
    
	public void IncreaseSpeedByFactorOfForTime(float factor, float time){
		float[] speedScaleStateValue = (float[])speedScaleState.GetStateValue();
		speedScaleStateValue[0] = runSpeedScale * factor;
		speedScaleState.SetState(speedScaleStateValue);
		StartCoroutine(DescreaseSpeedScaleAfterTime(time, speedScaleStateValue));
	}

    public void IncreaseJumpByFactorOfForTime(float factor, float time)
    {
        float[] speedScaleStateValue = (float[])speedScaleState.GetStateValue();
        speedScaleStateValue[0] = runSpeedScale * factor;
        speedScaleStateValue[1] = jumpSpeedScale * factor;
        speedScaleState.SetState(speedScaleStateValue);
        StartCoroutine(DescreaseJumpScaleAfterTime(time, speedScaleStateValue));
    }

    IEnumerator DescreaseSpeedScaleAfterTime(float time, float[] speedScaleValues){
		yield return new WaitForSeconds(time);
		//print("speed returned to normal");
		speedScaleValues[0] = runSpeedScale;
		speedScaleState.SetState(speedScaleValues);
	}

    IEnumerator DescreaseJumpScaleAfterTime(float time, float[] speedScaleValues)
    {
        yield return new WaitForSeconds(time);
        //print("speed returned to normal");
        speedScaleValues[0] = runSpeedScale;
        speedScaleValues[1] = jumpSpeedScale;
        speedScaleState.SetState(speedScaleValues);
    }
}
