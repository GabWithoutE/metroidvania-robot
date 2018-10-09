using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerFallController : MonoBehaviour, IHammerFallData, IBeginObstacleAction {
	/**
	 * TODO: use current and previous to determine the direction to determine the clamping
	 */
	private Transform currentWaypointTransform;
	// private Transform previousWaypointTransform;
	private WayPointDirectives currentWaypointDirectives;
	private IMagnetHammerAnimationTriggers animationController;

	private bool isRising;
	private bool isLockingIn;
	
	public EditorMovementPath hammerPath;
	public int currentWaypointIndex = 0;
	/*
	 * For enabling the start of the hammer's movement 
	 */
	private bool performObstacleAction;
	private bool waitForAnimation = true;
	
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
		performObstacleAction = true;
		IncrementWaypoint();
	}
	// Use this for initialization
	void Awake () {
		/*
		 * Ensure the hammer is in the starting position
		 */
		currentWaypointTransform = hammerPath.pathWaypointTransforms[currentWaypointIndex];
		transform.position = currentWaypointTransform.position;
	}
	
	void Start() {
		currentWaypointDirectives = currentWaypointTransform.GetComponent<WayPointDirectives>();
		animationController = GetComponentInParent<MagnetHammer>().AnimationController;
		// MagnetHammerMachineAnimationController.AnimationTiming OnAnimationLockRelease =  animationController.Mach_GetAnimationTimingEvent();
		// OnAnimationLockRelease += UnblockWaitForAnimation;
		MagnetHammerMachineAnimationController.AnimationCatchupSubscription catchupSubscription = animationController.Mach_GetAnimationCatchupSubscription();
		catchupSubscription.OnAnimationCaughtUp += UnblockWaitForAnimation;
		// print(OnAnimationLockRelease.ToString());
		// print (OnAnimationLockRelease);
		TriggerAnimationsBasedOnWayPoint();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// print (performObstacleAction);
		// print (currentWaypointIndex);

		/* TODO: block the movement based on time for the bottom part.
		 * Use animation event to block movement until animation catches up with movement.
		 */
		if (!performObstacleAction){
			return;
		}

		if(waitForAnimation){
			return;
		}

		float distance = Vector3.Distance(currentWaypointTransform.position, transform.position);
		transform.position = Vector3.MoveTowards(transform.position, currentWaypointTransform.position,
			Time.fixedDeltaTime * currentWaypointDirectives.MovementSpeed);
		if (distance <= 0){
			if (currentWaypointDirectives.WaypointType == WaypointType.End){
				currentWaypointIndex = 0;
				GetWaypointData();
				TriggerAnimationsBasedOnWayPoint();
			} else if (currentWaypointDirectives.WaypointType == WaypointType.Start){
				performObstacleAction = false;
			} else {
				IncrementWaypoint();
				TriggerAnimationsBasedOnWayPoint();
			}
			waitForAnimation = true;
		}
	}

	private void UnblockWaitForAnimation(){
		waitForAnimation = false;
	}

	private void IncrementWaypoint(){
		currentWaypointIndex++;
		GetWaypointData();	
	}

	private void GetWaypointData(){
		currentWaypointTransform = hammerPath.pathWaypointTransforms[currentWaypointIndex];
		currentWaypointDirectives = currentWaypointTransform.GetComponent<WayPointDirectives>();
	}

	private void TriggerAnimationsBasedOnWayPoint(){
		switch (currentWaypointIndex){
			case 0:
				break;
			case 1:
				animationController.Mach_TriggerUnlockMachinery();
				break;
			case 2:
				break;
			case 3:
				animationController.Mach_TriggerMagnetUp();
				break;
			case 4:
				animationController.Mach_TriggerMagnetLockin();
				break;
		}
	}

}
