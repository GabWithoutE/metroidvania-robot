using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroInit : MonoBehaviour {
    public bool initiated;
	// Use this for initialization
	void Start () {
        initiated = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool getInitiated()
    {
        return initiated;
    }

    public void setInitiated(bool initInput)
    {
        initiated = initInput;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            initiated = true;
        }
    }
}
