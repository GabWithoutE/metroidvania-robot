using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSet: MonoBehaviour {
    public MoveSetNames moveSetName;
	public GameObject lightAttack;
	public GameObject heavyAttack;
    public GameObject utilityAbility;
	public GameObject oneOffAbility;

	public GameObject GetLightAttack(){
		return lightAttack;
	}

	public GameObject GetHeavyAttack(){
		return heavyAttack;
	}

	public GameObject GetUtilityAbility(){
		return utilityAbility;
	}

	public GameObject GetOneOffAbility(){
		return oneOffAbility;
	}

    public MoveSetNames GetMoveSetName(){
        return moveSetName;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public enum MoveSetNames{
    Normal,
    TimeWarp
}
