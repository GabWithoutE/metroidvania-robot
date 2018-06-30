using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using PlayerInputNameSpace;

public class MeleeWeaponKeyboardInput : MonoBehaviour {
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

	///**
	// * If in the middle of attacking in 1 direction (locked), unable to attack in another or
	// * in the same direction again until it the duratin of the attack has passed.
	// */ 
	//void Update () {
	//	if (!attackDisabled) {
	//		if (Input.GetKeyDown (KeyCode.W)) {
	//			StartCoroutine (SetTrueForDuration(isAttackingUpward));
	//		} else if (Input.GetKeyDown (KeyCode.D)) {
	//			StartCoroutine (SetTrueForDuration(isAttackingForward));
	//		} else if (Input.GetKeyDown (KeyCode.S)) {
	//			StartCoroutine (SetTrueForDuration(isAttackingDownward));
	//		}
	//	}
	//}

	///**
	// * Use the duration of the collider's activation to set the collider to true for that
	// * duration.
	// */ 
	//IEnumerator SetTrueForDuration(OldCharacterStatesManager.BinaryBehaviorState attackState){
	//	attackDisabled = true;
	//	attackState.SetStateTrue ();
	//	yield return new WaitForSeconds (attackDuration);
	//	attackState.SetStateFalse ();
	//	attackDisabled = false;
	//}
}
