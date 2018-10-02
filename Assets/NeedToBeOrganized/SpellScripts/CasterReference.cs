using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterReference : MonoBehaviour {
	private GameObject myCaster;

	public void SetCaster(GameObject caster){
		myCaster = caster;
	}

	public GameObject GetCaster(){
		return myCaster;
	}
        
    
}
