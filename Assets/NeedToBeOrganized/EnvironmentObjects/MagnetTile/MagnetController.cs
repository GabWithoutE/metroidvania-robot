using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Turns the magnet on and off. Changes the sprite as well

public class MagnetController : MonoBehaviour {
    private BoxCollider2D col;
    public Sprite switchedOn;
    public Sprite switchedOff;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        //Default to switched on
        TurnOnMagnet();
        //TurnOffMagnet();
    }

    public void TurnOnMagnet()
    {
        col.enabled = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = switchedOn;
    }

    public void TurnOffMagnet()
    {
        col.enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = switchedOff;
    }

    public bool GetMagnetState()
    {
        return col.enabled;
    }
}
