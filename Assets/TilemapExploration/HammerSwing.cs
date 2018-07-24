using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSwing : MonoBehaviour {
    public Vector2 swingVelocity;
    private Rigidbody2D rgb2d;
    // Use this for initialization
    void Start () {
        //swingVelocity = new Vector2(0, -5);
        rgb2d = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        rgb2d.MovePosition(rgb2d.position + swingVelocity * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject != null)
        {
            if (col.gameObject.tag == "Player")
            {
                //Do damage to player
                Debug.Log("Player hit by hammer");
            }
            else
            {
                //Reverse direction of swing
                swingVelocity.x *= -1;
                swingVelocity.y *= -1;
            }
        }        
    }
}
