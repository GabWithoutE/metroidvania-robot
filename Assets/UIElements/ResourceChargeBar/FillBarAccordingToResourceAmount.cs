using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillBarAccordingToResourceAmount : MonoBehaviour {
	private ICharacterStateObserver stateObserver;
	private UnityEngine.UI.Image resourceBarFill;
	void Awake(){
		stateObserver = GetComponentInParent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;
		resourceBarFill = GetComponent<UnityEngine.UI.Image>();
	}

	// Update is called once per frame
	void Update () {
		resourceBarFill.fillAmount = (float) stateObserver.GetCharacterStateValue(ConstantStrings.UTILITY_RESOURCE_STATE);
	}
}
