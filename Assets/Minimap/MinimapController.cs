using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour {
    public CanvasGroup minimapCanvasGroup;
    // Use this for initialization
    void Start () {
        minimapCanvasGroup.alpha = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Tab))
        {
            minimapCanvasGroup.alpha = 1;
        }
        else
        {
            minimapCanvasGroup.alpha = 0;
        }
    }
}
