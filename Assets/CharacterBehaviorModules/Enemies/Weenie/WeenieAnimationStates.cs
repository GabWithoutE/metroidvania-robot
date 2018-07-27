using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeenieAnimationStates : MonoBehaviour {
    private Animator weenieAnimator;
    private ICharacterStateObserver stateObserver;
    private int aggroedHash;
    private bool aggroed;

    void Awake()
    {
        weenieAnimator = GetComponent<Animator>();
        stateObserver = GetComponentInParent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;
        aggroedHash = Animator.StringToHash("Aggroed");
    }

    // Use this for initialization
    void Start () {
        aggroed = false;
    }
	
	// Update is called once per frame
	void Update () {
        //If weenie is aggroed, set animation parameter to true
		if(aggroed)
        {
            weenieAnimator.SetBool(aggroedHash, true);
        }
        else
        {
            weenieAnimator.SetBool(aggroedHash, false);
        }
	}
}
