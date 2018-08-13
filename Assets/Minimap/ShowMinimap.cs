using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Displays minimap when user presses tab key

public class ShowMinimap : MonoBehaviour {

    void Start()
    {
        //Do not show minimap by default
        gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Tab))
        {
            gameObject.SetActive(true);
            Debug.Log("Show minimap");
        }
        else
        {
            //gameObject.SetActive(false);
        }
	}
}
