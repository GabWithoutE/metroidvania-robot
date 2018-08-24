using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeWhenHit : MonoBehaviour {
    public bool hit = false;
    private CharacterStatesManager statesManager;
    private CharacterState hitState;

    void Awake()
    {
        statesManager = GetComponentInParent<CharacterStatesManager>();
        hitState = new CharacterState(ConstantStrings.HIT_STATE, hit);

        statesManager.RegisterCharacterState(hitState.name, hitState);
    }
    // Use this for initialization
    void Start () {
        CharacterState.CharacterStateSubscription healthStateSubscription = statesManager.GetCharacterStateSubscription(ConstantStrings.CURRENT_HEALTH);
        healthStateSubscription.OnStateChanged += CheckHit;
    }
	
	// Update is called once per frame
	void Update () {
        hit = (bool)hitState.GetStateValue();
    }

    private void CheckHit(object currentHealth)
    {
        if ((float)currentHealth <= 0)
        {
            
        }
    }
}
