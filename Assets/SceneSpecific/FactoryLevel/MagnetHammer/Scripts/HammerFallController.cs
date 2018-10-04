using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerFallController : MonoBehaviour, IHammerFallData, IBeginObstacleAction {
	/**
	 * TODO: Make the hammer fall and use the Grounded method with raycasting from before
	 * or use a clamp and move the position <- probably use this one.
	 */
	
	public float hammerFallSpeed;
	public float hammerRiseSpeed;
	public float hammerWaitBellowTime;

	private float currentSpeed;

	private bool isRising;
	private bool isLockingIn;
	
	public EditorMovementPath hammerPath;
	public int currentWaypointIndex;
	/*
	 * For enabling the start of the hammer's movement 
	 */
	private bool performObstacleAction;
	
	/*
	 * Public accessors
	 */
	public bool GetIsRising(){
		return isRising;
	}
	
	public bool GetLockingIn(){
		return isLockingIn;
	}

	public void BeginObstacleAction(){

	}
	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}


}
