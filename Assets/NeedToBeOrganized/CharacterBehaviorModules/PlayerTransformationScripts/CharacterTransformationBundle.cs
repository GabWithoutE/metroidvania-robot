using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTransformationBundle : MonoBehaviour {
	public GameObject caster;
	public GameObject movement;

	public GameObject GetCaster(){
		return caster;
	}

	public GameObject GetMovementObject(){
		return movement;
	}
}
