using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroDrop : MonoBehaviour {
    public bool dropped = false;
	// Use this for initialization
	void Start () {
		dropped = false;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public bool getDropped()
    {
        return dropped;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            dropped = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            dropped = true;
        }
    }
}
