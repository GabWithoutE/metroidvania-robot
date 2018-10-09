using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColliderBroadcaster2D : MonoBehaviour {
 //   public TriggerColliderBroadcastersNames triggerBroadcasterKeyName;
 //   public delegate void OnTriggerBroadcast(Collider2D col,TriggerColliderBroadcastersNames triggerBroadcasterName);
	//public event OnTriggerBroadcast OnTriggerColliderEnter;
	//public event OnTriggerBroadcast OnTriggerColliderStay;
	//public event OnTriggerBroadcast OnTriggerColliderExit;

	//private CharacterStatesManager characterStatesManager;

	//void Start(){
 //       if (triggerBroadcasterKeyName.ToString() == "") {
	//		throw new StringIsNullException ("String triggerBroadcasterKey in TriggerColliderBroadcaster is null");
	//	} else {
 //           characterStatesManager = GetComponentInParent<CharacterStatesManager> ();

	//		characterStatesManager.RegisterTriggerColliderBroadcaster (triggerBroadcasterKeyName, this);
	//	}
	//}

	//void Update(){

	//}


	///**
	// * Broadcast trigger's enter to all subscribers
	// */ 
	//void OnTriggerEnter2D(Collider2D col){
	//	if (OnTriggerColliderEnter != null) {
	//		OnTriggerColliderEnter (col, triggerBroadcasterKeyName);
	//	}
	//}

	///**
	// * Broadcast trigger's stay to all subscribers
	// */ 
	//void OnTriggerStay2D(Collider2D col){
	//	if (OnTriggerColliderStay != null) {
	//		OnTriggerColliderStay (col, triggerBroadcasterKeyName);
	//	}
	//}

	///**
	// * Broadcast trigger's exit t- all subscribers
	// */ 
	//void OnTriggerExit2D(Collider2D col){
	//	if (OnTriggerColliderExit != null) {
	//		OnTriggerColliderExit (col, triggerBroadcasterKeyName);
	//	}
	//}

	///**
	// * Remove the broadcaster entry from the dictionary when this broadcaster is destroyed
	// */ 
	//void OnDestroy(){
	//	characterStatesManager.DeregisterTriggerColliderBroadcaster (triggerBroadcasterKeyName);
	//}
}
