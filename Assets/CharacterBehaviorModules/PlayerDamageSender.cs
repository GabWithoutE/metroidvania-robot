using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageSender : MonoBehaviour {
	//private OldCharacterStatesManager csm;
	//private TriggerColliderBroadcaster damageBroadcaster;
	//private bool isSubscribed;
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
	//	damageBroadcaster = csm.GetTriggerColliderBroadcaster ("DamageCollider");
	//	damageBroadcaster.OnTriggerColliderEnter += SendDamage;
	//}

	//void SendDamage(Collider col, string colliderName){
	//	if (col.gameObject.tag == "Enemy") {
	//		col.gameObject.BroadcastMessage ("TakeDamage", 1);
	//	}
	//}
}
