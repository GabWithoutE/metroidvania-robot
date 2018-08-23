using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour {
    public CanvasGroup faderCanvasGroup;
    private bool startFading = false;       //Boolean to figure out when player enteres area to fade screen
    public GameObject player;               //Reference to player
    private BoxCollider2D boxCollider;      //Collider to detect when player is in area to fade screen
    private ChangeImageScale changeImageScale;  //Used to change the scale of the image
    public float defaultScale;  //Default scale of the image
    public float minScale;  //The minimum scale the image can have before the edge of the image becomes visible to player.


    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        changeImageScale = faderCanvasGroup.GetComponentInChildren<ChangeImageScale>();
        changeImageScale.ChangeScale(defaultScale);   //Default image scale
        faderCanvasGroup.alpha = 1f;                    //Have image at full alpha always
    }

    //Calculates how much to fade image by and maps that value to a value between 0 and 1
    private float CalculateFadeAmount()
    {
        //Calculates distance from player to center of boxcollider
        float distancePlayerToCenter = Mathf.Abs(boxCollider.transform.position.x - player.transform.position.x);
        //Clamps value between minimum scale and default scale
        return Mathf.Clamp(((distancePlayerToCenter / boxCollider.bounds.extents.x)) * defaultScale, minScale, defaultScale);        
    }

    void Update()
    {
        //If player hits the collider, calculate how much to fade by
        if(startFading)
        {
            changeImageScale.ChangeScale(CalculateFadeAmount());
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
