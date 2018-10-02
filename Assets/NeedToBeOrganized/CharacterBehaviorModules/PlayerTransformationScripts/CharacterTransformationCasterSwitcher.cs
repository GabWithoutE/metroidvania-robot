using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTransformationCasterSwitcher : MonoBehaviour {
	public GameObject currentAbilityCaster;
	private ICharacterStateObserver stateObserver;
	private CharacterState.CharacterStateSubscription transformationStateSubscription;
	private object currentTransformationStateValue;

	void Awake(){
		stateObserver = GetComponentInParent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;
	}

	// Use this for initialization
	void Start () {
		transformationStateSubscription = stateObserver.GetCharacterStateSubscription(ConstantStrings.TRANSFORMATION);
		currentTransformationStateValue = stateObserver.GetCharacterStateValue(ConstantStrings.TRANSFORMATION);

		currentAbilityCaster = Instantiate(((GameObject)currentTransformationStateValue).GetComponent<CharacterTransformationBundle>().GetCaster(), transform);
		transformationStateSubscription.OnStateChanged += SwitchCasterObject;
	}

	private void SwitchCasterObject (object transformationState){
		if (currentTransformationStateValue != transformationState)
        {
			currentTransformationStateValue = transformationState;
		    GameObject transformationGameObject = (GameObject)transformationState;

            GameObject caster = transformationGameObject.GetComponent<CharacterTransformationBundle>().GetCaster();
            Destroy(currentAbilityCaster.gameObject);
            currentAbilityCaster = Instantiate(caster, transform);
		}
      
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
