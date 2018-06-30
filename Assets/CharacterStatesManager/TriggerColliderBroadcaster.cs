using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColliderBroadcaster : MonoBehaviour {
	//public string triggerBroadcasterKeyString;
	//public delegate void OnTriggerBroadcast(Collider col, string triggerBroadcasterName);
	//public event OnTriggerBroadcast OnTriggerColliderEnter;
	//public event OnTriggerBroadcast OnTriggerColliderStay;
	//public event OnTriggerBroadcast OnTriggerColliderExit;

	//private OldCharacterStatesManager characterStatesManager;

	//void Start(){
	//	if (triggerBroadcasterKeyString == "") {
	//		throw new StringIsNullException ("String triggerBroadcasterKey in TriggerColliderBroadcaster is null");
	//	} else {
	//		characterStatesManager = GetComponentInParent<OldCharacterStatesManager> ();

	//		characterStatesManager.RegisterTriggerColliderBroadcaster (triggerBroadcasterKeyString, this);
	//	}
	//}

	//void Update(){
		
	//}


	///**
	// * Broadcast trigger's enter to all subscribers
	// */ 
	//void OnTriggerEnter(Collider col){
	//	if (OnTriggerColliderEnter != null) {
	//		OnTriggerColliderEnter (col, triggerBroadcasterKeyString);
	//	}
	//}

	///**
	// * Broadcast trigger's stay to all subscribers
	// */ 
	//void OnTriggerStay(Collider col){
	//	if (OnTriggerColliderStay != null) {
	//		OnTriggerColliderStay (col, triggerBroadcasterKeyString);
	//	}
	//}

	///**
	// * Broadcast trigger's exit t- all subscribers
	// */ 
	//void OnTriggerExit(Collider col){
	//	if (OnTriggerColliderExit != null) {
	//		OnTriggerColliderExit (col, triggerBroadcasterKeyString);
	//	}
	//}

	///**
	// * Remove the broadcaster entry from the dictionary when this broadcaster is destroyed
	// */ 
	//void OnDestroy(){
	//	characterStatesManager.DeregisterTriggerColliderBroadcaster (triggerBroadcasterKeyString);
	//}

}
