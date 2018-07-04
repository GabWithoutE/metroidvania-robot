using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour {
    public Transform Player;
    public int detectRange;
    public int chaseSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Calculate distance between player and enemy
        float distance = Vector3.Distance(transform.position, Player.position);
        //If player is within detection range, aggro enemy
        if(distance <= detectRange)
        {
            //Make enemy face in player's direction
            transform.LookAt(Player);
            //Move towards player
            Vector3 targetPosition = Player.transform.position;
            transform.position += transform.forward * chaseSpeed * Time.deltaTime;
        }
    }
}
