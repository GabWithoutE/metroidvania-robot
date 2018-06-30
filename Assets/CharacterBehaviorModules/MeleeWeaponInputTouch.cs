using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using PlayerInputNameSpace;

public class MeleeWeaponInputTouch : MonoBehaviour {
	//private OldCharacterStatesManager csm;
	//private OldCharacterStatesManager.BinaryBehaviorState isAttackingForward;
	//private OldCharacterStatesManager.BinaryBehaviorState isAttackingUpward;
	//private OldCharacterStatesManager.BinaryBehaviorState isAttackingDownward;

	///**
	// * This state represents whether one of the directional attacking binary behavior states is true
	// */ 
	//private bool attackDisabled;
	//public float attackDuration;


	//// Use this for initialization
	//void Start () {
	//	csm = GetComponentInParent<OldCharacterStatesManager> ();
	//	isAttackingForward = new OldCharacterStatesManager.BinaryBehaviorState ("AttackForward");
	//	isAttackingUpward = new OldCharacterStatesManager.BinaryBehaviorState ("AttackUpward");
	//	isAttackingDownward = new OldCharacterStatesManager.BinaryBehaviorState ("AttackDownward");
	//	csm.RegisterCharacterBinaryBehaviorState (isAttackingForward.name, isAttackingForward);
	//	csm.RegisterCharacterBinaryBehaviorState (isAttackingUpward.name, isAttackingUpward);
	//	csm.RegisterCharacterBinaryBehaviorState (isAttackingDownward.name, isAttackingDownward);
	//}

	//// Update is called once per frame
	//void Update () {
	//	if (!attackDisabled) {
	//		float horizontalAttackAxisValue = InputManager.GetAxisValue("HorizontalAttackAxis");
	//		float verticalAttackAxisValue = InputManager.GetAxisValue ("VerticalAttackAxis");
	//		if (horizontalAttackAxisValue != 0 && verticalAttackAxisValue != 0) {
	//			Vector2 swipeVector = new Vector2 (horizontalAttackAxisValue, verticalAttackAxisValue);
	//			float angle = Vector2.SignedAngle (swipeVector, Vector2.up);
	//			if (angle > -45 && angle < 45) {
	//				StartCoroutine (SetTrueForDuration (isAttackingUpward));
	//				Debug.Log ("Attacking Upward");
	//			} else if (angle > 45 && angle < 135) {
	//				StartCoroutine (SetTrueForDuration (isAttackingForward));
	//				Debug.Log ("attacking forward");
	//			} else if (angle > 135 || angle < -135) {
	//				StartCoroutine (SetTrueForDuration (isAttackingDownward));
	//				Debug.Log ("attacking downward");
	//			}
	//		}
	//	}

	//}

	//IEnumerator SetTrueForDuration (OldCharacterStatesManager.BinaryBehaviorState attackState) {
	//	attackDisabled = true;
	//	attackState.SetStateTrue ();
	//	yield return new WaitForSeconds (attackDuration);
	//	attackState.SetStateFalse ();
	//	attackDisabled = false;	
	//}
}
