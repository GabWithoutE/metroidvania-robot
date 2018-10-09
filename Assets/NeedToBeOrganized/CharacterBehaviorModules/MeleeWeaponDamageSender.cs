using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponDamageSender : MonoBehaviour {
	//public int damage = 1;
	//private OldCharacterStatesManager csm;
	//private TriggerColliderBroadcaster forwardBroadcaster;
	//private TriggerColliderBroadcaster upwardBroadcaster;
	//private TriggerColliderBroadcaster downwardBroadcaster;
	//private bool isSubscribed;

	//private OldCharacterStatesManager.BinaryBehaviorState.OnStateChange forward;
	//// Use this for initialization
	//void Start () {
	//	csm = GetComponentInParent<OldCharacterStatesManager> ();
	//}
	
	//// Update is called once per frame
	//void Update () {
	//	if (!isSubscribed) {
	//		Subscribe ();
	//		isSubscribed = true;
	//	}
	//}

	//private void Subscribe(){
	//	forwardBroadcaster = csm.GetTriggerColliderBroadcaster ("ForwardCollider");
	//	upwardBroadcaster = csm.GetTriggerColliderBroadcaster ("UpwardCollider");
	//	downwardBroadcaster = csm.GetTriggerColliderBroadcaster ("DownwardCollider");

	//	// Allow the attack to only deal damage 1 time.
	//	csm.GetBinaryBehaviorStateSubcription ("AttackForward").OnTrue += CanDealDamageForward;
	//	csm.GetBinaryBehaviorStateSubcription ("AttackUpward").OnTrue += CanDealDamageUpward;
	//	csm.GetBinaryBehaviorStateSubcription ("AttackDownward").OnTrue += CanDealDamageDownward;

	//	// Stop allowing the attack to deal damage after the duration of the attack.
	//	csm.GetBinaryBehaviorStateSubcription ("AttackForward").OnFalse += CantDealDamageForward;
	//	csm.GetBinaryBehaviorStateSubcription ("AttackUpward").OnFalse += CantDealDamageUpward;
	//	csm.GetBinaryBehaviorStateSubcription ("AttackDownward").OnFalse += CantDealDamageDownward;

	//}


	///**
	// * When a trigger collider for the melee attack is triggered, this method is called.
	// */
	//void SendDamage (Collider col, string colliderName) {
	//	col.gameObject.BroadcastMessage ("TakeDamage", damage);
	//	// remove the ability to deal damage after colliding
	//	if (colliderName == "ForwardCollider") {
	//		forwardBroadcaster.OnTriggerColliderStay -= SendDamage;
	//	}
	//	if (colliderName == "UpwardCollider") {
	//		upwardBroadcaster.OnTriggerColliderStay -= SendDamage;
	//	}
	//	if (colliderName == "DownwardCollider"){
	//		downwardBroadcaster.OnTriggerColliderStay -= SendDamage;
	//	}
	//}
			
	//void CanDealDamageForward (){
	//	forwardBroadcaster.OnTriggerColliderStay += SendDamage;
	//}

	//void CanDealDamageDownward(){
	//	downwardBroadcaster.OnTriggerColliderStay += SendDamage;
	//}

	//void CanDealDamageUpward(){
	//	upwardBroadcaster.OnTriggerColliderStay += SendDamage;
	//}

	//void CantDealDamageForward(){
	//	forwardBroadcaster.OnTriggerColliderStay -= SendDamage;
	//}

	//void CantDealDamageDownward(){
	//	downwardBroadcaster.OnTriggerColliderStay -= SendDamage;
	//}

	//void CantDealDamageUpward(){
	//	upwardBroadcaster.OnTriggerColliderStay -= SendDamage;
	//}
		
		
}
