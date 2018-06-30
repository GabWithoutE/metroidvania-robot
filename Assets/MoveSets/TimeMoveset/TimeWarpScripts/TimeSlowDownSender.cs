using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowDownSender : MonoBehaviour {

    public float slowdownDecimal;
    private float effectDuration;
    public ProjectileDespawnTime despawnAfterTime;

    // Use this for initialization
	void Start () {
        despawnAfterTime = GetComponent<ProjectileDespawnTime>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
        float[] effect = 
            new float[] { slowdownDecimal, despawnAfterTime.GetRemainingTime() };
        collision.gameObject.SendMessage("TimeWarpSlowDown", effect);
	}

	void OnTriggerExit2D(Collider2D collision)
	{
        collision.gameObject.SendMessage("RestoreSpeed");    
	}

}
