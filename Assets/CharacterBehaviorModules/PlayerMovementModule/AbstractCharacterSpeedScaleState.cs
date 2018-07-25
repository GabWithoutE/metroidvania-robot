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
	//public float percentOfJumpHeightToStartDecelerating;
	//public float slowestJumpSpeed;
	//private float startDecelerationHeight;
	//private float currentJumpHeight;
	private float currentJumpSpeedScale;

	private float accelerationRate;
	//public float fallDistanceToStopAccelerating;
	private float currentFallSpeedScale;
	//private float currentFallHeight;


 //   /*
 //    * time to stop acceleration fall (should reach max fall speed by this time)
 //    */ 
	//public float accelerationEndTime;
 //   /*
 //    * Time to start decelerating jump
 //    */ 
	//public float decelerationStartTime;

    /*
     * Amount of time character has to slow down.
     */ 
	//private float decelerationTime;

	private float currentJumpStartHeight;

	private float currentFallStartHeight;

	//private float decelerationRate;

    public float[] scaleStateValue;
	private bool startJump;
	private bool startFall;
    
    private void Awake()
	{
		stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
		//currentJumpHeight = 0;
		currentJumpSpeedScale = jumpSpeedScale;
		currentFallSpeedScale = 0;
		startJump = false;
		startFall = false;
       
		if (stateManager.ExistsState(ConstantStrings.SPEED_SCALE)){
			speedScaleState = stateManager.GetExistingCharacterState(ConstantStrings.SPEED_SCALE);
			speedScaleState.SetState(new float[] {runSpeedScale, jumpSpeedScale, currentFallSpeedScale});
		}else {
			speedScaleState = new CharacterState(ConstantStrings.SPEED_SCALE, new float[] { runSpeedScale, jumpSpeedScale, currentFallSpeedScale });
			stateManager.RegisterCharacterState(speedScaleState.name, speedScaleState);
		}
	}

	private void Start()
	{
		//decelerationTime = 
		//decelerationRate = jumpSpeedScale / decelerationTime;
		//startDecelerationHeight = percentOfJumpHeightToStartDecelerating * ((float[])stateManager.GetCharacterStateValue(ConstantStrings.VELOCITY))[2];
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
		} else {
			currentFallSpeedScale = 0;
		}
		speedScaleState.SetState(new float[] { runSpeedScale, currentJumpSpeedScale, currentFallSpeedScale });

		//scaleStateValue = (float[])speedScaleState.GetStateValue();
		//if (playerVelocity[1] == 1)
		//{
		//	if (!startJump)
		//	{
		//		startJump = true;
		//		currentJumpStartHeight = transform.position.y;
		//	}

		//	//currentJumpHeight = transform.position.y - currentJumpStartHeight;
		//	//if (currentJumpHeight >= startDecelerationHeight)
		//	//{
		//	currentJumpSpeedScale = Mathf.Clamp(currentJumpSpeedScale - decelerationRate * Time.deltaTime, slowestJumpSpeed, jumpSpeedScale);
		//	//}
		//}
		//else
		//{
		//	startJump = false;
		//	currentJumpHeight = 0;
		//	currentJumpSpeedScale = jumpSpeedScale;
		//}

		//if (playerVelocity[1] == -1){
		//	if (!startFall){
		//		startFall = false;
		//		currentFallStartHeight = transform.position.y;
		//	}
            
		//	if(currentFallSpeedScale <= fallSpeedScale){
		//		currentFallSpeedScale += accelerationRate * Time.deltaTime;
		//		currentFallSpeedScale = Mathf.Clamp(currentFallSpeedScale, 0, fallSpeedScale);
		//	}         
		//} else {
		//	startFall = false;
		//	currentFallSpeedScale = 0;
		//}
		//speedScaleState.SetState(new float[] { runSpeedScale, currentJumpSpeedScale, currentFallSpeedScale });

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
