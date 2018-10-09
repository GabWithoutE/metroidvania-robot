using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectileCollisionReceiver : MonoBehaviour {
	void HitWall() {
		Destroy (gameObject);
	}

     
}
