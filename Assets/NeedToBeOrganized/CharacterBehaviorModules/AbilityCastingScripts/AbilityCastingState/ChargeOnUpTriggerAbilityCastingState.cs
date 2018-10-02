using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ChargeOnUpTriggerAbilityCastingState : AbstractAbilityCastingState {

	private void Awake()
	{
		base.Awake();
		//abilityCastingState.OnStateChanged += test;
	}

	// Update is called once per frame
	protected override void Update () {
		if (CrossPlatformInputManager.GetButton(inputName)){
			abilityCastingState.SetState(true);
		}
		if(CrossPlatformInputManager.GetButtonUp(inputName)){
			abilityCastingState.SetState(false);
		}
	}

	//private void test(object state){
	//	print((bool)state);
	//}
    
}
