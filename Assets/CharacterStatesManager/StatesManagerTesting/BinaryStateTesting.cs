using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryStateTesting : MonoBehaviour {
//	private OldCharacterStatesManager csm;
//	private OldCharacterStatesManager.BinaryBehaviorState testBinaryState;
//	private OldCharacterStatesManager.BinaryBehaviorState.BinaryBehaviorStateSubscription testBinaryStateSubscription;
//	private bool registered;
//	// Use this for initialization
//	void Start () {
//		csm = GetComponentInParent<OldCharacterStatesManager> ();
//		testBinaryState = new OldCharacterStatesManager.BinaryBehaviorState ("TestBinaryState");
//		StartCoroutine (SetTrue());
//	}
//	IEnumerator SetTrue(){
//		yield return new WaitForSeconds (3);
//		testBinaryState.SetStateTrue ();
//	}
	
//	// Update is called once per frame
//	void Update () {
//		if (!registered) {
//			csm.RegisterCharacterBinaryBehaviorState (testBinaryState.name, testBinaryState);
//			Subscribe ();
//			registered = true;
//		}
////		Debug.Log (csm.GetCharacterBinaryBehaviorState ("TestBinaryState"));
//	}

//	private void Subscribe (){
//		testBinaryStateSubscription = csm.GetBinaryBehaviorStateSubcription (testBinaryState.name);
//		testBinaryStateSubscription.OnTrue += SayHelloOnTrue;
////		testBinaryStateSubscription.OnTrue -= SayHelloOnTrue;
	//}

	//void SayHelloOnTrue(){
	//	Debug.Log ("HELLO");
	//}
}
