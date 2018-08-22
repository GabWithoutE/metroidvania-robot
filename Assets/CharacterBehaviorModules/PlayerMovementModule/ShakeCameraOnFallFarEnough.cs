using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCameraOnFallFarEnough : MonoBehaviour {
	private ICharacterStateObserver stateObserver;
	public float fallTime;
	public float shakeTime;
	private bool startedFalling;
	public float currentFallTime;


	private void Awake()
	{
		startedFalling = false;
		currentFallTime = 0;
		stateObserver = 
			GetComponentInParent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;

	}
	// Use this for initialization
	void Start () {
		stateObserver.GetCharacterStateSubscription(ConstantStrings.VELOCITY).OnStateChanged += OnVelocityChange;
		stateObserver.GetCharacterStateSubscription(ConstantStrings.GROUNDED).OnStateChanged += OnGrounded;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		if (currentFallTime <= fallTime && startedFalling){
			currentFallTime += Time.deltaTime;
		}
	}
    
	private void OnVelocityChange(object velocity){
		float yVelocityArray = ((float[])velocity)[1];
		if (yVelocityArray < 0){
			startedFalling = true;
		}
	}
	private void OnGrounded(object groundedState){
		bool isGrounded = ((bool[])groundedState)[1];
		if (isGrounded && currentFallTime >= fallTime){
			currentFallTime = 0;
			startedFalling = false;
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            CameraShake cameraShake = camera.GetComponent<CameraShake>();
            cameraShake.ShakeForSeconds(shakeTime);
			//ICameraShaker cameraShaker = (ICameraShaker)GlobalGameObject.globalGameObjectInstance;
			//cameraShaker.ShakeCameraForSeconds(shakeTime);
		}
		if (isGrounded){
			currentFallTime = 0;
			startedFalling = false;
		}
	}
    
}
