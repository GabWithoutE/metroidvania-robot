using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTileActivate : MonoBehaviour {        
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            //Load up next level
            Debug.Log("Load next level");
        }
    }    
}
