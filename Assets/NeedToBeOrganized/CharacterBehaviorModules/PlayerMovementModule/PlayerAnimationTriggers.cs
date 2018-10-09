using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour {
    Animator playerAnimator;
    bool knockedBack = false;
	// Use this for initialization
	void Start () {
        playerAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //TODO: Set knocked back to current knocked back state
        //If player is knocked back from a hit, set knocked back to true to trigger knocked back animation
        playerAnimator.SetBool("KnockBack", knockedBack);
        if(Input.GetKey("InputLightAttack"))
        {
            playerAnimator.SetTrigger("LightAttack");
        }
        if (Input.GetKey("InputHeavyAttack"))
        {
            playerAnimator.SetTrigger("HeavyAttack");
        }
        if (Input.GetKey("InputUtility"))
        {
            playerAnimator.SetTrigger("Utility");
        }
        if (Input.GetKey("InputLightAttack"))
        {
            playerAnimator.SetTrigger("LightAttack");
        }
        if (Input.GetKey("InputHorizontal"))
        {
            playerAnimator.SetTrigger("Horizontal");
        }
        if (Input.GetKey("InputVertical"))
        {
            playerAnimator.SetTrigger("Vertical");
        }
        if (Input.GetKey("InputLightAttack"))
        {
            playerAnimator.SetTrigger("LightAttack");
        }
    }
}
