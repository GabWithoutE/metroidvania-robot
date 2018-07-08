using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTransformationMovementSwitcher : MonoBehaviour {
	public GameObject currentMovementObject;
	private ICharacterStateObserver stateObserver;
	private CharacterState.CharacterStateSubscription transformationStateSubscription;
	private object currentTransformationStateValue;

	private void Awake()
	{
		stateObserver = GetComponentInParent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;
	}
	// Use this for initialization
	void Start () {
		transformationStateSubscription = stateObserver.GetCharacterStateSubscription(ConstantStrings.TRANSFORMATION);
        currentTransformationStateValue = stateObserver.GetCharacterStateValue(ConstantStrings.TRANSFORMATION);
	
		currentMovementObject = 
			Instantiate(((GameObject)currentTransformationStateValue).GetComponent<CharacterTransformationBundle>().GetMovementObject(), transform);
		transformationStateSubscription.OnStateChanged += SwitchMovementObject;
	}

	private void SwitchMovementObject(object transformationState){
		if (currentTransformationStateValue != transformationState){
			currentTransformationStateValue = transformationState;
			GameObject transformationGameObject = (GameObject)transformationState;

			GameObject movement = transformationGameObject.GetComponent<CharacterTransformationBundle>().GetMovementObject();
			Destroy(currentMovementObject);
			currentMovementObject = Instantiate(movement, transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
