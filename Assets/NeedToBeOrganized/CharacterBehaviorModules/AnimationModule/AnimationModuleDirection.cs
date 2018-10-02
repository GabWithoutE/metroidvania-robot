using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationModuleDirection : MonoBehaviour {
	private ICharacterStateObserver stateObserver;

	private void Awake(){
		stateObserver = GetComponentInParent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;
        
	}
	
	// Update is called once per frame
	void Update () {
		float[] characterDirection = (float[]) stateObserver.GetCharacterStateValue(ConstantStrings.DIRECTION);
		if (characterDirection[0] < 0 ){
			transform.localScale = new Vector3(-1, 1, 1);
		}
		if (characterDirection[0] > 0){
			transform.localScale = new Vector3(1, 1, 1); 
		}
        
	}
}
