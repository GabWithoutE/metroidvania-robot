using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationStates : MonoBehaviour {
    private ICharacterStateObserver stateObserver;
    private Animator bossAnimator;
    private int walkingHash;
    private int jumpingHash;
    private int fallingHash;
    private int meleeHash;
    private int hammerHash;

    void Awake()
    {
        stateObserver = GetComponentInParent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;
        walkingHash = Animator.StringToHash("Walking");
        jumpingHash = Animator.StringToHash("Jumping");
        fallingHash = Animator.StringToHash("Falling");
        meleeHash = Animator.StringToHash("Melee");
        hammerHash = Animator.StringToHash("Hammer");
    }

    // Use this for initialization
    void Start () {
        bossAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
