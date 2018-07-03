using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class CharacterTransformationState : MonoBehaviour {
	public GameObject transformation1;
	public GameObject transformation2;
	public GameObject transformation3;

	private ICharacterStateManager stateManager;
	private CharacterState transformationState;

	// Use this for initialization
	void Awake () {
		stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;

		transformationState = new CharacterState(ConstantStrings.TRANSFORMATION, transformation1);

		stateManager.RegisterCharacterState(transformationState.name, transformationState);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("1")){
			transformationState.SetState(transformation1);
		} 
		else if (Input.GetKeyDown("2")){
			transformationState.SetState(transformation2);		
		}
		if (CrossPlatformInputManager.GetButtonDown(ConstantStrings.UI.Input.BUTTON_TRANSFORM)){
			transformationState.SetState(transformation2);
		}
	}
}
