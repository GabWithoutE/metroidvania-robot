using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class KeyboardInputToCrossplatformInputButton : MonoBehaviour {
	public string crossplatformInputButtonName;
	public string keyboardButton;

	private void Awake()
	{
		CrossPlatformInputManager.VirtualButton button = new CrossPlatformInputManager.VirtualButton(crossplatformInputButtonName, true);
		CrossPlatformInputManager.RegisterVirtualButton(button);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(keyboardButton)){
			CrossPlatformInputManager.SetButtonDown(crossplatformInputButtonName);
		} else if (Input.GetKeyUp(keyboardButton)){
			CrossPlatformInputManager.SetButtonUp(crossplatformInputButtonName);
		}
	}
}
