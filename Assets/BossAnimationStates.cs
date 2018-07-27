using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationStates : MonoBehaviour {
    private Animator bossAnimator;
    private int walkingHash = Animator.StringToHash("Walking");
    private int jumpingHash = Animator.StringToHash("Jumping");
    private int fallingHash = Animator.StringToHash("Falling");
    private int meleeHash = Animator.StringToHash("Melee");
    private int hammerHash = Animator.StringToHash("Hammer");

    // Use this for initialization
    void Start () {
        bossAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
