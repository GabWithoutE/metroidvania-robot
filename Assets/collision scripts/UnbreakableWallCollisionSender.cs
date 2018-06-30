using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnbreakableWallCollisionSender : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Projectile") {
			col.gameObject.SendMessage ("HitWall");
		}
	}

}
