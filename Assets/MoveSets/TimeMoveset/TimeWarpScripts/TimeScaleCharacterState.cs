using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleCharacterState : MonoBehaviour {
    private CharacterStatesManager statesManager;
    private CharacterState timeScale;
    // Use this for initialization
	void Start () {
        statesManager = GetComponent<CharacterStatesManager>();
        timeScale = new CharacterState(ConstantStrings.TIME_SCALE, (float)1);
        statesManager.RegisterCharacterState(timeScale.name, timeScale);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TimeWarpSlowDown(float[] slowdownAmount){
        timeScale.SetState((float)timeScale.GetStateValue() - (float)timeScale.GetStateValue() * slowdownAmount[0]);
    }
}
