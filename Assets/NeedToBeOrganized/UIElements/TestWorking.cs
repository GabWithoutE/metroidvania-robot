using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TestWorking : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//print (CrossPlatformInputManager.GetAxis(ConstantStrings.UI.Input.JOYSTICK_HORIZONTAL));
		print(CrossPlatformInputManager.GetButton(ConstantStrings.UI.Input.INPUT_LIGHT_ATTACK));
	}
}
