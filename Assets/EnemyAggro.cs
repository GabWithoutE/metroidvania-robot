using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour {
    public Transform player;
    public Transform enemyDetectStart;
    public Transform enemyDetectEnd;
    public int detectRange;
    public int chaseSpeed;
    public bool detected = false;
    public Vector2 playerDirection;
	// Use this for initialization
	void Start () {
        enemyDetectStart.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        enemyDetectEnd.position = new Vector3(enemyDetectStart.position.x + detectRange ,enemyDetectStart.position.y, 0);
	}
	
	// Update is called once per frame
	void Update () {
        Raycasting();
        if(detected)
        {
            //Make enemy face in player's direction
            transform.LookAt(player);
            //Move towards player
            Vector3 targetPosition = player.transform.position;
            transform.position += transform.forward * chaseSpeed * Time.deltaTime;
        }
    }

    void Raycasting()
    {
        detected = Physics2D.Linecast(enemyDetectStart.position, enemyDetectEnd.position, 1 << LayerMask.NameToLayer("Player"));
        /*
        if (Physics2D.Linecast(enemyDetectStart.position, enemyDetectEnd.position, 1 << LayerMask.NameToLayer("Player")))
        {
            //if()
            //{
                detected = true;
           // }
           // else
            //{
                detected = false;
            //}
        }
        else
        {
            detected = false;
        }
        */
    }
}
