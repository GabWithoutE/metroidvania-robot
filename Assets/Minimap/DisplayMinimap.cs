using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMinimap : MonoBehaviour
{
    public Camera main;
    public Camera minimap;

    void Start()
    {
        main.enabled = true;
        minimap.enabled = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            main.enabled = false;
            minimap.enabled = true;
        }
        else
        {
            main.enabled = true;
            minimap.enabled = false;
        }
    }
}