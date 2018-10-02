using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSet: MonoBehaviour {
    public MoveSetNames moveSetName;
	public GameObject lightAttackHorizontalRight;
	public GameObject lightAttackUpRight;
	public GameObject lightAttackDownRight;

	public GameObject heavyAttackHorizontalRight;
	public GameObject heavyAttackUpRight;
	public GameObject heavyAttackDownRight;

    public GameObject utilityAbility;
	public GameObject oneOffAbility;

	public GameObject GetLightAttackHorizontalRight(){
		return lightAttackHorizontalRight;
	}

	public GameObject GetLightAttackUpRight(){
		return lightAttackUpRight;
	}
    
	public GameObject GetLightAttackDownRight(){
		return lightAttackDownRight;
	}

	public GameObject GetHeavyAttackHorizontalRight(){
		return heavyAttackHorizontalRight;
	}

	public GameObject GetHeavyAttackUpRight(){
		return heavyAttackUpRight;
	}

	public GameObject GetHeavyAttackDownRight(){
		return heavyAttackDownRight;
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
