using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour {
    private float fadeDuration;
    public CanvasGroup faderCanvasGroup;
    private bool startFading = false;
    public GameObject player;
    private BoxCollider2D boxCollider;

    void Start()
    {
        faderCanvasGroup.alpha = 0f;
        fadeDuration = 0.1f;
        boxCollider = GetComponent<BoxCollider2D>();
        Debug.Log(boxCollider.bounds.extents);
    }

    //Calculates how much to fade image by and maps that value to a value between 0 and 1
    private float CalculateFadeAmount()
    {
        float distancePlayerToCenter = Mathf.Abs(boxCollider.transform.position.x - player.transform.position.x);
        return 1 - (distancePlayerToCenter / boxCollider.bounds.extents.x);        
    }

    void Update()
    {
        //If player hits the collider, calculate how much to fade by
        if(startFading)
        {
            faderCanvasGroup.alpha = CalculateFadeAmount();
        }
    }

    //Only start fading when player hits the collider, otherwise don't fade or the math will not work
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            startFading = true;
        }        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            startFading = false;
        }
    }
}
