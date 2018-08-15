using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Turns the magnet on and off.

public class MagnetController : MonoBehaviour {
    private BoxCollider2D col;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        col.enabled = true;
    }

    public void TurnOnMagnet()
    {
        col.enabled = true;
    }

    public void TurnOffMagnet()
    {
        col.enabled = false;
    }

    public bool GetMagnetState()
    {
        return col.enabled;
    }
}
