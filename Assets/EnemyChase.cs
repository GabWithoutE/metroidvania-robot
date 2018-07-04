using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour {
    public int chaseSpeed;
    public Transform Player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Make enemy face in player's direction
        transform.LookAt(Player);
        //Move towards player
        Vector3 targetPosition = Player.transform.position;
        transform.position += transform.forward * chaseSpeed * Time.deltaTime;
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref chaseSpeed, smoothTime);
    }
}
