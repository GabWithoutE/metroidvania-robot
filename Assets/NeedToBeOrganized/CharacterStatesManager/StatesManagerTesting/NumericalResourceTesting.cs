using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericalResourceTesting : MonoBehaviour {
//	private OldCharacterStatesManager csm;
//	private OldCharacterStatesManager.NumericalResource testNumericalResource;
//	private OldCharacterStatesManager.NumericalResource.NumericalResourceSubscription testNumericalResourceSubscription;
//	private bool registered;
//	private OldCharacterStatesManager.NumericalResource.ReadOnlyNumericalResource readOnlyTestNumericalResource;
//	// Use this for initialization
//	void Start () {
//		csm = GetComponentInParent<OldCharacterStatesManager> ();
//		testNumericalResource = new OldCharacterStatesManager.NumericalResource ("TestNumericalResource", 100, 100);
//		StartCoroutine (SetValues());

//	}

//	IEnumerator SetValues(){
//		yield return new WaitForSeconds (3);
//		testNumericalResource.SetMax (50);
//		testNumericalResource.SetCurrentToMax ();
//	}
	
//	// Update is called once per frame
//	void Update () {
//		if (!registered) {
//			csm.RegisterNumericalResource (testNumericalResource.name, testNumericalResource);
//			readOnlyTestNumericalResource = 
//				csm.GetReadOnlyNumericalResource ("TestNumericalResource");
//			Subscribe ();
//			registered = true;
//		}
////		Debug.Log ("Currrent: " + readOnlyTestNumericalResource.GetCurrent);
////		Debug.Log ("Maximum: " + readOnlyTestNumericalResource.GetMaximum);
	//}

	//private void Subscribe (){
	//	testNumericalResourceSubscription = csm.GetNumericalResourceSubscription (testNumericalResource.name);
	//	testNumericalResource.OnCurrentChanged += SayHelloOnCurrentChanged;
	//	testNumericalResource.OnMaximumChanged += SayHelloOnMaxChanged;
	//}

	//void SayHelloOnCurrentChanged(int delta){
	//	Debug.Log ("HELLO, CurrentChanged");
	//}

	//void SayHelloOnMaxChanged(int delta){
	//	Debug.Log ("HELLO, MaxChanged");
	//}
}
