using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class KeyboardInputToCrossplatformInputAxis : MonoBehaviour {
	public string crossplatformInputAxisName;

	public string keyboardButtonPositive;
	public string keyboardButtonNegative;

	private void Awake()
	{
		CrossPlatformInputManager.VirtualAxis axis = new CrossPlatformInputManager.VirtualAxis(crossplatformInputAxisName, true);
		CrossPlatformInputManager.RegisterVirtualAxis(axis);
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKeyUp(keyboardButtonPositive)){
  //          CrossPlatformInputManager.SetAxis(crossplatformInputAxisName, 0);
  //      }
		//if (Input.GetKeyUp(keyboardButtonNegative)){
  //          CrossPlatformInputManager.SetAxis(crossplatformInputAxisName, 0);
  //      }

		//if (Input.GetKeyDown(keyboardButtonPositive)){
		//	CrossPlatformInputManager.SetAxis(crossplatformInputAxisName, CrossPlatformInputManager.GetAxis(crossplatformInputAxisName) + 1);
		//}

		//if (Input.GetKeyDown(keyboardButtonNegative)){
			//CrossPlatformInputManager.SetAxis(crossplatformInputAxisName, CrossPlatformInputManager.GetAxis(crossplatformInputAxisName) - 1);
        //}

	}
}
