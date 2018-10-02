using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * TODO:
 * 1. trigger the unlock and drop the hammer. keep track of starting height
 * 2. detect when the hammer hits the ground. delay for a couple of seconds, then raise the hammer
 *    triggering the necessary things.
 * 3. when the hammer reaches the original height, trigger the lockin animation
 * 
 */ 

public class MagnetHammerControl : MonoBehaviour, ISynchronizableEnvironmentPiece {

	/*
	 * Make the magnet hammer drop and go through it's cycle
	 */
	public void BeginAction(){

	}
	/*
	 * Return the Action Cycle time in GameTime (based on frames, not real time)
	 */
	public float ActionCycleTime(){
		return 0;
	}

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
