using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitSteam : MonoBehaviour {
    public float steamDuration;
    public float steamInterval;
    private bool prevState;
    CapsuleCollider2D capCol;
	// Use this for initialization
	void Start () {
        capCol = GetComponent<CapsuleCollider2D>();
        capCol.enabled = false;
        prevState = capCol.enabled;
	}
	
	// Update is called once per frame
	void Update () {
        ToggleSteam();
    }    

    private void ToggleSteam()
    {
        if(prevState == capCol.enabled)
        {
            if (capCol.enabled)
            {
                prevState = !prevState;
                StartCoroutine(SteamDuration());
            }
            else
            {
                prevState = !prevState;
                StartCoroutine(SteamInterval());
            }            
        }        
    }

    IEnumerator SteamDuration()
    {
        yield return new WaitForSeconds(steamDuration);
        capCol.enabled = false;
    }

    IEnumerator SteamInterval()
    {
        yield return new WaitForSeconds(steamInterval);
        capCol.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.enabled)
        {
            if (col.gameObject.tag == "Player")
            {
                //Damage player
                Debug.Log("Player blasted by steam");
            }
        }        
    }
}
