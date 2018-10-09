using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpellDirectionalSpawnLocations : MonoBehaviour {

	public Vector2 upSpawnLocation;
	public Vector2 upRightSpawnLocation;
	public Vector2 rightSpawnLocation;
	public Vector2 downRightSpawnLocation;
	public Vector2 downSpawnLocation;
	public Vector2 downLeftSpawnLocation;
	public Vector2 leftSpawnLocation;
	public Vector2 upLeftSpawnLocation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector2 GetUpSpawnLocation(){
		return upSpawnLocation;
	}
	public Vector2 GetUpRightSpawnLocation() {
		return upRightSpawnLocation;
	}
	public Vector2 GetRightSpawnLocation (){
		return rightSpawnLocation;
	}
	public Vector2 GetDownRightSpawnLocation(){
		return downRightSpawnLocation;
	}
	public Vector2 GetDownSpawnLocation(){
		return downSpawnLocation;
	}
	public Vector2 GetDownLeftSpawnLocation(){
		return downLeftSpawnLocation;
	}
	public Vector2 GetLeftSpawnLocation(){
		return leftSpawnLocation;
	}
	public Vector2 GetUpLeftSpawnLocation(){
		return upLeftSpawnLocation;
	}


}
