using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTile : MonoBehaviour {
    private float timer;
    private float duration;
    public bool currentlyStepping;
	// Use this for initialization
	void Start () {
        timer = 0;
        duration = 3;
        currentlyStepping = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentlyStepping)
        {
            timer += Time.deltaTime;
            if(timer > duration)
            {
                Destroy(gameObject);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            currentlyStepping = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            currentlyStepping = false;
            timer = 0;
        }
    }

    private void CastRayUp()
    {

    }
}
