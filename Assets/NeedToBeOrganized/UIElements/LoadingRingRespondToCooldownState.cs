using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingRingRespondToCooldownState : PlayerUIElement {
	private UnityEngine.UI.Image loadingRing;
    
	private void Awake()
	{
		loadingRing = GetComponent<UnityEngine.UI.Image>();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateRingDisplay();
	}

	private void UpdateRingDisplay(){
		float[] cdValue = (float[])playerStatesManager.GetCharacterStateValue(characterStateName);

		float percentage = cdValue[1] / cdValue[0];

		if (percentage > 0){
			loadingRing.fillAmount = 1 - percentage;
		} else{
			loadingRing.fillAmount = 1;
		}


	}
}
