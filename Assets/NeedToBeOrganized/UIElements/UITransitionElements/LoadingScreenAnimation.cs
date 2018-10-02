using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenAnimation : MonoBehaviour {
	private UnityEngine.UI.Text loadingText;
	private int frameNumber;

	private void Awake()
	{
		loadingText = GetComponent<UnityEngine.UI.Text>();
		frameNumber = 1;
	}
	// Use this for initialization
	void Start () {
		InvokeRepeating("DisplayNextLoadingTextFrame", 0.0f, 0.5f);
	}

	void DisplayNextLoadingTextFrame(){
		switch(frameNumber){
			case 1:
				loadingText.text = "Loading";
				break;
			case 2:
				loadingText.text = "Loading.";
				break;
			case 3:
				loadingText.text = "Loading..";
				break;
			case 4:
				loadingText.text = "Loading...";
				frameNumber = 0;
				break;
		}
		frameNumber++;
	}
	
}
