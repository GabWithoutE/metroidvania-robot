using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterRangeSender : MonoBehaviour {
	//private OldCharacterStatesManager csm;
	//private TriggerColliderBroadcaster counterRangeBroadcaster;
	//private bool isSubscribed;

	//// Use this for initialization
	//void Start () {
	//	csm = GetComponentInParent<OldCharacterStatesManager> ();	
	//}
	
	//// Update is called once per frame
	//void Update () {
	//	if (!isSubscribed) {
	//		Subscribe ();
	//	}
	//}

	//private void Subscribe(){
	//	counterRangeBroadcaster = csm.GetTriggerColliderBroadcaster ("CounterRange");
	//	counterRangeBroadcaster.OnTriggerColliderEnter += SetToCounterable;
	//}

	//void SetToCounterable(Collider col, string colliderName){
	//	if (col.gameObject.tag == "Enemy") {
	//		col.gameObject.BroadcastMessage ("SetToCounterable");
	//	}
	//}
}
